using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace WpfCustomControlLibrary.Converters
{
    public class IsFirstItemToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 &&
            values[0] is DependencyObject container &&
            values[1] is ItemsControl itemsControl)
            {
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(container);
                return index == 0
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
