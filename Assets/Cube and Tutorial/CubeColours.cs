using UnityEngine;
using System.Collections;

public class CubeColours : MonoBehaviour {

	public Color[] cubeColours;
	Material mySkin;

	// Use this for initialization
	void Start () {
		mySkin = GetComponent<MeshRenderer>().material;
		mySkin.color = cubeColours[Random.Range( 0, cubeColours.Length)];
		StartCoroutine(ChangeColour());
	}
	
	// Update is called once per frame
	void Update () {

	}
	IEnumerator ChangeColour(){
		float waitTime = Random.Range(0.5f, 5f);
		float transitionSpeed = Random.Range(0.5f, 3f);
		float t = 0f;
		Color targetColor = cubeColours[Random.Range(0, cubeColours.Length)];
		while (mySkin.color != targetColor){
			mySkin.color = Color.Lerp(mySkin.color, targetColor, t);
			t += Time.deltaTime / transitionSpeed;
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		StartCoroutine(ChangeColour());
	}

}
