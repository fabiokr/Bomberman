using UnityEngine;
using System.Collections;

public class GroundBehavior : MonoBehaviour {
	public bool HasBomb() {
		Bounds bounds = collider.bounds;

		foreach(GameObject b in GameObject.FindGameObjectsWithTag (Tags.Bomb)) {
			if(bounds.Contains(b.transform.position)) {
				return true;
			}
		}

		return false;
	}
}
