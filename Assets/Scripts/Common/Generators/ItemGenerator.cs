using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Common.Generators
{
    public abstract class ItemGenerator<T> : IItemsPool<T>
    {
        protected Queue<T> items = new Queue<T>();

        public virtual void Dispose()
        {
            foreach (var item in items)
            {
                if (item is IDisposable disposable)
                    disposable.Dispose();
                else
                    break;
            }
            
            items.Clear();
            items = null;
        }

        public void CreateItems(int amount)
        {
            for (int i = 0; i <= amount; i++)
                CreateItem();
        }

        public T Get()
        {
            if (items.Count - 1 <= 0)
                CreateItem();

            return ConfiguredItem(items.Dequeue());
        }

        public void Return(T item) => items.Enqueue(item);

        protected abstract void CreateItem();

        protected abstract T ConfiguredItem(T item);
    }
}