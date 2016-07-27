using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {


	public Slider volumeSlider;
	public Slider musicVolumeSlider;
	public LevelManager levelManager;
	MusicManager musicManager;


	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();

		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.ChangeVolume(volumeSlider.value);
		musicManager.ChangeMusicVolume(musicVolumeSlider.value);
	}

	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetMusicVolume(musicVolumeSlider.value);
		levelManager.FadeOutAndLoad(1);
	}
	public void BackToTitle(){
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		SaveAndExit();
	}

	public void SetDefaults(){
		volumeSlider.value = 0.7f;
		musicVolumeSlider.value = 0.3f;
	}
}
