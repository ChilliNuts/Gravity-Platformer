using UnityEngine;
using System.Collections;

public class CubeSpeechTrigger : MonoBehaviour {

	public GameObject cubeSpeechBox;
	public string[] whatToSay;
	int whatToSayIndex = 0;
	public bool makePlayerfreeze;
	public bool cubeFollowsOnExit;
	public bool cubeEntersPlayerOnExit = false;
	bool triggered;
	bool speechClosed;
	PlayerController player;
	GameObject cube;
	Animator cubeSpeechBoxAnim;
	TextTyper cubeTyper;
	float finishedPageTimer = 0f;


	// Use this for initialization
	void Start () {
		cube = FindObjectOfType<RotateCube>().gameObject;
		player = FindObjectOfType<PlayerController>();
		cubeSpeechBoxAnim = cubeSpeechBox.GetComponent<Animator>();
		cubeTyper = cubeSpeechBox.GetComponentInChildren<TextTyper>();
		for (int i = 0; i < whatToSay.Length; i++){
			whatToSay[i] = whatToSay[i].Replace("\\n", "\n");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered && !speechClosed){
			if(Input.GetMouseButtonDown(0) && cubeTyper.isSpeaking == false){
				NextPageOrClose ();
			}else if(Input.GetMouseButtonDown(0) && cubeTyper.isSpeaking == true){
				cubeTyper.letterDelay = 0f;
			}
			if(cubeTyper.finishedPageText){
				if ( finishedPageTimer <= cubeTyper.nextPageDelay) {
					finishedPageTimer += Time.deltaTime;
				}else NextPageOrClose();
			}
		}
	}


	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject == player.gameObject && !triggered){
			triggered = true;
			cubeTyper.message = whatToSay[whatToSayIndex];
			cubeTyper.isSpeaking = true;
			cubeTyper.finishedPageText = false;
			finishedPageTimer = 0f;
			cubeSpeechBoxAnim.SetTrigger("trigger_StartOpening");
			if(makePlayerfreeze){
				player.canMove = false;
			}
		}
	}

	void NextPageOrClose (){
		finishedPageTimer = 0f;
		if (whatToSayIndex >= whatToSay.Length - 1) {
			speechClosed = true;
			cubeSpeechBoxAnim.SetTrigger ("trigger_StartClosing");
			if (cubeFollowsOnExit) {
				cube.GetComponent<CubeFollow> ().followTarget = GameObject.FindGameObjectWithTag ("CubeToFollow");
			}
			if(cubeEntersPlayerOnExit){
				cube.GetComponent<CubeFollow> ().followTarget = player.gameObject;
				StartCoroutine(EnterPlayer());
			}else player.canMove = true;
		}
		else {
			whatToSayIndex++;
			cubeTyper.message = whatToSay [whatToSayIndex];
			cubeTyper.StartTyping ();
		}
	}
	IEnumerator EnterPlayer(){
		yield return new WaitForSeconds(1f);
		cube.GetComponent<CubeFollow> ().OnExit();
		player.GetComponent<Gun>().enabled = true;
		cubeEntersPlayerOnExit = false;
		player.canMove = true;
	}
}
	
