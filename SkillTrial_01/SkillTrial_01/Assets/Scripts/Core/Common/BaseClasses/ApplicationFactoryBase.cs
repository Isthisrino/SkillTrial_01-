using Elder.Core.Common.Interfaces;
using Elder.Core.GameFlow.Application;
using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.Common.BaseClasses
{
    public class ApplicationFactoryBase : DisposableBase, IApplicationFactory
    {
        public bool TryBuildApplication(ISystemLocator serviceProvider, out IEnumerable<KeyValuePair<Type, IApplication>> appServices)
        {
            appServices = BuildAppStstemDictionary(serviceProvider);
            return appServices != null && appServices.Any(); // 논리 수정: null이 아니고, 비어있지 않음
        }

        private Dictionary<Type, IApplication> BuildAppStstemDictionary(ISystemLocator serviceProvider)
        {
            var appServices = new Dictionary<Type, IApplication>();
            RegisterCoreAppServices(appServices, serviceProvider);
            RegisterAdditionalAppServices(appServices, serviceProvider);
            return appServices;
        }
        private void RegisterCoreAppServices(Dictionary<Type, IApplication> appServices, ISystemLocator serviceProvider)
        {
            RegisterAppServices<GameFlowApplication>(appServices, serviceProvider);
        }
        protected void RegisterAppServices<T>(Dictionary<Type, IApplication> appServices, ISystemLocator serviceProvider) where T : ApplicationBase, new()    
        {
            var appService = new T();
            appService.SetSystemProvider(serviceProvider);
            appServices[typeof(T)] = appService;
        }
        protected virtual void RegisterAdditionalAppServices(Dictionary<Type, IApplication> appServices, ISystemLocator serviceProvider)
        {

        }

        protected override void DisposeManagedResources()
        {

        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
