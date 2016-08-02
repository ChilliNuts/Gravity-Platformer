using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {


	public float fadeInTime = 1f;
	public float fadeOutTime = 1f;
	private SpriteRenderer sprite;
	private Color currentColor = Color.black;
	bool fadeIn;
	public bool fadeOut;
	public float fadeOutAfter;
	float timerFOA = 0f;
	float timerFO = 0f;
	public int levelToLoadId;
	bool hasStartedFade;
	public static bool firstRunLvlOne = true;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		timerFOA = 0f;
		timerFO = 0f;
		if (fadeInTime > 0f) {
			fadeIn = true;
		}else {
			currentColor.a = 0f;
			sprite.color = currentColor;
		}
	}

	void Update(){
		if(fadeOutAfter > 0f){
			FadeOutAfter();
		}

		if (fadeIn) {
			if (firstRunLvlOne && SceneManager.GetActiveScene().buildIndex == 4) {
				currentColor.a = 0f;
				sprite.color = currentColor;
				fadeIn = false;  
				firstRunLvlOne = false;

			}else {
				FadeIn ();
			}
		}
		if (fadeOut){
			if (levelToLoadId == 4 && firstRunLvlOne) {
				SceneManager.LoadScene(levelToLoadId);
			}else FadeOut ();
		}
	}

	void FadeIn(){
		float mSFXVolume = PlayerPrefsManager.GetMasterVolume();
		if (Time.timeSinceLevelLoad < fadeInTime) {
			currentColor.a -= Time.deltaTime / fadeInTime;
			sprite.color = currentColor;
			if (AudioListener.volume < mSFXVolume) {
				AudioListener.volume += Time.deltaTime / fadeInTime / mSFXVolume;
			}
		} else {
			AudioListener.volume = mSFXVolume;
			currentColor.a = 0f;
			sprite.color = currentColor;
			fadeIn = false;
			gameObject.SetActive (false);
		}
	}

	void FadeOut(){
		float mSFXVolume = PlayerPrefsManager.GetMasterVolume();
		if(!hasStartedFade){
			timerFO = 0f;
			hasStartedFade = true;
		}
		currentColor.a += Time.deltaTime / fadeOutTime;
		sprite.color = currentColor;
		timerFO += Time.deltaTime;
		if (AudioListener.volume > 0) {
			AudioListener.volume -= Time.deltaTime / fadeInTime / mSFXVolume;
		}
		if(timerFO > fadeOutTime){
			hasStartedFade = false;
			fadeOut = false;
			SceneManager.LoadScene(levelToLoadId);
		}
	}

	void FadeOutAfter(){
		
		if(timerFOA < fadeOutAfter){
			timerFOA += Time.deltaTime;
		}else {
			fadeOutAfter = -1f;
			fadeOut = true;
		}
	}
}
