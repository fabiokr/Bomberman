using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class BombermanBehavior : MonoBehaviour {
	public float speed;
	public Boundary boundary;
	public GameObject bomb;
	public int bombLimit = 1;

	List<GameObject> bombs;

	void Start() {
		bombs = new List<GameObject> ();
	}

	void FixedUpdate ()
	{
		Move ();
		PlaceBomb ();
	}

	void Move() {
		float moveHorizontal = Input.GetAxis (Controls.Horizontal);
		float moveVertical = Input.GetAxis (Controls.Vertical);
		
		Vector3 vLook = transform.eulerAngles;
		bool move = false;
		
		if(moveVertical > 0) {
			vLook.y = 0.0f;
			move = true;
		} else if(moveHorizontal > 0) {
			vLook.y = 90.0f;
			move = true;
		} else if(moveVertical < 0) {
			vLook.y = 180.0f;
			move = true;
		} else if(moveHorizontal < 0) {
			vLook.y = 270.0f;
			move = true;
		}  
		
		if(move) {
			transform.eulerAngles = vLook;
			transform.position += transform.forward * speed * Time.deltaTime;
		} else if(rigidbody.velocity.magnitude > 0) {
			rigidbody.velocity = Vector3.zero;
		}
	}

	void PlaceBomb() {
		if (Input.GetKeyUp (Controls.Bomb)) {
			if(bombs.Count < bombLimit) {
				Vector3 bPosition = new Vector3(transform.position.x, 0f, transform.position.z);
				GameObject b = Instantiate(bomb, bPosition, Quaternion.identity) as GameObject;
				b.GetComponent<BombBehavior>().bomberman = gameObject;
				b.transform.parent = transform.root.Find(Stage.Bombs);
				bombs.Add(b);
			}
		}
	}
}
