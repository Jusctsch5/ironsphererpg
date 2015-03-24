// DisplayName = Tribes 2 RPG

//--- GAME RULES BEGIN ---
//Play T2RPG
//--- GAME RULES END ---

$IS::Web = "ironsphererpg.sourceforge.net";
function execrpg()
{
	exec("scripts/version.cs");
	exec("scripts/rpgglobals.cs");
	exec("scripts/rpgskills.cs");
	exec("scripts/rpgsound.cs");// load before items
    exec("scripts/rpgtelevalidation.cs");
	exec("scripts/rpgspells.cs");
	exec("scripts/rpgclasses.cs");
	exec("scripts/rpgitems.cs");	
	exec("scripts/rpgmining.cs");
	exec("scripts/rpgjail.cs");
	exec("scripts/rpgbonusstate.cs");
	exec("scripts/rpgchat.cs");
	exec("scripts/rpgaccessory.cs");
	exec("scripts/rpghp.cs");
	exec("scripts/rpgmana.cs");
	exec("scripts/rpgparty.cs");
	exec("scripts/rpgraces.cs");
	exec("scripts/rpgplayer.cs");
	exec("scripts/rpgstats.cs");
	exec("scripts/rpgzones.cs");
    //exec("scripts/RPGEnterZone.cs"); //enterzone    zones calls this.
	exec("scripts/rpgspawns.cs");
	exec("scripts/rpghouse.cs");
	exec("scripts/rpgfunk.cs");
	exec("scripts/rpgarray.cs");
	exec("scripts/rpgeconomy.cs");
	exec("scripts/LobbyGui.cs");
	exec("scripts/rpgboat.cs");
	exec("scripts/rpgshapes.cs");
	exec("scripts/weapons/shockLance.cs");
	exec("scripts/trident.cs"); //break activate
	exec("scripts/aiRPG.cs");
	exec("scripts/rpgguilds.cs");
	exec("scripts/rpgarena.cs");
	exec("scripts/rpgquest.cs");
	exec("scripts/rpgsmithing.cs");
    exec("scripts/RPGQuickBinder.cs"); //phantom's quickbind gui
    exec("scripts/ruby.cs");//fixes stupid arithmetic problems
    exec("scripts/helpers.cs");
    exec("scripts/teaparty.cs"); // general server setup in example admin passwords and other various thingies
    //exec("scripts/rpgshapes2.cs");  //new shapes for the mod      not right now.
	$execedagain++;
	DefineItems();
	DefineItemDatas();
}

execrpg();

//---------------------------------------------------------------------------------
$debugMode = false;
$Host::warmupTime = -1;
//---------------------------------------------------------------------------------

// stub functions to fix console warnings
function customLoadouts()
{

}
function customAISpawn()
{

}
function customZoneSpecifications()
{

}
function customTownBots()
{

}
function CustomMiningPoints()
{

}


function RPGGame::initGameVars(%game)
{
	%game.fadeTimeMS = 2000;
	%game.notifyMineDist = 7.5;
	InitSpawnPoints();
}

function RPGGame::timeLimitReached(%game)
{
}

function RPGGame::scoreLimitReached(%game)
{
}

function RPGGame::gameOver(%game)
{
}

////////////////////////////////////////////////////////////////////////////////////////

function RPGGame::checkScoreLimit(%game, %team)
{
}

function RPGGame::enterMissionArea(%game, %playerData, %player)
{
}

function RPGGame::leaveMissionArea(%game, %playerData, %player)
{
}

function RPGGame::startMatch(%game)
{
	if($debugMode == true) echo("RPGGame::startMatch(" @ %game @ ");");

	//%game.clearDeployableMaxes();
	$missionStartTime = getSimTime();
	$matchStarted = true;
	AISystemEnabled(true);
    %game.saveWorldSchedule = %game.schedule($saveWorldDelay, "saveWorld");
	%game.weatherCycleSchedule = %game.schedule($weatherWait, "weatherCycle");
    %game.currentWeather = 1;
	%game.switchToDusk();
 
if($MOTDprinting == true)
{
    %game.motdDisplaySchedule = %game.schedule($MOTDcycletime , "motdDisplay");
}
 
}
function RPGGame::saveWorld(%game)
{
         if($debugMode == true) echo("RPGGame::SaveWorld(" @ %game @ ");");
    if (isEventPending(%game.saveWorldSchedule))
        cancel(%game.saveWorldSchedule);
    echo("Saving world...");

    %cgroup = nametoID("ClientGroup");
    %count = %cgroup.getCount();
    for (%i = 0; %i < %count; %i++)
        %game.saveCharacter(%cgroup.getObject(%i));

    if ($SaveWorldMessage !$= "")  //if  $SaveWorldMessage string is not equal to "" , then
        BottomPrintAll($SaveWorldMessage @ "\n\n(World saved)", 5, 3);
    else
        BottomPrintAll("\n(World saved)", 5, 3);
    %game.saveWorldSchedule = %game.schedule($saveWorldDelay, "saveWorld");
}
function RpgGame::motdDisplay(%game)   //shinji's prideful motddisplay of nubbishness  (aka first real "new" script)
{                                      //i shall name it simba.
    if (isEventPending(%game.motdDisplaySchedule))
    cancel(%game.motdDisplaySchedule);

        BottomPrintAll($MOTDmessage, 6, 1);

    if($debugMode == true) echo("RpgGame::motdDisplay(" @ %game @ ");");
    echo($MOTDmessage);
    if($MOTDprinting == true)
    {
        %game.motdDisplaySchedule = %game.schedule($MOTDcycletime , "motdDisplay");
    }
}
function RpgGame::weatherCycle(%game)
{
    %group = nameToID("ClientGroup");
    %len = %group.getCount();
    $WeatherChanging = true;
    schedule(2000,0, eval, "$WeatherChanging=false;");

    bottomPrintAll("\nChanging weather...", 3, 3);

    %newWeather = %game.currentWeather++;
    if (%newWeather == 4)
        %newWeather = %game.currentWeather = 1;
    %wait = $weatherWait;
    switch(%newWeather)
    {
        case 1:
            if (!isFile("missions/" @ $CurrentMission @ "/night.cs"))     //is this is how it detects the day/night...?
                %game.exportNight();
            %game.schedule(1000,switchToDay);
        case 2:
            %game.schedule(1000,switchToDusk);
            %wait /= 2;
        case 3:
            if (!isFile("missions/" @ $CurrentMission @ "/day.cs"))
                %game.exportDay();
            %game.schedule(1000,switchToNight);
        default:
            %game.currentWeather = 0;
    }
    %game.weatherLoop = %game.schedule(%wait, "weatherCycle");
}
function RPGGame::exportDay(%game)
{
	if($debugMode == true) echo("RPGGame::exportDay(" @ %game @ ");");
    if ($exportedDay)
        return;
    if (!isObject(rpgdayeffect))
    {
        %game.schedule(10*1000, "ExportDay");
        return;
    }

	%obj = rpgdayeffect;
 	%obj.save("missions/" @ $CurrentMission @ "/day.cs");
	%obj.delete();
    $exportedDay = true;
}
function RPGGame::exportNight(%game)
{
	if($debugMode == true) echo("RPGGame::exportNight(" @ %game @ ");");
	//export and delete!
    if ($exportedNight)
        return;
    if (!isObject(rpgnighteffect))
    {
        %game.schedule(10*1000, "ExportNight");
        return;
    }
	%obj = rpgnighteffect;
	%obj.save("missions/" @ $CurrentMission @ "/night.cs");
	%obj.delete();
    $exportedNight = true;
}
function RPGGame::switchtoDay(%game)
{
	if($debugMode == true) echo("RPGGame::switchtoday(" @ %game @ ");");
	if($daynightcycle != -1)
	{
        if (isObject(RPGNightEffect))
		    RPGNightEffect.delete();

		exec("missions/" @ $CurrentMission @ "/day.cs");
		MissionCleanup.add(RPGDayEffect);
	}
	Game.addDaySky();
	
}
function RPGGame::switchtoNight(%game)
{
	if($debugMode == true) echo("RPGGame::switchtonight(" @ %game @ ");");
	if($daynightcycle != -1)
	{
        if (isObject(RPGDayEffect))
            RPGDayEffect.delete();
		exec("missions/" @ $CurrentMission @ "/night.cs");
		MissionCleanup.add(rpgNightEffect);
	}
	
	%game.addNightSky();
	
}
function RPGGame::switchtodusk(%game)
{
	if($debugMode == true) echo("RPGGame::switchtodusk(" @ %game @ ");");
	game.addDuskSky();
 //asdafgdfgdfg//
 //hfghfgh look at me i'm not pretty
}
function RPGGAME::AddDaySky(%game)
{
	if($debugMode == true) echo("RPGGame::adddaysky(" @ %game @ ");");
    if (isObject(Sky))
	   Sky.delete();
	new Sky(Sky) {
		position = "-1216 -848 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.5";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "1000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.490196 0.639215 0.701960 0.000000";
		fogDistance = "450";
		fogColor = "0.490196 0.639215 0.701960 1.000000";
		fogVolume1 = "200 1 46";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "RPG_WorldMapSky.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "0.415000 0.666000 0.786000 1.000000";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 -198748244414614883000000000000000000000.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 -222768174765569861000000000000000000000.000000";
		high_visibleDistance = "-1";
		high_fogDistance = "-1";
		high_fogVolume1 = "-1 100 100";
		high_fogVolume2 = "-1 0 0";
		high_fogVolume3 = "-1 1.73876e-39 9.48132e+33";

		locked = "true";
		cloudSpeed0 = "0.000000 0.000000";
	};
	MissionGroup.add(Sky);
}
function RPGGAME::AddNightSky(%game)
{
	if($debugMode == true) echo("RPGGame::addnightsky(" @ %game @ ");");
    if (isObject(Sky))
	   Sky.delete();
	new Sky(Sky) {
		position = "-1216 -848 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.5";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "650";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.0 0.0 0.0 0.000000";
		fogDistance = "25";
		fogColor = "0.000000 0.000000 0.000000 0.000000";
		fogVolume1 = "200 10 100";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "RPG_Night.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "0.415000 0.666000 0.786000 1.000000";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 -198748244414614883000000000000000000000.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 -222768174765569861000000000000000000000.000000";
		high_visibleDistance = "-1";
		high_fogDistance = "-1";
		high_fogVolume1 = "-1 100 100";
		high_fogVolume2 = "-1 0 0";
		high_fogVolume3 = "-1 1.73876e-39 9.48132e+33";

		locked = "true";
		cloudSpeed0 = "0.000000 0.000000";
	};
	MissionGroup.add(Sky);
}
function RPGGAME::AddDuskSky(%game)
{
	if($debugMode == true) echo("RPGGame::adddusksky(" @ %game @ ");");
    if (isObject(Sky))
	   Sky.delete();
	new Sky(Sky) {
		position = "-1216 -848 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.5";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "825";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.0 0.0 0.0 0.000000";
		fogDistance = "200";
		fogColor = "0.490196 0.639215 0.701960 0.500000";
		fogVolume1 = "200 1 46";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "RPG_50.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "0.415000 0.666000 0.786000 1.000000";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 -198748244414614883000000000000000000000.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 -222768174765569861000000000000000000000.000000";
		high_visibleDistance = "-1";
		high_fogDistance = "-1";
		high_fogVolume1 = "-1 100 100";
		high_fogVolume2 = "-1 0 0";
		high_fogVolume3 = "-1 1.73876e-39 9.48132e+33";

		locked = "true";
		cloudSpeed0 = "0.000000 0.000000";
	};
	MissionGroup.add(Sky);
}

function RPGGAME::CreateSkyMap(%game)
{
if($debugMode == true) echo("RPGGame::createskymap(" @ %game @ ");");		
	
}
function RPGGame::clientMissionDropReady(%game, %client)
{
	if($debugMode == true) echo("RPGGame::clientMissionDropReady(" @ %game @ ", " @ %client @ ");");

	//synchronize the clock HUD
	messageClient(%client, 'MsgSystemClock', "", 0, 0);

	//%game.sendClientTeamList(%client);
	%game.setupClientHuds(%client);

	// were ready to go.
	%client.matchStartReady = true;
	echo("Client" SPC %client SPC "is ready.");

	clearBottomPrint(%client);

	%retval = loadcharacter(%client);//use rpgname to load character...
	
	if(%retval)
	{
		if(%client.choosingGroup == true)
		{
			%client.game = %game;
			commandToClient(%client, 'OpenISMenu');
			commandToClient(%client, 'RPGplayMusic', "SoftTown");
			game.BuildMenu(%client, 0, 0);
		}
		else
		%game.spawnPlayer(%client, false);
	}
	else
	{
	      //this shouldnt happen but sometimes it can... ew....
	      // kill and delete this client
	      if( isObject( %client.player ) )
	         %client.player.delete();
      
         if ( isObject( %client ) )
         {
            echo("Critical error, character could not be loaded!!");
            %client.setDisconnectReason( "Your character could not be created or loaded! Notify server administrator to get it fixed!" );
	         %client.schedule(700, "delete");
         }
	}
}

function RPGGame::spawnPlayer(%game, %client, %respawn)
{
	if($debugMode == true) echo("RPGGame::spawnPlayer(" @ %game @ ", " @ %client @ ", " @ %respawn @ ");");
	if(!%respawn && !%client.newplayer )
	{
		%spawnpoint = fetchdata(%client, "camppos");
		
		if(!(%spawnpoint !$= "" && %spawnpoint != -1))
		{
			%spawnPoint = %game.pickPlayerSpawn(%client);
		}
		
		
	}
	else
	if(%client.newplayer)
	{
		if(isObject(TrainingSpawn))
		%spawnpoint = TrainingSpawn.getTransform();
		else
		%spawnPoint = %game.pickPlayerSpawn(%client);
	}
	else
	%spawnPoint = %game.pickPlayerSpawn(%client);
	%game.createPlayer(%client, %spawnPoint, %respawn);
}

