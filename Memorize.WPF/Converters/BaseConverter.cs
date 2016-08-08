using System;
using System.Globalization;
using System.Windows.Data;

namespace Memorize.WPF.Converters
{
    public abstract class BaseConverter<TSource, TTarget> : IValueConverter
    {
        public abstract TTarget Convert(TSource value);
        public virtual TSource ConvertBack(TTarget value)
        {
            return default(TSource);
        }

        public virtual TTarget Convert(TSource value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value);
        }
        public virtual TSource ConvertBack(TTarget value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (typeof(TTarget).IsAssignableFrom(targetType) &&
                value is TSource)
                return this.Convert((TSource) value, targetType, parameter, culture);
            else
                return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (typeof(TSource).IsAssignableFrom(targetType) &&
                value is TTarget)
                return this.Convert((TTarget)value, targetType, parameter, culture);
            else
                return null;
        }
    }
}