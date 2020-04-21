using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour {
    public GameObject iconPrefab;
    public List<Symbol> symbols = new List<Symbol>();

    void Start() {
        
    }

    void Update() {

    }
    public void setupManager(InteractionSectionStruct section) {
    // some operations about RectTransform
        foreach (Icon icon in section.icons) {
            Symbol tmp = Instantiate(iconPrefab, this.transform, false).GetComponent<Symbol>();            
            tmp.setSymbol(icon);
            symbols.Add(tmp.GetComponent<Symbol>());
        }
        checkSymbolsForCondtions();
    }

    public void checkSymbolsForCondtions() {
        foreach (Symbol symbol in symbols) {
            if (GameManager.Instance.checkConditions(symbol.getStarterCondition())) {
                symbol.fade(true);
            }
        }
    }    
    // check on conditions, sprite rendering, message from gamemanager
}
