using System.Windows.Input;
using AnimationMaker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class AnimationViewModel : ViewModelBase, IAnimationViewModel
	{
		private readonly Animation _animation;
		private int _currentFrameIndex;
		private IFrameViewModel _currentFrame;
		private EditMode _mode;
		private ICommand _nextFrame;
		private ICommand _previousFrame;
		private ICommand _save;
		private ICommand _load;

		public AnimationViewModel(IMessenger messenger)
			: base(messenger)
		{
			_animation = new Animation();
			_currentFrameIndex = 0;

			var initialFrame = _animation[_currentFrameIndex];
			_currentFrame = new FrameViewModel(initialFrame, MessengerInstance);
		}

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

		public string CurrentFrameIndex
		{
			get { return string.Format("Frame #{0}", _currentFrameIndex); }
		}
	}
}