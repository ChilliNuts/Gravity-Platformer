using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string MUSIC_VOLUME_KEY = "music_volume";
//	const string VCONTROL_KEY = "virtual_control";
	const string LEVEL_KEY = "level_unlocked_";
	const string MAX_LEVEL_UNLOCKED = "max_level_unlocked";
	const string TOTAL_DEATHS = "total_deaths";
	const string TOTAL_BEAMS_FIRED = "total_beams_fired";
	const string FIRST_PLAY = "first_play";

	public static void SetMasterVolume(float volume){
		if(volume >= 0f && volume <= 1f){
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}else{
			Debug.LogError ("master volume outside of range");
		}
	}
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	public static void SetMusicVolume(float volume){
		if(volume >= 0f && volume <= 1f){
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
		}else{
			Debug.LogError ("music volume outside of range");
		}
	}
	public static float GetMusicVolume(){
		return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
	}

	public static void UnlockMaxLevel(int level){
		if (level <= LevelManager.TotalPlayableLevels()){
			PlayerPrefs.SetInt (MAX_LEVEL_UNLOCKED, level);
		}
	}
	public static int ReturnMaxLevel(){
		return PlayerPrefs.GetInt (MAX_LEVEL_UNLOCKED);
	}

	public static void UnlockLevel(int level){
		if (level <= SceneManager.sceneCountInBuildSettings - 1){
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1);
		}else {
			Debug.LogError("level does not exist in build order");
		}
	}
	public static bool IsLevelUnlocked(int level){
		int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString ());
		bool isLevelUnlocked = (levelValue == 1);

		if (level <= SceneManager.sceneCountInBuildSettings - 1){
			return isLevelUnlocked;
		}else {
			Debug.Log ("level does not exist in build order");
			return false;
		}
	}
	public static void UpdateStats(int death, int fireBeam){
		PlayerPrefs.SetInt(TOTAL_DEATHS, PlayerPrefs.GetInt(TOTAL_DEATHS)+death);
		PlayerPrefs.SetInt(TOTAL_BEAMS_FIRED, PlayerPrefs.GetInt(TOTAL_BEAMS_FIRED)+fireBeam);
	}
	public static int ReturnStat(string stat){
		if(stat == "deaths"){
			return PlayerPrefs.GetInt(TOTAL_DEATHS);
		}else if(stat == "beamsFired"){
			return PlayerPrefs.GetInt(TOTAL_BEAMS_FIRED);
		}
		Debug.LogError("Unknown stat type, try either deaths, or beamsFired");
		return 0;
	}
	public static void ResetKeys(){
		PlayerPrefs.DeleteKey(MAX_LEVEL_UNLOCKED);
		PlayerPrefs.DeleteKey(TOTAL_DEATHS);
		PlayerPrefs.DeleteKey(TOTAL_BEAMS_FIRED);
	}

	public static void SetFirstPlay(){
		PlayerPrefs.SetInt(FIRST_PLAY, 1);
	}
	public static int GetFirstPlay(){
		return PlayerPrefs.GetInt(FIRST_PLAY);
	}

//	public static void SetVControl(float vControl){
//		if (vControl >= 0f && vControl <= 1f){
//			PlayerPrefs.SetFloat(VCONTROL_KEY, vControl);
//		}else {
//			Debug.LogError ("difficulty is out of range");
//		}
//	}
//	public static float GetVControl(){
//		return PlayerPrefs.GetFloat(VCONTROL_KEY);
//	}
}
