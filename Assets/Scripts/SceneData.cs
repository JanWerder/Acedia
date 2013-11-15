using UnityEngine;
using System.Collections;

public class SceneData: MonoBehaviour{

	public GameObject goPlayer;
	public PlayerMove coPlayer;
	private static SceneData instance;

	public static SceneData GetInstance()
	{
		if (!instance)
		{
			instance = (SceneData)GameObject.FindObjectOfType(typeof(SceneData));
			if (!instance)
				Debug.LogError("There needs to be one active v script on a GameObject in your scene.");
		}
		
		return instance;
	}

	public static void Start () {
		SceneData.GetInstance().goPlayer = GameObject.Find("player(Clone)");	
	}


}