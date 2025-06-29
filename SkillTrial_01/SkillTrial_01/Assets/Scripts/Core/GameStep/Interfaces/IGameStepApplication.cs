using Elder.Core.Common.Interfaces;
using Elder.Core.GameStep.Domain;
using System;

namespace Elder.Core.GameStep.Interfaces
{
    public interface IGameStepApplication : IApplication, IDisposable
    {
        public IObservable<(MainStep, MainStep)> MainStepChanged { get; }
    }
}
