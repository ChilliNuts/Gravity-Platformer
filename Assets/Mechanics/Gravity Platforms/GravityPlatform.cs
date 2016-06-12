using UnityEngine;
using System.Collections;

public class GravityPlatform : MonoBehaviour {

	ButtonSwitchesOn onSwitch;
	Rigidbody2D myRB;
	bool triggered = false;
	float originalGravityScale;
	public bool singleUse;
	public bool startOn = true;

	void Start(){
		onSwitch = GetComponent<ButtonSwitchesOn>();
		myRB = GetComponent<Rigidbody2D>();
		originalGravityScale = myRB.gravityScale;
		if (!startOn){
			FlipOnOff();
		}
	}

	void Update(){
		if (onSwitch != null && !triggered) {
			if (onSwitch.switchState == ButtonSwitchesOn.SwitchState.ON) {
				FlipOnOff ();
				onSwitch.switchState = ButtonSwitchesOn.SwitchState.IDLE;
				if(singleUse){
					triggered = true;
				}
			} else if (onSwitch.switchState == ButtonSwitchesOn.SwitchState.OFF) {
				FlipOnOff ();
				onSwitch.switchState = ButtonSwitchesOn.SwitchState.IDLE;
				if(singleUse){
					triggered = true;
				}
			}
		}


	}

	void FlipOnOff(){
		if (myRB.gravityScale == 0f){
			myRB.gravityScale = originalGravityScale;
		}else if(myRB.gravityScale == originalGravityScale){
			myRB.gravityScale = 0f;
		}
	}
}
