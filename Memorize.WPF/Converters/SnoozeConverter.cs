using System;
using System.Windows.Data;
using Memorize.Core.Helpers;

namespace Memorize.WPF.Converters
{
    internal class SnoozeConverter : BaseConverter<TimeSpan, string>, IValueConverter
    {
        public override string Convert(TimeSpan value)
        {
            return value == TimeSpan.MaxValue ? "<Custom>" : value.ToShortString();
        }
    }
}
