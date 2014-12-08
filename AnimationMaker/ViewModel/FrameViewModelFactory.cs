using System;
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

			var edges = frame.Edges.Select(e => _figureFactory.CreateEdge(e, token)).ToList();
			var notConnectedPoints = frame.GetNotConnectedPoints().Select(p => _figureFactory.CreatePoint(p, token));
			var points = edges
				.SelectMany(e => e.Points)
				.Concat(notConnectedPoints);
			var figures = edges.Concat<IFigureViewModel>(points);

			return new FrameViewModel(_figureFactory, _messenger, figures, token);
		}
	}
}