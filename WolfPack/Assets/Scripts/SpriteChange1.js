#pragma strict
@script RequireComponent(SpriteRenderer)

var boySprite : Texture2D;
var girlSprite : Texture2D;

function Start () {
    guiTexture.texture = PlayerPrefs.GetInt("Gender") == 0 ? boySprite : girlSprite;
}

function Update () {

}