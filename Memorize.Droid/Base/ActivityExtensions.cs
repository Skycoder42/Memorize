using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Memorize.Droid.Base
{
    static class ActivityExtensions
    {
        public static void ApplyStatusBarColor(this Activity activity, int colorId = Resource.Color.primary_dark)
        {
            var window = activity.Window;
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(new Color(activity.GetColor(colorId)));
        }
    }
}