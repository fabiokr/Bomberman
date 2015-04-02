using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class BombermanBehavior : MonoBehaviour, ExplodableInterface {
	public Boundary boundary;
	public GameObject bomb;
	public float speed = 2f, maxSpeed = 5f, bombSpeed = 4.5f, minBombSpeed = 3f;
	public int bombLimit = 1, bombPower = 1, hp = 1, maxHp = 2;
	public int player_number;
	public Controls controls;
	public AudioClip catchItem, dieAudioClip;

	StageGenerator stageGenerator;

	List<GameObject> bombs;

	void Start() {
		bombs = new List<GameObject> ();
		stageGenerator = GameObject.FindGameObjectWithTag(Tags.Stage).GetComponent<StageGenerator>();
		controls = new Controls (player_number);
		animation ["Idle"].speed = 1.0f;
		animation ["Walk"].speed = 3.0f;
	}

	void FixedUpdate ()
	{
		if (GameControllerBehavior.instance.gameInProgress) {
			Move ();
			PlaceBomb ();
		}
	}

	void Move() {
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;
		
		Vector3 vLook = transform.eulerAngles;
		bool move = false;
		
		if(controls.getUp()) {
			vLook.y = 90.0f;
			move = true;
		} else if(controls.getRight()) {
			vLook.y = 180.0f;
			move = true;
		} else if(controls.getDown()) {
			vLook.y = 270.0f;
			move = true;
		} else if(controls.getLeft()) {
			vLook.y = 0.0f;
			move = true;
		}  
		
		if (move) {
			transform.eulerAngles = vLook;
			transform.position -= transform.right * speed * Time.deltaTime;
			animation.CrossFade ("Walk");
		} else if (rigidbody.velocity.magnitude > 0) {
			rigidbody.velocity = Vector3.zero;
		} else {
			animation.CrossFade ("Idle");
		}
	}

	void PlaceBomb() {
		if (controls.getPlaceBomb()) {
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

	public void PlayCatchItemSound () {
		audio.PlayOneShot (catchItem);
	}

	public void PlayDieSound () {
		audio.PlayOneShot (dieAudioClip);
	}

	public void AddBombLimit() {
		PlayCatchItemSound ();
		if (bombLimit + 1 <= stageGenerator.size) {
			bombLimit += 1;
		}
	}

	public void AddBombPower() {
		PlayCatchItemSound ();
		if (bombPower + 1 <= stageGenerator.size) {
			bombPower += 1;
		}
	}

	public void AddSpeed() {
		PlayCatchItemSound ();
		if (speed + 0.5f <= maxSpeed) {
			speed += 0.5f;
		}
	}

	public void AddHp() {
		PlayCatchItemSound ();
		if (hp + 1 <= maxHp) {
			hp += 1;
		}
	}

	public void DecreaseBombSpeed() {
		PlayCatchItemSound ();
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
		PlayDieSound ();
		Destroy (gameObject, 1.2f);
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
		return Util.GetClosest (Tags.Ground, transform.position);
	}

	private bool HasBomb() {
		return GetGround().GetComponent<GroundBehavior>().HasBomb();
	}

	public void RemoveBomb(GameObject b) {
		bombs.Remove (b);
	}
}
