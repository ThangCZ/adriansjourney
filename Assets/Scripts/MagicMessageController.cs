using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagicMessageController : MonoBehaviour {

	public Text message1;
    public Text message2;

	private string message2_2;
	private int timeDuration2;

	private string message1_2;
	private int timeDuration1;

	// Use this for initialization
	void Start () {
		message1.text = string.Empty;
		message2.text = string.Empty;
	}

	public void setMessage1(string text, int timeDuration){
		message1.text = text;
		InvokeRepeating("clearMessage1", timeDuration, timeDuration * 2);
	}

	public void setMessage1(string text1, string text2, int timeDuration){
		message1.text = text1;
		message1_2 = text2;
		timeDuration1 = timeDuration;
		InvokeRepeating("changeMessage1", timeDuration, timeDuration * 2);
	}

	void changeMessage1(){
		message1.text = message1_2;
		CancelInvoke("changeMessage1");
		InvokeRepeating("clearMessage1", timeDuration1, timeDuration1);
	}

	void clearMessage1(){
		message1.text = string.Empty;
		CancelInvoke("clearMessage1");
	}


	public void setMessage2(string text, int timeDuration){
		message2.text = text;
		InvokeRepeating("clearMessage2", timeDuration, timeDuration * 2);
	}

	public void setMessage2(string text1, string text2, int timeDuration){
		message2.text = text1;
		message2_2 = text2;
		timeDuration2 = timeDuration;
		InvokeRepeating("changeMessage2", timeDuration, timeDuration * 2);
	}

	void changeMessage2(){
		message2.text = message2_2;
		CancelInvoke("changeMessage2");
		InvokeRepeating("clearMessage2", timeDuration2, timeDuration2);
	}
	
	void clearMessage2(){
		message2.text = string.Empty;
		CancelInvoke("clearMessage2");
	}

}
