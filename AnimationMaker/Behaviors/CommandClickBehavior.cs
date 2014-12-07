using System.Windows;
using System.Windows.Input;

namespace AnimationMaker.Behaviors
{
	public sealed class CommandClickBehavior : SwitchBehavior<UIElement>
	{
		private Point _mouseStartPosition;
		private bool _isMouseMoved;

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand),
			typeof(CommandClickBehavior),
			new FrameworkPropertyMetadata(null));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.MouseLeftButtonDown += Capture;
			AssociatedObject.MouseMove += Move;
			AssociatedObject.MouseLeftButtonUp += Release;
		}

		private void Capture(object sender, MouseButtonEventArgs args)
		{
			_mouseStartPosition = args.GetPosition(AssociatedObject);
		}

		private void Move(object sender, MouseEventArgs e)
		{
			if (Mouse.LeftButton != MouseButtonState.Pressed)
				return;
			_isMouseMoved = true;
		}

		private void Release(object sender, MouseButtonEventArgs args)
		{
			var point = GetMousePosition(args);
			if (_mouseStartPosition == point && !_isMouseMoved && IsEnabled)
				Command.Execute(new Model.Point(_mouseStartPosition.X, _mouseStartPosition.Y));

			_isMouseMoved = false;
		}

		private Point GetMousePosition(MouseButtonEventArgs args)
		{
			return args.GetPosition(AssociatedObject);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.MouseLeftButtonDown -= Capture;
			AssociatedObject.MouseLeftButtonUp -= Release;
		}
	}
}