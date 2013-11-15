using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	Rect startEndlessPos;
	Rect endPos;
	Rect logoPos;
	Rect startLevelPos;
	
	void Start () {
		logoPos = new Rect(Screen.width/2-(1500/2/2f),Screen.height/2-350, 1500/2,300/1.3f);
		startLevelPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2-100, 1024/2,128/1.3f);
		startEndlessPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2+50, 1024/2,128/1.3f);
		endPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2+200, 1024/2,128/1.3f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 
		
		GUI.DrawTexture(logoPos, (Texture2D)Resources.Load("Menu/logo")); 
		GUI.DrawTexture(startLevelPos, (Texture2D)Resources.Load("Menu/level_button")); 
		GUI.DrawTexture(startEndlessPos, (Texture2D)Resources.Load("Menu/endless_button")); 
		GUI.DrawTexture(endPos, (Texture2D)Resources.Load("Menu/exit_button")); 
		
		
		if(startLevelPos.Contains(mousePos) && Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("levelMenu");
		}
		
		if(startEndlessPos.Contains(mousePos) && Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("endlessMode");
		}
		
		if(endPos.Contains(mousePos) && Input.GetMouseButtonDown(0))
		{
			Application.Quit();
		}
	}
}
