using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour
{
    public GUISkin skin;
    public Rect LetterRect = new Rect(0f, 60f, 50f, 20f);
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);

    private string titleText = "INTRO";
    private string text = "";
    private int scene = 0;

    // Use this for initialization
	void Start ()
    {
        windowRect = adjRect(windowRect);
        LetterRect = adjRect(LetterRect);
	}

    void OnGUI()
    {
        GUI.skin = skin;
        
        switch(scene)
        {
        case 0:
            titleText = "INTRO";
            text = "You are a young ";
            text += (PlayerPrefs.GetInt("Gender") == 0) ? "boy" : "girl";
            text += " living on ";
            text += (PlayerPrefs.GetInt("Gender") == 0) ? "his" : "her";
            text += " own in the woods. Life is peaceful, albeit uneventful. But one day...";
            break;
        case 1:
            titleText = "OUTSIDE HOUSE";
            text = "You find a note posted on the door of your house. The note reads:";
            LetterRect = GUI.Window(1, LetterRect, LetterFunc, "");
            break;
        case 2:
            text = "Since you're quite fond of your home, you decide to set out at once to raise the money to pay off your debt.";
            break;
        case 3:
            Application.LoadLevel("World Map");
            break;
        }    

        windowRect = GUI.Window(0, windowRect, windowFunc, "");
    }

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title Label
        float width = windowRect.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent(titleText), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, titleText);

        // Text Box
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, text);

        // Prev Button
        float btnWidth = width / 3.0f;
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Next"), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height + height2, btnWidth, height3);
        if(GUI.Button(tempRect, "Prev") && scene != 0)
        {
            scene--;
        }

        // Skip Button
        tempRect = new Rect(leftBuffer + btnWidth, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, "Skip"))
        {
            Application.LoadLevel("World Map");
        }

        // Next Button
        tempRect = new Rect(leftBuffer + 2*btnWidth, topBuffer + height + height2, btnWidth, height3);
        if (GUI.Button(tempRect, "Next"))
        {
            scene++;
        }

        // make window draggable
        GUI.DragWindow();
    }

    void LetterFunc(int id)
    {
        GUILayout.BeginVertical();
        
        GUILayout.Space(50);
        GUILayout.Label("NOTE", "PlainText");
        
        GUILayout.Space(10);
        GUILayout.Label("", "Divider");

        string note = "ATTENTION CHEAP, NON-TAX-PAYING " +
                "RESIDENT:";
        GUILayout.Space(10);
        GUILayout.Label(note, "CursedText");

        GUILayout.Space(10);
        GUILayout.Label("", "Divider");

        GUILayout.BeginHorizontal();
        note = " It has come to our royal " +
                 "attention that you have been living " +
                 "in this forest without royal " +
                 "permission for ten years. As this " +
                 "land now belongs to the kingdom, " +
                 "you must pay a royal property tax " +
                 "for each year you've been living " +
                 "here! Your current royal debt is " +
                 "[MONEY GOAL] and must be paid in " +
                 "full by the end of the day. Failure " +
                 "to do so will result in your house " +
                 "being royally demolished. We " +
                 "realize this hasn't been the law in " +
                 "the past, but we also don't care. " +
                 "Sorry. But not THAT sorry, because " +
                 "we really want your sweet sweet " +
                 "money.";
        GUILayout.Space(10);
        GUILayout.Label(note, "PlainText");
        //GUILayout.TextArea(note);
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(200);
        note = "Signed,\n" +
                "The Royal Collection\n" +
                "Bureau";
        GUILayout.Label(note, "CursedText");
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        //GUILayout.Label("Plain Text", "PlainText");
        //GUILayout.Label("Italic Text", "ItalicText");
        //GUILayout.Label("Light Text", "LightText");
        //GUILayout.Label("Bold Text", "BoldText");
        //GUILayout.Label("Disabled Text", "DisabledText");
        //GUILayout.Label("Cursed Text", "CursedText");
        //GUILayout.Label("Legendary Text", "LegendaryText");
        //GUILayout.Label("Outlined Text", "OutlineText");
        //GUILayout.Label("Italic Outline Text", "ItalicOutlineText");
        //GUILayout.Label("Light Outline Text", "LightOutlineText");
        //GUILayout.Label("Bold Outline Text", "BoldOutlineText");

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

        // seal the letter and make it draggable
        WaxSeal(LetterRect.width, LetterRect.height);
        GUI.DragWindow();
    }

    void WaxSeal(float x, float y)
    {
	    float WSwaxOffsetX = x - 120;
	    float WSwaxOffsetY = y - 115;
	    float WSribbonOffsetX = x - 114;
	    float WSribbonOffsetY = y - 83;
	
	    GUI.Label(new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");
	    GUI.Label(new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");
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
