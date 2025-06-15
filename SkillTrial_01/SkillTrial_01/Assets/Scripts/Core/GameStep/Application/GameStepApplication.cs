using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Enums;
using Elder.Core.GameFlow.Interfaces;
using Elder.Core.GameStep.Domain;
using Elder.Core.GameStep.Interfaces;
using R3;
using System;

namespace Elder.Core.GameStep.Application
{
    public class GameStepApplication : ApplicationBase, IGameStepApplication
    {
        private IGameStepDomain _gameStepDomain;
        private IDisposable _gameFlowStateSubscription;

        private ReactiveCommand<(MainStep, MainStep)> _changeMainStepCommand;
        public IObservable<(MainStep, MainStep)> MainStepChanged { get; protected set; }

        public override void Initialize()
        {
            BindGameStepLoadSystem();
            InitializeChangeMainStepCommand();
            BindMainStepChangedObservable();
        }
        private void BindMainStepChangedObservable()
        {
            MainStepChanged = _changeMainStepCommand.AsSystemObservable();
        }
        private void InitializeChangeMainStepCommand()
        {
            _changeMainStepCommand = new();
        }
        private void BindGameStepLoadSystem()
        {
            if (!TryGetDomain<IGameStepDomain>(out var targetDomainSystem))
                return;

            _gameStepDomain = targetDomainSystem;
        }
        public override void PostInitialize()
        {
            SubscribeToGameFlowState();
        }
        private void SubscribeToGameFlowState()
        {
            if (!TryGetAppSystem<IGameFlowApplication>(out var gmaeFlowAppSystem))
                return;

            _gameFlowStateSubscription = gmaeFlowAppSystem.GameFlowStateChanged.Subscribe(HandleGameFlowStateChanged);
        }
        private void HandleGameFlowStateChanged(GameFlowState changedState)
        {
            // flow에 따라서 MainStep 변경 진행
            ChangeMainStepByGameFlow(changedState);
        }
        private void ChangeMainStepByGameFlow(GameFlowState changedState)
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
            _gameStepDomain = null;
        }
    }
}
