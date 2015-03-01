using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	public float countdown = 4;
	public float power = 1;

	static Vector3[] DIRS = {
	  Vector3.forward,
	  Vector3.right,
      Vector3.back,
      Vector3.left
	};

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
		foreach (Vector3 dir in DIRS) {
			RaycastHit hit;

			if(Physics.Raycast (transform.position, dir, out hit, power)) {
				Debug.Log("Hit " + hit.transform.gameObject.name);

				BlockBehavior blockBehavior = hit.transform.GetComponent<BlockBehavior>();

				if(blockBehavior && blockBehavior.destructible) {
					Destroy (blockBehavior.gameObject);
				}
			}
		}

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
