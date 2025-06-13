using Elder.Core.Common.Enums;

namespace Elder.Core.GameFlow.Interfaces
{
    public interface IGameFlowService 
    {
        public GameFlowState CurrentFlowState { get; }
        public bool TryChangeFlowState(GameFlowState nextState);
    }
}
