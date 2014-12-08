using System;
using AnimationMaker.Model;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class FigureViewModelFactory : IFigureViewModelFactory
	{
		private readonly IMessenger _messenger;

		public FigureViewModelFactory(IMessenger messenger)
		{
			if (messenger == null) throw new ArgumentNullException("messenger");
			_messenger = messenger;
		}

		public IPointViewModel CreatePoint(Point point, object token)
		{
			if (token == null) throw new ArgumentNullException("token");
			return new PointViewModel(point, token, _messenger);
		}

		public IEdgeViewModel CreateEdge(IPointViewModel start, IPointViewModel end, object token)
		{
			if (token == null) throw new ArgumentNullException("token");

			return new EdgeViewModel(start, end, token, _messenger);
		}

		public IEdgeViewModel CreateEdge(Edge edge, object token)
		{
			var start = CreatePoint(edge.Start, token);
			var end = CreatePoint(edge.End, token);

			return CreateEdge(start, end, token);
		}
	}
}