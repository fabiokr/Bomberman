using UnityEngine;
using System.Collections;

public class GameControllerBehavior : MonoBehaviour {
	public static GameControllerBehavior instance { get; private set; }

	public int startingTimer = 180;

	bool timerHurry = false, timerFinished = false;
	
	void Start () {
		instance = this;
	}

	public void TimerHurry() {
		if (!timerHurry) {
			timerHurry = true;
			Debug.Log("HURRY!");
		}
	}

	public void TimerFinished() {
		if (!timerFinished) {
			timerFinished = true;
			Debug.Log("GAME OVER!");
		}
	}
}
