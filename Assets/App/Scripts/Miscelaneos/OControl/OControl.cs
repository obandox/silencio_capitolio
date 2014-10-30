using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class OControl : MonoBehaviour {
	
	//Static Access start

	public static Vector2 GetAxis(){
		return Shared.VirtualJoystick.Unit ();
	}
	public static float GetAxis(string name){
		if(name == "Vertical"){
			return Shared.VirtualJoystick.Vertical();
		}else
		if(name == "Horizontal"){
			return Shared.VirtualJoystick.Horizontal();
		}
		return 0;
	}
	
	public static Vector2 UXPosition(){
		if (Shared == null)
			return new Vector2 (0,0);
		return Shared.Position;
	}
	public static Vector2 GUIPosition(){
		if (Shared == null)
			return new Vector2 (0,0);
		return new Vector2(Shared.Position.x,Screen.height- Shared.Position.y);
	}
	
	public static bool GetButtonDown(string name){
		if (Shared == null)
						return false;
		if(name == "Up"){
			return Shared.VirtualJoystick.Vertical() > 0.4;
		}else
		if(name == "Left"){
			return Shared.VirtualJoystick.Horizontal() < -0.4;
		}else
		if(name == "Down"){
			return Shared.VirtualJoystick.Vertical() < -0.4;
		}else
		if(name == "Right"){
			return Shared.VirtualJoystick.Horizontal() > 0.4;
		}else
		if(name == "Click"){
			return Shared.ClickDown;
		}else{
			
			foreach(OVirtualButton button in Shared.buttons){
				if(button.name == name){
					return button.Using();
				}
			}

		}
		return false;
	}
	
	public static bool GetButton(string name){
		if (Shared == null)
						return false;
		if(name == "Up"){
			return Shared.VirtualJoystick.Vertical() > 0.4;
		}else
		if(name == "Left"){
			return Shared.VirtualJoystick.Horizontal() < -0.4;
		}else
		if(name == "Down"){
			return Shared.VirtualJoystick.Vertical() < -0.4;
		}else
		if(name == "Right"){
			return Shared.VirtualJoystick.Horizontal() > 0.4;
		}else
		if(name == "Click"){
			return Shared.ClickDown;
		}else{
			
			foreach(OVirtualButton button in Shared.buttons){
				if(button.name == name){
					bool usingB = button.Using();
					if(usingB){
						Debug.Log("name "+button.name+" -- "+name);
						Debug.Log(usingB);
						button.Using(false);
						return true;
					} 
				}
			}

		}
		return false;
	}

	public static GameObject GetGameObject(){
		if(Shared==null) return null;
		return Shared.gameObject;
	}

	//end


	public enum ScreenPosition{LeftTop, LeftBottom, RightTop, RightBottom, Center};

	public Vector2 Position;

	public OVirtualJoystick VirtualJoystick;

	public OVirtualButton[] buttons;
	
	public bool  ClickDown;
	
	public static OControl Shared;

	
	void Awake(){
		Shared = this;
	}



	// Use this for initialization
	void Start () {
		if(!VirtualJoystick.block)
			VirtualJoystick.Start ();
		foreach(OVirtualButton button in buttons){
			if(button.show)
				button.Start ();
		}
	}

	void OnGUI(){
		if (!VirtualJoystick.show)
						return;
		VirtualJoystick.OnGUI ();

		foreach(OVirtualButton button in buttons){
			if(button.show)
				button.OnGUI ();
		}
	}

	// Update is called once per frame
	void Update () {		
		ClickDown=false;	
		VirtualJoystick.Using (false);
		Position = new Vector2(0,0);


		if(!VirtualJoystick.block){
			//Set From Keyboard
			Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
			if(moveVector != Vector2.zero){
				VirtualJoystick.UsingUnit(moveVector);
			}
		}		
		if(Input.GetMouseButton(0)){
			//SetFrom Mouse
			Position = Input.mousePosition;
			ClickDown     = true;
			if(!VirtualJoystick.block){
				VirtualJoystick.Using(Position);
			}
			foreach(OVirtualButton button in buttons){
				if(button.show && button.Using (Position)){
					break;
				}
			}
		}

		//For Touch
		for(int i = 0; i < Input.touchCount; i++){
			ClickDown=true;
			Touch touch = Input.GetTouch(i);
			Position = touch.position;

			bool ContinueFlag = false;			
			//Check Button TouchScreen
			foreach(OVirtualButton button in buttons){
				if(button.show && button.Using (Position)){
					ContinueFlag = true;
					break;
				}
			}
			if(ContinueFlag) continue;
			//SetFrom TouchScreen
			if(!VirtualJoystick.block){
				VirtualJoystick.Using(Position);
			}
		}

		
		if (!VirtualJoystick.block) {
			VirtualJoystick.Update();			
		}
		foreach(OVirtualButton button in buttons){
			if(button.show)
				button.Update ();
		}
	}


}
