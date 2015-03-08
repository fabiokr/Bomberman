using UnityEngine;
using System.Collections;

public class ItemHpBehavior : ItemBehavior {
	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = GetBombermanBehavior(other);
		
		if (bBehavior) {
			bBehavior.AddHp();
		}
	}
}
