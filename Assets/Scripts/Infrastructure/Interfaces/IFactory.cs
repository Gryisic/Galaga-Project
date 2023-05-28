using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IFactory<T>
    {
        void Load();
        void Create(Vector2 at, Transform parent, out T item);
    }
}