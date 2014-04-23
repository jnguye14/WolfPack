using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // lock z position to zero
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        // rotate the arrow based on its velocity
        float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360.0f;
        }
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y, angle);

        // Destroy the arrow if it falls off screen
        Vector3 pt = Camera.main.WorldToScreenPoint(this.transform.position);
        if (pt.y < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
