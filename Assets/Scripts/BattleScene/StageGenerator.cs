using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour {
	public GameObject ground, block, destructibleBlock, player;
	public int size = 15;
	public int nPlayers = 2;
	public float itemFrequency = 0.1f, emptyBlocksFrequency = 0.1f;
	Renderer rend;

	public List<GameObject> items;

	int players = 0;
	List<Vector3> playersPositions;

	// Use this for initialization
	void Start () {
		if (size % 2 == 0) {
			size++;
		}

		float offset = (size / 2f) - 0.5f;
		playersPositions = new List<Vector3> ();

		for(int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++) {
				Vector3 localPosition = transform.position + new Vector3(i - offset, 0, j - offset);

				// Ground
				GameObject g = Instantiate (ground) as GameObject;
				g.transform.localScale = new Vector3(1, 1, 1);
				g.transform.parent = transform.root.Find (Stage.Grounds);
				g.transform.position = localPosition;

				// Blocks
				int x = Mathf.Abs(j - (int)offset);
				int y = Mathf.Abs(i - (int)offset);

				GameObject b = null;

				if(x == offset - 1 && y == offset - 1) {
					playersPositions.Add(new Vector3(localPosition.x, 0.0f, localPosition.z));

					if(GameControllerBehavior.instance.startingPosition == new Vector3(0, 0, 0)) {
						GameControllerBehavior.instance.startingPosition = localPosition;
					}
				} else if((x == offset - 1 && y == offset - 1) || (x == offset - 2 && y == offset - 1) || (x == offset - 1 && y == offset - 2)) {
					// empty
				} else if(j == 0 || i == 0 || i == size - 1 || j == size - 1 || (i % 2 == 0 && j % 2 == 0)) {
					b = Instantiate(block) as GameObject;
					b.transform.parent = transform.Find(Stage.Bricks);
					b.transform.position = new Vector3(localPosition.x, 0.5f, localPosition.z);
				} else {
					if(Random.value >= emptyBlocksFrequency) {
						b = Instantiate(destructibleBlock) as GameObject;
						b.transform.parent = transform.Find(Stage.Bricks);
						b.transform.position = new Vector3(localPosition.x, 0.5f, localPosition.z);

						InstantiateItem(localPosition);
					}
				}
			}
		}

		SetupPlayers ();
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(transform.position.x - HalfSize(), transform.position.y, transform.position.z + HalfSize()), new Vector3(transform.position.x + HalfSize(), transform.position.y, transform.position.z + HalfSize()));
		Gizmos.DrawLine(new Vector3(transform.position.x - HalfSize(), transform.position.y, transform.position.z - HalfSize()), new Vector3(transform.position.x + HalfSize(), transform.position.y, transform.position.z - HalfSize()));
		Gizmos.DrawLine(new Vector3(transform.position.x - HalfSize(), transform.position.y, transform.position.z - HalfSize()), new Vector3(transform.position.x - HalfSize(), transform.position.y, transform.position.z + HalfSize()));
		Gizmos.DrawLine(new Vector3(transform.position.x + HalfSize(), transform.position.y, transform.position.z + HalfSize()), new Vector3(transform.position.x + HalfSize(), transform.position.y, transform.position.z - HalfSize()));
	}
	
	private void InstantiateItem(Vector3 position) {
		if (items.Count > 0 && Random.value <= itemFrequency) {
			GameObject item = Instantiate (RandomItem ()) as GameObject;
			item.transform.parent = transform.Find (Stage.Items);
			item.transform.position = new Vector3 (position.x, 0.5f, position.z);
		}
	}

	private GameObject RandomItem() {
		return items[(int)Random.Range(0, items.Count)];
	}

	private float HalfSize() {
		return size / 2f;
	}

	private void SetupPlayers() {
		List<Vector3> orderedPlayersPositions = new List<Vector3>();

		// reorders positions
		orderedPlayersPositions.Add (playersPositions [1]);
		orderedPlayersPositions.Add (playersPositions [3]);
		orderedPlayersPositions.Add (playersPositions [0]);
		orderedPlayersPositions.Add (playersPositions [2]);

		GameObject b = null;

		foreach (Vector3 position in orderedPlayersPositions) {
			if(players < nPlayers) {
				b = Instantiate(player) as GameObject;
				
				BombermanBehavior bb = b.GetComponent<BombermanBehavior> ();
				bb.player_number = players + 1;
				
				b.name = "Player " + bb.player_number;
				b.transform.parent = transform.Find(Stage.Players);
				b.transform.position = position;
				
				// Setup player clothes
				rend = b.GetComponentInChildren<Renderer> ();
				rend.materials = BombermanColors.GetBombermanColors(players, rend.materials);
				
				players++;
			}
		}
	}
}
