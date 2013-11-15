using UnityEngine;
using System.Collections;

public class PowerupSprint : IPowerupType {
	
	public void runPowerup(PlayerMove player){
				
		player.speedUpTimer = 0;
				
		player.playerSpeedMultiplier = 2f;

		//inform player
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			GameObject.Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "SPEED++";
		
	}
	
}
