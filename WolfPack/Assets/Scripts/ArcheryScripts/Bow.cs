using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public GameObject curTar; // for testing
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
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) // for testing
        {
            Shoot(curTar);
        }
	}

    void Shoot(GameObject target)
    {
        GameObject a = (GameObject)Instantiate(arrow, this.transform.position, Quaternion.identity);
        Vector3 direction = target.transform.position - this.transform.position;
        //direction.Normalize();
        // F = m * a;
        a.transform.LookAt(target.transform);
        a.rigidbody.AddForce(direction * power);
    }
}
