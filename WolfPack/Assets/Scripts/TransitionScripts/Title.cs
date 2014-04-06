using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
	public int scene = 0;
	// 0 = title
	// 1 = instructions
	// 2 = character select

	public Rect PlayButton = new Rect(0f,0f,50f,20f);
	public Rect InstructButton = new Rect(0f,60f,50f,20f);
	public Rect BackButton = new Rect(0f,60f,50f,20f);
	public Rect MaleButton = new Rect(0f,0f,50f,20f);
	public Rect FemaleButton = new Rect(0f,60f,50f,20f);
    public Texture2D MaleIcon;
    public Texture2D FemaleIcon;
    public string GameTitle = "Team Wolf Pack's Game";
    public Font titleFont;

    private string titleText = "Team Wolf Pack's Game";
    private Font defaultFont;
	
	public GUISkin skin;
    public GUITexture bg;
    public Texture charSelectScreen;
	bool isOn = false;

	// Use this for initialization
	void Start ()
	{
        titleText = GameTitle;
        if(skin != null)
        {
            defaultFont = skin.font;
        }
        PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () { }

	void OnGUI()
	{
        GUI.skin = skin;
        if (titleFont != null)
        {
            GUI.contentColor = Color.green;
            GUI.skin.font = titleFont;
        }//*/
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);
        GUI.contentColor = Color.cyan;
        GUI.skin.font = defaultFont;
        
		isOn = GUI.Toggle(new Rect(0,0,100,25), isOn, "Music");//52,26
        Rect tempBox;
		switch (scene)
		{
		case 0:
			if (GUI.Button (adjRect(PlayButton), "Play"))
			{
                titleText = "";// "What's your gender?";
                bg.texture = charSelectScreen;
				scene = 2;
			}
			if(GUI.Button (adjRect(InstructButton), "Instructions"))
			{
				titleText = "TUTORIAL";
				scene = 1;
			}
			string disclaimerText = "Upon pressing play, you agree to have your voice recorded for the purposes of this application.";
			GUI.Label (new Rect (0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), disclaimerText);
			break;
		case 1: // Instructions
            if (GUI.Button(adjRect(BackButton), "Back"))
			{
				titleText = GameTitle;
				scene = 0;
			}
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.skin.box.stretchHeight = true;
            GUI.skin.box.wordWrap = true;

			string instructText =
                    "What's that? You want to know how to play the game? Well, " +
                    "it's as easy as speaking into a microphone! In fact, that's " +
                    "exactly how you play. Almost every part of this game is " +
                    "controlled using your own voice. Individual parts of the " +
                    "game will have specific words you have to say or actions you " +
                    "must perform. Some sections will use the mouse as well. More " +
                    "specific instructions will be given before each specific " +
                    "game. Now let's get out there and make some money!";
	   		GUI.Box (new Rect (Screen.width * 0.25f, Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * 0.4f), instructText);
			break;
		case 2: // Character Select
            // Male Button
            if (GUI.Button(adjRect(MaleButton), "Male"))
			{
				PlayerPrefs.SetInt("Gender",0);
				Application.LoadLevel("Intro");
			}

            // Male Icon
            tempBox = new Rect(MaleButton.x / 100.0f * Screen.width,
                    (MaleButton.y + MaleButton.height) / 100.0f * Screen.height,
                    MaleButton.width / 100.0f * Screen.width,
                    MaleButton.width / 100.0f * Screen.width * 0.75f
                    );
            GUI.DrawTexture(tempBox, MaleIcon);

            // Female Button
            if (GUI.Button(adjRect(FemaleButton), "Female"))
			{
				PlayerPrefs.SetInt("Gender",1);
				Application.LoadLevel("Intro");
			}

            // Female Icon
            tempBox = new Rect(FemaleButton.x / 100.0f * Screen.width,
                    (FemaleButton.y + FemaleButton.height) / 100.0f * Screen.height,
                    FemaleButton.width / 100.0f * Screen.width,
                    FemaleButton.width / 100.0f * Screen.width * 0.75f
                    );
            GUI.DrawTexture(tempBox, FemaleIcon);
			break;
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
