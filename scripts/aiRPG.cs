
function AIConnection::onAIConnect(%client, %name, %team, %skill, %offense, %voice, %voicePitch)
{
	if ($DebugMode) echo("AIConnection::onAIConnect(" @ %client @ ", " @ %name @ ", " @ %team @ ", " @ %skill @ ", " @ %offense @ ", " @ %voice @ ", " @ %voicePitch @ ")");

	// Sex/Race defaults
	//%client.sex = "Male";
	//%client.race = "Orc";
	//%client.armor = "Light";
	//storedata(%client, "RACE", %client.sex @ %client.race);

	//setup the voice and voicePitch
	if (%voice $= "" && %name !$= "fish")
		%voice = "Derm2";
	%client.voice = %voice;
	%client.voiceTag = addTaggedString(%voice);
   
	if (%voicePitch $= "" || %voicePitch < 0.5 || %voicePitch > 2.0)
		%voicePitch = 1.0;
	%client.voicePitch = %voicePitch;

	if(getRandom() > 0.5)
		%client.skin = addTaggedString("basebot");
	else
		%client.skin = addTaggedString("basebbot");

	%client.name = addTaggedString( "\cp\c9" @ %name @ "\co" );
	%client.nameBase = %name;

	//echo(%client.name);
	//echo("CADD: " @ %client @ " " @ %client.getAddress());
	//$HostGamePlayerCount++;

	//setup the target for use with the sensor net, etc...
 
 
    // console spam fix:
    if ($DebugMode)
    {
    	echo("calling allocclienttarget with these vars:");
    	echo("%client: " @ %client);
    	echo("%client.name: " @ %client.name);
    	echo("%client.voiceTag: " @ %client.voiceTag);
    }
 
	%client.target = allocClientTarget(%client, %client.name, "", %client.voiceTag, '_BOTConnection', 0, 0, %client.voicePitch);//_ClientConnection
   //lets see if this removes the client from the server window =/
	//messageAllExcept(%client, -1, 'MsgClientJoin', "", %name, %client, %client.target, true);

	//set the initial team - Game.assignClientTeam() should be called later on...
	%client.team = %team;

	//assign the skill
	%client.setSkillLevel(100);
	//%client.displayonmasterserver = false;
	//assign the affinity
	%client.offense = true;

	//clear any flags
	%client.stop(); // this will clear the players move state
	%client.clearStep();
	%client.lastDamageClient = -1;
	%client.lastDamageTurret = -1;
	%client.setEngageTarget(-1);
	%client.setTargetObject(-1);
	%client.objective = "";

	//clear the defaulttasks flag
	%client.defaultTasksAdded = false;

	//if the mission is already running, spawn the bot
	if($missionRunning)
		%client.startMission();
}

function AIConnection::startMission(%client)
{
	if ($DebugMode) echo("AIConnection::startMission(" @ %client @ ")");

	%client.matchStartReady = true;

	//spawn the bot...
	onAIspawn(%client);
}

function AIConnection::onAIDrop(%client)
{
	if ($DebugMode) echo("AIConnection::onAIDrop(" @ %client @ ")");

	//make sure we're trying to drop an AI
	if(!isObject(%client) || !%client.isAIControlled())
		return;
	AIUnassignClient(%client);
	%client.clearTasks();
	%client.clearStep();
	
	if($numAIperSpawnPoint[%client.mySpawnPoint]>0)
	{
		$numAIperSpawnPoint[%client.mySpawnPoint]--;
	}//release the spawnpoint number
	//clear the ai from any objectives, etc...

	aiReleaseHumanControl(%client.controlByHuman, %client);

	//kill the player, which should cause the Game object to perform whatever cleanup is required.
	if(isObject(%client.player) && $missionrunning)
	{
		%client.player.scriptKill(0);
	}

	//do the nav graph cleanup
	%client.missionCycleCleanup();
	//%client.delete();
	//RPG stuff

}

function AIConnection::endMission(%client)
{
	//cancel the respawn thread, and spawn them manually
	cancel(%client.respawnThread);
	cancel(%client.objectiveThread);
}

