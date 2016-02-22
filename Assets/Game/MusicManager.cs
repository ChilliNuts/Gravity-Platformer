using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicArray;
	public AudioSource audioSource;


	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		audioSource.ignoreListenerVolume = true;
		if(PlayerPrefsManager.GetFirstPlay() == 0){
			PlayerPrefsManager.SetMasterVolume(1f);
			PlayerPrefsManager.SetMusicVolume(0.3f);
			PlayerPrefsManager.SetFirstPlay();
		}
		ChangeVolume(PlayerPrefsManager.GetMasterVolume());
		ChangeMusicVolume(PlayerPrefsManager.GetMusicVolume());
	}
	
	void OnLevelWasLoaded (int level){
		if (levelMusicArray.Length > 0) {
			AudioClip thisLevelsMusic = levelMusicArray [level];
			if (thisLevelsMusic) {// there is music attached
				audioSource.clip = thisLevelsMusic;
				audioSource.Play ();
				audioSource.loop = true;
			}
		}
	}
	public void ChangeMusicVolume(float volume){
		audioSource.volume = volume;
	}
	public void ChangeVolume(float volume){
		AudioListener.volume = volume;
	}
}
