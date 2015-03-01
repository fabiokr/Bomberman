using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	void Start() {
		PositionBombOnGroundCenter ();
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == bomberman) {
			collider.isTrigger = false;
		}
	}

	private void PositionBombOnGroundCenter() {
		transform.position = GetClosestGround().position;
	}

	private Transform GetClosestGround() {
		Transform[] grounds = transform.root.Find ("Grounds").transform.GetComponentsInChildren<Transform>();

		Transform closest = null;
		float closestMagnitude = Mathf.Infinity;

		foreach (Transform t in grounds) {
			float m = (t.position - transform.position).sqrMagnitude;

			if(m < closestMagnitude) {
				closest = t;
				closestMagnitude = m;
			}
		}

		return closest;
	}
}
