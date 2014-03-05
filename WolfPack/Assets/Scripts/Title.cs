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

	private string titleText = "Team Wolf Pack's Game";
	
	public GUISkin skin;
	bool isOn = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width*0.25f, Screen.height * 0.1f, Screen.width*0.5f, Screen.height * 0.2f), titleText);

		isOn = GUI.Toggle(new Rect(0,0,52,26),isOn,"");

		switch (scene)
		{
		case 0:
			if (GUI.Button (PlayButton, "Play"))
			{
				titleText = "What's your gender?";
				scene = 2;
			}
			if(GUI.Button (InstructButton, "Instructions"))
			{
				titleText = "Instructions";
				scene = 1;
			}
			string disclaimerText = "Upon pressing play, you agree to have your voice recorded for the purposes of this application.";
			GUI.Label (new Rect (0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), disclaimerText);
			break;
		case 1:
			if(GUI.Button (BackButton, "Back"))
			{
				titleText = "Team Wolf Pack's Game";
				scene = 0;
			}
			string instructText = "How To Play: kfjsdiofwsdmxcvhuis";
			GUI.Label (new Rect (0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), instructText);
			break;
		case 2:
			if (GUI.Button (MaleButton, "Male"))
			{
				PlayerPrefs.SetInt("Gender",0);
				Application.LoadLevel("World Map");
			}
			if(GUI.Button (FemaleButton, "Female"))
			{
				PlayerPrefs.SetInt("Gender",1);
				Application.LoadLevel("World Map");
			}
			break;
		}
	}
}
