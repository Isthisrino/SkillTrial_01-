using System;

namespace Elder.Core.Common.Interfaces
{
    public interface IAppSystem : IDisposable
    {
        public void Initialize();
        public void PostInitialize();
    }
}