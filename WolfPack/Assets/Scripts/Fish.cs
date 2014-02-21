using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
	public GameObject hook;
	private bool isHooked = false;

	// Use this for initialization
	void Start () {
		hook = GameObject.Find ("SphereHook");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isHooked)
		{
			this.transform.Translate(Time.fixedDeltaTime* (hook.transform.position-this.transform.position));
		}
	}

	void Hooked()
	{
		isHooked = true;
	}
}
