using UnityEngine;
using System.Collections;

public class Market : MonoBehaviour
{
    public Rect box = new Rect(0.0f,0.0f,50.0f,50.0f);
    public Rect otherBox = new Rect(50.0f, 0.0f, 50.0f, 10.0f);
    public Rect backButton = new Rect(50.0f,10.0f,50.0f,10.0f);
    public Item[] items;
    public GUISkin skin;

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetInt("Money",100);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.skin = skin;
        Rect tempRect = new Rect(
                otherBox.x * Screen.width / 100.0f,
                otherBox.y * Screen.height / 100.0f,
                otherBox.width * Screen.width / 100.0f,
                otherBox.height * Screen.height / 100.0f);
        string info = "Money: $" + PlayerPrefs.GetInt("Money") + ".00";
        foreach (Item i in items)
        {
            info += "\nNumber of " + i.name + ": " + PlayerPrefs.GetInt(i.name);
        }
        GUI.Box(tempRect, info);

        int index = 0;
        foreach(Item i in items)
        {
            // text
            tempRect = new Rect(
                    box.x * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    box.width * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f);
            GUI.Box(tempRect, " Item: " + i.name + "\n Cost: $" + i.cost + ".00");

            // icon
            tempRect = new Rect(
                    (box.x + box.width * 0.4f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.2f) * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f);
            GUI.DrawTexture(tempRect, i.icon);

            // buy button
            tempRect = new Rect(
                    (box.x + box.width * 0.6f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.2f) * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f); 
            if (GUI.Button(tempRect, "Buy"))
            {
                if (PlayerPrefs.GetInt("Money") >= i.cost)
                {
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")-i.cost);
                    PlayerPrefs.SetInt(i.name, PlayerPrefs.GetInt(i.name) + 1);
                }
            }

            // sell button
            tempRect = new Rect(
                    (box.x + box.width * 0.8f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.2f) * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f); 
            if (GUI.Button(tempRect, "Sell"))
            {
                if (PlayerPrefs.GetInt(i.name) > 0)
                {
                    PlayerPrefs.SetInt(i.name, PlayerPrefs.GetInt(i.name) - 1);
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + i.cost);
                }
            }
            index++;
        }

        tempRect = new Rect(
                backButton.x * Screen.width / 100.0f,
                backButton.y * Screen.height / 100.0f,
                backButton.width * Screen.width / 100.0f,
                backButton.height * Screen.height / 100.0f);
        if (GUI.Button(tempRect, "Back"))
        {
            Application.LoadLevel("World Map");
        }
    }

    [System.Serializable]
    public class Item
    {
        public string name;
        public int cost;
        public Texture2D icon;
    }
}
