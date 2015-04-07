using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigureGameBehaviour : MonoBehaviour {

	private GameObject press_button_alert;
	private GameObject current_button;

	void Start () {
		press_button_alert = GameObject.Find ("Press_Button_Alert");
		press_button_alert.SetActive (false); // Hide object
		current_button = null;

		// Update current buttons on screen
		UpdateButtonsText ();
	}

	void OnGUI () {
		Event e = Event.current;
		if (e.isKey && e.keyCode != KeyCode.None) {

			if (current_button != null) {

				// Set button on config
				SetButton(e.keyCode);

				// Shows new button to the player
				UpdateButtonText(current_button, e.keyCode);

				// Reset current button
				current_button = null;

				// Hide alert
				press_button_alert.SetActive(false);
			}
		}
	}

	public void SetCurrentButton(GameObject button) {
		Debug.Log ("Current button set to " + button);
		current_button = button;
		press_button_alert.SetActive(true); // Show alert
	}

	private void SetButton(KeyCode keycode) {
		string player = GetPlayer ();
		string key = current_button.name + GetPlayer ();
		int value = (int) keycode;
		
		PlayerPrefs.SetInt (key, value);
		
		Debug.Log ("Button " + current_button.name + " set to " + keycode + " for player " + player);
	}

	private KeyCode GetButton(string name) {
		string player = GetPlayer ();
		string key = name + GetPlayer ();

		return (KeyCode) PlayerPrefs.GetInt (key);
	}

	private void UpdateButtonText(GameObject button, KeyCode keycode) {
		Text text = button.transform.Find ("Current").GetComponent<Text>();
		text.text = keycode.ToString ();
	}

	public void UpdateButtonsText() {
		foreach (GameObject current in GameObject.FindGameObjectsWithTag("Button")) {
			KeyCode button = GetButton(current.name);
			UpdateButtonText(current, button);
		}
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
