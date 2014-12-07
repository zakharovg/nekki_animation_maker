using System.Windows.Input;

namespace AnimationMaker.ViewModel
{
	public interface IAnimationViewModel
	{
		IFrameViewModel CurrentFrame { get; }

		ICommand NextFrame { get; }
		ICommand PreviousFrame { get; }

		ICommand Save { get; }
		ICommand Load { get; }
		string CurrentFrameIndex { get; }
	}
}