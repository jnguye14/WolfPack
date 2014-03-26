using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour
{
	public Rect NextButton = new Rect(0f,60f,50f,20f);
	public Rect SkipButton = new Rect(0f,60f,50f,20f);
    public Rect LetterRect = new Rect(0f, 60f, 50f, 20f);
    public Font titleFont;

    private string titleText = "INTRO";
    private Font defaultFont;
	private int scene = 0;

	public GUISkin skin;

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
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.skin = skin;
        if (titleFont != null)
        {
            GUI.skin.font = titleFont;
        }//*/
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);
        GUI.skin.font = defaultFont;

        string text = "";
        Rect tempRect;
        switch(scene)
        {
        case 0:
            titleText = "INTRO";
            text = "You are a young ";
            text += (PlayerPrefs.GetInt("Gender") == 0) ? "boy" : "girl";
            text += " living on their own in the woods.\n" +
                    "Life is peaceful, albeit uneventful. But one day...";
            break;
        case 1:
            titleText = "OUTSIDE HOUSE";
            text = "You find a note posted on the door of your house. The note reads:";
            string note = "\nNOTE\n\n"+
                    "ATTENTION CHEAP, NON-TAX-PAYING\n"+
                    "RESIDENT: It has come to our royal\n" +
                    "attention that you have been living\n" +
                    "in this forest without royal\n" +
                    "permission for ten years. As this\n" +
                    "land now belongs to the kingdom,\n" +
                    "you must pay a royal property tax\n" +
                    "for each year you've been living\n" +
                    "here! Your current royal debt is\n" +
                    "[MONEY GOAL] and must be paid in\n" +
                    "full by the end of the day. Failure\n" +
                    "to do so will result in your house\n" +
                    "being royally demolished. We\n" +
                    "realize this hasn't been the law in\n" +
                    "the past, but we also don't care.\n" +
                    "Sorry. But not THAT sorry, because\n" +
                    "we really want your sweet sweet\n" +
                    "money.\n" +
                    "     Signed,   The Royal Collection\n" +
                    "     Bureau";
            tempRect = new Rect(
                    LetterRect.x * Screen.width / 100.0f,
                    LetterRect.y * Screen.height / 100.0f,
                    LetterRect.width * Screen.width / 100.0f,
                    LetterRect.height * Screen.height / 100.0f);
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.Box(tempRect,note);
                /*
                              NOTE
                    ATTENTION CHEAP, NON-TAX-PAYING
                    RESIDENT: It has come to our royal
                    attention that you have been living
                    in this forest without royal
                    permission for ten years. As this
                    land now belongs to the kingdom,
                    you must pay a royal property tax
                    for each year you've been living
                    here! Your current royal debt is
                    [MONEY GOAL] and must be paid in
                    full by the end of the day. Failure
                    to do so will result in your house
                    being royally demolished. We
                    realize this hasn't been the law in
                    the past, but we also don't care.
                    Sorry. But not THAT sorry, because
                    we really want your sweet sweet
                    money.
                         Signed,   The Royal Collection
                         Bureau
                //*/
            break;
        case 2:
            text = "Since you're quite fond of your home, you decide to set out at once to raise the money to pay off your debt.";
            break;
        case 3:
            Application.LoadLevel("World Map");
            break;
        }

        tempRect = new Rect(
                Screen.width * 0.1f,
                Screen.height * 0.3f,
                Screen.width * 0.2f,
                Screen.height * 0.5f);
        GUI.Label(tempRect, text);

        tempRect = new Rect(
                NextButton.x * Screen.width / 100.0f,
                NextButton.y * Screen.height / 100.0f,
                NextButton.width * Screen.width / 100.0f,
                NextButton.height * Screen.height / 100.0f);
        if (GUI.Button(tempRect, "Next"))
        {
            scene++;
        }

        tempRect = new Rect(
                SkipButton.x * Screen.width / 100.0f,
                SkipButton.y * Screen.height / 100.0f,
                SkipButton.width * Screen.width / 100.0f,
                SkipButton.height * Screen.height / 100.0f);
        if (GUI.Button(tempRect, "Skip"))
        {
            Application.LoadLevel("World Map");
        }
    }
}
