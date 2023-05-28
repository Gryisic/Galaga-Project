using System;
using Infrastructure.Interfaces;

namespace Common.Stages.Conditions
{
    public abstract class NextWaveCondition : ICondition
    {
        public abstract event Action Fulfilled;

        public abstract void StartChecking();

        public abstract void StopChecking();
    }
}