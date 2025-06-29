using Elder.Core.Common.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class InfrastructureBase : DisposableBase, IInfrastructure
    {
        public abstract void Initialize();
        protected override void DisposeManagedResources()
        {

        }
        protected override void DisposeUnmanagedResources()
        {

        }
    }
}
