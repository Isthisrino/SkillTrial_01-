using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class AppServiceContainer : DisposableBase
    {
        private ILogger _logger = LogAppService.In.CreateLogger<AppServiceContainer>();

        private Dictionary<Type, IAppService> _appServices;
        public AppServiceContainer()
        {
            InitializeAppServices();
        }
        private void InitializeAppServices()
        {
            _appServices = new();
        }
        public void RegisterAppService<T>(T appService) where T : IAppService
        {
            var type = typeof(T);
            if (_appServices.TryAdd(type, appService))
                return;

            _logger.Error($"AppService of type {type.Name} is already registered.");
        }
        public bool TryGetAppService<T>(out T appService) where T : IAppService
        {
            if (_appServices.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                appService = typed;
                return true;
            }
            appService = default!;
            return false;
        }
        protected override void DisposeManagedResources()
        {
            DisposeAppServices();
            DisposeLogger();
        }
        private void DisposeAppServices()
        {
            foreach (var appService in _appServices.Values)
                appService.Dispose();
            _appServices.Clear();
            _appServices = null;
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
