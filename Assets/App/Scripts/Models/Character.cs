using UnityEngine;
using System.Collections;

public class Character : CharacterBase {

    public int canJump = 1;
    public float speed = 6.0F;
    public float jumpSpeed = 15.0F;
    public float gravity = 20.0F;
    public GameObject _AttackCollider;
    private BoxCollider AttackCollider;
    private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	private Vector3 horizontalMove ;
	private float verticalMove = 0.0f;

	void Start () {		
		controller = GetComponent<CharacterController>();
        AttackCollider = _AttackCollider.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {	
		
		if (controller.isGrounded) {
			horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
			moveDirection = new Vector3(horizontalMove.x, 0, horizontalMove.z);
			moveDirection = transform.InverseTransformDirection(moveDirection);
			moveDirection *= speed;	
			verticalMove= 0;
            canJump = 1;
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
        if (canJump == 0) return;
        Debug.Log("JUMP!");
		verticalMove += jumpSpeed;
		verticalMove = Mathf.Clamp (verticalMove, 0, jumpSpeed);
        canJump--;
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
        Debug.Log("Attack 1");
	}

	public override void ButtonX(){
        Debug.Log("Attack 2");
	}

	public override void ButtonC(){
        Jump();
	}

	public override void ButtonV(){
		//Application.Quit();
	}
}
