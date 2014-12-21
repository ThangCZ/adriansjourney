using UnityEngine;
using System.Collections;

public class PickupControllerFirestorm : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player")) {
			MagicMenuSingleton.MagicMenu.FirestormSkill = true;
			Destroy(gameObject);
			MagicMessageController magicMessageController = GameObject.FindObjectOfType(typeof(MagicMessageController)) as MagicMessageController; 
			magicMessageController.setMessage1("Well done!", 8);
			magicMessageController.setMessage2("You have unlocked a new Spell: FIRESTORM", 8);
		}
	}
}
