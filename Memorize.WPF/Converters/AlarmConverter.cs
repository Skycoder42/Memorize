using System.Windows.Data;
using Memorize.Core.Models;

namespace Memorize.WPF.Converters
{
    internal class AlarmConverter : BaseConverter<IAlarm, string>, IValueConverter
    {
        public override string Convert(IAlarm value)
        {
            return value.ToString();
        }
    }
}
