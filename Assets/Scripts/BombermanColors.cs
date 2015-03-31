using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombermanColors : MonoBehaviour {

	public static Material[] GetBombermanColors (int player, Material[] orig_materials) {
		Material[][] materials = new Material[4][]; // Up to 4 players

		for (int i = 0; i < materials.Length; i++) {
			materials[i] = new Material[orig_materials.Length];

			for (int j = 0; j < orig_materials.Length; j++) {
				materials[i][j] = new Material(orig_materials[j]);

				Debug.Log(materials[i][j]);
			}
		}

		// Player 1
//		materials [0] [0].color = new Color (0.218f, 0.126f, 0.904f); // Torso
//		materials [0] [1].color = new Color (1.0f, 1.0f, 1.0f); // Helmet
//		materials [0] [2].color = new Color (0.800f, 0.224f, 0.505f); // Face
//		materials [0] [3].color = new Color (1.0f, 1.0f, 1.0f); // Hands
//		materials [0] [4].color = new Color (1.0f, 0.877f, 0.191f); // Eyes
//		materials [0] [5].color = new Color (0.0f, 0.0f, 0.0f); // Arms
//		materials [0] [6].color = new Color (0.0f, 0.0f, 0.0f); // Belt
//		materials [0] [7].color = new Color (0.800f, 0.472f, 0.224f); // Base
//		materials [0] [8].color = new Color (0.800f, 0.472f, 0.224f); // Buckle

		// Player 2
		materials [1] [0].color = new Color (0.0f, 0.0f, 0.0f); // Torso
		materials [1] [1].color = new Color (0.0f, 0.0f, 0.0f); // Helmet
//		materials [1] [2].color = new Color (0.800f, 0.224f, 0.505f); // Face
//		materials [1] [3].color = new Color (0.800f, 0.224f, 0.505f); // Hands
//		materials [1] [4].color = new Color (1.0f, 0.877f, 0.191f); // Eyes
		materials [1] [5].color = new Color (0.800f, 0.472f, 0.224f); // Arms
		materials [1] [6].color = new Color (0.218f, 0.126f, 0.904f); // Belt
//		materials [1] [7].color = new Color (0.800f, 0.472f, 0.224f); // Base
//		materials [1] [8].color = new Color (1.0f, 0.877f, 0.191f); // Buckle

		return materials [player];
	}
}
