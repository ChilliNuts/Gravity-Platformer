using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour {

	public Text statDisplay;

	// Use this for initialization
	void Start () {
		#if UNITY_WEBGL
		int totalDeaths = PlayerPrefsManager.ReturnStat("deaths");
		int totalBeamsFired = PlayerPrefsManager.ReturnStat("beamsFired");
		#endif
		#if UNITY_STANDALONE
		int totalDeaths = SaveManager.localDeaths;
		int totalBeamsFired = SaveManager.localShotsFired;
		#endif
		statDisplay.text += "\nYou died a total of "+ totalDeaths +" times,\n and fired a total of "
			+ totalBeamsFired +" Gravitron beams.";
	}
}
