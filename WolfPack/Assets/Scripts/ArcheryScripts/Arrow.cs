using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        this.transform.rotation = Quaternion.identity;
        Vector3 pt = Camera.main.WorldToScreenPoint(this.transform.position);
        if (pt.y < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
