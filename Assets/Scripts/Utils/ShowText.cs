using TMPro;
using UnityEngine;

namespace GameOverScreen
{
    public class ShowText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.enabled = false;
        }

        public void Show()
        {
            _text.enabled = true;
        }

        public void Hide()
        {
            _text.enabled = false;
        }
    }
}