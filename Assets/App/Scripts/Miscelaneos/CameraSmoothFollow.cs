using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {
	
	public GameObject cameraTarget; // object to look at or follow

	public float smoothTime = 0.1f;    // time for dampen
	public bool cameraFollowX = true; // camera follows on horizontal
	public bool cameraFollowY = true; // camera follows on vertical
	public bool cameraFollowHeight = true; // camera follow CameraTarget object height
	public float cameraHeight = 2.5f; // height of camera adjustable
	public float cameraLeftX = 0.0f; // left x of camera adjustable
	public float cameraLeftZ = 0.0f; // left z of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Vector3 clampPosition; // camera position
	private Transform thisTransform; // camera Transform
	
	// Use this for initialization
	void Start () {
		thisTransform = transform;
		clampPosition = thisTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraFollowX){
			clampPosition.x = Mathf.SmoothDamp (clampPosition.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
		}
		if (cameraFollowY) {
			clampPosition.y = Mathf.SmoothDamp (clampPosition.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime);
		}
		if (cameraFollowHeight) {
			thisTransform.position= new Vector3(clampPosition.x+cameraLeftX, clampPosition.y + cameraHeight,clampPosition.z+cameraLeftZ);	;
		} else {
			thisTransform.position = clampPosition;
		}
	}
}