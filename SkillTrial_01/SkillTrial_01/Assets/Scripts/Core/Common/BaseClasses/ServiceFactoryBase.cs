using Elder.Core.Common.Interfaces;
using Elder.Core.GameFlow.Domain;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.Common.BaseClasses
{
    public class ServiceFactoryBase : DisposableBase, IServiceFactory
    {
        private ILogger _logger = LogAppService.In.CreateLogger<ServiceFactoryBase>();
        private Dictionary<Type, Func<IService>> _serviceConstructors;

        public ServiceFactoryBase()
        {
            InitializeServiceConstructors();
            RegisterCoreServiceConstructors();
            RegisterAdditionalServiceConstructors();
        }
        private void InitializeServiceConstructors()
        {
            _serviceConstructors = new();
        }
        private void RegisterCoreServiceConstructors()
        {
            RegisterServiceConstructor<GameFlowService>();
        }
        protected void RegisterServiceConstructor<T>() where T : IService, new()
        {
            _serviceConstructors.Add(typeof(T), () => new T());
        }
        protected virtual void RegisterAdditionalServiceConstructors()
        {

        }
        public bool TryBuildCoreServices(IMainFrameworkServiceProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IService>> coreServices)
        {
            coreServices = BuildCoreServices(serviceProvider);
            return coreServices != null && coreServices.Any();
        }
        private IEnumerable<KeyValuePair<Type, IService>> BuildCoreServices(IMainFrameworkServiceProvider serviceProvider)
        {
            var services = new Dictionary<Type, IService>();
            RegisterCoreServices(services, serviceProvider);
            return services;
        }
        private void RegisterCoreServices(Dictionary<Type, IService> services, IMainFrameworkServiceProvider serviceProvider)
        {
            BuildrService<GameFlowService>(services, serviceProvider);
        }
        protected void BuildrService<T>(Dictionary<Type, IService> services, IMainFrameworkServiceProvider serviceProvider) where T : ServiceBase, new()
        {
            var serviceType = typeof(T);
            if (services.ContainsKey(serviceType))
            {
                _logger.Error($"[ServiceFactory] Service of type {serviceType.Name} is already registered. Skipping duplicate registration.");
                return;
            }
            services[serviceType] = new T();
        }
        protected virtual void RegisterAdditionalServices(Dictionary<Type, IService> services, IMainFrameworkServiceProvider serviceProvider)
       {

        }
        protected override void DisposeManagedResources()
        {
            DisposeServiceConstructors();
            DisposeLogger();
        }
        private void DisposeServiceConstructors()
        {
            _serviceConstructors.Clear();
            _serviceConstructors = null;
        }
        private void DisposeLogger()
        {
            _logger = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }

       
    }
}
