using System.ComponentModel;
using System.Windows.Input;

namespace AnimationMaker.ViewModel
{
	public interface IFigureViewModel : INotifyPropertyChanged
	{
		bool IsSelected { get; }
		ICommand TriggerSelection { get; }
	}
}