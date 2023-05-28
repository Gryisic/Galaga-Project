using UnityEngine;

namespace Common.PrefabsRoots
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultPosition;
        
        public Transform Transform => transform;
        public Vector2 DefaultPosition => _defaultPosition;
    }
}