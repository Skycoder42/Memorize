using Memorize.Core;
using System.Windows;
using Memorize.Core.Services;
using Memorize.WPF.Services;

namespace Memorize.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DoSetup();
            base.OnStartup(e);
        }

        public static void DoSetup()
        {
            CoreApp.Initialize(() => {
                CoreApp.RegisterService<ISettingsService, WpfSettingsService>();
            });
        }
    }
}
