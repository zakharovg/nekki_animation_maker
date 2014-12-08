using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnimationMaker.Model.Serialization.Contract
{
	public struct AnimationDto
	{
		[JsonProperty]
		public IEnumerable<FrameDto> Frames { get; set; }
	}
}