using System.Windows;
using System.Windows.Media;

namespace AnimationMaker.Extensions
{
	public static class UIElementExtensions
	{
		public static T FindParent<T>(DependencyObject child) where T : DependencyObject
		{
			//get parent item
			var parentObject = VisualTreeHelper.GetParent(child);

			//we've reached the end of the tree
			if (parentObject == null) return null;

			//check if the parent matches the type we're looking for
			var parent = parentObject as T;
			return parent ?? FindParent<T>(parentObject);
		}
	}
}