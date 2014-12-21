using UnityEngine;
using System.Collections;

public class PickupControllerFireball : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player")) {
			MagicMenuSingleton.MagicMenu.FireballSkill = true;
			Destroy(gameObject);
			MagicMessageController magicMessageController = GameObject.FindObjectOfType(typeof(MagicMessageController)) as MagicMessageController; 
			magicMessageController.setMessage1("Congratulations!", 8);
			magicMessageController.setMessage2("You have unlocked a new Spell: FIREBALL", 8);
		}
	}
}
