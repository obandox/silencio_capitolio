using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	
	public float speed = 6.0F;
    public float verticalMove = 0f;
	public float speedPenality = 0.3f;
	public float gravity = 20.0F;
    public bool dead = false;
    public bool isGrounded = true;
    //private Animator _Animator;
    private Vector3 moveDirection = Vector3.zero;
	
	protected Character _player;
	protected Vector3 horizontalMove ;
	
	protected Vector3 _position;

	// Use this for initialization
	void Start () {
		_player = Character.Instance;
		_position = transform.position;
        //_Animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //_Animator.SetBool("dead", dead);
        //_Animator.SetBool("isflying", !isGrounded);
        if (!isGrounded) verticalMove -= gravity * Time.deltaTime;
        else verticalMove = 0f;
        if (dead)
        {
            if (!isGrounded)
            {
                moveDirection = new Vector3();
                moveDirection.y = verticalMove;
                moveDirection.x = 1;
                moveDirection = transform.InverseTransformDirection(moveDirection);
                moveDirection.x *= speed;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            moveDirection = new Vector3();
            horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
            moveDirection = new Vector3(horizontalMove.x, 0, horizontalMove.z);
            moveDirection = transform.InverseTransformDirection(moveDirection);
            moveDirection *= speed;
        }
        _position = _position + (moveDirection * Time.deltaTime);
        horizontalMove.x = 0;
        horizontalMove.z = 0;
        transform.position = _position;
		if (_player != null && _player.transform.position.x - _position.x > 35) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		CollisionCallback (collision.collider);
	}

	void OnTriggerEnter(Collider other) {
		CollisionCallback (other);
	}

	void CollisionCallback(Collider other){

		if (other.tag.ToLower() == "player" && !dead) {
			//GameObject player = other.gameObject;
			_player.speed = _player.speed - _player.speed * speedPenality;

		}
        if (other.tag.ToLower() == "ground")
        {
            isGrounded = true;
        }
	}
	public virtual void kill(){
		//Destroy (gameObject);
        if (isGrounded)
            isGrounded = false;
        dead = true;
        if (_player != null)
        {
            speed = (float) _player.speed * 1.2f;
            verticalMove = 6f;
        }
	}

}
