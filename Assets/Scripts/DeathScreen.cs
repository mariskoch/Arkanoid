using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Ball"))
        {
            Ball ball = coll.GetComponent<Ball>();
            BallManager.Instance.Balls.Remove(ball);
            ball.Die();
        }
    }
}
