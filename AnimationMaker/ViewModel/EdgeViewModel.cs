using System;
using System.Collections.Generic;
using AnimationMaker.Model;
using GalaSoft.MvvmLight.Messaging;

namespace AnimationMaker.ViewModel
{
	public sealed class EdgeViewModel : FigureViewModel, IEdgeViewModel
	{
		private readonly IPointViewModel _start;
		private readonly IPointViewModel _end;

		public EdgeViewModel(
			IPointViewModel start,
			IPointViewModel end,
			object token,
			IMessenger messenger)
			: base(messenger, token)
		{
			if (start == null) throw new ArgumentNullException("start");
			if (end == null) throw new ArgumentNullException("end");
			if (start.Point.Equals(end.Point))
				throw new ArgumentException("Start and end points should have different coordinates");

			_start = start;
			_end = end;
		}

		public IPointViewModel Start
		{
			get { return _start; }
		}

		public IPointViewModel End
		{
			get { return _end; }
		}

		public IEnumerable<IPointViewModel> Points
		{
			get { return new[] { _start, _end }; }
		}

		public Edge GetEdge()
		{
			return new Edge(Start.Point, End.Point);
		}
	}
}