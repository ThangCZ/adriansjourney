using UnityEngine;
using System.Collections;

public class PickupAnimator : MonoBehaviour
{

    public float rotationSpeed = 15;
    public float upDownSpeed = 2;
    public float verticalDelta = 0.4f;

    private float upDownRotation = 0;
    private float initialYPosition;  

    void Start() {
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        upDownRotation += upDownSpeed * Time.deltaTime;
        if(upDownRotation > 360) {
            upDownRotation -= 360;
        }
        transform.position = new Vector3(
            transform.position.x,
            initialYPosition + (verticalDelta * Mathf.Sin(upDownRotation)),
            transform.position.z);
    }
}
