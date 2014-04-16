using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour
{
    public GUISkin skin;
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);
    public int amount = 100; // amount of money player needs to win game (i.e. pay off his/her taxes)

    private string titleText = "INTRO";
    private string text = "";
	private int scene = 0;
    private bool hasPaid = false;

	// Use this for initialization
	void Start ()
    {
        windowRect = adjRect(windowRect);
        hasPaid = PlayerPrefs.GetInt("Money") >= amount;
	}

    void OnGUI()
    {
        GUI.skin = skin;

        switch(scene)
        {
        case 0:
            titleText = "CASTLE";
            text = "Money in hand, you decide to head to the castle to pay off your debt. Hopefully you have enough to pay off your debt!";
            break;
        case 1:
            titleText = "DEBT COLLECTOR";
            text = "Oh hey, it's the debt kid. Hi, debt " +
                    "kid! Are you ready to fork over all " +
                    "of that hard-earned money of yours? " +
                    "I hope so, because I just LOVE me " +
                    "some coinage.";
            break;
        case 2:
            text = "(...)";
            break;
        case 3:
            if (hasPaid)
            {
                text = "Yes, yes! It's all here! His royal " +
                    "highness will be most pleased that " +
                    "he won't have to mow down your " +
                    "cruddy little house. And maybe I'll " +
                    "take a little cut for myself... Er, " +
                    "nevermind! You didn't hear that. " +
                    "Anyway, congrats. Now get out of " +
                    "here, debt kid!";
            }
            else
            {
                text = "What?! This isn't nearly enough! " +
                    "Get out of here and don't come back " +
                    "unless you've got enough to pay off " +
                    "your entire debt! If you do, I'll " +
                    "kick your butt AND demolish your " +
                    "house. Shoo!";
                text += "\nCurrent Amount: $"+PlayerPrefs.GetInt("Money")+".00";
                text += "\nNeeded: $"+amount+".00";
            }
            break;
        case 4:
            if (!hasPaid)
            {
                Application.LoadLevel("World Map");
            }
            else
            {
                titleText = "CASTLE";
                text = "You finally paid off all of your debt! Now you can live in " +
                    "peace. Congratulations! You've beaten the game. But if you " +
                    "want to test your skill, try playing again and beating your " +
                    "score!";
            }
            break;
        case 5:
            Application.LoadLevel("Credits");
            break;
        }

        windowRect = GUI.Window(0, windowRect, windowFunc, "");
    }

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title
        float width = windowRect.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent(titleText), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);

        GUI.Label(tempRect, titleText);

        // Text
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, text);

        // Prev Button
        float btnWidth = width / 3.0f;
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Next"), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, "Prev") && scene != 0)
        {
            scene--;
        }

        // Skip Button
        tempRect = new Rect(leftBuffer + btnWidth, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, "Skip"))
        {
            Application.LoadLevel((hasPaid) ? "Credits" : "World Map");
        }

        // Next Button
        tempRect = new Rect(leftBuffer + 2 * btnWidth, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, "Next"))
        {
            scene++;
        }

        // make window draggable
        GUI.DragWindow();
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
