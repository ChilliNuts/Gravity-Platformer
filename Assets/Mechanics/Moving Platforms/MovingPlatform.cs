﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public bool rotating = false;
	public float rotationSpeed;
	public bool moving = false;
	public Transform[] waypoints;
	public float moveSpeed;
	public bool reverse;
	bool reversing;
	ButtonSwitchesOn switchOn;
	[HideInInspector] public int targetWaypointIndex = 0;



	// Use this for initialization
	void Start () {
		switchOn = GetComponent<ButtonSwitchesOn>();
	}

	// Update is called once per frame
	void Update () {
		if (switchOn != null) {
			if (switchOn.switchState == ButtonSwitchesOn.SwitchState.ON) {

				if (switchOn.flipRotating) {
					flipRotating ();
				}
				if (switchOn.flipMoving) {
					flipMoving ();
				}
				switchOn.switchState = ButtonSwitchesOn.SwitchState.IDLE;
			} else if (switchOn.switchState == ButtonSwitchesOn.SwitchState.OFF) {
				if (switchOn.flipRotating) {
					flipRotating ();
				}
				if (switchOn.flipMoving) {
					flipMoving ();
				}
				switchOn.switchState = ButtonSwitchesOn.SwitchState.IDLE;
			}
		}
	}

	void FixedUpdate() {
		if(rotating){
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}
		if(moving){
			
			if(targetWaypointIndex >= waypoints.Length){
				if (reverse){
					reversing = true;
					targetWaypointIndex--;
				}
				else targetWaypointIndex = 0;
			}


			if(transform.position != waypoints[targetWaypointIndex].position){
				transform.position = Vector2.MoveTowards (transform.position, 
					waypoints[targetWaypointIndex].position, 
					moveSpeed * Time.deltaTime);
			}else{
				
				if (reversing) {
					if(targetWaypointIndex > 0){
						targetWaypointIndex--;
					}
					else reversing = false;
				}
				else targetWaypointIndex++;
			}
			if(targetWaypointIndex >= waypoints.Length){
				if (reverse){
					reversing = true;
					targetWaypointIndex--;
				}
				else targetWaypointIndex = 0;
			}
		}
	}

	void flipRotating(){
		if(rotating) rotating = false;
		else if(!rotating)rotating = true;
	}
	void flipMoving(){
		if(moving) moving = false;
		else if(!moving)moving = true;
	}
}
