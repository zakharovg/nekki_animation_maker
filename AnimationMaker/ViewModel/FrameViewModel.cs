using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimationMaker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class FrameViewModel : ViewModelBase, IFrameViewModel
	{
		public FrameViewModel(Frame frame, IMessenger messenger) : base(messenger)
		{
			if (frame == null) throw new ArgumentNullException("frame");
			Frame = frame;
		}

		public Frame Frame { get; private set; }
		public ObservableCollection<IFigureViewModel> SelectedItems { get; private set; }
		public ObservableCollection<IFigureViewModel> Figures { get; private set; }
		public ICommand RemoveSelected { get; private set; }
		public ICommand AddPoint { get; private set; }
		public ICommand AddEdge { get; private set; }
	}
}