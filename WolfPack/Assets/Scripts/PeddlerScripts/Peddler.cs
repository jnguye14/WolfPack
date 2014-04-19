using UnityEngine;
using System.Collections;

public class Peddler : MonoBehaviour
{
    public GUISkin skin;
    public Rect peddlerWindow = new Rect(0f, 0f, 100f, 100f);
    private bool hasMet = false;
    private bool isQuestionDone = false; // after some sec go to true
    private bool isThinking = false;
    private int scene = 0;
    private string text;
    private RiddleGUI rGUI;

    private int money
    {
        get { return rGUI.score * 3; }
    }
    private string moneyT
    {
        get { return money.ToString(); }
    }

    private enum State
    {
        Meeting,
        AskingRiddle,
        Thinking,
        DoneAskingEasy,
        GettingMoney,
        GivenMoney
    }
    State currentState = State.Meeting;

    void Start()
    {
        hasMet = PlayerPrefs.HasKey("MetPeddler");
        peddlerWindow = adjRect(peddlerWindow);
        rGUI = this.GetComponent<RiddleGUI>();
    }

    void Update()
    {
        // answered all questions
        if (currentState == State.AskingRiddle && !rGUI.isShowing)
        {
            if (rGUI.isEasy) // finished answering easy questions, needs to come up with new questions
            {
                currentState = State.Thinking;
            }
            else // finished answering hard questions, needs to get money
            {
                currentState = State.GettingMoney;
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        switch (currentState)
        {
        case State.Meeting: // peddler welcomes player to game
            if (hasMet) // second or later time meeting peddler
            {
                text = "You stop by the peddler again to test your wits and make " +
                        "some more dough.\n\n" +
                            "The Peddler says: \"" +
                            "Back again, I see. Very well, I " +
                            "shall give you my riddles once " +
                            "again, along with a small prize for " +
                            "each one you answer correctly.\"";
            }
            else // first time meeting peddler
            {
                PlayerPrefs.SetInt("MetPeddler", 1);
                text = "Along the way, you run across a strange peddler. He looks " +
                        "kind of sketchy.\n\n" +
                            "The Peddler says: \"" +
                            "Hello, young traveler. Our meeting " +
                            "here must be fate... Care to try " +
                            "your hand at a game? I have several " +
                            "riddles for you to solve. For each " +
                            "one you solve I will bestow upon " +
                            "you a small fortune. Care to try " +
                            "your luck?\"\n\n" +
                        "Seems legit. You decide to answer the mysterious peddler's " +
                        "riddles.";
            }
            break;
        case State.AskingRiddle:
            text = "Can you answer my riddles?";
            break;
        case State.Thinking:
            text = "Maybe those were too easy for you. Hold on While I think of some harder riddles.";
            break;
        case State.DoneAskingEasy:
            text = "Okay, let's try some harder riddles!";
            break;
        case State.GettingMoney:
            text = "Brilliant! Now hold on while I go get your money.";
            break;
        case State.GivenMoney :
            text = "Congratulations you won!\n" +
                    "After answering the strange peddler's riddles, you bid him farewell. Perhaps you can make more money elsewhere.\n" +
                    "Money earned: $" + moneyT + ".00";
            break;
        }

        peddlerWindow = GUI.Window(1, peddlerWindow, windowFunc, "");
    }

    // talking to peddler window
    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title
        float width = peddlerWindow.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("PEDDLER"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, "PEDDLER");

        // Text Box
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, text);

        // Button
        float btnWidth = width;
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Start"), width);
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, btnWidth, height3);
        if (currentState == State.Meeting || currentState == State.DoneAskingEasy)
        {
            if (GUI.Button(tempRect, "Start"))
            {
                rGUI.isShowing = true;
                currentState = State.AskingRiddle;
                this.SendMessage("ResumeTimer"); // start the timer at 30 second default
                this.SendMessage("StartRecording");
            }
        }
        else if (currentState == State.GivenMoney)//scene == 2)
        {
            if(GUI.Button(tempRect, "Map"))
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
                Application.LoadLevel("World Map");
            }
        }

        // make window draggable
        GUI.DragWindow();
    }

    void TimeUpEvent()
    {
        this.SendMessage("RestartTimer");
        this.SendMessage("StopRecording");
        if (rGUI.isEasy)
        {
            this.SendMessage("SaveRecording", "PeddlerCertain");
            if (rGUI.isShowing) // still haven't finished answering questions
            {
                // generate harder question
                rGUI.riddleNumber = Random.Range(5, 10);
                rGUI.isShowing = false;
            }
            currentState = State.DoneAskingEasy;
            rGUI.isEasy = false;
            rGUI.correct = 0; // reset the number correct
        }
        else
        {
            this.SendMessage("SaveRecording", "PeddlerUncertain");
            rGUI.isShowing = false; // in case user still hasn't finished answering questions
            currentState = State.GivenMoney;
        }
    }

    // returns Rectangle adjusted to screen size
    Rect adjRect(Rect r)
    {
        return new Rect(
                r.x * Screen.width / 100.0f,
                r.y * Screen.height / 100.0f,
                r.width * Screen.width / 100.0f,
                r.height * Screen.height / 100.0f);
    }
}