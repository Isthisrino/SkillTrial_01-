using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.MainFrameworks.Application.Container
{
    public class DomainContainer : DisposableBase
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<DomainContainer>();

        private Dictionary<Type, IDomain> _domains;
        public IEnumerable<IDomain> Domains => _domains.Values;

        public DomainContainer()
        {
            InitializeDomains();
        }
        private void InitializeDomains()
        {
            _domains = new();
        }
        public void RegisterDomain<T>(T domain) where T : IDomain
        {
            var type = typeof(T);
            if (_domains.TryAdd(type, domain))
                return;

            _logger.Error($"DomainSystem of type {type.Name} is already registered.");
        }
        public bool TryGetDomain<T>(out T domainSystem) where T : IDomain
        {
            if (_domains.TryGetValue(typeof(T), out var s) && s is T typed)
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
            foreach (var domainSystem in _domains.Values)
                domainSystem.Dispose();
            _domains.Clear();
            _domains = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }
    }
}