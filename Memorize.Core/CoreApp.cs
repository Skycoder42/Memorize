using System;
using System.Collections.Generic;
using Memorize.Core.Services;

namespace Memorize.Core
{
    public static class CoreApp
    {
        private static readonly IDictionary<Type, object> ServiceRegistry = new Dictionary<Type, object>();

        public static TService Service<TService>()
            where TService : class
        {
            return ServiceRegistry[typeof(TService)] as TService;
        }

        public static void RegisterService<TServiceInterface, TService>() 
            where TService : class, TServiceInterface, new()
        {
            ServiceRegistry[typeof(TServiceInterface)] = new TService();
        }

        public static void RegisterServiceClass<TService>()
            where TService : class, new()
            => RegisterService<TService, TService>();

        public static void Initialize(Action nativeSetupAction)
        {
            RegisterServiceClass<ReminderManagerService>();

            nativeSetupAction();

            var remService = Service<ReminderManagerService>();
            remService.AddExampleReminder();
            remService.AddExampleReminder();
            remService.AddExampleReminder();
            remService.AddExampleReminder();
        }
    }
}
