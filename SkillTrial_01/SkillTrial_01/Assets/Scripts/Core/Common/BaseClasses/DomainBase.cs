using Elder.Core.Common.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class DomainBase : DisposableBase, IDomain
    {
        public abstract void Initialize();
        public abstract void PostInitialize();

        protected override void DisposeManagedResources()
        {

        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
