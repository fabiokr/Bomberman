using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<BombermanBehavior> ()) {
			Destroy(gameObject);
		}
	}
}
