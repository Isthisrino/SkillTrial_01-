using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface ISystemLocator
    {
        public bool TryGetDomain<T>(out T targetDomain) where T : IDomain;
        public bool TryGetApplication<T>(out T targetApplication) where T : IApplication; 
    }
}
