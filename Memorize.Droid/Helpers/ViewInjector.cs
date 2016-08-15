using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.Views;

namespace Memorize.Droid.Helpers
{
    public class InjectViewAttribute : Attribute
    {
        public readonly int ResourceId;

        public InjectViewAttribute(int resourceId)
        {
            ResourceId = resourceId;
        }
    }

    public static class ViewInjector
    {
        public static void AutoInjectViews(this Activity activity)
        {
            var privateFields = activity.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(InjectViewAttribute)));
            foreach (var field in privateFields)
            {
                var injectionAttribute = field.GetCustomAttribute<InjectViewAttribute>(false);
                var injectedType = activity.FindViewById(injectionAttribute.ResourceId);
                field.SetValue(activity, injectedType);
            }
        }

        public static void AutoInjectViews(this Android.Support.V4.App.Fragment fragment, View view)
        {
            var privateFields = fragment.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(InjectViewAttribute)));
            foreach (var field in privateFields)
            {
                var injectionAttribute = field.GetCustomAttribute<InjectViewAttribute>(false);
                var injectedType = (view ?? fragment.View).FindViewById(injectionAttribute.ResourceId);
                field.SetValue(fragment, injectedType);
            }
        }

        public static void AutoInjectViews(this Dialog dialog)
        {
            var privateFields = dialog.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(InjectViewAttribute)));
            foreach (var field in privateFields)
            {
                var injectionAttribute = field.GetCustomAttribute<InjectViewAttribute>(false);
                var injectedType = dialog.FindViewById(injectionAttribute.ResourceId);
                field.SetValue(dialog, injectedType);
            }
        }
    }
}