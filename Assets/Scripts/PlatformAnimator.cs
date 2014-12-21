using UnityEngine;
using System.Collections;

public class PlatformAnimator : MonoBehaviour {

    public Vector3 MovementDelta;
    public float AnimationLength;

    private float currentTime;
    private bool goingForward = true;

    private Vector3 originalPosition;

    private GameObject playerOnTheTop;

	void Start () {
        originalPosition = transform.position;
        MovementDelta = transform.rotation * MovementDelta;
	}
	
	void Update () {
        currentTime += Time.deltaTime;
        if(currentTime > AnimationLength) {
            currentTime = AnimationLength;
        }
        Vector3 newPosition = originalPosition + (MovementDelta * interpolate(currentTime / AnimationLength) * (goingForward ? 1 : -1));;
        if(playerOnTheTop != null) {
            Vector3 movementDelta = newPosition - transform.position;
            playerOnTheTop.transform.position += movementDelta;
        }
        transform.position = newPosition;
        if(currentTime == AnimationLength) {
            currentTime = 0;
            goingForward = !goingForward;
            originalPosition = transform.position;
        }
	}

    private float interpolate(float progress) {
        return 1 - ((Mathf.Cos(progress * Mathf.PI) + 1) / 2);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Player")) {
            playerOnTheTop = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            playerOnTheTop = null;
        }
    }
}
