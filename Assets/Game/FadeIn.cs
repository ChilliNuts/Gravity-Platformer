using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {


	public float fadeTime = 1f;
	private Image panel;
	private Color currentColor = Color.black;
	bool levelLoaded;

	void OnLevelWasLoaded(){
		levelLoaded = true;
	}
	void Start() {
		panel = GetComponent<Image>();
	}
	void Update(){
		if (levelLoaded) {
			if (Time.timeSinceLevelLoad < fadeTime) {
				currentColor.a -= Time.deltaTime / fadeTime;
				panel.color = currentColor;
			} else {
				gameObject.SetActive (false);
				levelLoaded = false;
			}
		}
	}
}
