using System;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface INavigationStrategy : IDisposable
    {
        event Action NavigationCompleted; 
        
        Vector2 InitialPosition { get; }

        void StartMoving();

        void StopMoving();
    }
}