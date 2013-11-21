using UnityEngine;
using System.Collections;

public class JumppadBehaviour : MonoBehaviour {

	private GameObject goPlayer;
	private float timer;

	void Start () {
		goPlayer = SceneData.GetInstance().goPlayer;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(Vector3.Distance(this.gameObject.transform.position, goPlayer.transform.position) <= 0.2 && timer > 1.0f){
			timer = 0;
			goPlayer.transform.rigidbody.velocity = Vector3.zero;
			goPlayer.transform.rigidbody.AddForce(Vector3.up * 30000000);

		}
	}
}
