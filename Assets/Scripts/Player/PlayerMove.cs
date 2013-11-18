using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	void Start () {
		maincameraScript = ((cameraMove)Camera.main.gameObject.GetComponent<cameraMove>());
	}
	
	Vector3 targetPos;
	public bool isMoving = false;
	private cameraMove maincameraScript;
	private Color invincibleColor = Color.blue;
	public bool playerAlive = true;
	
	int aDir = 0;
	
	//powerup variables
	public bool playerInvincible = false;
	public float invincibleTimer = 0;
	public float invincibleTimermax = 5;
	public float speedUpTimer = 0;
	public float speedUpTimermax = 10;
	public float playerSpeedMultiplier = 1;
	public Material normalPlayerMat;
	public Material invinciblePlayerMat;
	
	//Swipe Variables
	Vector2 StartPos;
	int SwipeID = -1;
	float minMovement = 20.0f;
	Vector2 delta;

	int layerMask = 0 << 2;
	
	// here happens a lot of magic (xbox controller magic included)
	void Update () {
		
	//invincible reset
	if(playerInvincible)
		{
			invincibleTimer += Time.deltaTime;
			
			// Set specular shader
			Color plusColor = new Vector4(15, 100, 15, 1);
			invincibleColor = invincibleColor + plusColor;
			renderer.material.shader = Shader.Find("Specular");
			renderer.material.SetColor("_Color" , invincibleColor);
			
			if(invincibleTimer >= invincibleTimermax)
			{
				playerInvincible = false;
				invincibleTimer = 0;
				renderer.material = normalPlayerMat;
			}
		}
		
	//speed multiply reset
	if(playerSpeedMultiplier > 1)
		{
			speedUpTimer += Time.deltaTime;
			
			if(speedUpTimer >= speedUpTimermax)
			{
				playerSpeedMultiplier = 1;
				speedUpTimer = 0;
			}
		}
		
		
	if(playerAlive && !checkGroundUnderneath() && !isMoving){
		Instantiate(Resources.Load("txtGameover"));	
		playerAlive = false;
	}
	
	
	//Movement
	if(playerAlive)
		{
		//XBOX Controller & WASD Movement
		if (!isMoving){
			if (Input.GetAxis("Vertical") != 00){
				
					moveUpDown();
					
			}
			else if (Input.GetAxis("Horizontal") != 00){
				
					moveRightLeft();
					
			}
		}else{
			if (transform.position == targetPos ){
			isMoving = false;
			}else{
			this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, playerSpeedMultiplier * Time.deltaTime * 5);	
			}
			
		}
			
			
		//swipe movement		
		foreach (var T in Input.touches) {
	       var P = T.position;
	       if (T.phase == TouchPhase.Began && SwipeID == -1) {
	         SwipeID = T.fingerId;
	         StartPos = P;
	       } else if (T.fingerId == SwipeID) {
	         delta = P - StartPos;
	         if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement) {
	          SwipeID = -1;
	          if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
	              
				moveRightLeft();
				
	          } 
	          else {
	              
				moveUpDown();
							
	          }
	         } else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
	          SwipeID = -1;
	       } 
		}
	}	
  }
	
	bool checkGroundUnderneath()
	{
			return Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 10f);
	}
	
	void moveUpDown()
	{
		//Android
		if (Application.platform == RuntimePlatform.Android)
		{
			if (delta.y > 0) {
		 
		                //up
				if(!Physics.Raycast(transform.position, Vector3.forward, collider.bounds.extents.x + 1f,layerMask))
							aDir = 1;
									
		              } else {
		 
		                //down
				if(!Physics.Raycast(transform.position, Vector3.back, collider.bounds.extents.x + 1f,layerMask))
							aDir = -1;	
		              }
		}
		
		//PC
		if(Input.GetAxis("Vertical") < -0.1f && !Physics.Raycast(transform.position, Vector3.back, collider.bounds.extents.x + 1f)){
						//s
						aDir = -1;
					}else if (Input.GetAxis("Vertical") > 0.1f && !Physics.Raycast(transform.position, Vector3.forward, collider.bounds.extents.x + 1f)) {
						//w
						aDir = 1;	
					}
				isMoving = true;
				this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(transform.position.x,transform.position.y,transform.position.z+aDir),playerSpeedMultiplier * Time.deltaTime * 5);
				targetPos = new Vector3(Mathf.Round(transform.position.x),transform.position.y,Mathf.Round(transform.position.z+aDir));
		aDir = 0;
	}
	
	void moveRightLeft()
	{
		//Android
		if (Application.platform == RuntimePlatform.Android)
		{
			if (delta.x > 0) {
		 
		                //right
				if(!Physics.Raycast(transform.position, Vector3.right, collider.bounds.extents.x + 1f,layerMask))
							aDir = 1;
				
		              } else {
		 
		                //left
				if(!Physics.Raycast(transform.position, Vector3.left, collider.bounds.extents.x + 1f,layerMask))
							aDir = -1;			
		              }
		}
		
		//PC
		if(Input.GetAxis("Horizontal") < -0.1f && !Physics.Raycast(transform.position, Vector3.left, collider.bounds.extents.x + 1f)){
						//a
						aDir = -1;
					}else if(Input.GetAxis("Horizontal") > 0.1f && !Physics.Raycast(transform.position, Vector3.right, collider.bounds.extents.x + 1f)){
						//d
						aDir = 1;	
					}
		
		
				isMoving = true;
				this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(transform.position.x+aDir,transform.position.y,transform.position.z),playerSpeedMultiplier * Time.deltaTime * 5);
				targetPos = new Vector3(Mathf.Round(transform.position.x+aDir),transform.position.y,Mathf.Round(transform.position.z));
		aDir = 0;
	}
	
	
}