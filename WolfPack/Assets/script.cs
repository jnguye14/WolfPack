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
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked"); // Just so we could make sure Unity is actually running
		}

		if (Input.GetButtonDown("Jump")) // spacebar
		{
			Debug.Log ("Pressed Space; Encoding Data");

			//string str = "GetInput";
			//byte[] data = new byte[str.Length * sizeof(char)];
			//System.Buffer.BlockCopy(str.ToCharArray(), 0, data, 0, data.Length);

			Debug.Log ("Transmitting...");
			if(client.Connected)
			{
				//ASCIIEncoding asen = new ASCIIEncoding();
				//byte[] ba = asen.GetBytes("GetInput");
				byte[] ba = Encoding.ASCII.GetBytes("GetInput\n");

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
			byte[] buffer = new byte[1024];
			if(client.Connected)
			{
				StateObject state = new StateObject();
				state.workSocket = client;

				client.BeginReceive(state.buffer, 0, StateObject.BufferSize,
				                    0,
				                    new AsyncCallback(myReadCallBack),
				                    state);

				/*
				client.BeginReceive(buffer, 0, buffer.Length,
				                    SocketFlags.None,
				                    new AsyncCallback(myReadCallBack),
				                    client);
				//*/
				string returnData = Encoding.ASCII.GetString(buffer);
				//string returnData = asen.GetString (buffer);
				Debug.Log ("Received: " + returnData);
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
	public static void myWriteCallBack(IAsyncResult ar)
	{
		Socket s = (Socket) ar.AsyncState;
		int send = s.EndSend(ar);
	}
	
	public static void myReadCallBack(IAsyncResult ar)
	{
		//Socket s = (Socket) ar.AsyncState;
		//int receive = s.EndReceive(ar);

		try
		{
			// Retrieve the state object and the client socket from the asynchronous state object.
			StateObject state = (StateObject) ar.AsyncState;
			Socket client = state.workSocket;

			// Read data from the remote device.
			int bytesRead = client.EndReceive(ar);
			if (bytesRead > 0)
			{
				// There might be more data, so store the data received so far.
				state.sb.Append(Encoding.ASCII.GetString(state.buffer,0,bytesRead));
				Debug.Log (state.sb.ToString());

				/*//  Get the rest of the data.
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize,
				                    0,
				                    new AsyncCallback(myReadCallBack),
				                    state);//*/
			}
			else
			{
				string response = "";
				// All the data has arrived; put it in response.
				if (state.sb.Length > 1)
				{
					response = state.sb.ToString();
				}

				// Signal that all bytes have been received.
				//receiveDone.Set();
			}
		}
		catch (Exception e)
		{
			Debug.Log ("5");
			Console.WriteLine(e.ToString());
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
		// Received data string.
		public StringBuilder sb = new StringBuilder();
	}
}