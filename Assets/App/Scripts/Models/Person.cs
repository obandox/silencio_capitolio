using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	
	public float speed = 6.0F;
	public float speedPenality = 0.3f;
	public float gravity = 20.0F;
	
	protected Character _player;
	protected Vector3 horizontalMove ;
	
	protected Vector3 _position;

	// Use this for initialization
	void Start () {
		_player = Character.Instance;
		_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = new Vector3();
		horizontalMove.x = Mathf.Clamp(horizontalMove.x, 0.5f, 1);
		moveDirection = new Vector3(horizontalMove.x, 0, horizontalMove.z);
		moveDirection = transform.InverseTransformDirection(moveDirection);
		moveDirection *= speed;
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

		if (other.tag.ToLower() == "player") {
			//GameObject player = other.gameObject;
			_player.speed = _player.speed - _player.speed * speedPenality;

		}
	}
	public virtual void kill(){
		Destroy (gameObject);
	}

}
