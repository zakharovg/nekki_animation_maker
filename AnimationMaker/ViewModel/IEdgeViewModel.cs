using System.Collections.Generic;
using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IEdgeViewModel : IFigureViewModel
	{
		IPointViewModel Start { get; }
		IPointViewModel End { get; }
		IEnumerable<IPointViewModel> Points { get; }

		Edge GetEdge();
	}
}