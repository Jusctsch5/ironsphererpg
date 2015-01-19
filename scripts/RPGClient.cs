//exec("scripts/rpgitems.cs");
exec("scripts/version.cs");

function clientCmdMissionStartPhase3(%seq, %missionName)
{
   $MSeq = %seq;
   
   //Reset Inventory Hud... 
   if($Hud['RPGinventoryScreen'] !$= "")
   {
      %favList = $Hud['RPGinventoryScreen'].data[0, 1].type TAB $Hud['RPGinventoryScreen'].data[0, 1].getValue();
      for ( %i = 1; %i < $Hud['RPGinventoryScreen'].count; %i++ )
         if($Hud['RPGinventoryScreen'].data[%i, 1].getValue() $= invalid)
            %favList = %favList TAB $Hud['RPGinventoryScreen'].data[%i, 1].type TAB "EMPTY";  
         else
            %favList = %favList TAB $Hud['RPGinventoryScreen'].data[%i, 1].type TAB $Hud['RPGinventoryScreen'].data[%i, 1].getValue();  
      commandToServer( 'setClientFav', %favList );   
   }   
   else
      commandToServer( 'setClientFav', $pref::Favorite[$pref::FavCurrentSelect]);   
      
   // needed?
   $MissionName = %missionName;
   //commandToServer( 'getScores' );

   // only show dialog if actually lights
   if(lightScene("sceneLightingComplete", $LaunchMode $= "SceneLight" ? "forceWritable" : ""))
   {
      error("beginning SceneLighting....");
      schedule(1, 0, "updateLightingProgress");
      $lightingMission = true;
      LoadingProgress.setValue( 0 );
      DB_LoadingProgress.setValue( 0 );
      LoadingProgressTxt.setValue( "LIGHTING MISSION" );
      DB_LoadingProgressTxt.setValue( "LIGHTING MISSION" );
      $missionLightStarted = true;
      Canvas.repaint();
   }
   deletevariables("$inv::*");
   deletevariables("$menu::*");
}
function logecho(%something)
{
//annoying
}
function rpgtoggle()
{
	if($rpgbottomPrintActive)
		clientCmdCloseRPGbottomPrint();
}
//--------------------------------------------------------- gui auto downloader!

function clientCmdStartRecastDelayCountdown(%value)
{
	if(isobject(rpgrecastdelay))
	{
		rpgrecastdelay.text = %value / 1000;
		rpgrecastdelay.setvalue("Delay:" SPC %value / 1000);
		rpgrecastdelay.schedule(10, "tickdown", mfloor(%value/10)-1);
	}
}
function RPGrecastDelay::tickdown(%this, %value)
{
	if(%value <= 0)
	{
		rpgrecastdelay.text = 0;
		%this.setValue("Delay: 0.00");
		return;
	}
	%this.schedule(10, "tickdown", %value-1);
	%txt = %value / 100;
	if((%value % 100) == 0)
		%extra = ".0";
	else
		%extra = "";
	if((%value % 10) == 0)
		%extra = %extra @ "0";
	%this.setValue("Delay:" SPC %txt @ %extra);
	
}



function clientCmdExecGUI(%file)
{
	exec("gui/" @ %file @ ".cs");//whoo hoo!

}

//------------------------------------------------------------------------------

function clientCmdRPGPlayMusic(%type)
{
	//play us some music!
	//count off how many and take a random number
	for(%i = 0; $music::file[%type, %i] !$= ""; %i++)
	{}
	//%i is how many.
	//echo(%type);
	
	if(%i != 0)//Tribes 2 will CTD if %i = 0; Check this
	%i = 100*getRandom() % %i;//take modulus. this way it gives us a number from 0 to %i - 1. (if %i is 3, then it will give us a number from 0 to 2). 
	else
	%i = 0;
	%music = $music::file[%type, %i];
	clientCMDPlayMusic(%music);//play the song!
}
function placeBeacon( %val )
{
	if(%val)
	{
		OpenGuildManagementGUI();
	
	}
}


