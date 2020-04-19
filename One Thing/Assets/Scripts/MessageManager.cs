using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {
    public GameObject monologuePrefab;
    public List<Monologue> monologues = new List<Monologue>();

    void Start() {        
    }
    
    void Update() {     
    }

    public void setMonologuePosition(string id, Vector2 pos) {
        bool flag = false;
        short i = 0;
        while (!flag || i < monologues.Count) {
            if (monologues[i].checkId(id)) {
                flag = true;
                monologues[i].setPosition(pos);
            }
            i++;
        }
    }

    public void randomizeMonologuesPositions() {
        foreach (Monologue m in monologues) {
            m.setRandomPosition();
        }
    }

    public void setupManager(MessageSectionStruct section) {
        /*RectTransform tmp = GetComponent<RectTransform>();
        tmp.anchorMin = section.rect.anchorMin;
        tmp.anchorMax = section.rect.anchorMax;*/
        for (int i = 0; i < section.messages.Count; i++) {
            GameObject monoPre = Instantiate(monologuePrefab, this.transform, false);            
            monologues.Add(monoPre.GetComponent<Monologue>());
            monologues[i].setMonologue(section.messages[i]);
        }
    
    }
}
