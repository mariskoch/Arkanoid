using System;
using UnityEngine;

namespace GameEssentials.Border
{
    public class Border : MonoBehaviour
    {
        private AudioSource _ac;

        private void Awake()
        {
            _ac = this.GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (!coll.gameObject.CompareTag("Ball")) return;
            if (_ac.clip != null)
            {
                _ac.PlayOneShot(_ac.clip);
            }
        }
    }
}