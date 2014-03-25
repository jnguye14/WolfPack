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
	bool isOn = false;

	// Use this for initialization
	void Start ()
	{
        defaultFont = skin.font;
        PlayerPrefs.DeleteAll();
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
            GUI.contentColor = Color.green;
            GUI.skin.font = titleFont;
        }//*/
        GUI.Label(new Rect(Screen.width * 0.20f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);
        GUI.contentColor = Color.cyan;
        GUI.skin.font = defaultFont;
        
		isOn = GUI.Toggle(new Rect(0,0,100,25),isOn,"Music");//52,26
        Rect tempBox;
		switch (scene)
		{
		case 0:
            tempBox = new Rect(PlayButton.x/100.0f*Screen.width,
                    PlayButton.y/100.0f*Screen.height,
                    PlayButton.width/100.0f*Screen.width,
                    PlayButton.height/100.0f*Screen.height
                    );
			if (GUI.Button (tempBox, "Play"))
			{
				titleText = "What's your gender?";
				scene = 2;
			}
            tempBox = new Rect(InstructButton.x / 100.0f * Screen.width,
                    InstructButton.y / 100.0f * Screen.height,
                    InstructButton.width / 100.0f * Screen.width,
                    InstructButton.height / 100.0f * Screen.height
                    );
			if(GUI.Button (tempBox, "Instructions"))
			{
				titleText = "Instructions";
				scene = 1;
			}
			string disclaimerText = "Upon pressing play, you agree to have your voice recorded for the purposes of this application.";
			GUI.Label (new Rect (0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), disclaimerText);
			break;
		case 1: // Instructions
            tempBox = new Rect(BackButton.x / 100.0f * Screen.width,
                    BackButton.y / 100.0f * Screen.height,
                    BackButton.width / 100.0f * Screen.width,
                    BackButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Back"))
			{
				titleText = GameTitle;
				scene = 0;
			}
			string instructText = "How To Play: kfjsdiofwsdmxcvhuis";
			GUI.Label (new Rect (0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), instructText);
			break;
		case 2: // Character Select
            // Male Button
            tempBox = new Rect(MaleButton.x / 100.0f * Screen.width,
                    MaleButton.y / 100.0f * Screen.height,
                    MaleButton.width / 100.0f * Screen.width,
                    MaleButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Male"))
			{
				PlayerPrefs.SetInt("Gender",0);
				Application.LoadLevel("World Map");
			}

            // Male Icon
            tempBox = new Rect(MaleButton.x / 100.0f * Screen.width,
                    (MaleButton.y + MaleButton.height) / 100.0f * Screen.height,
                    MaleButton.width / 100.0f * Screen.width,
                    MaleButton.width / 100.0f * Screen.width * 0.75f
                    );
            GUI.DrawTexture(tempBox, MaleIcon);

            // Female Button
            tempBox = new Rect(FemaleButton.x / 100.0f * Screen.width,
                    FemaleButton.y / 100.0f * Screen.height,
                    FemaleButton.width / 100.0f * Screen.width,
                    FemaleButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Female"))
			{
				PlayerPrefs.SetInt("Gender",1);
				Application.LoadLevel("World Map");
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
}
