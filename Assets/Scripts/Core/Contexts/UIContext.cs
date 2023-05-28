using System.Linq;
using Common.UI;
using UnityEngine;

namespace Core.Contexts
{
    public class UIContext : MonoBehaviour
    {
        [SerializeField] private UIElement[] _uiElements;

        public T Resolve<T>() where T : UIElement => (T) _uiElements.First(element => element is T);
    }
}