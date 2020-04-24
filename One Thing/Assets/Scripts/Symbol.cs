using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Symbol : MonoBehaviour {    
    public int highlightOffset = 30;

    private Image targetImage;
    private Image iconImage;

    private Icon icon;

    bool fadeFlag = false;
    bool fadeDirection;
    float fadeTime = 0.4f;

    private void Awake() {
        targetImage = this.transform.Find("Target Image").GetComponent<Image>();
        iconImage = this.transform.Find("Icon Image").GetComponent<Image>();         
    }

    void Start() {
        Sprite tmp = Resources.Load<Sprite>(icon.sprite);
        iconImage.sprite = tmp;
        targetImage.sprite = iconImage.sprite;
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, iconImage.rectTransform.rect.width + highlightOffset);
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, iconImage.rectTransform.rect.height + highlightOffset);

        ColorBlock tmpBlock = transform.GetComponent<Button>().colors;
        tmpBlock.highlightedColor = GameManager.Instance.primaryColor;
        tmpBlock.normalColor = GameManager.Instance.invisibleColor;
        transform.GetComponent<Button>().colors = tmpBlock;
    }

    void Update() {
        if (fadeFlag) {
            int d = fadeDirection ? 1 : -1;
            Color tmp = iconImage.color;
            tmp.a += (Time.deltaTime/ fadeTime) * d;
            if (tmp.a >= 1.0f || tmp.a <= 0.0f) {
                fadeFlag = false;
                if (fadeDirection) {
                    tmp.a = 1;
                } else {
                    iconImage.enabled = false;                 
                }
            }
            iconImage.color = tmp;
        }
    }

    public void setSymbol(Icon i) {
        icon = i;
        iconImage.enabled = false;
        targetImage.enabled = false;
    }

    public int getSection() { return icon.section; }
    public int getId() { return icon.id; }
    public List<Condition> getStarterCondition() {
        return icon.starterConditions;
    }
    public void fade(bool direction) { // direction: true fades in; false fades out
        fadeFlag = true;
        fadeDirection = direction;
        if (direction) {
            Color tmp = iconImage.color;
            tmp.a = 0.1f; // slightly above zero to avoid false checks
            iconImage.color = tmp;
            iconImage.enabled = true;
            targetImage.enabled = true;
        } else {
            targetImage.enabled = false;
            GameManager.Instance.revertConditions(icon.starterConditions);
        }
    }

    public void sendConditionChange() {
        GameManager.Instance.changeConditions(icon.activatingConditions);
    }

}


