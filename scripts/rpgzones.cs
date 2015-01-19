function UpdateZone(%object)
{
	//CURRENTLY NOT USED IN T2RPG

	%client = Player::getClient(%object);
	%zoneflag = fetchData(%client, "tmpzone");

	//check if the player was found inside a zone
	if(%zoneflag !$= "")
	{
		//the player is inside a zone!
	
		//check if the player's current zone matches the one he's detected in
		if(fetchData(%client, "zone") !$= $Zone::FolderID[%zoneflag])
		{
			//the client's current zone does not match the one he really is in, so boot the player out of his
			//current zone (if any)
			if(fetchData(%client, "zone") !$= "")
				Zone::DoExit(Zone::getIndex(fetchData(%client, "zone")), %client);
	
			//throw the player inside this new zone
			Zone::DoEnter(%zoneflag, %client);
		}
		else
		{
			//the client is in the same zone as he was since the last zonecheck
			if($Zone::AmbientSound[%zoneflag] !$= "")
			{
				%m = $Zone::AmbientSoundPerc[%zoneflag];
				if(%m $= "") %m = 100;
	
				%r = floor(getRandom() * 100)+1;
				if(%r <= %m)
					Client::sendMessage(%client, 0, "~w" @ $Zone::AmbientSound[%zoneflag]);
			}
			if($Zone::Music[%zoneflag] !$= "")
			{
				if(%client.MusicTicksLeft < 1)
				{
					Client::sendMessage(%client, 0, "~w" @ $Zone::Music[%zoneflag]);
					%client.MusicTicksLeft = $Zone::MusicTicks[%zoneflag]+2;
				}
			}
			if($Zone::Type[%zoneflag] $= "WATER")
			{
				if(!IsDead(%client))
				{
					%noDrown = "";
					for(%i = 1; (%orb = $ItemList[Orb, %i]) !$= ""; %i++)
					{
						if($ProtectFromWater[%orb])
						{
							if(Player::getItemCount(%client, %orb @ "0"))
							{
								storeData(%client, "drownCounter", 0);
								%noDrown = true;
								break;
							}
						}
					}

					if(!%noDrown)
					{
						%dn = 10;

						storeData(%client, "drownCounter", 1, "inc");
						if((%dc = fetchData(%client, "drownCounter")) > %dn)
						{
							%dmg = Cap(floor(pow((%dc - %dn) / 1.2, 2)), 1.0, 1000) * "0.01";
							GameBase::virtual(%client, "onDamage", 0, %dmg, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %client);
							%snd = radnomItems(3, SoundDrown1, SoundDrown2, SoundDrown3);
							serverPlay3D(%snd, %client.player.getPosition());
						}
					}
				}
			}
		}
	}
	else
	{
		//the player is not inside any zone.
		//if the player has a current zone, then we need to kick him out of it
		if(fetchData(%client, "zone") !$= "")
			Zone::DoExit(Zone::getIndex(fetchData(%client, "zone")), %client);
	
		//start playing the ambient sound for the unknown zone
		if($Zone::AmbientSound[0] !$= "")
		{
			%m = $Zone::AmbientSoundPerc[0];
			if(%m $= "") %m = 100;
			
			%r = floor(getRandom() * 100)+1;
			if(%r <= %m)
				Client::sendMessage(%client, 0, "~w" @ $Zone::AmbientSound[0]);
		}
	
		//play the enter sound for the unknown zone
		if($Zone::EnterSound[0] !$= "")
			Client::sendMessage(%client, 0, "~w" @ $Zone::EnterSound[0]);
	}

	//-----------------------------------------------------------
	// Decrease music ticks
	//-----------------------------------------------------------
	if(%client.MusicTicksLeft > 0)
		%client.MusicTicksLeft--;

	//-----------------------------------------------------------
	// Decrease bonus state ticks
	//-----------------------------------------------------------
	DecreaseBonusStateTicks(%client);

	//-----------------------------------------------------------
	// Check if the player has moved since last ZoneCheck
	//-----------------------------------------------------------
	%pos = GameBase::getPosition(%client);
	if(%pos !$= %client.zoneLastPos && !IsDead(%client))
	{
		//train Weight Capacity
		if(OddsAre(8))
			UseSkill(%client, $SkillWeightCapacity, true, true, "", true);

		//cycle thru orbs
		for(%i = 1; (%orb = $ItemList[Orb, %i]) !$= ""; %i++)
		{
			if(OddsAre($BurnOut[%orb]))
			{
				if(Player::getItemCount(%client, %orb @ "0"))
				{
					Client::sendMessage(%client, $MsgRed, "Your " @ %orb.description @ " has burned out.");
					Player::decItemCount(%client, %orb @ "0", 1);
					RefreshAll(%client);
				}
			}
			if($BurnOutInRain[%orb] > 0)
			{
				if(fetchData(%client, "zone") $= "" && $isRaining)
				{
					if(OddsAre($BurnOutInRain[%orb]))
					{
						if(Player::getItemCount(%client, %orb @ "0"))
						{
							Client::sendMessage(%client, $MsgRed, "The rain has burned out your " @ %orb.description @ ".");
							Player::decItemCount(%client, %orb @ "0", 1);
							RefreshAll(%client);
						}
					}
				}
			}
		}

		//hard-coded list to save on CPU
		for(%z = 1; $ItemList[Badge, %z] !$= ""; %z++)
		{
			if(Player::getItemCount(%client, $ItemList[Badge, %z]))
			{
				%a = GetWord($BonusItem[$ItemList[Badge, %z]], 0);
				%b = GetWord($BonusItem[$ItemList[Badge, %z]], 1);
				%c = GetWord($BonusItem[$ItemList[Badge, %z]], 2);

				if(OddsAre(%c))
					GiveThisStuff(%client, %a @ " " @ %b, true);
			}
		}
	}
	%client.zoneLastPos = %pos;

	storeData(%client, "tmpzone", "");
}

