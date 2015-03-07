using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour {
	public GameObject bomberman;

	public float countdown = 4;
	public float power = 1;

	public Detonator explosion_prefab;

	private bool exploded = false;

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

		// To avoid recursion
		if (exploded) {
			return;
		}

		exploded = true;

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
					BombBehavior bombBehavior = hit.transform.GetComponent<BombBehavior> ();

					if (explodableBehavior) {
						explodableBehavior.Explode ();
					}
					else if (bombBehavior) {
						bombBehavior.Explode ();
					}
				}
			}
		}

		BombermanBehavior b = bomberman.GetComponent<BombermanBehavior> ();
		b.RemoveBomb (gameObject);
		Destroy (gameObject);
	}
}
