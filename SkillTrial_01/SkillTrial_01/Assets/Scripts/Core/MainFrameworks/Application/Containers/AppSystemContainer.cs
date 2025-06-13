using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class AppSystemContainer : DisposableBase
    {
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<AppSystemContainer>();

        private Dictionary<Type, IAppSystem> _appSystems;
        public IEnumerable<IAppSystem> AppSystems => _appSystems.Values;
        public AppSystemContainer()
        {
            InitializeAppSystems();
        }
        private void InitializeAppSystems()
        {
            _appSystems = new();
        }
        public void RegisterAppSystem<T>(T appSystem) where T : IAppSystem
        {
            var type = typeof(T);
            if (_appSystems.TryAdd(type, appSystem))
                return;

            _logger.Error($"AppService of type {type.Name} is already registered.");
        }
        public bool TryGetAppSystem<T>(out T appSystem) where T : IAppSystem
        {
            if (_appSystems.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                appSystem = typed;
                return true;
            }
            appSystem = default!;
            return false;
        }
        protected override void DisposeManagedResources()
        {
            DisposeAppSystems();
            DisposeLogger();
        }
        private void DisposeAppSystems()
        {
            foreach (var appService in _appSystems.Values)
                appService.Dispose();
            _appSystems.Clear();
            _appSystems = null;
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
