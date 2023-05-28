using System;

namespace Infrastructure.Interfaces
{
    public interface ICondition
    {
        event Action Fulfilled;
    }
}