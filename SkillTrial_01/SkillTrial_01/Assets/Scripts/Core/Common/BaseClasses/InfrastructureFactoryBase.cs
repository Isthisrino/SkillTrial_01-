using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;
using Elder.Unity.GameStep.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.Common.BaseClasses
{
    public class InfrastructureFactoryBase : DisposableBase, IInfrastructureFactory
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<InfrastructureFactoryBase>();

        public bool TryBuildInfrastructures(ISystemLocator serviceProvider, out IEnumerable<KeyValuePair<Type, IInfrastructure>> infrastructures)
        {
            infrastructures = BuildInfrastructureDictionary(serviceProvider);
            return infrastructures != null && infrastructures.Any();
        }
        private IEnumerable<KeyValuePair<Type, IInfrastructure>> BuildInfrastructureDictionary(ISystemLocator serviceProvider)
        {
            var infrastructures = new Dictionary<Type, IInfrastructure>();
            RegisterCoreInfrastructures(infrastructures, serviceProvider);
            RegisterAdditionalInfrastructures(infrastructures, serviceProvider);
            return infrastructures;
        }

        private void RegisterCoreInfrastructures(Dictionary<Type, IInfrastructure> infrastructures, ISystemLocator systemLocator)
        {
            RegisterInfrastructures<UnitySceneGameStepExecutor>(infrastructures, systemLocator);
        }
        protected void RegisterInfrastructures<T>(Dictionary<Type, IInfrastructure> infrastructures, ISystemLocator serviceProvider) where T : InfrastructureBase, new()
        {
            var infrastructureType = typeof(T);
            if (infrastructures.ContainsKey(infrastructureType))
            {
                return;
            }
            infrastructures[infrastructureType] = new T();
        }
        protected virtual void RegisterAdditionalInfrastructures(Dictionary<Type, IInfrastructure> infrastructures, ISystemLocator serviceProvider)
        {

        }

        protected override void DisposeManagedResources()
        {
            ClearLogger();
        }
        private void ClearLogger()
        {
            _logger = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
