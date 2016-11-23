using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource[] audioSources;
	public float fadeTime = 5f;

	public AudioClip firstTrack;

	float songTimer;
	float secondsLeftInSong;
	bool fadeOutMusic;
	int sourceID = 0;
	bool isMuted = false;

	void Awake(){
		DontDestroyOnLoad (gameObject);
		audioSources = GetComponents<AudioSource>();
		#if UNITY_STANDALONE
		SaveManager.LoadGame();
		#endif
	}

	// Use this for initialization
	void Start () {
		audioSources[0].ignoreListenerVolume = true;
		audioSources[1].ignoreListenerVolume = true;
		if(PlayerPrefsManager.GetFirstPlay() == 0){
			PlayerPrefsManager.SetMasterVolume(0.7f);
			PlayerPrefsManager.SetMusicVolume(0.3f);
			PlayerPrefsManager.SetFirstPlay();
		}
		ChangeVolume(PlayerPrefsManager.GetMasterVolume());
		ChangeMusicVolume(PlayerPrefsManager.GetMusicVolume());
	}

	void Update(){
		
		if (!isMuted) {
			secondsLeftInSong = SecondsLeftInSong (audioSources [sourceID].clip);
		}


		if (secondsLeftInSong <= fadeTime + 0.5f){
			fadeOutMusic = true;
		}
		if(fadeOutMusic){
			fadeOutMusic = false;
			LoadSong (audioSources[sourceID], audioSources[GetNextTrackID(sourceID)]);
			StartCoroutine(CrossFadeMusic(audioSources[sourceID], audioSources[GetNextTrackID(sourceID)], fadeTime));
		}
	}

	IEnumerator CrossFadeMusic(AudioSource currentSource, AudioSource nextSource, float fadeTime){

		float startVolume = PlayerPrefsManager.GetMusicVolume();
		nextSource.volume = 0f;
		sourceID = GetNextTrackID(sourceID);
		songTimer = 0f;
		nextSource.Play();
		fadeOutMusic = false;
		while(currentSource.volume > 0){
			currentSource.volume -= startVolume * Time.deltaTime / fadeTime;
			nextSource.volume += startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		nextSource.volume = startVolume;
		currentSource.Stop();
		currentSource.clip = null;
	}

	void LoadSong (AudioSource currentSource, AudioSource nextSource){
		
		AudioClip[] songPool = FindObjectOfType<LevelManager>().musicPoolArray;

		if(currentSource.clip == null){
			currentSource.clip = songPool[Random.Range(0,songPool.Length)];
		}
		if(nextSource.clip == null){
			nextSource.clip = songPool[Random.Range(0,songPool.Length)];
			while(nextSource.clip == currentSource.clip && songPool.Length > 1){
				nextSource.clip = songPool[Random.Range(0,songPool.Length)];
			}
		}
	}

	int GetNextTrackID(int currentTrack){
		if(currentTrack == 0) return 1;
		else return 0;
	}

	float SecondsLeftInSong(AudioClip song){
		if(songTimer < song.length && audioSources[sourceID].isPlaying){
			songTimer += Time.deltaTime;
		}
		return song.length - songTimer;
	}

	public void ChangeMusicOnExitMenus(){
		audioSources[GetNextTrackID(sourceID)].clip = firstTrack;
		fadeOutMusic = true;
	}

	public void SwitchMute(){
		if(!isMuted){
			audioSources[sourceID].Pause();
			audioSources[GetNextTrackID(sourceID)].Pause();
			ChangeMusicVolume(0f);
			ChangeVolume(0f);
			isMuted = true;
		}else if(isMuted){
			ChangeMusicVolume(PlayerPrefsManager.GetMusicVolume());
			ChangeVolume(PlayerPrefsManager.GetMasterVolume());
			audioSources[sourceID].Play();
			isMuted = false;
		}

	}
	
//	void OnLevelWasLoaded (int level){
//		if (levelMusicArray.Length > 0) {
//			AudioClip thisLevelsMusic = levelMusicArray [level];
//			if (thisLevelsMusic) {// there is music attached
//				audioSource.clip = thisLevelsMusic;
//				audioSource.Play ();
//				audioSource.loop = true;
//			}
//		}
//	}
	public void ChangeMusicVolume(float volume){
		foreach(AudioSource audioS in audioSources){
			audioS.volume = volume;
		}
	}
	public void ChangeVolume(float volume){
		AudioListener.volume = volume;
	}
}
