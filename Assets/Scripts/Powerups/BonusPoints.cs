using UnityEngine;
using System.Collections;

public class BonusPoints : IPowerupType {
	
	public void runPowerup(PlayerMove player){
		
		//Raise the score by 1000 for collecting
		((cameraMove)Camera.main.gameObject.GetComponent<cameraMove>()).ingameScore += 1000;
		
		//show points
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			GameObject.Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "+ 1000";
	}
	
}
