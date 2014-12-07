using System;
using System.Collections.ObjectModel;
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

		public FrameViewModel(Frame frame, IMessenger messenger) : base(messenger)
		{
			if (frame == null) throw new ArgumentNullException("frame");
			Frame = frame;

			AddPoint = new RelayCommand<Point>(point => _frame.AddPoint(point), point => _mode == EditMode.AddPoint);
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
		public ObservableCollection<IFigureViewModel> Figures { get; private set; }
		public ICommand RemoveSelected { get; private set; }
		public ICommand AddPoint { get; private set; }
		public ICommand AddEdge { get; private set; }
	}
}