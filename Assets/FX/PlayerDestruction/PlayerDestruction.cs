using UnityEngine;
using System.Collections;

public class PlayerDestruction : MonoBehaviour {

	public GameObject smokePrefab;
	public Vector2 scaleMinMax;
	public float destroyAfterSecs;
	public AudioClip explode;

	void Start(){
		if(destroyAfterSecs > 0){
			DestroyAfter (destroyAfterSecs);
		}
	}


	void OnCollisionEnter2D(){
			DestroyFX ();
	}

	void DestroyFX (){
		AudioSource.PlayClipAtPoint(explode, transform.position);
		GameObject smoke = Instantiate (smokePrefab, transform.position, Quaternion.identity) as GameObject;
		float newScale = Random.Range (scaleMinMax.x, scaleMinMax.y);
		smoke.transform.localScale = new Vector3 (newScale, newScale, smoke.transform.localScale.z);
		Destroy (gameObject);
	}

	

	
	IEnumerator DestroyAfter(float time){
		yield return new WaitForSeconds(time);
		DestroyFX ();
	}
	
	
	public void DestroyGameObject () {
		Destroy (gameObject);
	}
}
