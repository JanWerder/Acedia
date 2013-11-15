using UnityEngine;
using System.Collections;

public class levelMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public static int currentLevel = 0;
	
	// Update is called once per frame
	void Update () {
		
	// Cold Restart
		if (Input.GetButtonDown("Start")){
		Application.LoadLevel (0);  	
		}
		
		//On Android
		if (Application.platform == RuntimePlatform.Android)

        {

            if (Input.GetKey(KeyCode.Escape))

            {

                Application.LoadLevel (0); 

            }
		}
		
	}
	
	void OnMouseDown()
	{
		currentLevel = int.Parse(transform.name);
		Application.LoadLevel("levelMode");
	}
}
