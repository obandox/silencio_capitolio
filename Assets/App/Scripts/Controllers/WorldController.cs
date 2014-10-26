using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	public GameObject[] ColumnPrefabs;
	public float[] ColumnZPos;
	public float ColumnsDistance = 23;

	private int currentColumn;
	private float lastX;
	private Character _player;
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
		if (lastX <= _player.transform.position.x + 40) {
			AddColumn();
		}
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
