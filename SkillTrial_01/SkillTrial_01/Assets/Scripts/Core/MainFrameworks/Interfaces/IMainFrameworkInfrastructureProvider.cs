using Elder.Core.Common.Interfaces;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IFactoryProvider
    {
        public IDomainFactory DomainFactory { get; }
        public IApplicationFactory ApplicationFactory { get; }
      
    }
}
