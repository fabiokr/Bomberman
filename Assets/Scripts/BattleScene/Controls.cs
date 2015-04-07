using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls {

	private Hashtable buttons;

	public Controls (int player) {

		// Default values
		switch (player) {
		case 1:
			buttons = new Hashtable ()
			{
				{ "Up", KeyCode.UpArrow },
				{ "Down", KeyCode.DownArrow },
				{ "Left", KeyCode.LeftArrow },
				{ "Right", KeyCode.RightArrow },
				{ "Bomb", KeyCode.Space },
			};

			break;
		case 2:
			buttons = new Hashtable ()
			{
				{ "Up", KeyCode.Keypad8 },
				{ "Down", KeyCode.Keypad2 },
				{ "Left", KeyCode.Keypad4 },
				{ "Right", KeyCode.Keypad6 },
				{ "Bomb", KeyCode.Keypad0 },
			};

			break;
		case 3:
			buttons = new Hashtable ()
			{
				{ "Up", KeyCode.Y },
				{ "Down", KeyCode.U },
				{ "Left", KeyCode.I },
				{ "Right", KeyCode.O },
				{ "Bomb", KeyCode.P },
			};

			break;
		case 4:
			buttons = new Hashtable ()
			{
				{ "Up", KeyCode.G },
				{ "Down", KeyCode.H },
				{ "Left", KeyCode.J },
				{ "Right", KeyCode.K },
				{ "Bomb", KeyCode.L },
			};

			break;
		default:
			throw new UnityException();
			break;
		}

		// If config has new values, the defaults will be overwritten
		LoadButtonsFromConfig (player);
	}

	private void LoadButtonsFromConfig(int player) {
		// Get list of keys in the buttons Hashtable		
		List<string> button_keys = new List<string> ();
		foreach (string key in buttons.Keys) {
			button_keys.Add (key);
		}

		// Update buttons Hashtable with config values
		foreach (string button in button_keys) {
			string key = button + player.ToString();

			if (PlayerPrefs.HasKey(key)) {
				buttons[button] = PlayerPrefs.GetInt(key);
			}
		}
	}

	public bool getUp () {
		return Input.GetKey ((KeyCode) buttons["Up"]);
	}

	public bool getDown () {
		return Input.GetKey ((KeyCode) buttons["Down"]);
	}

	public bool getLeft () {
		return Input.GetKey ((KeyCode) buttons["Left"]);
	}

	public bool getRight () {
		return Input.GetKey ((KeyCode) buttons["Right"]);
	}

	public bool getPlaceBomb () {
		return Input.GetKeyDown ((KeyCode) buttons["Bomb"]);
	}
}
