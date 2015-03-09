using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class BombermanBehavior : MonoBehaviour, ExplodableInterface {
	public Boundary boundary;
	public GameObject bomb;
	public float speed = 2f, maxSpeed = 5f, bombSpeed = 4.5f, minBombSpeed = 3f;
	public int bombLimit = 1, bombPower = 1, hp = 1, maxHp = 2;

	StageGenerator stageGenerator;

	List<GameObject> bombs;

	void Start() {
		bombs = new List<GameObject> ();
		stageGenerator = GameObject.FindGameObjectWithTag(Tags.Stage).GetComponent<StageGenerator>();
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
			if(bombs.Count < bombLimit && !HasBomb()) {
				GameObject b = Instantiate(bomb, GetGround().transform.position, Quaternion.identity) as GameObject;
				b.transform.parent = transform.root.Find(Stage.Bombs);
				bombs.Add(b);

				BombBehavior bBehavior = b.GetComponent<BombBehavior>();
				bBehavior.bomberman = gameObject;
				bBehavior.power = bombPower;
				bBehavior.timer = bombSpeed;
			}
		}
	}

	public void AddBombLimit() {
		if (bombLimit + 1 <= stageGenerator.size) {
			bombLimit += 1;
		}
	}

	public void AddBombPower() {
		if (bombPower + 1 <= stageGenerator.size) {
			bombPower += 1;
		}
	}

	public void AddSpeed() {
		if (speed + 0.5f <= maxSpeed) {
			speed += 0.5f;
		}
	}

	public void AddHp() {
		if (hp + 1 <= maxHp) {
			hp += 1;
		}
	}

	public void DecreaseBombSpeed() {
		if (bombSpeed - 1 >= minBombSpeed) {
			bombSpeed -= 1;
		}
	}

	public void Hit() {
		hp -= 1;

		if (hp <= 0) {
			Die ();
		}
	}

	public void Die() {
		Destroy (gameObject);
	}

	public void Explode() {
		// To avoid recursion
		gameObject.layer = Layers.IgnoreRaycast;

		Hit ();
	}

	public void LateUpdate() {
		// Restore to default layer
		gameObject.layer = Layers.Default;
	}

	private GameObject GetGround() {
		GameObject[] grounds = GameObject.FindGameObjectsWithTag (Tags.Ground);
		
		GameObject closest = null;
		float closestMagnitude = Mathf.Infinity;
		
		foreach (GameObject g in grounds) {
			float m = (g.transform.position - transform.position).sqrMagnitude;
			
			if (m < closestMagnitude) {
				closest = g;
				closestMagnitude = m;
			}
		}
		
		return closest;
	}

	private bool HasBomb() {
		return GetGround().GetComponent<GroundBehavior>().HasBomb();
	}

	public void RemoveBomb(GameObject b) {
		bombs.Remove (b);
	}
}
