using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ButtonSwitchesOn))]
public class SwitchLazer : MonoBehaviour {

	ButtonSwitchesOn lazerSwitch;
	LineRenderer lineRend;
	BoxCollider2D box;
	ParticleSystem particles;
	ParticleSystem.EmissionModule particleEmission;
	AudioSource audioSource;

	public bool repeating;
	public float initialDelay;
	public float secsOn;
	public float secsOff;
	public Vector2 awakeDelaySleepDelay = new Vector2(1, 2);
	public bool on = true;



	// Use this for initialization
	void Start () {
		lazerSwitch = GetComponent<ButtonSwitchesOn>();
		lineRend = GetComponent<LineRenderer>();
		box = GetComponent<BoxCollider2D>();
		particles = GetComponent<ParticleSystem>();
		audioSource = GetComponent<AudioSource>();
		particleEmission = particles.emission;
		if(!on){
			lineRend.enabled = false;
			box.enabled = false;
			particleEmission.enabled = false;
		}
		if(secsOn + secsOff > 0){
			if(repeating){
				StartCoroutine ("OffOnRepeating");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!repeating) {

			if (lazerSwitch != null) {
				if (lazerSwitch.switchState == ButtonSwitchesOn.SwitchState.ON) {
					FlipOnOff ();
					lazerSwitch.switchState = ButtonSwitchesOn.SwitchState.IDLE;
				} else if (lazerSwitch.switchState == ButtonSwitchesOn.SwitchState.OFF) {
					FlipOnOff ();
					lazerSwitch.switchState = ButtonSwitchesOn.SwitchState.IDLE;
				}
			}
		}
	}
	public void FlipOnOff(){
		if(!on){
			StopCoroutine ("TurnOffLazer");
			StartCoroutine ("TurnOnLazer");
			audioSource.Play();
			on = true;
		}else if (on){
			StopCoroutine ("TurnOnLazer");
			StartCoroutine ("TurnOffLazer");
			audioSource.Stop();
			on = false;
		}
	}
	IEnumerator TurnOffLazer(){
		lineRend.enabled = false;
		box.enabled = false;
		yield return new WaitForSeconds(awakeDelaySleepDelay.y);
		particleEmission.enabled = false;
	}
	IEnumerator TurnOnLazer(){
		particleEmission.enabled = true;
		yield return new WaitForSeconds(awakeDelaySleepDelay.x);
		lineRend.enabled = true;
		box.enabled = true;
	}
	IEnumerator OffOnRepeating(){
		yield return new WaitForSeconds (initialDelay);
		while (repeating) {	
			lineRend.enabled = false;
			box.enabled = false;
			yield return new WaitForSeconds (awakeDelaySleepDelay.y);
			particleEmission.enabled = false;
			audioSource.Stop();
			
			yield return new WaitForSeconds (secsOff);
			
			particleEmission.enabled = true;
			yield return new WaitForSeconds (awakeDelaySleepDelay.x);
			lineRend.enabled = true;
			box.enabled = true;
			audioSource.Play();

			yield return new WaitForSeconds (secsOn);
		}
	}
}
