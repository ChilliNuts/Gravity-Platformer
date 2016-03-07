﻿using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public float respawnDelay = 1;
	public float pushForce;
	Vector2 spawnPos;
	AudioSource boxHit;
	Rigidbody2D boxBody;
	BoxCollider2D box;
	PlayerController player;
	float speed;

	// Use this for initialization
	void Start () {
		spawnPos = transform.position;
		boxHit = GetComponent<AudioSource>();
		boxBody = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		player = FindObjectOfType<PlayerController>();
	}

	public IEnumerator Respawn(){
		yield return new WaitForSeconds(respawnDelay);
		transform.position = spawnPos;
	}

	void FixedUpdate(){
		speed = boxBody.velocity.sqrMagnitude;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject != player.gameObject){
			if (speed > 250f){
				if (!boxHit.isPlaying) {
					boxHit.Play ();
				}
			}
		}
		if(col.gameObject == player.gameObject && !IsNotOnBox()){
			Vector2 force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 0.5f;
			float pushDamp;
			if(player.magnetised){
				pushDamp = 4f;
			}else pushDamp = 1f;

			Vector2 contactPoint = new Vector2(transform.position.x, transform.position.y -0.5f);
			boxBody.AddForceAtPosition(force * DampenPush(pushForce, pushDamp), contactPoint, ForceMode2D.Impulse);
		}
	}
	bool IsNotOnBox(){
		return (box.bounds.Contains(player.groundedTarget1.transform.position) || box.bounds.Contains(player.groundedTarget2.transform.position));
	}
	float DampenPush(float value, float divide){
		return value/divide;
	}
}