using UnityEngine;
using System.Collections;

public class RiddleGUI : MonoBehaviour
{
    // GUI vars
    public GUISkin skin;
    public bool isShowing = false;
    public Rect peddlerWindow = new Rect(0f, 0f, 100f, 100f);

    // Riddle vars
    private Riddle[] Riddles;
    public int riddleNumber;
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
        0, // none
        1, // Nothing
        0, // E
        2, // Balance
        1, // Mary
        1, // Footsteps
        1, // Echo
        1, // Darkness
        1, // Splinter
        1  // Oldage
    };

    // other game vars
    public bool isEasy = true;
    public int correct = 0; // the number of riddles answered correctly
    public int score = 0;
    private string scoreT
    {
        get { return score.ToString(); }
    }

    void Start()
    {
        peddlerWindow = adjRect(peddlerWindow);
        Riddles = new Riddle[10];
        for (int i = 0; i < 10; i++)
        {
            Riddles[i] = new Riddle(riddles[i], answers[i], correctAnswers[i]);
        }
        riddleNumber = Random.Range(0, 5);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (isShowing)
        {
            peddlerWindow = GUI.Window(0, peddlerWindow, riddleWinFunc, "");
        }
    }

    // actual window displaying riddles
    void riddleWinFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title Label
        float width = peddlerWindow.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("RIDDLE"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, "RIDDLE #" + riddleNumber);

        // Riddle Text Box
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(Riddles[riddleNumber].text), width);
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, Riddles[riddleNumber].text);

        float height3 = GUI.skin.box.CalcHeight(new GUIContent("Score"), width);
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, width, height3);
        GUI.Label(tempRect, "Score: " + scoreT);

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
            Color temp = GUI.backgroundColor;
            Color c = GUI.backgroundColor;
            switch (i)
            {
                case 0: c = Color.blue; break;
                case 1: c = Color.red; break;
                case 2: c = Color.green; break;
                case 3: c = Color.yellow; break;
            }
            GUI.backgroundColor = c;
            if (GUI.Button(tempRect, Riddles[riddleNumber].answer[i]))
            {
                AnswerRiddle(i); // TODO: Will be disabled for SR
            }
            GUI.backgroundColor = temp;
        }

        // make window draggable
        GUI.DragWindow();
    }

    void ParseData(string data)
    {
        if (data == "choose answer blue")
        {
            AnswerRiddle(0);
        }
        if (data == "choose answer red")
        {
            AnswerRiddle(1);
        }
        if (data == "choose answer green")
        {
            AnswerRiddle(2);
        }
        if (data == "choose answer yellow")
        {
            AnswerRiddle(3);
        }
    }

    void AnswerRiddle(int a)
    {
        if (!isShowing) // same as canAnswer, if it's not showing, you can't answer
        {
            return;
        }

        //this.GetComponent<script>().isGettingData = false;
        if (a == Riddles[riddleNumber].correctNum)
        {
            score += 10;
            correct++;
            if (correct == 2)
            {
                isShowing = false;
            }
            int oldNum = riddleNumber;
            while (riddleNumber == oldNum) // so it doesn't come up with the same riddle twice
            {
                if (isEasy)
                {
                    riddleNumber = Random.Range(0, 5);
                }
                else
                {
                    riddleNumber = Random.Range(5, 10);
                }
            }
        }
        else
        {
            score -= 5;
        }
        //this.GetComponent<script>().isGettingData = true;
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
