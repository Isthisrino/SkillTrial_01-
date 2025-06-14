using Elder.Core.Common.Enums;

namespace Elder.Core.GameStep.Domain
{
    public readonly struct MainStep
    {
        public readonly MainStepState StepState;

        public MainStep(MainStepState stepState)
        {
            StepState = stepState;
        }
    }
}
