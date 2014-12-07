using System;
using System.Windows;
using System.Windows.Controls;
using AnimationMaker.ViewModel;

namespace AnimationMaker.Resources
{
	public sealed class FiguresTemplateSelector : DataTemplateSelector
	{
		public DataTemplate PointTemplate { get; set; }
		public DataTemplate EdgeTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var point = item as IPointViewModel;
			if (point != null)
				return PointTemplate;

			var edge = item as IEdgeViewModel;
			if (edge != null)
				return EdgeTemplate;

			throw new InvalidOperationException("Template for item is not defined");
		}
	}
}