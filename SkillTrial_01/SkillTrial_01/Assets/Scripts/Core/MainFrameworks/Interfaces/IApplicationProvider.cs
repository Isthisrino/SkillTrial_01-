using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IApplicationProvider 
    {
        public bool TryGetApplication<T>(out T targetApplication) where T : IApplication;
    }
}