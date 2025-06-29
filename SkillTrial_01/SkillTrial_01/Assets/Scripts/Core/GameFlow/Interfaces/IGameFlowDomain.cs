using Elder.Core.Common.Enums;
using Elder.Core.Common.Interfaces;

namespace Elder.Core.GameFlow.Interfaces
{
    public interface IGameFlowDomain : IDomain
    {
        public GameFlowState CurrentFlowState { get; }
        public bool TryChangeFlowState(GameFlowState nextState);
    }
}
