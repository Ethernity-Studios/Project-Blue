using UnityEngine;

public class BaitController : MonoBehaviour
{
    public bool IsInWater;
    public bool WasInWater;

    Rigidbody2D rb;
    DistanceJoint2D dj;

    [SerializeField] Transform rodTop;

    RodController rodController;

    public Bait Bait;
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

            rb.AddTorque(.2f,ForceMode2D.Force);

            IsInWater = true;
            WasInWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            rb.angularVelocity = 0;
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
