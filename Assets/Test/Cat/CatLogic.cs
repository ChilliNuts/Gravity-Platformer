using UnityEngine;
using System.Collections;

public class CatLogic : MonoBehaviour {


	public float guardRadius = 20;
	public float speed = 10;
	public float jumpForce = 20;
	public float maxSpeed = 10;
	PlayerController player;
	Rigidbody2D myBody;
	float distance;
	bool grounded;
	Vector2 chaseDir;
	public LayerMask selfMask;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		myBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			distance = Vector2.Distance (transform.position, player.transform.position);
			chaseDir = player.transform.position - transform.position;
			
			
			if(distance <= guardRadius){
				RaycastHit2D hit = Physics2D.Raycast (transform.position, chaseDir, distance, ~selfMask);
				if(hit.collider.gameObject == player.gameObject){
					AttackPlayer ();
				}
		}

		}
	}

	void AttackPlayer(){
		print ("attack");
		if(myBody.velocity.sqrMagnitude <= maxSpeed){
			myBody.AddForce(chaseDir * speed);
		}
		if(grounded){
			myBody.velocity += (Vector2.up * jumpForce);
			grounded = false;
		}
	}
	void OnCollisionEnter2D(){
		grounded = true;
	}
	void OnCollisionExit2D(){
		grounded = false;
	}

}
