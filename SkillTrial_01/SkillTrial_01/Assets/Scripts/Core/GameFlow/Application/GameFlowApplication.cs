using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;
using R3;
using System;

namespace Elder.Core.GameFlow.Application
{
    public class GameFlowApplication : ApplicationBase, IGameFlowApplication
    {
        private IGameFlowDomain _gameFlowDomain;
        private ReactiveCommand<GameFlowState> _changeGameFlowStateCommand;

        public GameFlowState CurrentFlowState =>_gameFlowDomain.CurrentFlowState;
        public IObservable<GameFlowState> GameFlowStateChanged { get; protected set; }

        public override void Initialize()
        {
            BindGameFlowDomain();
            InitializeGameFlowStateCommand();
            BindGameFlowStateObservable();
        }
        public override void PostInitialize()
        {
            
        }
        private void BindGameFlowDomain()
        {
            if (!TryGetDomain<IGameFlowDomain>(out var targetDomain))
                return;

            _gameFlowDomain = targetDomain;
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
            if (!_gameFlowDomain.TryChangeFlowState(targetFlowState))
                return;

            _changeGameFlowStateCommand.Execute(targetFlowState);
        }
        protected override void DisposeManagedResources()
        {
            ClearGameFlowStateObservable();
            DisposeGameFlowStateCommand();
            ClearGmaeFlowDomain();

            base.DisposeManagedResources();
        }
        private void ClearGmaeFlowDomain()
        {
            _gameFlowDomain = null;
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
