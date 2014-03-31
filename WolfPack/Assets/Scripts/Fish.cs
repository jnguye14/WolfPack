using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
	public GameObject hook;
    private Vector3 target;
    public string colorName;
	private bool isHooked = false;

	// Use this for initialization
	void Start ()
    {
		hook = GameObject.Find ("Net");
        target = GetRndPos();
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
        // check if fish is close enough to target
        if ((target - this.transform.position).sqrMagnitude < 3)
        {
            if (isHooked)
            {
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                DestroyObject(this.gameObject);
            }
            else // change target
            {
                target = GetRndPos();
            }
        }

        // flip sprite accordingly to direction fish is facing
        Vector3 direction = (target - this.transform.position).normalized;
        if (direction.x < 0)
        {
            this.transform.rotation = Quaternion.identity;
        }
        else
        {
            this.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            direction = new Vector3(-direction.x, direction.y, direction.z);
        }

        // move fish towards target
        this.transform.Translate(Time.fixedDeltaTime * direction);
	}

	void OnGUI()
	{
		GUI.depth = -1;

		Vector3 point = Camera.main.WorldToScreenPoint (this.transform.position);
		GUI.color = this.renderer.material.color;
        GUI.skin.label.fontSize = 20;
		GUI.Label (new Rect(point.x-30,Screen.height+20-point.y,500,50), colorName/*this.renderer.material.color.ToString()*/);
	}

    // returns a random position within the screen space
    Vector3 GetRndPos()
    {
        // Note: z = 9.0f since main camera is -10.0f units away from world origin
        Vector3 temp = new Vector3(Random.Range(0.0f, Screen.width), Random.Range(0.0f, Screen.height), 9.0f);
        return Camera.main.ScreenToWorldPoint(temp);
    }

	void Hooked()
	{
		isHooked = true;
        target = hook.transform.position;
	}
}