function onAIspawn(%client)
{
return 0;//bleh
}
function RPGGame::onAIEnterLiquid(%game, %data, %player, %type)
{
	%player.client.wet = true;
	if(%player.client.targetpos)
	%player.client.stepMove(%player.client.targetPos, 0, 3);
}
function RPGGame::onAILeaveLiquid(%game, %data, %player, %type)
{
	%player.client.wet = false;
	if(%player.client.targetpos)
	%player.client.stepMove(%player.client.targetPos, 0, 3);
}
function RPGGame::onAIDoDamage(%game, %clvictim, %bot, %damagetype, %sourceobject)
{
	if(!IsSameRace(%clVictim, %bot))
	%bot.lastAttackCounter = %bot.AttackCounter;
}

function RPGGame::onAIDamaged(%game, %clVictim, %clAttacker, %damageType, %sourceObject)
{

	if(%clVictim.isAiControlled())
	StartDefendSelf(%clVictim, %clAttacker);
	if(!IsSameRace(%clVictim, %clAttacker))
	%clVictim.AttackCounter++;
}

function RPGGame::onAIFriendlyFire(%game, %clVictim, %clAttacker, %damageType, %sourceObject)
{
}

function RPGGame::onAIKilled(%game, %clVictim, %clKiller, %damageType, %implement)
{
	if($debugMode == true) 
		echo("RPGGame::onAIKilled(" SPC %game SPC "," SPC %clvictim SPC "," SPC %clKiller SPC "," SPC %damagetype SPC "," SPC %implement SPC ") -aiRPG.cs");
	%ai = %clVictim;
	%ai.stop();
	%ai.clearTasks();
	%ai.clearStep();
	%ai.lastDamageClient = -1;
	%ai.lastDamageTurret = -1;
	%ai.shouldEngage = -1;
	%ai.lastAttackCounter = 0;
	%ai.AttackCounter = 0;
	%ai.CountStuck = 0;
	%ai.setEngageTarget(-1);
	%ai.setTargetObject(-1);
	storedata(%ai, "guardzone", 0);
	if(%ai.wandertaskid != 0)
	{
		%ai.wandertaskid.delete();
		%ai.wandertaskid = 0;
	}
	if(%ai.attacktaskid != 0)
	{
		%ai.attacktaskid.delete();
		%ai.attacktaskid = 0;
	}
	if(%ai.weapontaskid != 0)
	{
		%ai.weapontaskid.delete();
		%ai.weapontaskid = 0;
	}

	$numAIperSpawnPoint[%clVictim.mySpawnPoint]--;
	%ai.defendself = 1;
	EndDefendSelf(%ai);
	%ai.player = 0;
	$aistack[fetchData(%ai,"SpawnIndex")].push(%ai);//push onto stack so we wont have to reconnect a NEW ai each time
	return;
}

function RPGGame::onAIKilledClient(%game, %clVictim, %clAttacker, %damageType, %implement)
{
	%clAttacker.setVictim(%clVictim, %clVictim.player);
	%clattacker.defendself = 1;
	EndDefendSelf(%clattacker);
	
}

function RPGGame::AIInit(%game)
{
	AIInit();
}

function AIFindClosestEnemy(%srcClient, %radius, %losTimeout)
{
	//see if there's an enemy near our defense location...
	if (isObject(%srcClient.player))
		%srcLocation = %srcClient.player.getWorldBoxCenter();
	else
		%srcLocation = "0 0 0";

	return AIFindClosestEnemyToLoc(%srcClient, %srcLocation, %radius, %losTimeout, false, true);
}

