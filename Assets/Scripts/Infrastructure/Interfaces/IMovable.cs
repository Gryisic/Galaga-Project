using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IMovable
    {
        Transform Transform { get; }
        float Speed { get; }

        void SetPosition(Vector2 position);
        
        void StartMoving(Vector2 direction);

        void StopMoving();
    }
}