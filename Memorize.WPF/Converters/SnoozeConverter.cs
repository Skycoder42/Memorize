using System;
using System.Windows.Data;
using Memorize.Core.Helpers;

namespace Memorize.WPF.Converters
{
    public class SnoozeConverter : BaseConverter<TimeSpan, string>, IValueConverter
    {
        public override string Convert(TimeSpan value)
        {
            return value.ToShortString();
        }
    }
}
