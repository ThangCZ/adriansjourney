using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class MagicHandler : MonoBehaviour
{
    public GameObject fireballPrefab;

    public GameObject fireballDirectionObject;

	public Transform firestorm;
	private Transform firestormInstance;

	public Transform mightyPush;
	private Transform mightyPushInstance;

	PlayerStatsHolder statsHolder;

    void Update()
    {
		if(Input.GetButtonDown("Fire2") && MagicMenuSingleton.MagicMenu.MagicActive)	
        {
			switch(MagicMenuSingleton.MagicMenu.SelectedSpell){
				case MagicSpellType.fireball:
					Vector3 fireballPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
					GameObject fireball = (GameObject)Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
					FireballController fireballController = fireball.GetComponent<FireballController>();
					fireballController.Direction = fireballDirectionObject.transform.rotation * Vector3.forward;
					MagicMenuSingleton.MagicMenu.MagicActive = false;
					if(statsHolder == null)
						statsHolder = this.gameObject.GetComponent<PlayerStatsHolder>();
					statsHolder.modifyMana(-20);
					break;
				case MagicSpellType.firestorm:
					if(firestormInstance == null){
						Vector3 firestormPosition = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
						//display firestorm in front of you
						firestormPosition += transform.forward * 4;
						//Quaternion rot = new Quaternion(270, 0, 0, 0);
						Quaternion rot = transform.rotation;
						rot = rot * Quaternion.Euler(270, 0, 0);
						firestormInstance = Instantiate(firestorm, firestormPosition, rot) as Transform;
						MagicMenuSingleton.MagicMenu.MagicActive = false;
						if(statsHolder == null)
							statsHolder = this.gameObject.GetComponent<PlayerStatsHolder>();
						statsHolder.modifyMana(-50);
						}
					break;
				case MagicSpellType.mightyPush:
					if(mightyPushInstance == null){
						mightyPushInstance = Instantiate(mightyPush) as Transform;
						mightyPushInstance.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
						MagicMenuSingleton.MagicMenu.MagicActive = false;
						if(statsHolder == null)
							statsHolder = this.gameObject.GetComponent<PlayerStatsHolder>();
						statsHolder.modifyMana(-40);
					}
					break;
				default:
					break;
			}
			/*
			if(MagicMenuSingleton.MagicMenu.SelectedSpell == MagicSpellType.fireball)
			{
	            Vector3 fireballPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
	            GameObject fireball = (GameObject)Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
	            FireballController fireballController = fireball.GetComponent<FireballController>();
	            fireballController.Direction = fireballDirectionObject.transform.rotation * Vector3.forward;
			}
			else if(MagicMenuSingleton.MagicMenu.SelectedSpell == MagicSpellType.firestorm)
			{	
				if(firestormInstance == null)
					firestormInstance = Instantiate(firestorm) as Transform;
			}
			*/
        }
    }
}
