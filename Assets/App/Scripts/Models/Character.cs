using UnityEngine;
using System.Collections;

public class Character : Singleton<Character> {

    public int canJump = 1;
    public float speed = 6.0F;
    public float verticalSpeed = 10f;
    public float jumpSpeed = 15.0F;
    public float gravity = 20.0F;
	
	public float maxSpeed = 22.0F;
	public float acceleration = 2.0F;

    public GameObject _LegObject;
    public GameObject _AttackCollider;
    private Animator LegAnimator;
    private AttackCollider AttackTargets;
    private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	private Vector3 horizontalMove ;
	private float verticalMove = 0.0f;
	
	public PersonController personController;
	public WorldController worldController;

	void Start () {
		personController = PersonController.Instance;
		controller = GetComponent<CharacterController>();
        AttackTargets = _AttackCollider.GetComponent<AttackCollider>();
        LegAnimator = _LegObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		speed += acceleration * Time.deltaTime;
        speed *= Mathf.Clamp(horizontalMove.x, 0.5f, 1);
        speed = Mathf.Clamp(speed, maxSpeed / 2, maxSpeed);

		if (controller.isGrounded) {
            moveDirection = new Vector3(speed, 0, horizontalMove.z * verticalSpeed);
			moveDirection = transform.InverseTransformDirection(moveDirection);
			verticalMove= 0;
            canJump = 1;
		}
        else verticalMove -= gravity * Time.deltaTime;

		moveDirection.y = verticalMove;
		controller.Move(moveDirection * Time.deltaTime);
		horizontalMove.x = 0;
		horizontalMove.z = 0;
        LegAnimator.speed = speed / maxSpeed * 2;
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

	public void kill(){
		Debug.Log ("Game Over");
		Destroy (gameObject);
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
        Debug.Log("Attack 1");
        foreach (GameObject target in AttackTargets.Targets)
        {
            //do shit
        }
	}

	public void ButtonX(){
        Debug.Log("Attack 2");
        foreach (GameObject target in AttackTargets.Targets)
        {
            //do shit
        }
	}

	public void ButtonC(){
        Jump();
	}

	public void ButtonV(){
		//Application.Quit();
	}
}
