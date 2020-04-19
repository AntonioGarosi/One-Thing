using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Message {
    public Message(string txt = "", string i = "", Vector2 p = new Vector2()) {
        text = txt;
        id = i;
        initialPosiiton = p;
    }
    public string text;
    public string id;
    public Vector2 initialPosiiton;
}

public struct MessageSectionStruct {
    public RectTransform rect;
    public List<Message> messages;
}

[System.Serializable]
public struct Condition {
    public Condition(int s, int i) {
        section = s;
        id = i;
        flag = false;
    }
    public int section;
    public int id;
    public bool flag;
}

[System.Serializable]
public struct Conditions {
    public Condition[] conditions;
}

public struct Icon {
    public Icon(string i) {
        id = i;        
        conditions = new List<string>();
        activactionConditions = new List<string>();
    }
    
    string id;
    List<string> conditions;
    List<string> activactionConditions;
}

public struct InteractionSectionStruct {
    public RectTransform rect;
    public List<Icon> icons;
}

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance {
        get {
            return instance;
        }
    }

    public Color primaryColor = new Color(0.6367924f, 0.7196355f, 1.0f, 1.0f);
    public List<MessageSectionStruct> messageSectionData = new List<MessageSectionStruct>();
    public MessageManager messageManager;

    public TextAsset conditionFile;
    public List<Condition> gameConditions = new List<Condition>();

    public List<InteractionSectionStruct> interactionSectionData = new List<InteractionSectionStruct>();
    public IconManager iconManager;
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        // Game conditions initialization
        Conditions c = JsonUtility.FromJson<Conditions>(conditionFile.text);
        foreach (Condition condition in c.conditions) {
            gameConditions.Add(condition);
        }

        foreach (Condition co in gameConditions) {
            Debug.Log(co.id);
        }

        // Message sections data initialization
        MessageSectionStruct section = new MessageSectionStruct();
        section.messages = new List<Message>();
        Message message = new Message("I only have One Thing", "0-0", new Vector2(0, 100));
        section.messages.Add(message);
        message = new Message("and it is devouring me", "0-1", new Vector2(0, 0));
        section.messages.Add(message);
        messageSectionData.Add(section);

        // Message manager setup
        messageManager.setupManager(messageSectionData[0]);

        // Interaction section data initialization

        // Icon manager setup
        // iconManager.setupManager(InteractionSectionStruct[0]);
    }


    void Update() {
    }
}
