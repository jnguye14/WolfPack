using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
	public GameObject hook;
    public string colorName;
	private bool isHooked = false;

	// Use this for initialization
	void Start () {
		hook = GameObject.Find ("Net");
	}

    void SetColor(Color color)
    {
        this.renderer.material.color = color;
        if (color == Color.green)
        {
            colorName = "Green";
        }
        else if (color == Color.red)
        {
            colorName = "Red";
        }
        else if (color == Color.black)
        {
            colorName = "Black";
        }
        else if (color == Color.blue)
        {
            colorName = "Blue";
        }
        else if (color == Color.gray)
        {
            colorName = "Gray";
        }
        else if (color == Color.cyan)
        {
            colorName = "Cyan";
        }
        else if (color == Color.magenta)
        {
            colorName = "Magenta";
        }
        else if (color == Color.white)
        {
            colorName = "White";
        }
        else if (color == Color.yellow)
        {
            colorName = "Yellow";
        }
    }

	// Update is called once per frame
	void Update ()
	{
		if (isHooked)
		{
			this.transform.Translate(Time.fixedDeltaTime* (hook.transform.position-this.transform.position));
			if((hook.transform.position - this.transform.position).sqrMagnitude < 3)
			{
				PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+1);
				DestroyObject(this.gameObject);
			}
		}
	}

	void OnGUI()
	{
		GUI.depth = -1;

		Vector3 point = Camera.main.WorldToScreenPoint (this.transform.position);
		GUI.color = this.renderer.material.color;
        GUI.skin.label.fontSize = 20;
		GUI.Label (new Rect(point.x-30,Screen.height+20-point.y,500,50), colorName/*this.renderer.material.color.ToString()*/);
	}

	void Hooked()
	{
		isHooked = true;
	}
}
