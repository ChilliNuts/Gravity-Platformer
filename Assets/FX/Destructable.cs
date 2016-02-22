using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
	
	public GameObject destructFX;
	public AudioClip destructSFX;

	public void Destruct(){
		AudioSource.PlayClipAtPoint(destructSFX, transform.position, (100/1/(Vector2.Distance(transform.position, Camera.main.transform.position) * 1f)));
		Instantiate(destructFX, transform.position, Quaternion.identity);
		if(GetComponent<Box>() != null){
			transform.position = new Vector2(Random.Range(-1000, -2000), Random.Range(-1000, -2000));
			GetComponent<Box>().StartCoroutine("Respawn");
		}else {
			Destroy (gameObject);
		}
	}
}
