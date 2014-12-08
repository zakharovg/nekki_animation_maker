using Newtonsoft.Json;

namespace AnimationMaker.Model.Serialization.Contract
{
	public struct EdgeDto
	{
		[JsonProperty]
		public PointDto Start { get; set; }

		[JsonProperty]
		public PointDto End { get; set; }
	}
}