using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

	public float distance = 3;
	public float aimTolerance = 5;
	public Transform center;
	Camera2DFollow followCam;

	public float m_Damping = 1f;
	public float m_LookAheadFactor = 3f;
	public float m_LookAheadReturnSpeed = 0.5f;
	public float m_LookAheadMoveThreshold = 0.1f;


	void Start(){
		followCam = FindObjectOfType<Camera2DFollow>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			followCam.MouseLookOn(m_Damping, m_LookAheadFactor, m_LookAheadReturnSpeed, m_LookAheadMoveThreshold);
		}
		if(Input.GetMouseButton(1)){
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(Camera.main.transform.position, mousePos) > aimTolerance) {
				Vector3 newPos = Vector3.zero;
				int deltaX = (int)(mousePos.x - center.position.x);
				newPos.x = deltaX;
				int deltaY = (int)(mousePos.y - center.position.y);
				newPos.y = deltaY;
				
				transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), distance) + center.position;
			}

		}else if(Input.GetMouseButtonUp (1)){
			followCam.MouseLookOff();
			transform.localPosition = Vector3.zero;
		}
	}
}
