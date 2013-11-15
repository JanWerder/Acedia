using UnityEngine;
using System.Collections;

public class PowerupSprint : MonoBehaviour, IPowerupType {
	
	public void runPowerup(playerMove player){
				
		player.speedUpTimer = 0;
				
		player.playerSpeedMultiplier = 2f;
		
		//inform player
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "SPEED++";
		
	}
	
}
