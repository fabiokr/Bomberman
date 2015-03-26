using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (GameControllerHurryBehavior))]
public class GameControllerBehavior : MonoBehaviour {
	public static GameControllerBehavior instance { get; private set; }

	public int startingTimer = 180;
	public bool gameInProgress = false;
	public bool gameStarted = false;
	public float timer;
	public Vector3 startingPosition;
	public StageGenerator stageGenerator;
	public GameObject gameOverText;

	bool timerFinished = false;
	float scriptStartingTime;
	
	GameControllerHurryBehavior hurryBehavior;
	TimerBehavior timerBehavior;
	
	void Start () {
		scriptStartingTime = Time.time;
		instance = this;
		stageGenerator = GameObject.FindGameObjectWithTag (Tags.Stage).GetComponent<StageGenerator>();
		hurryBehavior = GetComponent<GameControllerHurryBehavior> ();
		hurryBehavior.enabled = false;
		timer = startingTimer;
		SetGameOverText ("Start!");
	}

	void Update() {
		if (!gameStarted && Time.time - scriptStartingTime > 3) {
			gameInProgress = true;
			gameStarted = true;
			SetGameOverText ("");
		}

		if (gameInProgress) {
			Timer ();
			GameOverCheck ();
		}
	}

	public void TimerHurry() {
		if (!hurryBehavior.enabled) {
			hurryBehavior.enabled = true;
		}
	}

	public void TimerFinished() {
		if (!timerFinished) {
			timerFinished = true;
		}
	}

	void Timer() {
		if (!timerFinished) {
			timer -= Time.deltaTime;
		
			if ((int)timer == 60) {
				TimerHurry ();
			} else if ((int)timer == 0) {
				TimerFinished ();
			}
		}
	}

	void GameOverCheck() {
		GameObject[] list = BombermanList ();

		if (list.Length == 1) {
			GameOver ();
			SetGameOverText(list[0].name + " wins!");
		} else if (list.Length == 0) {
			GameOver ();
			SetGameOverText("Draw!");
		}
	}

	void GameOver() {
		gameInProgress = false;
		TimerFinished ();
	}

	GameObject[] BombermanList() {
		return GameObject.FindGameObjectsWithTag (Tags.Bomberman);
	}

	void SetGameOverText(string text) {
		gameOverText.GetComponent<Text> ().text = text;
	}
}
