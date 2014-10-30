using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PersonController  : Singleton<PersonController> {
	
	public GameObject[] PersonPrefab;
	public GameObject[] PolicePrefab;
	
	public float IntervalSpawn = 5;	
	public float TimeoutIntervalSpawn = 0.15f;

	public int NumberSpawn = 10;	
	public float DistanceSpawn = 30;
	public Vector3 VectorDistanceSpawn = new Vector3(1,1.1f,0);

	private float _CurrentInterval = 0;
	private Character _player;
	private float lastSpawnX = 0;
	public bool active = true;

    private List<GameObject> objects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		_player = Character.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(!active) return;
		_CurrentInterval -= Time.deltaTime;
		if (_CurrentInterval <= 0 && Mathf.Abs(lastSpawnX - _player.transform.position.x) > 35) {
			AddPersons(NumberSpawn);
			AddPolice();
			lastSpawnX=_player.transform.position.x;
			_CurrentInterval = IntervalSpawn;
		}
	}
	public void stop(){
		active = false;
	}
	public void start(){
		active = true;
	}

	public void clear(){
        foreach (GameObject obj in objects)
		{	if(obj == null) continue;
			Destroy(obj);
        }
		objects.Clear();
	}

	void AddPersons(int num){
		if(!active) return;
		if (num <= 0)   return;
		AddPerson ();
		Util.SetTimeout (() => {
			AddPersons (num - 1);
		}, 1);
	}
	
	void AddPerson(){
		if(PersonPrefab.Length > 0){
			GameObject obj = Instantiate(PersonPrefab[UnityEngine.Random.Range(0,PersonPrefab.Length)], new Vector3(
				_player.transform.position.x + DistanceSpawn + VectorDistanceSpawn.x, 
				VectorDistanceSpawn.y, 
				VectorDistanceSpawn.z + UnityEngine.Random.Range(-4.5f, 4.5f)
				), Quaternion.identity) as GameObject;
			objects.Add(obj);
		}
	}
	
	void AddPolice(){
		if(PolicePrefab.Length > 0){
			GameObject obj = Instantiate(PolicePrefab[UnityEngine.Random.Range(0,PolicePrefab.Length)], new Vector3(
				_player.transform.position.x + DistanceSpawn + VectorDistanceSpawn.x, 
				VectorDistanceSpawn.y, 
				VectorDistanceSpawn.z + UnityEngine.Random.Range(-4.5f, 4.5f)
				), Quaternion.identity) as GameObject;
			objects.Add(obj);		
		}
	}



}
