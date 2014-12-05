using System;

namespace AnimationMaker.Model
{
	public struct Point : IEquatable<Point>
	{
		private readonly float _x;

		private readonly float _y;

		public Point(float x, float y)
		{
			if (x < 0) throw new ArgumentException("Should not be less than zero", "x");
			if (y < 0) throw new ArgumentException("Should not be less than zero", "y");

			_x = x;
			_y = y;
		}

		public float X
		{
			get { return _x; }
		}

		public float Y
		{
			get { return _y; }
		}

		public bool Equals(Point other)
		{
			return _x.Equals(other._x) && _y.Equals(other._y);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Point && Equals((Point) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_x.GetHashCode()*397) ^ _y.GetHashCode();
			}
		}
	}
}