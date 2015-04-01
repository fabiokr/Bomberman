using UnityEngine;
using System.Collections;

public class BombAnimation : MonoBehaviour {
	public float animationLength = 1f;
	float timer = 0f;
	Vector3 initialScale, halfInitialScale;

	void Start() {
		initialScale = transform.localScale;
		halfInitialScale = new Vector3 (initialScale.x / 2, initialScale.y / 2, initialScale.z / 2);
	}

	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;

		if (timer < animationLength / 2) {
			transform.localScale = Vector3.Lerp(transform.localScale, halfInitialScale, 0.5f * Time.deltaTime);
		} else if ((timer > animationLength / 2) && timer < animationLength) {
			transform.localScale = Vector3.Lerp(transform.localScale, initialScale, 0.5f * Time.deltaTime);
		} else {
			timer = 0f;
		}
	}
}
