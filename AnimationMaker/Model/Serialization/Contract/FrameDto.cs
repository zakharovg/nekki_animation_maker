using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnimationMaker.Model.Serialization.Contract
{
	public struct FrameDto
	{
		[JsonProperty]
		public IEnumerable<PointDto> Points { get; set; }

		[JsonProperty]
		public IEnumerable<EdgeDto> Edges { get; set; }
	}
}