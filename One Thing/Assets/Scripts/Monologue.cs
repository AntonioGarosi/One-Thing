using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monologue : MonoBehaviour {    
    private Text text;
    string message = "I only have One Thing"; // placeholder text
    public int fontMinSize = 32;
    public int fontMaxSize = 57;

    public float fadeTime = 0.4f;
    private bool fadeIn;
    private bool fadeFlag;

    private bool bounceFlag; // check in bounce state or not
    public int bounceLeap = 4; // how much the text is bounced
    public float bouncePeriod = 8; // every when a bounce state happen
    public float bounceTime = 0.2f; // how long a bounce state lasts
    public float bounceFrequency = 0.5f; // how often a bounce state is set
    public float bounceRate = 0.05f; // how often a bounce effect is repeated inside a bounce state
    private float bounceTimer;

    void Start() {
        this.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        this.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        this.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        text = transform.GetComponent<Text>();
        text.color = GameManager.Instance.primaryColor;
        Color tmp = text.color;
        tmp.a = 0;
        text.color = tmp;
        bounceFlag = false;
        text.text = message;
        updateSize();
        fadeFlag = true;
        fadeIn = true;
    }

    void Update() {
        if (fadeFlag) {
            fade();
        } else {
            fadeIn = false;
        }

        if (bounceFlag) {
            if (Random.value < bounceRate)
                bouncePosition();
            if (Time.time - bounceTimer > bounceTime)
                bounceFlag = false;
        } else {
            if ((int)Time.time % bouncePeriod == 0 && Random.value < bounceFrequency) {
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

    public void fade() {
        Color tmp = text.color;
        float delta = Time.deltaTime / fadeTime;        
        if (fadeIn) {
            tmp.a += delta;
            if (tmp.a >= 1) {
                fadeFlag = false;
                tmp.a = 1;
            }
        } else {
            tmp.a -= delta;
            if (tmp.a <= 0) {
                fadeFlag = false;
                tmp.a = 0;
            }
        }
        text.color = tmp;
    }

    public void setPosition(Vector3 position) {
        transform.localPosition = position;
    }

    public void setRandomPosition() {
        Vector3 tmp = new Vector3(0, 0, transform.localPosition.z);
        bool validFlag = false;
        Rect p = transform.parent.GetComponent<RectTransform>().rect;
        Rect l = transform.GetComponent<RectTransform>().rect;
        float v;
        while (!validFlag) {
            v = Random.Range(0, p.width);
            if (v + l.width < p.width) {
                tmp.x = v;
                validFlag = true;
            }
        }
        validFlag = false;        
        while (!validFlag) {
            v = Random.Range(0, p.height);
            if (v + l.height < p.height) {
                tmp.y = v;
                validFlag = true;
            }
        }
        setPosition(tmp);
    }
}
