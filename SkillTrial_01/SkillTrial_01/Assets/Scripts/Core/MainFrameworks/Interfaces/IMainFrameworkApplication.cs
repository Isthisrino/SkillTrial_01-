using System;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkApplication : ISystemLocator, IApplicationProvider, IEngineEventHandler, IDisposable
    {

    }
}
