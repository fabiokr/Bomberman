using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour, ExplodableInterface {
	bool activated = false;

	public Color colorStart = Color.green;
	public Color colorEnd = Color.yellow;
	public float duration = 20.0f;
	Renderer rend;

	void Start () {
		rend = GetComponent<Renderer>();
	}

	void Update () {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);

		Quaternion rotation1 = Quaternion.Euler(new Vector3(50.0f, 0.0f, -10.0f));
		Quaternion rotation2 = Quaternion.Euler(new Vector3(50.0f, 0.0f, 10.0f));
		float t = Mathf.PingPong (Time.time, duration) / duration;
		transform.rotation = Quaternion.Slerp (rotation1, rotation2, t);
	}

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
