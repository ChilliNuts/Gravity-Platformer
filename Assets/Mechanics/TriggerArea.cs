using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerArea : MonoBehaviour {

	public List<ButtonSwitchesOn> switchOn;
	public bool activateOnExit = false;
	public bool singleUse;
	PlayerController player;


	void Start(){
		player = FindObjectOfType<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D trig){
		if (!activateOnExit) {
			if (trig.gameObject == player.gameObject) {
				foreach (ButtonSwitchesOn s in switchOn) {
					if (s.switchState == ButtonSwitchesOn.SwitchState.IDLE) {
						s.switchState = ButtonSwitchesOn.SwitchState.ON;
						print ("triggerarea on");
						if (singleUse) {
							Destroy (gameObject);
						}
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D trig){
		if (activateOnExit) {
			if (trig.gameObject == player.gameObject) {
				foreach (ButtonSwitchesOn s in switchOn) {
					if (s.switchState == ButtonSwitchesOn.SwitchState.IDLE) {
						s.switchState = ButtonSwitchesOn.SwitchState.ON;
						print("triggerarea on");
						if (singleUse){
							Destroy(gameObject);
						}
					}
				}
			}
		}
	}
	//	void OnCollisionStay2D(Collision2D col){
	//		if(col.gameObject.tag == "Box"){
	//			if(myBody.isKinematic == true){
	//				myBody.isKinematic = false;
	//			}
	//		}
	//	}
}
