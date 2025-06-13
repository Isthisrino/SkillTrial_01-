using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class AppSystemBase : DisposableBase, IAppSystem
    {
        private ISystemProvider _systemProvider;

        protected bool TryGetDomainSystem<T>(out T targetDomainSystem) where T : IDomainSystem => _systemProvider.TryGetSystem<T>(out targetDomainSystem);
        protected bool TryGetAppSystem<T>(out T targetAppSystem) where T : IAppSystem => _systemProvider.TryGetAppSystem<T>(out targetAppSystem);

        public abstract void Initialize();
        public abstract void PostInitialize();
        public void SetSystemProvider(ISystemProvider systemProvider)
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