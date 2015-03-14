﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerBehavior : MonoBehaviour {	
	public float timer = 180;

	Text text;
	bool finished = false;

	void Start() {
		text = GetComponent<Text> ();
		timer = GameControllerBehavior.instance.startingTimer;
	}

	void Update () {
		if (!finished) {
			UpdateTimer();

			int displaySeconds = (int)(timer % 60);

			if (displaySeconds == 60) {
				displaySeconds = 0;
			}

			int displayMinutes = (int)Mathf.Floor (timer / 60);

			text.text = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds);
		}
	}

	void UpdateTimer() {
		timer -= Time.deltaTime;

		if ((int)timer == 175) {
			GameControllerBehavior.instance.TimerHurry();
		} else if ((int)timer == 0) {
			finished = true;
			GameControllerBehavior.instance.TimerFinished();
		}
	}
}