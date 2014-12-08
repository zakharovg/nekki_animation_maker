using System;
using System.Windows;
using System.Windows.Media;
using AnimationMaker.ViewModel;

namespace AnimationMaker.Behaviors.AddEdge
{
	public abstract class AddEdgeBehaviorState
	{
		protected bool HitTest(FrameworkElement e, Point clickLocation)
		{
			var hitTest = VisualTreeHelper.HitTest(e, clickLocation);
			return hitTest != null && (hitTest.VisualHit != null && e.DataContext is IPointViewModel);
		}

		protected void AddEdge(IAddEdgeBehavior context)
		{
			if (context.StartPoint.Equals(context.EndPoint)) 
				return;

			context.AddEdgeCommand.Execute(new Tuple<Model.Point, Model.Point>(context.StartPoint, context.EndPoint));
		}
	}
}