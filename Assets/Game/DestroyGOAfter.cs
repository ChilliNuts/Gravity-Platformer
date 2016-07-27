using UnityEngine;
using System.Collections;

public class DestroyGOAfter : MonoBehaviour {

	public float destroyAfterSecs;
	float timer = 0f;
	bool dest;

	void Start(){
		if(destroyAfterSecs > 0){
			dest = true;
		}
	}
	void Update(){
		if(dest){
			if(timer < destroyAfterSecs){
				timer += Time.deltaTime;
			}else DestroyGameObject();
		}
	}

	public void DestroyGameObject () {
		Destroy (this.gameObject);
	}
}