function AIFindClosestEnemyToLoc(%srcClient, %srcLocation, %radius, %losTimeout, %ignoreLOS, %distFromClient)
{
	if(%ignoreLOS $= "")
		%ignoreLOS = false;
	if(%distFromClient $= "")
		%distFromClient = false;

	%count = ClientGroup.getCount();
	%closestClient = -1;
	%closestDistance = 32767;
	for(%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);

		//make sure we find someone who's alive
		if(AIClientIsAlive(%cl) && !IsSameRace(%cl, %srcClient))
		{
			//make sure we don't do extra LOS commands on players who are guaranteed to be out of LOS range
			if(vectorDist(%cl.player.getPosition(), %srcLocation) <= %radius)
			{
				//make sure the client can see the enemy
				if(%cl.sleepMode > 0)
				{
					%cast = containerRayCast(%cl.player.getWorldBoxCenter(), %srcClient.player.getWorldBoxCenter(), $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType, 0);
					if(!%cast)
						%hasLOS = true;
				}
				else
					%hasLOS = %srcClient.hasLOSToClient(%cl);
					
				if(fetchdata(%cl, "invisible"))
					continue;
				//%losTime = %srcClient.getClientLOSTime(%cl);
				if(%haslos || %ignorelos)
				{
			
					%testPos = %cl.player.getWorldBoxCenter();
					if(%distFromClient)
						%distance = %srcClient.getPathDistance(%testPos);
					else
						%distance = AIGetPathDistance(%srcLocation, %testPos);

					if(%distance > 0 && (%radius < 0 || %distance < %radius) && %distance < %closestDistance)
					{
						%closestClient = %cl;
						%closestDistance = %distance;
					}

				}
			}
		}
	}

	return %closestClient SPC %closestDistance;
}

//---------------------------------------------------------------------------
function StartDefendSelf(%client, %attacker)
{
	if(%client.hasattacktask && !IsSameRace(%client, %attacker))// client must have an attack task...
	{
		%client.overrideattacktask = true;//override the attack and wander tasks!
		%client.overridewandertask = true;
		%client.setEngageTarget(%attacker.player);
		%client.setTargetObject(%attacker.player, 0, "Destroy");
		%client.defendself++;
		%client.currenttarget = %attacker;
		%weaponrange = Game.getRange(Game.GetItem(fetchdata(%client, "weaponinhand")));
		%client.stepMove(%attacker.player.getPosition(), %weaponrange , $AIModeWalk);
		schedule(60*1000, %client, "EndDefendSelf", %client);

		
	}
}
function EndDefendSelf(%client)
{
	%client.defendself--;
	if(%client.defendself <= 0)
	{
		//client hasnt been hit for over a min, cancel chase.
		%client.defendself = 0;
		%client.overrideattacktask = false;
		%client.overridewandertask = false;
	}
}
function AIRPGAttackTask::init(%task, %client)
{
	%client.monitorTicker = "";
	%client.attacktaskid = %task;
	
}

function AIRPGAttackTask::assume(%task, %client)
{
	//%task.setWeightFreq(40);
	%min = 40;
	%max = 80;
	%freq = mfloor((getRandom() * (%max - %min)) + %min);
	%task.setMonitorFreq(%freq);
	%client.hasattacktask = true;
}

function AIRPGAttackTask::retire(%task, %client)
{

}

function AIRPGAttackTask::weight(%task, %client)
{
	%player = %client.player;

	%task.setWeight(2000);
}

