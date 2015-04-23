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
		return Input.GetAxis (vertical) > 0.5 && isPredominant(vertical, horizontal);
	}

	public bool getDown () {
		return Input.GetAxis (vertical) < -0.5 && isPredominant(vertical, horizontal);
	}

	public bool getLeft () {
		return Input.GetAxis (horizontal) < -0.5 && isPredominant(horizontal, vertical);
	}

	public bool getRight () {
		return Input.GetAxis (horizontal) > 0.5 && isPredominant(horizontal, vertical);
	}

	public bool getPlaceBomb () {
		return Input.GetAxis (bomb) != 0;
	}

	private bool isPredominant(string axisA, string axisB) {
		return Mathf.Abs(Input.GetAxis (axisA)) > Mathf.Abs(Input.GetAxis (axisB));
	}
}
