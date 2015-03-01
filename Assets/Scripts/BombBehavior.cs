using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	void OnTriggerExit(Collider other) {
		if (other.gameObject == bomberman) {
			collider.isTrigger = false;
		}
	}
}
