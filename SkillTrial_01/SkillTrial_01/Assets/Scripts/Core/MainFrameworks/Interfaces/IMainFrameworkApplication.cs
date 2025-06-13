using System;

namespace Elder.Core.MainFrameworks.Interfaces
{
    public interface IMainFrameworkApplication : IMainFrameworkServiceProvider, IEngineEventHandler, IDisposable
    {

    }
}
