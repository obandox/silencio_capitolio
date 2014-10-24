using UnityEngine;
using System.Collections;
// Declare your serializable data.
[System.Serializable]
public class OVirtualJoystick
{		
	public  bool    show = true;
	public  bool    block = false;
	public  Texture background;
	public  Texture button;
	public  Vector2 initial_position;
	public  float   extendRect = 40.0F;	
	public  float   smoothBackTime = 0.01F;
	public  float   smoothBackvel = 0.3F;
	
	public  GameObject targetMessage;

	private Vector2 input_position;
	private Vector2 input_real_position;
	private Vector2 input;
	private Rect    background_rect;
	private Rect    button_rect;
	private float radius;
	private float angleRad;
	private float CosX;
	private float SinY;
	private float AbsCosX;
	private float AbsSinY;
	private bool UsingJoystick;
	private Rect extendedRect;

	public bool Using(){
		return UsingJoystick;
	}
	public bool Using(bool active){
		UsingJoystick = active;
		return Using ();
	}
	
	public bool Using(Vector2 position){
		if (!InExtendedRect(position)) return false;
		UsingJoystick = true;
		OnMove(position);
		SendDirectionMessage();
		return Using ();
	}
	
	public bool UsingUnit(Vector2 unit){
		UsingJoystick = true;
		SetUnit(unit);
		SendDirectionMessage();
		return Using ();
	}

	public void SendDirectionMessage(){
		if(targetMessage == null) return;
		Vector2 axis = Unit();		
		if(axis.x < -0.4 && axis.y > -0.4 && axis.y < 0.4){ //Left
			targetMessage.SendMessage("Left");
		}else
		if(axis.x < -0.4 && axis.y > 0.4){ //LeftUp
			targetMessage.SendMessage("LeftUp");
		}else
		if(axis.x > -0.4 && axis.x < 0.4 && axis.y > 0.4){ //Up
			targetMessage.SendMessage("Up");
		}else
		if(axis.x > 0.4 && axis.y > 0.4){ //RightUp
			targetMessage.SendMessage("RightUp");
		}else
		if(axis.x > 0.4 && axis.y > -0.4 && axis.y < 0.4){ //Right
			targetMessage.SendMessage("Right");
		}else
		if(axis.x > 0.4 && axis.y < -0.4){ //RightDown
			targetMessage.SendMessage("RightDown");
		}else
		if(axis.x < -0.4 && axis.y < -0.4){ //LeftDown
			targetMessage.SendMessage("LeftDown");
		}else
		if(axis.x > -0.4 && axis.x < 0.4 && axis.y < -0.4){ //Down
			targetMessage.SendMessage("Down");
		}
	}
		
	public void Start(){		
		if (background != null) {
						initial_position.y = Screen.height - background.height / 2;
						initial_position.x = background.width / 2;
						input_position = initial_position;
						radius = background.width / 2;
						background_rect = new Rect (
							0, 
							0,
							background.width, 
							background.height
						);

						extendedRect = new Rect (
							0, 
							0,
							background.width+extendRect, 
							background.height+extendRect
						);

						extendedRect.center = initial_position;
						background_rect.center = initial_position;
						
		}
		if (button != null) {
			button_rect = new Rect(
				0, 
				0, 
				button.width, 
				button.height
			);
		}		
	}
	public void OnGUI(){
		if (!show)  return;
		if(background != null && button!=null){
			GUI.DrawTexture(
				background_rect, 
				background
				);
			GUI.DrawTexture(
				button_rect, 
				button
				);
		}
	}

	public Rect Rect(){
		return background_rect;
	}

	public float Vertical(){
		return Mathf.Clamp( (input_real_position.y - initial_position.y)/radius , -1 ,1) *-1;
	}
	public float Horizontal(){
		return Mathf.Clamp ((input_real_position.x - initial_position.x) / radius, -1, 1);
	}
	
	public void ClampInCircle(){			
		float angle = Vector2.Angle(input_position-initial_position, Vector2.right);
		if (input_position.y < initial_position.y)
			angle = 360.0f - angle;
		angleRad = angle * Mathf.Deg2Rad;
		float h = Vector2.Distance (initial_position,input_position);
		CosX = Mathf.Cos (angleRad);
		SinY = Mathf.Sin (angleRad);
		AbsCosX = Mathf.Abs (CosX);
		AbsSinY = Mathf.Abs (SinY);
		if (h > radius) {
			input_real_position.x = initial_position.x + radius * CosX;
			input_real_position.y = initial_position.y + radius * SinY;			
		} else {
			input_real_position.x = input_position.x;
			input_real_position.y = input_position.y;			
		}
	}
	public void SetUnit(Vector2 unit){
		input_position = input_real_position = new Vector2 (initial_position.x+Mathf.Clamp(unit.x, -1 ,1)*radius, ( initial_position.y+Mathf.Clamp(unit.y, -1 ,1)*radius*-1));						
		ClampInCircle ();
	}
	
	public Vector2  Unit(){
		return new Vector2 (
			Mathf.Clamp( (input_real_position.x - initial_position.x)/radius , -1 ,1),
			-1*Mathf.Clamp( (input_real_position.y - initial_position.y)/radius , -1 ,1)
		);
	}
	public void OnMove(Vector2 position){
		input_position.x = position.x;
		input_position.y = Screen.height-position.y;
		ClampInCircle ();
	}
	public bool InRect(Vector2 position){
		Vector2 toCheck = new Vector2();
		toCheck.x = position.x;
		toCheck.y = Screen.height-position.y;
		return background_rect.Contains (toCheck);
	}
	public bool InExtendedRect(Vector2 position){
		Vector2 toCheck = new Vector2();
		toCheck.x = position.x;
		toCheck.y = Screen.height-position.y;
		return extendedRect.Contains (toCheck);
	}

	public void Update(){
		if (background == null || button == null)
			return;
		if (!Using()) {
			input_real_position.x = Mathf.SmoothDamp(input_real_position.x, initial_position.x, ref smoothBackvel, smoothBackTime * AbsCosX);
			input_real_position.y = Mathf.SmoothDamp(input_real_position.y, initial_position.y, ref smoothBackvel, smoothBackTime * AbsSinY);			
		}
		button_rect.center = input_real_position;
	}
}
