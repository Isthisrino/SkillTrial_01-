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
    public class DomainFactoryBase : DisposableBase, IDomainFactory
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<DomainFactoryBase>();

        public bool TryBuildDomains(ISystemLocator systemProvider, out IEnumerable<KeyValuePair<Type, IDomain>> domainSystems)
        {
            domainSystems = BuildDomainSystems(systemProvider);
            return domainSystems != null && domainSystems.Any();
        }
        private IEnumerable<KeyValuePair<Type, IDomain>> BuildDomainSystems(ISystemLocator systemProvider)
        {
            var services = new Dictionary<Type, IDomain>();
            RegisterCoreDomainSystems(services, systemProvider);
            RegisterAdditionalDomainSystems(services, systemProvider);
            return services;
        }
        private void RegisterCoreDomainSystems(Dictionary<Type, IDomain> systems, ISystemLocator systemProvider)
        {
            RegisterDomainSystem<GameFlowDomain>(systems, systemProvider);
        }
        protected void RegisterDomainSystem<T>(Dictionary<Type, IDomain> systems, ISystemLocator systemProvider) where T : DomainBase, new()
        {
            var systemType = typeof(T);
            if (systems.ContainsKey(systemType))
            {
                _logger.Error($"[SystemFactory] System of type {systemType.Name} is already registered. Skipping duplicate registration.");
                return;
            }
            systems[systemType] = new T();
        }
        protected virtual void RegisterAdditionalDomainSystems(Dictionary<Type, IDomain> systems, ISystemLocator systemProvider)
        {

        }
        protected override void DisposeManagedResources()
        {
            DisposeLogger();
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
