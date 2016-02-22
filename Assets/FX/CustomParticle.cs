using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomParticle : MonoBehaviour {
	
	public int explodeInto = 1;
	public float explosionForce = 5f;
	public float upForce = 0f;
	public bool addRotation = false;
	public float shrinkRate = 0.75f;
	public int bounceNumber = 4;
	public Vector2 sizeOverTime;
	public Color color;
	public float destroyAfter = 10f;
	public List<GameObject> additionalParticles;

	Rigidbody2D myBody;
	int bounces = 0;

	void Start(){
		GetComponent<SpriteRenderer>().color = color;
		SpawnParticles ();
		if (explodeInto > 0) {
			foreach (GameObject adPart in additionalParticles) {
				SpawnAdditionalParticles (adPart);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetScale = new Vector2(sizeOverTime.x, sizeOverTime.x);
		transform.localScale = Vector2.Lerp(transform.localScale, targetScale, sizeOverTime.y * Time.deltaTime);
		if (bounces >= bounceNumber){
			Destroy (gameObject);
		}
		Invoke ("DestroyGO", destroyAfter);
	}

	void OnCollisionEnter2D(Collision2D collision){
		transform.localScale = transform.localScale * shrinkRate;
		bounces++;
	}
	void DestroyGO(){
		Destroy (gameObject);
	}

	void SpawnParticles (){
		for (int i = 0; i < explodeInto; i++) {
			Vector2 force = new Vector2 (Random.Range (-explosionForce, explosionForce), Random.Range (-explosionForce, explosionForce));
			GameObject particle = Instantiate (gameObject) as GameObject;
			myBody = particle.GetComponent<Rigidbody2D> ();
			particle.GetComponent<CustomParticle> ().explodeInto = 0;
			myBody.velocity = force;
			myBody.AddRelativeForce(Physics2D.gravity * upForce);
			if (addRotation) {
				float xPos = transform.position.x + Random.Range (-transform.localScale.x * 0.5f, +transform.localScale.x * 0.5f);
				float yPos = transform.position.y + Random.Range (-transform.localScale.y * 0.5f, +transform.localScale.y * 0.5f);
				Vector2 addRotPos = new Vector2 (xPos, yPos);
				myBody.AddForceAtPosition (force * 50, addRotPos);
			}
		}
	}
	void SpawnAdditionalParticles(GameObject toSpawn){
		Vector2 force = new Vector2 (Random.Range (-explosionForce, explosionForce), Random.Range (-explosionForce, explosionForce));
		GameObject newParticle = Instantiate (toSpawn, transform.position, Quaternion.identity) as GameObject;
		myBody = newParticle.GetComponent<Rigidbody2D> ();
		myBody.velocity = force;
		float xPos = transform.position.x + Random.Range (-transform.localScale.x * 0.5f, +transform.localScale.x * 0.5f);
		float yPos = transform.position.y + Random.Range (-transform.localScale.y * 0.5f, +transform.localScale.y * 0.5f);
		Vector2 addRotPos = new Vector2 (xPos, yPos);
		myBody.AddForceAtPosition (force * 50, addRotPos);
	}
}
