using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;
using Elder.Core.GameStep.Interfaces;
using System;

namespace Elder.Core.GameStep.Application
{
    public class GameStepAppSystem : AppSystemBase, IGameStepAppSystem
    {
        private IGameStepSystem _gameStepSystem;
        private IDisposable _gameFlowStateSubscription;

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
            if (!TryGetAppSystem<IGameFlowAppSystem>(out var gmaeFlowAppSystem))
                return;

            _gameFlowStateSubscription = gmaeFlowAppSystem.GameFlowStateChanged.Subscribe(HandleGameFlowStateChanged);
        }
        private void HandleGameFlowStateChanged(GameFlowState changedState)
        {
            
        }
        protected override void DisposeManagedResources()
        {
            DisposeGameFlowStateSubscription();
            ClearGameStepSystem();

            base.DisposeManagedResources();
        }
        private void DisposeGameFlowStateSubscription()
        {
            _gameFlowStateSubscription.Dispose();
            _gameFlowStateSubscription = null;
        }
        private void ClearGameStepSystem()
        {
            _gameStepSystem = null;
        }
    }
}
