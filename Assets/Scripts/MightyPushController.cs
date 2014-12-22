using UnityEngine;
using System.Collections;

public class MightyPushController : MonoBehaviour {

	private float lifeSpan = 7f;

	// Use this for initialization
	void Start () {
		//Enemy objekty pro mighty push musi mit collider a rigidbody
		
		//get all colliders within radius
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        //Debug.Log(hitColliders.Length);
        foreach( var c in hitColliders){
			if(c.tag == "Enemy"){
                //if(c.rigidbody != null)
					c.rigidbody.AddExplosionForce(15, transform.position, 10, 0, ForceMode.Impulse);
				//pro debugovani - oznac zasazeny objekt cervene (bude odstraneno)
                //c.transform.renderer.material.color = Color.red;
                    EnemyStatsHolder enemyStatsHolder = c.gameObject.GetComponent<EnemyStatsHolder>();
                    enemyStatsHolder.modifyHealth(-50);
			}
		}

		Object.Destroy(this.gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
