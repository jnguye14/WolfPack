using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour
{
	public Rect area = new Rect(0f,0f,10f,5f);
	public Texture2D bgTexture;
	public Texture2D fillTexture;
	public float buffer = 5f;
	
	private float barFill = 0.5f;
    public float BarFill
    {
        get
        {
            return barFill;
        }
        set
        {
            barFill = Mathf.Clamp(value,0.0f,1.0f);
        }
    }
    
    public bool Full
    {
        get
        {
            return barFill == 1.0f;
        }
    }
   
    public bool Empty
    {
        get
        {
            return barFill == 0.0f;
        }
    }

    public bool isMoving = true; // flag to indicate whether or not it should move up and down
    public float moveSpeed = 0.01f;
    public bool isIncreasing = false; // decreases first

	// Use this for initialization
	void Start ()
	{
        if (bgTexture == null)
        {
            Texture2D temp = new Texture2D(1, 1);
            temp.SetPixel(0, 0, Color.white);
            temp.Apply();
            bgTexture = temp;
        }
        if (fillTexture == null)
        {
            Texture2D temp = new Texture2D(1, 1);
            temp.SetPixel(0, 0, Color.white);
            temp.Apply();
            fillTexture = temp;
        }
		BarFill = 0.5f; // start game half way
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoving)
        {
            if (isIncreasing)
            {
                increaseBar(moveSpeed);
                if (Full)
                {
                    isIncreasing = !isIncreasing;
                }
            }
            else
            {
                decreaseBar(moveSpeed);
                if (Empty)
                {
                    isIncreasing = !isIncreasing;
                }
            }
        }
        //setAmount(this.GetComponent<Mic>().Loudness);
    }

	void OnGUI()
	{
		GUI.depth = -1; // draw on top of everything

		//GUIStyle style;

        Rect area = adjRect(this.area);
        // draw the background:
		GUI.BeginGroup (area);
		GUI.DrawTexture(new Rect (0f,0f, area.width, area.height), bgTexture);
		
		// change color of fill based on current amount
		if(barFill < 0.33f)
		{
			GUI.color = Color.red;
		}
		else if(barFill < 0.66f)
		{
			GUI.color = Color.yellow;
		}
		else
		{
			GUI.color = Color.green;
		}

		// draw the filled-in part
		GUI.DrawTexture  (new Rect (buffer, buffer, (area.width-2*buffer)*barFill, area.height - 2*buffer), fillTexture);
		GUI.EndGroup ();
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

	public void increaseBar(float amount)
	{
        BarFill += amount;
	}
	
	public void decreaseBar(float amount)
	{
        BarFill -= amount;
	}
	
	public void setAmount(float amount)
	{
		BarFill = amount;
	}
}