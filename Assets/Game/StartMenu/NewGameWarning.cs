using UnityEngine;
using System.Collections;

public class NewGameWarning : MonoBehaviour {

	public GameObject warning;
	LevelManager levelManager;
	Canvas menuCanvas;
	float loadAfter = 8.5f;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		warning.gameObject.SetActive(false);
		menuCanvas = FindObjectOfType<Canvas>();
	}

	public void NewGame(){
		#if UNITY_WEBGL
		if(PlayerPrefsManager.ReturnMaxLevel() > 1 || PlayerPrefsManager.ReturnStat("deaths") > 0 || 
			PlayerPrefsManager.ReturnStat("beamsFired") > 0){
			warning.gameObject.SetActive(true);
		} else StartCoroutine(StartLevelOne()); //levelManager.LoadLevel("02Level_01");
		#endif
		#if UNITY_STANDALONE
		if(SaveManager.localMaxLevel > 1 || SaveManager.localDeaths > 0 || 
			SaveManager.localShotsFired > 0){
		warning.gameObject.SetActive(true);
		} else StartCoroutine(StartLevelOne()); //levelManager.LoadLevel("02Level_01");
		#endif
	}

	public void OkStartNewGame(){
		#if UNITY_WEBGL
		PlayerPrefsManager.ResetKeys();
		#endif
		#if UNITY_STANDALONE
		SaveManager.ResetData();
		#endif
		warning.gameObject.SetActive(false);
		StartCoroutine(StartLevelOne());
		//levelManager.LoadLevel("02Level_01");
	}

	public void CancelWarning(){
		warning.gameObject.SetActive(false);
	}
	IEnumerator StartLevelOne(){
		Animator canvasAnim = menuCanvas.GetComponent<Animator>();
		canvasAnim.SetTrigger("startGame");
		menuCanvas.GetComponent<StartGame>().gameStarted = true;
		yield return new WaitForSeconds(loadAfter);
		levelManager.LoadLevel(4);
	}
}
