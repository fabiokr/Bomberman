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

		GameObject g = Instantiate (ground) as GameObject;
		g.transform.localScale = new Vector3(size, size, 1);
		g.transform.parent = transform;
		g.transform.position = transform.position;

		float offset = (size / 2f) - 0.5f;

		for(int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++) {
				int x = Mathf.Abs(j - (int)offset);
				int y = Mathf.Abs(i - (int)offset);
				float heightOffset = 0.5f;

				GameObject b = null;

				if(x == offset - 1 && y == offset - 1) {
					if(players < nPlayers) {
						b = Instantiate(player) as GameObject;
						b.transform.parent = transform.Find("Players");
						heightOffset = 0.9f;
						players++;
					}
				} else if((x == offset - 1 && y == offset - 1) || (x == offset - 2 && y == offset - 1) || (x == offset - 1 && y == offset - 2)) {
					// empty
				} else if(j == 0 || i == 0 || i == size - 1 || j == size - 1 || (i % 2 == 0 && j % 2 == 0)) {
					b = Instantiate(block) as GameObject;
					b.transform.parent = transform.Find("Bricks");
				} else {
					b = Instantiate(destructibleBlock) as GameObject;
					b.transform.parent = transform.Find("Bricks");
				}

				if(b) {
					b.transform.position = transform.position + new Vector3(i - offset, heightOffset, j - offset);
				}
			}
		}
	}
}
