﻿using UnityEngine;
using System.Collections;

public class CubeSpeechBoxFollow : MonoBehaviour {

	GameObject followTarget;
	public float followSpeed;
	CubeTextTyper textTyper;
	ParticleSystem particles;

	void Start(){
		followTarget = FindObjectOfType<CubeTextFollow>().gameObject;
		textTyper = GetComponentInChildren<CubeTextTyper>();
		particles = GetComponent<ParticleSystem>();
	}
	// Update is called once per frame
	void Update () {
		Bob();
		if(followTarget != null){
			transform.position = Vector3.Slerp(transform.position, followTarget.transform.position, followSpeed*Time.deltaTime);
		}
	}
	void Bob(){
		float yPos = transform.position.y + 0.025f - Mathf.PingPong(Time.time * 0.04f, 0.05f);
		transform.position = new Vector3(transform.position.x, yPos , transform.position.z);
	}
	public void StartText(){
		textTyper.StartTyping();
	}
	public void ParticlesOn(){
		particles.Play();
	}
	public void ParticlesOff(){
		particles.Stop();
	}
}
