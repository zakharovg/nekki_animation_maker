using System;
using System.Collections.Generic;

namespace AnimationMaker.Model
{
	public sealed class Frame
	{
		private readonly HashSet<Point> _points;
		private readonly HashSet<Edge> _edges;

		public Frame()
		{
			_points = new HashSet<Point>();
			_edges = new HashSet<Edge>();
		}

		public Frame(Frame source) : this()
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

		public void AddEdge(Point start, Point end)
		{
			var edge = new Edge(start, end);
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
		}
	}
}