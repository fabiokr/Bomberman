using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (GameControllerHurryBehavior))]
public class GameControllerBehavior : MonoBehaviour {
	public static GameControllerBehavior instance { get; private set; }

	public int startingTimer = 180;
	public int gameStartDelay = 5;
	public bool gameInProgress = false;
	public bool gameStarted = false;
	public float timer;
	public Vector3 startingPosition;
	public StageGenerator stageGenerator;
	public GameObject gameOverText;
	AudioSource audioSource;
	public AudioClip startClip, battleClip, battleHurryClip, winClip, drawClip;

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

		audioSource = GetComponent<AudioSource> ();
		StartCoroutine(PlayAudioDelayed(startClip, 0f));
		StartCoroutine(PlayAudioDelayed(battleClip, gameStartDelay));
	}

	void Update() {
		if (!gameStarted && Time.time - scriptStartingTime > gameStartDelay) {
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
			SetGameOverText("Hurry!");
			StartCoroutine(ClearGameOverText(3));
			PlayAudio(battleHurryClip);
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
			PlayAudio(winClip);
		} else if (list.Length == 0) {
			GameOver ();
			SetGameOverText("Draw!");
			PlayAudio(drawClip);
		}
	}

	void GameOver() {
		gameInProgress = false;
		hurryBehavior.enabled = false;
		TimerFinished ();
	}

	GameObject[] BombermanList() {
		return GameObject.FindGameObjectsWithTag (Tags.Bomberman);
	}

	IEnumerator ClearGameOverText(float delay) {
		yield return new WaitForSeconds(delay);
		SetGameOverText ("");
	}

	void SetGameOverText(string text) {
		gameOverText.GetComponent<Text> ().text = text;
	}

	IEnumerator PlayAudioDelayed(AudioClip clip, float delay) {
		yield return new WaitForSeconds(delay);
		PlayAudio (clip);
	}

	void PlayAudio(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play();
	}
}
