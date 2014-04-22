using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Reference: http://msdn.microsoft.com/en-us/library/bbx2eya8(v=vs.110).aspx

public class script: MonoBehaviour
{
	public GameObject sendMessegeTo;

	private const int listenPort = 29129;
	Socket client;
	IPEndPoint remoteEP;
	private string response = "";
    private string prevResponse = "";
    public bool isGettingData = true;

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
        if (sendMessegeTo != null)
        {
            sendMessegeTo.SendMessage("ParseData", response);
            prevResponse = response;
            //int messagesent = 1;
        }

        if (isGettingData)
        {
            transmitData("GetInput\n");
            receiveData();
        }

        if (prevResponse == response)
        {
            transmitData("Reset\n");
        }
	}

    void transmitData(string t)
    {
        //Debug.Log ("Transmitting...");
        if (client.Connected)
        {
            // string converted to byte[] to send
            byte[] ba = Encoding.ASCII.GetBytes(t);

            // send the byte[]
            client.BeginSend(ba, 0, ba.Length,
                                SocketFlags.None,
                                new AsyncCallback(myWriteCallBack),
                                client);
        }
        else
        {
            Debug.Log("Not Connected; Unable to Transmit.");
        }
    }

    void receiveData()
    {
		//Debug.Log ("Receiving Data...");
		if(client.Connected && isGettingData)
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
	
    //void OnDestroy()
    //{
    //    if (client.Connected)
    //    {
    //        client.Shutdown(SocketShutdown.Both);
    //        client.Close ();
    //    }
    //}

	// call back methods
	public void myWriteCallBack(IAsyncResult ar)
	{
        // when transmitting data, all you do is write to the socket
        Socket s = (Socket)ar.AsyncState;
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
			this.response = response.Substring(0,response.Length-2);
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