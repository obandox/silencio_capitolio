using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {

	public enum WorldState{Init, Loop, Last, End};

	public WorldState status = WorldState.Loop;
	public GameObject[] ColumnPrefabs;
	public float[] ColumnZPos;
	public float ColumnsDistance = 23;

	public float EndDistance = 7920;

	private int currentColumn;
	private float lastX;
	private Character _player;


	public float TimerTap = 360;
	public float TimerTapRecover = 10;
	public float TimerTapRemove = 20;
	public float CurrentTimerTap = 0;

	// Use this for initialization
	void Start () {
		_player = Character.Instance;
	}

	float nextColumnZ(){
		float value = 0;
		if (currentColumn < ColumnZPos.Length) {
			value = ColumnZPos [currentColumn];
		} else {
			currentColumn=0;
			if (currentColumn < ColumnZPos.Length) {
				value = ColumnZPos [currentColumn];
			}
		}
		currentColumn += 1;
		return value;
	}

	// Update is called once per frame
	void Update () {
		if(status == WorldState.Init){
			init();
		}else
		if(status == WorldState.Loop){
			loop();
		}else
		if(status == WorldState.Last){
			last();
		}else
		if(status == WorldState.End){
			end();
		}

	}
	void loop(){		
		if (_player != null && lastX <= _player.transform.position.x + 40) {
			AddColumn();
		}
		if(_player.transform.position.x >= EndDistance){
			status = WorldState.Last;
			CurrentTimerTap = TimerTap;
			_player.stop();
			PersonController.Instance.stop();
			PersonController.Instance.clear();
		}
	}
	void init(){
		
	}
	void last(){
		bool something = false;
		if(OControl.GetButton("ButtonZ")){
			something = true;
			CurrentTimerTap -= TimerTapRemove * Time.deltaTime;
		}
		if(OControl.GetButton("ButtonX")){
			something = true;
			CurrentTimerTap -= TimerTapRemove * Time.deltaTime;
		}
		if(OControl.GetButton("ButtonC")){
			something = true;
			CurrentTimerTap -= TimerTapRemove * Time.deltaTime;
		}
		if(OControl.GetButton("ButtonV")){
			something = true;
			CurrentTimerTap -= TimerTapRemove * Time.deltaTime;
		}
		if(!something)
			CurrentTimerTap += TimerTapRecover * Time.deltaTime;

		CurrentTimerTap = Mathf.Clamp(CurrentTimerTap, 0, TimerTap);
		if(CurrentTimerTap <= 0) 
			status = WorldState.End;
	}
	void end(){
		Debug.Log("END");
	}

	void gameover(){
		StartCoroutine (reload());
	}
	IEnumerator reload(){
		yield return new WaitForSeconds (5);  // or however long you want it to wait
		Application.LoadLevel(Application.loadedLevel);
	}
	float AddColumn(){
		lastX += ColumnsDistance;
		Instantiate(ColumnPrefabs[UnityEngine.Random.Range(0,ColumnPrefabs.Length)], new Vector3(
				lastX, 
				1.1f, 
				nextColumnZ()
			), Quaternion.identity);
		return ColumnsDistance;
	}
}
