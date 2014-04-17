using UnityEngine;
using System.Collections;

public class Peddler : MonoBehaviour
{
    // GUI vars
    public GUISkin skin;
    public Rect peddlerWindow = new Rect(0f, 0f, 100f, 100f);
    private int scene = 0;
    // 0 = start
    // 1 = game
    // 2 = end

    // Riddle vars
    private Riddle[] Riddles;
    private int riddleNumber;
    public class Riddle
    {
        public string text;
        public int correctNum;
        public string[] answer;

        public Riddle() { }

        public Riddle(string riddle, string[] answers, int corNum)
        {
            text = riddle;
            correctNum = corNum;
            answer = answers;
        }
    }

    // TODO: actual riddle data. bad way to do this, better if we serialize the Riddle class, but this works good enough for now
    private string[] riddles = new string[]
    {
        "How much would a would chuck chuck if a would chuck could chuck wood?",
        "I am greater than God,\n more evil than the devil,\n the poor have me,\n the rich don't,\n and if you eat me,\n you'll die.\n What am I?",
        "I am the beginning of the end,\n the end of every place.\nI am the beginning of eternity,\n the end of time and space.\n What am I?",
        "A natural state,\n I'm sought by all.\nGo without me,\n and you shall fall.\nYou do me when you spend,\nand use me when you eat to no end.\n What am I?",
        "Mary's father has 4 children;\n three are named Nana, Nene, and Nini.\n So what is is the 4th child's name?",
        "The more of them you take,\n the more you leave behind.\n What are they?",
        "It lives without a body,\n hears without ears,\n speaks without a mouth,\n and is born in air.\n What is it?",
        "It cannot be seen,\ncannot be felt cannot be heard,\n cannot be smelt it lies behind stars\n and under hills and empty holes it fills",
        "I got it in a forest but didn't want it.\n Once I had it, I couldn't see it.\n The more I searched for it, the less I liked it.\n I took it home in my hand because I could not find it.\n What was it?",
        "A clever thief in the olden days was charged\n with treason against the king and sentenced to death.\n But the king decided to be a little lenient\n so he let the thief choose his own way to die.\n What way should the thief choose?"
    };
    private string[][] answers = new string[][]
    {
        new string[]{"none", "a few", "a lot", "tons"},
        new string[]{"The air", "Nothing", "money", "The Church"},
        new string[]{"E", "Heaven", "God", "Yourself"},
        new string[]{"Power", "Nothing", "Balance", "Knowledge"},
        new string[]{"5a", "Mary", "5c", "5d"},
        new string[]{"6a", "Footsteps", "6c", "6d"},
        new string[]{"7a", "Echo", "7c", "7d"},
        new string[]{"8a", "Darkness", "8c", "8d"},
        new string[]{"9a", "Splinter", "9c", "9d"},
        new string[]{"10a", "Oldage", "10c", "10d"}
    };
    private int[] correctAnswers = new int[]
    {
        1, // none
        1, // Nothing
        0, // E
        1, // Balance
        1, // Mary
        1, // Footsteps
        1, // Echo
        1, // Darkness
        1, // Splinter
        1  // Oldage
    };

    // other game vars
    private string voiceAnswer;
    private int correct = 0; // the number of riddles answered correctly
    private int score = 0;
    private string scoreT
    {
        get { return score.ToString(); }
    }
    private int money
    {
        get { return score * 3; }
    }
    private string moneyT
    {
        get { return money.ToString(); }
    }

    void Start()
    {
        peddlerWindow = adjRect(peddlerWindow);
        Riddles = new Riddle[10];
        for (int i = 0; i < 10; i++)
        {
            Riddles[i] = new Riddle(riddles[i], answers[i], correctAnswers[i]);
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        switch (scene)
        {
            case 0: // Before Game
                peddlerWindow = GUI.Window(0, peddlerWindow, windowFunc, "");
                break;
            case 1: // During Game
                peddlerWindow = GUI.Window(0, peddlerWindow, riddleWinFunc, "");
                break;
            default:
                peddlerWindow = GUI.Window(1, peddlerWindow, windowFunc, "");
                break;
        }
    }

    void riddleWinFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title Label
        float width = peddlerWindow.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("PEDDLER"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, "PEDDLER");

        // Riddle Text Box
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(Riddles[riddleNumber].text), width);
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, Riddles[riddleNumber].text);

        // Button answers
        for (int i = 0; i < 4; i++)
        {
            height += height2;
            height2 = GUI.skin.button.CalcHeight(new GUIContent(Riddles[riddleNumber].answer[i]), width);
            float offset = topBuffer + height + height2;
            tempRect = new Rect(
                    leftBuffer,
                    offset,
                    width,
                    height2);
            if(GUI.Button(tempRect, Riddles[riddleNumber].answer[i]))
            {
                AnswerRiddle(i);
            }
        }

        // make window draggable
        GUI.DragWindow();
    }

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

        // Text
        string text = (id == 0) ?
                "Hello, let's play a game!" :
                "Congratulations you won!\n" +
                    "After answering the strange peddler's riddles, you bid him farewell. Perhaps you can make more money elsewhere.\n" +
                    "Money earned: " + moneyT;
        /* // Branden's script
BEFORE PEDDLER (FIRST TIME)
Along the way, you run across a strange peddler. He looks
kind of sketchy.
		PEDDLER
	Hello, young traveler. Our meeting
	here must be fate... Care to try
	your hand at a game? I have several
	riddles for you to solve. For each
	one you solve I will bestow upon
	you a small fortune. Care to try
	your luck?
Seems legit. You decide to answer the mysterious peddler's
riddles.

BEFORE PEDDLER (SECOND TIME)
You stop by the peddler again to test your wits and make
some more dough.
		PEDDLER
	Back again, I see. Very well, I
	shall give you my riddles once
	again, along with a small prize for
	each one you answer correctly.
         * //*/

        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, text);

        // Start Button
        float btnWidth = width;
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Start"), width);
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, (id == 0) ? "Start" : "Map"))
        {
            if(id == 0)
            {
                riddleNumber = Random.Range(0, 10);
                scene++;
            }
            else
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
                Application.LoadLevel("World Map");
            }
        }

        // make window draggable
        GUI.DragWindow();
    }

    void ParseData(string data)
    {
        if (data == "choose answer a")
        {
            AnswerRiddle(0);//voiceAnswer = "a";
        }
        if (data == "choose answer b")
        {
            AnswerRiddle(1);//voiceAnswer = "b";
        }
        if (data == "choose answer c")
        {
            AnswerRiddle(2);//voiceAnswer = "c";
        }
        if (data == "choose answer d")
        {
            AnswerRiddle(3);//voiceAnswer = "d";
        }
    }

    void AnswerRiddle(int a)
    {
        if (a == Riddles[riddleNumber].correctNum)
        {
            score += 10;
            correct++;
            if (correct == 4)
            {
                scene++;
            }
            riddleNumber = Random.Range(0, 10);
        }
        else
        {
            score -= 5;
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