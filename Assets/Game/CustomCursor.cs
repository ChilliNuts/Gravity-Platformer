using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {


	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public bool useCustomCursor = false;
	public bool updateCursor = false;

	// Use this for initialization
	void Start () {
		UpdateCursor();
	}

	void Update(){
		if(updateCursor){
			UpdateCursor();
			updateCursor = false;
		}
	}
	void UpdateCursor(){
		if(useCustomCursor){
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		}
		else if(!useCustomCursor){
			Cursor.SetCursor(null, Vector2.zero, cursorMode);
		}
	}
}
