using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {

	LevelManager levelManager;
	PlayerController player;
	public float loadNextLevelAfter = 0f;
	public GameObject exitFX;
	public AudioClip exitSFX;
	bool exited = false;

	void Start(){
		levelManager = FindObjectOfType<LevelManager>();
		player = FindObjectOfType<PlayerController>();
	}

	void OnTriggerStay2D(Collider2D trigger){

		if(trigger.gameObject == player.gameObject && CorrectRotation (trigger) && !exited){
			exited = true;
			Invoke("Exit", 0.25f);
		}
	}
	bool CorrectRotation(Collider2D trigger){
		string thisRot = Mathf.Abs ((int)transform.rotation.eulerAngles.z).ToString ();
		string triggerRot = Mathf.Abs ((int)trigger.gameObject.transform.rotation.eulerAngles.z).ToString ();
		print (thisRot +" , "+ triggerRot);
		if(thisRot == triggerRot) return true;
		else return false;
	}

	void Exit(){
		AudioSource.PlayClipAtPoint(exitSFX, transform.position);
		Instantiate(exitFX, player.transform.position, player.transform.rotation);
		player.cameraTargetChild.transform.parent = null;
		player.transform.position = new Vector3(666, 666, 666);
		Camera2DFollow.firstSlowPan = true;
		Invoke("NextLevel", loadNextLevelAfter);
	}

	void NextLevel(){
		levelManager.LoadNextLevel();
	}
}
