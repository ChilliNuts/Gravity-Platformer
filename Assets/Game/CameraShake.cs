using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float shakeMagnitude;
	public float shakeAmount;
	public float shakeDelay;

	IEnumerator CamShake(){
		float tempMag = shakeMagnitude;
		float tempDelay = shakeDelay;
		for(int i = 0; i <= shakeAmount; i++){
			float newX = transform.position.x +  Random.Range(-tempMag, tempMag);
			float newY = transform.position.y +  Random.Range(-tempMag, tempMag);
			Vector3 newPos = new Vector3(newX, newY, transform.position.z);
			transform.position = newPos;

			yield return new WaitForSeconds(tempDelay);
			tempMag *= 0.8f;
			tempDelay *= 1.1f;
		}
	}
	public void Shake(){
		StartCoroutine(CamShake());
	}
}
