using System;
using System.Collections.Generic;
using Memorize.Core.Services;

namespace Memorize.Core
{
    public static class CoreApp
    {
        private static object _initialized = false;
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

        public static void RegisterService<TServiceInterface>(Func<TServiceInterface> serviceConstruct)
        {
            ServiceRegistry[typeof(TServiceInterface)] = serviceConstruct();
        }

        public static void RegisterServiceClass<TService>()
            where TService : class, new()
            => RegisterService<TService, TService>();

        public static void Initialize(Action nativeSetupAction)
        {
            lock (_initialized) {
                var bInit = (bool) _initialized;
                if (!bInit) {
                    RegisterServiceClass<ReminderManagerService>();

                    nativeSetupAction();

                    var remService = Service<ReminderManagerService>();
                    remService.AddExampleReminder();
                    remService.AddExampleReminder();
                    remService.AddExampleReminder();
                    remService.AddExampleReminder();

                    _initialized = true;
                }
            }
        }
    }
}
