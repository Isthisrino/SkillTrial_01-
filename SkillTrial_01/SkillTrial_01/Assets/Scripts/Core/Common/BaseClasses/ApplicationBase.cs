using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class ApplicationBase : DisposableBase, IApplication
    {
        private ISystemLocator _systemProvider;

        protected bool TryGetDomain<T>(out T targetDomainSystem) where T : IDomain => _systemProvider.TryGetDomain<T>(out targetDomainSystem);
        protected bool TryGetAppSystem<T>(out T targetAppSystem) where T : IApplication => _systemProvider.TryGetApplication<T>(out targetAppSystem);

        public abstract void Initialize();
        public abstract void PostInitialize();
        public void SetSystemProvider(ISystemLocator systemProvider)
        {
            _systemProvider = systemProvider;
        }
        protected override void DisposeManagedResources()
        {
            ClearSystemProvider();
        }
        private void ClearSystemProvider()
        {
            _systemProvider = null;
        }
        protected override void DisposeUnmanagedResources()
        {

        }

      
    }
}