function Zone::DoEnter(%zoneId, %client, %delay)
{	
	if(%delay != 1)
	schedule(100, 0, "zonecheck", %client, %zoneid);
	else
	{
	%oldZone = fetchData(%client, "zone");
	%newZone = %zoneId;
	commandtoClient(%client, 'StopMusic');
	storeData(%client, "zone", %newZone);
	if(fetchdata(%client, "guardzone") == 0)
	storedata(%client, "guardzone", %newZone);

	if(Zone::OnFriendlyTerms(%zoneId, %client))
		%color = $MsgBeige;
	else
		%color = $MsgRed;

	%msg = "You have entered " @ %zoneId.description @ ".";

	%rzs = GetRaceZoneString(%client, %zoneId);
	if(%rzs !$= "")
		%msg = %msg @ "  " @ %rzs;

	if(%msg !$= "")
		messageClient(%client, 'ZoneDoEnter', %color @ %msg);

	//if(%newZone.enterSound !$= "")
	//	%client.player.playAudio(0, %zoneId.enterSound);
    commandToClient(%client, 'RPGEnterZone', %zoneId.description, %rzs,
                    Zone::OnFriendlyTerms(%zoneId, %client), true);
	if(%newZone.musicType !$= "")
	{
		commandtoClient(%client, 'RPGPlayMusic', %newzone.MusicType);
	}
	Zone::onEnter(%client, %oldZone, %newZone);
	}
}
function zonecheck(%client, %zoneid)
{
Zone::DoEnter(%zoneid, %client, 1);
}
function Zone::DoExit(%zoneId, %client)
{
	if(inArena(%client))
	return;
	%zoneLeft = fetchData(%client, "zone");
	commandtoClient(%client, 'StopMusic');
	storeData(%client, "zone", "");
	%color = $MsgBeige;
	%msg = "You have left " @ %zoneId.description @ ".";

	messageClient(%client, 'ZoneDoExit', %color @ %msg);
    commandToClient(%client, 'RPGEnterZone', %zoneId.description, "",
                    Zone::OnFriendlyTerms(%zoneId, %client), false);
	//if(%zoneId.exitSound !$= "")
	//	%client.player.playAudio(0, %zoneId.exitSound);
	commandtoClient(%client, 'RPGPlayMusic', "Wilderness");
	Zone::onExit(%client, %zoneLeft);
}

function Zone::OnFriendlyTerms(%zoneId, %client)
{
	%clraceId = $RaceID[fetchData(%client, "RACE")];
	%zraceId = %zoneId.RaceID;

	if(%zraceId !$= %clraceId)
		return true;
	else
		return false;
}

function IsInBetween(%x, %r1, %r2)
{
	if(%r1 > %r2)
	{
		%tmp = %r1;
		%r1 = %r2;
		%r2 = %tmp;
	}
	if(%x >= %r1 && %x <= %r2)
		return true;
	else
		return false;
}
function Zone::handleTeleport(%zoneid, %client)
{
	//echo("fix an error");
	
	//Zone::DoExit(%zoneid, %client);
}
function Zone::onEnter(%client, %oldZone, %newZone)
{
	refreshHPREGEN(%client);	//this is because you regen faster or slower depending on the zone you are in
}

function Zone::onExit(%client, %zoneLeft)
{
	refreshHPREGEN(%client);	//this is because you regen faster or slower depending on the zone you are in
}