function RPGGame::pickPlayerSpawn(%game, %client)
{
	if($debugMode == true) echo("RPGGame::pickPlayerSpawn(" @ %game @ "," SPC %client @ ");");
	if(fetchdata(%client, "lastzone") !$= "")
	{
		%lzone = fetchdata(%client, "lastzone");
		if(%lzone.deathspawn !$= "")
		{
			return %lzone.deathspawn SPC "1 0 0 0";
		}
		
	}
	return selectRandomMarker("MissionGroup/DefaultSpawnPoints");
}

function RPGGame::createPlayer(%game, %client, %spawnLoc, %respawn)
{
	if($debugMode == true) echo("RPGGame::createPlayer(" @ %game @ ", " @ %client @ ", " @ %spawnLoc @ ", " @ %respawn @ ");");

	if(%client.isAiControlled())
		GiveAIDefaults(%client);
	
	%client.team = 1;

	if(%client.player > 0)
		error("Attempting to create an angus ghost!");

	//defaultplayerarmor is in 'players.cs'
	if(%spawnLoc == -1)
        ///%spawnLoc="-1125.34 -2238.63 216.076 1 0 0 0";
		%spawnLoc = "0 0 300 1 0 0 0";

	
	%armor = fetchData(%client, "RACE") @ "Armor";
	%armor = "D";
	
	switch$(%armor)
	{
		case "MaleOrcArmor": echo("valid");
		case "MaleHumanArmor":echo("valid");
		default:echo("INVALID ARMOR - " SPC %armor SPC " - using server default.");
		%armor = "MaleHumanArmor";
	}
	%client.armor = %armor;
	//echo("PLAYER" @ %client.key);
	//%client.armor = "PLAYER" @ %client.key;
	%player = new Player()
	{
		dataBlock = %client.armor;
	};

	%player.setTransform(%spawnLoc);
	MissionCleanup.add(%player);

	// setup some info
	%player.setOwnerClient(%client);
	%player.team = %client.team;
	%client.player = %player;
	if(%respawn)
		if(%game.isjailed(%client))
		%player.setPosition(game.GetPositionForJailNumber(game.GetJailNumber(%client) ) );

	%game.playerSpawned(%client.player);
}

function UpdateTargetStuff(%client)
{
	if($debugMode == true) echo("UpdateTargetStuff(" @ %client @ ");");
	%player = %client.player;

	%player.setTarget(%client.target);
	setTargetDataBlock(%client.target, %player.getDatablock());

	setTargetSensorData(%client.target, PlayerSensor);
	setTargetSensorGroup(%client.target, 1);
	%client.setSensorGroup(1);

	setSensorGroupColor(1, 0xfffffffe, "196 188 10 128");
}

function RPGGame::playerSpawned(%game, %player)
{
	if($debugMode == true) echo("RPGGame::playerSpawned(" @ %game @ ", " @ %player @ ");");

	%client = %player.client;

	if(%client.respawnTimer)
		cancel(%client.respawnTimer);

	%client.observerStartTime = "";

	//set the spawn time (for use by the AI system)
	%client.spawnTime = getSimTime();

	%client.camera.mode = "";

	commandToClient(%client, 'setHudMode', 'Standard');
	%client.setControlObject(%player);

	// updates client's target info for this player
	UpdateTargetStuff(%client);

	//--- temp until loadchar comes in
	if(fetchdata(%client, "tmpHP")>0)
	setHP(%client, fetchData(%client, "tmpHP"));
	else
	setHP(%client, fetchData(%client, "maxHP"));
	if (fetchData(%client, "tmpMANA") > 0)
	setMANA(%client, fetchData(%client, "tmpMANA"));
	else
	setMANA(%client, fetchData(%client, "maxMANA"));
	
	storeData(%client, "templvl", GetLevel(fetchData(%client, "EXP")));
	//----
	commandToClient(%client, 'setReticle', "gui/ret_blaster" , true);
	storeData(%client, "HasLoadedAndSpawned", true);
	if(%client.newplayer)
	{
		%client.newplayer = false;
		MessageClient(%client, 'MessageStart', "Go to the nearest human and type #say hi to talk to him.");
	}
	if($rules $= "dm" )
	{
		clearInventory(%client);
		GiveDmEquipment(%client);
	}
	//%player.setrechargerate(0.1);
	RefreshAll(%client);
}


