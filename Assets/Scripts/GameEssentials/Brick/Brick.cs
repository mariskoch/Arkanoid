using System;
using Managers;
using UnityEngine;

namespace GameEssentials.Brick
{
    public class Brick : MonoBehaviour
    {
        public int hitPoints;
        public int OriginalHitPoints { private set; get; }
        public ParticleSystem destroyAnimation;

        public static event Action<Brick> OnBrickDestruction;

        [SerializeField] private ParticleSystem.MinMaxGradient animationColor;
        [SerializeField] private bool isUnbreakable = false;
        [SerializeField] private AudioClip audioClip;
        private SpriteRenderer _sr;
        private BoxCollider2D _bc;

        private void Start()
        {
            _bc = this.GetComponent<BoxCollider2D>();
            _sr = this.gameObject.GetComponent<SpriteRenderer>();
            if (isUnbreakable) return;
            this._sr.sprite = BrickManager.Instance.Sprites[this.hitPoints - 1];

            BrickManager.Instance.aliveBrickIDs.Add(GetInstanceID());

            OriginalHitPoints = hitPoints;
        }

        private void Update()
        {
            if (!isUnbreakable) return;
            _bc.size = _sr.size;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Ball")) return;
            BrickManager.Instance.PlaySound(audioClip);
            if (!isUnbreakable)
            {
                HandleReduceLife();
            }
        }

        private void HandleReduceLife()
        {
            hitPoints--;

            if (hitPoints <= 0)
            {
                OnBrickDestruction?.Invoke(this);
                SpawnDestroyEffect();
                BrickManager.Instance.aliveBrickIDs.Remove(GetInstanceID());
                Destroy(this.gameObject);
            }
            else
            {
                this._sr.sprite = BrickManager.Instance.Sprites[this.hitPoints - 1];
            }
        }

        private void SpawnDestroyEffect()
        {
            Vector3 brickPos = this.transform.position;
            GameObject effect = Instantiate(destroyAnimation.gameObject, brickPos, Quaternion.identity);

            ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
            mm.startColor = animationColor;
            Destroy(effect, destroyAnimation.main.startLifetime.constant);
        }
    }
}