using UnityEngine;
using System.Collections;

public class FirestormController : MonoBehaviour {

	// lifespan nastaven na 7 sekund. Firestorm prefab emituje castice po dobu 3 sekund, 
	// zbytek casu (tj. 4 sekundy) je doba po kterou muzou posledni vygenerovane castice padat k zemi, coz realne trva tak 2-3 sekundy.
	// Nastavil jsem to na dele pro pripad, ze by byl firestorm spusten napr. nad propasti, tj. aby castice nezmizely uprostred padani.
	private float lifeSpan = 7f;
	public int damage;
	// Use this for initialization
	void Start () {
		Object.Destroy(this.gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision (GameObject other) 
	{    
		//Poznamka - aby kolize fungovala, enemy nemuze mit zaskrtnuto Is Trigger v BoxCollider
		//other.transform.renderer.material.color = Color.blue;

		//avoid any interaction with firestorm and Adrian
		if (other.tag != "Player")
		{
			if (other.tag == "Enemy")
			{
				Debug.Log("enemy collision");
				EnemyStatsHolder enemyStatsHolder = other.GetComponent<EnemyStatsHolder>();
				enemyStatsHolder.modifyHealth(-damage);
			}
		}

	}

}
