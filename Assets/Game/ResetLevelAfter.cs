using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevelAfter : MonoBehaviour {

	public float resetAfterSecs;
	

	// Update is called once per frame
	void Update () {
		Invoke ("ResetLevel", resetAfterSecs);
	}

	void ResetLevel(){
		FindObjectOfType<LevelManager>().FadeOutAndLoad (SceneManager.GetActiveScene().buildIndex);
	}

}
