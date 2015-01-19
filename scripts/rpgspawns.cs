$numspawnpoints = 0;
function createAISpawn(%pos, %box, %mindelay, %maxdelay, %botlist, %team, %attack, %zone)
{
	$AIspawn::spawnPoint[$numspawnpoints] = %pos;//transform..
	$AIspawn::spawnBox[$numspawnPoints] = %box;
	$AIspawn::mindelay[$numspawnpoints] = %mindelay;
	$AIspawn::maxdelay[$numspawnpoints] = %maxdelay;
	$AIspawn::List[$numspawnpoints] = %botlist;
	$AIspawn::Team[$numspawnpoints] = %team;
	$AIspawn::Attack[$numspawnpoints] = %attack;
	$AISpawn::needsSpawn[$numspawnpoints] = 1;
 
// if($debugMode)
 //echo("-spawning("@%pos@","@%box@","@%mindelay@","@%maxdelay@","@%botlist@","@%team@","@%attack %zone@")");
 
 //if($debugMode) echo("spawnbot(" @ %index @ ", " @ %si @ ")");
 
	$numspawnpoints++;
}

function GameConnection::deSpawn(%client)
{
	if(!%client.isAiControlled())
		return;
	
	%client.despawning = true;
	%client.player.setPosition("0 0 -1000");
	%client.player.schedule(500, "delete");
	%client.schedule(10, "drop");
}

function InitSpawnPoints()
{
	if($rules !$="dm")
	{
		DefineLoadouts();
		for(%i = 1; $SpawnIndex[%i] !$= ""; %i++)
		{
			$aistack[%i] = initstack();
		}
		DefineAISpawn();
		for(%i = 0; %i < $numspawnpoints; %i++)
		{
			$numaiperspawnpoint[%i] = 0;
			Game.SpawnLoop(%i);
		}
		echo("Initialized " @ %i @ " spawn points.");
	}
	//aisystemenabled(true);

}

function shouldAIBeAt(%pos)
{
	%dist = 200;
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl.isAIControlled())
			continue;
		
		if(isObject(%cl.player))
		{
			if(VectorDist(%cl.player.getWorldBoxCenter(), %pos) < %dist)
				return true;
		}
	}
	return false;
}

function RPGGame::SpawnLoop(%game, %index)
{
	if($serverallowinfinitespawn)
		$numAIperSpawnPoint[%index] = 0;
	%transform = $AIspawn::SpawnPoint[%index];
	%mindelay = $AIspawn::mindelay[%index];
	%maxdelay = $AIspawn::maxdelay[%index];
	%diff = %maxdelay - %mindelay;
	%delay = mfloor(getRandom() * %diff) + %mindelay;

	if($numaiperspawnpoint[%index] < 0)
		$numaiperspawnpoint[%index] = 0;

	%zone = positionInWhichZone($AIspawn::spawnPoint[%index]);
	if(!shouldAIBeAt($AIspawn::SpawnPoint[%index]))
	{
		%game.schedule(1000, "SpawnLoop", %index);
		return;
	}

	if($spawnMultiplier == 0) %flag = false;

	if($numaiperspawnpoint[%index] == 0)
	{
		//determine %bottype based on indexes

		for(%i = 0; getword($aispawn::list[%index], %i) !$= ""; %i++)
		{
		}

		%randpick = getRPGroll("1r" @ %i);
        // console spam fix
		//echo("%randpick = " @ %randpick @ "| %i = " @ %i);
		%randpick--;
		%randpick = GetWord($aiSpawn::list[%index], %randpick);
		if(%randpick !$= "")
			%client = %Game.SpawnBot(%index, %randpick);

		//spawn successful!
		if(isObject(%client))
		{
			%client.homeZone = %zone;

			if($AIspawn::Attack[%index] > 0)
			{

				$aiAttack[%client] = $AIspawn::Attack[%index];
				//echo("Spawn: Attack pos->" SPC $aiAttack[%client] SPC $AIspawn::Attack[%index]);
			}

			$numAIperSpawnPoint[%index]++;
			%client.mySpawnPoint = %index;
		}
		else
			error("Error spawning bot at spawnPoint index " @ %index);
	}
	//always call back the spawn loop, in case a spot is freed up for a helper to spawn
	%game.schedule(%delay * 1000, "SpawnLoop", %index);
	
}

