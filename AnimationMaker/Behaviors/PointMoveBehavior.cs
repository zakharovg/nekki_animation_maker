using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AnimationMaker.ViewModel;
using MahApps.Metro.Controls;

namespace AnimationMaker.Behaviors
{
	public sealed class PointMoveBehavior : SwitchBehavior<System.Windows.Shapes.Path>
	{
		private Point _currentPosition;
		private Point _mouseStartPosition;
		private Canvas _canvas;
		private EllipseGeometry _ellipseGeometry;

		protected override void OnAttached()
		{
			base.OnAttached();

			_canvas = AssociatedObject.TryFindParent<Canvas>();
			_ellipseGeometry = AssociatedObject.Data as EllipseGeometry;

			AssociatedObject.MouseLeftButtonDown += Capture;
			AssociatedObject.MouseMove += Move;
			AssociatedObject.MouseLeftButtonUp += Release;
		}

		private void Capture(object sender, MouseButtonEventArgs args)
		{
			if (!IsEnabled)
				return;

			_mouseStartPosition = args.GetPosition(_canvas);
			AssociatedObject.CaptureMouse();
		}

		private void Move(object sender, MouseEventArgs args)
		{
			var diff = args.GetPosition(_canvas) - _mouseStartPosition;
			if (!AssociatedObject.IsMouseCaptured || Mouse.LeftButton != MouseButtonState.Pressed)
				return;

			var point = (IPointViewModel)AssociatedObject.DataContext;

			_currentPosition.X = CalculateX(diff);
			_currentPosition.Y = CalculateY(diff);

			point.CenterPoint = new Point(_currentPosition.X, _currentPosition.Y);
		}

		private double CalculateX(Vector diff)
		{
			var x = _mouseStartPosition.X + diff.X;

			if (x <= _ellipseGeometry.RadiusX)
				return _ellipseGeometry.RadiusX;

			var maxX = _canvas.ActualWidth - _ellipseGeometry.RadiusX;
			if (x > maxX)
				return maxX;

			return x;
		}

		private double CalculateY(Vector diff)
		{
			var y = _mouseStartPosition.Y + diff.Y;

			if (y <= _ellipseGeometry.RadiusY)
				return _ellipseGeometry.RadiusY;

			var maxY = _canvas.ActualHeight - _ellipseGeometry.RadiusY;
			if (y > maxY)
				return maxY;

			return y;
		}

		private void Release(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			AssociatedObject.ReleaseMouseCapture();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.MouseLeftButtonDown -= Capture;
			AssociatedObject.MouseMove -= Move;
			AssociatedObject.MouseLeftButtonUp -= Release;
		}
	}
}