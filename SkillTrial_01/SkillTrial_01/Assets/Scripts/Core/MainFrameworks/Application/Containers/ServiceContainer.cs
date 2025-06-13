using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class ServiceContainer : DisposableBase
    {
        private ILogger _logger = LogAppService.In.CreateLogger<ServiceContainer>();

        private Dictionary<Type, IService> _services;

        public ServiceContainer()
        {
            InitializeServices();
        }
        private void InitializeServices()
        {
            _services = new();
        }
        public void RegisterServices<T>(T service) where T : IService
        {
            var type = typeof(T);
            if (_services.TryAdd(type, service))
                return;

            _logger.Error($"Service of type {type.Name} is already registered.");
        }
        public bool TryGetService<T>(out T service) where T : IService
        {
            if (_services.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                service = typed;
                return true;
            }
            service = default!;
            return false;
        }

        protected override void DisposeManagedResources()
        {
            DisposeServices();
            DisposeLogger();
        }
        private void DisposeLogger()
        {
            _logger = null;
        }
        private void DisposeServices()
        {
            foreach (var service in _services.Values)
                service.Dispose();
            _services.Clear();
            _services = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }
    }
}