function AIRPGAttackTask::monitor(%task, %client)
{
	%player = %client.player;
	%player.attackThread = "";

	
		
	%losrange = 40;
	%afce = AIFindClosestEnemy(%client, %losrange, 15000);

	%targetId = firstWord(%afce);
	%targetDist = GetWord(%afce, 1);

	if(%targetId !$= -1)
	{
		%client.lastTimeSpotEnemy = getTime();
		
		//time to do a check for the bots in guard mode
		if(fetchdata(%client, "attb") == 1)
		{
			
			if( fetchdata(%client, "guardzone") !$= fetchdata(%targetid, "zone") )
			{
				%targetid = -1;
			}
		}
	}
	if(%client.overrideattacktask && %client.currenttarget > 0)
	{

	%targetid = %client.currenttarget;

	}


	%clientPos = %client.player.getPosition();
	%wrange = Game.GetRange(%client, fetchData(%client, "weaponInHand"));
	%wdelay = Game.GetDelay(%client, fetchData(%client, "weaponInHand"));
	
	%tolerance = %wrange;
	
	
	if(%targetId !$= -1 && isobject(%targetid.player))
		%targetPos = %targetId.player.getPosition();
	else
		%targetPos = -1;

	//if targetId is -1, it will cancel these tasks out
	%client.setEngageTarget(%targetId.player);
	%client.setTargetObject(%targetId.player, 0, "Destroy");
	%itemid = fetchdata(%client, "weaponinhand");
	%dt = $ItemDamageType[Game.getItem(%client, %itemId)];
	if(%client.trapped)
		%wrange *= 5;
//check if using magic as a main way of attacking
	if(%targetId !$= -1)
	{
	
		if(!fetchdata(%client, "magic"))
		{
			%weapon = (%client.player.getMountedImage($WeaponSlot) == 0) ? "" : %client.player.getMountedImage($WeaponSlot);
			%ammo = $itemAmmo[Game.getItem(%itemid)];
			if(%dt == $DamageType::Archery)
			{
				%ammo = $itemAmmo[Game.getItem(%itemid)];
				if(%client.invcount[%ammo] <= 0)
					serverCmdCycleWeapon(%client, "next");//no ammo switch weapon again.
				%wrange = %wrange / 3;
			}
			//if( %targetDist <= %wrange && (%client.lastfire +%wdelay < getSimTime()))
			//{
			//	schedule(250,0,"doaifire", %weapon, %player);
			//	%client.lastfire = getSimTime();
			//}
			//randomize target position by tolerance (%wrange).

					
			%targetpos = (GetWord(%targetPos, 0) + %wrange * (getrandom()-0.5)) SPC (GetWord(%targetPos, 1) + %wrange * (getrandom()-0.5)) SPC (GetWord(%targetPos, 2) + %wrange * (getrandom()-0.5));
			
			%hd = GetWord(%targetpos, 2) - GetWord(%client.player.getposition(), 2);
			if(%targetid.player)
			{
				%vel = %targetid.player.getVelocity();
				%targetpos = vectoradd(%targetpos, %vel);//should make better bots when chasing.
			}
			if(%client.wet)
			{
			  %nvec =  VectorSub( %targetid.player.getPosition(), %client.player.getPosition()) ;
			 
			  %targetpos = VectorAdd(%nvec, VectorAdd(%nvec, %targetpos));
			}
		
			
			if(%client.trapped)
			%mode = $AIModeExpress;
			else if (%client.wet)
			 if(%hd > 15)
			  %mode =  $AIModeGainHeight;
			 else
			  %mode = $AiModeExpress;
			else
			%mode = $AIModeWalk;
			if(%client.trapped)
			{
				if(getRandom(0,1))
					%targetpos = %client.player.getPosition();
			}	

			%client.stepMove(%targetPos, 0, %mode);
			%client.targetpos = %targetpos;
			%client.overridewandertask = true;
			%client.countStuck = 2;//backup for one step and pounce back. should be better. 
			%client.trapped = false;
		}
		else
		{
			//see if you have LOS to the target... if los is aquired FIRE else move
			
			%client.overridewandertask = true;
			%haslos = newhasLOStoclient(%client, %targetid);
			
			if(%haslos)
			{
				//allowed spells for bots
				%spell[1] = "thorn";
				%spell[2] = "fireball";
				%spell[3] = "icespike";
				%spell[4] = "icestorm";
				%spell[5] = "spikes";
				%spell[6] = "melt";
				%spell[7] = "cloud";
				%spell[8] = "powercloud";
				%spell[9] = "beam";
				%spell[10] = "hellstorm";
				%spell[11] = "snowstorm";
				%spell[12] = "dimensionrift";
				%spell[13] = "ironfist";
				for(%i = 0; %i < 3; %i++)
				{
					%d = getRandom(1,10);
					%spell = %spell[%d];
					if(SkillCanUse(%client, %spell))
					{
						break;
					}
				}
				if(!SkillCanUse(%client, %spell)) %spell = "thorn";
				//FIRE!
				RPGchat(%client, 0, "#cast " @ %spell);
				%client.stepmove(%targetpos, 30, $AIModeExpress);
			}
			else
			{
				%hd = GetWord(%targetpos, 2) - GetWord(%client.player.getposition(), 2);
				if(%client.trapped)
				%mode = $AIModeExpress;
				else if (%client.wet)
				 if(%hd > 15)
				  %mode =  $AIModeGainHeight;
				 else
				  %mode = $AiModeExpress;
				else
				%mode = $AIModeWalk;
				if(%client.trapped)
				{
					if(getRandom(0,4))
						%targetpos = %client.player.getPosition();
				}
				%targetpos = (GetWord(%targetPos, 0) + %wrange * (getrandom()-0.5)) SPC (GetWord(%targetPos, 1) + %wrange * (getrandom()-0.5)) SPC (GetWord(%targetPos, 2) + %wrange * (getrandom()-0.5));

				%client.stepmove(%targetpos, 0, %mode);//walk but keep distance!
				%client.targetpos = %targetpos;
			}
			
		}
		
		//are we hitting our target????
		if(%client.lastAttackCounter >= %client.AttackCounter && %client.checkCounter >= %client.attackCounter)
		{
			// yes
			
			%client.countStuck = 0;
			%client.trapped = false;
		}
		else
		{
		%client.checkCounter = %client.attackCounter;
		%client.countStuck++;
		}
		if(%client.countStuck > 2)
		{
			//echo(%client SPC "IS TRAPPED!!!" SPC %client.namebase);
			%client.trapped = true;
			%client.trappcounter++;
			if(%client.trappcounter > 10)
			%client.lastAttackCounter = %client.attackCounter;//break the running around like a madman.
		}
	}
	else
		if(!%client.overrideattacktask)
			%client.overridewandertask = false;
	

	%client.lastPos = %clientPos;
}
function doaifire(%weapon,%player)
{

	ShapeBaseImageData::onFire(%weapon, %player, $weaponslot);

}

