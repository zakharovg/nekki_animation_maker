using Newtonsoft.Json;

namespace AnimationMaker.Model.Serialization.Contract
{
	public struct PointDto
	{
		[JsonProperty]
		public double X { get; set; }
		[JsonProperty]
		public double Y { get; set; }
	}
}