function RPGGame::SpawnBot(%game, %index, %si)
{
	if($debugMode) echo("spawnbot(" @ %index @ ", " @ %si @ ")");
	%spawnPoint = $AIspawn::SpawnPoint[%index];
	%name = GenerateName(%si);
	%race = $aiRace[$spawnIndex[%si]];
	%raceId = $RaceID[%race];
 
    if($DebugMode) echo("-spawned("@%spawnPoint@","@%name@","@%race@","@%raceId@")");
 
 //if ($DebugMode) echo("spawnTownbot(" @ %pos @ ", " @ %team @ ", " @ %type @ ", " @ %race @ ", " @ %shop @ ", " @ %name @ ")");
 
 //if($debugMode)
 //echo( this.%pos  this.%box  this.%mindelay  this.%maxdelay  this.%botlist  this.%team  this.%attack  this.%zone );
 //echo(
	if($aistack[%si].empty())
	{
		//if the race's stack is empty connect a new AI
		%ai = aiConnect(%name, %si);
		//create its data field
		%ai.data = new ScriptObject();
	}
	else
	{
		
		%ai = $aistack[%si].pop();//take a used ai off the stack for play.
		%ai.data.delete();//whoops RESET player.
		%ai.data = new ScriptObject();
		
	}
	if(isObject(%ai))
	{
		Game.AIHasJoined(%ai);
		
		//bah hardcode this part
		for(%i = 0; Getword($enemyprofile[$SpawnIndex[%si]], %i) !$= ""; %i += 2)
		{
			%w = Getword($enemyprofile[$SpawnIndex[%si]], %i);
			%w2 = Getword($enemyprofile[$SpawnIndex[%si]], %i+1);

			if(%w $= "LVL") 
			{
				%lvl = getRPGroll(%w2);
				storeData(%ai, "tempLVL", %lvl);
				StoreData(%ai, "EXP", (%lvl-1)*1000);
			}
			else if(%w $= "COINS")
			{
				storedata(%ai, "COINS", GetRPGRoll(%w2));
			}
			else if(%w $= "CLASS")
			{
				storeData(%ai, "CLASS", %w2);
				storeData(%ai, "GROUP", $ClassGroup[fetchData(%client, "CLASS")]);
			}
		
		}
		storeData(%ai, "RACE", %race);
		storeData(%ai, "SpawnIndex", %si);
		storedata(%ai, "attb", $attb[$SpawnIndex[%si]]);//attack behavior?
		storedata(%ai, "magic", $bot::magic[$SpawnIndex[%si]]);//can use magic?
		%ai.weight = 0;
		HardcodeAIskills(%ai);
		//GiveThisStuff(%id, $EnemyProfile[%si]);
		%ai.team = $AISpawn::Team[%index];
		//%ai.team = 1;
		if($RaceSkin[%raceid] !$="")
		{
			%client.skin = $raceSkin[%raceid];// should work
			setTargetSkin(%client.target, %client.skin);
		}
		%armor = fetchData(%ai, "RACE") @ "Armor";
		//%armor = "D";
		//echo(%armor);
  
        // console spam fix
		switch$(%armor)
		{
			case "MaleOrcArmor": %fish = false;
			case "MaleHumanArmor":%fish = false;
			case "OrcArmor": %fish = false;
			case "GoblinArmor":%armor = "MaleOrcArmor"; %fish = false;
			case "FishArmor":%fish = true;
			case "GnollArmor": %fish = false;
			case "ElementalArmor": %fish = false;
			case "MaleElfArmor": %armor = "ElfArmor";
			case "ElfArmor": %fish = false;
			case "MinotaurArmor": %fish = false;
			case "OgreArmor": %fish = false;
			default: %armor = "MaleHumanArmor";%fish = false;
		}
		//echo(%armor);
		%ai.armor = %armor;
		//echo(%armor.scale);
		%player = new Player()
		{
			dataBlock = %armor;

		};
		
		%player.setDataBlock(%armor);//lets try this

		%minrad = %spawnpoint @ " 1 0 0 0";
		
		%rad = getword(%spawnpoint, 0) + getrandom() * getword($AIspawn::box[%index],0) SPC getword(%spawnpoint, 1) + getrandom() * getword($AIspawn::box[%index],1) SPC getword(%spawnpoint, 2) + getrandom() * getword($AIspawn::box[%index],2);
		//echo(%spawnloc);
   
   	%c = containerRayCast(%rad, vectorAdd(%rad, "0 0 -100"), $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType, 0);
   
  	%rad = posFromRaycast(%c);
  	
		%player.setTransform(%rad @ " 1 0 0 0");
		%player.startFade(0,0,1);
		MissionCleanup.add(%player);
		%player.setVelocity("0 0 -1");
  
        //*PHANTOM139 : Spawn effect call
        BotSpawnEffects(%player);
        //

		%player.setOwnerClient(%ai);
		%player.team = %ai.team;
		%ai.player = %player;
		%player.startFade(1000,0,0);
		//make sure the player object is the AI's control object - even during the mission warmup time
		//the function AISystemEnabled(true/false) will control whether they actually move...
		%ai.setControlObject(%ai.player);

		//clear the objective and choose a new one
		AIUnassignClient(%ai);
		%ai.stop();
		%ai.clearStep();
		%ai.lastDamageClient = -1;
		%ai.lastDamageTurret = -1;
		%ai.setEngageTarget(-1);
		%ai.setTargetObject(-1);
		%ai.pilotVehicle = false;
		%ai.isfish = %fish;
		//set the spawn time
		%ai.spawnTime = getSimTime();
		%ai.respawnThread = "";
		if(%ai.respawnTimer)
			cancel(%ai.respawnTimer);
		%ai.observerStartTime = "";
		%ai.camera.mode = "";
		// updates client's target info for this player
		UpdateTargetStuff(%ai);
		//--- temp until loadchar comes in
		setHP(%ai, fetchData(%ai, "MaxHP"));
		setMANA(%ai, fetchData(%ai, "MaxMANA"));
		//storeData(%client, "templvl", GetLevel(fetchData(%client, "EXP")));
		//----

		storeData(%ai, "HasLoadedAndSpawned", true);
		//timeslice the objective reassessment for the bots
		if(!isEventPending(%ai.objectiveThread))
		{
			%curTime = getSimTime();
			%remainder = %curTime % 5000;
			%schedTime = $AITimeSliceReassess - %remainder;
			if (%schedTime <= 0)
				%schedTime += 5000;
			%ai.objectiveThread = schedule(%schedTime, %ai, "AIReassessObjective", %ai);

			//set the next time slice "slot"
			$AITimeSliceReassess += 300;
			if ($AITimeSliceReassess > 5000)
				$AITimeSliceReassess -= 5000;
		}

		if(!%fish)
		{
			%ai.rpgAttackTask = %ai.addTask(AIRPGAttackTask);
			%ai.rpgWanderTask = %ai.addTask(AIRPGWanderTask);
			%ai.rpgSelectWeaponTask = %ai.addTask(AIRPGSelectWeaponTask);		

		}
		else
		{
			%ai.rpgFishWanderTask = %ai.addTask(AIRPGFishWanderTask);
			//%ai.voice = "";
			//%ai.voiceTag = "";// lets try this
		}
		$ainumber++;
		%ai.rpgName = %name @ " Bot";
		%ai.namebase = %name;
		%ai.mySpawnPoint = %index;
		//clear the inventory
		ClearInventory(%ai);
		%ai.equiplist = "";
		for(%i = 0; GetWord($EnemyWeapons[%si],%i) !$= ""; %i = %i+3)
		{
			%weapon = GetWord($enemyWeapons[%si],%i);
			
			%pre = GetWord($enemyWeapons[%si],%i+1);
			%suff = GetWord($enemyWeapons[%si],%i+2);
			//echo("%weapon = " @ %weapon SPC "%pre = " @ %pre SPC "%suff =" SPC %suff SPC "%equipped =" SPC (%i == 0));
			AddToInventory(%ai, 1, %weapon, %pre, %suff, (%i == 0));
		}
		for(%i = 0; GetWord($EnemyArmors[%si],%i) !$= ""; %i = %i+3)
		{
			%earmor = GetWord($EnemyArmors[%si],%i);
			%pre = GetWord($EnemyArmors[%si],%i+1);
			%suff = GetWord($EnemyArmors[%si],%i+2);
			AddToInventory(%ai, 1, %earmor, %pre, %suff, false);
		}
		for(%i = 0; GetWord($EnemyItems[%si],%i) !$= ""; %i = %i+3)
		{
			%item = GetWord($EnemyItems[%si],%i);
			%roll1 = GetWord($EnemyItems[%si],%i+1);
			%roll2 = GetWord($EnemyItems[%si],%i+2);
			%roll1 = GetRPGroll(%roll1);
			if(GetRPGroll(%roll2) == 1)
				AddToInventory(%ai, %roll1, %item, 0, 0, false);
		}
		//setup stats etc
		refreshall(%ai);
		//echo(%armor);
		if(%armor $= "ElementalArmor")
		{
			%player.setCloaked(true);
			loopshieldeffect(%player);
		}
	}

	return %ai;
}
function loopshieldeffect(%player)
{
%player.playshieldeffect(getvector(%player));
schedule(1000,%player, loopshieldeffect, %player);
}
function GenerateName(%si)
{

	return $SpawnIndex[%si];
}

