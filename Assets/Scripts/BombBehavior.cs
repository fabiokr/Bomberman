﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour, ExplodableInterface {
	public GameObject bomberman;

	public float timer = 4.5f;
	public float power = 1f;

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
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Explode();
		}
	}

	public void Explode() {
		// To avoid recursion
		gameObject.layer = Layers.IgnoreRaycast;

		// Explosion effect
		Instantiate (explosion_prefab, gameObject.transform.position, Quaternion.identity);

		Ray ray;
		RaycastHit hit;

		// Current postion hit check from a revesal ray
		ray = new Ray(new Ray(transform.position, Vector3.up).GetPoint(5), Vector3.down);

		if (Physics.Raycast (ray, out hit, 10)) {
			ExplosionHit(hit);
		}

		// Directional positions hit checks
		for (int i = 1; i <= power; i++) {
			foreach (Vector3 dir in DIRS) {
				ray = new Ray(transform.position, dir);
			
				if (Physics.Raycast (ray, out hit, i)) {
					ExplosionHit(hit);
				} else {
					// Explosion fire effect
					GameObject fire = Instantiate (explosion_fire_prefab, transform.position + (dir * i), Quaternion.identity) as GameObject;
					Destroy (fire, 3.0f); // Auto destroy after 3 seconds
				}
			}
		}

		if (bomberman) {
			BombermanBehavior b = bomberman.GetComponent<BombermanBehavior> ();
			b.RemoveBomb (gameObject);
		}

		Destroy (gameObject);
	}

	void ExplosionHit(RaycastHit hit) {
		Debug.Log("Hit:" + hit.transform.gameObject);
		
		ExplodableInterface explodable = (ExplodableInterface)hit.transform.GetComponent (typeof(ExplodableInterface));
		
		if (explodable != null) {
			Debug.Log("Explodable:" + explodable);
			explodable.Explode ();
		}
	}
}
