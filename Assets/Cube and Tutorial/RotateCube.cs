using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {

	public Vector2 WaitMinMax;
	float waitTime = 2f;
	float originalRotSpeed;
	float rotSpeed = 3.5f;
	float timer = 0f;
	Quaternion newTargetRot;
	int[] randomInts = new int[] {-180, -90, -90, -90, 0, 90, 90, 90, 180};
	[HideInInspector] public bool canRotate = true;

	// Use this for initialization
	void Start () {
		SetTargetRot(transform.rotation);
		originalRotSpeed = rotSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= waitTime && canRotate){
			RotateCubeTo(transform.rotation, rotSpeed);

		}
	}
	void RotateCubeTo(Quaternion cRot,float t){
		
		transform.rotation = Quaternion.Slerp(cRot,newTargetRot,t*Time.deltaTime);
		if(timer >= waitTime + 1f){
			transform.rotation = newTargetRot;
			timer = 0f;
			SetTargetRot(transform.rotation);
			waitTime = Random.Range(WaitMinMax.x, WaitMinMax.y);
			rotSpeed = originalRotSpeed + waitTime*0.5f;
		}
	}
	void SetTargetRot(Quaternion rot){
		Vector3 nRot = rot.eulerAngles; 
		newTargetRot.eulerAngles = new Vector3(nRot.x + randomInts[Random.Range(0,randomInts.Length)], nRot.y, nRot.z + randomInts[Random.Range(0,randomInts.Length)]);
	}

}
