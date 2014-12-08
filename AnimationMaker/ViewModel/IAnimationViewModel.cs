using System.Windows.Input;

namespace AnimationMaker.ViewModel
{
	public interface IAnimationViewModel
	{
		EditMode Mode { get; set; }

		IFrameViewModel CurrentFrame { get; }

		ICommand NextFrame { get; }
		ICommand PreviousFrame { get; }

		ICommand Save { get; }
		ICommand Load { get; }

		ICommand ClearAnimation { get; }
		ICommand ClearFrame { get; }
		ICommand Delete { get; }

		string StatusText { get; }
		bool CanNavigateLeft { get; }
		bool CanDeleteFrame { get; }
	}
}