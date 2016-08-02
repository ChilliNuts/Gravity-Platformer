using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour {

	public Text statDisplay;

	// Use this for initialization
	void Start () {
		int totalDeaths = PlayerPrefsManager.ReturnStat("deaths");
		int totalBeamsFired = PlayerPrefsManager.ReturnStat("beamsFired");
		statDisplay.text += "\nYou died a total of "+ totalDeaths +" times,\n and fired a total of "
			+ totalBeamsFired +" Gravitron beams.";
	}
}
