using UnityEngine;
using System.Collections;

public class EndlessDifficultyDirector : MonoBehaviour {

	public ArrayList possibleLocations;
	public float timer = 0;
	public float timermax = 3;
	public float percentage = 0.90f;

	public float difficulty = 0;
	private float enemySpawnDelay = 0;

	void Start () {
	
	}

	//TODO: Fix Level mode
	void Update () {
	
		timer += Time.deltaTime;
		enemySpawnDelay += Time.deltaTime;

		if(timer >= timermax)
		{	
			difficulty++;
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

        //if(difficulty > 5 && enemySpawnDelay > 10){
        if (difficulty > 1 && enemySpawnDelay > 5)
        {
            difficulty *= 1.5f;
			enemySpawnDelay = 0;


            calcAllRollerRoads();

            Vector3[] road = (Vector3[])allRoutes[(int)(Random.value * (allRoutes.Count+1))];
            Vector3 field = road[0];

			//Set the rotation relative to the road
			int rotation = 0;
			if(road[0].x == road[1].x){
				rotation = 90;
			}

			field.y += 15;
			GameObject roller = (GameObject)Instantiate(Resources.Load("Enemies/roller"), field,  Quaternion.Euler(new Vector3(90, rotation, 0)));
			RollerAI rollerAi = roller.GetComponent<RollerAI>();
			Debug.Log (road[0] + "" + road[1]);
			rollerAi.road = road;
		}
	}

    private ArrayList allRoutes = new ArrayList();

	private void calcAllRollerRoads(){
        allRoutes.Clear();

        int[,] currentmap = SceneData.GetInstance().currentmap;
		ArrayList startEnd = new ArrayList();

		int trackcounter = 0;
		int[] start = new int[2];

		for (int i = 0;i < currentmap.GetLength(0);i++ )
		{
			for (int j = 0; j < currentmap.GetLength(1); j++)
			{ 
                if (i != start[0])
                {
                    trackcounter = 0;
                    start[0] = 0;//x
                    start[1] = 0;//z
                }
				if(currentmap[i,j] == 1){
					if(trackcounter == 0){
					start[0] = i;//x
					start[1] = j;//z
					}
					if(trackcounter <=2)
					trackcounter++;
				}else{
					trackcounter = 0;
					start[0] = 0;//x
					start[1] = 0;//z
				}
				if(trackcounter == 3 && start[0] != 0 && start[1] != 0){
                    Vector3[] vec = new Vector3[2];
					vec[0] = new Vector3(start[0],-5,start[1]);
					vec[1] = new Vector3(i,-5,j);
                    allRoutes.Add(vec);
                    trackcounter = 0;                
					start[0] = 0;//x
					start[1] = 0;//z
				}
			}
		}

        for (int i = 0; i < currentmap.GetLength(1); i++)
        {
            for (int j = 0; j < currentmap.GetLength(0); j++)
            {
                if (i != start[0])
                {
                    trackcounter = 0;
                    start[0] = 0;//x
                    start[1] = 0;//z
                }
                if (currentmap[j, i] == 1)
                {
                    if (trackcounter == 0)
                    {
                        start[0] = j;//x
                        start[1] = i;//z
                    }
                    if (trackcounter <= 2)
                        trackcounter++;
                }
                else
                {
                    trackcounter = 0;
                    start[0] = 0;//x
                    start[1] = 0;//z
                }
                if (trackcounter == 3 && start[0] != 0 && start[1] != 0)
                {
                    Vector3[] vec = new Vector3[2];
                    vec[0] = new Vector3(start[0], -5, start[1]);
                    vec[1] = new Vector3(j, -5, i);
                    allRoutes.Add(vec);
                    trackcounter = 0;
                    start[0] = 0;//x
                    start[1] = 0;//z
                }
            }
        }


    }
}
