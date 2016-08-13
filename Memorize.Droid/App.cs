using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Memorize.Core;
using Memorize.Core.Services;
using Memorize.Droid.Services;

namespace Memorize.Droid
{
    [Application]
    public class App : Application
    {
        public App() { }

        public App(IntPtr javaReference, JniHandleOwnership transfer) : 
            base(javaReference, transfer)
        { }

        public override void OnCreate()
        {
            base.OnCreate();
            DoSetup(this.ApplicationContext);
        }

        public static void DoSetup(Context appContext)
        {
            CoreApp.Initialize(() => {
                CoreApp.RegisterService<ISettingsService>(() => new DroidSettingsService(appContext));
            });
        }
    }
}