//----
function AIRPGWanderTask::init(%task, %client)
{
	%client.wandertaskid = %task;
}

function AIRPGWanderTask::assume(%task, %client)
{
	%client.lastStuckPos = %client.player.getPosition();

	%min = 50;
	%max = 120;
	%freq = mfloor((getRandom() * (%max - %min)) + %min);

	%task.setWeightFreq(%freq);
}

function AIRPGWanderTask::retire(%task, %client)
{
%client.stepIdle();
}

function AIRPGWanderTask::monitor(%task, %client)
{
	%player = %client.player;

	%task.setWeight(900);
}

function AIRPGWanderTask::weight(%task, %client)
{
	if(!shouldAIBeAt(%client.player.position))
	{
		if(%client.despawntime == 0)
			%client.despawntime = getSimTime() + 30000;
			
		if(getSimTime() > %client.despawntime)
		{
			%client.deSpawn();
		}
	}
	else
		%client.despawntime = 0;
	
	if(%client.overridewandertask == true)
	return; //overridden for now. This should result in smarter bots.
	%player = %client.player;

	if(%client.getEngageTarget() != -1)
		return;

	%clientPos = %client.player.getPosition();

	//until i figure out more of this AI, use the stepEngage line if you want the bot to avoid the player,
	//or use the stepMove line to make the bot attack the player head-on.  Make sure that the skill
	//level is set to 10 in order for the bots to turn fast enough.

	//something to think about: once i can get AI to damage others, then it would be a good idea to keep the last
	//time of damage, and if the bot sticks around a player without getting any damage thru to its target for too
	//long, then make the bot run away somewhere else.

	%minrad = 5;
	%maxrad = 30;

	%tolerance = %maxrad;

	%m = VectorDist(%clientPos, %client.lastPos);
	if(%m <= 0.4)
	{
		//the bot might be stuck against something, take proper action



		if(%client.stuckCounter $= "")
		{
			%distTravelled = vectorDist(%client.player.getPosition(), %client.lastStuckPos);



			%client.stuckCounter = 0;
			%min = Cap(mfloor(%distTravelled / 40), 1, 500);
			%max = %min + 5;
			%client.stuckCounterLimit = mfloor(getRandom() * (%max - %min)) + %min + 1;



			%client.stepIdle(%clientPos);
			%client.stepName = "";
			%client.stepPos = "";
		}

		%client.stuckCounter++;

		if(%client.stuckCounter >= %client.stuckCounterLimit)
		{
	
			//max out tolerance
			%tolerance = 99999;

			//make sure the other conditions aren't blocked by the 2 second jumping around thing
			cancel(%client.TSEsched);
			%client.TempStepEngage = "";

			//stop idling the bot
			%client.clearStep();

			%client.lastStuckPos = %client.player.getPosition();

			%client.stuckCounter = "";
		}
	}

	if(!%client.TempStepEngage)
	{
		%movePos = -1;
		%stepname = %client.getStepName();
		%stepstatus = %client.getStepStatus();
		if(%stepname $= "NONE" && %client.stepName $= "stepMove")
		{
			//limitation to getStepName involving stepMove, workaround follows:
			%stepname = "AIStepMove";
			if(VectorDist(%clientPos, %client.stepNamePos) <= %tolerance)
			{
				%stepname = "NONE";
				%stepstatus = "Finished";
				%client.stepName = "";
				%client.stepPos = "";
			}
			else
				%stepstatus = "InProgress";
		}

		if(%stepstatus !$= "InProgress")
		{
			
			//Select a random InteriorInstance, and go towards it
			//%interiorId = selectRandomObjectWithRaceID("MissionGroup/Interiors", $RaceID[fetchData(%client, "RACE")]);
			//%originalPos = %interiorId.position;

			 %originalPos = %client.player.getPosition();
			 %r = 0;
			 for(%i = 0; $ai::attackPos[$aiattack[%client], %i] !$= ""; %i++)
			 {
			 	%d++;
			 }
			if(%d != 0)
			 %r = %d*getRandom() % %d;
			 if(%r < 0) %r = 0;
			if($Ai::AttackPos[$aiAttack[%client],%r] !$="")
				%originalPos = $Ai::AttackPos[$aiAttack[%client],%r];
		
			%p = randomPositionXY(%minrad, %maxrad);
			%x = firstWord(%p);
			%y = GetWord(%p, 1);
			%ox = firstWord(%originalPos);
			%oy = GetWord(%originalPos, 1);
			%oxx = %ox + %x;
			%oyy = %oy + %y;
			%ozz = getTerrainHeight(%oxx @ " " @ %oyy @ " 0");

			%movePos = %oxx @ " " @ %oyy @ " " @ %ozz;
			if(getRpgRoll("1r3") == 1 )
				%moveMode = $AIModeWalk;
			else
				%movemode = $AIModeExpress;

		}
		else if(%stepname $= "AIStepIdlePatrol")
		{
			//no need to make the bot do a stepMove, a stepIdle has already been issued
			%movePos = -1;
		}
		else
		{
		
			%movePos = %client.stepPos;
			%moveMode = $AIModeWalk;
		}

		if(%movePos !$= -1)
		{
			%client.stepMove(%movePos, %tolerance, %moveMode);
			//%client.setPath(%movePos);

			//getStepName doesn't recognize stepMove (stupid limitation?? dunno)
			//workaround follows: (which turns out to be more powerful because I can determine position)
			%client.stepName = "stepMove";
			%client.stepPos = %movePos;
		}
	}

	%client.lastPos = %clientPos;
}
function RPGGame::clearTempStepEngage(%game, %client)
{
	%client.TempStepEngage = "";
}

