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
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        // Message sections data initialization
        MessageSectionStruct section = new MessageSectionStruct();
        section.messages = new List<Message>();

        Message message = new Message("I only have One Thing", "0-0", new Vector2(0, 100));
        section.messages.Add(message);

        Message message1 = new Message("and it is devouring me", "0-1", new Vector2(0, 0));
        section.messages.Add(message1);

        messageSectionData.Add(section);

        // Message manager setup
        messageManager.setupManager(messageSectionData[0]);
    }

    void Update() {

    }
}
