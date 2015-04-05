using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigureGameBehaviour : MonoBehaviour {

	GameObject press_button_alert;
	string current_button;

	void Start () {
		press_button_alert = GameObject.Find ("Press_Button_Alert");
		press_button_alert.SetActive (false); // Hide object
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

				// Hide alert
				press_button_alert.SetActive(false);
			}
		}
	}

	public void SetCurrentButton(string button) {
		Debug.Log ("Current button set to " + button);
		current_button = button;
		press_button_alert.SetActive(true); // Show alert
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
