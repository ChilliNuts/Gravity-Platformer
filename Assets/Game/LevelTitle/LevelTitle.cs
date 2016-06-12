using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTitle : MonoBehaviour {

	public Text levelText;
	string level;

	// Use this for initialization
	void Start () {
		level = LevelManager.ReturnLevelNumber().ToString();
		levelText.text = "Test: "+level;
		if(Camera2DFollow.firstSlowPan == false){
			SetInactive();
		}

	}
	public void SetInactive(){
		gameObject.SetActive(false);
	}
}
