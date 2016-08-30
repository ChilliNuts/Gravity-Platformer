using UnityEngine;
using System.Collections;

public class CubeSpeechTrigger : MonoBehaviour {

	public GameObject cubeSpeechBox;
	public string whatToSay;
	bool triggered;
	PlayerController player;
	Animator cubeSpeechBoxAnim;
	TextTyper cubeTyper;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		cubeSpeechBoxAnim = cubeSpeechBox.GetComponent<Animator>();
		cubeTyper = cubeSpeechBox.GetComponentInChildren<TextTyper>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject == player.gameObject && !triggered){
			triggered = true;
			cubeTyper.message = whatToSay;
			cubeSpeechBoxAnim.SetTrigger("trigger_StartOpening");
			Destroy(gameObject);
		}
	}
}
