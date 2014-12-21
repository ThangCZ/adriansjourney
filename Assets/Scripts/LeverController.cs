using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {


	private int position;
	private bool leverReady;
	private float liftDistance = 3.5f;


	public GameObject bridgeA;
	public GameObject bridgeB;

	private bool bridgeUpMoving;
	private bool bridgeDownMoving;

	private float startPositionUp;
	private float startPositionDown;

	private GameObject bridgeUp;
	private GameObject bridgeDown;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.AngleAxis(20, Vector3.right);
		position = 0;
		leverReady = false;

		//set the bridgeA to be lifted on init
		bridgeA.transform.position = new Vector3(bridgeA.transform.position.x, bridgeA.transform.position.y + liftDistance, bridgeA.transform.position.z);
	}

	public void Switch(){
		if(position == 0){
			transform.rotation = Quaternion.AngleAxis(-20, Vector3.right);
			position = 1;

			bridgeDown = bridgeA;
			bridgeUp = bridgeB;

			bridgeUpMoving = true;
			bridgeDownMoving = true;
		}else{
			transform.rotation = Quaternion.AngleAxis(20, Vector3.right);
			position = 0;

			bridgeDown = bridgeB;
			bridgeUp = bridgeA;

			bridgeUpMoving = true;
			bridgeDownMoving = true;
		}
	}

	public int GetPosition(){
		return position;
	}

	void Update() {
		if (leverReady && Input.GetKeyDown(KeyCode.E) && !bridgeDownMoving && !bridgeUpMoving) {
			Switch();
			startPositionDown = bridgeDown.transform.position.y;
			startPositionUp = bridgeUp.transform.position.y;
		}

		if(bridgeDownMoving){
			BridgeController bridgeControllerDown = bridgeDown.GetComponent<BridgeController>();
			if(bridgeControllerDown.movable){
			bridgeDown.transform.position = Vector3.Lerp(bridgeDown.transform.position,
			new Vector3(bridgeDown.transform.position.x, bridgeDown.transform.position.y - liftDistance, bridgeDown.transform.position.z),
				             Time.deltaTime * 0.3f );
			if(bridgeDown.transform.position.y <= startPositionDown - liftDistance){
				bridgeDownMoving = false;
			}
			}
		}

		if(bridgeUpMoving){
			BridgeController bridgeControllerUp = bridgeUp.GetComponent<BridgeController>();
			if(bridgeControllerUp.movable){
			bridgeUp.transform.position = Vector3.Lerp(bridgeUp.transform.position,
			                                           new Vector3(bridgeUp.transform.position.x, bridgeUp.transform.position.y + liftDistance, bridgeUp.transform.position.z),
			                                             Time.deltaTime * 0.3f );
			if(bridgeUp.transform.position.y >= startPositionUp + liftDistance){
				bridgeUpMoving = false;
			}
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			leverReady = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			leverReady = false;
		}
	}
	

}
