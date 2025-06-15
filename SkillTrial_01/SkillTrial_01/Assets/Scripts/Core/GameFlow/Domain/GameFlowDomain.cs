using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;

namespace Elder.Core.GameFlow.Domain
{
    public class GameFlowDomain : DomainBase, IGameFlowDomain
    {
        private GameFlowState _currentFlowState;
        public GameFlowState CurrentFlowState => _currentFlowState;

        public override void Initialize()
        {

        }
        public override void PostInitialize()
        {

        }
        public bool TryChangeFlowState(GameFlowState nextState)
        {
            if (_currentFlowState == nextState)
                return false;

            _currentFlowState = nextState;
            return true;
        }
    }
}
