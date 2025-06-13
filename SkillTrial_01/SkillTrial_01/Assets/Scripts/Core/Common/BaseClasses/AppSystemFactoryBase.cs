using Elder.Core.Common.Interfaces;
using Elder.Core.GameFlow.Application;
using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.Common.BaseClasses
{
    public class AppSystemFactoryBase : DisposableBase, IAppSystemFactory
    {
        public bool TryBuildAppSystems(ISystemProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IAppSystem>> appServices)
        {
            appServices = BuildAppStstemDictionary(serviceProvider);
            return appServices != null && appServices.Any(); // 논리 수정: null이 아니고, 비어있지 않음
        }

        private Dictionary<Type, IAppSystem> BuildAppStstemDictionary(ISystemProvider serviceProvider)
        {
            var appServices = new Dictionary<Type, IAppSystem>();
            RegisterCoreAppServices(appServices, serviceProvider);
            RegisterAdditionalAppServices(appServices, serviceProvider);
            return appServices;
        }
        private void RegisterCoreAppServices(Dictionary<Type, IAppSystem> appServices, ISystemProvider serviceProvider)
        {
            RegisterAppServices<GameFlowAppSystem>(appServices, serviceProvider);
        }
        protected void RegisterAppServices<T>(Dictionary<Type, IAppSystem> appServices, ISystemProvider serviceProvider) where T : AppSystemBase, new()    
        {
            var appService = new T();
            appService.SetSystemProvider(serviceProvider);
            appServices[typeof(T)] = appService;
        }
        protected virtual void RegisterAdditionalAppServices(Dictionary<Type, IAppSystem> appServices, ISystemProvider serviceProvider)
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
