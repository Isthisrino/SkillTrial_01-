using Elder.Core.Common.Interfaces;
using Elder.Core.GameStep.Domain;

namespace Elder.Core.GameStep.Interfaces
{
    public interface IGameStepDomain : IDomain
    {
        public MainStep CurrentMainStep { get; }
        public SubStep CurrentSubStep { get; }
        public bool TrySetMainStep(in MainStep newMainStep, out MainStep previousMainStep);
        public void AddSubStep(in SubStep newSubStep);
        public bool TryRemoveSubStep(out SubStep poppedSubStep);
    }
}
