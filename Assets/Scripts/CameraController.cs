using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform baitTransform;
    [SerializeField] RodController rodController;

    [SerializeField] float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        if(baitTransform.position.x > 0 && rodController.RodState == RodState.Casted)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, baitTransform.position, smoothSpeed);
            transform.position = new Vector3(smoothPosition.x,transform.position.y,-10);
        }
        if(baitTransform.position.y < 10 && rodController.RodState == RodState.Casted)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, baitTransform.position,smoothSpeed);
            transform.position = new Vector3(transform.position.x, smoothPosition.y, -10);
        }
    }
}
