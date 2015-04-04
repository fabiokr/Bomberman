using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class HurryBlockBehavior : MonoBehaviour {
	public AudioClip blockClip;

	AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		gameObject.GetComponent<Collider> ().isTrigger = true;
	}
	
	void Update () {
		transform.position = new Vector3(
			transform.position.x, 
			transform.position.y - 30f * Time.deltaTime, 
			transform.position.z);

		if (transform.position.y < 0.5f) {
			transform.position = new Vector3(
				transform.position.x, 
				0.5f, 
				transform.position.z);

			audioSource.clip = blockClip;
			audioSource.Play();

			enabled = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		BombermanBehavior bBehavior = other.gameObject.GetComponent<BombermanBehavior> ();

		if (bBehavior) {
			bBehavior.Die();
		}
	}
}
