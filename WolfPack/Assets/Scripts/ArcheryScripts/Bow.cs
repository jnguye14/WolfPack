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
        // rotate the bow to the mouse angle
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z));
        Vector3 direction = pos - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360.0f;
        }
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x,this.transform.rotation.y,angle);

        // Shoot an arrow
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

        GameObject a = (GameObject)Instantiate(arrow, this.transform.position, this.transform.rotation);//Quaternion.identity);
        //a.transform.LookAt(target.transform);
        //a.SendMessage("SetTargetColor", targetColor);
        a.rigidbody.AddForce(direction * power);
    }
}