function Player::scriptKill(%player, %damageType)
{
	if($debugMode == true) echo("Player::scriptKill(" @ %player @ ");");
   //%player.scriptKilled = 1; 
   %player.setInvincible(false);

   //%targetObject, %sourceObject, %position, %amount, %damageType, %momVec
   //game.onClientDamaged(%player.client, %player.client, 2, 0, 0);
   %player.damage(0, %player.getPosition(), 100, $DamageType::Suicide);
}
function unfreeze(%id)
{
	if($debugMode == true) echo("unfreeze(" @ %id @ ");");
	%id.setControlObject(%id.player);
	storeData(%id, "frozen", "");
}
function RPGGame::onClientDamaged(%game, %clVictim, %clAttacker, %damageType, %sourceObject, %value, %pos, %damloc) {
	if($debugMode == true) echo("RPGGame::onClientDamaged(" @ %game @ "," SPC %clVictim @ "," SPC %clAttacker @ "," SPC %damageType @ "," SPC %sourceObject @ "," SPC %value @ "," SPC %pos @ "," SPC %damloc @ ");");
	//if(%damageType == $DamageType::Lava)
	//	echo("lava: " @ "%clvictim -> " @ %clvictim @ " %clattacker -> " @ %clattacker);
	%weapon = fetchData(%clVictim, "weaponInHand");
	if(%clVictim > 0)
	{
		//even if it ends up a miss, the persons healing rate will stop.
		%client.lasthittime = getSimTime();
	        %clVictim.humiliated = "";
		%suicide = ( %damageType == 99 ? 1 : 0 );
		if(%suicide)
		{
			%clAttacker = %clVictim;
		}
		%backupweapon = %weapon;
		%weapon = %game.GetItem(%clAttacker, fetchData(%clAttacker, "weaponInHand"));
		%weaponid = fetchData(%clattacker, "weaponinHand");
		%dweapon = %game.GetItem(%clVictim, fetchData(%clVictim, "weaponInHand"));
		%vguildid = IsInWhatGuild(%clVictim);
		%aguildid = IsInWhatGuild(%clAttacker);
		
		if(%vguildid != -1)
			%vGuild = GuildGroup.getObject(%vguildid);
		if(%aguildid != -1)
			%aGuild = GuildGroup.GetObject(%aguildid);
		
		%focused = 0;
		%bash = 0;
		%backstab = 0;
		%mugged = 0;
		%cleave = 0;
		%ignite = 0;
		%disrupt = 0;
		if(%clAttacker && %clAttacker !$= %clVictim)
		{
			%clVictim.lastDamageTime = getSimTime();
			%clVictim.lastDamageClient = %clAttacker;
			if(%clVictim.isAIControlled())
				%clVictim.clientDetected(%clAttacker);
		}   

		%skilltype = $SkillType[%weapon];
		%dskilltype = $SkillType[%dweapon];
		%clVictimPos = %clVictim.player.getPosition();
		if(%clAttacker.player)
		%clAttackerPos = %clAttacker.player.getPosition();

		%victimCurrentArmor = GetCurrentlyWearingArmor(%clVictim);

		//==============
		//PROCESS STATS
		//==============
		%isMiss = false;
		%sameRaceNull = false;
		//------------- CREATE DAMAGE VALUE -------------
		if(%damagetype == $DamageType::Spell)
		{
		        if(%clAttacker.focus == true)
		        {
		        	%focused = true;
		        	%clAttacker.focus = false;
		        }
			%spell = %sourceobject.spell; //workaround
			
			if(%spell $= "")
			%spell = %clAttacker.lastspell;
			%clAttacker.lastspell = "";
			%weapon = %spell;
			%value = $spelldata[%spell, DamageMod];//booom!
			%element = $spelldata[%spell, Element];
			//For the case of SPELLS, the initial damage has already been determined before calling this function
			%skilltype = $Skill::OffensiveCasting;

			%dmg = %value;
			%value = (%dmg / 100) * GetPlayerSkill(%clAttacker, %skilltype) + %dmg/10;
			if(%clAttacker.isAiControlled())
				%value /= 2;//drop damage by 2 for bots, bots dont have to worry about mana.
			%md = fetchdata(%clVictim, "MDEF");
			
			
			if( $Spell::ElementDefense[%element] != 3 )
			%md = CombineRpgRolls(%md, AddPoints(%clVictim,  $Spell::ElementDefense[%element]), 0, "inf");
			%md = GetRPGRoll(%md);
			
			%ab = (getRandom() * (%md*10)) + 1;
			%value = %value - %ab;
			
			//damage resistance now
			
			%value = Cap((%value)/1000, 0, "inf");
			
		}
		else if(%damageType != $DamageType::Ground && %damageType != $DamageType::Impact)
		{
			if(fetchData(%clVictim, "invisible") && %clVictim.adminLevel < 5)
				Client::unhide(%clvictim);

			//weapon
			//----------
			//%rweapon = getitem(fetchData(%clAttacker, "weaponInHand"));

			//if(%rweapon !$= "")
			//	%rweapondamage = GetRpgRoll(GetWord(GetAccessoryVar(%rweapon, $SpecialVar), 1));
			//else
			//	%rweapondamage = 0;
			%weapondamage = GetRpgRoll(AddPoints(%clAttacker, 6));
			
			%weapdam = %weapondamage;
			
			%multi = Cap((%clAttacker.data.PlayerSkill[%skilltype]*(getRandom()+0.2) ), 1, "inf");
			if(%multi < 0) %multi = 0;
			%finalweapondamage = round(%weapdam * (%multi/1000+1) + %clattacker.data.PlayerSkill[%skilltype]*getRandom()/8);
			
			//armor
			//----------
			%rarmor = GetRpgRoll(fetchData(%clVictim, "DEF"));//its definately here
			if(fetchdata(%clattacker, "NextHitCleave") == true && %skilltype == $skill::slashing)
			{
				if(fetchdata(%clAttacker, "Mana") >= 2)
				{
					%rarmor = 0;

					%multi = Cap((%clAttacker.data.PlayerSkill[%skilltype]*getRandom()/2), 1, "inf");
					if(%multi < 0) %multi = 0;
					%finalweapondamage = round(%weapdam * (%multi/1000+1) + %clattacker.data.PlayerSkill[%skilltype]*getRandom()/10 )*2;

					%cleave = true;
					refreshMANA(%clAttacker, 2);
				}
				else
					MessageClient(%clAttacker, 'failedCleave', "You do not have enough Mana to Cleave.");
				game.schedule(15*1000, "resetcleaveflag", %clAttacker);
				storedata(%clAttacker, "NextHitCleave", false);
			}

			if(fetchdata(%clattacker, "NextHitBackstab") == true && %skilltype == $skill::Piercing)
			{
				%backfront = getword(%damloc, 1);
				storedata(%clattacker, "NextHitBackstab", false);
				game.schedule(10*1000, "resetbackstabflag", %clAttacker);			
				if(%backfront $= "back_right" || %backfront $= "back" || %backfront $= "back_left" || %backfront $= "middle_back")
				{
					if(fetchdata(%clAttacker, "Mana") >= 1)
					{
						%backstab = true;
						%finalweapondamage *= 4;//OUCH
						refreshMANA(%clAttacker, 1);
					}
					else
					MessageClient(%clAttacker, 'failedbackstab', "You do not have enough Mana to backstab.");
					
				}

			}
			if(fetchdata(%clattacker, "NextHitLeg") == true && %skilltype == $skill::Slashing)
			{
				
				%targetleg = true;
				storedata(%clAttacker, "NextHitLeg", false);
				game.schedule(20*1000, "resetlegflag", %clattacker);
	
			}
			if(fetchdata(%clattacker, "NextHitEncumber") == true && %skilltype == $skill::Piercing)
			{
				%encumber = true;
				
				storedata(%clAttacker, "NextHitEncumber", false);
				game.schedule(20*1000, "resetEncumberflag", %clattacker);

			}
			if(fetchdata(%clattacker, "NextHitStun") == true && %skilltype == $skill::Bludgeoning)
			{
				%stun = true;
				
				storedata(%clAttacker, "NextHitStun", false);
				game.schedule(20*1000, "resetStunflag", %clattacker);
				
		
			}
			if(fetchdata(%clattacker, "NextHitMug") == true)
			{
				
				storedata(%clattacker, "NextHitMug", false);
				
				%mugged = true;
				game.schedule(15*1000, "resetmugflag", %clattacker);
			
			}
			if(%clattacker.hitignite == true)
			{
				%ignite = true;
				%clattacker.hitignite = false;
				%em = createEmitter(%pos, "minelampemitter", "0 0 0");
				%em.schedule(1000,"delete");
				%value *= 1.5;
				
				
			}
			%value = Cap( (%finalweapondamage - %rarmor)/2, 0, "inf");//yea armor does take effect.
			
			if(%value < 1)
				%value = 1;
			if(%suicide)
				%value = fetchData(%clVictim, "MaxHP");
			%value = (%value / $TribesDamageToNumericDamage);
			if(%skilltype == $skill::Bludgeoning)
			{
				if(fetchdata(%clAttacker, "NextHitBash") == true)
				{
					//bash!
					if(fetchdata(%clAttacker, "Mana") >= 1)
					{
						%bash = true;
						%eye = %clAttacker.player.getEyeVector();
						%vec = vectorScale(%eye, %clAttacker.data.PlayerSkill[$Skill::Bashing]/5);
						%vec = VectorAdd(%vec, "0 0 15");
						refreshMANA(%clAttacker, 1);
					}
					else
					MessageClient(%clAttacker, 'failedbash', "You do not have enough Mana to bash");
					game.schedule(10 * 1000, "resetbashflag", %clAttacker);
					storedata(%clAttacker, "NextHitBash", false);
      				}
      				else if(fetchdata(%clAttacker, "NextHitDisrupt") == true)
      				{
      					if(fetchdata(%clAttacker, "Mana") >= 1)
      					{
      						
      						%Disrupt = true;
      						refreshmana(%clattacker, 1);
      						if(iseventPending(%clvictim.spellcast))
      						{
      							cancel(%clvictim.spellcast);
      						}
      					}
      					else
      						messageClient(%clattacker, 'faileddisrupt', "You do not have enough Mana to disrupt");
      					game.schedule(15 * 1000, "resetDisruptFlag", %clAttacker);
      					storedata(%clAttacker, "NextHitDisrupt", false);
      					
      				}
			}
		}
		else
		{
			%skilltype = 0;
		}

		//------------- DETERMINE MISS OR HIT -------------
		if(%damagetype !$= $LandingDamageType && %clAttacker !$= %clVictim && !%suicide && !(%clattacker.isaicontrolled() && %clvictim.isaicontrolled()))
		{
			//%skilldiff = %clVictim.PlayerSkill[%skilltype] - %clAttacker.PlayerSkill[%skilltype];
			//if(%skilldiff >= 0)
			//	%m = 0.038;
			//else
			//	%m = -0.038;
			%d = %clAttacker.data.PlayerSkill[%skilltype] * getRandom() * 1.2 + 2;
			%k = (%clvictim.data.PlayerSkill[%skilltype] / 20 + (%clvictim.data.PlayerSkill[%dskilltype] / 2)) * getrandom();
			//%y = %m * %skilldiff * %skilldiff + 5;
			//%p = Cap(%y, 0, 100);

			//%r = (getRandom() * 100) + 1;
			
			if(%d / %k < 1)
			{
				if(%focused)
				{
					%weakhit = true;
					%value /= 2;
				}
				else
					%isMiss = true;
			}
			else if (%d / %k < 2)
			{
				if(!%focused)
				{
					%weakhit = true;
					%value /= 2;
				}
			}
			else if (%d / %k > 5)
			{
				%criticalhit = true;
				%value *= 2;
			}
			else
			{
				if(%focused)
				{
					%criticalhit = true;
					%value *= 2;
				}
			}
		}
		else if(%damageType == $DamageType::Lava)
		{
			%value = fetchdata(%clvictim, "HP") * %value;
			%value = (%value / $TribesDamageToNumericDamage);
		}
		else if(%damageType == $DamageType::Impact || %damageType == $DamageType::Ground)
		{
			%value = fetchdata(%clvictim, "HP") * %value;
			%value = (%value / $TribesDamageToNumericDamage);
		}
		//-------------------------------------------------
		// IF PLAYER IS ADMIN, NULLIFY LANDING DAMAGE
		// IF PLAYER IS SUPERADMIN, NULLIFY ALL DAMAGE
		//-------------------------------------------------
		if(%clVictim.adminLevel >= 4 && (%damageType == $DamageType::Impact || %damageType == $DamageType::Ground))
			%value = 0;
		if(%clVictim.adminLevel >= 5)
			%value = 0;

		//------------------------------------------------
		// SAME RACE CHECKS
		//------------------------------------------------
		if(IsSameRace(%clVictim, %clAttacker) && %clAttacker !$= %clVictim && $rules !$= "dm")
		{
            %pvpZneAtt = fetchData(%clAttacker, "inpvpzone");
            %pvpZneVic = fetchData(%clVictim, "inpvpzone");
			if(inarenabattle(%clAttacker) && inarenabattle(%clVictim))
			{
				//combat in the arena. allow damage!
			}
            else if(%pvpZneAtt && %pvpZneVic) {
                //both players in pvp zone, allow damage
            }
			else if( %aGuild > 0 && fetchdata(%clAttacker, "zone").owner == %aguild && %aGuild != %vGuild && fetchdata(%clattacker, "zone") == fetchdata(%clvictim, "zone"))
			{
				//let damage pass
			}
			else if( !(IsInCommaList(fetchData(%clVictim, "targetlist"), %clAttacker.rpgname) || IsInCommaList(fetchData(%clAttacker, "targetlist"), %clVictim.rpgname)) )
			{
				//no target-list involved
	
				if(%clVictim.guildmatchpvp && %clAttacker.guildmatchpvp)
				{
					if(%clvictim.enemyguild == %aGuild || %clAttacker.enemyguild == %vguild)
					{
						//valid let damage pass
					}
					else
					{
						%value = 0;
						%isMiss = false;
						%noImpulse = true;
						%sameRaceNull = true;				
					}
				}
				else
				{
					%value = 0;
					%isMiss = false;
					%noImpulse = true;
					%sameRaceNull = true;				
				}				

			}
			else
			{
				//echo("true");
				//one of the people involved has the other one on his/her target-list.
				//so let damage go thru
			}
		}
		//-------------------------------------------------
		// SAME PLAYER CHECKS
		//-------------------------------------------------
		if((%clVictim $= %clAttacker) && %suicide == 0)
		{
			if(%damagetype $= $DamageType::Spell)
			{
				%value = %value / 3;
				if(%value < 0.01)
				 %value = 0.01;
			}
		}
		else
		{
			//suicide stabbed self
		}

		if(!IsDead(%clVictim))
		{
			%hitby = %clAttacker.nameBase;
			%msgcolor = "";
			//calculate spelldamage resistance
			if(%skilltype == $Skill::OffensiveCasting)
			{
			
				//absorb damage
				%value *= $TribesDamageToNumericDamage;
				%rvalue = round(%value);
			
				
				%rvalue = ModifyBonusState(%clvictim, $Spell::ElementResistance[%element], %rvalue);
				if(%rvalue > 0) //continue
				%rvalue = ModifyBonusState(%clvictim, $Spell::ElementResistance[Generic], %rvalue);
				if(%rvalue <= 0)
				{
				%value = 0;
			
				}
				else
				%value = %rvalue;
				%value /= $TribesDamageToNumericDamage;
				
			}
			
			if(%isMiss)
			{
				%msgcolor = $MsgRed;
				%value = 0;

                // Fix for some silly message you get when admin
                if (trim(%hitby) $= "")
                    %msgcolor = "";
			}
			else if(!%isMiss && %value $= 0 && %clAttacker !$= %clVictim)
			{
				%msgcolor = $MsgWhite;
			}
			
			if(%msgcolor !$= "")
			{
				if(%damagetype !$= $DamageType::Spell)
				{
					messageClient(%clAttacker, 'onClientDamaged', %msgcolor @ "You try to hit " @ %clVictim.nameBase @ ", but miss!");
					messageClient(%clVictim, 'onClientDamaged', %msgcolor @ %hitby @ " tries to hit you, but misses!");
				}
				else
				{
					messageClient(%clAttacker, 'onClientDamaged', %msgcolor @ %clVictim.nameBase @ " resists your spell!");
					messageClient(%clVictim, 'onClientDamaged', %msgcolor @ "You resist " @ %hitby @ "'s spell!");
				}
			}

			//-------------------------------------------------
			// SKILLS
			//-------------------------------------------------
			if(%skilltype >= 1 && !%sameRaceNull && %clAttacker !$= %clVictim)
			{
				%base1 = Cap(35 + (fetchData(%clAttacker, "LVL") - fetchData(%clVictim, "LVL")), 1, "inf");
				%base2 = Cap(35 + (fetchData(%clVictim, "LVL") - fetchData(%clAttacker, "LVL")), 1, "inf");
				if(%isMiss)
				{
					if(%skilltype > 0)
					UseSkill(%clAttacker, %skilltype, false, true);
					UseSkill(%clVictim, $SkillEndurance, true, true, 60);

				}
				else if(!%isMiss && %value $= 0)
				{
					if(%skilltype > 0)
					UseSkill(%clAttacker, %skilltype, false, true);
					UseSkill(%clVictim, $SkillEndurance, true, true, 60);

				}
				else
				{
					UseSkill(%clVictim, $SkillEndurance, true, true, 60);
					if(%skilltype > 0)
					UseSkill(%clAttacker, %skilltype, true, true, %base1);

				}
			
				if(%bash || %disrupt)
					UseSkill(%clAttacker, $skill::Bashing, true, true, 5);
				if(%cleave || %targetleg)
					UseSkill(%clAttacker, $skill::Cleaving, true, true, 5);
				if(%focused)
					UseSkill(%clAttacker, $skill::focus, true, true, 5);
				if(%backstab || %encumber)
					UseSkill(%clAttacker, $skill::Backstabbing, true, true, 1/10);
				if(%ignite)
					UseSkill(%clAttacker, $skill::ignitearrow, true, true, 4);
				
					
			}
			if(%value < 0)
				%value = 0;

			if(%value)
			{
				if(%encumber)
				{
					//do the effect
					%time = 4;
					AddBonusState(%clVictim, "12 -40", %time, "Encumber");
					weightcall(%clVictim);
					//schedule(%time*1000+1, "CalculateBonusState", %clvictim);
					schedule(%time*1000+2, %clvictim, "weightStep", %clvictim);				
				}
				if(%stun)
				{

				//do the effect
					%time = 3;

					
					AddBonusState(%clVictim, "12 -100", %time, "Stun");
					%clvictim.player.setPosition(%clvictim.player.getPosition());
					%clvictim.player.applyimpulse("0 0 1");
					%clvictim.player.setVelocity("0 0 0");
					weightcall(%clVictim);
					%clvictim.schedule(%time*1000+2, "setControlObject",%clvictim.player);

					//schedule(%time*1000+1, "CalculateBonusState", %clvictim);
					schedule(%time*1000+2, %clvictim,  "weightStep", %clVictim);
				}
				if(%targetleg)
				{
				//do the effect
					%time = 5;

					AddBonusState(%clVictim, "12 -30", %time, "TargetLeg");

					weightcall(%clVictim);
					//schedule(%time*1000+1, "CalculateBonusState", %clvictim);
					schedule(%time*1000+2, %clvictim, "weightcall", %clvictim);				
				}
				if(%mugged)
				{
					domug(%clattacker, %clvictim, %damloc);
				
				}
				%backupValue = %value;
				
				//can it be a lck miss?
				if(%damageType == $DamageType::Lava )
					%canlck = 0;
				else
					%canlck = 1;
				//resolve damage
				%client.canlck = %canlck;
				%rhp = refreshHP(%clVictim, %value);
				if(%bash)
				%clVictim.player.setVelocity(%vec);
				if(%rhp $= -1)
					%value = -1;	//There was an LCK miss
				else
				{
					//if(%victimCurrentArmor !$= "")
					//	%ahs = $ArmorHitSound[%victimCurrentArmor];
					//else
						//%ahs = GetHitFleshSound(%weapon);
					if(%skilltype == $Skill::Slashing)
						%clVictim.player.Play3D(%ahs);
					else if(%skilltype == $Skill::Bludgeoning)
						%clVictim.player.Play3D(%ahs);
					else if(%skilltype == $Skill::Piercing)
						%clVictim.player.Play3D(%ahs);
					else if(%skilltype == $Skill::Archery)
						%clVictim.player.Play3D(ArrowHit);
					else
						%clVictim.player.Play3D(HitFlesh);
				}

				if(%clVictim.isAiControlled() && fetchData(%clVictim, "SpawnBotInfo") !$= "")
				{
					serverPlay3D(RandomRaceSound(fetchData(%clVictim, "RACE"), Hit), %clVictimPos);
				}

				//display amount of damage caused
				
				%convValue = round(%value * $TribesDamageToNumericDamage);
				
				if(%convValue > 0)
				{
					if(%damageType == $DamageType::Ground)
					{
						%hitby = "ground";
					}
					if(%clAttacker == %clVictim)
					{
							%hitby = "yourself";
					}
					else if(%clAttacker $= 0)
						%hitby = "an NPC";
					else
					{
						if(fetchData(%clAttacker, "invisible"))
							%hitby = "an unknown assailant";
						else
							%hitby = %clAttacker.nameBase;
					}

					if(%Backstab)
					{
						%daction = "backstabbed";
						%saction = "backstabbed";
					}
					else if(%Bash)
					{
						%daction = "bashed";
						%saction = "bashed";
					}
					else if(%Disrupt)
					{
						%daction = "disrupted";
						%saction = "disrupted";
					}
					else if(%cleave)
					{
						%daction = "cleaved";
						%saction = "cleaved";
					}
					else if(%mugged)
					{
						%daction = "mugged";
						%saction = "mugged";
					}
					else if(%targetleg)
					{
						%daction = "hit";// in the leg";
						%saction = "hit";// in the leg";
						%daAction = "in the leg ";
						%saAction = "in the leg ";
					}
					else if(%encumber)
					{
						%daction = "encumbered";
						%saction = "encumbered";
					}
					else if(%stun)
					{
						%daction = "stunned";
						%saction = "stunned";
					}
					else
					{
						%daction = "hit";
						%saction = "hit";
					}
					if(%weakhit)
					{
						%daction = "weakly" SPC %daction;
						%saction = "weakly" SPC %saction;
					}
					if (%criticalhit)
					{
						%daction = "critically" SPC %daction;
						%saction = "critically" SPC %saction;
					}
					//--------------------
					//display to involved
					//--------------------
					if(!%suicide)
					{
						if(%clAttacker !$= %clVictim && %clAttacker != 0)
							messageClient(%clAttacker, 'onClientDamaged', $MsgRed @ "You " @ %saction SPC %clVictim.nameBase SPC %saAction @ "for " @ %convValue @ " points of damage!");
							
						if(%damageType == $DamageType::Ground || %damageType == $DamageType::Impact)
							messageClient(%clVictim, 'onClientDamaged', $MsgRed @ "You fell to the ground for " @ %convValue @ " points of damage!");
						else
							messageClient(%clVictim, 'onClientDamaged', $MsgRed @ "You were " @ %daction SPC %daAction @ "by " @ %hitby @ " for " @ %convValue @ " points of damage!");
					}
					else
					{
						messageClient(%clVictim, 'onClientDamaged', $MsgRed @ "You hit yourself for " @ %convValue @ " points of damage!");
					
					}
					//--------------------
					//display to radius
					//--------------------
					if(%clAttacker $= 0)
					{
						
						%sname = "An NPC";
						%dname = %clVictim.rpgname;
					}
					else if(%clAttacker $= %clVictim)
					{
						%sname = %clAttacker.rpgname;
						if(stricmp(%clVictim.sex, "Male") $= 0)
							%dname = "himself";
						else if(stricmp(%clVictim.sex, "Female") $= 0)
							%dname = "herself";
						else
							%dname = "itself";
					}
					else
					{
						if(fetchData(%clAttacker, "invisible"))
						{
							%sname = "An unknown assailant";
							Client::unhide(%clAttacker);
						}
						else
							%sname = %clAttacker.rpgName;
						%dname = %clVictim.rpgName;
					}
					if(%damageType == $DamageType::Lava )
					{
						radiusAllExcept(%clVictim, 0, $MsgBeige @ %dname @ "'s life came to an untimely end in the lava.");
					}
					else if(%damageType == $DamageType::Ground || %damageType == $DamageType::Impact)
					{
						radiusAllExcept(%clVictim, 0, $MsgBeige @ %dname @ " fell to their death.");
					}
					else
						radiusAllExcept(%clVictim, %clAttacker, $MsgBeige @ %sname SPC %saction SPC %dname @ " for " @ %convValue @ " points of damage!");
					
				}
				else if(%convValue < 0)
				{
					//this happens when there's a LCK consequence as miss

					%hitby = %clAttacker.rpgName;

					messageClient(%clAttacker, 'onClientDamaged', $MsgRed @ "You try to hit " @ %clVictim.rpgName @ ", but miss! (LCK)");
					messageClient(%clVictim, 'onClientDamaged', $MsgRed @ %hitby @ " tries to hit you, but misses! (LCK)");
				}
				else
				{
					errorReport("ERROR! RPGGame.cs RPGGame::onClientDamaged(" @ %game @ ", " @ %clVictim @ ", " 
						@ %clAttacker @ ", " @ %damageType @ ", " @ %sourceObject @ ", " @ %weapon @ 
						") %convValue == 0 || %convValue $= \"\" %value = " @ %value @ " %backupvalue = " @ %backupvalue @ 
						" %clvictim.rpgname = " @ %clvictim.rpgname @ " %clAttacker.rpgName = " @ %clAttacker.rpgName @ 
						" Report this error line to dev if it does not show up on any known error list", 0);
				}

				//-------------------------------------------
				//add entry to damagedClient's damagedBy list
				//-------------------------------------------

				//make new entry with shooter's name
				if( %clAttacker !$= 0 && !%isMiss && fetchData(%clAttacker, "LVL") <= 150 )
				{
					%sname = %clAttacker.nameBase;
					%dname = %clVictim.nameBase;
					if(%clAttacker !$= %clVictim)
					{
						%index = "";
						for(%i = 1; %i <= $maxDamagedBy; %i++)
						{
							if(%clVictim.damagedBy[%i] $= "" && %index $= "")
								%index = %i;
						}
						if(%index !$= "")
						{
							%clVictim.damagedBy[%index] = %clAttacker @ " " @ %backupValue;
							%clVictim.player.schedule($damagedByEraseDelay * 1000, "clearDamagedBy", %index);
						}
						else
						{
							//too many hits on waiting list, he doesn't get in on exp.
						}
					}
				}

                if (%clVictim.isAIControlled())
                    %clVictim.hitCheck = ruby_add(epochTime(),10);

				%flash = Cap(%clVictim.player.getDamageFlash() + (%value * 2), 0, 0.75);
				%clVictim.player.setDamageFlash(%flash);

				if(fetchData(%clVictim, "HP") <= 0)
				{

					if(%clAttacker.isAiControlled())
					{
						if(false)
						serverPlay3D(RandomRaceSound(fetchData(%clAttacker, "RACE"), Taunt), %clAttackerPos);
					}
				}
			}

			if(%isMiss)
			{
				if(fetchData(%clVictim, "isBonused"))
				{
					//GameBase::activateShield(%this, "0 0 1.57", 1.47);
					serverPlay3D(SoundHitShield, %clVictimPos);
				}
			}
		}
	}
	else
	{
		errorReport("ERROR! RPGGame.cs RPGGame::onClientDamaged(" @ %game @ ", " @ %clVictim @ ", " 
		@ %clAttacker @ ", " @ %damageType @ ", " @ %sourceObject @ ", " @ %value @ 
		") :" @
		" Report this error line to dev if it does not show up on any known error list", 0);
	}

	//call the game specific AI routines...
	if(isObject(%clVictim) && %clVictim.isAIControlled() && %convValue > 0)
		%game.onAIDamaged(%clVictim, %clAttacker, %damageType, %sourceObject);
	if(isObject(%clAttacker) && %clAttacker.isAIControlled() )
		%game.onAIDoDamage(%clVictim, %clAttacker, %damageType, %sourceObject); //bot did damage

	//if(isObject(%clAttacker) && %clAttacker.isAIControlled())
	//	%game.onAIFriendlyFire(%clVictim, %clAttacker, %damageType, %sourceObject);
}
function errorReport(%string)
{
	echo(%string);
}
function RPGGame::ResetDisruptFlag(%game, %client)
{
	storedata(%client, "blockDisrupt", false);
}
function RPGGame::ResetBashFlag(%game, %client)
{
	storedata(%client, "blockbash", false);
}
function RPGGame::ResetCleaveFlag(%game, %client)
{
	storedata(%client, "blockcleave", false);
}
function RPGGame::ResetLegFlag(%game, %client)
{
	storedata(%client, "blockLeg", false);
}
function RPGGAME::ResetBackstabFlag(%game, %client)
{
	storedata(%client, "blockbackstab", false);
}
function RPGGAME::resetmugflag(%game, %client)
{
	storedata(%client, "blockHitMug", false);
}
function RPGGAME::resetEncumberFlag(%game, %client)
{
	storedata(%client, "blockEncumber", false);
}
function RPGGAME::resetStunFlag(%game, %client)
{
	storedata(%client, "blockStun", false);
}
function Armor::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType, %momVec)
{
	if(%targetObject.getState() $= "Dead")
		return;
	%selfkiller = "";

	if(%targetObject.isMounted())
	{
		%mount = %targetObject.getObjectMount();
		if(%mount.team == %targetObject.team)
		{
			%found = -1;
			for(%i = 0; %i < %mount.getDataBlock().numMountPoints; %i++)
			{
				if(%mount.getMountNodeObject(%i) == %targetObject)
				{
					%found = %i;
					break;
				}
			}

			if(%found != -1)
			{
				if(%mount.getDataBlock().isProtectedMountPoint[%found])
				{
					%mount.getDataBlock().damageObject(%mount, %sourceObject, %position, %amount, %damageType);
					return;
				}
			}
		}
	}

	
	%clVictim = %targetObject.getOwnerClient();
	if(%clVictim $= "")
	{
		for(%i = 0; GetWord($townbotlist,%i) !$= ""; %i++)
			if(%targetObject ==  GetWord($townbotlist,%i))
				return false;		
	}
	%clAttacker = %sourceObject ? %sourceObject.getOwnerClient() : 0;
    if( %damagetype == 8 )
      %clattacker = %sourceobject.sourceobject.getownerclient();
	Game.onClientDamaged(%clVictim, %clAttacker, %damageType, %sourceObject, %amount, %position,  %targetObject.getDamageLocation(%position));
	
	%clVictim.lastDamagedBy = %damagingClient;
	%clVictim.lastDamaged = getSimTime();

	//now call the "onKilled" function if the client was... you know...  
	if(%targetObject.getState() $= "Dead")
	{
		// where did this guy get it?
		%damLoc = %targetObject.getDamageLocation(%position);

		// If we were killed, max out the flash
		%targetObject.setDamageFlash(0.75);               
		%damLoc = %targetObject.getDamageLocation(%position);
		
		Game.onClientKilled(%clVictim, %clAttacker, %damageType, %sourceObject, %damLoc);
	}
	else if(%amount > 0.1)
	{
		if(%targetObject.station $= "" && %targetObject.isCloaked())
		{
			%targetObject.setCloaked(false);
			%targetObject.reCloak = %targetObject.schedule( 500, "setCloaked", true ); 
		}

		
	}
	playPain( %targetObject );
	%targetObject.scriptKilled = "";
}
function Armor::onImpact(%data, %playerObject, %collidedObject, %vec, %vecLen)
{
	//problem happens BEFORE this function is called. 
	//if(%playerobject.client.rpgname $= $merpgname)
	
	%data.damageObject(%playerObject, 0, VectorAdd(%playerObject.getPosition(),%vec), %vecLen * %data.speedDamageScale, $DamageType::Ground);
}

