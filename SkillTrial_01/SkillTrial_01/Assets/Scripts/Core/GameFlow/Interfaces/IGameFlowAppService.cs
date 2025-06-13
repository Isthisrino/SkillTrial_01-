using Elder.Core.Common.Enums;
using Elder.Core.Common.Interfaces;
using System;

namespace Elder.Core.GameFlow.Interfaces
{
    public interface IGameFlowAppService : IAppService
    {
        public IObservable<GameFlowState> GameFlowStateChanged { get; }
        public void ChangeState(GameFlowState targetState);
    }
}
