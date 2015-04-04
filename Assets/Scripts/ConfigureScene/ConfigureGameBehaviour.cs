using UnityEngine;
using System.Collections;

public class ConfigureGameBehaviour : MonoBehaviour {
	
	void Start () {
	
	}

	void OnGUI () {
		Event e = Event.current;
		if (e.isKey)
			Debug.Log("Detected key code: " + e.keyCode);
	}

	public void SetUpButton() {

	}
}
