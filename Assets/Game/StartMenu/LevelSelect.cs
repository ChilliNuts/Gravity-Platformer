using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public Text levelNumberText;
	public Slider levelNumberSlider;
	public Text unlockedLevels;
	int levelToLoad;
	int maxLevelUnlocked;
	public Button loadLevelButton;


	// Use this for initialization
	void Start () {
		#if UNITY_WEBGL
		maxLevelUnlocked = PlayerPrefsManager.ReturnMaxLevel();
		#endif
		#if UNITY_STANDALONE
		maxLevelUnlocked = SaveManager.localMaxLevel;
		#endif
		levelNumberSlider.maxValue = maxLevelUnlocked;
		unlockedLevels.text = ("Levels Unlocked: " + maxLevelUnlocked.ToString()+" / "
			+(LevelManager.TotalPlayableLevels()).ToString());
		if(maxLevelUnlocked > 0){
			levelNumberSlider.minValue = 1;
		} else loadLevelButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		levelNumberText.text = levelNumberSlider.value.ToString();
		levelToLoad = (int)levelNumberSlider.value + 3;
	}

	public void GoToSelectedLevel(){
		if (levelToLoad == 4 && LevelManager.ReturnLevelNumber() != 1) {
			Fade.firstRunLvlOne = false;
		}
		FindObjectOfType<LevelManager>().FadeOutAndLoad(levelToLoad);
		FindObjectOfType<MusicManager>().ChangeMusicOnExitMenus(levelToLoad);
		//SceneManager.LoadScene(levelToLoad);
	}
}
