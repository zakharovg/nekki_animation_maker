using System.IO;

namespace AnimationMaker.Model
{
	public sealed class AnimationSerializer : IAnimationSerializer
	{
		public Animation Read(Stream stream)
		{
			return new Animation();
		}

		public void Write(Animation animation, Stream stream)
		{
			
		}
	}
}