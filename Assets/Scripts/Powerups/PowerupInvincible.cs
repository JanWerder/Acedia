using UnityEngine;
using System.Collections;

public class PowerupInvincible : IPowerupType {
	
	public void runPowerup(PlayerMove player){
				
		player.playerInvincible = true;
		player.invincibleTimer = 0;
		
		//inform player
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			GameObject.Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "INVINCIBILITY";
	}
	
}
