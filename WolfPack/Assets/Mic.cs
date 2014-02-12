using System;
using System.IO;
using UnityEditor; // EditorUtility
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Mic : MonoBehaviour
{
	public string microphoneName = "Built-in Microphone";
	public bool hasPermission = false;
	public KeyCode startKey = KeyCode.Space;
	public KeyCode stopKey = KeyCode.Backspace;
	public KeyCode playKey = KeyCode.Return;
	public KeyCode saveKey = KeyCode.Tab;

	// Use this for initialization
	IEnumerator Start () {
	//void Start(){
//# if UNITY_WEBPLAYER
		// Request permission to use both webcam and microphone.
		yield return Application.RequestUserAuthorization (UserAuthorization.Microphone);

		if (true || Application.HasUserAuthorization(UserAuthorization.Microphone))
		{
			// we got permission. Set up microphone.
			hasPermission = true;

			foreach(string device in Microphone.devices)
			{
				Debug.Log("Name: " + device);
			}
			microphoneName = Microphone.devices[0]; /*"Built-in Microphone"*/
		}
		else
		{
			// no permission. Show error here.
			hasPermission = false;
			Debug.Log("Need permission to use microphone");
		}
//#endif
		yield return null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//*
		if(Microphone.IsRecording(microphoneName))
		{
			Debug.Log ("recording");
		}//*/

		if (Input.GetKeyDown (startKey))
		{
			StartRecording();
		}
		if (Input.GetKeyDown (stopKey))
		{
			StopRecording();
		}
		if (Input.GetKeyDown (playKey))
		{
			PlayRecording(false);	
		}
		if (Input.GetKeyDown (saveKey))
		{
			SaveRecording("myFile");	
		}
	}

	void StartRecording()
	{
		if (!hasPermission)
		{
			Debug.Log ("No permission to use microphone");
			return;
		}

		if (!Microphone.IsRecording (microphoneName))
		{
			audio.clip = Microphone.Start (microphoneName, false, 10, 44100);
			// micName, loopRecording, numSecs, freq);
		}
		else
		{
			Debug.Log ("already recording");
		}
	}

	void StopRecording()
	{
		if (!hasPermission)
		{
			Debug.Log ("No permission to use microphone");
			return;
		}

		if (Microphone.IsRecording (microphoneName))
		{
			Microphone.End (microphoneName);
		}
		else
		{
			Debug.Log ("not recording");
		}
	}

	void PlayRecording(bool looping)
	{
		if (!hasPermission)
		{
			Debug.Log ("No permission to use microphone");
		}

		if (!Microphone.IsRecording (microphoneName) && !audio.isPlaying)
		{
			audio.loop = looping;
			audio.Play ();
		}
		else
		{
			Debug.Log ("Unable to play recording");
		}
	}

	void SaveRecording(string filename)
	{
		if (!hasPermission)
		{
			Debug.Log ("No permission to use microphone");
		}

		if (!Microphone.IsRecording (microphoneName))
		{
			//filename += System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") +".ogg";
			//var filepath = Path.Combine(Application.dataPath, filename);
			//EditorUtility.ExtractOggFile(audio.clip, filepath);
			SavWav.Save (filename, audio.clip);
		}
		else
		{
			Debug.Log ("unable to save, still recording");
		}
	}
}
