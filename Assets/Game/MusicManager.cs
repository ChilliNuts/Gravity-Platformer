using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource[] audioSources;
	public float fadeTime = 5f;

	public AudioClip firstTrack;
	public AudioClip introTrack;

	float secondsLeftInSong;
	bool fadeOutMusic;
	bool canFadeMusic = true;
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
		if (audioSources [sourceID].clip != null) {
			secondsLeftInSong = SecondsLeftInSong (audioSources [sourceID]);
		}

		if (secondsLeftInSong <= fadeTime + 0.5f){
			fadeOutMusic = true;
		}
		if(fadeOutMusic){
			
			LoadSong (audioSources[sourceID], audioSources[GetNextTrackID(sourceID)]);
			StartCoroutine(CrossFadeMusic(audioSources[sourceID], audioSources[GetNextTrackID(sourceID)], fadeTime));
		}
	}

	IEnumerator CrossFadeMusic(AudioSource currentSource, AudioSource nextSource, float fadeTime){
		if(!canFadeMusic){
			yield return new WaitUntil(() => canFadeMusic == true);
		}
		canFadeMusic = false;
		float startVolume = PlayerPrefsManager.GetMusicVolume();
		nextSource.volume = 0f;
		sourceID = GetNextTrackID(sourceID);
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
		canFadeMusic = true;
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

	float SecondsLeftInSong(AudioSource song){
		
		return song.clip.length - song.time;
	}
		

	public void ChangeMusicOnExitMenus(int lvl){
		if (!fadeOutMusic) {
			if (lvl >= 15 && lvl <= 33 && audioSources[sourceID].clip != firstTrack) {
				audioSources [GetNextTrackID (sourceID)].clip = firstTrack;
				fadeOutMusic = true;
			}else if(audioSources[sourceID].clip != introTrack){
				audioSources [GetNextTrackID (sourceID)].clip = introTrack;
				fadeOutMusic = true;
			} 
		}
	}

	public void SwitchMute(){
		if(!isMuted){
			ChangeMusicVolume(0f);
			ChangeVolume(0f);
			isMuted = true;
		}else if(isMuted){
			ChangeMusicVolume(PlayerPrefsManager.GetMusicVolume());
			ChangeVolume(PlayerPrefsManager.GetMasterVolume());
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
