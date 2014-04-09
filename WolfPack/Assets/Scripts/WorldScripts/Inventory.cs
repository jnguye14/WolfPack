using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public KeyCode inventoryKey = KeyCode.I;
    public GUISkin skin;
    private bool showInventory = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            showInventory = !showInventory;
        }
	}

    void OnGUI()
    {
        if (showInventory)
        {
            GUI.skin = skin;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.contentColor = Color.black;
            GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.5f),
                    "INVENTORY" +
                    "\n\nNumber of Nets: " + PlayerPrefs.GetInt("Net") +
                    "\nNumber of Fish: " + PlayerPrefs.GetInt("Fish") +
                    "\nNumber of Bows: " + PlayerPrefs.GetInt("Bow") + 
                    "\nTotal Number of Arrows: " + (PlayerPrefs.GetInt("Arrows") * 5)); // arrows come in sets of five
        }
    }
}
