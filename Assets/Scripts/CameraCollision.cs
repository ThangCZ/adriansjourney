using UnityEngine;
using System.Collections;

public class CameraCollision : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	public float collisionRadius = 0.5f;
	private Vector3 offset;
	private Vector3 oldpos;
	private Vector3 velocity1 = Vector3.zero;
	private Vector3 velocity2 = Vector3.zero;
	void Start ()
	{
		offset = transform.localPosition;
	}

	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit = new RaycastHit();
		int mask = ~(1 << 8);
		Debug.DrawLine(target.position, Vector3.MoveTowards(this.transform.position, target.position, -collisionRadius), Color.cyan);
		if(Physics.Linecast(target.position, Vector3.MoveTowards(this.transform.position, target.position, -collisionRadius), out hit, mask))
		{
            //Debug.Log (hit.transform.name);
			Vector3 to = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
			to = Vector3.MoveTowards(to, target.position, collisionRadius); //Vector3.Distance(this.transform.position, to) - 2.5f);
			this.transform.position = Vector3.SmoothDamp(this.transform.position, to, ref velocity1, smoothTime); 
			//this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(hit.point.x, this.transform.position.y, hit.point.z), ref velocity1, smoothTime); 
		}
		else
		{
			this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, offset, ref velocity2, smoothTime); 
		}
	}
}
