using AnimationMaker.Model;

namespace AnimationMaker.ViewModel
{
	public interface IFrameViewModelFactory
	{
		IFrameViewModel Create(Frame frame);
	}
}