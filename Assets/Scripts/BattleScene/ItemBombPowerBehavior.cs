using UnityEngine;
using System.Collections;

public class ItemBombPowerBehavior : ItemBehavior {
	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = GetBombermanBehavior(other);

		if (bBehavior) {
			bBehavior.AddBombPower();
		}
	}
}