function GetNearestZone(%client, %mtype, %returnType)
{
	if (%mtype $= "town")
	{
		%type = "Protected";
		%all = 0;
	}
	else if(%mtype $= "dungeon")
	{
		%type = "Dungeon";
		%all = 0;
	}
	else
	{
		%all = 1;// if we are just searching for the closest zone
	}
	%closestDist = 999999;
	%closestZone = "";
	%mpos = "";
	%clpos = %client.player.getPosition();

	%group = nameToId("MissionGroup/Zones");
	if(%group !$= -1)
	{
		%count = %group.getCount();
		for(%i = 0; %i < %count; %i++)
		{
			%object = %group.getObject(%i);

			if(%object.type $= %type || %all)
			{
				%modpos = %object.position;
				%dist = vectorDist(%modpos, %clpos);
				if(%dist < %closestDist)
				{
					%closestDist = %dist;
					//%clpos = %modpos;
					%closestZoneDesc = %object.description;
					%closestZone = %object;
					%mpos = %modpos;
				}
			}
		}
	}

	if(%mpos $= "")		//no zones were found (this means there are NO zones in the map...)
		return false;
	
	//%returnType:
	//1 = returns the distance from the client to the nearest zone
	//2 = returns the description of the zone nearest to the client
	//3 = returns the zone id of the zone nearest to the client
	//4 = returns the position of the middle of the zone nearest to the client
	if(%returnType $= 1)
		return %closestDist;
	else if(%returnType $= 2)
		return %closestZoneDesc;
	else if(%returnType $= 3)
		return %closestZone;
	else if(%returnType $= 4)
		return %mpos;
}

function GetZoneByKeywords(%client, %keywords, %returnType)
{
	%mpos = "";

	%group = nameToId("MissionGroup/Zones");

	if(%group !$= -1)
	{
		%count = %group.getCount();
		for(%i = 0; %i < %count; %i++)
		{
			%object = %group.getObject(%i);
			%desc = %object.description;

			if(strstr(%desc, %keywords) !$= -1)
			{
				if(%returnType == 1)
					%dist = vectorDist(%object.position, %client.player.getPosition());

				//%returnType:
				//1 = returns the distance from the client to the zone
				//2 = returns the description of the zone
				//3 = returns the zone id
				//4 = returns the position of the middle of the zone

				if(%returnType $= 1)
					return %dist;
				else if(%returnType $= 2)
					return %desc;
				else if(%returnType $= 3)
					return %object;
				else if(%returnType $= 4)
					return %mpos;
			}
		}
		return false;	
	}
	else
		return false;
}

function Zone::getNumPlayers(%z, %all)
{
	%n = 0;

	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%id = ClientGroup.getObject(%icl);

		if(fetchData(%id, "zone") $= %z)
		{
			if(%all)
				%n++;
			else
			{
				if(!%id.isAiControlled())
					%n++;
			}
		}
	}

	return %n;
}

function ObjectInWhichZone(%object)
{
	return positionInWhichZone(%object.position);
}

function positionInWhichZone(%pos)
{
	%fid = "";
	%closest = 99999;
	
	%zid = "";
	%group = nameToID("MissionGroup/Zones");
	if(%group !$= -1)
	{
		for(%z = 0; %z < %group.getCount(); %z++)
		{
			%zoneId = %group.getObject(%z);

			%rad = (GetWord(%zoneId.scale, 0) + GetWord(%zoneId.scale, 1) + GetWord(%zoneId.scale, 2)) / 3;
			%dist = vectorDist(%pos, %zoneId.position);
			if(%dist <= %rad)
			{
				if(%dist < %closest)
				{
					%closest = %dist;
					%zid = %zoneId;
				}
			}
		}
	}
	return %zid;
}

function Zone::getPlayerList(%zoneId, %type)
{
	%aa = "";

	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%id = ClientGroup.getObject(%icl);

		if(fetchData(%id, "zone") $= %zoneId)
		{
			%flag = false;
			if(%type $= 1)
				%flag = true;
			else if(%type $= 2)
			{
				if(!%id.isAiControlled())
					%flag = true;
			}
			else if(%type $= 3)
			{
				if(%id.isAiControlled())
					%flag = true;
			}

			if(%flag)
				%aa = %aa @ %id @ " ";
		}
	}

	return %aa;
}

