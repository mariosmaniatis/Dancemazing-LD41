var crosshairTexture : Texture2D;
var position : Rect;
static var OriginalOn = true;
 
function Start()

{
    Screen.lockCursor = true;
    position = Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - 
        crosshairTexture.height) /2, crosshairTexture.width, crosshairTexture.height);
}
 
function OnGUI()
{
    if(OriginalOn == true)
    {
        GUI.DrawTexture(position, crosshairTexture);
    }
}

function OnMouseDown () {
    // Lock the cursor
    Screen.lockCursor = true;
}