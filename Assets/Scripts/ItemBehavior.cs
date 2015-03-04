using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour {
	bool activated = false;

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
