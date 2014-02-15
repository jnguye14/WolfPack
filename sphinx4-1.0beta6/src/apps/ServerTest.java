import java.io.*;
import java.net.*;

public class ServerTest {
	private static int portNumber = 29129;
	
	public static void main(String[] args) // throws IOException
	{
		System.out.println("hello");;
		
		try (
			ServerSocket serverSocket = new ServerSocket(portNumber);
            Socket clientSocket = serverSocket.accept();
            PrintWriter out = new PrintWriter(clientSocket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
        )
		{
    		System.out.println("start try statement");
            String inputLine;
            while ((inputLine = in.readLine()) != null) // wait for user input
			{
        		System.out.println("in while loop: "+inputLine);
            	// identify input
            	if(inputLine.equalsIgnoreCase("GetInput"))
            	{
            		System.out.println("Sending message back");;
            		String oString = "Hello World";
            		out.println(oString);
            		out.flush();
            	}
            	else
            	{
                    System.out.println("Unknown Input: " + inputLine);
            	}
            	
        		// send client data about current user input
            }
    		System.out.println("End of program");
        }
		catch (IOException e)
		{
            System.out.println("Exception caught when trying to listen on port " + portNumber + " or listening for a connection");
            System.out.println(e.getMessage());
        }//*/
	}
}