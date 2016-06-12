using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public Camera mCamera;
	public GameObject target;
	public float overTime;

	public bool gameStarted;
	public float targetFieldOfView;

	float accMax = 15f;
	float acc = 0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted){
			MoveCameraToStart();
		}
	}

	void MoveCameraToStart(){
		Vector3 camPos = mCamera.transform.position;
		mCamera.transform.position = Vector3.SlerpUnclamped(mCamera.transform.position, target.transform.position, overTime*Time.deltaTime);
		camPos = mCamera.transform.position;
		camPos.z = -10f;
		mCamera.transform.position = camPos;
		if(mCamera.orthographicSize < targetFieldOfView){
			if(acc <= accMax){
				acc += 0.05f;
			}
			mCamera.orthographicSize += (overTime*acc*Time.deltaTime); 
		}else mCamera.orthographicSize = targetFieldOfView;

	}
}
