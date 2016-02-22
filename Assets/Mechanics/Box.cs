using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public float respawnDelay = 1;
	Vector2 spawnPos;
	AudioSource boxHit;
	Rigidbody2D boxBody;
	float speed;

	// Use this for initialization
	void Start () {
		spawnPos = transform.position;
		boxHit = GetComponent<AudioSource>();
		boxBody = GetComponent<Rigidbody2D>();
	}

	public IEnumerator Respawn(){
		yield return new WaitForSeconds(respawnDelay);
		transform.position = spawnPos;
	}

	void FixedUpdate(){
		speed = boxBody.velocity.sqrMagnitude;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag != "Player"){
			if (speed > 250f){
				if (!boxHit.isPlaying) {
					boxHit.Play ();
				}
			}
		}
	}
}
