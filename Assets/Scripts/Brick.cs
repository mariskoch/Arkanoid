using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoints;
    public ParticleSystem destroyAnimation;
    
    public static event Action<Brick> OnBrickDestruction;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        this.sr.sprite = BrickManager.Instance.Sprites[this.hitPoints -1];
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
        OnBrickDestruction?.Invoke(this);
        
        if (hitPoints <= 0)
        {
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite = BrickManager.Instance.Sprites[this.hitPoints -1];
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = this.transform.position;
        GameObject effect = Instantiate(destroyAnimation.gameObject, brickPos, Quaternion.identity);
        
        ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, destroyAnimation.main.startLifetime.constant);
    }
}
