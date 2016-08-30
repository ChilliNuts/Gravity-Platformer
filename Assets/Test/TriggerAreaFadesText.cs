using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerAreaFadesText : MonoBehaviour {

	public Text textToFade;
	public float fadeDuration = 2f;
	bool triggered = false;
	Color fade;
	// Use this for initialization
	void Start () {
		fade = textToFade.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered){
			
			if(textToFade.color.a > 0f){
				fade.a -= Time.deltaTime / fadeDuration;
				textToFade.color = fade;
			}else {
				Destroy(textToFade.gameObject);
				Destroy(gameObject);
			}

		}
	}
	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject.tag == "Player" && !triggered){
			triggered = true;
		}
	}

}
