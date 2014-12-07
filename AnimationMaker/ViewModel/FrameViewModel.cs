using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using AnimationMaker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class FrameViewModel : ViewModelBase, IFrameViewModel
	{
		private Frame _frame;
		private EditMode _mode;
		private readonly ObservableCollection<IFigureViewModel> _figures;

		public FrameViewModel(Frame frame, IMessenger messenger)
			: base(messenger)
		{
			if (frame == null) throw new ArgumentNullException("frame");
			Frame = frame;

			AddPoint = new RelayCommand<Point>(point => _frame.AddPoint(point), point => _mode == EditMode.AddPoint);
			_figures = new ObservableCollection<IFigureViewModel>();
			_frame.Points.CollectionChanged += OnPointsOnCollectionChanged;
		}

		private void OnPointsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			switch (notifyCollectionChangedEventArgs.Action)
			{
				case NotifyCollectionChangedAction.Add:
					var point = notifyCollectionChangedEventArgs.NewItems.OfType<Point>().First();
					_figures.Add(new PointViewModel(point, MessengerInstance));
					break;
				case NotifyCollectionChangedAction.Remove:
					var pointToRemove = notifyCollectionChangedEventArgs.OldItems.OfType<Point>().First();
					var viewModelToRemove = _figures.OfType<IPointViewModel>().First(p => p.Point.Equals(pointToRemove));
					_figures.Remove(viewModelToRemove);
					break;
				case NotifyCollectionChangedAction.Replace:
					var pointToAdd = notifyCollectionChangedEventArgs.NewItems.OfType<Point>().First();
					var pointToReplace = notifyCollectionChangedEventArgs.OldItems.OfType<Point>().First();

					Replace(pointToReplace, pointToAdd);
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Replace(Point pointToReplace, Point pointToAdd)
		{
			var viewModelToReplace = _figures.OfType<IPointViewModel>().First(p => p.Point.Equals(pointToReplace));
			var index = _figures.IndexOf(viewModelToReplace);
			if (index < -1)
				return;

			_figures.Remove(viewModelToReplace);
			_figures.Add(new PointViewModel(pointToAdd, MessengerInstance));
		}

		public Frame Frame
		{
			get { return _frame; }
			private set { Set(ref _frame, value); }
		}

		public EditMode Mode
		{
			get { return _mode; }
			set { Set(ref _mode, value); }
		}

		public ObservableCollection<IFigureViewModel> SelectedItems { get; private set; }

		public ObservableCollection<IFigureViewModel> Figures
		{
			get { return _figures; }
		}

		public ICommand RemoveSelected { get; private set; }
		public ICommand AddPoint { get; private set; }
		public ICommand AddEdge { get; private set; }

		public override void Cleanup()
		{
			base.Cleanup();
			_frame.Edges.CollectionChanged -= OnPointsOnCollectionChanged;
		}
	}
}