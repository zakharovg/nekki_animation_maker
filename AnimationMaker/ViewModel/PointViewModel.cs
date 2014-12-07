using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class PointViewModel : ViewModelBase, IPointViewModel
	{
		private readonly Model.Point _point;

		public PointViewModel(Model.Point point, IMessenger messenger)
			: base(messenger)
		{
			_point = point;
		}

		public bool IsSelected { get; set; }
		public ICommand TriggerSelection { get; private set; }

		public Model.Point Point
		{
			get { return _point; }
		}

		public Point CenterPoint
		{
			get { return new Point(_point.X, _point.Y); }
		}

		public double X
		{
			get { return _point.X; }
		}

		public double Y
		{
			get { return _point.Y; }
		}
	}
}