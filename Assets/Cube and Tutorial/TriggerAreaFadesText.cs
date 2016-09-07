using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerAreaFadesText : MonoBehaviour {

	public Text textToFade;
	public float fadeDuration = 2f;
	public enum FadeDirection {IN, OUT};
	public FadeDirection fadedirection;
	bool triggered = false;
	static Color fade;
	// Use this for initialization
	void Start () {
		fade = textToFade.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered){
			if(fadedirection == FadeDirection.OUT){
				if(textToFade.color.a > 0){
					fade.a -= Time.deltaTime / fadeDuration;
					textToFade.color = fade;
				}else {
					textToFade.gameObject.SetActive(false);
					this.gameObject.SetActive(false);
				}
			}
			if(fadedirection == FadeDirection.IN){
				if(textToFade.color.a < 1){
					fade.a += Time.deltaTime / fadeDuration;
					textToFade.color = fade;
				}else {
					fade.a = 1;
					textToFade.color = fade;
					this.gameObject.SetActive(false);
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject.tag == "Player" && !triggered){
			triggered = true;
		}
	}

}
