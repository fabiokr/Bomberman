using UnityEngine;
using System.Collections;

public class StageGenerator : MonoBehaviour {

	public GameObject ground, block, destructibleBlock, player;
	public int size = 15;
	public int nPlayers = 1;

	int players = 0;

	// Use this for initialization
	void Start () {
		if (size % 2 == 0) {
			size++;
		}

		float offset = (size / 2f) - 0.5f;

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
					if(players < nPlayers) {
						b = Instantiate(player) as GameObject;
						b.transform.parent = transform.Find(Stage.Players);
						b.transform.position = new Vector3(localPosition.x, 0.9f, localPosition.z);
						players++;
					}
				} else if((x == offset - 1 && y == offset - 1) || (x == offset - 2 && y == offset - 1) || (x == offset - 1 && y == offset - 2)) {
					// empty
				} else if(j == 0 || i == 0 || i == size - 1 || j == size - 1 || (i % 2 == 0 && j % 2 == 0)) {
					b = Instantiate(block) as GameObject;
					b.transform.parent = transform.Find(Stage.Bricks);
					b.transform.position = new Vector3(localPosition.x, 0.5f, localPosition.z);
				} else {
					b = Instantiate(destructibleBlock) as GameObject;
					b.transform.parent = transform.Find(Stage.Bricks);
					b.transform.position = new Vector3(localPosition.x, 0.5f, localPosition.z);
				}
			}
		}
	}
}
