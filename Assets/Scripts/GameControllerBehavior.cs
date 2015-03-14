using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GameControllerHurryBehavior))]
public class GameControllerBehavior : MonoBehaviour {
	public static GameControllerBehavior instance { get; private set; }

	public int startingTimer = 180;
	public Vector3 startingPosition;
	public StageGenerator stageGenerator;

	bool timerFinished = false;
	
	GameControllerHurryBehavior hurryBehavior;
	
	void Start () {
		instance = this;
		stageGenerator = GameObject.FindGameObjectWithTag (Tags.Stage).GetComponent<StageGenerator>();
		hurryBehavior = GetComponent<GameControllerHurryBehavior> ();
		hurryBehavior.enabled = false;
	}

	public void TimerHurry() {
		if (!hurryBehavior.enabled) {
			hurryBehavior.enabled = true;
		}
	}

	public void TimerFinished() {
		if (!timerFinished) {
			timerFinished = true;
			Debug.Log("GAME OVER!");
		}
	}
}