function RPGGame::onClientLeaveGame(%game, %client)
{

	// if there is a player attached to this client, kill it
	
	if ($DebugMode) echo("RPGGAME::onClientLeaveGame(" @ %game @ ", " @ %client @ ")");
	
	//cancel a scheduled call...

	logEcho(%client.nameBase@" (cl "@%client@") dropped");
}

function RPGGame::WeaponOnInventory(%game, %this, %obj, %amount)
{
	return true;
}
function RPGGame::ShapeThrowWeapon(%game, %this)
{
	return true;
}
function RPGGame::WeaponOnUse(%game, %data, %obj)
{
	return true;
}


function GameConnection::onConnect( %client, %name, %raceGender, %skin, %voice, %voicePitch )
{
	if ($DebugMode) echo("GameConnection::onConnect(" @ %client @ ", " @ %name @ ", " @ %raceGender @ ", " @ %skin @ ", " @ %voice @ ", " @ %voicePitch @ ")");

	//sendLoadInfoToClient( %client );
 
    commandToClient( %client, 'setBlackout', 1, true );
    commandToClient( %client, 'RPGLoadscreen', true );
    commandToClient( %client, 'RPGLoadscreenTitle', $Host::GameName );
    

	// Get the client's unique id:
	%authInfo = %client.getAuthInfo();
	%client.guid = getField( %authInfo, 3 );
	//gui check
	%client.where = 0;
	//create unique 'key' for player
	for(%i = 0; %i<$Host::MaxPlayers;%i++)
	{
		if($client::key[%i] == 0)
		{
			$client::key[%i] = %client;
			%client.key = %i;
			break;
		}
	}
	// check admin and super admin list, and set status accordingly
	if(!%client.isSuperAdmin)
	{
		if(isOnSuperAdminList(%client))
		{
			%client.isAdmin = true;
			%client.isSuperAdmin = true;   
		}
		else if(isOnAdminList(%client))
		{
			%client.isAdmin = true;
		}
	}

	// Sex/Race defaults
	switch$(%raceGender)
	{
		case "Human Male":
			%client.sex = "Male";
			%client.race = "Human";
		case "Human Female":
			%client.sex = "Female";
			%client.race = "Human";
		case "Bioderm":
			%client.sex = "Male";
			%client.race = "Bioderm";
	}
	%client.armor = "Light";

	// RPG mod will use a entirely different system for smurfs...
	%realName = getField(%authInfo, 0);
	%tag = getField( %authInfo, 1 );
	%rpgSpecial = %tag; //server owners can set that they have to be in a certain clan in order to be able to play on their server? alternate to a password...
				//or shall we have it for 'tribes'
	if(!$playingonline)
		%realname = %name;//doh!
	%rpgname = %name @ " " @ %realName;
	if(strcmp(%name, %realName) == 0)
	{
		%client.isSmurf = false;

		// Add the tribal tag:
		%append = getField( %authInfo, 2 );
		%rpgname = %name;
		%name = %name; // DO NOTHING WITH NAME! Needed for savecharacter!!		
		//addToServerGuidList( %client.guid );
		//%client.sendGuid = %client.guid;
	}
	else
	{
		%client.isSmurf = true;
		%smurfName = %name;
		%name = %realName;//for savecharacter!
		//rpgname is set above so we dont need to set it here
		
	}

	
	if(%client.isSmurf)
		%client.nameBase = %smurfName;
	else
		%client.nameBase = %realName;
		%client.name = addTaggedString(%client.namebase);
	//echo("client: " @ %client @ " name: " @ %realname);
	%client.realName = %realName;
	%client.rpgName = %rpgname;
	%client.clan = "";
	%client.special = %rpgSpecial;//unno we might use this later...
	%client.justConnected = true;
	%client.isReady = false;

	// Make sure that the connecting client is not trying to use a bot skin:
	%temp = detag(%skin);
	if( %temp $= "basebot" || %temp $= "basebbot")
		%client.skin = addTaggedString( "base" );
	else
		%client.skin = addTaggedString( %skin );

	// full reset of client target manager
	clientResetTargets(%client, false);

	%client.voice = %voice;
	%client.voiceTag = addtaggedString(%voice);

	//set the voice pitch based on a lookup table from their chosen voice
	%client.voicePitch = getValidVoicePitch(%voice, %voicePitch);

	%client.target = allocClientTarget(%client, %client.name, %client.skin, %client.voiceTag, '_ClientConnection', 0, 0, %client.voicePitch);
	%client.score = 0;
	%client.team = 0;

	$instantGroup = ServerGroup;
	$instantGroup = MissionCleanup;

	//echo("CADD: " @ %client @ " " @ %client.getAddress());

	%count = ClientGroup.getCount();
	for(%cl = 0; %cl < %count; %cl++)
	{
		%recipient = ClientGroup.getObject(%cl);
		if((%recipient != %client))
		{
			messageClient(%recipient, 'MsgClientJoin', %client.rpgname SPC "has joined the game.", 
	            	%recipient.name, 
		            %recipient, 
		            %recipient.target, 
		            %recipient.isAIControlled(), 
		            %recipient.isAdmin, 
		            %recipient.isSuperAdmin, 
		            %recipient.isSmurf, 
		            %recipient.sendGuid);
		}
	}

	//commandToClient(%client, 'getManagerID', %client);

	commandToClient(%client, 'setBeaconNames', "Target Beacon", "Marker Beacon", "Bomb Target");

	if($CurrentMissionType !$= "SinglePlayer") 
	{
		messageClient(%client, 'MsgClientJoin', '\c2Welcome to Tribes2 RPG %1.',
			%name, 
			%client, 
			%client.target, 
			false,   // isBot 
			%client.isAdmin, 
			%client.isSuperAdmin, 
			%client.isSmurf, 
			%client.sendGuid );

		messageAllExcept(%client, -1, 'MsgClientJoin', "", 
			%name, 
			%client, 
			%client.target, 
			false,   // isBot 
			%client.isAdmin, 
			%client.isSuperAdmin, 
			%client.isSmurf,
			%client.sendGuid );
	}
	else
	{
		messageClient(%client, 'MsgClientJoin', "\c0Mission Insertion complete...", 
			%name, 
			%client, 
			%client.target, 
			false,   // isBot 
			false,   // isAdmin 
			false,   // isSuperAdmin 
			false,   // isSmurf
			%client.sendGuid );
	}

	//Game.missionStart(%client);
	//setDefaultInventory(%client);

	if($missionRunning)
		%client.startMission();

	$HostGamePlayerCount++;
}
function RPGGame::checkClientVersion(%game, %client)
{
    %ver = %client.rpgver;
    echo("client has version" SPC %ver);
    if(%ver == $rpgver)
    {
        return false;
    }
    else
    {
        %pre = "Ironsphere RPG" SPC $rpgver @ "\n";
        if (trim(%ver) $= "")
            %client.setDisconnectReason(%pre NL "You do not have Ironsphere. You can download the mod at" SPC $IS::Web @ ".");
        else
            %client.setDisconnectReason(%pre NL "Your version of Ironsphere (" @ %ver @ ") is incompatible with this server. (" @ $rpgver @"). Please SVN Update on your ironsphererpg folder to update!");
        %client.delete();
        return true;
    }
}
function servercmdsetClientVersion(%client, %ver)
{
    if (%client.rpgver $= "")
        %client.rpgver = %ver;
}
function GameConnection::onDrop(%client, %reason)
{
	if ($DebugMode) echo("GameConnection::onDrop(" @ %client @ ", " @ %reason @ ")");

	if($missionRunning)
	{
		if(inArena(%client))
		{
			if(!inArenaRoster(%client))
			{
				%client.player.scriptKill(0);
			}
			LeaveArena(%client);//leave arena for saving.
		}
		%guildid = IsInWhatGuild(%client);
		%guild = GuildGroup.getObject(%guildid);
		if(%client.guildmatchpvp)
		{
			//eject from match, calculate if ended.
			%client.guildmatchpvp = "";
			%client.participate = "";
			%zone = fetchdata(%client, "zone");
			if(%guild == %zone.owner)
			{
				%zone.home--;
				if(%zone.home <= 0) 
					%zone.owner.EndZoneMatch( %zone, %zone.challenger);
			}
			if(%guild == %zone.challenger)
			{
				%zone.away--;
				if(%zone.away <= 0)
					%zone.owner.EndZoneMatch( %zone, %zone.challenger);
			}
			%client.enemyguild = "";
		}
		 //save if player is alive some reason %client.player remains set if server runs a DISSCONNECT yet the player is gone
		savecharacter(%client);
	}
	cancel(%client.weightstepcall);
	game.onClientLeaveGame(%client);//game doesnt end, lets see if this is the problem
	//fixed?...^^
	$client::key[%client.key] = "";
	if(isObject(%client.player))
		%client.player.schedule(500, delete);
	
	cancel(%client.respawnTimer);
	%client.respawnTimer = "";

	//if(!%client.isSmurf)
	//	removeFromServerGuidList( %client.guid );
	if(!%client.isaicontrolled())
	messageAllExcept(%client, -1, 'MsgClientDrop', %client.rpgname @ " has dropped.");

	if ( isObject( %client.camera ) )
		%client.camera.delete();

	freeClientTarget(%client);
	removeTaggedString(%client.name);
	//echo("CDROP: " @ %client @ " " @ %client.getAddress());
	if(!%client.isaicontrolled())
	$HostGamePlayerCount--;

	// reset the server if everyone has left the game
	//if( $HostGamePlayerCount == 0 && $Host::Dedicated)
	//	schedule(0, 0, "resetServerDefaults");
}

