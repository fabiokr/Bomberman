using UnityEngine;
using System.Collections;

public class ItemBombBehavior : ItemBehavior {
	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = GetBombermanBehavior(other);

		if (bBehavior) {
			bBehavior.AddBombLimit();
		}
	}
}
