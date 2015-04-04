using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Music
{
	public AudioClip normalClip, hurryClip;
}

[RequireComponent (typeof (GameControllerHurryBehavior))]
[RequireComponent (typeof (AudioSource))]
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
	public AudioClip startClip, hurryClip, winClip, drawClip;
	public List<Music> playlist;

	bool timerFinished = false;
	float scriptStartingTime;
	
	GameControllerHurryBehavior hurryBehavior;
	TimerBehavior timerBehavior;

	Music music;
	
	void Start () {
		scriptStartingTime = Time.time;
		instance = this;
		stageGenerator = GameObject.FindGameObjectWithTag (Tags.Stage).GetComponent<StageGenerator>();
		hurryBehavior = GetComponent<GameControllerHurryBehavior> ();
		hurryBehavior.enabled = false;
		timer = startingTimer;
		SetGameOverText ("Start!");

		music = GetRandomMusic ();

		audioSource = GetComponent<AudioSource> ();
		StartCoroutine(PlayAudioDelayed(startClip, 0f));
		StartCoroutine(PlayAudioDelayed(music.normalClip, gameStartDelay));
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
			SetGameOverText("Hurry!");
			PlayAudio(hurryClip);
			StartCoroutine(EnableHurryBehavior());
		}
	}

	IEnumerator EnableHurryBehavior() {
		yield return new WaitForSeconds(3f);
		SetGameOverText ("");
		PlayAudio (music.hurryClip);
		yield return new WaitForSeconds(1f);
		hurryBehavior.enabled = true;
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
		StartCoroutine(LoadStartMenu());
	}

	GameObject[] BombermanList() {
		return GameObject.FindGameObjectsWithTag (Tags.Bomberman);
	}

	void SetGameOverText(string text) {
		gameOverText.GetComponent<Text> ().text = text;
	}

	IEnumerator PlayAudioDelayed(AudioClip clip, float delay) {
		yield return new WaitForSeconds(delay);
		PlayAudio (clip);
	}

	IEnumerator LoadStartMenu() {
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("Start"); 
	}

	void PlayAudio(AudioClip clip) {
		audioSource.Stop ();
		audioSource.clip = clip;
		audioSource.Play();
	}

	Music GetRandomMusic() {
		return playlist[Random.Range (0, playlist.Count)];
	}
}
