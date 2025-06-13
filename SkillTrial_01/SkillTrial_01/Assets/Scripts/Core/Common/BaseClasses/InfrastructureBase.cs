using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class InfrastructureBase : DisposableBase, IInfrastructure
    {
        private IAppSystemProvider _appSystemProvider;
        
        protected bool TryGetAppSystem<T>(out T targetAppSystem) where T : IAppSystem => _appSystemProvider.TryGetAppSystem<T>(out targetAppSystem);

        public abstract void Initialize();
        public void SetAppSystemProvider(IAppSystemProvider appSystemProvider)
        {
            _appSystemProvider = appSystemProvider;
        }
        protected override void DisposeManagedResources()
        {
            ClearAppSystemProvider();
        }
        private void ClearAppSystemProvider()
        {
            _appSystemProvider = null;
        }

        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
