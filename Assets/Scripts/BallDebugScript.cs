using System;
using UnityEngine;

public class BallDebugScript : MonoBehaviour
{
    [SerializeField] private Vector2 initSpeed = Vector2.zero;
    // [SerializeField] private float ballSpeed;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        Time.timeScale = 0.05f;
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        _rb.velocity = initSpeed.normalized * BallManager.Instance.BallSpeed;
    }

    private void Update()
    {
        Debug.Log(_rb.velocity);
    }
}