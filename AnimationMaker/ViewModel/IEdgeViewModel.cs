namespace AnimationMaker.ViewModel
{
	public interface IEdgeViewModel : IFigureViewModel
	{
		IPointViewModel Start { get; }
		IPointViewModel End { get; }
	}
}