function RandomTransform(%trans, %minrad, %maxrad)
{
	%tempPos = RandomPositionXY(%minrad, %maxrad);
	%xPos = GetWord(%tempPos, 0) + GetWord(%trans, 0);
	%yPos = GetWord(%tempPos, 1) + GetWord(%trans, 1);
	%zPos = GetTerrainHeight(%xPos SPC %ypos);

	%newtrans = %xPos SPC %yPos SPC %zPos SPC GetWords(%trans, 3, 6);
}
//merchants etc.. non ai bots...

function spawnTownBot(%pos, %team, %type, %race , %shop , %name)
{

	//%spawnPoint = $AIspawn::SpawnPoint[%index];

	if ($DebugMode) echo("spawnTownbot(" @ %pos @ ", " @ %team @ ", " @ %type @ ", " @ %race @ ", " @ %shop @ ", " @ %name @ ")");

	if (!(%type $= "Merchant" || %type $= "Bank" || %type $= "Guild" || %type $= "Porter" || %type $= "Boat"))
	return false;

	%armor = %race @ "Armor";
    // console spam fix
	switch$(%armor)
	{
		case "MaleOrcArmor": %doyouwantamedalorsomething = true;//echo("valid");
		case "MaleHumanArmor": %doyouwantamedalorsomething = true;//echo("valid");
		case "GoblinArmor":%armor = "MaleOrcArmor";
		default:%armor = "MaleHumanArmor";
	}
	%player = new Player()
	{
		dataBlock = %armor;
	};
	$townbotlist = %player SPC $townbotlist;
	%player.setTransform(%pos);
	MissionCleanup.add(%player);
	%player.name = %name;
	%player.namebase = %name;
	%player.istownbot = true;
	%player.team = %team;
	%player.mtype = %type;
	%player.shop = %shop;
	if(%type $= "Merchant")
	{
		%player.shopwloadout = $shop::loadoutweapons[%shop];
		%player.shopaloadout = $shop::loadoutarmors[%shop];
		%player.shopiloadout = $shop::loadoutItems[%shop];
	}
	return %player;
}

