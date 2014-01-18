using UnityEngine;
using System.Collections;

public class RollerAI : MonoBehaviour {

	public Vector3[] road;
	public bool IsGrounded = false;
	public float speed = 3;
	private bool move0;
	private bool move1;

	// Use this for initialization
	void Start () {
		rigidbody.mass = 10000000;
	}


	void OnCollisionEnter(Collision theCollision){

			IsGrounded = true;


	}
	

	
	// Update is called once per frame
	void Update () {
		if(IsGrounded){ 
			road[0].y = gameObject.transform.position.y;
			road[1].y = gameObject.transform.position.y;
			if(Vector3.Distance(transform.position, road[0]) <= 0.1f){
				move1 = true;
				move0 = false;
			}
			if(Vector3.Distance(transform.position, road[1]) <= 0.1f){
				move0 = true;
				move1 = false;
			}
			if(move1){
				transform.position = Vector3.MoveTowards(transform.position, road[1], speed * Time.deltaTime);
			}
			if(move0){
				transform.position =  Vector3.MoveTowards(transform.position, road[0], speed * Time.deltaTime);
			}
		}
	}
}
