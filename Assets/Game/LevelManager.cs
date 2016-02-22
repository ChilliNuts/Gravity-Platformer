using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
	bool gamePaused = false;
	public GameObject pauseMenu;
	Gun playerGun;
	Rigidbody2D playerBody;
	CustomCursor cursor;

	void Start(){
		if(GameObject.FindObjectOfType<PlayerController>() != null){
			playerBody = GameObject.FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
		}
		cursor = FindObjectOfType<CustomCursor>();
		playerGun = FindObjectOfType<Gun>();

		if(PlayerPrefsManager.ReturnMaxLevel() < 1){
			PlayerPrefsManager.UnlockMaxLevel(1);
		}
		if (SceneManager.GetActiveScene().name == "00Splash"){
			if (autoLoadNextLevelAfter <= 0){
				Debug.Log("Auto load disabled, use a positive number to enable");
			}else{
				Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
			}
		}
		if(SceneManager.GetActiveScene().buildIndex <= 3){
			SetCustomCursor(false);
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(LevelManager.ReturnLevelNumber() >= 1 && pauseMenu != null){
				if(!gamePaused){
					EnterPauseMenu();
				}else if(gamePaused){
					ExitPauseMenu();
				}
			}
		}
	}

	public void LoadLevel(string level){
		Debug.Log("New level load: "+ level);
		Camera2DFollow.firstSlowPan = true;
		SceneManager.LoadScene(level);
	}
	public void ContinueGame(){
		Camera2DFollow.firstSlowPan = true;
		SceneManager.LoadScene(PlayerPrefsManager.ReturnMaxLevel() + 3);
	}
	public void QuitRequest(){
		Debug.Log("You have quit!");
		Application.Quit();
	}
	public void LoadNextLevel(){
		if ((LevelManager.ReturnLevelNumber()) >= PlayerPrefsManager.ReturnMaxLevel()) {
			PlayerPrefsManager.UnlockMaxLevel (PlayerPrefsManager.ReturnMaxLevel () + 1);
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void EnterPauseMenu(){
		SetCustomCursor(false);
		playerGun.canFireLazer = false;
		pauseMenu.SetActive(true);
		gamePaused = true;
		Time.timeScale = 0f;
	}
	public void ExitPauseMenu(){
		SetCustomCursor(true);
		playerGun.canFireLazer = true;
		pauseMenu.SetActive(false);
		gamePaused = false;
		Time.timeScale = 1f;
		playerBody.velocity = Vector2.zero;
	}

	void SetCustomCursor(bool c){
		cursor.useCustomCursor = c;
		cursor.updateCursor = true;
	}

	public static int ReturnLevelNumber(){
		int numberOfOptionsScenes = 3;
		return SceneManager.GetActiveScene().buildIndex - numberOfOptionsScenes;
	}
	public static int TotalPlayableLevels(){
		int numberOfNonPlayableLevels = 5;
		return SceneManager.sceneCountInBuildSettings - numberOfNonPlayableLevels;
	}
}
