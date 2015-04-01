using UnityEngine;
using System.Collections;

public class ItemSkateBehavior : ItemBehavior {
	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = GetBombermanBehavior(other);
		
		if (bBehavior) {
			bBehavior.AddSpeed();
		}
	}
}
