using System;
using System.IO;
using UnityEditor; // EditorUtility
using UnityEngine;
using System.Collections;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

[RequireComponent(typeof(AudioSource))]
public class Mic : MonoBehaviour
{
	public string microphoneName = "Built-in Microphone"; // should probably use null for default microphone
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

		// TODO: remove "true ||" before official build. temporary for play testing within Unity
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

		/*
		// create speech recognizer
		SpeechRecognizer sr = new SpeechRecognizer();

		// create grammar with choices
		Choices colors = new Choices();
		colors.Add(new string[] {"red", "green", "blue"});
		GrammarBuilder gb = new GrammarBuilder();
		gb.Append(colors);
		// Create the Grammar instance.
		Grammar g = new Grammar(gb);

		// Load grammar into recognizer
		sr.LoadGrammar (g);

		// give it events
		sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
		//*/

		yield return null;
	}

	/*// event handler
	void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
	{
		Debug.Log ("said something, that it recognized");
		//MessageBox.Show(e.Result.Text);
		if (e.Result.Text == "red")
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = Vector3.zero;
		} else if (e.Result.Text == "green") {
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = Vector3.zero;
		}
		// make something appear in unity
	}//*/

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
			// micName, loopRecording, numSecs, freqHz);
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

	// http://www.kaappine.fi/tutorials/using-microphone-input-in-unity3d/
	float GetAveragedVolume()
	{
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}
}
