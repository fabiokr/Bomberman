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
	public float speed = 0.05f;
	
	float lastUpdate;
	Vector3 currentPosition;
	int directionIndex, pathSize, currentPathSize, currentTurn;

	void Start () {
		Debug.Log("HURRY!");
		directionIndex = 3;
		pathSize = GameControllerBehavior.instance.stageGenerator.size - 3;
		currentPathSize = pathSize;
		currentTurn = 3;
		lastUpdate = Time.time;
		currentPosition = GameControllerBehavior.instance.startingPosition;
	}

	void Update () {
		if (Time.time - lastUpdate >= speed) {
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
		currentPosition += DIRS[directionIndex];
		currentPathSize -= 1;

		if(currentPathSize <= 0) {
			// change direction
			directionIndex = NextDirectionIndex();
			
			currentTurn -= 1;

			if(currentTurn == 0) {
				pathSize -= 1;
				currentTurn = 2;
			}

			if(pathSize <= 0 && currentPathSize <= 0 && currentTurn == 1) {
				enabled = false;
			}
			
			currentPathSize = pathSize;

		}
	}

	int NextDirectionIndex() {
		if(directionIndex + 1 > DIRS.Length - 1) {
			return 0;
		} else {
			return directionIndex + 1;
		}
	}
}
