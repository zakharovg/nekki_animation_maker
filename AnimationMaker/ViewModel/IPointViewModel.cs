using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IPointViewModel : IFigureViewModel
	{
		Point Point { get; }

		System.Windows.Point CenterPoint { get; }
		void SetCoordinates(double x, double y);
	}
}