//----
function AIRPGSelectWeaponTask::init(%task, %client)
{
	%client.weapontaskid = %task;
}

function AIRPGSelectWeaponTask::assume(%task, %client)
{
	%task.setWeightFreq(100);
}

function AIRPGSelectWeaponTask::retire(%task, %client)
{
}

function AIRPGSelectWeaponTask::monitor(%task, %client)
{
	%player = %client.player;

	%task.setWeight(900);
}

function AIRPGSelectWeaponTask::weight(%task, %client)
{
	%player = %client.player;

	%task.selectWeaponTaskTicker++;

	if(%task.selectWeaponTaskTicker > %task.selectWeaponTaskLimit)
	{
		%min = 20;
		%max = 150;
		%nlimit = mfloor((getRandom() * (%max - %min)) + %min);
		%task.selectWeaponTaskLimit = %nlimit;
		%task.selectWeaponTaskTicker = 0;

		//serverCmdCycleWeapon(%client, "next");//need to change this so the bots discard useless items..
		Game.ShapeBasecycleWeapon(%client.player, "next");
		%itemid = fetchdata(%client, "weaponinhand");
		
		if(%itemid == 0)
		{
			Game.ShapeBasecycleWeapon(%client.player, "next");
			%itemid = fetchdata(%client, "weaponinhand");
			
		}
		if($ItemDamageType[Game.getItem(%client, %itemId)] == $DamageType::Archery)
		{
			%ammo = $itemAmmo[Game.getItem(%client, %itemid)];
			if(%client.data.invcount[%ammo, 3, 1] <= 0)
				Game.ShapeBasecycleWeapon(%client.player, "next");//no ammo switch weapon again.
		}
	}
}
function AIRPGFishWanderTask::init(%task, %client)
{
	%client.wandertaskid = %task;
}

