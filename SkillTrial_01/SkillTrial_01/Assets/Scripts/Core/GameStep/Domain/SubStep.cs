namespace Elder.Core.GameStep.Domain
{
    public readonly struct SubStep
    {
        public readonly string SubStepKey;

        public SubStep(string subStepKey)
        {
            SubStepKey = subStepKey;
        }
    }
}
