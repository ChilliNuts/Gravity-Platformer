using UnityEngine;
using System.Collections;

public class FireBeam : MonoBehaviour {

	LineRenderer beam;
	GameObject lazerSpawner;
	public float beamLife = 1f;
	public AudioClip beamSFX;
	Gun gun;


	// Use this for initialization
	void Start () {
		beam = GetComponent<LineRenderer>();
		gun = FindObjectOfType<PlayerController>().GetComponent<Gun>();
		lazerSpawner = GameObject.Find("GunTip");
		beam.enabled = false;
	}
	
	public IEnumerator Beam(RaycastHit2D rayHit){
		AudioSource.PlayClipAtPoint(beamSFX, Camera.main.transform.position, 0.6f);
		float storedBeamLife = beamLife;
		float counter = 0f;
		float endWidth = 0.8f;
		float startWidth = 0.3f;
		beam.enabled = true;
		transform.position = lazerSpawner.transform.position;

		beam.SetPosition(0, transform.position);
		beam.SetPosition(1, rayHit.point);

		while (counter <= storedBeamLife){
			endWidth -= Time.deltaTime * 0.8f;
			startWidth -= Time.deltaTime * 0.3f;
			beam.SetWidth (startWidth , endWidth);
			counter += Time.deltaTime;
			yield return null;
		}
		gun.canFireLazer = true;
		beam.enabled = false;
	}
}
