using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IApplicationFactory : IDisposable
    {
        public bool TryBuildApplication(ISystemLocator domainSystemProvider, out IEnumerable<KeyValuePair<Type, IApplication>> appSystems);
    }
}
