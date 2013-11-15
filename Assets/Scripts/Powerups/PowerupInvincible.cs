using UnityEngine;
using System.Collections;

public class PowerupInvincible : MonoBehaviour, IPowerupType {
	
	public void runPowerup(playerMove player){
				
		player.playerInvincible = true;
		player.invincibleTimer = 0;
		
		//inform player
		if (GameObject.Find("txtplusPoints(Clone)") == null)
		{
			Instantiate(Resources.Load("txtinfo"));
		}
		GameObject.Find("txtinfo(Clone)").guiText.text = "INVINCIBILITY";
	}
	
}
