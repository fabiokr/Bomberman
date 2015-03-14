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
	
	float lastUpdate;
	Vector3 currentPosition;
	int directionIndex;

	void Start () {
		Debug.Log("HURRY!");
		directionIndex = 3;
		lastUpdate = Time.time;
		currentPosition = GameControllerBehavior.instance.startingPosition;
	}

	void Update () {
		if (Time.time - lastUpdate >= 1f) {
			InstantiateBlock();
			UpdateNextPosition();

			lastUpdate = Time.time;
		}
	}

	void InstantiateBlock() {
		GameObject b = Instantiate(block) as GameObject;
		b.transform.parent = transform.root.Find(Stage.Bricks);
		b.transform.position = currentPosition + new Vector3(0, 10f, 0);
		b.AddComponent<HurryBlockBehavior>();
	}

	void UpdateNextPosition() {
		// if the next one is a block
		if(Util.GetClosest(Tags.Block, currentPosition + DIRS[directionIndex], 0.5f)) {
			// change direction
			directionIndex = NextDirectionIndex();
		}

		currentPosition += DIRS[directionIndex];

		//if(true) {
		//	enabled = false;
		//}
	}

	int NextDirectionIndex() {
		if(directionIndex + 1 > DIRS.Length - 1) {
			return 0;
		} else {
			return directionIndex + 1;
		}
	}
}
