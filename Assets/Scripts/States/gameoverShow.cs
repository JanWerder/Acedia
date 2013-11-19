using UnityEngine;
using System.Collections;

public class gameoverShow : MonoBehaviour {
	
	private cameraMove maincamera;
	Rect tryAgainPos;
	
	// Use this for initialization
	void Start () {
	maincamera = ((cameraMove)Camera.main.gameObject.GetComponent("cameraMove"));	
	maincamera.isRunning = false;

	SceneData sc = SceneData.GetInstance();
	this.gameObject.guiText.text += "\n" + sc.roundScore;
	tryAgainPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2+200, 1024/2,128/1.3f);

	//Data Storage
		if(PlayerPrefs.GetInt("totalExp") == 0){
			PlayerPrefs.SetInt("totalExp", sc.roundScore);
		}else{
			int oldExp =  PlayerPrefs.GetInt("totalExp");
			int newExp = sc.roundScore;			
			this.gameObject.guiText.text += "\nYour EXP:" + (oldExp + newExp);
			PlayerPrefs.SetInt("totalExp", (oldExp + newExp));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 
		GUI.DrawTexture(tryAgainPos, (Texture2D)Resources.Load("Menu/tryagain_button")); 

		if(tryAgainPos.Contains(mousePos) && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
