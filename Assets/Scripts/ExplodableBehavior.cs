using UnityEngine;
using System.Collections;

public class ExplodableBehavior : MonoBehaviour {
	public void Explode() {
		rigidbody.isKinematic = false;
		rigidbody.AddForce(Vector3.up * 40, ForceMode.Impulse);
		Destroy (gameObject, 3.0f);
	}
}
