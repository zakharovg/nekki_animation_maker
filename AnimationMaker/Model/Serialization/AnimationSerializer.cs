using System;
using System.IO;
using System.Linq;
using AnimationMaker.Model.Serialization.Contract;
using Newtonsoft.Json;

namespace AnimationMaker.Model.Serialization
{
	public sealed class AnimationSerializer : IAnimationSerializer
	{
		private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

		public Animation Read(Stream stream)
		{
			if (stream == null) throw new ArgumentNullException("stream");

			var animationDto = Deserialize(stream);
			return FromExternal(animationDto);
		}

		private Animation FromExternal(AnimationDto animationDto)
		{
			return new Animation(animationDto.Frames.Select(FromExternal));
		}

		private Frame FromExternal(FrameDto frameDto)
		{
			var points = frameDto.Points.Select(FromExternal);
			var edges = frameDto.Edges.Select(e => new Edge(FromExternal(e.Start), FromExternal(e.End)));

			return new Frame(points, edges);
		}

		private static Point FromExternal(PointDto pointDto)
		{
			return new Point { X = pointDto.X, Y = pointDto.Y };
		}

		private AnimationDto Deserialize(Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonReader = new JsonTextReader(streamReader))
				return _jsonSerializer.Deserialize<AnimationDto>(jsonReader);
		}

		public void Write(Animation animation, Stream stream)
		{
			if (animation == null) throw new ArgumentNullException("animation");
			if (stream == null) throw new ArgumentNullException("stream");

			var animationDto = new AnimationDto
			{
				Frames = animation.Frames.Select(ToExternal)
			};

			using (var streamWriter = new StreamWriter(stream))
				_jsonSerializer.Serialize(streamWriter, animationDto);
		}

		private static FrameDto ToExternal(Frame frame)
		{
			return new FrameDto
			{
				Points = frame.Points.Select(ToExternal),
				Edges = frame.Edges.Select(ToExternal)
			};
		}

		private static PointDto ToExternal(Point point)
		{
			return new PointDto { X = point.X, Y = point.Y };
		}

		private static EdgeDto ToExternal(Edge edge)
		{
			return new EdgeDto
			{
				Start = ToExternal(edge.Start),
				End = ToExternal(edge.End)
			};
		}
	}
}