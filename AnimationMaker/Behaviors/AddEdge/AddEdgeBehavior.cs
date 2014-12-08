using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Point = AnimationMaker.Model.Point;

namespace AnimationMaker.Behaviors.AddEdge
{
	public sealed class AddEdgeBehavior : SwitchBehavior<Canvas>, IAddEdgeBehavior
	{
		private IAddEdgeBehaviorState _state;

		public static readonly DependencyProperty AddEdgeCommandProperty =
			DependencyProperty.Register("AddEdgeCommand", typeof(ICommand),
			typeof(AddEdgeBehavior),
			new FrameworkPropertyMetadata(null));

		public ICommand AddEdgeCommand
		{
			get { return (ICommand)GetValue(AddEdgeCommandProperty); }
			set { SetValue(AddEdgeCommandProperty, value); }
		}

		public static readonly DependencyProperty AddPointCommandProperty =
			DependencyProperty.Register("AddPointCommand", typeof(ICommand),
			typeof(AddEdgeBehavior),
			new FrameworkPropertyMetadata(null));

		public ICommand AddPointCommand
		{
			get { return (ICommand)GetValue(AddPointCommandProperty); }
			set { SetValue(AddPointCommandProperty, value); }
		}

		public AddEdgeBehavior()
		{
			_state = new FirstPointAddEdgeBehaviorState();
		}

		public Canvas Canvas
		{
			get { return AssociatedObject; }
		}

		public Point StartPoint { get; set; }

		public Point EndPoint { get; set; }

		public void SetState(IAddEdgeBehaviorState state)
		{
			if (state == null) throw new ArgumentNullException("state");
			_state = state;
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.MouseLeftButtonDown += GetOrCreateStartPoint;
			AssociatedObject.MouseLeftButtonDown += AddEdge;
		}

		private void GetOrCreateStartPoint(object sender, MouseButtonEventArgs args)
		{
			if (!IsEnabled)
				return;

			_state.HandleMouseDown(this, args);
		}

		private void AddEdge(object sender, MouseButtonEventArgs args)
		{
			if (!IsEnabled)
				return;

			_state.HandleMouseUp(this, args);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();

			AssociatedObject.MouseLeftButtonDown -= GetOrCreateStartPoint;
			AssociatedObject.MouseLeftButtonDown -= AddEdge;
		}
	}
}