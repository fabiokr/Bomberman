using UnityEngine;
using System.Collections;

public class Controls {
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	public KeyCode PlaceBomb = KeyCode.Space;

	public Controls (int player) {
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

	public bool getUp () {
		return Input.GetKey (Up);
	}

	public bool getDown () {
		return Input.GetKey (Down);
	}

	public bool getLeft () {
		return Input.GetKey (Left);
	}

	public bool getRight () {
		return Input.GetKey (Right);
	}

	public bool getPlaceBomb () {
		return Input.GetKeyUp (PlaceBomb);
	}
}
