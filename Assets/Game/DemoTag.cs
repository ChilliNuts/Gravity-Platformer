using UnityEngine;
using System.Collections;

public class DemoTag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_STANDALONE
		Destroy(gameObject);
		#endif
	}
}
