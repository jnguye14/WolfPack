using UnityEngine;
using System.Collections;

public class CampfireGUI : MonoBehaviour
{
    public GUISkin skin;
    public Rect windowRect = new Rect(0f, 0f, 100f, 100f);

	// Use this for initialization
	void Start ()
    {
        windowRect = adjRect(windowRect);
    }
	
	void OnGUI ()
    {
        GUI.skin = skin;
        windowRect = GUI.Window(0, windowRect, windowFunc, "");	
	}

    void windowFunc(int id)
    {
        // window buffer values
        float leftBuffer = 50; // should be same as rightBuffer ...and bottomBuffer?
        float topBuffer = 100;

        // Tutorial Label
        float width = windowRect.width - 2 * leftBuffer;
        float height = GUI.skin.label.CalcHeight(new GUIContent("CAMPFIRE"), width);
        GUI.Label(new Rect(leftBuffer, topBuffer, width, height), "CAMPFIRE");

        // Dialogue Box
        string text = "You decide to try some cooking. Cooked fish will no doubt " +
                "sell for more at the market, so Put on that chef's hat and " +
                "make some food!\nToo bad you don't know how to cook...";
        float height2 = GUI.skin.box.CalcHeight(new GUIContent(text), width);
        GUI.Box(new Rect(leftBuffer, topBuffer + height, width, height2), text);

        height += height2;
        height2 = GUI.skin.button.CalcHeight(new GUIContent("Back"), width);
        if (GUI.Button(new Rect(leftBuffer, topBuffer + height, width, height2), "Back to map"))
        {
            Application.LoadLevel("World Map");
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
}
