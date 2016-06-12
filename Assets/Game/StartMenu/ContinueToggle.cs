using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueToggle : MonoBehaviour {
	public Button continueButton;
	// Use this for initialization
	void Start () {
		if(PlayerPrefsManager.ReturnMaxLevel() > 1){
			continueButton.interactable = true;
		}else {
			continueButton.interactable = false;
		}
	}
}
