//exec("gui/ISGameMenu.gui");
if(!isobject(rpgbottomprint))
exec("gui/rpgbottomprint.gui");


//------------------------------------------------------------------------------


function clientCmdRPGBottomPrint(%time, %lines, %text0, %text1, %text2, %text3, %text4, %text5)
{
	%val = 15;
	%text = %text0 @ %text1 @ %text2 @ %text3 @ %text4 @ %text5;
	if($rpgbottomPrintActive)
	{
		if( isEventPending(RPGbottomPrint.removePrint))
		{
			cancel(RPGbottomPrint.removePrint);
		}

			RPGBPTXT.setText(%text);
			RPGBPBackGround.setposition(FirstWord(RPGBPBackGround.position), 540-%lines*%val);
	RPGBPBackGround.setextent( firstWord(RPGBPBackGround.extent) , (%lines*%val+20));
			RPGBPTXT.extent = firstWord(RPGBPBackGround.extent) SPC %lines*%val;

		if(%time > 0)
		  RPGbottomPrint.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdCloseRPGbottomPrint" );

		return;
	}
	//showHud('RPGBottomPrint');
	Canvas.pushDialog(RPGBottomPrint);
	$rpgbottomPrintActive = true;
	//toggleCursorHuds('RPGbottomPrint');
	RPGBPTXT.setText(%text);
	RPGBPBackGround.setposition(FirstWord(RPGBPBackGround.position), 540-%lines*%val);
	RPGBPBackGround.setextent( firstWord(RPGBPBackGround.extent) , (%lines*%val+20));
	RPGBPTXT.extent = firstWord(RPGBPBackGround.extent) SPC %lines*%val;
	if(%time > 0)
	  RPGbottomPrint.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdCloseRPGbottomPrint" );
}
function clientCmdCloseRPGbottomPrint()
{

	RPGbottomPrint.onDone();
}	
function clientCmdOpenRPGbottomPrint()
{

     // showHud('RPGBottomPrint');
     Canvas.pushDialog(RPGbottomPrint);

	//toggleCursorHuds('RPGbottomPrint');
}
//------------------------------------------------------------------------------
function RPGbottomPrint::onDone( %this )
{
  	$rpgbottomPrintActive = 0;
  	RPGbottomPrint.visible = 0;
   
   	RPGbottomPrint.removePrint = "";

     // hideHud('RPGBottomPrint');
      Canvas.popDialog(RPGBottomPrint);
      //clientCmdTogglePlayHuds(true);


	commandToServer('ClientCloseRPGbottomPrint');
}

$guiVer["RPGBottomPrint"] = 1.0;