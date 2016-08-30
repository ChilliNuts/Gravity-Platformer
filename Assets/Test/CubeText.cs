using UnityEngine;
using System.Collections;

public class CubeText : MonoBehaviour {

	SpriteRenderer mySkin;
	Color alphaColor;
	public bool activeText;
	public float fadeDuration;
	public float alphaTarget;


	// Use this for initialization
	void Start () {
		mySkin = GetComponent<SpriteRenderer>();
		alphaColor = mySkin.color;
		if (activeText){
			alphaColor.a = 255;
		}else alphaColor.a = 0;
		mySkin.color = alphaColor;
	}
	
	// Update is called once per frame
	void Update () {
		if(alphaTarget < mySkin.color.a && alphaTarget != mySkin.color.a){
			FadeAlpha(-1, fadeDuration);
		}else if(alphaTarget > mySkin.color.a && alphaTarget != mySkin.color.a){
			FadeAlpha(1, fadeDuration);
		}

	}
	void FadeAlpha(int dir, float dur){
		if(alphaColor.a <= alphaTarget + 0.1f && alphaColor.a >= alphaTarget - 0.1f){
			alphaColor.a = alphaTarget;
		}else alphaColor.a += dir * Time.deltaTime / dur;
		mySkin.color = alphaColor;
	}
}
