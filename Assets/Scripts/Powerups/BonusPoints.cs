using UnityEngine;
using System.Collections;

public class BonusPoints : MonoBehaviour, IPowerupType {
	
	public void runPowerup(playerMove player){
		
		//Raise the score by 1000 for collecting
		((cameraMove)Camera.main.gameObject.GetComponent<cameraMove>()).ingameScore += 1000;
		
		//show points
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "+ 1000";
	}
	
}
