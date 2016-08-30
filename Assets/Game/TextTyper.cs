using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour {

	public float letterDelay = 0.2f;
	[HideInInspector] public string message;
	Text textComp;


	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text>();
		message = textComp.text;
		textComp.text = "";
	}

	public void StartTyping(){
		StopCoroutine(TypeText());
		textComp.text = "";
		StartCoroutine(TypeText());
	}

	public IEnumerator TypeText(){
		foreach(char c in message.ToCharArray()){
			textComp.text += c;
			yield return new WaitForSeconds(letterDelay);
		}
	}
}
