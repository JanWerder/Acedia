using UnityEngine;
using System.Collections;

public class endlessMode : MonoBehaviour {
	
	
	int[,] currentmap;
	public GameObject tile;
	public ArrayList possibleLocations;
	private ArrayList spawnedTiles;
	private float dropTime;
	
	// Use this for initialization
	void Start () {
		
	possibleLocations = new ArrayList();
	//map to be loaded
	currentmap = new int[,]{
		{2,0,1,0,0,0},
		{1,0,1,1,1,1},
		{1,1,1,1,0,1},
		{1,0,1,1,1,2},
		{1,1,1,1,0,3}
	};

		SceneData.GetInstance().currentmap = currentmap;
	
	// loop through the arrays and spawn a tile whenever there is a 1 and spawn blocks if theres a higher number
		for (int i = 0;i < currentmap.GetLength(0);i++ )
			{
				for (int j = 0; j < currentmap.GetLength(1); j++)
				{
					if(currentmap[i,j] == 0)
					{
						
					}
					else if (currentmap[i,j] == 1)
					{
						Instantiate(Resources.Load("tile"),new Vector3(i,-5,j),Quaternion.identity);
	
						possibleLocations.Add(new Vector3(i,-5,j));
					}
					else
					{
						float yPosition = -6f + currentmap[i,j];
						bool upperSpawn = true;
						while (yPosition > -5f)
						{
							Instantiate(Resources.Load("block"),new Vector3(i,yPosition,j),Quaternion.identity);
							
							if(upperSpawn)
							{
								possibleLocations.Add(new Vector3(i,yPosition,j));
								upperSpawn = false;
							}
						
							yPosition--;
						}
						Instantiate(Resources.Load("tile"),new Vector3(i,yPosition,j),Quaternion.identity);
					
					}
				}
			}
	
	//PlayerSpawn
	Instantiate(Resources.Load("player"), (Vector3)possibleLocations[Random.Range(0,possibleLocations.Count)]+new Vector3(0,1,0), Quaternion.identity);
	//transfer the possible Locations
	//Mode Diff
	((EndlessDifficultyDirector)Camera.main.GetComponent("EndlessDifficultyDirector")).possibleLocations = possibleLocations;	
	((powerupspawn)Camera.main.GetComponent("powerupspawn")).possibleLocations = possibleLocations;

	
	}
	
	void Update () {
		
		// Cold Restart
		if (Input.GetButtonDown("Start")){
		Application.LoadLevel (0);  	
		}
		
		//On Android
		if (Application.platform == RuntimePlatform.Android)

        {

            if (Input.GetKey(KeyCode.Escape))

            {

                Application.LoadLevel (0); 

            }
		}
	}
}
