using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IAppSystemFactory : IDisposable
    {
        public bool TryBuildAppSystems(ISystemProvider domainSystemProvider, out IEnumerable<KeyValuePair<Type, IAppSystem>> appSystems);
    }
}
