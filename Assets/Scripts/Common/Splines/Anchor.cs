using UnityEngine;

namespace Common.Splines
{
    public class Anchor : MonoBehaviour
    {
        [SerializeField] private Handle _firstHandle;
        [SerializeField] private Handle _secondHandle;
        
        public Vector2 Position => transform.position;
        
        public Vector2 FirstHandlePosition => _firstHandle.Position;
        public Vector2 SecondHandlePosition => _secondHandle.Position;
        
#if UNITY_EDITOR
        [SerializeField] private bool _drawGizmos = true;
        private void OnDrawGizmos()
        {
            if (_drawGizmos == false)
                return;
            
            Gizmos.color = Color.yellow;

            var position = transform.position;
            Gizmos.DrawLine(_firstHandle.Position, position);
            Gizmos.DrawLine(_secondHandle.Position, position);
        }
#endif
    }
}