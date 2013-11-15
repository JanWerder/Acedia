using UnityEngine;
using System.Collections;

public class powerup : MonoBehaviour {
	
	private cameraMove maincameraScript;
	//feature
	private string feature = "";
	public bool destroyed = false;

	private IPowerupType powerupType;
	PlayerMove playerMove;
	
	// Use this for initialization
	void Start () {
		maincameraScript = ((cameraMove)Camera.main.gameObject.GetComponent<cameraMove>());

    	playerMove = SceneData.GetInstance().goPlayer.GetComponent<PlayerMove>();
		
		int number = Random.Range(1,4);
			
		if(number == 1)
		{
			powerupType = new BonusPoints();
		}
		else if(number == 2)
		{
			powerupType = new PowerupInvincible();
		}
		else if(number == 3)
		{
			powerupType = new PowerupSprint();
		}
		
			
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!destroyed)
			transform.rotation = transform.rotation * Quaternion.Euler(1, 1, -1);
	
	}
	
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.collider.gameObject.name == "player(Clone)" && maincameraScript.isRunning && !destroyed)
		{
			//Player collected powerup
				
			powerupType.runPowerup(playerMove);
			
			//Delete powerup
			destroyPowerup();
		}
		else if(collision.collider.gameObject.name == "rocket(Clone)")
		{
			destroyPowerup();	
		}
		else if(collision.collider.gameObject.name == "powerup(Clone)" && !collision.collider.gameObject.GetComponent<powerup>().destroyed)
		{
			destroyPowerup();	
		}
		
	}
	
	void destroyPowerup()
	{
		destroyed = true;
		foreach (Transform child in transform)
		{
    		child.rigidbody.useGravity = true;
			Destroy(child.gameObject, Random.Range(1,11)); //Destroy children by and by
		}
		rigidbody.useGravity = true;
		Destroy(gameObject, 10); //Destroy powerup in seconds
	}
}
