using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerBehavior : MonoBehaviour {	
	Text text;

	void Start() {
		text = GetComponent<Text> ();
	}

	void Update () {
		int displaySeconds = (int)(GameControllerBehavior.instance.timer % 60);

		if (displaySeconds == 60) {
			displaySeconds = 0;
		}

		int displayMinutes = (int)Mathf.Floor (GameControllerBehavior.instance.timer / 60);

		text.text = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds);
	}
}
