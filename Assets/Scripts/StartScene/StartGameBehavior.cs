using UnityEngine;
using System.Collections;

public class StartGameBehavior : MonoBehaviour {
	public void StartGame() {
		Debug.Log ("Starting Game");
		Application.LoadLevel ("Arena"); 
	}
}
