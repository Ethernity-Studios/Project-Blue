using System.Collections;
using UnityEngine;

public class Glompy : FishBase
{
    public Fish Fish;

    SpriteRenderer fishSprite;
    Vector3 targetRotation;

    [SerializeField] int side;

    private void Start()
    {
        fishSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        swim();
        chaseBait();
    }


    void swim()
    {
        if (FishState == FishState.Chasing || FishState == FishState.Caught) return;
        //checkDistace();

        if (FishState == FishState.Swimming)
        {
            transform.position += transform.right * Time.deltaTime * Fish.Speed;
            if (side == 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 180, targetRotation.z)), 0.25f * Time.deltaTime);
            else transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, targetRotation.z)), 0.25f * Time.deltaTime);

        }

        if (FishState == FishState.Idle)
        {
            StartCoroutine(swimming());
        }
    }

    IEnumerator swimming()
    {
        FishState = FishState.Waiting;
        yield return new WaitForSeconds(.5f);
        if (Random.Range(0, 2) == 0) // if fish should swim
        {
            FishState = FishState.Swimming;
            side = Random.Range(0, 2); // 0 = left || 1 = right
            if (side == 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin: transform.position, direction: Vector2.left, distance: 4f, layerMask: terrainMask); // check if there is something on left
                if (hit.collider != null) side = 1;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(origin: transform.position, direction: Vector2.right, distance: 4f, layerMask: terrainMask); // check if there is something on right
                if (hit.collider != null) side = 0;
            }

            if (side == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                fishSprite.flipX = true;
                if (Random.Range(0, 2) == 0) // if fish should rotate
                {
                    checkCollisionRotation();
                }
                else targetRotation = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                fishSprite.flipX = true;
                if (Random.Range(0, 2) == 0) // if fish should rotate
                {
                    checkCollisionRotation();
                }
                else targetRotation = Vector3.zero;
            }
            yield return new WaitForSeconds(Random.Range(1, 3)); // how long fish should swim
            targetRotation = Vector3.zero;
            FishState = FishState.Idle;
        }
        else
        {
            yield return new WaitForSeconds(3f); // attems before trying another swimming
            targetRotation = Vector3.zero;
            FishState = FishState.Idle;
        }
    }

    void checkCollisionRotation()
    {
        int max = 45;
        int min = -45;
        RaycastHit2D hitSky = Physics2D.Raycast(origin: transform.position, direction: Vector2.up, distance: 1f, layerMask: skyMask);
        if (hitSky.collider != null) 
        {
            max = -20;
            min = -45;
        } 

        RaycastHit2D hitUp = Physics2D.Raycast(origin: transform.position, direction: Vector2.up, distance: 1f, layerMask: terrainMask);
        if (hitUp.collider != null)
        {
            max = -20;
            min = -45;
        }
        RaycastHit2D hitDown = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: 1f, layerMask: terrainMask);
        if (hitDown.collider != null)
        {
            max = 45;
            min = 20;
        }

        targetRotation.z = Random.Range(min,max);
    }

    void checkDistace()
    {
        if (transform.position.x > SpawnPosition.x + 3 || transform.position.x < SpawnPosition.x - 3 ||
           transform.position.y > SpawnPosition.y + 3 || transform.position.y < SpawnPosition.y - 3)
        {
            if (FishState != FishState.Swimming) return;
            StopCoroutine("swimming");
        }
    }

    void chaseBait()
    {

    }



}
