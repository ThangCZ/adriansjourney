using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour {
    public float runVelocity;
    public float walkVelocity;
    public float strafeVelocity;
    public float jumpHeight;
    public float jumpElevationAngle;

    // percentual decay of horizontal velocity per second - as fraction of 1
    // used to simulate air resistance for horizontal speed during jumping and falling
    public float horizontalVelocityDeceleration;

    public float gravitationalAccelerationMultiplier;

    public GameObject cameraPivot;
    public GameObject animatedObject;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    private float rotationY = 0F;

    private Vector3 lastVelocity = Vector3.zero;

    private bool isJumping = false;
    private float jumpStartTime;

    private float jumpInitialVelocity;
    private float elevationAngleSin;
    private float elevationAngleCos;

    private bool isFalling = false;

    private Animator avatarAnimator;
    private PlayerWeaponsHolder playerWeaponsHolder;
    private CharacterController controller;

	public GameObject arrowPrefab;

	public bool isHoldingBox = false;

    void Start() {
        avatarAnimator = animatedObject.GetComponent<Animator>();
        playerWeaponsHolder = GetComponent<PlayerWeaponsHolder>();
        controller = GetComponent<CharacterController>();

        elevationAngleSin = Mathf.Sin(jumpElevationAngle);
        elevationAngleCos = Mathf.Cos(jumpElevationAngle);
        jumpInitialVelocity = Mathf.Sqrt((2 * jumpHeight * -Physics.gravity.y * gravitationalAccelerationMultiplier) / Mathf.Pow(elevationAngleSin, 2));

        Screen.showCursor = false;
    }

    private float normalizeInput(float inputMeasurement) {
        if (inputMeasurement > 0) {
            return 1;
        } else if (inputMeasurement < 0) {
            return -1;
        } else {
            return 0;
        }
    }

    private Vector3 getHorizontalVelocity() {
        // get controls values
        float moveHorizontal = normalizeInput(Input.GetAxis("Horizontal"));
        float moveVertical = normalizeInput(Input.GetAxis("Vertical"));

        bool walkToggled = Input.GetButton("Walk");

        // figure movement
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement.Normalize();

        float frontBackQuotient = Mathf.Abs(movement.z);
        float strafingQuotient = Mathf.Abs(movement.x);
        if (strafingQuotient > 0) {
            movement *= strafingQuotient * strafeVelocity;
        } else {
            if (movement.z < 0 || walkToggled) { // going backwards
                movement *= frontBackQuotient * walkVelocity;
            } else {
                movement *= frontBackQuotient * runVelocity;
            }
        }
		
        movement = transform.TransformDirection(movement);
        return movement;
    }

	private Vector3 getHorizontalVelocityBox() {
		// get controls values
		float moveHorizontal = normalizeInput(Input.GetAxis("Horizontal"));
		float moveVertical = normalizeInput(Input.GetAxis("Vertical"));
		
		bool walkToggled = Input.GetButton("Walk");
		
		// figure movement
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		movement.Normalize();
		
		float frontBackQuotient = Mathf.Abs(movement.z);
		//float strafingQuotient = Mathf.Abs(movement.x);
		float strafingQuotient = 0;
		if (strafingQuotient > 0) {
			movement *= strafingQuotient * strafeVelocity;
		} else {
			if (movement.z < 0 || walkToggled) { // going backwards
				movement *= frontBackQuotient * walkVelocity;
			} else {
				movement *= frontBackQuotient * runVelocity;
			}
		}
		
		movement = transform.TransformDirection(movement);
		return movement;
	}

    void Update() {
        float rotateX = Input.GetAxis("Mouse X");
        float rotateY = Input.GetAxis("Mouse Y");

        bool jumpCommand = Input.GetButtonDown("Jump");
        bool attackCommand = Input.GetButtonDown("Fire1");

		if(!isHoldingBox){
        // mouse look x
        transform.Rotate(0, rotateX * sensitivityX * Time.deltaTime, 0);
		}
        // mouse look y
        rotationY += rotateY * sensitivityY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        cameraPivot.transform.localEulerAngles = new Vector3(-rotationY, cameraPivot.transform.localEulerAngles.y, 0);

        Vector3 velocity;
        if (!isJumping && !isFalling) {
            velocity = getHorizontalVelocity();
            velocity.y = -10; // keeps controller more
        } else {
            velocity = lastVelocity;
        }

        if (!controller.isGrounded) {
            float jumpTime = Time.time - jumpStartTime;
            if (isFalling) {
                velocity.y = Physics.gravity.y * gravitationalAccelerationMultiplier * jumpTime;
            } else if (isJumping) {
                velocity.y = (jumpInitialVelocity * elevationAngleSin) + (Physics.gravity.y * gravitationalAccelerationMultiplier * jumpTime);
            }

            // apply air resistance
            float horizontalVelocityQuotient = 1 - (horizontalVelocityDeceleration * Time.deltaTime);
            velocity.x = velocity.x * horizontalVelocityQuotient;
            velocity.z = velocity.z * horizontalVelocityQuotient;
        }

        if (controller.isGrounded) {
            isJumping = false;
            isFalling = false;
            avatarAnimator.SetBool("isJumping", false);
            if (jumpCommand) {
                avatarAnimator.SetBool("isJumping", true);
                isJumping = true;
                jumpStartTime = Time.time;

                velocity.y = -1;
                velocity *= (jumpInitialVelocity * elevationAngleSin) / velocity.magnitude;
                velocity.y = jumpInitialVelocity * elevationAngleCos;
            }
        } else if (!isFalling && !isJumping) {
            isFalling = true;
            jumpStartTime = Time.time;
        }

		if (isHoldingBox){
			velocity = getHorizontalVelocityBox();
			velocity.y = -1;
		}

        lastVelocity = velocity;

        controller.Move(velocity * Time.deltaTime);

        // set animation state
        avatarAnimator.SetBool("isGrounded", controller.isGrounded);
        avatarAnimator.SetBool("isJumping", !controller.isGrounded);
		if (controller.isGrounded && !isHoldingBox) {
			//avatarAnimator.SetBool("isRunningForward", normalizeInput(Input.GetAxis("Vertical")) > 0);
			avatarAnimator.SetFloat("ForwardBackward", normalizeInput(Input.GetAxis("Vertical")));
			//avatarAnimator.SetBool("isWalkingBackward", normalizeInput(Input.GetAxis("Vertical")) < 0);
			//avatarAnimator.SetBool("strafingLeft", normalizeInput(Input.GetAxis("Horizontal")) < 0);
			avatarAnimator.SetFloat("RightLeft", normalizeInput(Input.GetAxis("Horizontal")));
			//avatarAnimator.SetBool("strafingRight", normalizeInput(Input.GetAxis("Horizontal")) > 0);
			avatarAnimator.SetBool("stHands", false);
        } 
		else if(controller.isGrounded && isHoldingBox){
			avatarAnimator.SetFloat("ForwardBackward", normalizeInput(Input.GetAxis("Vertical")));
			avatarAnimator.SetBool("stHands", true);
		}
		else {
            avatarAnimator.SetBool("isRunningForward", false);
            avatarAnimator.SetBool("isWalkingBackward", false);
            avatarAnimator.SetBool("strafingLeft", false);
            avatarAnimator.SetBool("strafingRight", false);
        }
        if (attackCommand) {
			avatarAnimator.SetBool("isAttacking", true);
            switch (playerWeaponsHolder.getCurrentlyEquippedWeaponType()) {
                case PlayerWeaponsHolder.WeaponType.WeaponMelee:
                    avatarAnimator.SetInteger("isEquiped", 1);
					MeleeAttack();
                    break;
                case PlayerWeaponsHolder.WeaponType.WeaponRangedBow:
					avatarAnimator.SetInteger("isEquiped", 2);
					//shoot the arrow with a 1 second delay, due to the animation
					Invoke("ShootArrow", 1f);
                    break;
                case PlayerWeaponsHolder.WeaponType.WeaponRangedCrossbow:
					avatarAnimator.SetInteger("isEquiped", 3);
                    break;
            }
        } else {
			avatarAnimator.SetBool("isAttacking", false);
        }
    }
	void MeleeAttack()
	{
		RaycastHit hit;
		CharacterController charContr = GetComponent<CharacterController>();
		Vector3 p1 = transform.position + charContr.center + Vector3.up * -charContr.height * 0.5F;
		Vector3 p2 = p1 + Vector3.up * charContr.height;
		if (Physics.CapsuleCast(p1, p2, charContr.radius, transform.forward, out hit, 1))
		{
			EnemyStatsHolder stats = hit.transform.GetComponentInParent<EnemyStatsHolder>();
			if(stats != null)
				stats.modifyHealth(-50);
		}
	}

	void ShootArrow(){
		//pri vystreleni zobraz sip kousek pred hlavnim hrdinou
		Vector3 arrowPosition = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
		arrowPosition += transform.forward * 0.5f;
		Quaternion rot = transform.rotation;
		rot = rot * Quaternion.Euler(0, 270, 0);
		
		GameObject arrow = (GameObject)Instantiate(arrowPrefab, arrowPosition, rot);
		ArrowController arrowController = arrow.GetComponent<ArrowController>();
		arrowController.Direction = Vector3.right;
	}
}

