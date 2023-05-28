using Common.PrefabsRoots;
using Infrastructure.Interfaces;

namespace Common.Generators
{
    public abstract class UnityItemGenerator<T> : ItemGenerator<T> where T: IUnityItem
    {
        protected IFactory<T> factory;
        
        private readonly Root _root;

        protected UnityItemGenerator(Root root)
        {
            _root = root;
        }

        protected override void CreateItem()
        {
            factory.Create(_root.DefaultPosition, _root.Transform, out T item);
            item.Hide();

            items.Enqueue(item);
        }
    }
}