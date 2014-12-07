using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class PointViewModel : ViewModelBase, IPointViewModel
	{
		private Model.Point _point;
		private double _x;
		private double _y;
		private Point _centerPoint;

		public PointViewModel(Model.Point point, IMessenger messenger)
			: base(messenger)
		{
			_point = point;
			SetCoordinates(_point.X, _point.Y);
		}

		public bool IsSelected { get; set; }
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