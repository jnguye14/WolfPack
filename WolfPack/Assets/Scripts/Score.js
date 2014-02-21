#pragma strict
@script RequireComponent(GUIText)

function Start () {
	PlayerPrefs.SetInt("Score",0);
}

function Update () {
	guiText.text = "Score: " + PlayerPrefs.GetInt("Score");
}