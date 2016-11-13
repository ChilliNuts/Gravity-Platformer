using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubeTextTyper : MonoBehaviour {

	public float letterDelay = 0.02f;
	float originalLetterDelay;
	public float nextPageDelay = 3f;
	[HideInInspector] public string message;
	[HideInInspector] public Text textComp;
	[HideInInspector] public bool isSpeaking = false;
	[HideInInspector] public bool finishedPageText = false;
	AudioSource CubeVoice;


	// Use this for initialization
	void Start () {
		CubeVoice = FindObjectOfType<RotateCube>().GetComponent<AudioSource>();
		textComp = GetComponent<Text>();
		message = textComp.text;
		textComp.text = "";
		originalLetterDelay = letterDelay;
	}

	public void StartTyping(){
		StopCoroutine(TypeText());
		textComp.text = "";
		StartCoroutine(TypeText());
	}
	public void PrintAllText(){
		StopCoroutine(TypeText());
		textComp.text = "";
		textComp.text = message;
	}

	public IEnumerator TypeText(){
		finishedPageText = false;
		isSpeaking = true;
		CubeVoice.Play();
		foreach(char c in message.ToCharArray()){
			if (textComp.text.Length < message.Length) {
				textComp.text += c;
			}
			yield return new WaitForSeconds(letterDelay);
		}
		letterDelay = originalLetterDelay;
		isSpeaking = false;
		finishedPageText = true;
	}
}
