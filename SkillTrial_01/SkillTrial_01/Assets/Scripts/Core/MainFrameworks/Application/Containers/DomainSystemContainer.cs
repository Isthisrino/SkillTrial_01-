using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class DomainSystemContainer : DisposableBase
    {
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<DomainSystemContainer>();

        private Dictionary<Type, IDomainSystem> _domainSystems;
        public IEnumerable<IDomainSystem> DomainSystems => _domainSystems.Values;

        public DomainSystemContainer()
        {
            InitializeSystems();
        }
        private void InitializeSystems()
        {
            _domainSystems = new();
        }
        public void RegisterDomainSystems<T>(T domainSystem) where T : IDomainSystem
        {
            var type = typeof(T);
            if (_domainSystems.TryAdd(type, domainSystem))
                return;

            _logger.Error($"DomainSystem of type {type.Name} is already registered.");
        }
        public bool TryGetSDomainSystem<T>(out T domainSystem) where T : IDomainSystem
        {
            if (_domainSystems.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                domainSystem = typed;
                return true;
            }
            domainSystem = default!;
            return false;
        }

        protected override void DisposeManagedResources()
        {
            DisposeDomainSystems();
            DisposeLogger();
        }
        private void DisposeLogger()
        {
            _logger = null;
        }
        private void DisposeDomainSystems()
        {
            foreach (var domainSystem in _domainSystems.Values)
                domainSystem.Dispose();
            _domainSystems.Clear();
            _domainSystems = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }
    }
}