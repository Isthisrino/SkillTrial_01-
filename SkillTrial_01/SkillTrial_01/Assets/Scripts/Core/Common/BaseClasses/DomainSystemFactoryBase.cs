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
    public class DomainSystemFactoryBase : DisposableBase, IDomainSystemFactory
    {
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<DomainSystemFactoryBase>();

        public bool TryBuildDomainSystems(ISystemProvider systemProvider, out IEnumerable<KeyValuePair<Type, IDomainSystem>> domainSystems)
        {
            domainSystems = BuildDomainSystems(systemProvider);
            return domainSystems != null && domainSystems.Any();
        }
        private IEnumerable<KeyValuePair<Type, IDomainSystem>> BuildDomainSystems(ISystemProvider systemProvider)
        {
            var services = new Dictionary<Type, IDomainSystem>();
            RegisterCoreDomainSystems(services, systemProvider);
            RegisterAdditionalDomainSystems(services, systemProvider);
            return services;
        }
        private void RegisterCoreDomainSystems(Dictionary<Type, IDomainSystem> systems, ISystemProvider systemProvider)
        {
            RegisterDomainSystem<GameFlowSystem>(systems, systemProvider);
        }
        protected void RegisterDomainSystem<T>(Dictionary<Type, IDomainSystem> systems, ISystemProvider systemProvider) where T : DomainSystemBase, new()
        {
            var systemType = typeof(T);
            if (systems.ContainsKey(systemType))
            {
                _logger.Error($"[SystemFactory] System of type {systemType.Name} is already registered. Skipping duplicate registration.");
                return;
            }
            systems[systemType] = new T();
        }
        protected virtual void RegisterAdditionalDomainSystems(Dictionary<Type, IDomainSystem> systems, ISystemProvider systemProvider)
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
