using System;
using System.Windows.Input;
using AnimationMaker.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public abstract class FigureViewModel : ViewModelBase, IFigureViewModel
	{
		private readonly object _token;
		private bool _isSelected;

		protected FigureViewModel(IMessenger messenger, object token) : base(messenger)
		{
			if (token == null) throw new ArgumentNullException("token");
			_token = token;

			TriggerSelection = new RelayCommand(ChangeIsSelected);
		}

		public bool IsSelected
		{
			get { return _isSelected; }
			set { Set(ref _isSelected, value); }
		}

		public ICommand TriggerSelection { get; private set; }

		protected object Token
		{
			get { return _token; }
		}

		protected void ChangeIsSelected()
		{
			IsSelected = !IsSelected;
			MessengerInstance.Send(new FigureSelectionMessage(this, IsSelected), _token);
		}
	}
}