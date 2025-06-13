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
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<MainFrameworkApplication>();

        private DomainSystemContainer _domainSystemContainer;
        private AppSystemContainer _appSystemContainer;

        public MainFrameworkApplication(IMainFrameworkInfrastructureProvider provider)
        {
            InitializeDomainLayer(provider.DomainSystemFactory);
            InitializeAppLayer(provider.AppSystemFactory);
            StartApplicationFlow();
        }
        private void StartApplicationFlow()
        {
            if (!TryGetAppSystem<IGameFlowAppSystem>(out var gameFlowAppSystem))
            {
                _logger.Error("[StartApplicationFlow] Failed to find IGameFlowAppSystem. Cannot start application flow.");
                return;
            }
            gameFlowAppSystem.ChangeFlowState(GameFlowState.Boot);
        }
        private void InitializeDomainLayer(IDomainSystemFactory domainFactory)
        {
            InitializeDomainSystemContainer();
            RegisterDomainSystems(domainFactory);
            InitializeDomainSystems();
        }
        private void InitializeAppLayer(IAppSystemFactory appFactory)
        {
            InitializeAppSystemContainer();
            RegisterAppSystems(appFactory);
            InitializeAppSystems();
            PostInitializeAppSystems();
        }
        private void PostInitializeAppSystems()
        {
            var appSystems = _appSystemContainer.AppSystems;
            foreach (var appSystem in appSystems)
                appSystem.PostInitialize();
        }
        private void InitializeDomainSystems()
        {
            var domainSystems = _domainSystemContainer.DomainSystems;
            foreach (var domainSystem in domainSystems)
                domainSystem.Initialize();
        }
        private void InitializeAppSystems()
        {
            var appSystems = _appSystemContainer.AppSystems;
            foreach (var appSystem in appSystems)
                appSystem.Initialize();
        }
        private void RegisterDomainSystems(IDomainSystemFactory domainSystemFactory)
        {
            if (!domainSystemFactory.TryBuildDomainSystems(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _domainSystemContainer.RegisterDomainSystems(pair.Value);
        }
        private void RegisterAppSystems(IAppSystemFactory appSystemFactory)
        {
            if (!appSystemFactory.TryBuildAppSystems(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _appSystemContainer.RegisterAppSystem(pair.Value);
        }
        private void InitializeDomainSystemContainer()
        {
            _domainSystemContainer = new();
        }
        private void InitializeAppSystemContainer()
        {
            _appSystemContainer = new();
        }
        public bool TryGetSystem<T>(out T targetService) where T : IDomainSystem
        {
            return _domainSystemContainer.TryGetSDomainSystem(out targetService);
        }
        public bool TryGetAppSystem<T>(out T targetAppService) where T : IAppSystem
        {
            return _appSystemContainer.TryGetAppSystem(out targetAppService);
        }
        protected override void DisposeManagedResources()
        {
            ClearLogger();

            DisposeAppSystemContainer();
            DisposeDomainSystemContainer();
            DisposeLogAppSystem();
        }
        private void ClearLogger()
        {
            _logger = null;
        }
        private void DisposeLogAppSystem()
        {
            LogAppSystem.In.Dispose();
        }
        private void DisposeDomainSystemContainer()
        {
            _domainSystemContainer.Dispose();
            _domainSystemContainer = null;
        }
        private void DisposeAppSystemContainer()
        {
            _appSystemContainer.Dispose();
            _appSystemContainer = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}