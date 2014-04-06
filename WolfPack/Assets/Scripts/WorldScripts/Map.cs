using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public GameObject currentNode;
	public GameObject Player;
	public float speed = 5.0f;

	private bool isMoving = false;

	// Use this for initialization
	void Start ()
	{
        // default value, i.e. no previous level
        if(PlayerPrefs.GetString("PrevLevel") == "")
        {
            Player.transform.position = currentNode.transform.position;
            return;
        }

        GameObject[] Nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject n in Nodes)
        {
            if (n.GetComponent<Node>().Level == PlayerPrefs.GetString("PrevLevel"))
            {
                currentNode = n;
                break;
            }
        }
        Player.transform.position = currentNode.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isMoving)
		{
            Vector3 direction = (currentNode.transform.position - Player.transform.position);
			if (direction.sqrMagnitude < 0.01f)
			{
				isMoving = false;
			}
			direction.Normalize();
			Player.transform.Translate(direction * Time.deltaTime * speed);
		}
		else
		{
            Player.transform.position = currentNode.transform.position;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				// go backwards
				currentNode.SendMessage("GoBack");
			}
			
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)
			    || Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))
			{
				// move forwards
				currentNode.SendMessage("GoForward");
			}
			
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow))
			{
				// move alternate fowards
				currentNode.SendMessage("GoAltForward");
			}
			
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKey(KeyCode.Return))
			{
				// go to node's scene
				currentNode.SendMessage ("GoToLevel");
			}
		}
	}

	void SetNode(GameObject newNode)
	{
		currentNode = newNode;
		isMoving = true;
	}
}
