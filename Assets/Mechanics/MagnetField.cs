using UnityEngine;
using System.Collections;


[RequireComponent(typeof(ButtonSwitchesOn))]
public class MagnetField : MonoBehaviour {


	public enum mForceDirection {DOWN, UP, LEFT, RIGHT};
	public mForceDirection mForceDir;
	Vector2 magnetForceDirection;
	public float magnetStrength = 2;
	PlayerController player;
	public bool on = true;
	ButtonSwitchesOn switchOn;
	ParticleSystem particles;
	BoxCollider2D boxCol;

	void Start () {
		player = FindObjectOfType<PlayerController>();
		float gravityValue = Mathf.Abs(Physics2D.gravity.x) + Mathf.Abs (Physics2D.gravity.y);
		magnetForceDirection = SetForceDirection(mForceDir) * gravityValue;
		switchOn = GetComponent<ButtonSwitchesOn>();
		particles = GetComponent<ParticleSystem>();
		boxCol = GetComponent<BoxCollider2D>();
		if(!on){
			boxCol.enabled = false;
			particles.Stop();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(switchOn.switchState == ButtonSwitchesOn.SwitchState.ON){
			FlipOnOff();
			switchOn.switchState = ButtonSwitchesOn.SwitchState.IDLE;
		}
		if(switchOn.switchState == ButtonSwitchesOn.SwitchState.OFF){
			FlipOnOff();
			switchOn.switchState = ButtonSwitchesOn.SwitchState.IDLE;
		}
	}
	void OnTriggerEnter2D(Collider2D trigger){
		if (on) {
			if (trigger.gameObject == player.gameObject) {
				player.magnetised = true;
				SetPlayerFloor ();
				StartCoroutine (player.RotatePlayer ()); 
			}
		}
	}
	void OnTriggerStay2D(Collider2D trigger){
		if (on) {
			Rigidbody2D trigBody = trigger.GetComponent<Rigidbody2D> ();
			if (trigBody != null && trigger.gameObject.tag != "Button") {
				trigBody.AddForce (magnetForceDirection * ((trigBody.gravityScale + trigBody.mass) * magnetStrength));
			}
		}
	}
	void OnTriggerExit2D(Collider2D trigger){
		if (on) {
			if (trigger.gameObject == player.gameObject) {
				player.magnetised = false;
				player.ResetPlayerFloor ();
				StartCoroutine (player.RotatePlayer ());
			}
		}
	}

	void FlipOnOff(){
		if(on){
			boxCol.enabled = false;
			particles.Stop();
			on = false;
			player.magnetised = false;
			player.ResetPlayerFloor();
			StartCoroutine (player.RotatePlayer ());
		}else if (!on){
			on = true;
			particles.Play();
			boxCol.enabled = true;
			player.magnetised = true;
			SetPlayerFloor();
			StartCoroutine (player.RotatePlayer ());
		}
	}

	void SetPlayerFloor(){
		if(mForceDir == mForceDirection.DOWN){
			player.floorDirection = PlayerController.FloorDirection.DOWN;
		}else if(mForceDir == mForceDirection.UP){
			player.floorDirection = PlayerController.FloorDirection.UP;
		}else if(mForceDir == mForceDirection.LEFT){
			player.floorDirection = PlayerController.FloorDirection.LEFT;
		}else if(mForceDir == mForceDirection.RIGHT){
			player.floorDirection = PlayerController.FloorDirection.RIGHT;
		}
	}

	Vector2 SetForceDirection(mForceDirection mForceDir){
		if(mForceDir == mForceDirection.DOWN){
			return Vector2.down;
		}else if(mForceDir == mForceDirection.UP){
			return Vector2.up;
		}else if(mForceDir == mForceDirection.LEFT){
			return Vector2.left;
		}else if(mForceDir == mForceDirection.RIGHT){
			return Vector2.right;
		}
		return Vector2.zero;
	}
}
