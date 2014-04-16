using UnityEngine;
using System.Collections;

public class FishIntroScript : MonoBehaviour
{
    public GUISkin skin;
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);
    public GUITexture background;
    public Texture2D gameOverTexture;
	private int scene = 0;
    private bool showMe = true;

	// Use this for initialization
	void Start ()
    {
        windowRect = adjRect(windowRect);
	}

    void OnGUI()
    {
        GUI.depth = -1;
        GUI.skin = skin;
        if (showMe)
        {
            windowRect = GUI.Window(scene, windowRect, windowFunc, "");
        }
    }

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Title
        float width = windowRect.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("POND"), width);
        Rect tempRect = new Rect(leftBuffer, topBuffer, width, height);

        GUI.Label(tempRect, (id == 0) ? "POND" : "GAME OVER");

        // Text
        string text = (id == 0) ?
                "To start, you decide to head to a nearby lake. Fish " +
                        "sell for a fairly decent price at the market, so you decide " +
                        "to try your hand at fishing." :
                "That should be enough. Now that you've caught some fish, why not try selling them at the market?";
        GUI.skin.box.wordWrap = true;
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width); ;
        tempRect = new Rect(leftBuffer, topBuffer + height, width, height2);
        GUI.Box(tempRect, text);

        float btnWidth = width / 2.0f;
        float height3 = GUI.skin.button.CalcHeight(new GUIContent("Next"), width);
        if (id == 0) // next button only
        {
            // Next Button
            tempRect = new Rect(leftBuffer + btnWidth, topBuffer + height + height2, btnWidth, height3);
            if (GUI.Button(tempRect, "Next"))
            {
                showMe = false;
                this.SendMessage("ResumeTimer");
            }
        }
        else // restart and return to map buttons
        {
            // Restart Button
            tempRect = new Rect(leftBuffer, topBuffer + height + height2, btnWidth, height3);
            if (GUI.Button(tempRect, "Play Again") && scene != 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            // Return to Map Button
            tempRect = new Rect(leftBuffer + btnWidth, topBuffer + height + height2, btnWidth, height3);
            if (GUI.Button(tempRect, "Back to Map"))
            {
                Application.LoadLevel("World Map");
            }
        }

        // make window draggable
        GUI.DragWindow();
    }

    void TimeUpEvent()
    {
        int multiplier = 5;
        PlayerPrefs.SetInt("Fish", PlayerPrefs.GetInt("Fish") + PlayerPrefs.GetInt("Score")); // TODO: change to numFish
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + multiplier * PlayerPrefs.GetInt("Score")); // TODO: change to numFish
        background.texture = gameOverTexture;
        scene = 1;
        showMe = true;
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
}
