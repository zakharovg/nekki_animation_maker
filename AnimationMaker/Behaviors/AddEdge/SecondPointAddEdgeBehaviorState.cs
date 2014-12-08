using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AnimationMaker.ViewModel;
using Point = AnimationMaker.Model.Point;

namespace AnimationMaker.Behaviors.AddEdge
{
	public sealed class SecondPointAddEdgeBehaviorState : AddEdgeBehaviorState, IAddEdgeBehaviorState
	{
		public void HandleMouseDown(IAddEdgeBehavior context, MouseButtonEventArgs args)
		{
			if (context == null) throw new ArgumentNullException("context");
			if (args == null) throw new ArgumentNullException("args");

			var canvas = context.Canvas;

			var clickLocation = args.GetPosition(canvas);
			var pointUnderMouse = canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.DataContext is IPointViewModel && HitTest(e, clickLocation));

			if (pointUnderMouse == null)
			{
				context.EndPoint = new Point(clickLocation.X, clickLocation.Y);
				context.AddPointCommand.Execute(context.EndPoint);
			}
			else
			{
				var point = (IPointViewModel)pointUnderMouse.DataContext;
				context.EndPoint = point.Point;
			}
		}

		public void HandleMouseUp(IAddEdgeBehavior context, MouseButtonEventArgs args)
		{
			if (context == null) throw new ArgumentNullException("context");
			if (args == null) throw new ArgumentNullException("args");

			AddEdge(context);

			context.SetState(new FirstPointAddEdgeBehaviorState());
		}
	}
}