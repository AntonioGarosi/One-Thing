using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Message {
    public Message(int s, int i, string txt, Vector2 p) {
        section = s;
        id = i;
        text = txt;        
        initialPosiiton = p;
    }
    public int section;
    public int id;
    public string text;
    public Vector2 initialPosiiton;
}

[System.Serializable]
public struct ParsingMessage {
    public int section;
    public int id;
    public string text;
    [System.Serializable]
    public struct Position {
        public int x;
        public int y;
    }
    public Position initialPosition;    
}

[System.Serializable]
public struct Messages {
    public ParsingMessage[] messages;
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
    public TextAsset messagesFile;
    public List<Message> gameMessages = new List<Message>();

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
        // Game conditions iniialization
        Conditions c = JsonUtility.FromJson<Conditions>(conditionFile.text);
        foreach (Condition condition in c.conditions) {
            gameConditions.Add(condition);
        }

        // Game messages initialization
        Messages m = JsonUtility.FromJson<Messages>(messagesFile.text);
        foreach (ParsingMessage mes in m.messages) {
            Message tmp = new Message(mes.section, mes.id, mes.text, new Vector2(mes.initialPosition.x, mes.initialPosition.y));           
            gameMessages.Add(tmp);
        }
     
        // Message sections data initialization
        MessageSectionStruct section = new MessageSectionStruct();
        section.messages = new List<Message>();
        foreach (Message mes in gameMessages) {
            if (mes.section == 0) {
                section.messages.Add(mes);
            }
        }
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
