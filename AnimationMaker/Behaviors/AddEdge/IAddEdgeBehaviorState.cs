using System.Windows.Input;

namespace AnimationMaker.Behaviors.AddEdge
{
	public interface IAddEdgeBehaviorState
	{
		void HandleMouseUp(IAddEdgeBehavior context, MouseButtonEventArgs args);
		void HandleMouseDown(IAddEdgeBehavior context, MouseButtonEventArgs args);
	}
}