using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Elder.Core.Logging.Interfaces;
using Elder.Core.Logging.Application;

namespace Elder.Core.Common.BaseClasses
{
    public class InfrastructureFactoryBase : DisposableBase, IInfrastructureFactory
    {
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<InfrastructureFactoryBase>();

        public bool TryBuildInfrastructures(ISystemProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IInfrastructure>> infrastructures)
        {
            infrastructures = BuildInfrastructureDictionary(serviceProvider);
            return infrastructures != null && infrastructures.Any();
        }
        private IEnumerable<KeyValuePair<Type, IInfrastructure>> BuildInfrastructureDictionary(ISystemProvider serviceProvider)
        {
            var infrastructures = new Dictionary<Type, IInfrastructure>();
            RegisterCoreInfrastructures(infrastructures, serviceProvider);
            RegisterAdditionalInfrastructures(infrastructures, serviceProvider);
            return infrastructures;
        }

        private void RegisterCoreInfrastructures(Dictionary<Type, IInfrastructure> infrastructures, ISystemProvider serviceProvider)
        {

        }
        protected void RegisterInfrastructures<T>(Dictionary<Type, IInfrastructure> infrastructures, ISystemProvider serviceProvider) where T : InfrastructureBase, new()
        {
            var infrastructureType = typeof(T);
            if (infrastructures.ContainsKey(infrastructureType))
            {
                return;
            }
            infrastructures[infrastructureType] = new T();
        }
        protected virtual void RegisterAdditionalInfrastructures(Dictionary<Type, IInfrastructure> infrastructures, ISystemProvider serviceProvider)
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
