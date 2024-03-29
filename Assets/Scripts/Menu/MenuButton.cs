using UnityEngine;

namespace Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private AudioClip hoverSound;
        [SerializeField] private AudioClip clickSound;
        private AudioSource _as;

        private void Awake()
        {
            _as = this.GetComponent<AudioSource>();
        }

        public void HandleHover()
        {
            if (hoverSound != null) _as.PlayOneShot(hoverSound);
        }

        public void HandleClick()
        {
            if (clickSound != null) _as.PlayOneShot(clickSound);
        }
    }
}