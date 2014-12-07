using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Point = AnimationMaker.Model.Point;

namespace AnimationMaker.Resources.Converters
{
	public sealed class MouseLeftButtonUpToPointConverter : IEventArgsConverter
	{
		public object Convert(object value, object parameter)
		{
			var element = parameter as IInputElement;

			var args = value as MouseButtonEventArgs;

			if (args == null)
				throw new ArgumentException("Should be of type MouseButtonEventArgs", "value");

			var point = args.GetPosition(element);
			return new Point(point.X, point.Y);
		}
	}
}