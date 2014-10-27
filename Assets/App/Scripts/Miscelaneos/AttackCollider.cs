using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackCollider : MonoBehaviour {

    public List<GameObject> Targets = new List<GameObject>();
 
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("NPC")) {
            GameObject Object = other.gameObject;
            if(!Targets.Contains(Object)){
                Targets.Add(Object);
            }
        }
    }
 
    void OnTriggerExit(Collider other){
        if (other.CompareTag("NPC")) {
          GameObject Object = other.gameObject;
          Targets.Remove(Object);
        }
    }


}
