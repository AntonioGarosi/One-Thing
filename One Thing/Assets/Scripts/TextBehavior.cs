using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour {
    public int fontMinSize = 32;
    public int fontMaxSize = 57;
    private Text text;    

    void Start() {
        text = transform.GetComponent<Text>();
        updateSize();
    }

    void Update() {        
    }

    // metodo per effetto shaking
    // metodo per tagliare alcune parole
    // metodo per riposizionare il testo
    // metodo per aggiustare dimensione testo

    public void updateSize() {
        text.fontSize = Random.Range(fontMinSize, fontMaxSize);
    }
}
