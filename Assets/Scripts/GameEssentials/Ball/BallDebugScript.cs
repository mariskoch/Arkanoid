using UnityEngine;

public class BallDebugScript : MonoBehaviour
{
    [SerializeField] private Vector2 targetPoint = Vector2.zero;

    private Rigidbody2D _rb;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        Vector3 pos = this.transform.position;
        _rb.velocity = new Vector2(targetPoint.x - pos.x, targetPoint.y - pos.y).normalized * BallManager.Instance.BallSpeed;
    }
}