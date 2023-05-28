using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Background
{
    public class Parallax : MonoBehaviour, IDisposable
    {
        [SerializeField] private Sprite[] _templates;

        [SerializeField] private ParallaxGroup _parallaxGroup;
        [SerializeField] private ParallaxPart _currentPart;
        [SerializeField] private ParallaxPart _nextPart;

        private CancellationTokenSource _scrollTokenSource = new CancellationTokenSource();

        private float _timer;
        
        private float _scrollSpeed = 1f;

        private void Awake()
        {
            Sprite initialSprite = _templates[Random.Range(0, _templates.Length)];
            
            _currentPart.SetSprite(initialSprite);
        }

        public void Dispose()
        {
            _scrollTokenSource.Cancel();
            _scrollTokenSource.Dispose();
        }

        public void IncreaseSpeed(float multiplier) => _scrollSpeed += _scrollSpeed * multiplier;
        
        public void Scroll() => ScrollAsync().Forget();

        private void ShuffleParts()
        {
            _currentPart.SetSprite(_nextPart.Sprite);
            _nextPart.SetSprite(_templates[Random.Range(0, _templates.Length)]);
        }
        
        private float GetRemainingTime()
        {
            Texture2D texture = _currentPart.Sprite.texture;
            float textureUnitSize = texture.height / _currentPart.Sprite.pixelsPerUnit;
            
            return textureUnitSize / _scrollSpeed;
        }
        
        private async UniTask ScrollAsync()
        {
            float time = _timer;
            Transform groupTransform = _parallaxGroup.transform;
            
            while (_scrollTokenSource.IsCancellationRequested == false)
            {
                if (time <= 0)
                {
                    ShuffleParts();

                    groupTransform.position = Vector3.zero;
                    
                    time = GetRemainingTime();
                }
                
                groupTransform.Translate(Vector3.down * _scrollSpeed * Time.fixedDeltaTime);
                
                await UniTask.WaitForFixedUpdate(cancellationToken: _scrollTokenSource.Token);

                time -= Time.fixedDeltaTime;
            }

            _timer = time;
        }
    }
}