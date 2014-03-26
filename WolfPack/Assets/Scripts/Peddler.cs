using UnityEngine;
using System.Collections;

public class Peddler : MonoBehaviour {
	public int scene = 0;
		// 0 = title
		// 1 = instructions
		// 2 = character select
		
	public Rect StartButton = new Rect(0f,0f,50f,20f);
		public Rect AButton = new Rect(0f,0f,70f,20f);
		public Rect BButton = new Rect(0f,60f,50f,20f);
		public Rect CButton = new Rect(0f,60f,50f,20f);
		public Rect DButton = new Rect(0f,0f,70f,20f);
		
	public string Title= "Peddler  Mini game";


		public string GameTitle = "Peddler Mine-Game";
		public Font titleFont;
		
	private string titleText = "Peddler Mine-Game";
		private Font defaultFont;
	public int score = 0;
	public string scoreT;
		
		public GUISkin skin;
	public GUIText ScoreText;
		//bool isOn = false;
		
		// Use this for initialization
		void Start ()
		{
			defaultFont = skin.font;
		}
		
		// Update is called once per frame
		void Update ()
		{
		scoreT = score.ToString ();
		//ScoreText.text = "Score: " + score;
		}
		
		void OnGUI()
		{
			GUI.skin = skin;
			if (titleFont != null)
			{
				GUI.contentColor = Color.green;
				GUI.skin.font = titleFont;
			}//*/
			GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.2f), titleText);
		GUI.Box(new Rect(Screen.width/2, Screen.height/2,100,25), "Score: " +scoreT);
		GUI.contentColor = Color.cyan;
			GUI.skin.font = defaultFont;
			
			
			
			switch (scene)
			{
			case 0:
				if (GUI.Button (StartButton, "Start"))
				{
			
					titleText = "How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 2;

			}
		



				
				break;
			case 1:
				
			if (GUI.Button (AButton, "E")){
				score +=1;
				scene = 3;
				titleText = " I am greater than God, more evil than the devil, the poor have me, the rich don't, and if you eat me, you'll die. What am I?";
			}
			if (GUI.Button (BButton, "Heaven")){			
				score -=1;
			}
			if (GUI.Button (CButton, "God")){
				score -=1;
			}
			if (GUI.Button (DButton, "Yourself")){
				score -=1;
			}
				break;
			case 2:
				
			if (GUI.Button (AButton, "none")){
				score +=1;
				scene = 1;
				titleText = "I am the beginning of the end, the end of every place.I am the beginning of eternity, the end of time and space. What am I?";
			}
			if (GUI.Button (BButton, "a few")){			
				score -=1;
			}
			if (GUI.Button (CButton, "a lot")){
				score -=1;
			}
			if (GUI.Button (DButton, "tons")){
				score -=1;
			}
				
				
				
				break;
		case 3:
			if (GUI.Button (AButton, "The air")){
				score -=1;

			}
			if (GUI.Button (BButton, "Nothing")){			
				scene = 4;
				titleText = "A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
				score +=1;
			}
			if (GUI.Button (CButton, "money")){
				score -=1;
			}
			if (GUI.Button (DButton, "The Church")){
				score -=1;
			}
			break;
		case 4:
			if (GUI.Button (AButton, "Power")){
				score -=1;
				
			}
			if (GUI.Button (BButton, "Nothing")){			
				score -=1;
			}
			if (GUI.Button (CButton, "Balance")){
				scene = 4;
				titleText = "A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
				score +=1;
			}
			if (GUI.Button (DButton, "Knowledge")){
				score -=1;
			}
			break;
			}
		}
	
}