function RPGLSFindPictures()
{
    %search = "textures/gui/loadingPictures/*.PNG";
    $lsPicCount = -1;
    for (%f = findFirstFile(%search); %f !$= ""; %f = findNextFile(%search))
        $lsPic[$lsPicCount++] = %f;
}
function RPGLoadingScreen::onWake(%this)
{
    %lpic = nameToID("RPGLoadingPic");

    %res = getResolution();
    
    %x = getWord(%res,0);
    %y = getWord(%res,1);
    %lpic.setExtent(%x,%y);
    
    %randpic = getRandom(0, $lspicCount);
    %pic = $lsPic[%randpic];
    %lpic.setBitmap("gui/loadingPictures/" @ fileBase(%pic) @ fileExt(%pic));

    RPGLoadingCInfo.setValue("<just:center><font:Verdana Bold:20>Press ESCAPE to cancel.");
    createESCKeybind();
    CloseMessagePopup();
}
function RPGLoadingScreen::onSleep(%this)
{
    alxStop($HudHandle[shellScreen]);
}
function clientCmdRPGLoadscreen(%use)
{
    if (%use)
        canvas.setContent(RPGLoadingscreen);
}
function clientCmdRPGLoadscreenTitle(%title)
{
    RPGLoadingTitle.setValue("<just:center><font:Verdana Bold:30>Ironsphere RPG II\n" @ %title);
}
function createESCKeybind()
{
    if (isObject(RPGLSMap))
        return;
    echo("keybinded esc");
    new ActionMap(RPGLSMap);
    RPGLSMap.bindCmd(keyboard, "escape", "", "disconnect();");
    RPGLSMap.push();
}
loadgui("RPGLoadingScreen");
schedule(750,0,loadgui, "RPGloadingScreen");
schedule(1000,0,RPGLSFindPictures);
