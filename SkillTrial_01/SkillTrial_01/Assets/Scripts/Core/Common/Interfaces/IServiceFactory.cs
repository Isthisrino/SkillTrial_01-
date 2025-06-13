using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IServiceFactory : IDisposable
    {
        public bool TryBuildCoreServices(IMainFrameworkServiceProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IService>> service);
    }
}
