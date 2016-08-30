using UnityEngine;
using System.Collections;

public class CubeTextFollow : MonoBehaviour {
	public GameObject cube;
	public float yOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y + yOffset, 0);
	}
}
