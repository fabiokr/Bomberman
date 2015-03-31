using UnityEngine;
using System.Collections;

public class StartGameBehavior : MonoBehaviour {
	void Update() {
		if (Input.GetKey (KeyCode.Return)) {
			StartGame();
		}
	}

	public void StartGame() {
		Debug.Log ("Starting Game");
		Application.LoadLevel ("Arena"); 
	}
}
