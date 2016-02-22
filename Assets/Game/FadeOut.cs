using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public float fadeTime = 1f;
	public float fadeAfter;
	private Image panel;
	private Color currentColor = Color.black;
	
	void Start() {
		panel = GetComponent<Image>();
		currentColor.a = 0;
	}
	void Update(){
		if(fadeAfter > 0){
			fadeAfter -= Time.deltaTime;
		}else {
			currentColor.a += Time.deltaTime/fadeTime;
			panel.color = currentColor;
		}
	}
}
