using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {

	private bool playerBy;
	private bool playerHolding;

	private float yPositionThreshold;
	private bool boxFalling;

	//private PlayerMovementController pmc;
	// Use this for initialization
	void Start () {
		playerBy = false;
		playerHolding = false;
		yPositionThreshold = transform.position.y - 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(playerBy && Input.GetKeyUp(KeyCode.E)){
			PlayerMovementController pmc = GameObject.FindObjectOfType(typeof(PlayerMovementController)) as PlayerMovementController;
			if(playerHolding){
				playerHolding = false;
				pmc.isHoldingBox = false;
				transform.parent = null;
				//transform.renderer.material.color = Color.white;

				playerBy = false;
			}else{
				playerHolding = true;
				pmc.isHoldingBox = true;
				transform.parent = pmc.gameObject.transform;
				//transform.renderer.material.color = Color.red;
			}
		}

		if(boxFalling){
			//pokud pada, zadej novou threshold
			yPositionThreshold = transform.position.y - 1.0f;
			boxFalling = false;
		}

		//handle box falling down
		if(transform.position.y < yPositionThreshold){
			boxFalling = true;
			//transform.renderer.material.color = Color.green;
			if(playerHolding){
				playerHolding = false;
				PlayerMovementController pmc = GameObject.FindObjectOfType(typeof(PlayerMovementController)) as PlayerMovementController;
				pmc.isHoldingBox = false;
				transform.parent = null;
				//transform.renderer.material.color = Color.white;
			}
		}
	}

	/*
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			playerBy = true;
		}
	}
	*/

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			playerBy = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			if(!playerHolding)
				playerBy = false;
		}
	}

}
