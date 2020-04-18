using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour {
    public int fontMinSize = 32;
    public int fontMaxSize = 57;
    private Text text;
    public string message;

    private bool bounceFlag; // check in bounce state or not
    public int bounceLeap = 4; // how much the text is bounced
    public float bouncePeriod = 5; // every when a bounce state happen
    public float bounceTime = 0.2f; // how long a bounce state lasts
    public float bounceFrequency = 0.5f; // how often a bounce state is set
    public float bounceRate = 0.05f; // how often a bounce effect is repeated inside a bounce state
    private float bounceTimer;

    void Start() {
        bounceFlag = false;
        text = transform.GetComponent<Text>();
        text.text = message;
        updateSize();
    }

    void Update() {
        if (bounceFlag) {
            if (Random.value < bounceRate)
                bouncePosition();
            if (Time.time - bounceTimer > bounceTime)
                bounceFlag = false;
        } else {
            if ((int)Time.time % bouncePeriod == 0 && Random.value < bounceFrequency)
            {
                bounceFlag = true;
                bounceTimer = Time.time;
            }
        }
    }

    // metodo per tagliare alcune parole
    // metodo per riposizionare il testo

    public void updateSize() {
        text.fontSize = Random.Range(fontMinSize, fontMaxSize);
    }

    public void bouncePosition() {
        // add a way to check that the text remains inside the message panel
        int mx, my;
        mx = (int)Random.Range(-bounceLeap, bounceLeap); 
        my = (int)Random.Range(-bounceLeap, bounceLeap);
        Vector3 tmp = transform.GetComponent<RectTransform>().position;
        tmp.x += mx;
        tmp.y += my;
        transform.GetComponent<RectTransform>().position = tmp;
    }
}
