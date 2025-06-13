using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkInfrastructureProvider
    {
        public IDomainSystemFactory DomainSystemFactory { get; }
        public IAppSystemFactory AppSystemFactory { get; }
    }
}
