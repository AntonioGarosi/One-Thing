using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour {    
    public int highlightOffset = 30;

    private Image targetImage;
    private Image iconImage;

    private Icon icon;

    private void Awake() {
        targetImage = this.transform.Find("Target Image").GetComponent<Image>();
        iconImage = this.transform.Find("Icon Image").GetComponent<Image>();         
    }

    void Start() {                       
    }

    void Update() {
    }

    public void setSymbol(Icon i) {
        icon = i;

        iconImage.sprite = Resources.Load<Sprite>(icon.sprite);
        targetImage.sprite = iconImage.sprite;
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, iconImage.rectTransform.rect.width + highlightOffset);
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, iconImage.rectTransform.rect.height + highlightOffset);

        ColorBlock tmpBlock = transform.GetComponent<Button>().colors;
        tmpBlock.highlightedColor = GameManager.Instance.primaryColor;
        tmpBlock.normalColor = GameManager.Instance.invisibleColor;
        transform.GetComponent<Button>().colors = tmpBlock;
    }

    public int getSection() { return icon.section; }
    public int getId() { return icon.id; }
}
