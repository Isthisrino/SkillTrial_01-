using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class AppServiceBase : DisposableBase, IAppService
    {
        private IMainFrameworkServiceProvider _serviceProvider;

        protected bool TryGetAppService<T>(out T targetService) where T : IAppService =>_serviceProvider.TryGetAppService<T>(out targetService);

        public void SetServiceProvider(IMainFrameworkServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
     
        protected override void DisposeManagedResources()
        {
            DisposeServiceProvider();
        }
        private void DisposeServiceProvider()
        {
            _serviceProvider = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}