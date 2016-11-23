using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	[HideInInspector] public bool grounded = false;
	[HideInInspector] public bool rotating = false;
	public Transform groundedTarget1;
	public Transform groundedTarget2;
	public LayerMask playerMask;
	public GameObject deathFX;
	public GameObject playerGhost;
	[HideInInspector] public bool magnetised = false;
	public enum FloorDirection {DOWN, UP, LEFT, RIGHT};
	[HideInInspector] public FloorDirection floorDirection;
	public GameObject cameraTargetChild;
	[HideInInspector] public bool facingRight = true;
	Quaternion targetRot = Quaternion.identity;
	public AudioClip hitGroundSFX;
	public AudioClip dieSFX;
	Rigidbody2D rB2D;
	Animator anim;
	[HideInInspector] public bool playerDestroyed = false;
	[HideInInspector] public bool canMove = true;
	public GameObject entryFX;
	Gun gun;
	ParticleSystem dust;
	CameraShake cShake;
	bool onMPlatform;

	

	// Use this for initialization
	void Start () {
		rB2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		cameraTargetChild = transform.FindChild("Camera Target").gameObject;
		Instantiate(entryFX, transform.position, transform.rotation);
		gun = GetComponent<Gun>();
		dust = GetComponentInChildren<ParticleSystem>();
		cShake = Camera.main.GetComponent<CameraShake>();
	}
	void FixedUpdate(){
		if (canMove) {
			Move ();
		}
	}
	// Update is called once per frame
	void Update () {
		
		if (canMove) {
			Jump ();
		}else if(anim.GetBool("boolRun")) {
			anim.SetBool("boolRun", false);
		}

		// Check if grounded
		if(Physics2D.Linecast(groundedTarget1.transform.position, groundedTarget2.transform.position, ~playerMask)){
			//Debug.DrawLine (transform.position, groundedTarget.transform.position, Color.green);
			if (!grounded) {
				grounded = true;
				AudioSource.PlayClipAtPoint (hitGroundSFX, transform.position);
				dust.Play();
				if (!anim.GetBool ("boolGrounded")) {
					anim.SetBool ("boolGrounded", true);
				}
			}
		}else {
			if (grounded) {
				grounded = false;
				dust.Play();
				if (anim.GetBool ("boolGrounded")) {
					anim.SetBool ("boolGrounded", false);
				}
			}
		}
		if(magnetised){
			CheckRotation();
		}
	}

	public IEnumerator RotatePlayer(){


		if(rotating)yield return new WaitForSeconds(0.45f);
		if (!rotating) {
			rotating = true;
			if (floorDirection == FloorDirection.UP) {
				targetRot.eulerAngles = new Vector3 (0, 0, 180);
				while (transform.rotation.eulerAngles != targetRot.eulerAngles) {
					transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 720f * Time.deltaTime);
					yield return null;
				}
			} else if (floorDirection == FloorDirection.DOWN) {
				targetRot.eulerAngles = new Vector3 (0, 0, 0);
				while (transform.rotation.eulerAngles != targetRot.eulerAngles) {
					transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 720f * Time.deltaTime);
					yield return null;
				}
			} else if (floorDirection == FloorDirection.LEFT) {
				targetRot.eulerAngles = new Vector3 (0, 0, 270);
				while (transform.rotation.eulerAngles != targetRot.eulerAngles) {
					transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 720f * Time.deltaTime);
					yield return null;
				}
			} else if (floorDirection == FloorDirection.RIGHT) {
				targetRot.eulerAngles = new Vector3 (0, 0, 90);
				while (transform.rotation.eulerAngles != targetRot.eulerAngles) {
					transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 720f * Time.deltaTime);
					yield return null;
				}
			}
		}
		rotating = false;

		yield return null;
	}
		

	void Move(){
		Vector2 moveVel = rB2D.velocity;

		if (floorDirection == FloorDirection.UP ||
		    floorDirection == FloorDirection.DOWN) {

			//Start/Stop run animation
			if (grounded) {
				if(Input.GetAxis ("Horizontal") != 0 && !anim.GetBool("boolRun")){
					anim.SetBool("boolRun", true);
				}else if(Input.GetAxis ("Horizontal") == 0 && anim.GetBool("boolRun")){
					anim.SetBool("boolRun", false);
				}
				if (!magnetised) {
					if (Mathf.Abs (moveVel.x) < moveSpeed * 0.55f) {
						moveVel.x += Input.GetAxisRaw ("Horizontal") * moveSpeed * 10;
						rB2D.AddForce (moveVel);
					}
				}else {
					moveVel.x = Input.GetAxis ("Horizontal") * moveSpeed * 0.5f;
					rB2D.velocity = moveVel;
				}
			}else if(Mathf.Abs ( moveVel.x) < moveSpeed * 0.55f){
				moveVel.x += Input.GetAxisRaw ("Horizontal") * moveSpeed * 5;
				rB2D.AddForce(moveVel);
			}


			//Flip player facing
			if (floorDirection == FloorDirection.DOWN) {
				if (Input.GetAxis ("Horizontal")  > 0 && !facingRight)
					Flip ();
				else if (Input.GetAxis ("Horizontal")  < 0 && facingRight)
					Flip ();
			}else if (floorDirection == FloorDirection.UP) {
				if (Input.GetAxis ("Horizontal")  > 0 && facingRight)
					Flip ();
				else if (Input.GetAxis ("Horizontal")  < 0 && !facingRight)
					Flip ();
			}
		}
		if (floorDirection == FloorDirection.LEFT ||
		    floorDirection == FloorDirection.RIGHT) {

			//Start/Stop run animation
			if (grounded) {
				if(Input.GetAxis ("Vertical") != 0 && !anim.GetBool("boolRun")){
					anim.SetBool("boolRun", true);
				}else if(Input.GetAxis ("Vertical") == 0 && anim.GetBool("boolRun")){
					anim.SetBool("boolRun", false);
				}
				if (!magnetised) {
					if (Mathf.Abs (moveVel.y) < moveSpeed * 0.55f) {
						moveVel.y += Input.GetAxisRaw ("Vertical") * moveSpeed * 10;
						rB2D.AddForce (moveVel);
					}
				}else{
					moveVel.y = Input.GetAxis ("Vertical") * moveSpeed * 0.5f;
					rB2D.velocity = moveVel;
				}
			}else if(Mathf.Abs ( moveVel.y) < moveSpeed * 0.55f){
				moveVel.y += Input.GetAxisRaw ("Vertical") * moveSpeed * 5;
				rB2D.AddForce(moveVel);
			}


			//Flip player facing
			if (floorDirection == FloorDirection.RIGHT) {
				if (Input.GetAxis ("Vertical")  > 0 && !facingRight)
					Flip ();
				else if (Input.GetAxis ("Vertical")  < 0 && facingRight)
					Flip ();
			}else if (floorDirection == FloorDirection.LEFT) {
				if (Input.GetAxis ("Vertical")  > 0 && facingRight)
					Flip ();
				else if (Input.GetAxis ("Vertical")  < 0 && !facingRight)
					Flip ();
			}
		}
	}

	void Jump(){
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
			
			if (grounded && floorDirection == FloorDirection.DOWN) {
				//rB2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				rB2D.velocity += jumpForce * Vector2.up;
				grounded = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
			
			if (grounded && floorDirection == FloorDirection.UP) {
				rB2D.velocity += jumpForce * Vector2.down;
				grounded = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
			
			if (grounded && floorDirection == FloorDirection.RIGHT) {
				rB2D.velocity += jumpForce * Vector2.left;
				grounded = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
			
			if (grounded && floorDirection == FloorDirection.LEFT) {
				rB2D.velocity += jumpForce * Vector2.right;
				grounded = false;
			}
		}
	}
		
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void DestroyPlayer(){
		if (!playerDestroyed) {
			cShake.Shake();
			#if UNITY_WEBGL
			PlayerPrefsManager.UpdateStats (1, 0);
			#endif
			#if UNITY_STANDALONE
			SaveManager.localDeaths++;
			#endif
			cameraTargetChild.GetComponent<CameraTarget>().playerIsDead = true;
			playerDestroyed = true;
			transform.FindChild("CubeFollowTarget").parent = null;
		}
		AudioSource.PlayClipAtPoint(dieSFX, transform.position);
		Instantiate(deathFX, transform.position, Quaternion.identity);
		Instantiate(playerGhost, transform.position, Quaternion.identity);
		cameraTargetChild.transform.parent = null;
		gun.DropBox(gun.heldObject);
		transform.position = new Vector3(-666, -666, -666);
		rB2D.velocity = Vector2.zero;
		rB2D.gravityScale = 0;

	}
	void OnCollisionEnter2D(Collision2D col){
		if (!onMPlatform) {
			if (col.gameObject.GetComponentInParent<MovingPlatform> ()) {
				transform.parent = col.gameObject.transform.parent;
				onMPlatform = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D col){
		if (!onMPlatform) {
			if (col.gameObject.GetComponentInParent<MovingPlatform> ()) {
				transform.parent = col.gameObject.transform.parent;
				onMPlatform = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (transform.parent != null && col.gameObject.layer != 14) {
			transform.parent = null;
			onMPlatform = false;
		}
	}
	public void ResetPlayerFloor(){
		if(SetGravityDirection.gDirection == SetGravityDirection.GDirection.DOWN){
			floorDirection = PlayerController.FloorDirection.DOWN;
		}else if(SetGravityDirection.gDirection == SetGravityDirection.GDirection.UP){
			floorDirection = PlayerController.FloorDirection.UP;
		}else if(SetGravityDirection.gDirection == SetGravityDirection.GDirection.LEFT){
			floorDirection = PlayerController.FloorDirection.LEFT;
		}else if(SetGravityDirection.gDirection == SetGravityDirection.GDirection.RIGHT){
			floorDirection = PlayerController.FloorDirection.RIGHT;
		}
	}
	void CheckRotation(){
		if (floorDirection == FloorDirection.UP) {
			if(transform.rotation.eulerAngles != new Vector3 (0, 0, 180)){
				if (!rotating) {
					StartCoroutine(RotatePlayer());
				}
			}
		} else if (floorDirection == FloorDirection.DOWN) {
			if(transform.rotation.eulerAngles != new Vector3 (0, 0, 0)){
				if (!rotating) {
					StartCoroutine(RotatePlayer());
				}
			}
		} else if (floorDirection == FloorDirection.LEFT) {
			if(transform.rotation.eulerAngles != new Vector3 (0, 0, 270)){
				if (!rotating) {
					StartCoroutine(RotatePlayer());
				}
			}
		} else if (floorDirection == FloorDirection.RIGHT) {
			if(transform.rotation.eulerAngles != new Vector3 (0, 0, 90)){
				if (!rotating) {
					StartCoroutine(RotatePlayer());
				}
			}
		}
	}
}
