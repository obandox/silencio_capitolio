using UnityEngine;
using System.Collections;
// Declare your serializable data.
[System.Serializable]
public class OVirtualButton {

	
	public  bool    show = true;
	public  Texture button;
	public  string name;
	public  string message;
	public  GameObject targetMessage;
	public  string keyboard;
	public  OControl.ScreenPosition fromPosition;
	public  Vector2 position;
	public  float   extendRect = 3.0F;	
	public  float   timeToRelease = 0.1F;	


	private Rect    button_rect;
	private Rect extendedRect;
	private bool UsingButton;


	private float timeUnusing;

	public void Start(){
		if (targetMessage == null) {
			targetMessage = OControl.GetGameObject();		
		}
		button_rect = new Rect(
			0, 
			0, 
			button.width, 
			button.height
	     );	
		extendedRect = new Rect(
			0, 
			0, 
			button.width+extendRect, 
			button.height+extendRect
	     );	
		ProcessPosition ();
	}

	
	public bool Using(){
		return UsingButton;
	}
	public bool Using(bool active){
		if(active){
			UsingButton = true;
			timeUnusing = timeToRelease;
			if(targetMessage!= null && message.Length>0){
				targetMessage.SendMessage(message);
			}
		}else {
			UsingButton = false;
			timeUnusing=0;
		}
		return Using ();
	}
	
	public bool Using(Vector2 position){
		if (!InExtendedRect(position)) return false;
		return Using (true);
	}
	
	public bool InRect(Vector2 position){
		Vector2 toCheck = new Vector2(position.x,Screen.height-position.y);
		return button_rect.Contains (toCheck);
	}
	
	public bool InExtendedRect(Vector2 position){
		Vector2 toCheck = new Vector2(position.x,Screen.height-position.y);
		return extendedRect.Contains (toCheck);
	}


	public Rect Rect(){
		return button_rect;
	}
	
	
	private void ProcessPosition(){
		extendedRect.center = button_rect.center = ProcessPosition(position);
	}

	private Vector2 ProcessPosition(Vector2 local_position){
		Vector2 ret_position= new Vector2();
		if (fromPosition == OControl.ScreenPosition.LeftTop) {
			ret_position=local_position;
		}else
		if (fromPosition == OControl.ScreenPosition.RightTop) {
			ret_position = new Vector2(Screen.width-local_position.x, local_position.y);
		}else
		if (fromPosition == OControl.ScreenPosition.RightBottom) {
			ret_position = new Vector2(Screen.width-local_position.x, Screen.height-local_position.y);			
		}else
		if (fromPosition == OControl.ScreenPosition.LeftBottom) {
			ret_position = new Vector2(local_position.x, Screen.height-local_position.y);				
		}else
		if (fromPosition == OControl.ScreenPosition.Center) {			
			ret_position = new Vector2(Screen.width/2+local_position.x, Screen.height/2+local_position.y);		
		}
		return ret_position;
	}

	public void OnGUI(){		
		if (!show)  return;

		if(button!=null){
			GUI.DrawTexture(
				button_rect, 
				button
			);
		}

	}
	public void Update(){
		if(Input.GetKey(keyboard)){
			Using (true);
		}
		if (timeUnusing <= 0) {
			Using(false);
		}else{
			timeUnusing-=Time.deltaTime;
		}

	}
}
