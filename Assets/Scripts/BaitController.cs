using UnityEngine;

public enum BaitSize
{
    none, small, medium, large
}

public class BaitController : MonoBehaviour
{
    public bool IsInWater;
    public bool WasInWater;

    Rigidbody2D rb;
    DistanceJoint2D dj;

    [SerializeField] Transform rodTop;

    RodController rodController;

    public BaitSize BaitSize;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dj = GetComponent<DistanceJoint2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            dj.distance = Vector2.Distance(transform.position, rodTop.position);
            rb.gravityScale = .1f;
            rb.velocity = rb.velocity / 2f;

            IsInWater = true;
            WasInWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            IsInWater = false;
            rb.gravityScale = 1;
        }
    }

    private void Update()
    {
        if (!IsInWater) return;
        if(transform.position.x <= -1.5f)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            //transform.position = new Vector2(-1.7f,transform.position.y);
        }
    }
}
