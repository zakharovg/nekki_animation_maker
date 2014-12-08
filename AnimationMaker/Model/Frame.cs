using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimationMaker.Model
{
	public sealed class Frame
	{
		private readonly List<Point> _points;
		private readonly List<Edge> _edges;

		public Frame() : this(Enumerable.Empty<Point>(), Enumerable.Empty<Edge>())
		{ }

		public Frame(IEnumerable<Point> points, IEnumerable<Edge> edges)
		{
			if (points == null) throw new ArgumentNullException("points");
			if (edges == null) throw new ArgumentNullException("edges");

			_points = points.ToList();
			_edges = edges.ToList();
		}

		public Frame(Frame source)
			: this()
		{
			if (source == null) throw new ArgumentNullException("source");

			foreach (var point in source.Points)
				_points.Add(point);

			foreach (var edge in source.Edges)
				_edges.Add(edge);
		}

		public IEnumerable<Point> Points
		{
			get { return _points; }
		}

		public IEnumerable<Edge> Edges
		{
			get { return _edges; }
		}

		public IEnumerable<Point> GetPointsOfEdges()
		{
			return _edges.SelectMany(e => new List<Point> { e.Start, e.End });
		}

		public IEnumerable<Point> GetNotConnectedPoints()
		{
			return _points.Except(GetPointsOfEdges());
		}

		public void AddEdge(Edge edge)
		{
			if (_edges.Contains(edge))
				throw new ArgumentException("Edge with same points already exits");
			_edges.Add(edge);
		}

		public void AddPoint(Point point)
		{
			_points.Add(point);
		}

		public void RemoveEdge(Edge edge)
		{
			_edges.Remove(edge);
		}

		public void RemovePoint(Point point)
		{
			_points.Remove(point);

			var edgesToRemove = _edges.Where(e => e.End.Equals(point) || e.Start.Equals(point)).ToList();
			foreach (var edge in edgesToRemove)
				RemoveEdge(edge);
		}
	}
}