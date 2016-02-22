using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public GameObject player;
	public ParticleSystem gunPickupFX;
	public AudioClip pickupSFX;
	float yPos;

	void Update(){
		Bob ();
	}
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject == player.gameObject){
			AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
			player.GetComponent<Gun>().enabled = true;
			gunPickupFX.loop = false;
			Destroy (gameObject);
		}
	}
	void Bob(){
		yPos = transform.position.y - 0.025f + Mathf.PingPong(Time.time * 0.1f, 0.05f);
		transform.position = new Vector3(transform.position.x, yPos , transform.position.z);
	}
}
