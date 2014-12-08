using System;
using System.Collections.Generic;
using System.Linq;
using AnimationMaker.Model;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class FrameViewModelFactory : IFrameViewModelFactory
	{
		private readonly IMessenger _messenger;
		private readonly IFigureViewModelFactory _figureFactory;

		public FrameViewModelFactory(IMessenger messenger, IFigureViewModelFactory figureFactory)
		{
			if (messenger == null) throw new ArgumentNullException("messenger");
			if (figureFactory == null) throw new ArgumentNullException("figureFactory");
			_messenger = messenger;
			_figureFactory = figureFactory;
		}

		public IFrameViewModel Create(Frame frame)
		{
			if (frame == null) throw new ArgumentNullException("frame");
			var token = new object();

			var points = frame.Points.Select(p => _figureFactory.CreatePoint(p, token)).ToList();
			var edges = CreateEdges(frame.Edges, points, token);
			var figures = edges.Concat<IFigureViewModel>(points);

			return new FrameViewModel(_figureFactory, _messenger, figures, token);
		}

		private IEnumerable<IEdgeViewModel> CreateEdges(IEnumerable<Edge> edges, List<IPointViewModel> points, object token)
		{
			foreach (var edge in edges)
			{
				var startPoint = points.First(p => p.Point.Equals(edge.Start));
				var endPoint = points.First(p => p.Point.Equals(edge.End));
				yield return _figureFactory.CreateEdge(startPoint, endPoint, token);
			}
		}
	}
}