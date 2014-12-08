using System;
using System.Linq;
using System.Windows.Input;
using AnimationMaker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class AnimationViewModel : ViewModelBase, IAnimationViewModel
	{
		private readonly IFrameViewModelFactory _frameFactory;
		private readonly Animation _animation;

		private ICommand _nextFrame;
		private ICommand _previousFrame;
		private ICommand _save;
		private ICommand _load;

		private int _currentFrameIndex;
		private IFrameViewModel _currentFrame;

		public AnimationViewModel(IMessenger messenger, IFrameViewModelFactory frameFactory)
			: base(messenger)
		{
			if (frameFactory == null) throw new ArgumentNullException("frameFactory");

			_frameFactory = frameFactory;
			_animation = new Animation();
			CurrentFrameIndex = 0;

			SetupCommands();

			var initialFrame = _animation[CurrentFrameIndex];
			_currentFrame = _frameFactory.Create(initialFrame);
		}

		private void SetupCommands()
		{
			_nextFrame = new RelayCommand(() =>
			{
				SaveCurrentFrame();
				++CurrentFrameIndex;

				var nextFrame = _animation.Frames.Count() <= CurrentFrameIndex
					? _animation.AddNew()
					: _animation[CurrentFrameIndex];

				CurrentFrame = _frameFactory.Create(nextFrame);
			});
			_previousFrame = new RelayCommand(() =>
			{
				if (CurrentFrameIndex == 0)
					throw new InvalidOperationException("Current frame is first frame in animation");

				SaveCurrentFrame();

				var previousFrame = _animation[--CurrentFrameIndex];
				CurrentFrame = _frameFactory.Create(previousFrame);
			});
			_save = new RelayCommand(() => { });
			_load = new RelayCommand(() => { });
		}

		private void SaveCurrentFrame()
		{
			var currentFrame = CurrentFrame.GetFrame();
			_animation[CurrentFrameIndex] = currentFrame;
		}

		public IFrameViewModel CurrentFrame
		{
			get { return _currentFrame; }
			private set { Set(ref _currentFrame, value); }
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

		public string CurrentFrameText
		{
			get { return string.Format("Frame #{0}", CurrentFrameIndex); }
		}

		public bool CanNavigateLeft
		{
			get { return CurrentFrameIndex > 0; }
		}

		private int CurrentFrameIndex
		{
			get { return _currentFrameIndex; }
			set
			{
				_currentFrameIndex = value;
				RaisePropertyChanged("CurrentFrameText");
				RaisePropertyChanged("CanNavigateLeft");
			}
		}
	}
}