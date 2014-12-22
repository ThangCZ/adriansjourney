using UnityEngine;
using System.Collections;

public class CinematCameraController : MonoBehaviour {

    private bool start = true;
    private bool enabled;

    private float maxRotationSpeed = -5;
    private float rotationStartEndLength = 1;

    private float milestone;

	void Update () {
	    if(Input.GetKeyDown(KeyCode.J)) {
            enabled = !enabled;
            milestone = Time.time;
            start = false;
        }

        float milestoneLength = Time.time - milestone;
        if (!start && (enabled || milestoneLength < rotationStartEndLength)) {
            if (milestoneLength < rotationStartEndLength) {
                transform.Rotate(Time.deltaTime * interpolate(milestoneLength / rotationStartEndLength) * maxRotationSpeed, 0, 0, Space.Self);
            } else {
                transform.Rotate(Time.deltaTime * maxRotationSpeed, 0, 0, Space.Self);
            }
        }
	}

    private float interpolate(float progress) {
        if (enabled) {
            return 1 - Mathf.Cos(progress * (Mathf.PI / 2));
        } else {
            return 1 - Mathf.Cos((1 - progress) * (Mathf.PI / 2));
        }
    }
}
