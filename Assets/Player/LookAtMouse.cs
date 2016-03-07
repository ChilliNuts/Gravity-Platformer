using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	PlayerController player;
	float x;
	Vector3 ls;
	//public float damping = 1f;
	public bool flip;
	public bool rotate;

	public bool clampRotation;
	public float clampAngle;

	void Start() {
		x = transform.localScale.x;
		ls = transform.localScale;
		player = FindObjectOfType<PlayerController>();
	}

	void Update () {
		CheckAimDirection();
	}

	void CheckAimDirection(){

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);  

		float rotZ = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;

		//rotZ *= damping * 0.1f;


		if (player.floorDirection == PlayerController.FloorDirection.UP ||
			player.floorDirection == PlayerController.FloorDirection.DOWN){
			if (player.facingRight) {
				if (dir.x >= 0) {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							if (clampRotation) {
								float clampedRotZ = Mathf.Clamp (rotZ, -clampAngle, clampAngle);
								transform.rotation = Quaternion.Euler (0f, 0f, clampedRotZ);
							}else{
								transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
							}
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							float clampedRotZ = Mathf.Clamp (rotZ, -clampAngle, clampAngle);
							if (clampRotation) {
								
								transform.rotation = Quaternion.Euler (0f, 0f, clampedRotZ + 180);
							}else{
								transform.rotation = Quaternion.Euler (0f, 0f, -rotZ + 180);
							}
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							ls.x = x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							ls.x = -x;
							transform.localScale = ls;
						}
					}
				} else {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ + 180);
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							ls.x = -x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							ls.x = x;
							transform.localScale = ls;
						}
					}
				}
			} else if (!player.facingRight) {
				if (dir.x >= 0) {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ + 180);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							ls.x = -x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							ls.x = x;
							transform.localScale = ls;
						}
					}
				} else {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ + 180);
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.DOWN) {
							ls.x = x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.UP){
							ls.x = -x;
							transform.localScale = ls;
						}
					}
				}
			}
		}else if (player.floorDirection == PlayerController.FloorDirection.LEFT ||
			player.floorDirection == PlayerController.FloorDirection.RIGHT){
			if (player.facingRight) {
				if (dir.y >= 0) {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							ls.x = x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							ls.x = -x;
							transform.localScale = ls;
						}
					}
				} else {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ);
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							ls.x = -x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							ls.x = x;
							transform.localScale = ls;
						}
					}
				}
			} else if (!player.facingRight) {
				if (dir.y >= 0) {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							ls.x = -x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							ls.x = x;
							transform.localScale = ls;
						}
					}
				} else {
					if (rotate) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							transform.rotation = Quaternion.Euler (0f, 0f, -rotZ);
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
						}
					}
					if (flip) {
						if (player.floorDirection == PlayerController.FloorDirection.RIGHT) {
							ls.x = x;
							transform.localScale = ls;
						}else if(player.floorDirection == PlayerController.FloorDirection.LEFT){
							ls.x = -x;
							transform.localScale = ls;
						}
					}
				}
			}
		}
	}
}
