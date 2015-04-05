using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfigureGameBehaviour : MonoBehaviour {

	string current_button;
	Hashtable buttons;

	void Start () {
		buttons = new Hashtable();
		current_button = "";
	}

	void OnGUI () {
		Event e = Event.current;
		if (e.isKey) {

			if (current_button.Length > 0) {
				string key = current_button + GetPlayer();

				if (buttons.ContainsKey(key))
					buttons.Remove(key);

				buttons.Add(key, e.keyCode);

				Debug.Log("Button " + current_button + " set to " + e.keyCode + " for player " + GetPlayer());

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
