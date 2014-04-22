using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    //public GameObject curTar; // for testing

    private Color targetColor; // ready a color
    private Vector3 targetPos; // aim at position

    private float power
    {
        get
        {
            // bar fill is from 0 to 1
            return this.GetComponent<Bar>().BarFill * 100.0f;
            //return 100.0f;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // for testing
        {
            Shoot();
        }
	}

    void Ready(string s)
    {
        targetColor = Color.white;
    }

    void Aim()
    {
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z));
    }

    void Shoot()
    {
        Aim();
        Vector3 direction = targetPos - this.transform.position;
        //Vector3 direction = curTar.transform.position - this.transform.position;
        //direction.Normalize();
        // F = m * a;
        
        GameObject a = (GameObject)Instantiate(arrow, this.transform.position, Quaternion.identity);
        //a.transform.LookAt(target.transform);
        //a.SendMessage("SetTargetColor", targetColor);
        a.rigidbody.AddForce(direction * power);
    }
}
