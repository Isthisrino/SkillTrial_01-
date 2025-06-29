using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class ApplicationContainer : DisposableBase
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<ApplicationContainer>();

        private Dictionary<Type, IApplication> _applications;
        public IEnumerable<IApplication> Applications => _applications.Values;
        public ApplicationContainer()
        {
            InitializeAppSystems();
        }
        private void InitializeAppSystems()
        {
            _applications = new();
        }
        public void RegisterApplication<T>(T appSystem) where T : IApplication
        {
            var type = typeof(T);
            if (_applications.TryAdd(type, appSystem))
                return;

            _logger.Error($"AppService of type {type.Name} is already registered.");
        }
        public bool TryGetApplication<T>(out T application) where T : IApplication
        {
            if (_applications.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                application = typed;
                return true;
            }
            application = default!;
            return false;
        }
        protected override void DisposeManagedResources()
        {
            DisposeAppSystems();
            DisposeLogger();
        }
        private void DisposeAppSystems()
        {
            foreach (var appService in _applications.Values)
                appService.Dispose();
            _applications.Clear();
            _applications = null;
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
