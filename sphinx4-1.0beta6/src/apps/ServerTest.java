import java.io.*;
import java.net.*;

//import edu.cmu.sphinx.demo.helloworld.HelloWorld;
import edu.cmu.sphinx.frontend.util.Microphone;
import edu.cmu.sphinx.recognizer.Recognizer;
//import edu.cmu.sphinx.result.Result;
import edu.cmu.sphinx.util.props.ConfigurationManager;

public class ServerTest {
	private static int portNumber = 29129;
	
	public static void main(String[] args) // throws IOException
	{
		// create the speech recognizer
		URL resource = ServerTest.class.getResource("servertest.config.xml");
		ConfigurationManager cm = new ConfigurationManager(resource);		
        Recognizer recognizer = (Recognizer) cm.lookup("recognizer");
        recognizer.allocate();

        // start the microphone or exit if the program if this is not possible
        Microphone microphone = (Microphone) cm.lookup("microphone");
        if (!microphone.startRecording())
        {
            System.out.println("Cannot start microphone.");
            recognizer.deallocate();
            System.exit(1);
        }
		
        
        SpeechRecognizerThread t = new SpeechRecognizerThread(recognizer);
        t.start();
        
		System.out.println("hello");
		
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
            		//t.getResultText();
            		String oString = t.getResultText();//"Hello World";
            		System.out.println("Sending message back: " + oString);
            		out.println(oString);
            		out.flush();
            	}
            	else if(inputLine.equalsIgnoreCase("Reset"))
            	{
            		t.resetText();
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