function AIRPGFishWanderTask::assume(%task, %client)
{
	%client.lastStuckPos = %client.player.getPosition();

	%min = 50;
	%max = 120;
	%freq = mfloor((getRandom() * (%max - %min)) + %min);

	%task.setWeightFreq(%freq/2);
}

function AIRPGFishWanderTask::retire(%task, %client)
{
}

function AIRPGFishWanderTask::monitor(%task, %client)
{
	//%player = %client.player;

	%task.setWeight(600);
}

function AIRPGFishWanderTask::weight(%task, %client)
{
	%player = %client.player;
	if(!isobject(%client.player))
	{
		if ($DebugMode) echo("ERROR: Task should not have been called - aiRPG.cs AIRPGFishWanderTask Client:" SPC %client SPC " Playerid:" SPC %client.player SPC "Task:" SPC %task);
		//%task.delete();
		return;
	}
	if(%client.getEngageTarget() !$= -1)
		return;

	%clientPos = %client.player.getPosition();

	//until i figure out more of this AI, use the stepEngage line if you want the bot to avoid the player,
	//or use the stepMove line to make the bot attack the player head-on.  Make sure that the skill
	//level is set to 10 in order for the bots to turn fast enough.


	%minrad = -50;
	%maxrad = 50;

	%tolerance = %maxrad;
	%stuck = false;
	%m = VectorDist(%clientPos, %client.lastPos);
	%d = VectorDist(%clientPos, %client.stepPos);
	if(%m <= 0.5 && %d > 1.5 )
	{
		%unstuckpos = getword(%clientPos, 0)+(getword(%clientPos, 0) - getWord(%client.stepPos, 0)) SPC getword(%clientPos, 1)+(getword(%clientPos, 1) - getWord(%client.stepPos, 1)) SPC getword(%clientPos, 2)+(getword(%clientPos, 2) - getWord(%client.stepPos, 2)); 
		%stuck = true;
		%client.stuckcounter++;
		if(%client.stuckcounter > 2)
		{
			%unstuckpos = "";
			//if(%client.stuckcounter > 30)
			//	%client.player.scriptkill($damagetype::suicide);
		}
		
	}
	else
	{
	%client.unstuckpos = "";
	%client.stuckcounter = 0;
	}

	%movePos = -1;
	%stepname = %client.getStepName();
	%stepstatus = "none";


	 %originalPos = %clientPos;
	if($Ai::AttackPos[$aiAttack[%client],0] !$="")
		%originalPos = $Ai::AttackPos[$aiAttack[%client],0];
	if(%stuck)
	{
		%originalPos = %unstuckPos;
	}
	%p = randomPositionXY(%minrad, %maxrad);
	%x = firstWord(%p);
	%y = GetWord(%p, 1);
	%ox = firstWord(%originalPos);
	%oy = GetWord(%originalPos, 1);
	%oxx = %ox + %x;
	%oyy = %oy + %y;
	%ozz = getTerrainHeight(%oxx @ " " @ %oyy @ " 0");	
	%movePos = %oxx @ " " @ %oyy @ " " @ %ozz +5;
	if(getRpgRoll("1r5") == 1 )
		%moveMode = $AIModeGainHeight;
	else
		%movemode = $AIModeExpress;
	if(%movePos !$= -1)
	{
		%client.stepMove(%movePos, %tolerance, %moveMode);
		//%client.setPath(%movePos);

		//getStepName doesn't recognize stepMove (stupid limitation?? dunno)
		//workaround follows: (which turns out to be more powerful because I can determine position)
		%client.stepName = "stepMove";
		%client.tempmovemode = %moveMode;
		if(!%stuck)
		%client.stepPos = %movePos;
	}


	%client.lastPos = %clientPos;
}
