using UnityEngine;
using System.Collections;

public class SpawnBox : MonoBehaviour {

	public float spawnDelay;
	public GameObject boxPrefab;
	Box box;
	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("canSpawn", true);

	}
	
	// Update is called once per frame
	void Update () {
		if(box != null && box.destroyed == true){
			anim.SetBool("canSpawn", true);
			box.destroyed = false;
		}
	}

	public void boxSpawn(){
		anim.SetBool("canSpawn", false);
		if (box == null) {
			GameObject thisBox = Instantiate (boxPrefab, transform.position, Quaternion.identity) as GameObject;
			box = thisBox.GetComponent<Box> ();
			box.respawnDelay = spawnDelay;
		}else {
			box.Reset();
			}
		}
}
