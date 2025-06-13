using System;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkApplication : ISystemProvider, IAppSystemProvider, IEngineEventHandler, IDisposable
    {

    }
}
