using UnityEngine;

public class Test : MonoBehaviour
{

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * 2;
    }
}
