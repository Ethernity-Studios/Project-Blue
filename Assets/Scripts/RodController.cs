using UnityEngine;

public enum RodState
{
    Idle, Charging, Casting, Casted
}

public class RodController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] DistanceJoint2D distanceJoint2D;

    [SerializeField] Transform rodTop;
    [SerializeField] GameObject bait;
    [SerializeField] Rigidbody2D baitRb;

    [SerializeField] BaitController baitController;

    public float CastProgress = 0;
    public RodState RodState;
    [SerializeField] bool reeling;

    FishingRod equpiedRod;

    private void Start()
    {
        equpiedRod = GameManager.Instance.EqupiedRod;
    }

    void Update()
    {
        updateLine();
        transform.localEulerAngles = new Vector3(0, 0, CastProgress * 10);

        if (RodState == RodState.Idle && Input.GetMouseButtonDown(0))
        {
            RodState = RodState.Charging;
        }
        if (Input.GetMouseButton(0))
        {

            if (CastProgress < 15f && RodState == RodState.Charging)
            {
                distanceJoint2D.distance = .1f;
                CastProgress += 1.5f * Time.deltaTime * equpiedRod.CastSpeed;
            }
            else if (CastProgress >= 0f && RodState == RodState.Casting)
            {
                CastProgress -= 1.5f * Time.deltaTime * equpiedRod.CastSpeed * 1.5f;
            }
        }

        if(RodState == RodState.Idle || RodState == RodState.Casted)
        {
            if(CastProgress > 0f)
            CastProgress -= 1.5f * Time.deltaTime * equpiedRod.CastSpeed * 2f;
        }


        if (CastProgress >= 15f)
        {
            RodState = RodState.Casting;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (RodState == RodState.Charging)
            {
                RodState = RodState.Idle;
                distanceJoint2D.distance = .5f;
            }
            if (RodState == RodState.Casting)
            {
                if(CastProgress > 1f)
                {
                    RodState = RodState.Casted;
                    cast();
                }
                else RodState = RodState.Idle;
            }
        }

        if(baitController.WasInWater && RodState == RodState.Casted)
        {
            if (Input.GetMouseButton(0))
            {
                float distance = Vector2.Distance(rodTop.position, bait.transform.position);
                if (distance <= .5f)
                {
                    bait.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    RodState = RodState.Idle;
                    baitController.WasInWater = false;
                    reeling = false;
                }
                else
                {
                    reeling = true;
                    distanceJoint2D.distance = Vector2.Distance(bait.transform.position, rodTop.position);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                reeling = false;
                baitRb.velocity = new Vector2(0,-2f);
            }
        }
    }

    public void FixedUpdate()
    {
        if (reeling)
        {
            bait.transform.position = Vector2.MoveTowards(bait.transform.position, rodTop.position, equpiedRod.ReelSpeed * Time.fixedDeltaTime);
            baitRb.velocity = baitRb.velocity / 2;
        }
    }

    void cast()
    {
        bait.GetComponent<DistanceJoint2D>().distance = 100;
        bait.GetComponent<Rigidbody2D>().AddForce(new Vector2(CastProgress * equpiedRod.XForce, CastProgress * equpiedRod.YForce));

    }

    void updateLine()
    {
        lineRenderer.SetPosition(0, rodTop.position);
        lineRenderer.SetPosition(1, bait.transform.position);
    }
}
