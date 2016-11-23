using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueToggle : MonoBehaviour {
	public Button continueButton;
	// Use this for initialization
	void Start () {
		#if UNITY_WEBGL
		if(PlayerPrefsManager.ReturnMaxLevel() > 1){
			continueButton.interactable = true;
		}else {
			continueButton.interactable = false;
		}
		#endif
		#if UNITY_STANDALONE
		if(SaveManager.localMaxLevel > 1){
			continueButton.interactable = true;
		}else {
			continueButton.interactable = false;
		}
		#endif
	}
}
