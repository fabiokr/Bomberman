using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGameBehavior : MonoBehaviour {
	void Start() {
		Text players = GameObject.Find ("Players").GetComponent<Text> ();
		SetPlayers (players.text);
	}

	void Update() {
		if (Input.GetKey (KeyCode.Return)) {
			StartGame();
		}
	}

	public void StartGame() {
		Debug.Log ("Starting Game");
		Application.LoadLevel ("Arena"); 
	}

	public void SetPlayers (string players) {
		PlayerPrefs.SetInt ("Players", int.Parse (players));
	}
}
