﻿using UnityEngine;
using System.Collections;

public class JumppadBehaviour : MonoBehaviour {

	private GameObject goPlayer;

	void Start () {
		goPlayer = SceneData.GetInstance().goPlayer;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.gameObject.transform.position, goPlayer.transform.position) <= 0.5f){
			goPlayer.transform.rigidbody.mass = 10;
			goPlayer.transform.rigidbody.AddForce(Vector3.up * 1000);
		}
	}
}