using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour {
	
	private Character _player;
	// Use this for initialization
	void Start () {
		_player = Character.Instance;
	}
	
	// Update is called once per frame
	void Update () {		
		if (_player.transform.position.x - transform.position.x > 35) {
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
		Debug.Log (other);
		if (other.tag.ToLower() == "player") {
			//GameObject player = other.gameObject;
			_player.kill();
			
		}
	}
}
