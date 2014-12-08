using System.Windows.Controls;
using System.Windows.Input;
using AnimationMaker.Model;

namespace AnimationMaker.Behaviors.AddEdge
{
	public interface IAddEdgeBehavior
	{
		Canvas Canvas { get; }
		Point StartPoint { set; get; }
		Point EndPoint { set; get; }

		ICommand AddEdgeCommand { get; set; }
		ICommand AddPointCommand { get; set; }

		void SetState(IAddEdgeBehaviorState state);
	}
}