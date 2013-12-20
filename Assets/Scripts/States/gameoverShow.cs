using UnityEngine;
using System.Collections;

public class gameoverShow : MonoBehaviour {
	
	private cameraMove maincamera;
	Rect tryAgainPos;
	Rect progressPos;
	Rect homeUiPos;
	Rect progressFillPos;
	float progressFillWidth = 1024/2.175f;

	float progressMultiplier = 0f;
	float progressCurrentMultiplier = 0f;
	float progressMultiplieroldExp = 0f;
	//missing EXP
	private int missingExp = 0;
	//level EXP (current level having exp)
	private int gainedExp = 0;
	private int totalLevelExp = 0;
	private int gainedOldExp = 0;
	
	// Use this for initialization
	void Start () {
	maincamera = ((cameraMove)Camera.main.gameObject.GetComponent("cameraMove"));	
	maincamera.isRunning = false;

	SceneData sc = SceneData.GetInstance();
	this.gameObject.guiText.text += "\n" + sc.roundScore;
	tryAgainPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2+200, 1024/2,128/1.3f);
	progressPos = new Rect(Screen.width/2-(1024/2/2f),Screen.height/2+300, 1024/2,128/1.3f);
		homeUiPos = new Rect(Screen.width-80,0,80,80);
	

	//Data Storage
		if(PlayerPrefs.GetInt("totalExp") == 0){
			PlayerPrefs.SetInt("totalExp", sc.roundScore);
		}else{
			int oldExp =  PlayerPrefs.GetInt("totalExp");
			int newExp = sc.roundScore;			
			int calcExp = oldExp + newExp;
			int level = (int)(0.01f * Mathf.Sqrt(calcExp));


			while((int)(0.01f * Mathf.Sqrt(calcExp + missingExp)) <= level)
			{
				missingExp++;
			}

			while((int)(0.01f * Mathf.Sqrt(calcExp - gainedExp)) >= level)
			{
				gainedExp++;
			}

			while((int)(0.01f * Mathf.Sqrt(oldExp - gainedOldExp)) >= level)
			{
				gainedOldExp++;
			}

			totalLevelExp = (gainedExp + missingExp);

			//old Exp 
			progressMultiplieroldExp = ((float)gainedOldExp)/((float)totalLevelExp);
			progressMultiplier = ((float)gainedExp)/((float)totalLevelExp);

			this.gameObject.guiText.text += "\nYour EXP: " + calcExp + "\nLevel: " + level;
			PlayerPrefs.SetInt("totalExp", calcExp);


		}
	}
	
	// Update is called once per frame
	void Update () {
	if (progressMultiplieroldExp <= progressMultiplier){
			progressMultiplieroldExp += 0.01f;
	} 
		progressFillPos = new Rect(Screen.width/2-(1024/2/2f-20),Screen.height/2+335, progressFillWidth * progressMultiplieroldExp,128/4f);
	}
	void OnGUI(){
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 
		GUI.DrawTexture(tryAgainPos, (Texture2D)Resources.Load("Menu/tryagain_button")); 
		GUI.DrawTexture(progressPos, (Texture2D)Resources.Load("Menu/progressbar")); 
		GUI.DrawTexture(progressFillPos, (Texture2D)Resources.Load("Menu/progressbar_fill")); 

		GUI.DrawTexture(homeUiPos, (Texture2D)Resources.Load("Menu/ui/corner")); 
		GUI.DrawTexture(new Rect(Screen.width-64,0,64,64), (Texture2D)Resources.Load("Menu/ui/home")); 

		if(tryAgainPos.Contains(mousePos) && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if(homeUiPos.Contains(mousePos) && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("mainMenu");
		}
	}
}
