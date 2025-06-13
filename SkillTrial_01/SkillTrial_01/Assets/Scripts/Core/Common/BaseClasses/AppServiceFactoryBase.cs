using Elder.Core.Common.Interfaces;
using Elder.Core.GameFlow.Application;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.Common.BaseClasses
{
    public class AppServiceFactoryBase : DisposableBase, IAppServiceFactory
    {
        private ILogger _logger = LogAppService.In.CreateLogger<AppServiceFactoryBase>();

        public bool TryBuildAppServices(IMainFrameworkServiceProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IAppService>> appServices)
        {
            appServices = BuildAppServiceDictionary(serviceProvider);
            return appServices != null && appServices.Any(); // 논리 수정: null이 아니고, 비어있지 않음
        }

        private Dictionary<Type, IAppService> BuildAppServiceDictionary(IMainFrameworkServiceProvider serviceProvider)
        {
            var appServices = new Dictionary<Type, IAppService>();
            RegisterCoreAppServices(appServices, serviceProvider);
            RegisterAdditionalAppServices(appServices, serviceProvider);
            return appServices;
        }
        private void RegisterCoreAppServices(Dictionary<Type, IAppService> appServices, IMainFrameworkServiceProvider serviceProvider)
        {
            RegisterAppServices<GameFlowAppService>(appServices, serviceProvider);
        }
        protected void RegisterAppServices<T>(Dictionary<Type, IAppService> appServices, IMainFrameworkServiceProvider serviceProvider) where T : AppServiceBase, new()
        {
            var appService = new T();
            appService.SetServiceProvider(serviceProvider);
            appServices[typeof(T)] = appService;
        }
        protected virtual void RegisterAdditionalAppServices(Dictionary<Type, IAppService> appServices, IMainFrameworkServiceProvider serviceProvider)
        {

        }

        protected override void DisposeManagedResources()
        {
            DispsoeLogger();
        }
        private void DispsoeLogger()
        {
            _logger = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
