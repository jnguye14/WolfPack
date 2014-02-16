using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Reference: http://msdn.microsoft.com/en-us/library/bbx2eya8(v=vs.110).aspx

public class script: MonoBehaviour
{
	private const int listenPort = 29129;
	Socket client;
	IPEndPoint remoteEP;
	Color currentColor = Color.clear;

	void Start()
	{
		try
		{
			Debug.Log ("Connecting");

			IPAddress ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];
			remoteEP = new IPEndPoint(ipAddress, listenPort);

			// Create a TCP/IP  socket.
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			client.Connect(remoteEP);

			Debug.Log ("Connected");
		}
		catch(Exception e)
		{
			Debug.Log("Error: " + e.StackTrace);
		}
	}

	void Update ()
	{
		this.gameObject.renderer.material.color = currentColor;
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked"); // Just so we could make sure Unity is actually running
		}

		//if (Input.GetButtonDown("Jump")) // spacebar
		{
			Debug.Log ("Transmitting...");
			if(client.Connected)
			{
				// string converted to byte[] to send
				byte[] ba = Encoding.ASCII.GetBytes("GetInput\n");

				// send the byte[]
				client.BeginSend(ba, 0, ba.Length,
				                 SocketFlags.None,
				                 new AsyncCallback(myWriteCallBack),
				                 client);
			}
			else
			{
				Debug.Log ("Not Connected; Unable to Transmit.");
			}


			Debug.Log ("Receiving Data...");
			if(client.Connected)
			{
				// stateobject holds information for callback
				StateObject state = new StateObject();
				state.workSocket = client;

				// receive the information (see callback)
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize,
				                    0,
				                    new AsyncCallback(myReadCallBack),
				                    state);
			}
			else
			{
				Debug.Log ("Not connected; Unable to Receive Data.");
			}
		}
	}
	
	void OnDestroy()
	{
		if (client.Connected)
		{
			client.Shutdown(SocketShutdown.Both);
			client.Close ();
		}
	}

	// call back methods
	public void myWriteCallBack(IAsyncResult ar)
	{
		Socket s = (Socket) ar.AsyncState;
		int send = s.EndSend(ar);
	}
	
	public void myReadCallBack(IAsyncResult ar)
	{
		// Retrieve the state object and the client socket from the asynchronous state object.
		StateObject state = (StateObject) ar.AsyncState;
		Socket client = state.workSocket;

		// Read data from the remote device.
		int bytesRead = client.EndReceive(ar);
		if (bytesRead > 0)
		{
			// interpret result and act accordingly
			string response = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
			if(response.Substring(0,response.Length-2) == "green")
			{
				currentColor = Color.green;
			}
			else if(response.Substring(0,response.Length-2) == "red")
			{
				currentColor = Color.red;
			}
			else if(response.Substring(0,response.Length-2) == "black")
			{
				currentColor = Color.black;
			}
			else if(response.Substring(0,response.Length-2) == "blue")
			{
				currentColor = Color.blue;
			}
			else if(response.Substring(0,response.Length-2) == "gray")
			{
				currentColor = Color.gray;
			}
			else if(response.Substring(0,response.Length-2) == "cyan")
			{
				currentColor = Color.cyan;
			}
			else if(response.Substring(0,response.Length-2) == "magenta")
			{
				currentColor = Color.magenta;
			}
			else if(response.Substring(0,response.Length-2) == "white")
			{
				currentColor = Color.white;
			}
			else if(response.Substring(0,response.Length-2) == "yellow")
			{
				currentColor = Color.yellow;
			}
			else
			{
				currentColor = Color.clear;
			}
		}
	}
	
	// state object to store data being passed around
	public class StateObject
	{
		// Client socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 256;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
	}
}