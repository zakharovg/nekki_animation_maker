using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using AnimationMaker.ViewModel;
using MahApps.Metro.Controls;

namespace AnimationMaker.Behaviors
{
	public sealed class PointMoveBehavior : Behavior<FrameworkElement>
	{
		private Point _currentPosition = new Point();
		private Point _mouseStartPosition;
		private readonly TranslateTransform _transform = new TranslateTransform();
		private Canvas _canvas;

		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.RenderTransform = _transform;

			_canvas = AssociatedObject.TryFindParent<Canvas>();
			
			AssociatedObject.MouseLeftButtonDown += Capture;
			AssociatedObject.MouseMove += Move;
			AssociatedObject.MouseLeftButtonUp += Release;
		}

		private void Capture(object sender, MouseButtonEventArgs args)
		{
			_mouseStartPosition = args.GetPosition(_canvas);
			AssociatedObject.CaptureMouse();
		}

		private void Move(object sender, MouseEventArgs args)
		{
			var diff = args.GetPosition(_canvas) - _mouseStartPosition;
			if (!AssociatedObject.IsMouseCaptured)
				return;

			_transform.X = _currentPosition.X + diff.X;
			_transform.Y = _currentPosition.Y + diff.Y;

			var point = (IPointViewModel)AssociatedObject.DataContext;
			point.SetCoordinates(_transform.X, _transform.Y);
		}

		private void Release(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			AssociatedObject.ReleaseMouseCapture();
			_currentPosition.X = _transform.X;
			_currentPosition.Y = _transform.Y; 
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