using Elder.Core.Common.BaseClasses;
using Elder.Core.GameStep.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Elder.Core.GameStep.Domain
{
    public class GameStepSystem : DomainSystemBase, IGameStepSystem
    {
        private MainStep _currrentMainStep;
        private MainStep _previousMainStep;

        // 이거 수정 
        private Stack<SubStep> _subSteps;

        public MainStep CurrentMainStep => _currrentMainStep;
        public SubStep CurrentSubStep => _subSteps.Any() ? _subSteps.Last() : default;

        public override void Initialize()
        {
            InitializeSubStepsStack();
        }
        private void InitializeSubStepsStack()
        {
            _subSteps = new();
        }
        public override void PostInitialize()
        {

        }
        public bool TrySetMainStep(in MainStep newMainStep, out MainStep previousMainStep)
        {
            previousMainStep = _previousMainStep;
            if (_currrentMainStep.StepState == newMainStep.StepState)
                return false;

            _currrentMainStep = newMainStep;
            return true;
        }
        public void AddSubStep(in SubStep newSubStep)
        {
            _subSteps.Push(newSubStep);
        }
        public bool TryRemoveSubStep(out SubStep poppedSubStep)
        {
            if (!_subSteps.Any())
            {
                poppedSubStep = default;
                return false;
            }
            poppedSubStep = _subSteps.Pop();
            return true;
        }
        protected override void DisposeManagedResources()
        {
            DisposeSubStepsStack();

            base.DisposeManagedResources();
        }
        private void DisposeSubStepsStack()
        {
            _subSteps.Clear();
            _subSteps = null;
        }
    }
}
