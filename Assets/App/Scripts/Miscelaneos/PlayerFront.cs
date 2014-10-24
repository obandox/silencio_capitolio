using UnityEngine;
using System.Collections;

public class PlayerFront : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerStay(Collider other) {
		if(other.tag == "Tile"){
			other.transform.position = new Vector3(other.transform.position.x - 60,other.transform.position.y,other.transform.position.z) ;
		}
	}

}
