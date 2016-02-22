using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float damping = 1f;
    public float lookAheadFactor = 3f;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

	public float slowPanDamping = 10f;
	public static bool firstSlowPan = true;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

	private bool beforeMove;
	private float xMoveDelta;
	private static Vector3 playerDeathPos;
	float o_Damping;
	float o_LookAheadFactor;
	float o_LookAheadReturnSpeed;
	float o_LookAheadMoveThreshold;

    // Use this for initialization
    private void Start()
    {
		o_Damping = damping;
		o_LookAheadFactor = lookAheadFactor;
		o_LookAheadMoveThreshold = lookAheadMoveThreshold;
		o_LookAheadReturnSpeed = lookAheadReturnSpeed;

        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;

		if(firstSlowPan){
			transform.position = GameObject.FindGameObjectWithTag("Exit").transform.position + Vector3.forward * m_OffsetZ;
			beforeMove = true;
			firstSlowPan = false;
		}else transform.position = playerDeathPos;
    }


    // Update is called once per frame
    private void Update()
    {

		if(beforeMove){
			xMoveDelta = (target.position - m_LastTargetPosition).x;
			if(Mathf.Abs (xMoveDelta) > 0.01f){
				beforeMove = false;
			}

			Vector3 aheadTargetPos = target.position + Vector3.forward * m_OffsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, slowPanDamping);
			
			transform.position = newPos;

			m_LastTargetPosition = target.position;
		}
		else if (target != null) {
			// only update lookahead pos if accelerating or changed direction
			xMoveDelta = (target.position - m_LastTargetPosition).x;
        	
			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;
        	
			if (updateLookAheadTarget) {
				m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
			} else {
				m_LookAheadPos = Vector3.MoveTowards (m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
			}
        	
			Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
        	
			transform.position = newPos;
        	
			m_LastTargetPosition = target.position;
			playerDeathPos = target.position;
		}
    }


	public void MouseLookOn(float damp, float lAFactor, float lAReturnSpeed, float lAMoveThreshold){
		damping = damp;
		lookAheadFactor = lAFactor;
		lookAheadReturnSpeed = lAReturnSpeed;
		lookAheadMoveThreshold = lAMoveThreshold;
	}
	public void MouseLookOff(){
		damping = o_Damping;
		lookAheadFactor = o_LookAheadFactor;
		lookAheadReturnSpeed = o_LookAheadReturnSpeed;
		lookAheadMoveThreshold = o_LookAheadMoveThreshold;
	}
}
