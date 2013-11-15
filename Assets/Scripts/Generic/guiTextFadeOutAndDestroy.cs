using UnityEngine;
using System.Collections;

public class guiTextFadeOutAndDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(gameObject.guiText.material.color.a >=0)
		{
			Color color = gameObject.guiText.material.color;
			color.a -= 0.04f;
       		gameObject.guiText.material.color = color;
		}
		else
			Destroy(gameObject);
		
	}
}
