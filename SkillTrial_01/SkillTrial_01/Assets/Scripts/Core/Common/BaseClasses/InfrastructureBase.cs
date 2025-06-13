using Elder.Core.Common.Interfaces;

namespace Elder.Core.Common.BaseClasses
{
    public class InfrastructureBase : DisposableBase, IInfrastructure
    {
        protected override void DisposeManagedResources()
        {
        }

        protected override void DisposeUnmanagedResources()
        {
        }
    }
}
