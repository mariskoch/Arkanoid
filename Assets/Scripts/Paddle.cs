using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Paddle : MonoBehaviour
{
    public float speed = 0.2f;
    private Rigidbody2D rb;
    private Vector3 movement = Vector3.zero;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = new Vector3(1 * speed, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement = new Vector3(-1 * speed, 0, 0);
        } else if (Input.GetKeyUp(KeyCode.RightArrow) && movement.x > 0)
        {
            movement = Vector3.zero;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow) && movement.x < 0)
        {
            movement = Vector3.zero;
        }
        rb.velocity = movement;
    }
}
