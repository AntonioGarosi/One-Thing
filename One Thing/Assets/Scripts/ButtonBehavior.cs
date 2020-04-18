using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public Color defaultColor = new Color(.0f, .0f, .0f, .0f);
    public Color highlightColor = new Color(0.6367924f, 0.7196355f, 1.0f, 1.0f);
    public int highlightOffset = 30;

    private Image targetImage;
    private Image iconImage;

    void Start()
    {
        // How to deal with black pixels?
        targetImage = this.transform.Find("Target Image").GetComponent<Image>();
        iconImage = this.transform.Find("Icon Image").GetComponent<Image>();
        targetImage.sprite = iconImage.sprite;
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, iconImage.rectTransform.rect.width + highlightOffset);
        targetImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, iconImage.rectTransform.rect.height + highlightOffset);
        ColorBlock tmpBlock = transform.GetComponent<Button>().colors;
        tmpBlock.highlightedColor = highlightColor;
        tmpBlock.normalColor = defaultColor;
        transform.GetComponent<Button>().colors = tmpBlock;
    }

    void Update()
    {   
    }
}
