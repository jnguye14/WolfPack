using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Texture2D CoinIcon;
    public Texture2D FishIcon;
	public GUISkin skin;

    public Rect box = new Rect(0.0f,0.0f,50.0f,30.0f);
    public Rect errorBox = new Rect(0.0f, 30.0f, 50.0f, 30.0f); 
    private Map m;

    // error message vars
    private bool showError = false;
    private float startTime = 0.0f;
    private string errorMsg = "";

	// Use this for initialization
	void Start ()
	{
        m = this.GetComponent<Map>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI()
	{
        GUI.skin = skin;
        GUI.contentColor = Color.black;
        GUI.skin.box.alignment = TextAnchor.MiddleLeft;
        GUI.Box(adjRect(box),
                " Score: " + PlayerPrefs.GetInt("Score") + 
                "\n\n Cash: $" + PlayerPrefs.GetInt("Money") + ".00" +
                "\n\n Fish: " + PlayerPrefs.GetInt("Fish") +
                "\n\n Level: " + m.currentNode.GetComponent<Node>().Level);
        float iconLength = box.height / 3.0f;
        /*
        GUI.DrawTexture(new Rect(
                (box.x + box.width - iconLength) * Screen.width / 100.0f,
                (box.y + iconLength) * Screen.height / 100.0f,
                iconLength * Screen.width / 100.0f,
                iconLength * Screen.height / 100.0f),
                CoinIcon);
        GUI.DrawTexture(new Rect(
                (box.x + box.width - iconLength) * Screen.width / 100.0f,
                (box.y + 2*iconLength) * Screen.height / 100.0f,
                iconLength * Screen.width / 100.0f,
                iconLength * Screen.height / 100.0f),
                FishIcon);
        //*/

        if (showError)
        {
            if (Time.time - startTime > 1.0f) // after one second
            {
                showError = false;
            }
            Color temp = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            GUI.Box(adjRect(errorBox), errorMsg);
            GUI.backgroundColor = temp;
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

    void MissingKey(string key)
    {
        startTime = Time.time;
        errorMsg = "Need " + key + " to continue.";
        showError = true;
    }
}
