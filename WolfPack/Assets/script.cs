using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class script: MonoBehaviour
{
	private const int listenPort = 29129;
	TcpClient tcpclnt;
	NetworkStream stm;
	//IPEndPoint groupEP;

	void Start()
	{
		try
		{
			Debug.Log ("Connecting");
			tcpclnt = new TcpClient ();
			tcpclnt.Connect(System.Net.Dns.GetHostName(),listenPort);
			stm = tcpclnt.GetStream();
		}
		catch(Exception e)
		{
			Debug.Log("Error: " + e.StackTrace);
		}
		Debug.Log ("Connected");
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked");		
		}

		if (tcpclnt.Connected && Input.GetButtonDown("Jump")) // spacebar
		{
			Debug.Log ("Pressed Space; Encoding Data");
			ASCIIEncoding asen = new ASCIIEncoding();
			byte[] ba = asen.GetBytes("GetInput");
			//byte[] data = new byte[str.Length * sizeof(char)];
			//System.Buffer.BlockCopy(str.ToCharArray(), 0, data, 0, data.Length);

			Debug.Log("Transmitting.....");
			stm.BeginWrite(ba, 0, ba.Length, new AsyncCallback(myWriteCallBack), stm);
			//stm.Write(ba, 0, ba.Length);

			//*
			byte[] bb = new byte[100];
			stm.BeginRead(bb, 0, bb.Length, new AsyncCallback(myReadCallBack), stm);
			//int k = stm.Read(bb, 0, 100);
			string op = asen.GetString(bb);
			//string[] words = op.Split(' ');
			Debug.Log (op);
			//*/


			//stm.Close();
			//tcpclnt.Close();
		}
		else if(!tcpclnt.Connected)
		{
			Debug.Log("need to connect");
			Destroy(this.gameObject);
		}


	}

	public static void myWriteCallBack(IAsyncResult ar)
	{
		NetworkStream myNetworkStream = (NetworkStream)ar.AsyncState;
		myNetworkStream.EndWrite(ar);
	}

	public static void myReadCallBack(IAsyncResult ar)
	{
		NetworkStream myNetworkStream = (NetworkStream)ar.AsyncState;
		myNetworkStream.EndRead(ar);
	}

	void OnDestroy()
	{
		if(tcpclnt.Connected)
		{
			tcpclnt.Close();
		}
	}
}