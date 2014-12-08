using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IPointViewModel : IFigureViewModel
	{
		Point Point { get; }

		System.Windows.Point CenterPoint { get; set; }
		//void SetCoordinates(double x, double y);
	}
}