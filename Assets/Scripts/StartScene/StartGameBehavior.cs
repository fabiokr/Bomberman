using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGameBehavior : MonoBehaviour {
	public int players = 2;
	public GameObject button;
	public string buttonText = "< %n% Players >";
	public float buttonAnimationFactor = 0.10f;
	public float buttonAnimationSpeed = 1f;

	string axis = "Player 1 Horizontal";
	string confirm = "Player 1 Bomb";
	string confirm2 = "Submit";
	bool axisInUse = false;
	int buttonFontSize;

	void Start() {
		buttonFontSize = button.GetComponent<Text> ().fontSize;
		UpdatePlayers ();
	}

	void Update() {
		Move ();

		if (Input.GetButtonDown (confirm) || Input.GetButtonDown (confirm2)) {
			StartGame();
		}
	}

	public void StartGame() {
		PlayerPrefs.SetInt ("Players", players);
		Debug.Log ("Starting Game");
		Application.LoadLevel ("Arena"); 
	}

	void Move() {
		float x = Input.GetAxisRaw (axis);

		if (x != 1f && x != -1f) {
			axisInUse = false;
		} else {
			if(!axisInUse) {
				axisInUse = true;

				if(x > 0 && (players + 1 <= 4)) {
					players++;
					UpdatePlayers();
				} else if(x < 0 && (players - 1 > 1)) {
					players--;
					UpdatePlayers();
				}
			}
		}    
	}

	void UpdatePlayers() {
		button.GetComponent<Text> ().text = buttonText.Replace ("%n%", players.ToString());
	}
}
