using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IDomainSystemFactory : IDisposable
    {
        public bool TryBuildDomainSystems(ISystemProvider domainSystemProvider, out IEnumerable<KeyValuePair<Type, IDomainSystem>> domainSystems);
    }
}
