using System;
using UnityEngine;
using Utils;

public class Brick : MonoBehaviour
{
    public int hitPoints;
    public int OriginalHitPoints { private set; get; }
    public ParticleSystem destroyAnimation;

    public static event Action<Brick> OnBrickDestruction;

    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = this.gameObject.GetComponent<SpriteRenderer>();
        this._sr.sprite = BrickManager.Instance.Sprites[this.hitPoints - 1];

        BrickManager.Instance.bricksAlive++;

        OriginalHitPoints = hitPoints;
    }

    /*
    // TODO: Remove, was for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && (GameManager.Instance.GameState == GameState.ReadyToPlay ||
                                            GameManager.Instance.GameState == GameState.GameRunning))
        {
            for (int i = 0; i < hitPoints; i++)
            {
                HandleReduceLife();
            }
        }
    }
    */

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
            BrickManager.Instance.bricksAlive--;
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
        mm.startColor = this._sr.color;
        Destroy(effect, destroyAnimation.main.startLifetime.constant);
    }
}