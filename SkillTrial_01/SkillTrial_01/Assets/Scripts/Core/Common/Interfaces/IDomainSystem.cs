using System;

namespace Elder.Core.Common.Interfaces
{
    public interface IDomainSystem : IDisposable
    {
        public void Initialize();
        public void PostInitialize();
    }
}