using UnityEngine;
using System.Collections;

public class gameoverShow : MonoBehaviour {
	
	private cameraMove maincamera;
	
	// Use this for initialization
	void Start () {
	maincamera = ((cameraMove)Camera.main.gameObject.GetComponent("cameraMove"));	
	maincamera.isRunning = false;
	this.gameObject.guiText.text += "\n" + maincamera.ingameScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