//  missioncleanup and missiongroup are checked prior to entering game code
function RPGGame::missionLoadDone(%game)
{
	//  walks through the mission group and sets the power stuff up
	//   - groups get initialized with power count 0 then iterated to 
	//     increment powercount if an object within is powered
	//   - powers objects up/down
	//MissionGroup.objectiveInit();
//#marker
	MissionGroup.clearPower();
	MissionGroup.powerInit(0);

	%game.initGameVars();  //set up scoring variables and other game specific globals

	setSensorGroupAlwaysVisMask(1, 0xffffffff);
	setSensorGroupFriendlyMask(1, 0xffffffff);

	// update colors:
	// - enemy teams are red
	// - same team is green
	// - team 0 is white
	for(%i = 0; %i < 32; %i++)
	{
		%team = (1 << %i);
		//setSensorGroupColor(%i, %team, "0 255 0 255");
		//setSensorGroupColor(%i, ~%team, "255 0 0 255");
		//setSensorGroupColor(%i, 1, "255 255 255 255");
		setSensorGroupColor(%i, %team, "0 255 0 255");
		setSensorGroupColor(%i, ~%team, "0 0 0 255");
		setSensorGroupColor(%i, 1, "0 255 255 255");	
		 //setup the team targets (always friendly and visible to same team)
		setTargetAlwaysVisMask(%i, %team);
		setTargetFriendlyMask(%i, %team);
	}

	//set up the teams
	%game.setUpTeams();//this will setup the townbots and guards

	//initialize the AI system
	%game.aiInit();//game::aiInit will call the map's ai load function.
	$townbotlist = "";
	DefineTownBots();
	DefineMiningPoints();
	LoadServerGuilds();//load up the guilds

	//Save off respawn or Siege Team switch information...
	//if(%game.class !$= "SiegeGame")
	//	MissionGroup.setupPositionMarkers(true);
	echo("RPGGame mission load done.");
}


function RPGGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation)
{
	if($debugMode == true) echo("RPGGame::onClientKilled(" @ %game @ "," SPC %clVictim @ "," SPC %clKiller @ "," SPC %damageType @ "," SPC %implement @ "," SPC %damageLocation  @ ");");
	%plVictim = %clVictim.player;
	%plKiller = %clKiller.player;
        %clKiller.tkills++;
        %clVictim.tkills++;
        if($rules $= "dm")
        {
		%ditem = fetchData(%clVictim, "weaponInHand");
		%clVictim.player.throw(%ditem);
	}
//============
	%guildid = IsInWhatGuild(%clvictim);
	%guild = GuildGroup.getObject(%guildid);
	if(%clvictim.guildmatchpvp)
	{
		
		//eject from match, calculate if ended.
		%clvictim.guildmatchpvp = "";
		%clvictim.participate = "";
		%zone = fetchdata(%clvictim, "zone");
		if(%guild == %zone.owner)
		{
			%zone.home--;
			if(%zone.home <= 0) 
				%zone.owner.EndZoneMatch( %zone, %zone.challenger);
		}
		if(%guild == %zone.challenger)
		{
			%zone.away--;
			if(%zone.away <= 0)
				%zone.owner.EndZoneMatch( %zone, %zone.challenger);
		}
		%clvictim.enemyguild = "";
		%guildkilled = true;
	}

	if(fetchData(%clVictim, "deathmsg") !$= "")
	{
		%kitem = Player::getMountedItem(%clKiller, $WeaponSlot);
		RPGchat(%clVictim, 0, fetchData(%clVictim, "deathmsg"));
	}

	if(%clVictim.isAiControlled())
	{
		//event stuff
		%i = GetEventCommandIndex(%clVictim, "onkill");
		if(%i !$= -1)
		{
			%name = GetWord($EventCommand[%clVictim, %i], 0);
			%type = GetWord($EventCommand[%clVictim, %i], 1);
			%cl = getClientByName(%name);
			if(%cl $= -1)
				%cl = 2048;

			%cmd = getSubStr($EventCommand[%clVictim, %i], String::findSubStr($EventCommand[%clVictim, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %clVictim, %clKiller);
			$EventCommand[%clVictim, %i] = "";
			schedule("RPGchat(" @ %cl @ ", 0, \"" @ %pcmd @ "\");", 2);	// \"" @ %name @ "\");", 2);
		}
		ClearEvents(%clVictim);
	}
	//ok now get the players lck.. if player died without any lck, drop everything, thats the shaft!
	%lootfl = fetchdata(%clVictim, "noDropLootbagFlag");
	if(inArena(%clVictim))
	   %lootfl = 1;
	if(%guildkilled)
	   %lootfl = 2;
	%lck = fetchdata(%clVictim, "LCK");
	if(%lck < 0 )
	   %timer = 60;
	else
	   %timer = 600;
	%ai = %clVictim.isaicontrolled();
	%loot = "";
    //Phantom139:
    //Added support for PvP Zones, certian zones are "item Safe" which means
    //players who die in these zones do not lose their items.
    %inv = fetchData(%clVictim, "inventory");
    %PvPISafe = false;
    if(fetchData(%clVictim, "inpvpzone") == true) {
       if(fetchData(%clVictim, "initemsafezone") == true) {
          %PvPISafe = true;
       }
    }
	for(%i = 0; (%itemstr = GetWord(%inv, %i)) !$= "" && %lootfl $= ""; %i++)
	{
		
		
		%itemstr = strreplace(%itemstr, "%", " ");
		%item = GetWord(%itemstr, 1);
		%prefix = GetWord(%itemstr, 0);
		%suffix = GetWord(%itemstr, 2);
		%num = %game.getItemCount(%clvictim, %item, %prefix, %suffix);
		%fullitem = %game.GetFullItemName(%prefix, %item, %suffix);
		%itemid = %clvictim.data.count[strreplace(%fullitem, " ", "x")];
		%type = $itemType[%item];
		
		
		if(%type $= "weapon" && %lck >= 0 && !%ai )
		{
			if(%clvictim.data.equipped[%itemid] )
			{
				%game.InventoryUse(%clvictim, %itemid);
                if(!%PvPISafe) {
				   %game.RemoveFromInventory(%clVictim, 1, %item, %prefix, %suffix);
				   %loot = %loot @ %prefix @ "%" @ %item @ "%" @ %suffix @ "%" @ 1 @ " ";
                }
			}
		}
		else if(%type $= "Armor" &&  %lck >= 0 && !%ai )
		{
			if(%clvictim.data.equipped[%itemid] )
			{
				%game.InventoryUse(%clvictim, %itemid);
                if(!%PvPISafe) {
				   %game.RemoveFromInventory(%clVictim, 1, %item, %prefix, %suffix);
				   %loot = %loot @ %prefix @ "%" @ %item @ "%" @ %suffix @ "%" @ 1 @ " ";
                }
			}		
		
		}
		else
		{
			if(%lck < 0 || %ai)
			{
				if(%clvictim.data.equipped[%itemid])
					%game.InventoryUse(%client, %itemid);//clean up
                if(!%PvPISafe) {
				   %game.RemoveFromInventory(%clVictim, %num, %item, %prefix, %suffix);
				   %loot = %loot @ %prefix @ "%" @ %item @ "%" @ %suffix @ "%" @ %num @ " ";
				}
			}
		}
	}
	
	if(%lootfl $= "" && !%PvPISafe) {
		%coins = FetchData(%clVictim, "COINS")+1-1;
		StoreData(%clVictim, "COINS", 0);
	}
	else if(%lootfl == 2 && !%PvPISafe) {
		%coins = Fetchdata(%clVictim, "COINS")+1-1;
		%bank = FetchData(%clVictim, "BANK")+1-1;
		%asset = %coins+%bank;
		%charge = mfloor(%asset/10);
		if(%charge > %coins)
		{
			%bank = %bank - (%charge - %coins);
			
		}
		else
		%charge = %coins;
		
		storedata(%clvictim, "COINS", 0);
		storedata(%clVictim, "BANK", %bank);
		%coins = %charge;
		%asset = "";
		%bank = "";
		%charge = "";
		%timer = 0;
	}

	if(%lootfl $= "" || %lootfl == 2)
       if(!%PvPISafe)
	      %Game.TossLootBag(%clVictim, %loot,%coins,  %timer);

	storeData(%clVictim, "noDropLootbagFlag", "");

	storeData(%clVictim, "SpellCastStep", "");
	%clVictim.sleepMode = "";
	refreshHPREGEN(%clVictim);
	refreshMANAREGEN(%clVictim);

	//remember the last zone the player was in.
	storeData(%clVictim, "lastzone", fetchData(%clVictim, "zone"));

	//%clVictim.player.playAudio(0, RandomRaceSound(fetchData(%clVictim, "RACE"), Death));

	DistributeExpForKilling(%clVictim);
	//RefreshExp(%clKiller);
//============


	%clVictim.plyrPointOfDeath = %plVictim.getPosition();
	%clVictim.waitRespawn = 1;

	cancel( %plVictim.reCloak );
	cancel(%clVictim.respawnTimer);

	%respawnDelay = 2;

	%game.schedule(%respawnDelay*1000, "clearWaitRespawn", %clVictim);

	if(%plVictim.lastVehicle !$= "")
	{
		schedule(15000, %plVictim.lastVehicle,"vehicleAbandonTimeOut", %plVictim.lastVehicle);
		%plVictim.lastVehicle.lastPilot = "";
	}   

	// unmount pilot or remove sight from bomber
	if(%plVictim.isMounted())
	{
		if(%plVictim.vehicleTurret)
			%plVictim.vehicleTurret.getDataBlock().playerDismount(%plVictim.vehicleTurret);
		else
		{
			%plVictim.getDataBlock().doDismount(%plVictim, true);
			%plVictim.mountVehicle = false;
		}
	}

	if(%plVictim.inStation)
		commandToClient(%plVictim.client,'setStationKeys', false);
	%clVictim.camera.mode = "playerDeath";

	// reset who triggered this station and cancel outstanding armor switch thread   
	if(%plVictim.station)
	{
		%plVictim.station.triggeredBy = "";
		%plVictim.station.getDataBlock().stationTriggered(%plVictim.station,0);
		if(%plVictim.armorSwitchSchedule)
			cancel(%plVictim.armorSwitchSchedule);
	}   

	//Close huds if player dies...
	messageClient(%clVictim, 'CloseHud', "", 'RPGinventoryScreen');
	messageClient(%clVictim, 'CloseHud', "", 'RPGshopScreen');
	messageClient(%clVictim, 'CloseHud', "", 'vehicleHud');
	commandToClient(%clVictim, 'setHudMode', 'Standard', "", 0);

	// $weaponslot from item.cs
	%plVictim.setRepairRate(0);
	%plVictim.setImageTrigger($WeaponSlot, false);
	
	
	playDeathAnimation(%plVictim, %damageLocation, %damageType);
	playDeathCry(%plVictim);
	

	//%ridx = mFloor(getRandom() * ($numDeathMsgs - 0.01));
	%victimName = %clVictim.name;

	//%game.displayDeathMessages(%clVictim, %clKiller, %damageType, %implement);

	// toss whatever is being carried, '$flagslot' from item.cs
	// MES - had to move this to after death message display because of Rabbit game type


	// target manager update
	setTargetDataBlock(%clVictim.target, 0);

	// clear the hud
	%clVictim.SetWeaponsHudClearAll();
	%clVictim.SetInventoryHudClearAll();
	%clVictim.setAmmoHudCount(-1);

	//if the killer was an AI...
	if(isObject(%clKiller) && %clKiller.isAIControlled())
		%game.onAIKilledClient(%clVictim, %clKiller, %damageType, %implement);

	// reset control object on this player: also sets 'playgui' as content
	ResetControlObject(%clVictim);

	// set control object to the camera   
	%clVictim.player.client = 0;
	%clVictim.player = 0;
	%transform = %plVictim.getTransform();

	//note, AI's don't have a camera...
	if (isObject(%clVictim.camera))
	{
		%clVictim.camera.setTransform(%transform);
		%clVictim.camera.setOrbitMode(%plVictim, %plVictim.getTransform(), 0.5, 4.5, 4.5);
		%clVictim.setControlObject(%clVictim.camera);
	}

	//hook in the AI specific code for when a client dies
	if(%clVictim.isAIControlled())
	{
		aiReleaseHumanControl(%clVictim.controlByHuman, %clVictim);
		%game.onAIKilled(%clVictim, %clKiller, %damageType, %implement);
	}
	else
		aiReleaseHumanControl(%clVictim, %clVictim.controlAI);

	//used to track corpses so the AI can get ammo, etc...
	AICorpseAdded(%plVictim);

	//if the death was a suicide, prevent respawning for 5 seconds...
	%clVictim.lastDeathSuicide = false;
	if(%damageType == $DamageType::Suicide)
	{
		%clVictim.lastDeathSuicide = true;
		%clVictim.suicideRespawnTime = getSimTime() + 5000;
	}
	if(inArena(%clVictim))
	{
		if(!%clvictim.isAiControlled()) //dontrespawn if AI.
		Game.createPlayer( %clVictim,ArenaExit1.getposition(), true);
		

	//give the clvictim a new player object then tele him out of the arena!
		LeaveArena(%clVictim);
		ResetControlObject(%clVictim);
		
	}
	if(%guildkilled)
	{
		storedata(%clvictim, "LCK", fetchdata(%clvictim, "LCK")+1);//negate lck cost. note lck is negitive 1 if killed with no lck
		if(fetchdata(%clvictim, "LCK") > 0)
		MessageClient(%clVictim, 'Lck', "You gained a LCK point");
	}
	if(%lck < 0)
		storedata(%clVictim, "LCK", 0);
	
}

function ItemData::onCollision(%data, %obj, %col)
{
	game.ItemDataonCollision(%data, %obj, %col);
	return;
	
}
function RPGshopScreen::updateHud(%this, %client, %tag, %aiId)
{
	//echo("RPGshopScreen::updateHud(" @ %this @ ", " @ %client @ ", " @ %tag @ "," SPC %aiId @ ")");
	commandToClient(%client, 'ShopUpdateHud');
	//Server-side
	%inv = fetchData(%client, "inventory");
	%coins = fetchdata(%client, "COINS");
	commandToClient(%client, 'SetCoins', %coins);
	%val = 0;
	for(%i = 0; (%item = GetWord(%aiId.shopwloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0,%item, $ItemDesc[%item]);
	}
	for(%i = 0; (%item = GetWord(%aiId.shopaloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0 ,%item, $ItemDesc[%item]);
	}
	for(%i = 0; (%item = GetWord(%aiId.shopiloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0 ,%item, $ItemDesc[%item]);
	}
	commandToClient(%client, 'ShopDone');
}
function serverCmdShopListOnSelect(%client, %item)
{
	//Server-side
	
	commandToClient(%client, 'ShopListOnSelect', GetItemCost(%client, %item));
}
function serverCmdShopInvListOnSelect(%client, %itemid, %item)
{

	commandToClient(%client, 'ShopInvListOnSelect', GetItemValue(%client, GetItem(%itemid)));
}
function serverCmdShopOnBuy(%client, %item, %delta)
{
	Client::OnBuy(%client, %Item, %delta);
}
function serverCmdShopOnSell(%client, %item, %delta)
{
	Client::OnSell(%client, %Item, %delta);
}
function serverCmdOnOpenStat(%client)
{
	RPGstatScreen::updateHud(1, %client, 'RPGstatScreen');
}
function RPGstatScreen::updateHud(%this, %client, %tag)
{
	
	//Server-side
	Game.UpdateRPGStatScreen(%this, %client, %tag);
	
}
function RPGGame::UpdateRPGStatScreen(%game, %this, %client, %tag)
{
	commandToClient(%client, 'statUpdateHud');
	%sp = fetchdata(%client, "SP");
	commandToClient(%client, 'statSetSP', %sp);
	%taggedskill[fetchdata(%client, "TagSkill1")] = "Tagged";
	%taggedskill[fetchdata(%client, "TagSkill2")] = "Tagged";
	%taggedskill[fetchdata(%client, "TagSkill3")] = "Tagged";
	for(%i = 1; $SkillDesc[%i] !$= ""; %i++)
	{
		%skill = GetPlayerSkill(%client, %i);

		%multi = GetSkillMultiplier(%client, %i);

		%desc = $SkillData[%i];
		%tagged = %taggedSkill[%i];
		
		commandToClient(%client, 'StatAddRow',$SkillDesc[%i],%skill,%desc, %multi, %i, %tagged);
	}

	commandToClient(%client, 'StatDone');

}
function serverCmdStatonIncrease(%client, %skill, %amount)
{
	//Server-side
	if(%amount >=1 && fetchdata(%client, "SP") >= %amount && %skill > 0)
	{
		%askill = GetPlayerSkill(%client, %skill);
		%lvl = fetchData(%client, "LVL");
		if(%lvl > 101 && !isTaggedSkill(%client, %skill)) %lvl = 101;
		%ub = ($skillRangePerLevel * %lvl) + 20;
		for ( %i = 1; %askill + (%i * GetSkillMultiplier(%client, %skill)) <= %ub && %i <= mfloor(%amount); %i++)
		{
			storeData(%client, "SP", 1, "dec");
			AddSkillPoint(%client, %skill, 1);
		}
		
	}
	RPGstatScreen::updateHud(1,%client, 'RPGstatScreen');
	
}
function RPGGame::TagOnSelect(%game, %client, %id)
{
//get client level.
	%remove = true;

	%removed = false;
	%lvl = fetchdata(%client, "LVL");
	if(%lvl >= 101) %remove = false;
	//first check to see if we are removing any tag skill
	if(%remove)
	{
		if(fetchdata(%client, "TagSkill1") == %id)
		{
			storedata(%client, "TagSkill1", "");
			commandToClient(%client, 'RemoveTag', %id);
			%removed = true;
		}
		if(fetchdata(%client, "TagSkill2") == %id)
		{
			storedata(%client, "TagSkill2", "");
			commandToClient(%client, 'RemoveTag', %id);
			%removed = true;
		}
		if(fetchdata(%client, "TagSkill3") == %id)
		{
			storedata(%client, "TagSkill3", "");
			commandToClient(%client, 'RemoveTag', %id);
			%removed = true;
		}
	}
	if( !%removed &&  fetchdata(%client, "TagSkill1") != %id && fetchdata(%client, "TagSkill2") != %id && fetchdata(%client, "TagSkill3") != %id  )
	{
		if(fetchdata(%client, "TagSkill1") == 0)
		{
			storedata(%client, "TagSkill1", %id);
			commandToClient(%client, 'AddTag', %id);
			%tagged = true;
		}
		else
		if(fetchdata(%client, "TagSkill2") == 0)
		{
			storedata(%client, "TagSkill2", %id);
			commandToClient(%client, 'AddTag', %id);
			%tagged = true;
		}
		else
		if(fetchdata(%client, "TagSkill3") == 0)
		{
			storedata(%client, "TagSkill3", %id);
			commandToClient(%client, 'AddTag', %id);
			%tagged = true;
		}		
	}
	if(%tagged)
		MessageClient(%client, 'Tagged', "You have tagged skill:" SPC $SkillDesc[%id] @ ".");
	else if(%removed)
		MessageClient(%client, 'RemoveTag', "You have detagged skill:" SPC $SkillDesc[%id] @ ".");
	else if(!%remove && !%tagged)
		MessageClient(%client, 'CantTag', "You cannot remove any Tag skills after level 100 and you cannot assign more than 3 tag skills.");
	else

		MessageClient(%client, 'CantTag', "You cannot tag more than 3 skills.");
	

}
function serverCmdTagOnSelect(%client, %id)
{
	Game.TagOnSelect(%client, %id);
}
function serverCmdOnOpenMenu(%client)
{
	ISGameMenu::updateHud(1, %client, 'ISGameMenu',0);
}
function serverCMDRequestPlayerList(%client)
{
	commandToClient(%client, 'ISGameMenuUpdatePlayer');
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if (!%cl.isAIControlled())
		{
			commandToClient(%client, 'ISPlayerListAddRow', %cl, %cl.rpgname, GetLevel(fetchdata(%cl, "EXP")));

		}
		
	}
	

}

function ISGameMenu::updateHud(%this, %client, %tag, %where)
{
	%client.menuwhere = %where;

	BuildMenu(%client, %where, "");
}
function buildMenu(%client, %where, %check)
{
	game.BuildMenu(%client, %where, %check);
}
function RPGGAME::BuildMenu(%game, %client, %where, %check)
{
	commandToClient(%client, 'ISGameMenuUpdateHud');
	%client.gamecheck = "";
	if(%client.choosingGroup == true)
	{
		for(%i = 0; $group[%i] !$= ""; %i++)
		{
			commandToClient(%client, 'ISMenuAddRow', %i+1, %i + 1 @":" SPC $group[%i]);
		}
	
	}
	else if(%client.choosingclass == true)
	{
		for(%i = 0; $class[%client.chosegroup, %i] !$= ""; %i++)
		{
			commandToClient(%client, 'ISMenuAddRow', %i+1, %i + 1 @":" SPC $class[%client.chosegroup, %i]);
		}	
		commandToClient(%client, 'ISMenuAddRow', %i+1, %i + 1 @":" SPC "Back");
	}
	else if(%check == %client.selclient && %client.selclient !$= "")
	{
		if(%where == 0)
		{
			%client.gamecheck = %check;
			if(%client.selclient == %client)
			{
				commandToClient(%client, 'ISMenuAddRow', 1, "1: View Info");
			}
			else
			{
				commandToClient(%client, 'ISMenuAddRow', 1, "1: " @ (IsInTargetList(%client, %client.selclient) == 1 ? "Remove From Target List" : (%client.targeting != %cl ? "Abort target" : "Add to Target List")));
				commandToClient(%client, 'ISMenuAddRow', 2, "2: " @ (IsInGroupList(%client, %client.selclient) ? "Remove From Group List" : "Add to Group List"));
				commandToClient(%client, 'ISMenuAddRow', 3, "3: View Info");
				commandToClient(%client, 'ISMenuAddRow', 4, "4: " @ (%client.muted[%client.selclient] ? "UnMute" : "Mute" ) );
				if(fetchData(%client, "partyOwned") && (IsInWhichParty(%client.selclient) == %client))
					commandToclient(%client, 'ISMenuAddRow', 5, "5: Kick from Party");
				else if(!IsInWhichParty(%client.selclient) && fetchData(%client, "partyOwned"))
					commandToclient(%client, 'ISMenuAddRow', 5, "5: Invite To Party");
			}
		}
		%client.menuwhere = %where;
		commandToClient(%client, 'ISGameMenuDone');
		return;
	}
	else if(%where == 0)
	{
		commandToClient(%client, 'ISMenuAddRow', 1, "1: Stats...");
		commandToClient(%client, 'ISMenuAddRow', 2, "2: Skill Points...");
		commandToClient(%client, 'ISMenuAddRow', 3, "3: Set LCK to (" @ (fetchData(%client, "LCKconsequence") $= "death" ? "miss" : "death") @ ")");
		commandToClient(%client, 'ISMenuAddRow', 4, "4: Party Options...");
	}
	else if(%where == 4)
	{
		if(%client.inviteparty)
		{
			commandToClient(%client, 'ISMenuAddRow', 1, "1: Join");
			commandToClient(%client, 'ISMenuAddRow', 2, "2: Reject");
			commandToClient(%client, 'ISMenuAddRow', 3, "3: Back");
		}
		else if(fetchData(%client, "partyOwned"))
		{
			commandToClient(%client, 'ISMenuAddRow', 1, "1: Disband Party");
			commandToClient(%client, 'ISMenuAddRow', 2, "2: Back");
		}
		else
		{
			if(!fetchData(%client, "partyOwned") && !(IsInWhichParty(%client) != false))
				commandToClient(%client, 'ISMenuAddRow', 1, "1: Create");
			if((IsInWhichParty(%client) != false) && !fetchData(%client, "partyOwned"))
				commandToclient(%client, 'ISMenuAddRow', 1, "1: Leave");
			commandToclient(%client, 'ISMenuAddRow', 2, "2: Back");
		}
	}
	
	%client.menuwhere = %where;
	commandToClient(%client, 'ISGameMenuDone');
	%client.selclient = "";
}
function serverCmdISmenuOnSelect(%client, %id, %text)
{
	game.onISMenuSelect(%client, %id, %text);
}
function RPGGAME::onISmenuSelect(%game, %client, %id, %text)
{
	if(%client.ignoreclick)
		return;
	%client.ignoreclick = true;
	schedule(100, %client, "unignoreclick", %client);
	%where = %client.menuwhere;
	if(%client.choosinggroup)
	{

		if($group[%id-1] !$= "")
		{
			%client.choosinggroup = false;
			%client.choosingclass = true;
			%client.chosegroup = %id-1;
			storedata(%client, "GROUP", $group[%id-1]);
		}
		else
		{
			%client.choosinggroup = true;
			%client.choosingclass = false;
			%client.chosegroup = "";
			storedata(%client, "GROUP", "");
		}
	}
	else if(%client.choosingclass)
	{
		if(getword(%text, 1) $= "Back")
		{
			%client.choosinggroup = true;
			%client.choosingclass = false;
			%client.chosegroup = "";
		}
		else if ( $class[%client.chosegroup, %id-1] !$= "")
		{
			storedata(%client, "CLASS", $class[%client.chosegroup, %id-1]);
			%client.choosingclass = false;
			%client.chosegroup = "";
			%game = %client.game;
			%client.game = "";
			SetSkillsToMulti(%client);
			commandToClient(%client, 'closeISMenu');
			%client.newplayer = true;
			game.spawnplayer(%client, false);
		}
	
	
	}
	else if(%client.gamecheck == %client.selclient && %client.selclient !$= "")
	{
		if(%where == 0)
		{
			if(%client.selclient.rpgname !$= "")
			if(%client.selclient == %client)
			{
				switch(%id)
				{
					case 1:commandToClient(%client, 'closeISMenu');
						displayGetInfo(%client, %client.selclient);
					default: %client.selclient= "";
					
				}
			}
			else
			{
				switch(%id)
				{
					case 1: if(IsInTargetList(%client, %client.selclient))
						{
							cancel(%client.autoremovetarget);
							RemoveFromTargetList(%client, %client.selclient, false);

						}
						else if(%client.targeting == %client.selclient)
						{
							cancel(%client.scheduletarget);
							%client.ScheduleTarget = "";
							%client.targeting = "";
							messageClient(%client, 'cancelTarget', "You stop considering to target " @ %client.selclient.rpgname);
						}
						else if(!%client.targeting)
						{
							%time = 15 + (fetchdata(%client, "LvL") - fetchdata(%client.selclient, "LVL"));
							%client.ScheduleTarget = schedule(%time*1000, %client,  "AddToTargetList", %client, %client.selclient);
							%client.targeting = %client.selclient;
							messageClient(%client, 'AddTarget', "You will add" SPC %client.selclient.rpgname SPC "to your target list in" SPC %time SPC "seconds.");
							messageClient(%client.selclient, 'Targeted', "" SPC %client.rpgname SPC "will add you to his or her target list in" SPC %time SPC "seconds!");
							%client.autoremovetarget = schedule(300 * 1000, %client, "RemoveFromTargetList", %client, %client.selclient, true);
						}

					case 2: if(IsInGroupList(%client, %client.selclient))
						{
							RemoveFromGroupList(%client, %client.selclient);

						}
						else
							AddToGroupList(%client, %client.selclient);
						%client.selclient= "";
					case 3:	commandToClient(%client, 'closeISMenu');
						 DisplayGetInfo(%client, %client.selclient);
						//%time = 15;
						//%line = "<COLOR:FF0000>Name:<COLOR:FFFFFF>" SPC %client.selclient.rpgname SPC "\n<COLOR:FF0000>Info:<COLOR:FFFFFF>" SPC fetchData(%client.selclient, "PlayerInfo");
						//SendRPGBottomPrint(%client, %time, 2, %line);
					case 4: if(%client.muted[%client.selclient])
							%client.muted[%client.selclient] = false;
						else
							%client.muted[%client.selclient] = true;
						%client.selclient= 0;
					case 5:
						echo(%client.selclient SPC %client);
						if(fetchData(%client, "partyOwned") && (IsInWhichParty(%client.selclient) == %client))
						{
							//kick
							RemoveFromParty(%client, %client.selclient, true);
							%client.selclient.inparty = false;
						}
						else if(!IsInWhichParty(%client.selclient) && fetchData(%client, "partyOwned"))
						{
							//invite
							%client.selclient.inviteparty = %client;
							MessageClient(%client, 'InvitedParty', "You invited" SPC %client.selclient.rpgname SPC "to your party");
							MessageClient(%client.selClient, 'InvitedParty', %client.rpgname SPC "has invited you to join" SPC %client.namebase @ "'s party");
						}
						%client.selclient= "";
					
				}
			}
			%check = false;
		}
	}
	else if(%where == 0)
	{
		switch(%id)
		{
			case 1: commandToClient(%client, 'closeISMenu');
				//%client.BPopen = "";
				%time = 30;
				%rdsl = "<COLOR:FF0000>/";
				%red = "<COLOR:FF0000>";
				%blco = "<COLOR:000000>:";
				%white = "<COLOR:FFFFFF>";
				%brwn = "<COLOR:663300>";
				%yel = "<COLOR:FFFF00>";
				%blu = "<COLOR:000066>";
				%gren = "<COLOR:6699FF>";
				%blk = "<COLOR:000000>";
				%line = "<COLOR:FF0000>HP:<COLOR:FFFFFF>" SPC Game.FetchData(%client, "HP")  @ %rdsl @ %white @  Game.fetchData(%client, "MaxHP") @
					"\n<COLOR:FF0000>Mana:<COLOR:FFFFFF>" SPC Game.FetchData(%client, "Mana")  @ %rdsl @ %white @  Game.FetchData(%client, "MaxMana") @ 
					"\n<COLOR:FF0000>EXP:<COLOR:FFFFFF>" SPC Game.FetchData(%client, "EXP") @ 
					"\n<COLOR:FF0000>LVL:<COLOR:FFFFFF>" SPC Game.fetchData(%client, "LVL") @ 
					"\n<COLOR:FF0000>Next Level:<COLOR:FFFFFF>" SPC GetExp(fetchData(%client, "LVL")+1, %client) - Game.fetchData(%client, "EXP") @ 
					"\n<COLOR:FF0000>LCK:<COLOR:FFFFFF>" SPC Game.Fetchdata(%client, "LCK") @ 
					"\n<COLOR:FF0000>Weight:<COLOR:FFFFFF>" SPC Game.fetchdata(%client, "weight") @ %rdsl @ %white @ Game.FetchData(%client, "MaxWeight") NL 
					"<COLOR:FF0000>Armor:<COLOR:FFFFFF>" SPC Game.FetchData(%client, "DEF") NL
					"<COLOR:FF0000>MDEF:<COLOR:FFFFFF>" SPC Game.AddPoints(%client, 3) @ %blco @ %white @ AddPoints(%client, 19) 
						SPC %brwn @ Addpoints(%client, 22) @ %blco @ %brwn @ AddPoints(%client, 15)
						SPC %red @ Addpoints(%client, 20) @ %blco @ %red @ AddPoints(%client, 13)
						SPC %blu @ Addpoints(%client, 21) @ %blco @ %blu @ AddPoints(%client, 14)
						SPC %yel @ Addpoints(%client, 24) @ %blco @ %yel @ AddPoints(%client, 17)
						SPC %gren @ Addpoints(%client, 25) @ %blco @ %gren @ AddPoints(%client, 18)
						SPC %blk @ Addpoints(%client, 23) @ %blco @ %blk @ AddPoints(%client, 16);
				commandToClient(%client, 'RPGBottomPrint', %time, 9, getsubStr(%line, 0, 255), getsubstr(%line, 255, 255), getsubstr(%line, 255*2, 255), getsubstr(%line, 255*3, 255), getsubstr(%line, 255*4, 255));
				//%client.BPopen = Schedule(60*1000, %client, "CloseBottomPrint", %client);
			case 2: commandToClient(%client, 'closeISMenu');
				commandToClient(%client, 'OpenStatGUI');
				%client.menuwhere = 0;
				return;

			case 3: storedata(%client, "LCKconsequence", (fetchData(%client, "LCKconsequence") $= "death" ? "miss" : "death"));
			case 4: 
			%where = 4;
			
			default: echo("ERROR: " @ %client.rpgname SPC "(" SPC %client SPC ")" SPC "submitted an invalid menu command");

		}
	}
	if(%where == 4)
	{
		switch(%id)
		{
			case 1:
				if(%client.inviteparty)
				{
					//commandToClient(%client, 'ISMenuAddRow', 1, "1: Join");
					//AddToPartyList(%client.inviteparty, %client, true);
					AddToParty(%client.inviteparty, %client);
					%client.inparty = %client.inviteparty;
					%client.inviteparty = false;
				}
				else if(!fetchData(%client, "partyOwned") && !(IsInWhichParty(%client) != false))
				{
					//create
					//%client.partyleader = true;
					//%client.inparty = %client;
					//addToPartyList(%client.inparty, %client, false);
					CreateParty(%client);
				}
				else if(IsInWhichParty(%client) != false && !fetchData(%client, "partyOwned"))
				{
					//commandToclient(%client, 'ISMenuAddRow', 1, "1: Leave");
					RemoveFromParty(%client.inparty, %client, true);
					
					%client.inparty = false;	
				}
				else if(fetchData(%client, "partyOwned"))
				{
					//commandToClient(%client, 'ISMenuAddRow', 2, "2: Disband Party");
					DisbandParty(%client);
				}				
			case 2:
				if(%client.inviteparty)
				{
					%client.inviteparty = false;
					
				}
				else
				%where = 0;
			case 3:	%where = 0;
			
		}
		
	}
	
	BuildMenu(%client, %where, %check);

	//ISGameMenu::updateHud(1, %client, 'ISGameMenu', %where);
}
function unignoreclick(%client)
{
%client.ignoreclick = false;
}
function SendRPGBottomPrint(%client, %time, %line, %text)
{
	commandToClient(%client, 'RPGBottomPrint', %time, %line, getsubStr(%text, 0, 255), getsubstr(%text, 255, 255), getsubstr(%text, 255*2, 255), getsubstr(%text, 255*3, 255), getsubstr(%text, 255*4, 255));
}
function serverCmdClientCloseRPGbottomPrint(%client)
{
	return;
}
function serverCmdClientCloseISMenu(%client)
{
	if(%client.choosinggroup || %client.choosingclass) schedule(1000, 0, "commandToClient", %client, 'OpenISMenu');//force the client to reopen the window...

	ISGameMenu::updateHud(1, %client, %tag,0,0);
}
function serverCmdISPlayerListOnSelect(%client, %id, %text)
{
	%client.selclient = %id;
	BuildMenu(%client, 0, %id);
	return;//nothing for now
}
//----------------------------------------------------------------------------
// MessageHud message handlers
function serverCmdTeamMessageSent(%client, %text)
{
	if(strlen(%text) >= $Host::MaxMessageLen)
		%text = getSubStr(%text, 0, $Host::MaxMessageLen);

	RPGchat(%client, 1, %text);

	//chatMessageTeam(%client, %client.team, '\c3%1: %2', %client.name, %text);
}

//------------------------------------------------------------------------------
function serverCmdMessageSent(%client, %text)
{
	if(strlen(%text) >= $Host::MaxMessageLen)
		%text = getSubStr(%text, 0, $Host::MaxMessageLen);

	RPGchat(%client, 0, %text);

	//chatMessageAll(%client, '\c4%1: %2', %client.name, %text);
}
function serverCmdShowHud(%client, %tag)
{

   %tagName = getWord(%tag, 1);
   %tag = getWord(%tag, 0);

   messageClient(%client, 'OpenHud', "", %tag);
   switch$ (%tagname)
   {
      case "RPGinventoryScreen":
         %client.numFavsCount = 0;
         %client.shop = false;
         if(!%client.alreadysentinventory)
         RPGinventoryScreen::updateHud(1,%client,%tag);
      case "vehicleHud":
         vehicleHud::updateHud(1,%client,%tag);
      case "scoreScreen":
         updateScoreHudThread(%client, %tag);
      case "ISGameMenu":
         ISGameMenu::updateHud(1, %client, %tag,0,0);
      case "RPGstatScreen":
      	RPGstatScreen::updateHud(1, %client, %tag);
      	 

   }
   //to see if it is the hud menu here that may be the problem
}

function cleanUpHuds()
{
   if($Hud['RPGinventoryScreen'] !$= "")
   {
      for(%lineNum = 0; $Hud['RPGinventoryScreen'].data[%lineNum, 0] !$= ""; %lineNum++)
         for(%i = 0; %i < $Hud['RPGinventoryScreen'].numCol; %i++)
         {
            $Hud['RPGinventoryScreen'].childGui.remove($Hud['RPGinventoryScreen'].data[%lineNum, %i]);
            $Hud['RPGinventoryScreen'].data[%lineNum, %i] = "";      
         }
   }
}

function serverCmdThrow(%client,%data)
{

}

function serverCmdThrowWeapon(%client,%data)
{

	%itemId = fetchData(%client, "weaponInHand");
	%obj = %client.getControlObject();
	%obj.Throw(%itemId);
}

function serverCmdThrowPack(%client,%data)
{

}

function ShapeBase::use(%this, %itemId)
{
	
	%data = Game.GetModelName(%this.client, %itemId);

	if(%data !$= "")
	{
	
		%this.mountImage(%data.image, $WeaponSlot);
		storeData(%this.client, "weaponInHand", %itemId);


		
		return true;
	}
	else
	{
		DoConsider(%this.client);
		//echo("hi");
		//user is hitting the Pack key,  onconsider here???
	
	}
	return false;
}

function ShapeBase::pickup(%this, %obj, %amount)
{

	%data.onPickup(%obj, %this, %amount);

	return true;
}

function ShapeBase::hasInventory(%this, %data)
{
	// changed because it was preventing weapons cycling correctly (MES)
   return (%this.inv[%data] > 0);
}

function ShapeBase::incInventory(%this, %data, %equipped)
{
	if(%equipped $= "")
		%equipped = false;

	%tmp = AddToInventory(%this.client,1, %data, 3, 1, %equipped);
}

function ShapeBase::decInventory(%this, %data, %amount, %equipped)
{
	%tmp = RemoveFromInventory(%this.client, %data, %amount, %equipped);
}

function ShapeBase::setInventory(%this, %data, %value)
{

	return 0;
}

function ShapeBase::getInventory(%this, %data)
{
	if(%data)
	return %this.inv[%data.getName()];
}

function ShapeBase::hasAmmo( %this, %weapon )
{
	return true;

   switch$ ( %weapon )
   {
      case Blaster:
         return( true );
      case Plasma:
         return( %this.getInventory( PlasmaAmmo ) > 0 );
      case Chaingun:
         return( %this.getInventory( ChaingunAmmo ) > 0 );
      case Disc:
         return( %this.getInventory( DiscAmmo ) > 0 );
      case GrenadeLauncher:
         return( %this.getInventory( GrenadeLauncherAmmo ) > 0 );
      case SniperRifle:
			return( %this.getInventory( EnergyPack ) );
      case ELFGun:
         return( true );
      case Mortar:
         return( %this.getInventory( MortarAmmo ) > 0 );
      case MissileLauncher:
         return( %this.getInventory( MissileLauncherAmmo ) > 0 );
      case ShockLance:
         return( true );
      // This is our kinda hacky way to keep from cycling to the targeting laser:
      case TargetingLaser:
         return( false );
      default:
         warn( "\"" @ %weapon @ "\" is not recognized as a weapon!" );
         return( false );
   }
}

function ShapeBase::throw(%this, %itemId)
{
	game.ShapeBaseThrow(%this, %itemid);

}

function ShapeBase::throwItem(%this, %itemId)
{
	Game.ShapeBasethrowItem(%this, %itemId);
	
}

function ShapeBase::throwObject(%this,%obj)
{

   //-------------------------------------------
   // z0dd - ZOD, 5/27/02. Fixes flags hovering
   // over friendly player when collision occurs
   if(%obj.getDataBlock().getName() $= "Flag")
      %obj.static = false;
   //-------------------------------------------

   //if the object is being thrown by a corpse, use a random vector
   if (%this.getState() $= "Dead")
   {
      %vec = (-1.0 + getRandom() * 2.0) SPC (-1.0 + getRandom() * 2.0) SPC getRandom();
      %vec = vectorScale(%vec, 10);
   }

   // else Initial vel based on the dir the player is looking
   else
   {
      %eye = %this.getEyeVector();
      %vec = vectorScale(%eye, 20);
   }

   // Add a vertical component to give the item a better arc
   %dot = vectorDot("0 0 1",%eye);
   if (%dot < 0)
      %dot = -%dot;
   %vec = vectorAdd(%vec,vectorScale("0 0 8",1 - %dot));

   // Add player's velocity
   %vec = vectorAdd(%vec,%this.getVelocity());
   %pos = getBoxCenter(%this.getWorldBox());

   //since flags have a huge mass (so when you shoot them, they don't bounce too far)
   //we need to up the %vec so that you can still throw them...
   if (%obj.getDataBlock().getName() $= "Flag")
      %vec = vectorScale(%vec, 40);

   //
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos,%vec);
   %obj.setCollisionTimeout(%this);
   %data = %obj.getDatablock();
   %data.onThrow(%obj,%this);

   //call the AI hook
   AIThrowObject(%obj);
}

function ShapeBase::cycleWeapon(%this, %data)
{
	Game.ShapeBasecycleWeapon(%this, %data);
}

function serverCmdTogglePack(%client,%data)
{
	
	return;

   %client.getControlObject().togglePack();
}

   
//------------------------------------------------------------------------------
function RPGInventoryScreen::updateHud(%this, %client, %tag)
{

	commandToClient(%client, 'InventoryUpdateHud');
	//Server-side
	%inv = fetchData(%client, "inventory");
	for(%i = 0; (%itemId = GetWord(%inv, %i)) !$= ""; %i++)
	{
		%fullitem = GetFullItemName(%itemId);
		%type = $itemType[GetItem(%itemId)];
		commandToClient(%client, 'InventoryAddRow', %itemId, %fullitem, %type, %itemId.shapeFile);
	}
	
	commandToClient(%client, 'InventoryDone');
	commandToClient(%client, 'SetCoins', fetchdata(%client, "COINS"));
	%client.alreadysentinventory = true;
}
function serverCmdRequestInvList(%client)
{
	RPGInventoryScreen::updateHud('RPGInventoryScreen', %client);
}


function serverCmdInventoryListOnSelect(%client, %itemId)
{
	game.inventoryListOnSelect(%client, %itemid);

}


//------------------------------------------------------------------------------

function serverCmdInventoryDrop(%client, %itemId)
{
	game.InventoryDrop(%client, %itemid);

}

//------------------------------------------------------------------------------

function serverCmdInventoryUse(%client, %itemId)
{
	game.InventoryUse(%client, %itemid);
}

//------------------------------------------------------------------------------
function RPGGame::UsePotion(%Game, %client, %itemid)
{
	%prevhp = fetchdata(%client, "HP");
	%item = %game.GetItem(%client, %itemid);
	%prefix = %game.GetPrefix(%client, %itemid);
	%suffix = %game.GetSuffix(%client, %itemid);
	%addHp = %game.AddItemSpecificPoints(%itemid, 4, %client);

	if(%addHP)
	{
		%client.phealtick += 30;//(10 ticks a second)
		%client.hamt += %addhp;
		
		if(!IsEventPending(%client.healevent))
			%client.healevent = %game.schedule(100, "PotionHeal", %client, %prevhp);
	}
	
	%addMana = %game.AddItemSpecificPoints(%itemid, 5, %client);
	//modifyhere #1
	//setHP(%client, fetchData(%client, "HP")+%addhp, true);
	setMana(%client, fetchData(%client, "MANA")+%addMana, true);
	%Game.RemoveFromInventory(%client, 1, %item, %prefix, %suffix);
	//ClearInvInfo(%itemId);
	%newhp = fetchdata(%client, "HP");
	if(%prevhp < %newhp)
		UseSkill(%client, $Skill::Healing, true, true, 10);
	//RPGInventoryScreen::updateHud(1, %client, 'RPGInventoryScreen');	
}
function RPGGame::PotionHeal(%game, %client, %prevhp)
{
	if(%client.phealtick == 0) return;
	%amt = mfloor(%client.hamt / %client.phealtick);
	if(%amt > %client.hamt) %amt = %client.hamt;

	setHP(%client, fetchdata(%client, "HP")+%amt);
	%client.hamt -= %amt;
	
	if(( %client.phealtick % 30 ) == 0)
	{
		%newhp = fetchdata(%client, "HP");
		if(%prevhp < %newhp)
			UseSkill(%client, $Skill::Healing, true, true, 10);
		%prevhp = %newhp;
	}
	%client.phealtick -= 1;
	if(%client.hamt == 0) %client.phealtick = 0;
	if(%client.phealtick > 0)
	{
		%client.healevent = %game.schedule(100, "PotionHeal",  %client, %prevhp);
	}
	
}
function serverCmdSelectWeaponSlot( %client, %data )
{
	if(fetchdata(%client, "QuickBind" @ %data) !$= "")
		RPGchat(%client, 1, fetchdata(%client, "QuickBind" @ %data));
}

//------------------------------------------------------------------------------

function RPGInventoryScreen::onWake(%this)
{
   if ( $HudHandle['inventoryScreen'] !$= "" )
      alxStop( $HudHandle['RPGInventoryScreen'] );
   alxPlay(HudInventoryActivateSound, 0, 0, 0);
   $HudHandle['inventoryScreen'] = alxPlay(HudInventoryHumSound, 0, 0, 0);

   if ( isObject( hudMap ) )
   {
      hudMap.pop();
      hudMap.delete();
   }
   new ActionMap( hudMap );
   hudMap.blockBind( moveMap, toggleScoreScreen );
   hudMap.blockBind( moveMap, toggleCommanderMap );
   hudMap.bindCmd( keyboard, escape, "", "RPGInventoryScreen.onDone();" );
   hudMap.push();
}

//------------------------------------------------------------------------------
function RPGInventoryScreen::onSleep()
{
	hudMap.pop();
	hudMap.delete();   
	alxStop($HudHandle['inventoryScreen']);
	alxPlay(HudInventoryDeactivateSound, 0, 0, 0);
	$HudHandle['inventoryScreen'] = "";
}

//------------------------------------------------------------------------------


function serverCmdStartNewVote(%client, %typeName, %arg1, %arg2, %arg3, %arg4, %playerVote)
{
 	return false;      
}

function resetVotePrivs( %client )
{
	return false;
}

function serverCmdSetPlayerVote(%client, %vote)
{
	return false;
}

function calcVotes(%typeName, %arg1, %arg2, %arg3, %arg4)
{
	return false;
}

// Tournament mode Stuff-----------------------------------

function setModeFFA( %mission, %missionType )
{
	return false;
}

//------------------------------------------------------------------

function setModeTournament( %mission, %missionType )
{
	return false;
}
function serverCmdClientTeamChange( %client )
{
	return false; //DOH!
}
function serverCmdCannedChat( %client, %command, %fromAI )
{
   %cmdCode = getWord( %command, 0 );
   %cmdId = getSubStr( %cmdCode, 1, strlen( %command ) - 1 );
   %cmdString = getWord( %command, 1 );
   if ( %cmdString $= "" )
      %cmdString = getTaggedString( %cmdCode );

   if ( !isObject( $ChatTable[%cmdId] ) )
   {
      error( %cmdString @ " is not a recognized canned chat command." );
      return;
   }

   %chatItem = $ChatTable[%cmdId];
   
   //if there is text
   if (%chatItem.text !$= "" || !%chatItem.play3D)
   {
      %message = %chatItem.text @ "~w" @ %chatItem.audioFile;

      if ( %chatItem.teamOnly )
         cannedChatMessageTeam( %client, %client.team, '\c3%1: %2', %client.name, %message, %chatItem.defaultKeys );
      else
         cannedChatMessageAll( %client, '\c4%1: %2', %client.name, %message, %chatItem.defaultKeys );
   }

   //if no text, see if the audio is to be played in 3D...
   else if ( %chatItem.play3D && %client.player )
      playTargetAudio(%client.target, addTaggedString(%chatItem.audioFile), AudioClosest3d, true);

   if ( %chatItem.animation !$= "" )
      serverCmdPlayAnim(%client, %chatItem.animation);

	//removed - hopefully this will fix the vvh cheat
   // Let the AI respond to the canned chat messages (from humans only)
   //if (!%fromAI)
   //   CreateVoiceServerTask(%client, %cmdCode);
}

function ShapeBase::Play3D(%obj, %sound)
{
	serverPlay3D(%sound, %obj.getTransform());
}
function serverCmdResetControlObject(%cleint)
{
	return false;
}
function RPGGame::getServerStatusString(%game)
{
   %status = %game.numTeams;
   for ( %team = 1; %team - 1 < %game.numTeams; %team++ )
   {
      %score = isObject( $teamScore[%team] ) ? $teamScore[%team] : 0;
      %teamStr = getTaggedString( %game.getTeamName(%team) ) TAB %score;
      %status = %status NL %teamStr;
   }

   %status = %status NL ClientGroup.getCount();
   for ( %i = 0; %i < ClientGroup.getCount(); %i++ )
   {
      %cl = ClientGroup.getObject( %i );
      //%score = %cl.score $= "" ? 0 : %cl.score;
      if(%cl.isaicontrolled())
      	continue;
      	
      %playerStr = getTaggedString( %cl.name ) TAB getTaggedString( %game.getTeamName(%cl.team) ) TAB fetchdata(%cl, "LVL");
      %status = %status NL %playerStr;
   }
   return( %status );
}

