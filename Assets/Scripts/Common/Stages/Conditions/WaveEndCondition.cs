using System;
using Infrastructure.Interfaces;

namespace Common.Stages.Conditions
{
    public abstract class WaveEndCondition : ICondition
    {
        public abstract event Action Fulfilled;
    }
}