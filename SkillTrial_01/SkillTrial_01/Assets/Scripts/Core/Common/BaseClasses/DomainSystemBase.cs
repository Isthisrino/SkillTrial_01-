using Elder.Core.Common.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class DomainSystemBase : DisposableBase, IDomainSystem
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
