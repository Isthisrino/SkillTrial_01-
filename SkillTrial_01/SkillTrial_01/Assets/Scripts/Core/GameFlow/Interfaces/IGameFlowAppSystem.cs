using Elder.Core.Common.Enums;
using Elder.Core.Common.Interfaces;
using System;

namespace Elder.Core.GameFlow.Interfaces
{
    public interface IGameFlowAppSystem : IAppSystem
    {
        public GameFlowState CurrentFlowState { get; }
        public IObservable<GameFlowState> GameFlowStateChanged { get; }
        public void ChangeFlowState(GameFlowState targetState);
    }
}
