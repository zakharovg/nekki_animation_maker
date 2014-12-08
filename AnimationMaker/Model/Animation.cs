using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimationMaker.Model
{
	public sealed class Animation
	{
		private readonly List<Frame> _frames;

		public Animation(IEnumerable<Frame> frames)
		{
			if (frames == null) throw new ArgumentNullException("frames");
			var framesList = frames.ToList();
			if (framesList.Contains(null)) throw new ArgumentException("Should not contain null", "frames");

			_frames = framesList;
		}

		public Animation()
		{
			var defaultFrame = new Frame();
			_frames = new List<Frame> { defaultFrame };
		}

		public IEnumerable<Frame> Frames
		{
			get { return _frames; }
		}

		public Frame AddNew()
		{
			var lastFrame = _frames.Last();
			var frameCopy = new Frame(lastFrame);
			_frames.Add(frameCopy);

			return frameCopy;
		}

		public void Remove(int index)
		{
			_frames.RemoveAt(index);
		}

		public Frame this[int index]
		{
			get { return _frames[index]; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				_frames[index] = value;
			}
		}
	}
}