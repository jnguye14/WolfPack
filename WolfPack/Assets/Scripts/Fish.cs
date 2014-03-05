using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
	public GameObject hook;
	private bool isHooked = false;

	// Use this for initialization
	void Start () {
		hook = GameObject.Find ("Net");
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

	void OnGUI()
	{
		GUI.depth = -1;

		Vector3 point = Camera.main.WorldToScreenPoint (this.transform.position);
		GUI.color = this.renderer.material.color;
		GUI.Label (new Rect(point.x,Screen.height-point.y,500,50), this.renderer.material.color.ToString());
	}

	void Hooked()
	{
		isHooked = true;
	}
}
