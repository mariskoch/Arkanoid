using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class Brick : MonoBehaviour
{
    public int hitPoints;
    public int OriginalHitPoints { private set; get; }
    public ParticleSystem destroyAnimation;

    public static event Action<Brick> OnBrickDestruction;

    [SerializeField] private ParticleSystem.MinMaxGradient animationColor;
    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = this.gameObject.GetComponent<SpriteRenderer>();
        this._sr.sprite = BrickManager.Instance.Sprites[this.hitPoints - 1];

        BrickManager.Instance.aliveBrickIDs.Add(GetInstanceID());

        OriginalHitPoints = hitPoints;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
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