using UnityEngine;
using System.Collections;

public class MagicMenuProgressBar : MonoBehaviour {

	public Texture coolDownBar;
	private float barWidth;
	private float barWidthMax = 400;

	private float left;
	private float top;
	private float height;

	// Use this for initialization
	void Start () {
		ResetWidth();
		top = 86;
		height = 8;
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(left, top, barWidth, height)
		                , coolDownBar, ScaleMode.StretchToFill, true, 1.0f); 
	}

	// Update is called once per frame
	void Update () {
		if(!MagicMenuSingleton.MagicMenu.MagicActive){
			if(barWidth < barWidthMax){
				barWidth += 2f;
				left -= 2f;
			}else{
				MagicMenuSingleton.MagicMenu.MagicActive = true;
				ResetWidth();
			}
		}
	}

	void ResetWidth(){
		left = Screen.width;
		barWidth = 0f;
	}
}
