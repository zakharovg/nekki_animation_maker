using System.Windows;
using System.Windows.Interactivity;

namespace AnimationMaker.Behaviors
{
	public class SwitchBehavior<T> : Behavior<T> where T : DependencyObject
	{
		public static readonly DependencyProperty IsEnabledProperty =
		   DependencyProperty.Register("IsEnabled", typeof(bool),
		   typeof(SwitchBehavior<T>),
		   new FrameworkPropertyMetadata(null));

		public bool IsEnabled
		{
			get { return (bool)GetValue(IsEnabledProperty); }
			set { SetValue(IsEnabledProperty, value); }
		}
	}
}