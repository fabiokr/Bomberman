using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigureGameBehaviour : MonoBehaviour {

	string current_button;

	void Start () {
		current_button = "";
	}

	void OnGUI () {
		Event e = Event.current;
		if (e.isKey && e.keyCode != KeyCode.None) {

			if (current_button.Length > 0) {
				string player = GetPlayer();
				string key = current_button + GetPlayer();
				int value = (int) e.keyCode;

				PlayerPrefs.SetInt(key, value);

				Debug.Log("Button " + current_button + " set to " + e.keyCode + " for player " + player);

				// Reset current button
				current_button = "";
			}
		}
	}

	public void SetCurrentButton(string button) {
		Debug.Log ("Current button set to " + button);
		current_button = button;
	}

	public void GoBack() {
		PlayerPrefs.Save ();
		Application.LoadLevel ("Start");
	}

	private string GetPlayer() {
		string player = "";
		Toggle[] toggles = FindObjectsOfType(typeof(Toggle)) as Toggle[];

		foreach (Toggle toggle in toggles) {
			if (toggle.isOn)
				player = toggle.name;
		}
		
		return player;
	}
}
