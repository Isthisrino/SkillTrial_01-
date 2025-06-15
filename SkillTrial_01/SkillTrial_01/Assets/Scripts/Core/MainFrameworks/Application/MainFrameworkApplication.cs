using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.Common.Interfaces;
using Elder.Core.GameFlow.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using Elder.Core.MainFrameworks.Application.Container;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.MainFrameworks.Application
{
    public class MainFrameworkApplication : DisposableBase, IMainFrameworkApplication
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<MainFrameworkApplication>();

        private IInfrastructureProvider _infrastructureProvider;

        private DomainContainer _domainContainer;
        private ApplicationContainer _applicationContainer;

        public MainFrameworkApplication(IInfrastructureProvider infrastructureProvider, IFactoryProvider factoryProvider)
        {
            _infrastructureProvider = infrastructureProvider;

            InitializeDomainLayer(factoryProvider.DomainFactory);
            InitializeAppLayer(factoryProvider.ApplicationFactory);
            StartApplicationFlow();
        }
        private void StartApplicationFlow()
        {
            if (!TryGetApplication<IGameFlowApplication>(out var gameFlowAppSystem))
            {
                _logger.Error("[StartApplicationFlow] Failed to find IGameFlowAppSystem. Cannot start application flow.");
                return;
            }
            gameFlowAppSystem.ChangeFlowState(GameFlowState.Boot);
        }
        private void InitializeDomainLayer(IDomainFactory domainFactory)
        {
            InitializeDomainContainer();
            RegisterDomains(domainFactory);
            InitializeDomains();
        }
        private void InitializeAppLayer(IApplicationFactory appFactory)
        {
            InitializeApplicationContainer();
            RegisterApplications(appFactory);
            InitializeApplications();
            PostInitializeApplications();
        }
        private void PostInitializeApplications()
        {
            var applications = _applicationContainer.Applications;
            foreach (var application in applications)
                application.PostInitialize();
        }
        private void InitializeDomains()
        {
            var domains = _domainContainer.Domains;
            foreach (var domain in domains)
                domain.Initialize();
        }
        private void InitializeApplications()
        {
            var applications = _applicationContainer.Applications;
            foreach (var application in applications)
                application.Initialize();
        }
        private void RegisterDomains(IDomainFactory domainFactory)
        {
            if (!domainFactory.TryBuildDomains(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _domainContainer.RegisterDomain(pair.Value);
        }
        private void RegisterApplications(IApplicationFactory appSystemFactory)
        {
            if (!appSystemFactory.TryBuildApplication(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _applicationContainer.RegisterApplication(pair.Value);
        }
        private void InitializeDomainContainer()
        {
            _domainContainer = new();
        }
        private void InitializeApplicationContainer()
        {
            _applicationContainer = new();
        }
        public bool TryGetDomain<T>(out T targetDomain) where T : IDomain
        {
            return _domainContainer.TryGetDomain(out targetDomain);
        }
        public bool TryGetApplication<T>(out T targetApplication) where T : IApplication
        {
            return _applicationContainer.TryGetApplication(out targetApplication);
        }
        protected override void DisposeManagedResources()
        {
            ClearLogger();

            DisposeApplicationContainer();
            DisposeDomainContainer();
            DisposeLogAppSystem();
        }
        private void ClearLogger()
        {
            _logger = null;
        }
        private void DisposeLogAppSystem()
        {
            LogApplication.In.Dispose();
        }
        private void DisposeDomainContainer()
        {
            _domainContainer.Dispose();
            _domainContainer = null;
        }
        private void DisposeApplicationContainer()
        {
            _applicationContainer.Dispose();
            _applicationContainer = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}