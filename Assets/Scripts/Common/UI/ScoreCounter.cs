using TMPro;
using UnityEngine;

namespace Common.UI
{
    public class ScoreCounter : UIElement
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public override void Activate()
        {
            _counter.text = "0";

            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void UpdateCounter(int value) => _counter.text = value.ToString();
    }
}