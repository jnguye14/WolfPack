using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    //public Texture2D CoinIcon;
    //public Texture2D FishIcon;
	public GUISkin skin;

    public Rect box = new Rect(0.0f,0.0f,50.0f,30.0f);
    private Map m;

    // error message vars
    private bool showError = false;
    private float startTime = 0.0f;
    private string errorMsg = "";

	// Use this for initialization
	void Start ()
	{
        box = adjRect(box);
        m = this.GetComponent<Map>();
	}

	void OnGUI()
	{
        GUI.skin = skin;
        box = GUI.Window(0, box, windowFunc, "");
	}

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer
        float topBuffer = 100;
        //float bottomBuffer = 65;

        // main HUD box
        GUI.skin.box.alignment = TextAnchor.MiddleLeft;
        string text = "Score: " + PlayerPrefs.GetInt("Score") +
                "\nCash: $" + PlayerPrefs.GetInt("Money") + ".00" +
                "\nFish: " + PlayerPrefs.GetInt("Fish") +
                "\nLevel: " + m.currentNode.GetComponent<Node>().Level;
        float width = box.width - 2 * leftBuffer;
        float height = GUI.skin.box.CalcHeight(new GUIContent(text), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);
        GUI.Box(tempRect, text);

        // in case of error message
        if (showError)
        {
            if (Time.time - startTime > 1.0f) // after one second
            {
                showError = false;
            }
            float height2 = GUI.skin.box.CalcHeight(new GUIContent("error"), width);
            tempRect = new Rect(leftBuffer, topBuffer+height, width, height2);
            Color c = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            GUI.Box(tempRect, errorMsg);
            GUI.backgroundColor = c;
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

    void MissingKey(string key)
    {
        startTime = Time.time;
        errorMsg = "Need " + key + " to continue.";
        showError = true;
    }
}
