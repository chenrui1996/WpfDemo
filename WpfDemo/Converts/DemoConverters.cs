using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfDemo.Converts
{
    /// <summary>
    /// 单值转换器（Bool => Visibility）
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                return flag ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        // 目标 -> 源
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is Visibility v)
            {
                return v == Visibility.Visible;
            }
            return false;
        }
    }

    /// <summary>
    /// 单值转换器参数（字符串截断最大长度）
    /// </summary>
    public class TruncateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value?.ToString() ?? string.Empty;
            if (int.TryParse(parameter?.ToString(), out int length))
            {
                return text.Length > length ? string.Concat(text.AsSpan(0, length), "...") : text;
            }
            return text;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        ) => throw new NotImplementedException();
    }

    /// <summary>
    /// 多值转换（价格折扣转换）
    /// </summary>
    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (values[0] is decimal originalPice && values[1] is decimal discount)
                return (originalPice * discount).ToString();

            return "0";
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        ) => throw new NotImplementedException();
    }

    /// <summary>
    /// 对象转换器（object => Json）
    /// </summary>
    public class ObjectToJsonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            return JsonSerializer.Serialize(
                value,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is string json && !string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    return JsonSerializer.Deserialize(json, targetType) ?? "";
                }
                catch
                {
                    return Binding.DoNothing;
                }
            }
            return Binding.DoNothing;
        }
    }
}
