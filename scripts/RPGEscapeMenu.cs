function RPGEscapeMenu::onWake(%this)
{
    if (!isObject(EscMM))
    {
        new ActionMap(EscMM);
        EscMM.bindCmd(keyboard, "escape", "", "RPGReturnToGame();");
    }
    MoveMap.pop();
    EscMM.push();
}
function RPGEscapeMenu::onSleep(%this)
{
    EscMM.pop();
    MoveMap.push();
}
function escapeFromGame()
{
    canvas.pushDialog(RPGEscapeMenu);
}
function RPGReturnToGame()
{
    canvas.popDialog(RPGEscapeMenu);
}
loadgui("RPGEscapeMenu");
