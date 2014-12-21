using UnityEngine;
using System.Collections;

public class MagicMenuSingleton : MonoBehaviour {
	
	private static MagicMenuSingleton singletonInstance;
	
	public delegate void OnSpellChangeEvent();	
	public event OnSpellChangeEvent SpellChange;
	public event OnSpellChangeEvent MenuActivate;
	public event OnSpellChangeEvent MenuDeactivate;
	
	//private constructor
	private MagicMenuSingleton() {
	}
	
	public static MagicMenuSingleton MagicMenu{
		get{
			singletonInstance = GameObject.FindObjectOfType(typeof(MagicMenuSingleton)) as MagicMenuSingleton; 
			
			if(singletonInstance == null){
				GameObject obj = new GameObject("Singleton::MagicMenuSingleton");
				singletonInstance = obj.AddComponent<MagicMenuSingleton>();
			}
			return singletonInstance;
		}
	}
	
	private MagicSpellType selectedSpell = MagicSpellType.noSpell;
	
	public MagicSpellType SelectedSpell
	{
		get{ return selectedSpell; }
		set
		{ 
			selectedSpell = value;
			SpellChange();
		}
	}

	private bool fireballSkill = false;
	public bool FireballSkill{
		get{ return fireballSkill; }
		set { 
			fireballSkill = value;
			SpellChange();
		}
	}

	private bool firestormSkill = false;
	public bool FirestormSkill{
		get{ return firestormSkill; }
		set { 
			firestormSkill = value;
			SpellChange();
		}
	}

	private bool mightyPushSkill = false;
	public bool MightyPushSkill{
		get{ return mightyPushSkill; }
		set { 
			mightyPushSkill = value;
			SpellChange();
		}
	}

	//this is used for the progress bar - cool down period during which a player cant use any spell
	private bool magicActive = true;
	public bool MagicActive{
		get {return magicActive;}
		set {
			magicActive = value;
			if(magicActive)
				MenuActivate();
			else
				MenuDeactivate();
		}
	}
}
