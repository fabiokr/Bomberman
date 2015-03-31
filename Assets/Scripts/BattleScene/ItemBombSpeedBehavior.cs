using UnityEngine;
using System.Collections;

public class ItemBombSpeedBehavior : ItemBehavior {
	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = GetBombermanBehavior(other);
		
		if (bBehavior) {
			bBehavior.DecreaseBombSpeed();
		}
	}
}
