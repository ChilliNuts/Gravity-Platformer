using UnityEngine;
using System.Collections;

public class SwitchTutorialText : MonoBehaviour {

	public GameObject text1;
	public GameObject text2;
	Gun playerGun;
	bool ableToTrigger = false;
	public bool playerEnteredBounds = false;


	void Start(){
		playerGun = FindObjectOfType<PlayerController>().GetComponent<Gun>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(playerGun.enabled){
				ableToTrigger = true;
			}
		}
		if (ableToTrigger) {
			if (SetGravityDirection.gDirection == SetGravityDirection.GDirection.LEFT ||
			   SetGravityDirection.gDirection == SetGravityDirection.GDirection.RIGHT) {
				text1.SetActive (false);
				text2.SetActive (true);
			}
		}
	}
}
