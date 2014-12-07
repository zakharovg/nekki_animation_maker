using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IEdgeViewModel : IFigureViewModel
	{
		IPointViewModel Start { get; }
		IPointViewModel End { get; }
		Edge Edge { get; set; }
	}
}