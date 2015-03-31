using UnityEngine;
using System.Collections;

public class Util {
	public static GameObject GetClosest(string tag, Vector3 position) {
		return GetClosest (tag, position, Mathf.Infinity);
	}

	public static GameObject GetClosest(string tag, Vector3 position, float closestMagnitude) {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (tag);
		GameObject closest = null;
		
		foreach (GameObject g in gameObjects) {
			float m = (g.transform.position - position).sqrMagnitude;

			if (m < closestMagnitude) {
				closest = g;
				closestMagnitude = m;
			}
		}

		return closest;
	}
}
