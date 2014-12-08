using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class PointViewModel : FigureViewModel, IPointViewModel
	{
		private Model.Point _point;
		private Point _centerPoint;

		public PointViewModel(Model.Point point, object token, IMessenger messenger)
			: base(messenger, token)
		{
			if (token == null) throw new ArgumentNullException("token");
			_point = point;
			_centerPoint = new Point(_point.X, _point.Y);
		}

		public Model.Point Point
		{
			get { return _point; }
		}

		public Point CenterPoint
		{
			get { return _centerPoint; }
			set
			{
				if (!Set(ref _centerPoint, value))
					return;

				_point.X = _centerPoint.X;
				_point.Y = _centerPoint.Y;
			}
		}
	}
}