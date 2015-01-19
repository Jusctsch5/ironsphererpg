if(!isobject(rpgstatscreen))
exec("gui/rpgstatscreen.gui");	
function RPGstatScreen::loadHud( %this, %tag )
{
	//echo("RPGstatScreen::loadHud(" @ %this @ ", " @ %tag @ ")");
	//exec("gui/rpgstatscreen.gui");	
	//$sExeced = true;
	//$Hud[%tag] = RPGstatScreen;
	//$Hud[%tag].childGui = RPGSTAT_ROOT;
	//$Hud[%tag].parent = RPGSTAT_ROOT;

}
function togglestatHud( %val )
{
   //if ( %val )
   //   toggleCursorHuds('RPGstatScreen');
   if (%val)
   Canvas.PushDialog(RpgStatScreen);
}

function RPGstatScreen::onDone( %this )
{
	//toggleCursorHuds( 'RPGstatScreen' );
	Canvas.popDialog(RPGStatScreen);
}
//------------------------------------------------------------------------------
function RPGstatScreen::setupHud( %this, %tag )
{
	echo("RPGstatScreen::setupHud(" @ %this @ ", " @ %tag @ ")");
}
function RPGstatScreen::onWake(%this)
{
	CommandToServer('OnOpenStat');
}
//---------------------------------------------------------
function clientCmdOpenStatGUI()
{
	togglestatHud(true);
}
function clientCmdStatUpdateHud()
{//should work now


	
	//start
	//SkillAmount.clear();
	SkillName.clear();



}
function clientCmdStatAddRow(%skillname, %amount, %description, %multi, %i, %tag)
{
	$statscreen::Skill[%i,0] = %skillname;
	$statscreen::Skill[%i,1] = %amount;
	$statscreen::Skill[%i,2] = %description;
	$statscreen::Skill[%i,3] = %multi;
	$statScreen::Skill[%i,4] = %tag;

	//SkillAmount.addRow(0,%amount);
	SkillName.addRow(%i,%amount SPC %skillname);
	
	// Hacky but it works.
	%rN = SkillName.getRowNumById($statscreen::selectedSkill);
	if(%rN != -1)
		SkillName.setSelectedRow(%rN);
}
function RPGstatScreen::onIncrease(%this)
{
	
	%amount = RSSAmount.getValue();
	%id = SkillName.getSelectedId();
  
  $statscreen::selectedSkill = %id;
	commandToServer('StatOnIncrease',%id,%amount);//increase skill by amount
}
function clientCmdRemoveTag(%tag)
{
	$statscreen::Skill[%tag, 4] = "";
}
function clientCmdAddTag(%tag)
{
	$statscreen::skill[%tag, 4] = "Tagged";
}
function RPGstatScreen::onTag(%this)
{
	%id = SkillName.getSelectedId();
	CommandToServer('TagOnSelect', %id);
}
function RPGstatScreen::onAmount(%this)
{
	if(RSSAmount.getValue() > SP.value)
	 RSSAmount.setValue(SP.value);
	 
	 //little client handleing will be checked by the server as well.
}

function clientCmdStatSetSP(%amount)
{
	sp.value = %amount;
	sp.setvalue("SP:" SPC %amount);
}
function SkillName::onSelect(%this, %itemId, %text)
{
	%d = "";
	for(%i = 1; getword(%text, %i) !$= "";%i++)
	%d = %d SPC getword(%text, %i);
	%text = getsubStr(%d,1,strlen(%d)-1);

	$client::skill[0] = $statscreen::skill[%itemId,0];
	$client::skill[1] = $statscreen::skill[%itemId,1];
	$client::skill[2] = $statscreen::skill[%itemId,2];
	$client::skill[3] = $statscreen::skill[%itemId,3];
	$client::skill[4] = $statscreen::skill[%itemid,4];
	SkillDescription.setValue($client::skill[2] @ "\n" SPC "\c3Multiplier:\c7" SPC $client::skill[3] NL "\c2" @ $client::skill[4]);
	//Client-side
	//commandToServer('ShopInvListOnSelect', %itemId, %text);
}
function clientCmdStatDone()
{
//finish

}


$guiVer["RPGstatScreen"] = 1.0;
