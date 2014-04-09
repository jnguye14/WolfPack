using UnityEngine;
using System.Collections;

public class FishIntroScript : MonoBehaviour
{
    public Rect TitleRect = new Rect(0f, 0f, 0f, 0f);
    public Rect TextRect = new Rect(0f, 0f, 0f, 0f);
    public Rect NextButton = new Rect(0f, 0f, 0f, 0f);
    public Rect RestartButton = new Rect(0f, 0f, 0f, 0f);
    public Font titleFont;
    public GUISkin skin;

    private string titleText = "POND";
    private Font defaultFont;
	private int scene = 0;

	// Use this for initialization
	void Start ()
    {
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
        GUI.depth = -1;
        switch(scene)
        {
        case 0: // BEFORE FISHING
            ShowIntro();
            break;
        case 1: // WHILE FISHING
            break;
        case 2: // AFTER FISHING
            ShowEnd();
            break;
        }
    }

    void ShowIntro()
    {
        GUI.skin = skin;
        if (titleFont != null)
        {
            GUI.skin.font = titleFont;
        }//*/
        GUI.contentColor = Color.black;
        GUI.Label(adjRect(TitleRect), titleText);
        GUI.skin.font = defaultFont;

        titleText = "POND"; // JIC
        string text = "To start, you decide to head to a nearby lake. Fish " +
                "sell for a fairly decent price at the market, so you decide " +
                "to try your hand at fishing.";

        GUI.Label(adjRect(TextRect), text);

        if (GUI.Button(adjRect(NextButton), "Next"))
        {
            this.SendMessage("ResumeTimer");
            scene++;
        }
    }

    void TimeUpEvent()
    {
        int multiplier = 5;
        PlayerPrefs.SetInt("Fish", PlayerPrefs.GetInt("Fish") + PlayerPrefs.GetInt("Score")); // TODO: change to numFish
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + multiplier * PlayerPrefs.GetInt("Score")); // TODO: change to numFish
        scene = 2;
    }

    void ShowEnd()
    {
        GUI.skin = skin;
        if (titleFont != null)
        {
            GUI.skin.font = titleFont;
        }//*/
        GUI.contentColor = Color.black;
        GUI.Label(adjRect(TitleRect), titleText);
        GUI.skin.font = defaultFont;

        titleText = "GAME OVER"; // JIC
        string text = "That should be enough. Now that you've caught some fish, why not try selling them at the market?";
        GUI.Label(adjRect(TextRect), text);

        if (GUI.Button(adjRect(RestartButton), "Play Again"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (GUI.Button(adjRect(NextButton), "Back to Map"))
        {
            Application.LoadLevel("World Map");
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
