using UnityEngine;
using System.Collections;

public class NewGameWarning : MonoBehaviour {

	public GameObject warning;
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		warning.gameObject.SetActive(false);
	}

	public void NewGame(){
		if(PlayerPrefsManager.ReturnMaxLevel() > 1 || PlayerPrefsManager.ReturnStat("deaths") > 0 || 
			PlayerPrefsManager.ReturnStat("beamsFired") > 0){
			warning.gameObject.SetActive(true);
		} else levelManager.LoadLevel("02Level_01");
	}

	public void OkStartNewGame(){
		PlayerPrefsManager.ResetKeys();;
		levelManager.LoadLevel("02Level_01");
	}

	public void CancelWarning(){
		warning.gameObject.SetActive(false);
	}
}
