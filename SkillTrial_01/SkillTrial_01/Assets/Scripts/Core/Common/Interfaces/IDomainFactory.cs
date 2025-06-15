using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IDomainFactory : IDisposable
    {
        public bool TryBuildDomains(ISystemLocator domainSystemProvider, out IEnumerable<KeyValuePair<Type, IDomain>> domainSystems);
    }
}
