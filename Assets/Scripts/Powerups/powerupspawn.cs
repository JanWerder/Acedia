using UnityEngine;
using System.Collections;

public class powerupspawn : MonoBehaviour {

	public ArrayList possibleLocations;
	public float timer = 0;
	public float timermax = 1;
	
	// Use this for initialization
	void Start () {
	}
	
	// Count down seconds to spawn powerup
	void Update () {
	timer += Time.deltaTime;
     if(timer >= timermax)
     {	
		//Possible Locations contains all field that are possible to spawn on
		Vector3 field = (Vector3)(possibleLocations[(int)(Random.value*(possibleLocations.Count))]);
			
		field.y -= -0.9f;
			
        Instantiate(Resources.Load("powerup"), field,  Quaternion.Euler(45,45,45));
		
		timer = 0;
     }
	}
}