//*********
datablock ParticleData(AISpawnGlobeSmoke) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.5;   // rises slowly
	inheritedVelFactor   = 0.0;
	windCoefficient = 0.0;

	lifetimeMS           = 1000;  // lasts 2 second
	lifetimeVarianceMS   = 0;   // ...more or less

	textureName          = "special/BlueImpact";

	//useInvAlpha = true;
	spinRandomMin = -300.0;
	spinRandomMax = 300.0;

	constantAcceleration = 0;

	colors[0]     = "0.7 0.5 1.0 0.0";
	colors[1]     = "0.7 0.5 1.0 0.2";
	colors[2]     = "0.7 0.5 1.0 0.2";
	colors[3]     = "0.7 0.5 1.0 0.0";

	sizes[0]      = 0.6;
	sizes[1]      = 0.5;
	sizes[2]      = 0.4;
	sizes[3]      = 0.2;

	times[0]      = 0.0;
	times[1]      = 0.33;
	times[2]      = 0.9;
	times[3]      = 1.0;
};

datablock ParticleEmitterData(AISpawnGlobeEmitter) {
   ejectionPeriodMS = 0.1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 2;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "AISpawnGlobeSmoke";
};

function BotSpawnEffects(%player) {
   %position = %player.getWorldBoxCenter();
   
   //%c = containerRayCast(%opos, vectorAdd(%opos, "0 0 -100"), $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType, 0);
   
   //%position = vectorAdd(posFromRaycast(%c), "0 0 1");
   //******************************
   %sound = LightningHitSound; //change if you want to.
   Serverplay3D(%sound, %position);
   //******************************
   %spawnEMIT = new ParticleEmissionDummy() {
      position = %position;
      rotation = "1 0 0 1";
      scale = "1 1 1";
      dataBlock = defaultEmissionDummy;
      emitter = AISpawnGlobeEmitter;
      velocity = "1";
   };
   MissionCleanup.add(%spawnEMIT);
   %spawnEMIT.schedule(1000, "Delete");
}
