using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkInfrastructureProvider
    {
        public IServiceFactory ServiceFactory { get; }
        public IAppServiceFactory AppServiceFactory { get; }
    }
}
