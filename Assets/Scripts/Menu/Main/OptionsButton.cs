using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class OptionsButton : MonoBehaviour
    {
        private Button _optionsButton;

        private void Awake()
        {
            _optionsButton = this.GetComponent<Button>();
            _optionsButton.onClick.AddListener(() =>
            {
                MenuManager.Instance.SwitchToOptions();
            });
        }
    }
}