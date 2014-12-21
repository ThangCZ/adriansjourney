using UnityEngine;
using System.Collections;

public class FloatingSlider : MonoBehaviour {

    public GameObject slider;
    public GameObject target;
    public float maxVisibleDistance;
    public GameObject player;

	void Update () {
        if (Vector3.Distance(target.transform.position, player.transform.position) > maxVisibleDistance) {
            slider.SetActive(false);
        } else {
            slider.SetActive(true);
        }
        
        var wantedPos = Camera.main.WorldToScreenPoint(target.transform.position);
        slider.transform.position = wantedPos;
	}
}
