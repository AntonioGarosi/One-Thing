using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {
    public GameObject monologuePrefab;
    void Start() {
        Debug.Log(transform.position);
        GameObject mono = Instantiate(monologuePrefab, this.transform, false);
        mono.name = "Mono";
    }
    
    void Update() {
        
    }

    public void setMonologuePosition() {
        //transform.Find("Mono").GetComponent<Monologue>().setPosition(new Vector3(100, 100, 0));
        transform.Find("Mono").GetComponent<Monologue>().setRandomPosition();
    }
}
