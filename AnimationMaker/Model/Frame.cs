using System;
using System.Collections.ObjectModel;

namespace AnimationMaker.Model
{
	public sealed class Frame
	{
		private readonly ObservableCollection<Point> _points;
		private readonly ObservableCollection<Edge> _edges;

		public Frame()
		{
			_points = new ObservableCollection<Point>();
			_edges = new ObservableCollection<Edge>();
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

		public ObservableCollection<Point> Points
		{
			get { return _points; }
		}

		public ObservableCollection<Edge> Edges
		{
			get { return _edges; }
		}

		public void AddEdge(Point start, Point end)
		{
			var edge = new Edge(start, end);
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
		}
	}
}