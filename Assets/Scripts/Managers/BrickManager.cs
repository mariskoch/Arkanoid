using System;
using System.Collections.Generic;
using Managers;
using TimerUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;

namespace Managers
{
    public class BrickManager : MonoBehaviour
    {
        #region Singleton

        private static BrickManager _instance;

        public static BrickManager Instance => _instance;

        private void Awake()
        {
            _as = this.GetComponent<AudioSource>();
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

        public Sprite[] Sprites;
        public GameObject LevelPassedCanvasPrefab;
        public static event Action OnLevelPassed;
        private GameObject _levelPassedInstance;
        private AudioSource _as;

        [SerializeField] private AudioClip levelPassedSound;

        [HideInInspector] public List<int> aliveBrickIDs = new List<int>();

        private void Update()
        {
            if (aliveBrickIDs.Count <= 0 && GameManager.Instance.GameState == GameState.GameRunning)
            {
                GameManager.Instance.GameState = GameState.Win;
                OnLevelPassed?.Invoke();
                BallManager.Instance.FreezeBalls();
                if (levelPassedSound != null) PlaySound(levelPassedSound);
                if (_levelPassedInstance == null)
                {
                    _levelPassedInstance = Instantiate(LevelPassedCanvasPrefab);
                    if (GameManager.AreLivesAndScoreCounted()) UIManager.Instance.SaveScore();
                    Timer.Instance.StopTimer();
                }
            }
        }

        public void PlaySound(AudioClip clip)
        {
            _as.PlayOneShot(clip);
        }
    }
}