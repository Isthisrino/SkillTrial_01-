using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IInfrastructureFactory : IDisposable
    {
        public bool TryBuildInfrastructures(ISystemLocator serviceProvider, out IEnumerable<KeyValuePair<Type, IInfrastructure>> infrastructures);
    }
}
