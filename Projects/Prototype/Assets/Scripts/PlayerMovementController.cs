using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float overallSpeed;
    public float forwardVelocity;
    public float backwardVelocity;
    public float strafeVelocity;

    public GameObject cameraPivot;

    public GameObject animatedObject;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float JumpHeight = 1;

    private float rotationY = 0F;

    private float jumpStarted = -1;
    private float playerHeight = 0;

    private Animator avatarAnimator;

    private const float GRAVITATIONAL_ACCELERATION = 9.82f;

    private float _InitialVelocity;
    private float InitialVelocity
    {
        get
        {
            if (_InitialVelocity == 0)
            {
                _InitialVelocity = Mathf.Sqrt(2 * JumpHeight * GRAVITATIONAL_ACCELERATION);
            }
            return _InitialVelocity;
        }
    }

    private float _JumpLength;
    public float JumpLength
    {
        get
        {
            if (_JumpLength == 0)
            {
                _JumpLength = (2 * InitialVelocity) / GRAVITATIONAL_ACCELERATION;
            }
            return _JumpLength;
        }
    }

    void Start()
    {
        avatarAnimator = (Animator) animatedObject.GetComponent("Animator");
    }

    void Update()
    {
        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float rotateX = Input.GetAxis("Mouse X");
        float rotateY = Input.GetAxis("Mouse Y");

        bool jumpCommand = Input.GetKeyDown("space");

        // mouse look x
        transform.Rotate(0, rotateX * sensitivityX, 0);

        // mouse look y
        rotationY += rotateY * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        cameraPivot.transform.localEulerAngles = new Vector3(-rotationY, cameraPivot.transform.localEulerAngles.y, 0);

        // figure movement
        CharacterController controller = GetComponent<CharacterController>();
        if (moveVertical > 0)
        {
            moveVertical *= forwardVelocity;
        }
        else
        {
            moveVertical *= backwardVelocity;
        }

        if (jumpCommand)
        {
            if (controller.isGrounded)
            {
                jumpStarted = Time.time;
            }
            else
            {

            }
        }

        float yDelta = 0;
        if (Time.time > jumpStarted + JumpLength)
        {
            jumpStarted = -1; // jump ended
            playerHeight = 0;
        }
        else
        {
            float jumpTime = Time.time - jumpStarted;
            yDelta = playerHeight;
            playerHeight = Mathf.Max(0, (InitialVelocity * jumpTime) - (GRAVITATIONAL_ACCELERATION * Mathf.Pow(jumpTime, 2)) / 2);
            yDelta = playerHeight - yDelta;
        }

        Vector3 movement;

        if(yDelta != 0) {
            movement = new Vector3(0, yDelta * 100, 0);
            controller.Move(transform.rotation * movement * Time.deltaTime);
        } else {
            movement = new Vector3(moveHorizontal * strafeVelocity, 0, moveVertical);
            controller.SimpleMove(transform.rotation * movement * overallSpeed * Time.deltaTime);
        }

        // set animations
        avatarAnimator.SetBool("isRunningForward", moveVertical > 0);
        avatarAnimator.SetBool("isWalkingBackward", moveVertical < 0);
        avatarAnimator.SetBool("strafingLeft", moveHorizontal < 0);
        avatarAnimator.SetBool("strafingRight", moveHorizontal > 0);
        avatarAnimator.SetBool("rotatingLeft", rotateX < 0);
        avatarAnimator.SetBool("rotatingRight", rotateX > 0);
    }
}
