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
            text = "Intro text informing player has reached castle";
            break;
        case 1:
            titleText = "GUARD";
            if (hasPaid)
            {
                text = "You paid off your taxes!";
            }
            else
            {
                text = "You don't have enough money, come back later";
                text += "\nCurrent Amount: $"+PlayerPrefs.GetInt("Money")+".00";
                text += "\nNeeded: $"+amount+".00";
            }
            break;
        case 2:
            if (!hasPaid)
            {
                Application.LoadLevel("World Map");
            }
            else
            {
                Application.LoadLevel("Credits");
            }
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
