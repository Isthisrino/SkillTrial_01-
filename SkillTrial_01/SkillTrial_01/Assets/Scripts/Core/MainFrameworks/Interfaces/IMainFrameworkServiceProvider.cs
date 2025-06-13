using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkServiceProvider
    {
        public bool TryGetService<T>(out T targetService) where T : IService;
        public bool TryGetAppService<T>(out T targetAppService) where T : IAppService; 
    }
}
