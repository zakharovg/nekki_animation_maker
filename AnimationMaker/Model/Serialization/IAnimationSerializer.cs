using System.IO;

namespace AnimationMaker.Model.Serialization
{
	public interface IAnimationSerializer
	{
		Animation Read(Stream stream);
		void Write(Animation animation, Stream stream);
	}
}