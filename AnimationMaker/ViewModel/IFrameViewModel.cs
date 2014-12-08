using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IFrameViewModel
	{
		ObservableCollection<IFigureViewModel> Figures { get; }

		ICommand RemoveSelected { get; }
		ICommand AddPoint { get; }
		ICommand AddEdge { get; }

		Frame GetFrame();
	}
}