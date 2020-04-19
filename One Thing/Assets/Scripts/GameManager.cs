using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Message {
    public Message(int s, int i, string txt, Vector2 p, Condition starter) {
        section = s;
        id = i;
        text = txt;        
        initialPosiiton = p;
        starterCondition = starter;
    }
    public int section;
    public int id;
    public string text;
    public Vector2 initialPosiiton;
    public Condition starterCondition;
}

[System.Serializable]
public struct Position {
    public int x;
    public int y;
}

[System.Serializable]
public struct ParsingMessage {
    public int section;
    public int id;
    public string text;    
    public Position initialPosition;
    public Condition starterCondition;
}

[System.Serializable]
struct Messages {
    public ParsingMessage[] messages;
}

public struct MessageSectionStruct {
    public RectTransform rect;
    public List<Message> messages;
}

[System.Serializable]
public struct Condition {
    public Condition(int s, int i, bool f) {
        section = s;
        id = i;
        flag = f;
    }
    public int section;
    public int id;
    public bool flag;
}

[System.Serializable]
struct Conditions {
    public Condition[] conditions;
}

[System.Serializable]
struct ParsingIcon {    
    public int section;
    public int id;
    public string sprite;
    public Position position;
    public Condition[] starterConditions;
    public Condition[] activatingConditions;
}

[System.Serializable]
struct Icons {
    public ParsingIcon[] icons;
}

public struct Icon {
    public Icon(int s, int i, string sp, Vector2 p, List<Condition>sc, List<Condition>ac) {
        section = s;
        id = i;
        sprite = "Assets/Sprites/Icons/" + sp;
        position = p;
        starterConditions = sc;
        activatingConditions = ac;
    }
    public int section;
    public int id;
    public string sprite;
    public Vector2 position;
    public List<Condition> starterConditions;
    public List<Condition> activatingConditions;
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
    public Color invisibleColor = new Color(0, 0, 0, 0);

    public TextAsset conditionFile;
    public TextAsset messagesFile;
    public TextAsset iconsFile;
    public List<Condition> gameConditions = new List<Condition>();
    public List<Message> gameMessages = new List<Message>();
    public List<Icon> gameIcons = new List<Icon>();

    public IconManager iconManager;
    public MessageManager messageManager;

    public List<InteractionSectionStruct> interactionSectionData = new List<InteractionSectionStruct>();
    public List<MessageSectionStruct> messageSectionData = new List<MessageSectionStruct>();
    
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
            Message tmp = new Message(
                mes.section,
                mes.id,
                mes.text,
                new Vector2(mes.initialPosition.x, mes.initialPosition.y),
                new Condition(mes.starterCondition.section, mes.starterCondition.id, mes.starterCondition.flag));           
            gameMessages.Add(tmp);
        }

        // Game icons initializations
        Icons i = JsonUtility.FromJson<Icons>(iconsFile.text);
        foreach (ParsingIcon icon in i.icons) {
            gameIcons.Add(new Icon(
                icon.section,
                icon.id,
                icon.sprite,
                new Vector2(icon.position.x, icon.position.y),
                new List<Condition>(icon.starterConditions),
                new List<Condition>(icon.activatingConditions)
                ));            
        }

        // Ssection message setup
        MessageSectionStruct messageSection = new MessageSectionStruct();
        messageSection.messages = new List<Message>();
        foreach (Message mes in gameMessages) {
            if (mes.section == 0) {
                messageSection.messages.Add(mes);
            }
        }
        messageSectionData.Add(messageSection);        
        messageManager.setupManager(messageSectionData[0]);

        // Section interactors setup
        InteractionSectionStruct interactionSection = new InteractionSectionStruct();

        interactionSection.icons = new List<Icon>();
        foreach (Icon icon in gameIcons) {
            if (icon.section == 0) {
                interactionSection.icons.Add(icon);
            }
        }
        interactionSectionData.Add(interactionSection);
        iconManager.setupManager(interactionSectionData[0]);
    }

    void Update() {
    }

    // this one reverse the state
    public bool changeCondition(int section, int id) {
        bool flag = false;
        int i = 0;
        while (!flag || i < gameConditions.Count) {
            //ugh!
            if (gameConditions[i].section == section && gameConditions[i].id == id) {
                Condition tmp = gameConditions[i];
                tmp.flag = false;
                gameConditions[i] = tmp;
                flag = true;
            }
            i++;
        }
        return flag;
    }

    public bool changeConditions(int[][] values) {
        bool flag = true;
        for (int i = 0; i < values.Length; i++) {
            if (!changeCondition(values[i][0], values[i][1]))
                flag = false;
        }
        return flag;
    }

    public bool changeCondition(int section, int id, bool state) {
        bool flag = false;
        int i = 0;
        while (!flag || i < gameConditions.Count) {
            //ugh!
            if (gameConditions[i].section == section && gameConditions[i].id == id) {
                Condition tmp = gameConditions[i];
                tmp.flag = state;
                gameConditions[i] = tmp;
                flag = true;
                messageManager.SendMessage("checkMonologueForCondition", new Vector2(gameConditions[i].section, gameConditions[i].id));
            }
            i++;
        }
        return flag;
    }

    public bool checkCondition(int section, int id) {
        for (int i = 0; i < gameConditions.Count; i++) {
            if (gameConditions[i].section == section && gameConditions[i].id == id) {
                return gameConditions[i].flag;
            }
        }
        return false;
    }

    public void test() {
        changeCondition(0, 1, true);
    }
}
