using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	public float countdown = 4;

	void Start() {
		PositionBombOnGroundCenter ();
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == bomberman) {
			collider.isTrigger = false;
		}
	}

	void FixedUpdate() {
		countdown -= Time.deltaTime;

		if (countdown <= 0) {
			Explode();
		}
	}

	private void Explode() {
		Destroy (gameObject);
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
