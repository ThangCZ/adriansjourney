#pragma strict

var sound:AudioClip;
function Swing () {
	audio.PlayOneShot(sound);
	yield WaitForSeconds(1);
}
