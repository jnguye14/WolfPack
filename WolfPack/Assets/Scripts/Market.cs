using UnityEngine;
using System.Collections;

public class Market : MonoBehaviour
{
    public Rect box = new Rect(0.0f,0.0f,50.0f,50.0f);
    public Rect otherBox = new Rect(50.0f, 0.0f, 50.0f, 10.0f);
    public Rect backButton = new Rect(50.0f,10.0f,50.0f,10.0f);
    public Item[] items;
    public GUISkin skin;

    /*
    BEFORE MARKET

You decide to head to the town's marketplace. There are
plenty of shops there that will buy fish for a decent amount
of money.

BEFORE PEDDLER (FIRST TIME)

Along the way, you run across a strange peddler. He looks
kind of sketchy.

		PEDDLER
	Hello, young traveler. Our meeting
	here must be fate... Care to try
	your hand at a game? I have several
	riddles for you to solve. For each
	one you solve I will bestow upon
	you a small fortune. Care to try
	your luck?

Seems legit. You decide to answer the mysterious peddler's
riddles.

BEFORE PEDDLER (SECOND TIME)

You stop by the peddler again to test your wits and make
some more dough.

		PEDDLER
	Back again, I see. Very well, I
	shall give you my riddles once
	again, along with a small prize for
	each one you answer correctly.

AFTER PEDDLER

After answering the strange peddler's riddles, you bid him
farewell. Perhaps you can make more money elsewhere.
    //*/

    void OnGUI()
    {
        GUI.skin = skin;
        string info = "Money: $" + PlayerPrefs.GetInt("Money") + ".00";
        foreach (Item i in items)
        {
            info += "\nNumber of " + i.name + ": " + PlayerPrefs.GetInt(i.name);
        }
        GUI.Box(adjRect(otherBox), info);

        int index = 0;
        Rect tempRect;
        foreach(Item i in items)
        {
            // text
            tempRect = new Rect(
                    box.x * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.7f) * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f);
            GUI.Box(tempRect, " Item: " + i.name + "\n Description: " + i.description + "\n Cost: $" + i.cost + ".00");

            // icon
            if (i.icon != null)
            {
                tempRect = new Rect(
                    (box.x + box.width * 0.7f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.1f) * Screen.width / 100.0f,
                    (box.height / items.Length) * Screen.height / 100.0f);
                GUI.DrawTexture(tempRect, i.icon);
            }

            // buy button
            tempRect = new Rect(
                    (box.x + box.width * 0.8f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.1f) * Screen.width / 100.0f,
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
                    (box.x + box.width * 0.9f) * Screen.width / 100.0f,
                    (box.y + index * box.height / items.Length) * Screen.height / 100.0f,
                    (box.width * 0.1f) * Screen.width / 100.0f,
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

        if (GUI.Button(adjRect(backButton), "Back"))
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

    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int cost;
        public Texture2D icon;
    }
}
