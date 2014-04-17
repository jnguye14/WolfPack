using UnityEngine;
using System.Collections;

public class Market : MonoBehaviour
{
    public GUISkin skin;
    public Rect box = new Rect(0.0f, 0.0f, 50.0f, 50.0f); // for buying
    public Rect otherBox = new Rect(50.0f, 0.0f, 50.0f, 10.0f); // for selling (current inventory)
    public Rect backButton = new Rect(50.0f,10.0f,50.0f,10.0f); // to go back to map
    public Item[] items;

    void Start()
    {
        box = adjRect(box);
        otherBox = adjRect(otherBox);
    }

    /*
    BEFORE MARKET
You decide to head to the town's marketplace. There are
plenty of shops there that will buy fish for a decent amount
of money.
    //*/

    void OnGUI()
    {
        GUI.skin = skin;
        box = GUI.Window(0, box, marketWindow, "");
        otherBox = GUI.Window(1, otherBox, marketWindow, "");

        if (GUI.Button(adjRect(backButton), "Back"))
        {
            Application.LoadLevel("World Map");
        }
    }

    void FancyTop(float topX)
    {
	    float leafOffset = (topX / 2) - 64;
        float frameOffset = (topX / 2) - 27;
        float skullOffset = (topX / 2) - 20;

        GUI.Label(new Rect(leafOffset + 35, 35, 0, 0), "", "RibbonBlue"); // added
        GUI.Label(new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");
        GUI.Label(new Rect(frameOffset, 3, 0, 0), "", "IconFrame");
        //GUI.Label(new Rect(skullOffset, 12, 0, 0), "", "Skull");
    }

    void marketWindow(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer
        float topBuffer = 100;
        float bottomBuffer = 65;

        // fancy top
        float width = (id == 0) ? box.width : otherBox.width;
        FancyTop(width);

        // Title Label
        width -= 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("MARKET"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Label(tempRect, (id == 0) ? "MARKET" : "INVENTORY");

        // inventory, show amount of money you currently have
        if (id == 1)
        {
            string info = "Money: $" + PlayerPrefs.GetInt("Money") + ".00";
            float tempHeight = GUI.skin.label.CalcHeight(new GUIContent(info), width);
            tempRect = new Rect(leftBuffer, topBuffer + height, width, tempHeight);
            GUI.Label(tempRect, info, "PlainText");
            tempRect.y += tempHeight*0.5f;
            GUI.Label(tempRect,"", "Divider");
            height += tempHeight;
        }

        // items to sell
        int index = 0;
        float height2 = (box.height - topBuffer - height - bottomBuffer) / (float)items.Length;
        foreach (Item i in items)
        {
            float offset = topBuffer + height + index * height2;

            // text
            string text = "Item: " + i.name;
            text += (id == 0) ?
                    "\nDescription: " + i.description + "\nCost: $" + i.cost + ".00" :
                    "\nNumber of " + i.name + ": " + PlayerPrefs.GetInt(i.name);
            tempRect = new Rect(
                    leftBuffer,
                    offset,
                    width,
                    height2);
            GUI.skin.box.alignment = TextAnchor.MiddleLeft;
            GUI.Box(tempRect, text);

            // icon TOOD figure out best way to implement this, if at all
            //if (i.icon != null)
            //{
            //    tempRect = new Rect(
            //        leftBuffer,
            //        offset,
            //        width,
            //        height2);
            //    GUI.DrawTexture(tempRect, i.icon);
            //}

            // button
            tempRect = new Rect(
                    leftBuffer + width * 0.75f,
                    offset + height2 * 0.5f,
                    width * 0.25f,
                    height2 * 0.5f);
            if (id == 0) // Buy Button
            {
                if (GUI.Button(tempRect, "Buy") && PlayerPrefs.GetInt("Money") >= i.cost)
                {
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - i.cost);
                    PlayerPrefs.SetInt(i.name, PlayerPrefs.GetInt(i.name) + 1);
                }
            }
            else // Sell Button
            {
                if (GUI.Button(tempRect, "Sell") && PlayerPrefs.GetInt(i.name) > 0)
                {
                    PlayerPrefs.SetInt(i.name, PlayerPrefs.GetInt(i.name) - 1);
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + i.cost);
                }
            }

            index++;
        }

        // make window draggable
        GUI.DragWindow();
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
