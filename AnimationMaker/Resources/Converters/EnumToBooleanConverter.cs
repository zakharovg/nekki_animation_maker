using System;
using System.Globalization;
using System.Windows.Data;

namespace AnimationMaker.Resources.Converters
{
	public sealed class EnumToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter.Equals(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter;
		}
	}
}