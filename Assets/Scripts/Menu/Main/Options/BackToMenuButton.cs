using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main.Options
{
    public class BackToMenuButton : MonoBehaviour
    {
        private Button _backToMenuButton;

        private void Awake()
        {
            _backToMenuButton = this.GetComponent<Button>();
            _backToMenuButton.onClick.AddListener(() =>
            {
                MenuManager.Instance.SwitchToMainMenu();
            });
        }
    }
}