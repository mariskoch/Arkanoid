using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class Flashing : MonoBehaviour
    {
        [SerializeField] private float flashingInterval = 1.0f;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = this.GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            StartCoroutine(BlinkRoutine());
        }

        private IEnumerator BlinkRoutine()
        {
            while (true)
            {
                _text.enabled = !_text.enabled;
                yield return new WaitForSeconds(flashingInterval);
            }
        }
    }
}