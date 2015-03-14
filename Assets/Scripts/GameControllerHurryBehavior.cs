using UnityEngine;
using System.Collections;

public class GameControllerHurryBehavior : MonoBehaviour {
	static Vector3[] DIRS = {
		Vector3.right,
		Vector3.back,
		Vector3.left,
		Vector3.forward
	};

	public GameObject block;

	bool finished = false;
	float lastUpdate;
	Vector3 direction, currentPosition;

	void Start () {
		Debug.Log("HURRY!");
		direction = Vector3.forward;
		lastUpdate = Time.time;
		currentPosition = GameControllerBehavior.instance.startingPosition;
		currentPosition = new Vector3 (currentPosition.x, 10f, currentPosition.z);
	}

	void Update () {
		if (!finished && Time.time - lastUpdate >= 1f) {
			GameObject b = Instantiate(block) as GameObject;
			b.transform.parent = transform.root.Find(Stage.Bricks);
			b.transform.position = currentPosition;
			b.AddComponent<HurryBlockBehavior>();

			lastUpdate = Time.time;
			finished = true;
		}
	}
}
