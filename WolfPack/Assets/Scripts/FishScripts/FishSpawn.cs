using UnityEngine;
using System.Collections;

public class FishSpawn : MonoBehaviour {
	public GameObject Fish;
	private Color currentColor;

	// Use this for initialization
	void Start ()
	{
		SpawnFish(Color.green);
		SpawnFish(Color.red);
		SpawnFish(Color.black);
		SpawnFish(Color.blue);
		SpawnFish(Color.gray);
		SpawnFish(Color.cyan);
		SpawnFish(Color.magenta);
		SpawnFish(Color.white);
		SpawnFish(Color.yellow);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
	   {
			SpawnFish(Color.green);
		}
	}

	void ParseData(string data)
	{
		//Debug.Log ("color: " + data);
		//Color currentColor = Fish.renderer.material.color;
		if(data == "green")
		{
			currentColor = Color.green;
		}
		else if(data == "red")
		{
			currentColor = Color.red;
		}
		else if(data == "black")
		{
			currentColor = Color.black;
		}
		else if(data == "blue")
		{
			currentColor = Color.blue;
		}
		else if(data == "gray")
		{
			currentColor = Color.gray;
		}
		else if(data == "cyan")
		{
			currentColor = Color.cyan;
		}
		else if(data == "magenta" || data == "pink" || data == "purple")
		{
			currentColor = Color.magenta;
		}
		else if(data == "white")
		{
			currentColor = Color.white;
		}
		else if(data == "yellow")
		{
			currentColor = Color.yellow;
		}
		else
		{
            currentColor = Color.clear;
		}
		CommandFish ();
		//Fish.renderer.material.color = currentColor;//*/
	}

	void CommandFish()
	{
		GameObject[] fishes = GameObject.FindGameObjectsWithTag ("Fish");
		foreach (GameObject fish in fishes)
		{
			if(fish.renderer.material.color == currentColor)
			{
				fish.SendMessage("Hooked");
			}
		}
	}

	void SpawnFish(Color color)
	{
		Vector3 min = Camera.main.ScreenToWorldPoint(Vector3.zero);
		Vector3 max = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));

		Vector3 position = new Vector3 (Random.Range (min.x,max.x),Random.Range(min.y,max.y),-1);
		GameObject Fish = (GameObject)Instantiate(this.Fish, position, Quaternion.identity);
		Fish.SendMessage("SetColor",color);
	}
}
