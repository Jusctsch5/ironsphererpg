function RPGZoneEntry::newZone(%this, %zoneName, %rzs, %goodZone, %entered)
{
    %cTag = (%goodZone) ? "66FF00" : "990000";
    if (%goodZone)
       %t = "<just:center><font:Verdana Bold:30><color:66FF00>" @ %zoneName;
    else
       %t = "<just:center><font:Verdana Bold:30><color:990000>" @ %zoneName;
    RPGZoneEntryText.setValue(collapseEscape(%t));
    RPGZoneEntryTitle.setValue("<just:center><font:Verdana Bold:20><color:666666>You have ");
    RPGZoneEntryTitle.addText(%entered ? "entered" : "left", false);

    RPGZoneEntryInfo.setValue("<just:center><font:Verdana Bold:18><color:666666>" @ %rzs);
    %this.showText();
}

function RPGZoneEntry::showText(%this)
{
    canvas.pushDialog(%this);

    if (isEventPending(%this.dSch))
        cancel(%this.dSch);

    %this.dSch = canvas.schedule(5000, "popDialog", %this);
}

function clientCmdRPGEnterZone(%zoneName, %rzs, %goodZone, %entered)
{
    RPGZoneEntry.newZone(%zoneName, %rzs, %goodZone, %entered);
}
