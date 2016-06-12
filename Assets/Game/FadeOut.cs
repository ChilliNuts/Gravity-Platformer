using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public bool useFadeAfter;
	public float fadeTime = 1f;
	public float fadeAfter;
	private Image panel;
	private Color currentColor = Color.black;
	public bool startFade;
	
	void Start() {
		panel = GetComponent<Image>();
		currentColor.a = 0;
	}
	void Update(){
		if (useFadeAfter) {
			if (fadeAfter > 0) {
				fadeAfter -= Time.deltaTime;
			} else {
				currentColor.a += Time.deltaTime / fadeTime;
				panel.color = currentColor;
			}
		}else if(startFade){
			currentColor.a += Time.deltaTime / fadeTime;
			panel.color = currentColor;
		}
	}
}
