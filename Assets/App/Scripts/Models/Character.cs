using UnityEngine;
using System.Collections;

public class Character : CharacterBase {

    public float speed = 6.0F;
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
			moveDirection = new Vector3(horizontalMove.x, 0, horizontalMove.z);
		    moveDirection = transform.InverseTransformDirection(moveDirection);
			moveDirection *= speed;	
		if (controller.isGrounded) 
			verticalMove= 0;
		else
			verticalMove -= gravity * Time.deltaTime;
		moveDirection.y = verticalMove;
		controller.Move(moveDirection * Time.deltaTime);
		horizontalMove.x = 0;
		horizontalMove.z = 0;
	}
	
	public void Move(float horizontalX,float horizontalZ){	
		horizontalMove.x+=horizontalX;
		horizontalMove.x = Mathf.Clamp(horizontalMove.x, -1, 1);
		horizontalMove.z+=horizontalZ;
		horizontalMove.z = Mathf.Clamp(horizontalMove.z, -1, 1);
	}
	public void Jump(){	
		if (controller.isGrounded) {	
			verticalMove += jumpSpeed;
			verticalMove = Mathf.Clamp (verticalMove, 0, jumpSpeed);
		}
	}


	public override void Left(){
		Move(-1,0);
	}

	public override void LeftUp(){		
		Move(-1,1);

	}

	public override void Up(){	
		Move(0,1);
	}

	public override void RightUp(){
		Move(1,1);

	}

	public override void Right(){		
		Move(1,0);
	}

	public override void RightDown(){		
		Move(1,-1);
	}

	public override void Down(){	
		Move(0,-1);		
	}


	public override void LeftDown(){
		Move(-1,-1);		
	}

	public override void ButtonZ(){

	}

	public override void ButtonX(){

	}

	public override void ButtonC(){

	}

	public override void ButtonV(){
		Application.Quit();
	}
}
