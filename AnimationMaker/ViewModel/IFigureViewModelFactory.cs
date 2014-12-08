using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IFigureViewModelFactory
	{
		IPointViewModel CreatePoint(Point point, object token);

		IEdgeViewModel CreateEdge(IPointViewModel start, IPointViewModel end, object token);
		IEdgeViewModel CreateEdge(Edge edge, object token);
	}
}