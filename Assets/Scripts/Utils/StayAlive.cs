using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class StayAlive : MonoBehaviour
    {
        #region Singleton

        private static StayAlive _instance;

        public static StayAlive Instance => _instance;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion
    }
}