using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    #region Variables
    public GUISkin skin;
    private int scene = 0;
    // 0 = title
    // 1 = character select
    //private bool isOn = false; // for GUI.Toggle if wanted

    // title vars
	public Rect PlayButton = new Rect(0f,0f,50f,20f);
	public Rect InstructButton = new Rect(0f,60f,50f,20f);

    // instruction vars
    private bool showInstruct = false;
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);

    // character select vars
    public GUITexture background;
    public Texture2D charSelectScreen;
    public Texture2D MaleIcon;
    public Texture2D FemaleIcon;
    public Rect maleWinRect = new Rect(0f,0f,100f,100f);
    public Rect femaleWinRect = new Rect(0f, 0f, 100f, 100f);
    #endregion

    void Start ()
	{
        windowRect = adjRect(windowRect);
        maleWinRect = adjRect(maleWinRect);
        femaleWinRect = adjRect(femaleWinRect);
        PlayerPrefs.DeleteAll();
	}

	void OnGUI()
	{
        GUI.skin = skin;
        
		//isOn = GUI.Toggle(new Rect(0,0,100,25), isOn, "Music");//52,26
		switch (scene)
		{
		case 0: // Title
            DrawTitleScreen();
            if (showInstruct)
            {
                windowRect = GUI.Window(0, windowRect, InstructWindow, "");
            }
			break;
		case 1: // Character Select
            DrawCharSelect();
    		break;
		}
	}

    #region Draw Functions: Title Screen, Instructions, and Character Select
    void DrawTitleScreen()
    {
        GUI.skin.button.fontSize = 20;
        // buttons
        if (GUI.Button(adjRect(PlayButton), "Play"))
        {
            background.texture = charSelectScreen;
            scene = 1;
        }
        if (GUI.Button(adjRect(InstructButton), "Instructions"))
        {
            showInstruct = !showInstruct;
        }

        // disclaimer text
        GUI.skin.box.fontSize = 20;
        GUI.skin.box.wordWrap = true;
        string disclaimerText = "Upon pressing play, you agree to have your voice recorded for the purposes of this application.";
        float width = Screen.width / 2.0f;
        float height = GUI.skin.box.CalcHeight(new GUIContent(disclaimerText), width);
        Rect tempRect = new Rect(Screen.width/2.0f - width/2.0f, Screen.height * 0.8f, width, height);
        Color c = GUI.skin.box.normal.textColor;
        GUI.skin.box.normal.textColor = Color.red;
        GUI.Box(tempRect, disclaimerText);
        GUI.skin.box.normal.textColor = c;
    }

    void DrawCharSelect()
    {
        maleWinRect = GUI.Window(0, maleWinRect, CharWindow, "");
        femaleWinRect = GUI.Window(1, femaleWinRect, CharWindow, "");
    }
    #endregion

    #region Window Functions
    void InstructWindow(int id)
    {        
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Tutorial Label
        float width = windowRect.width-2*leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("TUTORIAL"), width);
        GUI.Label(new Rect(leftBuffer, topBuffer, width, height), "TUTORIAL");

        // Instruction Box
        string instructText =
                "What's that? You want to know how to play the game? Well, " +
                "it's as easy as speaking into a microphone! In fact, that's " +
                "exactly how you play. Almost every part of this game is " +
                "controlled using your own voice. Individual parts of the " +
                "game will have specific words you have to say or actions you " +
                "must perform. Some sections will use the mouse as well. More " +
                "specific instructions will be given before each specific " +
                "game. Now let's get out there and make some money!";
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(instructText), width);
        GUI.Box(new Rect(leftBuffer, topBuffer+height, width, height2), instructText);

        height += height2;
        height2 = GUI.skin.button.CalcHeight(new GUIContent("Close"), width);
        if (GUI.Button(new Rect(leftBuffer, topBuffer+height,width,height2), "Close"))
        {
            showInstruct = !showInstruct;
        }

        // make window draggable
        GUI.DragWindow();
    }

    void CharWindow(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Select Character Button
        float width = (id == 0) ? maleWinRect.width : femaleWinRect.width;
        width -= 2 * leftBuffer;
        float height = GUI.skin.button.CalcHeight(new GUIContent("Male"), width); // doesn't matter, just need some string

        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        if (GUI.Button(tempRect, (id == 0) ? "Male" : "Female"))
        {
            PlayerPrefs.SetInt("Gender", id);
            Application.LoadLevel("Intro");
        }

        // Player Icon
        tempRect = new Rect(leftBuffer, topBuffer + height, width, width);
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        GUI.Box(tempRect, (id == 0) ? MaleIcon : FemaleIcon);

        // left click on icon instead of button
        Vector2 pt = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y); // pixel to screen space
        tempRect.x += (id == 0) ? maleWinRect.x : femaleWinRect.x;
        tempRect.y += (id == 0) ? maleWinRect.y : femaleWinRect.y;
        if (Input.GetMouseButtonDown(0) && tempRect.Contains(pt))
        {
            PlayerPrefs.SetInt("Gender", id);
            Application.LoadLevel("Intro");
        }
    }
    #endregion

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
