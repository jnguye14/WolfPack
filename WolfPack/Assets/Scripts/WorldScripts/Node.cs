using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Node : MonoBehaviour
{
	public string Level;
	public string Key1;
	public string Key2;
    public Vector2 Coordinate = Vector2.zero;

    private GameObject map;
    private GameObject prev = null;
    private GameObject path1 = null;
    private GameObject path2 = null;
    private bool isPathUnlocked;
	private bool isAltPathUnlocked;

	// Use this for initialization
	void Start () 
	{
		isPathUnlocked = (PlayerPrefs.GetInt (Key1) == 1) ? false : true;//true : false;
		isAltPathUnlocked = (PlayerPrefs.GetInt (Key2) == 1) ? false : true;//true : false;
		map = GameObject.Find ("Map");

        if (this.transform.parent != null)
        {
            prev = this.transform.parent.gameObject;
        }
        if (this.transform.childCount > 0)
        {
            path1 = this.transform.GetChild(0).gameObject;
            if (this.transform.childCount > 1)
            {
                path2 = this.transform.GetChild(1).gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Coordinate.x/100.0f, Coordinate.y/100.0f, 10.0f));
        if (path1 != null)
        {
            LineRenderer l = path1.GetComponent<LineRenderer>();
            l.SetColors(new Color(0.65f,0.16f,0.16f), new Color(0.65f,0.16f,0.16f));
            //l.SetColors(Color.green, Color.blue);
            l.SetWidth(0.1f, 0.5f);
            l.SetPosition(0, this.transform.position + new Vector3(0, 0, 1));
            l.SetPosition(1, path1.transform.position + new Vector3(0, 0, 1));
        }
        if (path2 != null)
        {
            LineRenderer l = path2.GetComponent<LineRenderer>();
            l.SetColors(new Color(0.65f, 0.16f, 0.16f), new Color(0.65f, 0.16f, 0.16f));
            //l.SetColors(Color.green, Color.blue);
            l.SetWidth(0.1f, 0.5f);
            l.SetPosition(0, this.transform.position + new Vector3(0, 0, 1));
            l.SetPosition(1, path2.transform.position + new Vector3(0, 0, 1));
        }
    }
	
	public void GoBack()
	{
        if (prev != null)
        {
            map.SendMessage("SetNode", prev);
        }
	}
	
	public void GoForward()
	{
		if (isPathUnlocked && path1 != null)
		{
			map.SendMessage ("SetNode", path1);
		}
	}
	
	public void GoAltForward()
	{
		if (isAltPathUnlocked && path2 != null)
		{
			map.SendMessage ("SetNode", path2);
		}
	}

	public void GoToLevel()
	{
        if (Application.CanStreamedLevelBeLoaded(Level))
        {
            Application.LoadLevel(Level);
        }
	}
}
