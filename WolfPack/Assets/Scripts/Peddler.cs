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
    string riddle1 = "How much would a would chuck chuck if a would chuck could chuck wood?";
    string riddle2 = "I am greater than God,\n more evil than the devil,\n the poor have me,\n the rich don't,\n and if you eat me,\n you'll die.\n What am I?";
    string riddle3 = "I am the beginning of the end,\n the end of every place.\nI am the beginning of eternity,\n the end of time and space.\n What am I?";
    string riddle4 = "A natural state,\n I'm sought by all.\nGo without me,\n and you shall fall.\nYou do me when you spend,\nand use me when you eat to no end.\n What am I?";
    string riddle5 = "Mary's father has 4 children;\n three are named Nana, Nene, and Nini.\n So what is is the 4th child's name?";
    string riddle6 = "The more of them you take,\n the more you leave behind.\n What are they?";
    string riddle7 = "It lives without a body,\n hears without ears,\n speaks without a mouth,\n and is born in air.\n What is it?";
    string riddle8 = "It cannot be seen,\ncannot be felt cannot be heard,\n cannot be smelt it lies behind stars\n and under hills and empty holes it fills";
    string riddle9 = "I got it in a forest but didn't want it.\n Once I had it, I couldn't see it.\n The more I searched for it, the less I liked it.\n I took it home in my hand because I could not find it.\n What was it?";
    string riddle10 = "A clever thief in the olden days was charged\n with treason against the king and sentenced to death.\n But the king decided to be a little lenient\n so he let the thief choose his own way to die.\n What way should the thief choose?";
	
	public int riddleNumber;
	Random rand = new Random ();

		public string GameTitle = "Peddler Mine-Game";
		public Font titleFont;
		
	private string titleText = "Peddler Mine-Game";
		private Font defaultFont;
	public int score = 0;
	public string scoreT;
	public int correct = 0;
	public int money;
	public string moneyT;
	public string voiceAnswer;
		
		public GUISkin skin;
	public GUIText ScoreText;
		//bool isOn = false;
		
		// Use this for initialization
		void Start ()
		{
			defaultFont = skin.font;
            titleText = GameTitle;
		}
		
		// Update is called once per frame
		void Update ()
		{
		    scoreT = score.ToString ();
		    //ScoreText.text = "Score: " + score;
        }
		void ParseData(string data)
		{
			if(data == "choose answer a")
			{
				voiceAnswer = "a";
			}
			if(data == "choose answer b")
			{
				voiceAnswer = "b";
			}
			if(data == "choose answer c")
			{
				voiceAnswer = "c";
			}
			if(data == "choose answer d")
			{
				voiceAnswer = "d";
			}
		}
		
		
		void OnGUI()
		{
			GUI.skin = skin;
			if (titleFont != null)
			{
				GUI.contentColor = Color.green;
				GUI.skin.font = titleFont;
			}//*/
            GUI.skin.label.fontSize = 20;
            Vector2 dimensions = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(titleText));
            Rect riddleRect = new Rect(Screen.width/2.0f - dimensions.x / 2.0f, Screen.height * 0.1f,dimensions.x,dimensions.y);
			//GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.2f), titleText);
            GUI.Label(riddleRect, titleText);
		GUI.contentColor = Color.cyan;
			GUI.skin.font = defaultFont;
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Score: " + scoreT);
			
			
			
			switch (scene)
			{
			case 0:
				if (GUI.Button (StartButton, "Start"))
				{
				riddleNumber = Random.Range(1,11);	
				if(riddleNumber==1){
				titleText = riddle1;//"How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 2;
				}
				if(riddleNumber==2){
					titleText = riddle2;//"I am greater than God, more evil than the devil, the poor have me, the rich don't, and if you eat me, you'll die. What am I?"
					scene = 3;
				}
				if(riddleNumber==3){
					titleText = riddle3;//"I am the beginning of the end, the end of every place.I am the beginning of eternity, the end of time and space. What am I?";
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;//"A natural state, I'm sought by all.Go without me, and you shall fall.You do me when you spend,and use me when you eat to no end. What am I?";
					scene = 10;
				}
			}
		



				
				break;
			case 1:
				
			if (GUI.Button (AButton, "E")|| voiceAnswer== "a"){
				score +=10;
				correct +=1;
				riddleNumber =  Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}
			/*	if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}*/
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
			}
			if (GUI.Button (BButton, "Heaven")|| voiceAnswer== "b"){			
				score -=5;
			}
			if (GUI.Button (CButton, "God")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "Yourself")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
				break;
			case 2:
				
			if (GUI.Button (AButton, "none")|| voiceAnswer== "a"){
				score +=10;
				correct +=1;
                
				riddleNumber = Random.Range(1,11);
				/*if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}*/
				if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
			}
			if (GUI.Button (BButton, "a few")|| voiceAnswer== "b"){			
				score -=5;
			}
			if (GUI.Button (CButton, "a lot")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "tons")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
				
				
				break;
		case 3:
			if (GUI.Button (AButton, "The air")|| voiceAnswer== "a"){
				score -=5;

			}
			if (GUI.Button (BButton, "Nothing")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;

			}
			if (GUI.Button (CButton, "money")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "The Church")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 4:
			if (GUI.Button (AButton, "Power")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Nothing")|| voiceAnswer== "b"){			
				score -=5;
			}
			if (GUI.Button (CButton, "Balance")|| voiceAnswer== "c"){
				riddleNumber =  Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;//"How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 2;
				}
				if(riddleNumber==2){
					titleText = riddle2;//"How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 3;
				}
				if(riddleNumber==3){
					titleText = riddle3;//"How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 1;
				}
				/*if(riddleNumber==4){
					titleText = riddle4;//"How much would a would chuck chuck if a would chuck could chuck wood?";
					scene = 4;
				}*/
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;			
				correct +=1;

			}
			if (GUI.Button (DButton, "Knowledge")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 5:
			if (GUI.Button (AButton, "5a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Mary")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "5c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "5d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 6:
			if (GUI.Button (AButton, "6a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Footsteps")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "6c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "6d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 7:
			if (GUI.Button (AButton, "7a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Echo")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "7c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "7d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 8:
			if (GUI.Button (AButton, "8a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Darkness")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "8c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "8d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 9:
			if (GUI.Button (AButton, "9a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Splinter")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "9c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "9d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 10:
			if (GUI.Button (AButton, "10a")|| voiceAnswer== "a"){
				score -=5;
				
			}
			if (GUI.Button (BButton, "Oldage")|| voiceAnswer== "b"){			
				riddleNumber = Random.Range(1,11);
				if(riddleNumber==1){
					titleText = riddle1;
					scene = 2;
				}
				/*if(riddleNumber==2){
					titleText = riddle2;
					scene = 3;
				}*/
				if(riddleNumber==3){
					titleText = riddle3;
					scene = 1;
				}
				if(riddleNumber==4){
					titleText = riddle4;
					scene = 4;
				}
				if(riddleNumber==5){
					titleText = riddle5;
					scene = 5;
				}
				if(riddleNumber==6){
					titleText = riddle6;
					scene = 6;
				}
				if(riddleNumber==7){
					titleText = riddle7;
					scene = 7;
				}
				if(riddleNumber==8){
					titleText = riddle8; 
					scene = 8;
				}
				if(riddleNumber==9){
					titleText = riddle9;
					scene = 9;
				}
				if(riddleNumber==10){
					titleText = riddle10;
					scene = 10;
				}
				score +=10;
				correct +=1;
				
			}
			if (GUI.Button (CButton, "10c")|| voiceAnswer== "c"){
				score -=5;
			}
			if (GUI.Button (DButton, "10d")|| voiceAnswer== "d"){
				score -=5;
			}
			if(correct ==4){
				scene =11;
			}
			break;
		case 11:
			titleText = "Congratulations you won!";
			money =	score * 3;
			moneyT = money.ToString();
			GUI.Box(new Rect(Screen.width/2, Screen.height/2+50,175,25), "Money earned: " +moneyT);
            if(GUI.Button(StartButton, "Map"))
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
                Application.LoadLevel("World Map");
            }
			break;

			}
		}
	
}