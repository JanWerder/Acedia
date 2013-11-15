using UnityEngine;
using System.Collections;

public class missileLaunch : MonoBehaviour {
	
	public ArrayList possibleLocations;
	public float timer = 0;
	public float timermax = 3;
	public float percentage = 0.90f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Count to the refiring timer (timer) and launch a rocket. (Rocket Movement ist handled by missileMove)
	void Update () {
	timer += Time.deltaTime;
     if(timer >= timermax)
     {	
		//Possible Locations contains all field that are shootable
		Vector3 field = (Vector3)(possibleLocations[(int)(Random.value*(possibleLocations.Count))]);
		//Hard Y-Value to let them start from the same position
		field.y += 15;
        Instantiate(Resources.Load("rocket"), field,  Quaternion.identity);

		timer = 0;
		if (timermax > 0.3f)
			{
			timermax = timermax * percentage;  	
			}
     }
	}
	
	
	
	
}
