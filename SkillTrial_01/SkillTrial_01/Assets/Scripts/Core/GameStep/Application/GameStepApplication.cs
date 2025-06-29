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
            UpdateMainStepFromGameFlowState(changedState);
        }
        private void UpdateMainStepFromGameFlowState(GameFlowState changedState)
        {
            var targetMainStep = ConvertGameFlowStateToMainStep(changedState);
            if (!_gameStepDomain.TrySetMainStep(targetMainStep, out var prevMainStep))
                return;

            _changeMainStepCommand.Execute((prevMainStep, targetMainStep));
        }
        private MainStep ConvertGameFlowStateToMainStep(GameFlowState changedState)
        {
            return new MainStep(GameFlowStateToMainStepState(changedState));
        }
        private MainStepState GameFlowStateToMainStepState(GameFlowState changedState)
        {
            return changedState switch
            {
                GameFlowState.Boot => MainStepState.Boot,
                GameFlowState.Splash => MainStepState.Splash,
                GameFlowState.Title => MainStepState.Title,
                GameFlowState.GamePlay => MainStepState.GamePlay,
                _ => MainStepState.None,
            };
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
