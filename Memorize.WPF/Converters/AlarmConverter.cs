using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Memorize.Core.Models;

namespace Memorize.WPF.Converters
{
    public class AlarmConverter : BaseConverter<IAlarm, string>, IValueConverter
    {
        public override string Convert(IAlarm value)
        {
            return value.ToString();
        }
    }
}
