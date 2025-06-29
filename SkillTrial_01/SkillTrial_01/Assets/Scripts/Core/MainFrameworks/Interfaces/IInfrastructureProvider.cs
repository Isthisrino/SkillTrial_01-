using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IInfrastructureProvider 
    {
        public bool TryGetInfrastructure<T>(out T targetInfrastructure) where T : IInfrastructure;
    }
}
