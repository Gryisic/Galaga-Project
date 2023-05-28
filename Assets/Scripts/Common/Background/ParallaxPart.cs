using UnityEngine;

namespace Common.Background
{
    public class ParallaxPart : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        
        public Transform Transform => transform;
        public Sprite Sprite => _renderer.sprite;

        private void Awake()
        {
            if (_renderer == null)
                _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    }
}