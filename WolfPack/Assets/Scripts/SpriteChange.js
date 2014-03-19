#pragma strict

var boySprite : Sprite;
var girlSprite : Sprite;

function Start () {
    gameObject.GetComponent(SpriteRenderer).sprite = PlayerPrefs.GetInt("Gender") == 0 ? boySprite : girlSprite;
}

function Update () {

}