﻿using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public float respawnDelay;
	public float pushForce;
	Vector2 spawnPos;
	AudioSource boxHit;
	Rigidbody2D boxBody;
	BoxCollider2D box;
	PlayerController player;
	float speed;
	float bounciness;
	public bool destroyed;
	public bool isHeld;
	ParticleSystem orb;
	bool onMovingPlatform;

	// Use this for initialization
	void Start () {
		spawnPos = transform.position;
		boxHit = GetComponent<AudioSource>();
		boxBody = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		player = FindObjectOfType<PlayerController>();
		bounciness = box.sharedMaterial.bounciness;
		orb = GetComponentInChildren<ParticleSystem>();
		orb.Stop();
	}

	public IEnumerator Respawn(){
		if(isHeld){
			orb.Clear();
			FindObjectOfType<Gun>().DropBox(gameObject);
		}
		if (destroyed == false) {
			yield return new WaitForSeconds (respawnDelay);
			destroyed = true;
		}
	}
	public void Reset(){
		destroyed = false;
		transform.position = spawnPos;
		transform.rotation = Quaternion.identity;
		boxBody.angularVelocity = 0;
		boxBody.velocity = Vector2.zero;
	}
	void Update(){
		if(isHeld && orb.isStopped){
			orb.Play();
		}else if(!isHeld && orb.isPlaying){
			orb.Stop();
		}
	}

	void FixedUpdate(){
		speed = boxBody.velocity.sqrMagnitude;
	}
		
	void OnCollisionExit2D(Collision2D col){
		if (transform.parent != null && !isHeld) {
			transform.parent = null;
			box.sharedMaterial.bounciness = bounciness;
			onMovingPlatform = false;
		}
	}

	void OnCollisionStay2D(Collision2D col){

		if (!onMovingPlatform) {
			if (col.gameObject.GetComponentInParent<MovingPlatform> ()) {
				box.sharedMaterial.bounciness = 0f;
				transform.parent = col.gameObject.transform.parent;
				onMovingPlatform = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.GetComponentInParent<MovingPlatform>()){
			box.sharedMaterial.bounciness = 0f;
			transform.parent = col.gameObject.transform.parent;
			onMovingPlatform = true;
		}
		
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
