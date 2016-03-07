using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressureButtonButton : MonoBehaviour {

	Rigidbody2D myBody;
	SpriteRenderer mySprite;
	public float upForce = 1f;
	//public List<GameObject> connectedTo;
	public List<ButtonSwitchesOn> switchOn;
	public AudioClip onSFX;
	public AudioClip offSFX;
	bool boxCollide = false;
	public Color offColor, onColor;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D>();
		mySprite = GetComponent<SpriteRenderer>();
//		myBody.isKinematic = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!boxCollide) {
			if (transform.localPosition.y < 0f) {
				myBody.AddRelativeForce (Vector2.up * upForce);
			} else if (transform.localPosition.y > 0f) {
				transform.localPosition = Vector2.zero;
				//			myBody.isKinematic = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D trig){
		if(trig.gameObject.tag == this.gameObject.tag){
			foreach(ButtonSwitchesOn s in switchOn){
				if(s.switchState == ButtonSwitchesOn.SwitchState.IDLE){
					s.switchState = ButtonSwitchesOn.SwitchState.ON;
					AudioSource.PlayClipAtPoint(onSFX, transform.position);
					mySprite.color = onColor;
				}
			}
		}
		if(trig.gameObject.tag == "Box"){
			boxCollide = true;
		}
	}

	void OnTriggerExit2D(Collider2D trig){
		if(trig.gameObject.tag == this.gameObject.tag){
			foreach(ButtonSwitchesOn s in switchOn){
				if(s.switchState == ButtonSwitchesOn.SwitchState.IDLE){
					s.switchState = ButtonSwitchesOn.SwitchState.OFF;
					AudioSource.PlayClipAtPoint(offSFX, transform.position);
					mySprite.color = offColor;
				}
			}
		}
		if(trig.gameObject.tag == "Box"){
			boxCollide = false;
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
