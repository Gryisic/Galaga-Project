using System;
using Cysharp.Threading.Tasks;

namespace Common.UI
{
    public class SceneTransition : UIElement
    {
        public override void Activate() { }

        public override void Deactivate() { }
        
        public async UniTask ActivateAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0f));
        }

        public async UniTask DeactivateAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0f));
        }
    }
}