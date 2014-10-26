using UnityEngine;
using System.Collections;

public class Character : Singleton<Character> {
	
	public float speed = 0.0F;
	
	public float maxSpeed = 22.0F;
	public float acceleration = 2.0F;

    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	private Vector3 horizontalMove ;
	private float verticalMove = 0.0f;

	void Start () {		
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {			
		speed += acceleration * Time.deltaTime;
		speed = Mathf.Clamp (speed, 0, maxSpeed);
		if (controller.isGrounded) {
			horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
			moveDirection = new Vector3(horizontalMove.x, 0, horizontalMove.z);
			moveDirection = transform.InverseTransformDirection(moveDirection);
			moveDirection *= speed;	
			verticalMove= 0;
		}else
			verticalMove -= gravity * Time.deltaTime;
		moveDirection.y = verticalMove;
		controller.Move(moveDirection * Time.deltaTime);
		horizontalMove.x = 0;
		horizontalMove.z = 0;
	}
	
	public void Move(float horizontalX,float horizontalZ){	
		horizontalMove.x+=horizontalX;
		horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
		horizontalMove.z+=horizontalZ;
		horizontalMove.z = Mathf.Clamp(horizontalMove.z, -1, 1);
	}
	public void Jump(){	
		if (controller.isGrounded) {	
			verticalMove += jumpSpeed;
			verticalMove = Mathf.Clamp (verticalMove, 0, jumpSpeed);
		}
	}


	public void Left(){
		Move(-1,0);
	}

	public void LeftUp(){		
		Move(-1,1);

	}

	public void Up(){	
		Move(0,1);
	}

	public void RightUp(){
		Move(1,1);

	}

	public void Right(){		
		Move(1,0);
	}

	public void RightDown(){		
		Move(1,-1);
	}

	public void Down(){	
		Move(0,-1);		
	}


	public void LeftDown(){
		Move(-1,-1);		
	}

	public void ButtonZ(){

	}

	public void ButtonX(){

	}

	public void ButtonC(){

	}

	public void ButtonV(){
		Application.Quit();
	}
}
