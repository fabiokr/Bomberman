using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour, ExplodableInterface {
	bool activated = false;

	public void Explode() {
		// To avoid recursion
		gameObject.layer = Layers.IgnoreRaycast;

		Destroy (gameObject);
	}

	protected BombermanBehavior GetBombermanBehavior(Collider other) {
		BombermanBehavior b = other.gameObject.GetComponent<BombermanBehavior> ();

		// only return the Behavior once, otherwise if there are multiple
		// colliders they will all trigger the collider event
		if (b && !activated) {
			activated = true;
			Destroy (gameObject);
			return b;
		} else {
			return null;
		}
	}
}
