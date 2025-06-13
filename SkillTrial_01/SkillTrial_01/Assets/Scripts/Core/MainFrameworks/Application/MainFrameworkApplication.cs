using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.MainFrameworks.Application.Container;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.MainFrameworks.Application
{
    public class MainFrameworkApplication : DisposableBase, IMainFrameworkApplication
    {
        private ServiceContainer _serviceContainer;
        private AppServiceContainer _appServiceContainer;

        public MainFrameworkApplication(IMainFrameworkInfrastructureProvider infrastructureProvider)
        {
            InitializeMainFrameworkApplication(infrastructureProvider);
        }
        private void InitializeMainFrameworkApplication(IMainFrameworkInfrastructureProvider provider)
        {
            InitializeServiceContainer();
            InitializeAppServiceContainer();

            InitializeGeneralServices(provider);
        }
        private void InitializeGeneralServices(IMainFrameworkInfrastructureProvider provider)
        {
            RegisterServices(provider.ServiceFactory);
            RegisterAppServices(provider.AppServiceFactory);
        }
        private void RegisterServices(IServiceFactory serviceFactory)
        {
            if (!serviceFactory.TryBuildCoreServices(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _serviceContainer.RegisterServices(pair.Value);
        }
        private void RegisterAppServices(IAppServiceFactory appServiceFactory)
        {
            if (!appServiceFactory.TryBuildAppServices(this, out var pairs))
                return;

            foreach (var pair in pairs)
                _appServiceContainer.RegisterAppService(pair.Value);
        }
        private void InitializeServiceContainer()
        {
            _serviceContainer = new();
        }
        private void InitializeAppServiceContainer()
        {
            _appServiceContainer = new();
        }
        public bool TryGetService<T>(out T targetService) where T : IService
        {
            return _serviceContainer.TryGetService(out targetService);
        }
        public bool TryGetAppService<T>(out T targetAppService) where T : IAppService
        {
            return _appServiceContainer.TryGetAppService(out targetAppService);
        }
        protected override void DisposeManagedResources()
        {
            DisposeAppServiceContainer();
            DisposeServiceContainer();
            DisposeLogService();
        }
        private void DisposeLogService()
        {
            LogAppService.In.Dispose();
        }
        private void DisposeServiceContainer()
        {
            _serviceContainer.Dispose();
            _serviceContainer = null;
        }
        private void DisposeAppServiceContainer()
        {
            _appServiceContainer.Dispose();
            _appServiceContainer = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}