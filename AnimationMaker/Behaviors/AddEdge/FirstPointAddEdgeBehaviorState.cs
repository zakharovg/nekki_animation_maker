using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AnimationMaker.ViewModel;
using Point = AnimationMaker.Model.Point;

namespace AnimationMaker.Behaviors.AddEdge
{
	public sealed class FirstPointAddEdgeBehaviorState : AddEdgeBehaviorState, IAddEdgeBehaviorState
	{
		public void HandleMouseDown(IAddEdgeBehavior context, MouseButtonEventArgs args)
		{
			if (context == null) throw new ArgumentNullException("context");
			if (args == null) throw new ArgumentNullException("args");

			var canvas = context.Canvas;

			var clickLocation = args.GetPosition(canvas);
			if (clickLocation.X <= 0 || clickLocation.Y <= 0)
				return;

			var pointUnderMouse = canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => HitTest(e, clickLocation));

			if (pointUnderMouse == null)
			{
				var point = new Point(clickLocation.X, clickLocation.Y);
				context.AddPointCommand.Execute(point);
				context.StartPoint = point;
			}
			else
			{
				var point = (IPointViewModel)pointUnderMouse.DataContext;
				context.StartPoint = point.Point;
			}
		}

		public void HandleMouseUp(IAddEdgeBehavior context, MouseButtonEventArgs args)
		{
			if (context == null) throw new ArgumentNullException("context");
			if (args == null) throw new ArgumentNullException("args");

			context.SetState(new SecondPointAddEdgeBehaviorState());
		}
	}
}