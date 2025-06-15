using System;

namespace Elder.Core.Common.Interfaces
{
    public interface IDomain : IDisposable
    {
        public void Initialize();
        public void PostInitialize();
    }
}