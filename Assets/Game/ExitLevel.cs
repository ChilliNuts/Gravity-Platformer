using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {

	LevelManager levelManager;
	PlayerController player;
	Animator anim;
	public float loadNextLevelAfter = 0f;
	public GameObject exitFX;
	public AudioClip exitSFX;
	bool exited = false;
	CubeFollow cube;

	void Start(){
		levelManager = FindObjectOfType<LevelManager>();
		player = FindObjectOfType<PlayerController>();
		anim = GetComponent<Animator>();
		cube = FindObjectOfType<CubeFollow>();
	}

	void OnTriggerStay2D(Collider2D trigger){

		if(trigger.gameObject == player.gameObject && CorrectRotation (trigger) && !exited){
			exited = true;
			//Exit();
			Invoke("Exit", 0.15f);
		}
	}
	bool CorrectRotation(Collider2D trigger){
		int thisRot = Mathf.Abs ((int)transform.rotation.eulerAngles.z);
		int triggerRot = Mathf.Abs ((int)trigger.gameObject.transform.rotation.eulerAngles.z);
		if(thisRot == triggerRot) return true;
		else return false;
	}

	void Exit(){
		AudioSource.PlayClipAtPoint(exitSFX, transform.position);
		Instantiate(exitFX, player.transform.position, transform.rotation);
		player.GetComponent<Gun>().canFireLazer = false;
		player.cameraTargetChild.GetComponent<CameraTarget>().playerIsDead = true;
		player.cameraTargetChild.transform.parent = null;
		if (cube != null) {
			cube.OnExit ();
		}
		player.transform.position = new Vector3(-666, -666, -666);
		anim.SetTrigger("playerExit");
		Camera2DFollow.firstSlowPan = true;
		Invoke("NextLevel", loadNextLevelAfter);
	}

	void NextLevel(){
		levelManager.LoadNextLevel();
	}
}
