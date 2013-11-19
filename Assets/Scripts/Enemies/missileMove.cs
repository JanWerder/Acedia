using UnityEngine;
using System.Collections;

public class missileMove : MonoBehaviour {
	
	private GameObject warningText;
	public GameObject selfObject;
	private cameraMove maincameraScript;
	PlayerMove playerMove;
	
	// Use this for initialization
	void Start () {
	maincameraScript = ((cameraMove)Camera.main.gameObject.GetComponent<cameraMove>());
		
	warningText = (GameObject)Instantiate(Resources.Load("mark"), new Vector3(this.transform.position.x-0.3f, transform.position.y-14.49f, this.transform.position.z+0.5f),Quaternion.identity * Quaternion.AngleAxis(90, new Vector3(1,0,0)));
		
	
    playerMove = SceneData.GetInstance().goPlayer.GetComponent<PlayerMove>();
    
	}
	
	// Update is called once per frame
	void Update () {
	
	//Update the text on the mark to a distance counter
	float distance = Vector3.Distance (warningText.transform.position, this.transform.position);
	if(distance.ToString().IndexOf(".") < 2)
	warningText.GetComponent<TextMesh>().text = distance.ToString().Substring(0,1);
		
	}
	
	void OnCollisionEnter(Collision collision) {
	
		
		//Kill the Warning Mark
		Destroy(warningText);
		SceneData sc = SceneData.GetInstance();

		if(collision.collider.gameObject.name == "player(Clone)" && maincameraScript.isRunning)
		{
			//check if player is invincible
			if(playerMove.playerInvincible == false)
			{
				//The Rocket just hit the player
				Instantiate(Resources.Load("txtGameover"));
				playerMove.playerAlive = false;
			}
			else
			{


				//Raise the score by 100 for a dodged missile
				sc.roundScore += 100;
				
				//Raise score for another 700 for destroying missile with bodycheck
				sc.roundScore += 700;

				//show points
				if (GameObject.Find("txtinfo(Clone)") == null)
				{
					Instantiate(Resources.Load("txtinfo"));
				}
				GameObject.Find("txtinfo(Clone)").guiText.text = "+ 700";		
			}
		}else{
			//Raise the score by 100 for a dodged missile
			sc.roundScore += 100;
		}
		
		//Instantiate the explosion, but give a duration so it won't take up that much memory (low runtime since your focus is not on the particles)
		GameObject ex = (GameObject)Instantiate(Resources.Load("explosion"), collision.transform.position, Quaternion.identity);
		Destroy(ex,1);
		
		//Destroy the rocket itself
		Destroy(gameObject);
	}
}
