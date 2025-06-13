using Elder.Core.Common.BaseClasses;
using Elder.Core.GameFlow.Interfaces;
using Elder.Core.GameStep.Interfaces;

namespace Elder.Core.GameStep.Application
{
    public class GameStepAppSystem : AppSystemBase, IGameStepAppSystem
    {
        private IGameStepSystem _gameStepSystem;

        public override void Initialize()
        {
            BindGameStepSystem();
        }
        private void BindGameStepSystem()
        {
            if (!TryGetDomainSystem<IGameStepSystem>(out var targetDomainSystem))
                return;

            _gameStepSystem = targetDomainSystem;
        }
        public override void PostInitialize()
        {
            SubscribeToGameFlowState();
        }
        private void SubscribeToGameFlowState()
        {
            
        }
    }
}
