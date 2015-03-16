using UnityEngine;
using System.Collections;

public class HurryBlockBehavior : MonoBehaviour {
	void Start() {
		gameObject.GetComponent<Collider> ().isTrigger = true;
	}
	
	void Update () {
		transform.position = new Vector3(
			transform.position.x, 
			transform.position.y - 30f * Time.deltaTime, 
			transform.position.z);

		if (transform.position.y < 0.5f) {
			transform.position = new Vector3(
				transform.position.x, 
				0.5f, 
				transform.position.z);

			enabled = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = other.gameObject.GetComponent<BombermanBehavior> ();

		if (bBehavior) {
			bBehavior.Die();
		}
	}
}