function RPGGame::onEnterTrigger(%game, %triggerName, %data, %obj, %colobj)
{
	%player = %colobj;
	%client = %colobj.client;

//	echo("%player: " @ %player);
//	echo("%client: " @ %client);
//	echo("%triggerName: " @ %triggerName);
//	echo("%data: " @ %data);
//	echo("%obj: " @ %obj);
//	echo("%colobj: " @ %colobj);
//	echo("%obj.description: " @ %obj.description);
	if(%obj.type $= "teleport")
	{
		%player.setPosition(%obj.value);
	
	}
	else if (%obj.type $= "sleepzone")
	{
		//echo("You have entered sleepy");
		storedata(%client, "insleepzone", true);
		messageClient(%client, 'RPGchatCallback', "This area feels safe enough to #sleep.");
	}
	else if (%obj.type $= "BoatDock")
	{
		//echo(%data SPC %obj SPC %colObj SPC %colobj.client SPC %colobj.client.rpgname SPC %colobj.getdatablock().getName());
		if(%colobj.getdatablock().getName() $= RPGBoat)
		{
			%obj.full = true;
			%colobj.zone = %obj;
		}
	}
    else if (%obj.type $= "falldie")
    {
       //future reference for camera mod thing here.
       %colobj.scriptkill(); //die :D
       messageClient(%client, 'RPGchatCallback', "You fall to your death!");
    }
	else if (%obj.type $= "pvpzone")
	{
        storedata(%client, "inpvpzone", true);
        messageClient(%client, 'RPGchatCallback', "Danger! You have entered a PvP zone, anyone can attack you here.");
        if(%obj.isItemSafeZone) {
           messageClient(%client, 'RPGchatCallback', "An item security spell encompasses your body, protecting your items if you die.");
           storedata(%client, "initemsafezone", true);
        }
    }
	else if (%obj.type $= "guildzone")
	{
		
		if(%obj.fightinprogress )
		{
			if(!%client.participate)
			{
				MessageClient(%client, 'Error', "Fight in Progress");
				%vel = %player.getvelocity();
				//reverse direction
				%vel = vectorscale(%vel, -2);
				%player.setvelocity(%vel);
				//push client outside of zone
			}
			else
			Zone::DoEnter(%obj, %client);
			
		}
		else
		{
			Zone::DoEnter(%obj, %client);
			%guildid = IsInWhatGuild(%client);
			%guild = GuildGroup.getObject(%guildid);
			if(%obj.owned)
			{
				//check if client is in the same guild as the zone.
				if(%obj.owner $= %guild)
				{
					MessageClient(%client, 'GuildEnter', "You have entered your guilds territory");
				}
				else
				{
					//do nothing.
					MessageClient(%client, 'GuildEnterHostile', "This land is owned by" SPC %obj.owner.getName() @ ", you may #challenge this ownership");
				}
				
			}
			else
			{
				//if client is in a guild and the guild doesnt own any other zones, CLAIM IT!
				if(%guildid != -1)
					MessageClient(%client, 'GuildUnclaimed', "This zone is unclaimed, you may claim it with #claim");
				else
					MessageClient(%client, 'GuildUnclaimed', "No guild has claimed this land");
			}
		}
	}
	else
	{
		Zone::DoEnter(%obj, %client);
	}
}

function RPGGame::onLeaveTrigger(%game, %triggerName, %data, %obj, %colobj)
{
	%client = %colobj.client;
	if(%obj.type $= "teleport")
	{
	
	
	}
	else if (%obj.type $= "sleepzone")
	{
		storedata(%client, "insleepzone", false);
	}
	else if (%obj.type $= "BoatDock")
	{
		if(%colobj.getdatablock().getName() $= RPGBoat)
		{
			%obj.full = false;
			%colobj.zone = %obj;
		}		
	}
	else if (%obj.type $= "pvpzone")
	{
        messageClient(%client, 'RPGchatCallback', "You have exited the PvP Zone.");
		storedata(%client, "inpvpzone", false);
        if(%obj.isItemSafeZone) {
           messageClient(%client, 'RPGchatCallback', "Upon exiting the zone, the item security spell wears off.");
           storedata(%client, "initemsafezone", false);
        }
	}
	else if (%obj.type $= "guildzone")
	{
		Zone::DoExit(%obj, %client);
		%guildid = IsInWhatGuild(%client);
		%guild = GuildGroup.getObject(%guildid);
		if(%obj.fightinprogress && %client.participate)
		{
			if(%guild == %obj.owner)
			{
				%obj.home--;
				if(%obj.home <= 0) 
					%obj.owner.EndZoneMatch( %obj, %obj.challenger);
			}
			if(%guild == %obj.challenger)
			{
				%obj.away--;
				if(%obj.away <= 0)
					%obj.owner.EndZoneMatch( %obj, %obj.challenger);
			}
			%client.participate = "";
			%client.guildmatchpvp = "";
			%client.enemyguild = "";
		}
	}
	else
	{
		Zone::DoExit(%obj, %client);
	}
}
