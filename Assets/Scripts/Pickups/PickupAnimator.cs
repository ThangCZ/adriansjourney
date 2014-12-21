using UnityEngine;
using System.Collections;

public class PickupAnimator : MonoBehaviour
{
    public float rotationSpeedX = 15;
    public float rotationSpeedY = 0;
    public float rotationSpeedZ = 0;
    public float upDownSpeed = 2;
    public float verticalDelta = 0.4f;

    private float upDownRotation = 0;
    private float lastVerticalDeviation;

    void Update()
    {
        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);

        upDownRotation += upDownSpeed * Time.deltaTime;
        if(upDownRotation > 360) {
            upDownRotation -= 360;
        }
        float newVerticalDeviation = verticalDelta * Mathf.Sin(upDownRotation);
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + (newVerticalDeviation - lastVerticalDeviation),
            transform.position.z);
        lastVerticalDeviation = newVerticalDeviation;
    }
}
