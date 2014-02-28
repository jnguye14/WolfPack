using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public GameObject prev;
	public GameObject path1;
	public GameObject path2;

	public string Level;
	public string Key1;
	public string Key2;

	private GameObject map;
	private bool isPathUnlocked;
	private bool isAltPathUnlocked;

	// Use this for initialization
	void Start () 
	{
		isPathUnlocked = (PlayerPrefs.GetInt (Key1) == 1) ? false : true;//true : false;
		isAltPathUnlocked = (PlayerPrefs.GetInt (Key2) == 1) ? false : true;//true : false;
		map = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GoBack()
	{
		map.SendMessage ("SetNode", prev);
	}
	
	public void GoForward()
	{
		if (isPathUnlocked)
		{
			map.SendMessage ("SetNode", path1);
		}
	}
	
	public void GoAltForward()
	{
		if (isAltPathUnlocked)
		{
			map.SendMessage ("SetNode", path2);
		}
	}

	public void GoToLevel()
	{
		Application.LoadLevel (Level);
	}
}
