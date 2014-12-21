using UnityEngine;
using System.Collections;

public class PickupControllerMightyPush : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player")) {
			MagicMenuSingleton.MagicMenu.MightyPushSkill = true;
			Destroy(gameObject);
			MagicMessageController magicMessageController = GameObject.FindObjectOfType(typeof(MagicMessageController)) as MagicMessageController; 
			magicMessageController.setMessage1("Excellent!", 8);
			magicMessageController.setMessage2("You have unlocked a new Spell: MIGHTY PUSH", 8);
		}
	}
}
