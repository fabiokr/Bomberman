using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour, ExplodableInterface {
	public GameObject bomberman;

	public float countdown = 4;
	public float power = 1;

	public Detonator explosion_prefab;
	public GameObject explosion_fire_prefab;

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

	public void Explode() {
		// To avoid recursion
		gameObject.layer = Layers.IgnoreRaycast;

		// Explosion effect
		Instantiate (explosion_prefab, gameObject.transform.position, Quaternion.identity);

		for (int i = 1; i <= power; i++) {
			foreach (Vector3 dir in DIRS) {
				RaycastHit hit;

				if (Physics.Raycast (transform.position, dir, out hit, i)) {
					ExplodableInterface explodable = (ExplodableInterface)hit.transform.GetComponent (typeof(ExplodableInterface));

					if (explodable != null) {
						Debug.Log("Explodable:" + explodable);
						explodable.Explode ();
					}
				}
				else {
					// Explosion fire effect
					GameObject fire = Instantiate (explosion_fire_prefab, transform.position + (dir * i), Quaternion.identity) as GameObject;
					Destroy (fire, 3.0f); // Auto destroy after 3 seconds
				}
			}
		}

		BombermanBehavior b = bomberman.GetComponent<BombermanBehavior> ();
		b.RemoveBomb (gameObject);
		Destroy (gameObject);
	}
}
