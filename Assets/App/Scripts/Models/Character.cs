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

    public float attackTime = .5f;
    public float lastLeftAttack = 0f;
    public float lastRightAttack = 0f;

    public GameObject _SoundController;
    private SoundController SoundPlayer;
    public GameObject _LegObject;
    public GameObject _ArmObject;
    public GameObject _AttackCollider;
    private Animator LegAnimator;
    private Animator ArmAnimator;
    private AttackCollider AttackTargets;
    private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	private Vector3 horizontalMove ;
	private float verticalMove = 0.0f;
	
	public PersonController personController;
	public WorldController worldController;
	public bool active = true;

	void Start () {
		personController = PersonController.Instance;
		controller = GetComponent<CharacterController>();
        AttackTargets = _AttackCollider.GetComponent<AttackCollider>();
        LegAnimator = _LegObject.GetComponent<Animator>();
        ArmAnimator = _ArmObject.GetComponent<Animator>();
        SoundPlayer = _SoundController.GetComponent<SoundController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(active){			
			speed += acceleration * Time.deltaTime;
	        speed *= Mathf.Clamp(horizontalMove.x, 0.5f, 1);
	        speed = Mathf.Clamp(speed, maxSpeed / 2, maxSpeed);
		}else{
			speed -= acceleration * Time.deltaTime * 2;
	        speed = Mathf.Clamp(speed, 0, maxSpeed);

		}
		if (controller.isGrounded) {
            LegAnimator.SetBool("jumping", false);
            ArmAnimator.SetBool("jumping", false);
            moveDirection = new Vector3(speed, 0, 0);
			moveDirection = transform.InverseTransformDirection(moveDirection);
			verticalMove= 0;
            canJump = 1;
		}
        else verticalMove -= gravity * Time.deltaTime;
        moveDirection.z = horizontalMove.z * verticalSpeed;
		moveDirection.y = verticalMove;
        moveDirection = transform.InverseTransformDirection(moveDirection);
		controller.Move(moveDirection * Time.deltaTime);
		horizontalMove.x = 0;
		horizontalMove.z = 0;
        LegAnimator.speed = speed / maxSpeed * 2;
        if (lastRightAttack < Time.time) ArmAnimator.SetBool("ataqueder", false);
        if (lastLeftAttack < Time.time) ArmAnimator.SetBool("ataqueizq", false);
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
        LegAnimator.SetBool("jumping", true);
        ArmAnimator.SetBool("jumping", true);
		verticalMove += jumpSpeed;
		verticalMove = Mathf.Clamp (verticalMove, 0, jumpSpeed);
        canJump--;
	}

	public void kill(){
		Debug.Log ("Game Over");
		//Destroy (gameObject);
	}

	public void stop(){
		active = false;
		horizontalMove.x = 0;
		horizontalMove.z = 0;
	}
	
	public void Left(){
		if(!active) return;
		Move(-1,0);
	}

	public void LeftUp(){	
		if(!active) return;	
		Move(-1,1);

	}

	public void Up(){	
		if(!active) return;
		Move(0,1);
	}

	public void RightUp(){
		if(!active) return;
		Move(1,1);

	}

	public void Right(){
		if(!active) return;		
		Move(1,0);
	}

	public void RightDown(){
		if(!active) return;		
		Move(1,-1);
	}

	public void Down(){	
		if(!active) return;
		Move(0,-1);		
	}


	public void LeftDown(){
		if(!active) return;
		Move(-1,-1);		
	}

	public void ButtonZ(){
		if(!active) return;
        if (lastLeftAttack > Time.time) return;
        Debug.Log("Attack Left");
        ArmAnimator.SetBool("ataqueder", false);
        ArmAnimator.SetBool("ataqueizq", true);
        bool FoundTarget = false;
        foreach (GameObject target in AttackTargets.Targets)
		{	
            if(target == null) continue;
            FoundTarget = true;
			Person person = target.GetComponent<Person>();
			if(person != null) person.kill();
        }
        if (FoundTarget) SoundPlayer.PlayPunchSound();
        else SoundPlayer.PlaySwingSound();
        lastLeftAttack = Time.time + attackTime;
	}

	public void ButtonX(){
		if(!active) return;
        if (lastRightAttack > Time.time) return;
        Debug.Log("Attack Right");
        bool FoundTarget = false;
        ArmAnimator.SetBool("ataqueder", true);
        ArmAnimator.SetBool("ataqueizq", false);
        foreach (GameObject target in AttackTargets.Targets)
		{   
            if(target == null) continue;
            FoundTarget = true;
			Person person = target.GetComponent<Person>();
			if(person != null) person.kill();
        }
        if (FoundTarget) SoundPlayer.PlayPunchSound();
        else SoundPlayer.PlaySwingSound();
        lastRightAttack = Time.time + attackTime;
	}

	public void ButtonC(){
		if(!active) return;
        Jump();
	}

	public void ButtonV(){
		if(!active) return;
		//leave britney alone
		Application.Quit();
	}
}
