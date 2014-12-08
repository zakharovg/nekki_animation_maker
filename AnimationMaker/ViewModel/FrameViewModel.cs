using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AnimationMaker.Messages;
using AnimationMaker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class FrameViewModel : ViewModelBase, IFrameViewModel
	{
		private readonly object _token;
		private readonly IFigureViewModelFactory _figureFactory;
		private EditMode _mode;

		private readonly ObservableCollection<IFigureViewModel> _figures;
		private readonly List<IFigureViewModel> _selectedItems = new List<IFigureViewModel>();

		public FrameViewModel(IFigureViewModelFactory figureFactory, IMessenger messenger, IEnumerable<IFigureViewModel> figures, object token)
			: base(messenger)
		{
			if (token == null) throw new ArgumentNullException("token");
			if (figureFactory == null) throw new ArgumentNullException("figureFactory");
			if (figures == null) throw new ArgumentNullException("figures");

			SetupCommands();

			_token = token;
			_figureFactory = figureFactory;
			_figures = new ObservableCollection<IFigureViewModel>(figures.ToList());

			MessengerInstance.Register<FigureSelectionMessage>(this, _token, HandleItemSelection);
		}

		public ObservableCollection<IFigureViewModel> Figures
		{
			get { return _figures; }
		}

		public void HandleItemSelection(FigureSelectionMessage message)
		{
			if (message.IsSelected)
				_selectedItems.Add(message.Sender);
			else
				_selectedItems.Remove(message.Sender);
		}

		private void SetupCommands()
		{
			AddPoint = new RelayCommand<Point>(point =>
			{
				var viewModel = _figureFactory.CreatePoint(point, _token);
				_figures.Add(viewModel);
			});
			AddEdge = new RelayCommand<Tuple<Point, Point>>(tuple =>
			{
				var points = _figures.OfType<IPointViewModel>().ToList();
				var startPointViewModel = points.First(p => p.Point.Equals(tuple.Item1));
				var endPointViewModel = points.First(p => p.Point.Equals(tuple.Item2));
				var viewModel = _figureFactory.CreateEdge(startPointViewModel, endPointViewModel, _token);

				_figures.Add(viewModel);
			});
			RemoveSelected = new RelayCommand(RemoveSelectedFigures);
		}

		private void RemoveSelectedFigures()
		{
			foreach (var figure in _selectedItems)
				_figures.Remove(figure);
		}

		public ICommand RemoveSelected { get; private set; }

		public ICommand AddPoint { get; private set; }

		public ICommand AddEdge { get; private set; }

		public Frame GetFrame()
		{
			var points = _figures.OfType<IPointViewModel>().Select(f => f.Point).ToArray();
			var edges = _figures.OfType<IEdgeViewModel>().Select(e => e.GetEdge()).ToArray();
			return new Frame(points, edges);
		}

		public override void Cleanup()
		{
			base.Cleanup();
			MessengerInstance.Unregister<FigureSelectionMessage>(this);
		}
	}
}