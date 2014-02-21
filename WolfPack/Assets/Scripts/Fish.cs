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
			if((hook.transform.position - this.transform.position).sqrMagnitude < 3)
			{
				PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+1);
				DestroyObject(this.gameObject);
			}
		}
	}

	void Hooked()
	{
		isHooked = true;
	}
}
