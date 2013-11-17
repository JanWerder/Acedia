using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class levelMode: MonoBehaviour {
	
	
	int[,] currentmap;
	public GameObject tile;
	public ArrayList possibleLocations;
	private ArrayList spawnedTiles;
	private float dropTime;

	private int parseCounter = 1;

	//Recursive! Be careful! Do not feed!
	private int ParseLength(string c){

		bool isNumeric = Regex.IsMatch(c.Substring(0,parseCounter), @"^[0-9]+$");
		if(isNumeric){
			parseCounter++;
			ParseLength(c);
		}
		return parseCounter-1;

	}
	
	// Use this for initialization
	void Start () {
			
		possibleLocations = new ArrayList();
		//map to be loaded
		
		//read out map out of textfile
		StreamReader sr = new StreamReader(Application.dataPath + "\\levels\\" + levelMenu.currentLevel + ".txt");
		
		int rowcount = 0;
		int columncount = 0;
		string currentline = "";
		while((currentline = sr.ReadLine()) != null)
		{
			if(rowcount == 0)
			{
				foreach(char c in currentline)
					columncount++;
			}
			rowcount++;
		}

		currentmap = new int[columncount,rowcount];
		
		sr.BaseStream.Position = 0;
		sr.DiscardBufferedData();
		
		for (int i = 0;i < rowcount;i++)
		{
			currentline = sr.ReadLine();
			#region Read properties
			if(currentline.Contains("#")){
				if(currentline.Contains("jumppads")){
					MatchCollection match = Regex.Matches(currentline.Substring(9), "[0-9][,][0-9]");
					foreach (Match c in match){
						//long coordinate check
						int coordinateLength = this.ParseLength(c.Value);
						//+1 for comma
						Vector2 co = new Vector2(float.Parse(c.Value.Substring(0,coordinateLength)), float.Parse(c.Value.Substring(coordinateLength+1)));
						Instantiate(Resources.Load("jumppad"),new Vector3(co.x,-4.5f,co.y),Quaternion.identity * Quaternion.Euler(new Vector3(-90,0,0)));
						//TODO: Jump via coordinates or collision??
					}
				}
			}else{
			#endregion

				for (int j = 0;j < columncount; j++)
				{
					currentmap[j,i] = int.Parse(currentline[j].ToString());


				}
			}
		}
		
		sr.Close();
		//readout end
		
		
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
	    ((missileLaunch)Camera.main.GetComponent("missileLaunch")).possibleLocations = possibleLocations;	
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
