using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IAppSystemProvider 
    {
        public bool TryGetAppSystem<T>(out T targetAppSystem) where T : IAppSystem;
    }
}