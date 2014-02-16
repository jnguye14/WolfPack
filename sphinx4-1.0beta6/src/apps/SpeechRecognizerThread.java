import edu.cmu.sphinx.recognizer.Recognizer;
import edu.cmu.sphinx.result.Result;

public class SpeechRecognizerThread extends Thread
{
	Recognizer recognizer;
	String resultText = "";
	
	SpeechRecognizerThread(Recognizer rec)
    {
    	recognizer = rec;
    }

	public String getResultText()
	{
		return resultText;
	}
	
    public void run()
    {
    	// loop the recognition until the program exits.
        while (true) {
            //System.out.println("Start speaking. Press Ctrl-C to quit.\n");

            Result result = recognizer.recognize();

            if (result != null)
            {
                resultText = result.getBestFinalResultNoFiller();
                System.out.println("You said: " + resultText + '\n');
            }
            else
            {
            	resultText = "";
                System.out.println("I can't hear what you said.\n");
            }
        }
    }
}