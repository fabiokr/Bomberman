using UnityEngine;
using System.Collections;

public class ExplodableBehavior : MonoBehaviour {
	public void Explode() {
		Destroy (gameObject);
	}
}
