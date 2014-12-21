using UnityEngine;
using System.Collections;

public class MagicMenuController : MonoBehaviour {
	
	//beginnig of gui section
	public Texture2D spell;
	public Texture2D spellSelected;
	public Texture2D spellGrey;
	//public Texture2D spellHoverOver;
	public MagicSpellType thisSpellType;
	
	void Awake(){
		MagicMenuSingleton.MagicMenu.SpellChange += SpellChangeFunction;
		MagicMenuSingleton.MagicMenu.MenuActivate += ActivateMenu;
		MagicMenuSingleton.MagicMenu.MenuDeactivate += DeactivateMenu;
		PositionMenu();
	}

	void Update(){
		if((Input.GetKeyDown(KeyCode.Alpha1) && thisSpellType == MagicSpellType.fireball && MagicMenuSingleton.MagicMenu.FireballSkill) ||
		    (Input.GetKeyDown(KeyCode.Alpha2) && thisSpellType == MagicSpellType.firestorm && MagicMenuSingleton.MagicMenu.FirestormSkill) ||
		    (Input.GetKeyDown(KeyCode.Alpha3) && thisSpellType == MagicSpellType.mightyPush && MagicMenuSingleton.MagicMenu.MightyPushSkill)){
			if(MagicMenuSingleton.MagicMenu.MagicActive){
				MagicMenuSingleton.MagicMenu.SelectedSpell = thisSpellType;
				guiTexture.texture = spellSelected;
			}
		}
	}

	void DeactivateMenu(){
		guiTexture.texture = spellGrey;
	}

	void ActivateMenu(){
		if(MagicMenuSingleton.MagicMenu.SelectedSpell == thisSpellType)
			guiTexture.texture = spellSelected;
		else{
			switch(thisSpellType){
				case MagicSpellType.fireball:
					if(MagicMenuSingleton.MagicMenu.FireballSkill)
						guiTexture.texture = spell;
					break;
				case MagicSpellType.firestorm:
					if(MagicMenuSingleton.MagicMenu.FirestormSkill)
						guiTexture.texture = spell;
					break;
				case MagicSpellType.mightyPush:
					if(MagicMenuSingleton.MagicMenu.MightyPushSkill)
						guiTexture.texture = spell;
					break;
				default:
					break;
			}
		}
	}

	//set all spells as unselected
	void SpellChangeFunction(){
		guiTexture.texture = spellGrey;
			//if the player has the skill for particulare spell, display it the icon in the magic menu colored
			switch(thisSpellType){
				case MagicSpellType.fireball:
					if(MagicMenuSingleton.MagicMenu.FireballSkill)
						guiTexture.texture = spell;	
					break;
				case MagicSpellType.firestorm:
					if(MagicMenuSingleton.MagicMenu.FirestormSkill)
						guiTexture.texture = spell;
					break;
				case MagicSpellType.mightyPush:
					if(MagicMenuSingleton.MagicMenu.MightyPushSkill)
						guiTexture.texture = spell;
					break;
				default:
					break;

			}
	}

	void PositionMenu(){
		switch(thisSpellType){
			case MagicSpellType.fireball:
				transform.position = new Vector3(0f, 0f, 1f);
				transform.guiTexture.pixelInset = new Rect(Screen.width - 386, Screen.height - 95, 90, 90);

				//set the position of the menu background as well
				GUITexture gt = GameObject.Find("MagicMenuBackground").GetComponent("GUITexture") as GUITexture;
				gt.pixelInset = new Rect(Screen.width - 500, Screen.height - 99, 500, 100);
				gt.transform.position = Vector3.zero;
				break;
			case MagicSpellType.firestorm:
				transform.position = new Vector3(0f, 0f, 1f);
				transform.guiTexture.pixelInset = new Rect(Screen.width - 256, Screen.height - 95, 90, 90);
				break;
			case MagicSpellType.mightyPush:
				transform.position = new Vector3(0f, 0f, 1f);
				transform.guiTexture.pixelInset = new Rect(Screen.width - 126, Screen.height - 95, 90, 90);
				break;
			default:
				break;
		}
	}

	/* Nechal jsem to tady zakomentovane pro pripad, ze bychom se v budoucnu rozhodli pouzit mys,
	 * tak to prosim nemazte
	void OnMouseEnter(){
		guiTexture.texture = spellHoverOver;
	}
	
	void OnMouseExit(){
		//check if this spell is a selected spell
		MagicSpellType sSpell = MagicMenuSingleton.MagicMenu.SelectedSpell;
		if(sSpell == thisSpellType)
			guiTexture.texture = spellSelected;
		else
			guiTexture.texture = spell;
	}

	void OnMouseDown(){
		MagicMenuSingleton.MagicMenu.SelectedSpell = thisSpellType;
		guiTexture.texture = spellSelected;
	}
	*/
	
}
