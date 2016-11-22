using UnityEngine;
using System.Collections;
[RequireComponent(typeof (MovingPlatform))]
public class WallCrash : MonoBehaviour {

	MovingPlatform platform;
	public AudioClip SFX;
	bool activated;
	ParticleSystem dustParticles;

	// Use this for initialization
	void Start () {
		platform = GetComponent<MovingPlatform>();
		dustParticles = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated) {
			if (platform.transform.position == platform.waypoints [platform.targetWaypointIndex].transform.position) {
				AudioSource.PlayClipAtPoint (SFX, transform.position, 1f);
				activated = true;
				dustParticles.Play();
			}
		}
	}
}
