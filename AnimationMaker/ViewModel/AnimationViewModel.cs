using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace AnimationMaker.ViewModel
{
	public sealed class AnimationViewModel : ViewModelBase, IAnimationViewModel
	{
		private IFrameViewModel _currentFrame;
		private EditMode _mode;
		private ICommand _nextFrame;
		private ICommand _previousFrame;
		private ICommand _save;
		private ICommand _load;

		public IFrameViewModel CurrentFrame
		{
			get { return _currentFrame; }
		}

		public EditMode Mode
		{
			get { return _mode; }
			set { Set(ref _mode, value); }
		}

		public ICommand NextFrame
		{
			get { return _nextFrame; }
		}

		public ICommand PreviousFrame
		{
			get { return _previousFrame; }
		}

		public ICommand Save
		{
			get { return _save; }
		}

		public ICommand Load
		{
			get { return _load; }
		}
	}
}