using System;

namespace AnimationMaker.Model
{
	public struct Edge : IEquatable<Edge>
	{
		private readonly Point _start;

		private readonly Point _end;

		public Edge(Point start, Point end)
		{
			if (start.Equals(end))
				throw new ArgumentException("Start and end point should differ");

			_start = start;
			_end = end;
		}

		public Point Start
		{
			get { return _start; }
		}

		public Point End
		{
			get { return _end; }
		}

		public bool Equals(Edge other)
		{
			return _start.Equals(other._start) && _end.Equals(other._end);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Edge && Equals((Edge) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return _start.GetHashCode() ^ _end.GetHashCode();
			}
		}
	}
}