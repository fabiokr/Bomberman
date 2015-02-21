using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class BombermanBehavior : MonoBehaviour {
	string HORIZONTAL = "Horizontal";
	string VERTICAL = "Vertical";

	public float speed;
	public Boundary boundary;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis (HORIZONTAL);
		float moveVertical = Input.GetAxis (VERTICAL);

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
			Move(vLook);
		} else if(rigidbody.velocity.magnitude > 0) {
			rigidbody.velocity = Vector3.zero;
		}
	}

	void Move(Vector3 vLook) {
		transform.eulerAngles = vLook;
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
