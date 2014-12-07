using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IPointViewModel : IFigureViewModel
	{
		Point Point { get; }

		System.Windows.Point CenterPoint { get; }

		double X { get; }
		double Y { get; }
	}
}