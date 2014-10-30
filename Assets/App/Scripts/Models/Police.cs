using UnityEngine;
using System.Collections;

public class Police : Person {


	bool playerInCollider;
	bool aproachToPlayer = false;
	bool attack = false;
	
	// Use this for initialization
	void Start () {
		_player = Character.Instance;
		_position = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3();
        float distance = _player.transform.position.x - _position.x;
        float absDistance = Mathf.Abs(distance);
        if (!isGrounded) verticalMove -= gravity * Time.deltaTime;
        else verticalMove = 0f;
        if (dead)
        {
            if (!isGrounded)
            {
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
        {
            if (attack)
                Attack();
            if (aproachToPlayer)
            {
                horizontalMove.x = Mathf.Clamp(_player.transform.position.x - transform.position.x, -1, 1);
                horizontalMove.z = Mathf.Clamp(_player.transform.position.z - transform.position.z, -1, 1);
                moveDirection = new Vector3(horizontalMove.x, verticalMove, horizontalMove.z);
                moveDirection.x *= speed * 0.5f;
                moveDirection.z *= speed * 0.5f;
            }
            else
            {
                horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
                moveDirection = new Vector3(horizontalMove.x, verticalMove, horizontalMove.z);
                moveDirection = transform.InverseTransformDirection(moveDirection);
                moveDirection.x *= speed;
                moveDirection.z *= speed;
            }
        }
        horizontalMove.x = 0;
        horizontalMove.z = 0;
        _position = _position + (moveDirection * Time.deltaTime);
        if (_player != null)
        {
            if (distance > 35)
            {
                Destroy(gameObject);
            }
            else
            {
                if (absDistance <= 10 && !aproachToPlayer)
                {
                    verticalMove = 8f;
                    aproachToPlayer = true;
                    isGrounded = false;
                }
                if (absDistance <= 2)
                {
                    attack = true;
                }
            }
        }
        transform.position = _position;
    }
	
	void OnCollisionEnter(Collision collision) {
		CollisionCallback (collision.collider);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag.ToLower () == "player") {
			playerInCollider = true;
		}
		CollisionCallback (other);
	}
	void OnTriggerExit(Collider other) {
		if (other.tag.ToLower () == "player") {
			playerInCollider = false;
		}
	}

	void Attack(){
        if (playerInCollider)
        {
            _player.kill();
            attack = false;
            dead = true;
        }
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


	
	
}
