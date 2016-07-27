using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	PlayerController player;
	public AudioClip playerHitSFX;

	void Start(){
		player = FindObjectOfType<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject == player.gameObject){
			player.DestroyPlayer ();
			if (playerHitSFX != null) {
				AudioSource.PlayClipAtPoint (playerHitSFX, transform.position);
			}
		}else {
			Destructable d = trigger.gameObject.GetComponent<Destructable>();
			if(d != null){
				d.Destruct ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject == player.gameObject){
			player.DestroyPlayer ();
			if (playerHitSFX != null) {
				AudioSource.PlayClipAtPoint (playerHitSFX, transform.position);
			}
		}else {
			Destructable d = collision.gameObject.GetComponent<Destructable>();
			if(d != null){
				d.Destruct ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D trigger){
		if(trigger.gameObject == player.gameObject){
			player.DestroyPlayer ();
			if (playerHitSFX != null) {
				AudioSource.PlayClipAtPoint (playerHitSFX, transform.position);
			}
		}else {
			Destructable d = trigger.gameObject.GetComponent<Destructable>();
			if(d != null){
				d.Destruct ();
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject == player.gameObject){
			player.DestroyPlayer ();
			if (playerHitSFX != null) {
				AudioSource.PlayClipAtPoint (playerHitSFX, transform.position);
			}
		}else {
			Destructable d = collision.gameObject.GetComponent<Destructable>();
			if(d != null){
				d.Destruct ();
			}
		}
	}
}
