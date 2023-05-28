using System;

namespace Infrastructure.Interfaces
{
    public interface IItemsPool<T> : IDisposable
    {
        T Get();
        void Return(T item);
    }
}