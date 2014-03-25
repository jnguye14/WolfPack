using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Texture2D CoinIcon;
    public Texture2D FishIcon;
	public GUISkin skin;

    public Rect box = new Rect(0.0f,0.0f,50.0f,30.0f);
    private Map m;

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
        GUI.Box(new Rect(
                box.x * Screen.width / 100.0f,
                box.y * Screen.height / 100.0f,
                box.width * Screen.width / 100.0f,
                box.height * Screen.height / 100.0f),
                " Score: " + PlayerPrefs.GetInt("Score") + 
                "\n\n Cash: $" + PlayerPrefs.GetInt("Money") + ".00" +
                "\n\n Fish: " + PlayerPrefs.GetInt("Fish") +
                "\n\n Level: " + m.currentNode.GetComponent<Node>().Level);
        float iconLength = box.height / 3.0f;
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
        ;
	}
}
