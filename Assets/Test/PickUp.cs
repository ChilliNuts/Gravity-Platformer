using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public GameObject player;
	public ParticleSystem gunPickupFX;
	public AudioClip pickupSFX;
	public GameObject[] noGun;
	public GameObject withGun;
	public GameObject gunBarrel;
	public LookAtMouse lookAtMouse;
	float yPos;

	void Update(){
		Bob ();
	}
	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject == player.gameObject){
			
			CustomCursor cursor = Camera.main.GetComponent<CustomCursor>();
			cursor.useCustomCursor = true;
			cursor.updateCursor = true;

			AudioSource.PlayClipAtPoint(pickupSFX, transform.position);

			foreach (GameObject arm in noGun){
				arm.SetActive(false);
			}
			withGun.SetActive(true);
			gunBarrel.GetComponent<FireBeam>().lazerSpawner = GameObject.FindGameObjectWithTag("GunTip");

			gunPickupFX.loop = false;
			player.GetComponent<Gun>().enabled = true;
			lookAtMouse.flip = true;


			Destroy (gameObject);
		}
	}
	void Bob(){
		yPos = transform.position.y - 0.025f + Mathf.PingPong(Time.time * 0.1f, 0.05f);
		transform.position = new Vector3(transform.position.x, yPos , transform.position.z);
	}
}
