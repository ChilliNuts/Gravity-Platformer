using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public bool canFireLazer = true;
	public GameObject lazerHitFX;
	public static bool vControl = false;
	
	PlayerController player;
	FireBeam fireBeam;


	// Use this for initialization
	void Start () {
		canFireLazer = true;
		player = GetComponent<PlayerController>();
		fireBeam = FindObjectOfType<FireBeam>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && !player.rotating && !vControl){
			if (canFireLazer && !player.playerDestroyed){
				canFireLazer = false;
				PlayerPrefsManager.UpdateStats(0,1);
				FireLazer();
			}
		}
	}

	void FireLazer(){
		Vector3 shootDirection = Input.mousePosition;
		shootDirection = Camera.main.ScreenToWorldPoint (shootDirection);
		shootDirection = shootDirection - transform.position;

		RaycastHit2D hit = Physics2D.Raycast (transform.position, shootDirection, 300f, ~player.playerMask);

		StartCoroutine(fireBeam.Beam (hit));
		if (hit.collider.gameObject.tag == "walls") {
			LazerContact (hit);
			if (!player.magnetised) {
				player.ResetPlayerFloor ();
				StartCoroutine( player.RotatePlayer ());
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
