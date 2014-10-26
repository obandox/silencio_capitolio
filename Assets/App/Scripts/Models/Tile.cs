using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public static float maxDistance = 30;
	private Character _player;
	private Transform _transform;
	private Vector3 _position;
	// Use this for initialization
	void Start () {
		_player = Character.Instance;
		_transform = gameObject.transform;
		_position = _transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = _player.transform.position;
		float currentDistance = Mathf.Abs(playerPosition.x - _position.x);

		if(currentDistance > maxDistance){
			if(playerPosition.x > _position.x){
				_position.x+=maxDistance*2;
			}else{				
				_position.x+=maxDistance*2;
			}
		}
		
		_transform.position = _position;
	}
}
