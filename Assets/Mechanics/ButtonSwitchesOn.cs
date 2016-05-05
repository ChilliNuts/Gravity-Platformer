using UnityEngine;
using System.Collections;

public class ButtonSwitchesOn : MonoBehaviour {
	
	public enum SwitchState {ON, OFF, IDLE};
	public SwitchState switchState;
	public bool flipRotating, flipMoving;

	
	// Use this for initialization
	void Awake () {
		switchState = SwitchState.IDLE;
	}
}
