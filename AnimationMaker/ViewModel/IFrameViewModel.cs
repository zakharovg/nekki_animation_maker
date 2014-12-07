using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IFrameViewModel
	{
		Frame Frame { get; }

		ObservableCollection<IFigureViewModel> Figures { get; }

		EditMode Mode { get; set; }

		ICommand RemoveSelected { get; }
		ICommand AddPoint { get; }
		ICommand AddEdge { get; }
	}
}