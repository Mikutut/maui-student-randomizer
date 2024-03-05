using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.ValueConverters
{
	public class SchoolClassStudentsCountConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return ((SchoolClass?)value)?.Students.Count ?? 0;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}
	}
}
