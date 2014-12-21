using UnityEngine;
using System.Collections;

public class BridgeController : MonoBehaviour {

	public bool movable;

	// Use this for initialization
	void Start () {
		movable = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Pushbox"))
		{
			movable = false;
			//transform.renderer.material.color = Color.red;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("Pushbox"))
		{
			movable = true;
		}
	}
}
