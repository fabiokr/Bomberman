using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	public float countdown = 4;
	public float power = 1;

	public Detonator explosion_prefab;

	static Vector3[] DIRS = {
	  Vector3.forward,
	  Vector3.right,
      Vector3.back,
      Vector3.left
	};

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
		// Explosion effect
		Instantiate (explosion_prefab, gameObject.transform.position, Quaternion.identity);

		for (int i = 1; i <= power; i++) {
			foreach (Vector3 dir in DIRS) {
				RaycastHit hit;

				Debug.Log ("Raycast to " + dir.ToString() + " with power " + i);

				Debug.r

				if (Physics.Raycast (transform.position, dir, out hit, i)) {
					Debug.Log ("Hit " + hit.transform.gameObject.tag);

					ExplodableBehavior explodableBehavior = hit.transform.GetComponent<ExplodableBehavior> ();

					if (explodableBehavior) {
						explodableBehavior.Explode ();
					}
				}
			}
		}

		BombermanBehavior b = bomberman.GetComponent<BombermanBehavior> ();
		b.RemoveBomb (gameObject);
		Destroy (gameObject);
	}
}
