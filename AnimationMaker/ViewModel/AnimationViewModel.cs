using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using AnimationMaker.Model;
using AnimationMaker.Model.Serialization;
using AnimationMaker.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class AnimationViewModel : ViewModelBase, IAnimationViewModel
	{
		private readonly IFrameViewModelFactory _frameFactory;
		private readonly IUserDialogService _userDialogService;
		private readonly IAnimationSerializer _animationSerializer;
		private Animation _animation;

		private EditMode _mode;

		private ICommand _nextFrame;
		private ICommand _previousFrame;
		private ICommand _save;
		private ICommand _load;
		private ICommand _clear;

		private IFrameViewModel _currentFrame;

		public AnimationViewModel(
			IMessenger messenger, 
			IFrameViewModelFactory frameFactory, 
			IUserDialogService userDialogService,
			IAnimationSerializer animationSerializer)
			: base(messenger)
		{
			if (frameFactory == null) throw new ArgumentNullException("frameFactory");
			if (userDialogService == null) throw new ArgumentNullException("userDialogService");
			if (animationSerializer == null) throw new ArgumentNullException("animationSerializer");

			_frameFactory = frameFactory;
			_userDialogService = userDialogService;
			_animationSerializer = animationSerializer;
			Reset();

			SetupCommands();

			var initialFrame = _animation[CurrentFrameIndex];
			_currentFrame = _frameFactory.Create(initialFrame);
		}

		private void Reset()
		{
			_animation = new Animation();
			GoToFirstFrame();
		}

		private void GoToFirstFrame()
		{
			CurrentFrameIndex = 0;
			CurrentFrame = _frameFactory.Create(_animation.Frames.First());
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
			}, () => CanNavigateLeft);
			_save = new RelayCommand(async () =>
			{
				SaveCurrentFrame();
				var result = _userDialogService.GetSavePath();
				if (!result.IsSuccessful)
					return;

				Exception ex = null;
				try
				{
					using (var file = File.OpenWrite(result.Filename))
						_animationSerializer.Write(_animation, file);
				}
				catch (Exception exception)
				{
					ex = exception;
				}

				if (ex != null)
					await _userDialogService.Alert(ex.Message);
			});
			_load = new RelayCommand(async () =>
			{
				var result = _userDialogService.GetLoadPath();
				if (!result.IsSuccessful)
					return;

				Exception ex = null;
				try
				{
					using (var file = File.OpenRead(result.Filename))
						_animation = _animationSerializer.Read(file);

					GoToFirstFrame();
				}
				catch (Exception exception)
				{
					ex = exception;
				}

				if (ex != null)
					await _userDialogService.Alert(ex.Message);
			});
			_clear = new RelayCommand(Reset);
		}

		private void SaveCurrentFrame()
		{
			var currentFrame = CurrentFrame.GetFrame();
			_animation[CurrentFrameIndex] = currentFrame;
		}

		public IFrameViewModel CurrentFrame
		{
			get { return _currentFrame; }
			private set
			{
				Set(ref _currentFrame, value);
				RaisePropertyChanged("StatusText");
				RaisePropertyChanged("CanNavigateLeft");
			}
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

		public ICommand Clear
		{
			get { return _clear; }
		}

		public string StatusText
		{
			get { return string.Format("Frame #{0}, Total frames:{1}", CurrentFrameIndex + 1, _animation.Frames.Count()); }
		}

		public bool CanNavigateLeft
		{
			get { return CurrentFrameIndex > 0; }
		}

		private int CurrentFrameIndex { get; set; }
	}
}