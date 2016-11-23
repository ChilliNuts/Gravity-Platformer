using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public bool canFireLazer = true;
	public GameObject lazerHitFX;
	public GameObject lazerFailFX;
	public Transform holdPosition;
	public static bool vControl = false;
	public float grabDistance;
	
	PlayerController player;
	FireBeam fireBeam;
	public GameObject heldObject;
	bool holdingObject;

	int objectLayer;
	float objectGravity;


	// Use this for initialization
	void Start () {
		canFireLazer = true;
		player = GetComponent<PlayerController>();
		fireBeam = FindObjectOfType<FireBeam>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0) && !player.rotating){
			if (!holdingObject) {
				if (canFireLazer && !player.playerDestroyed) {
					canFireLazer = false;
					#if UNITY_WEBGL
					PlayerPrefsManager.UpdateStats (0, 1);
					#endif
					#if UNITY_STANDALONE
					SaveManager.localShotsFired++;
					#endif
					FireLazer ();
				}
			}else DropBox(heldObject);
		}
	}

	void FixedUpdate(){
		if (heldObject != null) {
			HoldObject (heldObject, holdPosition.position);
		}
	}

	void FireLazer(){
		Vector3 shootDirection = Input.mousePosition;
		shootDirection = Camera.main.ScreenToWorldPoint (shootDirection);
		shootDirection = shootDirection - transform.position;

		RaycastHit2D hit = Physics2D.Raycast (transform.position, shootDirection, 300f, ~player.playerMask);

		StartCoroutine(fireBeam.Beam (hit));
		if (hit.collider.gameObject != null) {
			if (hit.collider.gameObject.tag == "walls") {
				LazerContact (hit);
				if (!player.magnetised) {
					player.ResetPlayerFloor ();
					StartCoroutine (player.RotatePlayer ());
				}
			} else if (hit.collider.gameObject.tag == "Box") {
				if (hit.distance <= grabDistance) {
					PickUpBox (hit.collider.gameObject);
				} else {
					Instantiate (lazerFailFX, hit.point, Quaternion.identity);
				}
			} else {
				Instantiate (lazerFailFX, hit.point, Quaternion.identity);
			}
		}
	}

	void LazerContact(RaycastHit2D hit){
		// replace with custom particles

		Instantiate (lazerHitFX, hit.point, Quaternion.identity);

		if(hit.normal == -Vector2.up){
			SetGravityDirection.gDirection = SetGravityDirection.GDirection.UP;
		}
		if(hit.normal == -Vector2.down){
			SetGravityDirection.gDirection = SetGravityDirection.GDirection.DOWN;
		}
		if(hit.normal == -Vector2.left){
			SetGravityDirection.gDirection = SetGravityDirection.GDirection.LEFT;
		}
		if(hit.normal == -Vector2.right){
			SetGravityDirection.gDirection = SetGravityDirection.GDirection.RIGHT;
		}
	}

	void PickUpBox(GameObject target){
		if (heldObject == null) {
			holdingObject = true;
			objectLayer = target.layer;
			objectGravity = target.GetComponent<Rigidbody2D> ().gravityScale;
			target.layer = 9;
			target.GetComponent<Rigidbody2D> ().gravityScale = 0f;
			target.GetComponent<Box> ().isHeld = true;
			heldObject = target;
		}
	}
	public void DropBox(GameObject target){
		if(heldObject != null){
			target.layer = objectLayer;
			target.GetComponent<Rigidbody2D> ().gravityScale = objectGravity;
			target.GetComponent<Box> ().isHeld = false;
			heldObject = null;
			holdingObject = false;
		}
	}

	void HoldObject(GameObject target, Vector3 hPos){
		target.transform.position = Vector3.Slerp(target.transform.position, hPos, 10f * Time.deltaTime);
	}


//	void LaunchBullet(){
//		Vector3 mPos = Input.mousePosition;
//		Offset = Camera.main.ScreenToWorldPoint(mPos) - transform.position;
//		Offset.z = 0f;
//		Offset.Normalize ();
//		Offset = Offset * offsetAmount;
//		
//		GameObject currentBullet = Instantiate(gBullet, transform.position /*+ Offset*/, 
//		                                       Quaternion.identity) as GameObject; 
//		Rigidbody2D bulletRB = currentBullet.GetComponent<Rigidbody2D>();
//		Vector3 shootDirection = Input.mousePosition;
//		shootDirection = Camera.main.ScreenToWorldPoint (shootDirection);
//		shootDirection = shootDirection - transform.position;
//		shootDirection.z = 0.0f;
//		
//		if(shootDirection != Vector3.zero){
//			shootDirection.Normalize();
//		}
//		bulletRB.velocity = shootDirection * bulletSpeed;
//	}
}


//if (hit != null) {
//	if (hit.collider.gameObject.tag == "walls") {
//		LazerContact (hit);
//		if (!player.magnetised) {
//			player.ResetPlayerFloor ();
//			StartCoroutine (player.RotatePlayer ());
//		}
//		return;
//	}	
//
//	if (hit.collider.gameObject.tag == "Box") {
//		if (hit.distance <= grabDistance) {
//			PickUpBox (hit.collider.gameObject);
//			return;
//		} 
//	}
//
//	Instantiate (lazerFailFX, hit.point, Quaternion.identity);
//
//}