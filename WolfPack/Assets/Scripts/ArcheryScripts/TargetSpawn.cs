using UnityEngine;
using System.Collections;

public class TargetSpawn : MonoBehaviour
{
    public GameObject Bow;
    public GameObject Target;
    private Color currentColor;

    // Use this for initialization
    void Start()
    {
        SpawnTarget(Color.green);
        SpawnTarget(Color.red);
        SpawnTarget(Color.black);
        SpawnTarget(Color.blue);
        SpawnTarget(Color.gray);
        SpawnTarget(Color.cyan);
        SpawnTarget(Color.magenta);
        SpawnTarget(Color.white);
        SpawnTarget(Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTarget(Color.green);
        }//*/
    }

    void ParseData(string data)
    {
        // "aim [color]" // sets the current target
        // "shoot" // shoots the arrow
        //Debug.Log ("color: " + data);
        //Color currentColor = Target.renderer.material.color;
        if (data == "green")
        {
            currentColor = Color.green;
        }
        else if (data == "red")
        {
            currentColor = Color.red;
        }
        else if (data == "black")
        {
            currentColor = Color.black;
        }
        else if (data == "blue")
        {
            currentColor = Color.blue;
        }
        else if (data == "gray")
        {
            currentColor = Color.gray;
        }
        else if (data == "cyan")
        {
            currentColor = Color.cyan;
        }
        else if (data == "magenta" || data == "pink" || data == "purple")
        {
            currentColor = Color.magenta;
        }
        else if (data == "white")
        {
            currentColor = Color.white;
        }
        else if (data == "yellow")
        {
            currentColor = Color.yellow;
        }
        else
        {
            currentColor = Color.clear;
        }
        CommandTarget();
        //Target.renderer.material.color = currentColor;//*/
    }

    void CommandTarget()
    {
        GameObject[] Targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject Target in Targets)
        {
            if (Target.renderer.material.color == currentColor)
            {
                // Shoot Arrow at target
                Bow.SendMessage("Shoot", Target);
            }
        }
    }

    void SpawnTarget(Color color)
    {
        Vector3 min = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector3 position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), -1);
        GameObject Target = (GameObject)Instantiate(this.Target, position, Quaternion.identity);
        Target.SendMessage("SetColor", color);
    }
}
