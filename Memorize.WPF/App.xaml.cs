using Memorize.Core;
using System.Windows;

namespace Memorize.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Setup.Initialize();
            base.OnStartup(e);
        }
    }
}
