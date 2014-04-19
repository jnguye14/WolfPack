#pragma strict
// Note: this script requires a guiText Component

public var numSeconds : int = -1;
public var isStopped : boolean = true;

private var startTime : int;
private var delta : float = 0.0f;

function Start ()
{
	delta = 0.0f;
	if(numSeconds >= 0)
	{
		startTime = numSeconds;
	}
	else
	{
		startTime = 10; // 10 seconds default
	}
	guiText.text = "Time: " + numSeconds.ToString();
}

function Update ()
{
	if(!isStopped)
	{
		delta += Time.deltaTime;
		if(delta > 1.0f) // one second
		{
			delta -= 1.0f;
			numSeconds--;
			guiText.text = "Time: " + numSeconds.ToString();
		}
		
		if(numSeconds <= 0)
		{
			guiText.text = "Time's up";
			if(!isStopped)
			{
                this.SendMessage("TimeUpEvent", SendMessageOptions.DontRequireReceiver);
			}
			isStopped = true;
		}
	}
}

function AddTime(amount : int)
{
	numSeconds += amount;
}

function SubtractTime( amount : int)
{
	numSeconds -= amount;
}

function RestartTimer()
{
	numSeconds = startTime;
}

function SetTimer(amount : int)
{
	startTime = amount;
}

function StartTimer(amount : int)
{
	startTime = amount;
	numSeconds = startTime;
}

function PauseTimer()
{
	isStopped = true;
}

function ResumeTimer()
{
	isStopped = false;
}