using System.Windows.Input;

namespace AnimationMaker.ViewModel
{
	public interface IFigureViewModel
	{
		bool IsSelected { get; }
		ICommand TriggerSelection { get; }
	}
}