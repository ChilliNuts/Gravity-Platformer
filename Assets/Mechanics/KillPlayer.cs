using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	PlayerController player;
	Destructable[] destrucables;
	public AudioClip playerHitSFX;

	void Start(){
		player = FindObjectOfType<PlayerController>();
		destrucables = GameObject.FindObjectsOfType<Destructable>();
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject == player.gameObject){
			print("hit");
			player.DestroyPlayer ();
			if (playerHitSFX != null) {
				AudioSource.PlayClipAtPoint (playerHitSFX, transform.position);
			}
		}else foreach (Destructable d in destrucables){
			if(trigger.gameObject == d.gameObject){
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
		}else foreach (Destructable d in destrucables){
				if(collision.gameObject == d.gameObject){
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
		}else foreach (Destructable d in destrucables){
				if(trigger.gameObject == d.gameObject){
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
		}else foreach (Destructable d in destrucables){
				if(collision.gameObject == d.gameObject){
					d.Destruct ();
				}
			}
	}
}
