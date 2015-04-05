using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls {
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	public KeyCode PlaceBomb = KeyCode.Space;

	Hashtable buttons;

	public Controls (int player) {

		// Default values
		buttons = new Hashtable ()
		{
			{ "Up", KeyCode.UpArrow },
			{ "Down", KeyCode.DownArrow },
			{ "Left", KeyCode.LeftArrow },
			{ "Right", KeyCode.RightArrow },
			{ "Bomb", KeyCode.Space },
		};

		LoadButtonsFromConfig (player);

		switch (player) {
		case 1:
			Up = KeyCode.UpArrow;
			Down = KeyCode.DownArrow;
			Left = KeyCode.LeftArrow;
			Right = KeyCode.RightArrow;
			PlaceBomb = KeyCode.Space;

			break;
		case 2:
			Up = KeyCode.Keypad8;
			Down = KeyCode.Keypad2;
			Left = KeyCode.Keypad4;
			Right = KeyCode.Keypad6;
			PlaceBomb = KeyCode.Keypad0;

			break;
		case 3:
			// TODO
			break;
		case 4:
			// TODO
			break;
		default:
			break;
		}
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
