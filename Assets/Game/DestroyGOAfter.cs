using UnityEngine;
using System.Collections;

public class DestroyGOAfter : MonoBehaviour {

	public float destroyAfterSecs;

	void Start(){
		if(destroyAfterSecs > 0){
			DestroyAfter (destroyAfterSecs);
		}
	}

	IEnumerator DestroyAfter(float time){
		yield return new WaitForSeconds(time);
		DestroyGameObject();
	}


	public void DestroyGameObject () {
		Destroy (gameObject);
	}
}
