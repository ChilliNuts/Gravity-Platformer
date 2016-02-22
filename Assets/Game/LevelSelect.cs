﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public Text levelNumberText;
	public Slider levelNumberSlider;
	public Text unlockedLevels;
	int levelToLoad;
	int maxLevelUnlocked;


	// Use this for initialization
	void Start () {
		maxLevelUnlocked = PlayerPrefsManager.ReturnMaxLevel();
		levelNumberSlider.maxValue = maxLevelUnlocked;
		unlockedLevels.text = ("Levels Unlocked: " + maxLevelUnlocked.ToString()+" / "
			+(LevelManager.TotalPlayableLevels()).ToString());
	}
	
	// Update is called once per frame
	void Update () {
		levelNumberText.text = levelNumberSlider.value.ToString();
		levelToLoad = (int)levelNumberSlider.value + 3;
	}

	public void GoToSelectedLevel(){
		SceneManager.LoadScene(levelToLoad);
	}
}
