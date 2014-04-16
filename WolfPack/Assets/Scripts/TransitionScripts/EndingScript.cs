using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour
{
	public Rect NextButton = new Rect(0f,60f,50f,20f);
    public Font titleFont;
    public int amount = 100; // amount of money player needs to win game (i.e. pay off his/her taxes)

    private string titleText = "INTRO";
    private Font defaultFont;
	private int scene = 0;
    private bool hasPaid = false;
	public GUISkin skin;

	// Use this for initialization
	void Start ()
    {
        hasPaid = PlayerPrefs.GetInt("Money") >= amount;
        scene = 0;
        if (skin != null)
        {
            defaultFont = skin.font;
        }
	}
	
	// Update is called once per frame
	void Update () { }

    void OnGUI()
    {
        GUI.skin = skin;
        if (titleFont != null)
        {
            GUI.skin.font = titleFont;
        }//*/

        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.1f), titleText);
        GUI.skin.font = defaultFont;

        string text = "";
        switch(scene)
        {
        case 0:
            titleText = "CASTLE";
            text = "Money in hand, you decide to head to the castle to pay off your debt. Hopefully you have enough to pay off your debt!";
            break;
        case 1:
            titleText = "DEBT COLLECTOR";
            text = "Oh hey, it's the debt kid. Hi, debt\n" +
                    "kid! Are you ready to fork over all\n" +
                    "of that hard-earned money of yours?\n" +
                    "I hope so, because I just LOVE me\n" +
                    "some coinage.\n";
            break;
        case 2:
            text = "     (...)\n";
            break;
        case 3:
            if (hasPaid)
            {
                text = "Yes, yes! It's all here! His royal\n" +
                    "highness will be most pleased that\n" +
                    "he won't have to mow down your\n" +
                    "cruddy little house. And maybe I'll\n" +
                    "take a little cut for myself... Er,\n" +
                    "nevermind! You didn't hear that.\n" +
                    "Anyway, congrats. Now get out of\n" +
                    "here, debt kid!";
            }
            else
            {
                text = "What?! This isn't nearly enough!\n" +
                    "Get out of here and don't come back\n" +
                    "unless you've got enough to pay off\n" +
                    "your entire debt! If you do, I'll\n" +
                    "kick your butt AND demolish your\n" +
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
                text = "You finally paid off all of your debt! Now you can live in\n" +
                    "peace. Congratulations! You've beaten the game. But if you\n" +
                    "want to test your skill, try playing again and beating your\n" +
                    "score!";
            }
            break;
        case 5:
            Application.LoadLevel("Credits");
            break;
        }

        Rect tempRect = new Rect(
                Screen.width * 0.3f,
                Screen.height * 0.2f,
                Screen.width * 0.4f,
                Screen.height * 0.5f);
        GUI.Label(tempRect, text);

        if (GUI.Button(adjRect(NextButton), "Next"))
        {
            scene++;
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
