using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;
using R3;
using System;

namespace Elder.Core.GameFlow.Application
{
    public class GameFlowAppSystem : AppSystemBase, IGameFlowAppSystem
    {
        private IGameFlowSystem _gameFlowSystem;
        private ReactiveCommand<GameFlowState> _changeGameFlowStateCommand;

        public GameFlowState CurrentFlowState =>_gameFlowSystem.CurrentFlowState;
        public IObservable<GameFlowState> GameFlowStateChanged { get; protected set; }

        public override void Initialize()
        {
            BindGameFlowSystem();
            InitializeGameFlowStateCommand();
            BindGameFlowStateObservable();
        }
        public override void PostInitialize()
        {
            
        }
        private void BindGameFlowSystem()
        {
            if (!TryGetDomainSystem<IGameFlowSystem>(out var targetDomainSystem))
                return;

            _gameFlowSystem = targetDomainSystem;
        }
        private void InitializeGameFlowStateCommand()
        {
            _changeGameFlowStateCommand = new();
        }
        private void BindGameFlowStateObservable()
        {
            GameFlowStateChanged = _changeGameFlowStateCommand.AsSystemObservable();
        }
        public void ChangeFlowState(GameFlowState targetFlowState)
        {
            if (!_gameFlowSystem.TryChangeFlowState(targetFlowState))
                return;

            _changeGameFlowStateCommand.Execute(targetFlowState);
        }
        protected override void DisposeManagedResources()
        {
            ClearGameFlowStateObservable();
            DisposeGameFlowStateCommand();
            ClearGmaeFlowSystem();
        }
        private void ClearGmaeFlowSystem()
        {
            _gameFlowSystem = null;
        }
        private void ClearGameFlowStateObservable()
        {
            GameFlowStateChanged = null;
        }
        private void DisposeGameFlowStateCommand()
        {
            _changeGameFlowStateCommand.Dispose();
            _changeGameFlowStateCommand = null;
        }

     
    }
}
