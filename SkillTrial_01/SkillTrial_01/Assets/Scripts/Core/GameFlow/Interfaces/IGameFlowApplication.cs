using Elder.Core.Common.Enums;
using Elder.Core.Common.Interfaces;
using System;

namespace Elder.Core.GameFlow.Interfaces
{
    public interface IGameFlowApplication : IApplication
    {
        public IObservable<GameFlowState> GameFlowStateChanged { get; }
        public void ChangeFlowState(GameFlowState targetState);
    }
}
