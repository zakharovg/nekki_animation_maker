using System;
using System.Windows;
using System.Windows.Input;
using AnimationMaker.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class PointViewModel : ViewModelBase, IPointViewModel
	{
		private Model.Point _point;
		private readonly object _token;
		private Point _centerPoint;
		private bool _isSelected;

		public PointViewModel(Model.Point point, object token, IMessenger messenger)
			: base(messenger)
		{
			if (token == null) throw new ArgumentNullException("token");
			_point = point;
			_token = token;
			SetCoordinates(_point.X, _point.Y);
			TriggerSelection = new RelayCommand(ChangeIsSelected);
		}

		private void ChangeIsSelected()
		{
			IsSelected = !IsSelected;
			MessengerInstance.Send(new FigureSelectionMessage(this, IsSelected), _token);
		}

		public bool IsSelected
		{
			get { return _isSelected; }
			set { Set(ref _isSelected, value); }
		}

		public ICommand TriggerSelection { get; private set; }

		public Model.Point Point
		{
			get { return _point; }
		}

		public Point CenterPoint
		{
			get { return _centerPoint; }
		}

		public void SetCoordinates(double x, double y)
		{
			_centerPoint.X = _point.X;
			_centerPoint.Y = _point.Y;
			RaisePropertyChanged("CenterPoint");
		}
	}
}