using UnityEngine;
using System.Collections;

public class SetGravityDirection : MonoBehaviour {


	Vector2 gravityLeft, gravityRight, gravityUp, gravityDown;
	float gravity;
	public enum GDirection {UP, DOWN, LEFT, RIGHT};
	public static GDirection gDirection;
	GDirection currentGDir;

	void OnLevelWasLoaded(){
			gDirection = GDirection.DOWN;
			currentGDir = GDirection.DOWN;
	}
	// Use this for initialization
	void Start () {
		gravity = Mathf.Abs(Physics2D.gravity.y + Physics2D.gravity.x);
		gravityDown = new Vector2(0, -gravity);
		gravityUp = new Vector2(0, gravity);
		gravityLeft = new Vector2(-gravity, 0);
		gravityRight = new Vector2(gravity, 0);
		gDirection = GDirection.DOWN;
		currentGDir = GDirection.DOWN;
		UpdateGravity ();
	}


	
	// Update is called once per frame
	void Update () {
		if (currentGDir != gDirection) {
			UpdateGravity();
		}
	}
	void UpdateGravity(){
		if (gDirection == GDirection.LEFT) {
			Physics2D.gravity = gravityLeft;
			currentGDir = GDirection.LEFT;
		}
		if (gDirection == GDirection.RIGHT) {
			Physics2D.gravity = gravityRight;
			currentGDir = GDirection.RIGHT;
		}
		if (gDirection == GDirection.UP) {
			Physics2D.gravity = gravityUp;
			currentGDir = GDirection.UP;
		}
		if (gDirection == GDirection.DOWN) {
			Physics2D.gravity = gravityDown;
			currentGDir = GDirection.DOWN;
		}
	}
}
