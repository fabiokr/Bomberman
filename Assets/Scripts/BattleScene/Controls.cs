using UnityEngine;
using System.Collections;

public class Controls {
	private string horizontal, vertical, bomb;

	public Controls (string name) {
		horizontal = name + " Horizontal";
		vertical = name + " Vertical";
		bomb = name + " Bomb";
	}

	public bool getUp () {
		return Input.GetAxis (vertical) > 0;
	}

	public bool getDown () {
		return Input.GetAxis (vertical) < 0;
	}

	public bool getLeft () {
		return Input.GetAxis (horizontal) < 0;
	}

	public bool getRight () {
		return Input.GetAxis (horizontal) > 0;
	}

	public bool getPlaceBomb () {
		return Input.GetAxis (bomb) != 0;
	}
}
