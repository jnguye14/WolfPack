using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pt = Camera.main.WorldToScreenPoint(this.transform.position);
        if (pt.y < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
