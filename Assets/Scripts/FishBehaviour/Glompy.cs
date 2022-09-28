using System.Collections;
using UnityEngine;

public class Glompy : FishBase
{
    public Fish Fish;

    SpriteRenderer fishSprite;

    private void Start()
    {
        fishSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
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
        }

        if (FishState == FishState.Idle)
        {
            StartCoroutine(swimming());
        }
    }

    IEnumerator swimming()
    {
        FishState = FishState.Waiting;
        if (Random.Range(0, 2) == 0) // if fish should swim
        {
            Debug.Log("I will swimm");
            FishState = FishState.Swimming;

            if (Random.Range(0, 2) == 0) // 0 = right || 1 = left
            {
                Debug.Log("I will go right");
                transform.eulerAngles = new Vector3(0, 180, 0);
                fishSprite.flipX = true;
                if (Random.Range(0, 3) == 0) // if fish should rotate
                {
                    Debug.Log("I will rotate");
                    transform.eulerAngles = new Vector3(0, 180, Random.Range(45, -45));
                }
            }
            else
            {
                Debug.Log("I will go left");
                transform.eulerAngles = new Vector3(0,0,0);
                fishSprite.flipX = false;
                if (Random.Range(0, 5) == 0) // if fish should rotate
                {
                    Debug.Log("I will rotate");
                    transform.eulerAngles = new Vector3(0, 0, Random.Range(45,-45));
                }
            }
            yield return new WaitForSeconds(Random.Range(1, 3)); // how long fish should swim
            transform.eulerAngles = Vector3.zero;
            FishState = FishState.Idle;
        }
        else
        {
            Debug.Log("I will not swim");
            yield return new WaitForSeconds(3f); // attems before trying another swimming
            FishState = FishState.Idle;
        }
    }

    void checkDistace()
    {
        if (transform.position.x > SpawnPosition.x + 3 || transform.position.x < SpawnPosition.x - 3 ||
           transform.position.y > SpawnPosition.y + 3 || transform.position.y < SpawnPosition.y - 3)
        {
            if (FishState != FishState.Swimming) return;
            //Debug.Log("Im out of bounce");
            StopCoroutine("swimming");
            //transform.eulerAngles = Vector3.zero;
        }
    }

    void chaseBait()
    {

    }
}
