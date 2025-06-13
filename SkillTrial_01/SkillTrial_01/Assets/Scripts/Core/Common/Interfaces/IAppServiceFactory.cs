using Elder.Core.MainFrameworks.Interfaces;
using System;
using System.Collections.Generic;

namespace Elder.Core.Common.Interfaces
{
    public interface IAppServiceFactory : IDisposable
    {
        public bool TryBuildAppServices(IMainFrameworkServiceProvider serviceProvider, out IEnumerable<KeyValuePair<Type, IAppService>> appServices);
    }
}
