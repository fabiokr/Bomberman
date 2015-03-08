using UnityEngine;
using System.Collections;

public class DestructibleBlockBehavior : MonoBehaviour, ExplodableInterface {
	public void Explode() {
		rigidbody.isKinematic = false;
		rigidbody.AddForce(Vector3.up * 40, ForceMode.Impulse);
		Destroy (gameObject, 3.0f);
	}
}
