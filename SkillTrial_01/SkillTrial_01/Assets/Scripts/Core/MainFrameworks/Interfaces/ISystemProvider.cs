using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface ISystemProvider
    {
        public bool TryGetSystem<T>(out T targetSystem) where T : IDomainSystem;
        public bool TryGetAppSystem<T>(out T targetAppSystem) where T : IAppSystem; 
    }
}
