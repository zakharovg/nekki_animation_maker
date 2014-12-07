using System;
using AnimationMaker.ViewModel;

namespace AnimationMaker.Messages
{
	public sealed class FigureSelectionMessage
	{
		private readonly IFigureViewModel _sender;
		private readonly bool _isSelected;

		public FigureSelectionMessage(IFigureViewModel sender, bool isSelected)
		{
			if (sender == null) throw new ArgumentNullException("sender");
			_sender = sender;
			_isSelected = isSelected;
		}

		public IFigureViewModel Sender
		{
			get { return _sender; }
		}

		public bool IsSelected
		{
			get { return _isSelected; }
		}
	}
}