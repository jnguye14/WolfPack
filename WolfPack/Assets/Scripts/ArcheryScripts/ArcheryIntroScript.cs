using UnityEngine;
using System.Collections;

public class ArcheryIntroScript : MonoBehaviour
{
    public Rect TitleRect = new Rect(0f, 0f, 0f, 0f);
    public Rect TextRect = new Rect(0f, 0f, 0f, 0f);
    public Rect NextButton = new Rect(0f, 0f, 0f, 0f);
    public Rect RestartButton = new Rect(0f, 0f, 0f, 0f);
    public Font titleFont;
    public GUISkin skin;

    private string titleText = "ARCHERY";
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
        case 0: // BEFORE ARCHERY
            ShowIntro();
            break;
        case 1: // WHILE ARCHERY
            scene++; // temp
            break;
        case 2: // AFTER ARCHERY
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

        titleText = "ARCHERY"; // JIC
        string text = "It seems that there's an archery competition going on. If"
                + " you do well in it you might get a lot of money... You decide"
                + " to try it out..";

        GUI.Label(adjRect(TextRect), text);

        if (GUI.Button(adjRect(NextButton), "Next"))
        {
            //this.SendMessage("ResumeTimer"); // temp
            scene++;
        }
    }

    void TimeUpEvent()
    {
        //PlayerPrefs.SetInt("Fish", PlayerPrefs.GetInt("Fish") + PlayerPrefs.GetInt("Score"));
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
        string text = "Looks like you won! Good job. I'm proud of you. Now why not"
                + "take your winnings and make some more cash?";
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
