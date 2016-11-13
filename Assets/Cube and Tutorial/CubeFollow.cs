using UnityEngine;
using System.Collections;

public class CubeFollow : MonoBehaviour {

	public GameObject followTarget;
	public float followSpeed;
	public float minDistanceToExit = 10f;
	public GameObject exitFX;
	ExitLevel exit;
	PlayerController player;

	void Start(){
		exit = FindObjectOfType<ExitLevel>();
		player = FindObjectOfType<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		NearExit();
		Bob();
		if(player != null && player.playerDestroyed){
			//player.transform.FindChild("CubeFollowTarget").parent = null;
			GameObject.FindGameObjectWithTag("CubeToFollow").transform.parent = null;
		}
		if(followTarget != null){
			transform.position = Vector3.Slerp(transform.position, followTarget.transform.position, followSpeed*Time.deltaTime);
		}
	}
	void Bob(){
		float yPos = transform.position.y + 0.025f - Mathf.PingPong(Time.time * 0.04f, 0.05f);
		transform.position = new Vector3(transform.position.x, yPos , transform.position.z);
	}
	void NearExit(){
		if(followTarget != null){
			float d = Vector2.Distance(followTarget.transform.position, exit.gameObject.transform.position);
			if(d < minDistanceToExit){
				followTarget = exit.gameObject;
			}
		}
	}
	public void OnExit(){
		AudioSource.PlayClipAtPoint(exit.exitSFX, transform.position);
		GameObject eFX = Instantiate(exitFX, transform.position, Quaternion.identity)as GameObject;
		eFX.transform.localScale *= 0.25f;
		followTarget = null;
		foreach(CubeColours cubeSide in GetComponentsInChildren<CubeColours>()){
			cubeSide.gameObject.transform.position = new Vector3(-666,-666,-666);
		}
		GetComponent<RotateCube>().canRotate = false;
		GetComponent<ParticleSystem>().Stop(true);
		//GetComponentInChildren<ParticleSystem>().Stop();
	}
}
