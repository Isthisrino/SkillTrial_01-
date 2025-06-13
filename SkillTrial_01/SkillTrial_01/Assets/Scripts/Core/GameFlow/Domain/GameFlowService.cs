using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;

namespace Elder.Core.GameFlow.Domain
{
    public class GameFlowService : ServiceBase, IGameFlowService
    {
        private GameFlowState _currentFlowState;
        public GameFlowState CurrentFlowState => _currentFlowState;

        public bool TryChangeFlowState(GameFlowState nextState)
        {
            if (_currentFlowState == nextState)
                return false;

            _currentFlowState = nextState;
            return true;
        }
    }
}
