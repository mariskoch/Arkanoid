using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Paddle : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector3 movement = Vector3.zero;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        bc = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        AdaptBoxColliderToSpriteSize();
        
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

    private void AdaptBoxColliderToSpriteSize()
    {
        if (!bc.size.x.Equals(sr.size.x))
        {
            Vector2 size = sr.size;
            bc.size = new Vector2(size.x, size.y);
        }
    }
}