function setupObjHud(%gameType)
{
	return;
   switch$ (%gameType)
	{
		case BountyGame:
			// set separators
			objectiveHud.setSeparators("56 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "90 16";
				visible = "1";
			};
			// Target label ("TARGET")
			objectiveHud.targetLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "TARGET";
			};
			// your target's name
			objectiveHud.yourTarget = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "90 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.targetLabel);
			objectiveHud.add(objectiveHud.yourTarget);

		case CnHGame:
			// set separators
			objectiveHud.setSeparators("96 162 202");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "105 3";
				extent = "50 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "105 19";
				extent = "50 16";
				visible = "1";
			};
			// Hold label ("HOLD")
			objectiveHud.holdLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "165 3";
				extent = "35 16";
				visible = "1";
				text = "HOLD";
			};
			objectiveHud.holdLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "165 19";
				extent = "35 16";
				visible = "1";
				text = "HOLD";
			};
			// number of points held
			objectiveHud.numHeld[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "205 3";
				extent = "30 16";
				visible = "1";
			};
			objectiveHud.numHeld[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "205 19";
				extent = "30 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.holdLabel[%i]);
				objectiveHud.add(objectiveHud.numHeld[%i]);
			}

		case CTFGame:
			// set separators
			objectiveHud.setSeparators("72 97 130");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "65 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "65 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "75 3";
				extent = "20 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "75 19";
				extent = "20 16";
				visible = "1";
			};
			// Flag label ("FLAG")
			objectiveHud.flagLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "30 16";
				visible = "1";
				text = "FLAG";
			};
			objectiveHud.flagLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "30 16";
				visible = "1";
				text = "FLAG";
			};
			// flag location (at base/in field/player carrying it)
			objectiveHud.flagLocation[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "135 3";
				extent = "105 16";
				visible = "1";
			};
			objectiveHud.flagLocation[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "135 19";
				extent = "105 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.flagLabel[%i]);
				objectiveHud.add(objectiveHud.flagLocation[%i]);
			}

		case DMGame:
			// set separators
			objectiveHud.setSeparators("56 96 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "30 16";
				visible = "1";
			};
			// Your kills label ("KILLS")
			objectiveHud.killsLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "KILLS";
			};
			// Your kills
			objectiveHud.yourKills = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "30 16";
				visible = "1";
			};
			// Your deaths label ("DEATHS")
			objectiveHud.deathsLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "50 16";
				visible = "1";
				text = "DEATHS";
			};
			// Your deaths
			objectiveHud.yourDeaths = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "160 19";
				extent = "30 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.killsLabel);
			objectiveHud.add(objectiveHud.yourKills);
			objectiveHud.add(objectiveHud.deathsLabel);
			objectiveHud.add(objectiveHud.yourDeaths);

		case DnDGame:

      case HuntersGame:
			// set separators
			objectiveHud.setSeparators("96 132");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "30 16";
				visible = "1";
			};
			// flags label ("FLAGS")
			objectiveHud.flagLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
				text = "FLAGS";
			};
			// number of flags
			objectiveHud.yourFlags = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "30 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.flagLabel);
			objectiveHud.add(objectiveHud.yourFlags);

		case RabbitGame:
			// set separators
			objectiveHud.setSeparators("56 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "90 16";
				visible = "1";
			};
			// Rabbit label ("RABBIT")
			objectiveHud.rabbitLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "RABBIT";
			};
			// rabbit name
			objectiveHud.rabbitName = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "90 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.rabbitLabel);
			objectiveHud.add(objectiveHud.rabbitName);

		case SiegeGame:
			// set separators
			objectiveHud.setSeparators("96 122 177");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "20 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "20 16";
				visible = "1";
			};
			// Role label ("PROTECT" or "DESTROY")
			objectiveHud.roleLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "125 3";
				extent = "50 16";
				visible = "1";
			};
			objectiveHud.roleLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "125 19";
				extent = "50 16";
				visible = "1";
			};
			// number of objectives to protect/destroy
			objectiveHud.objectives[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "180 3";
				extent = "60 16";
				visible = "1";
			};
			objectiveHud.objectives[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "180 19";
				extent = "60 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.roleLabel[%i]);
				objectiveHud.add(objectiveHud.objectives[%i]);
			}

		case TeamHuntersGame:
			// set separators
			objectiveHud.setSeparators("57 83 197");
			objectiveHud.enableHorzSeparator();

			// flags label ("FLAGS")
			objectiveHud.flagLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "FLAGS";
			};
			// number of flags
			objectiveHud.yourFlags = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "20 16";
				visible = "1";
			};
			// team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "85 3";
				extent = "110 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "85 19";
				extent = "110 16";
				visible = "1";
			};
			// team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "200 3";
				extent = "40 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "200 19";
				extent = "40 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.flagLabel);
			objectiveHud.add(objectiveHud.yourFlags);
			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
			}

		case SinglePlayerGame:
			// no separator lines
			objectiveHud.setSeparators("");
			objectiveHud.disableHorzSeparator();

			// two lines to print objectives
			objectiveHud.spText[1] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.spText[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.add(objectiveHud.spText[1]);
			objectiveHud.add(objectiveHud.spText[2]);

		case RPGGame:
			// no separator lines
			objectiveHud.setSeparators("");
			objectiveHud.disableHorzSeparator();

			// two lines to print objectives
			objectiveHud.spText[1] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.spText[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.add(objectiveHud.spText[1]);
			objectiveHud.add(objectiveHud.spText[2]);
	}

	chatPageDown.setVisible(false);
}

function clientCmdSetReticle(%ret, %vis)
{
   reticleHud.setBitmap(%ret);
   ReticleFrameHud.setVisible(%vis);
}
function clientCmdgetClientRPGversion()
{
	//echo("CALLED");
	commandToServer('setClientVersion', $rpgver);
}

function isSet(%v) {
   return (%v !$= "");
}
