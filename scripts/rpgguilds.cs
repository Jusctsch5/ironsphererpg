//Guilds! These are stored in script objects for quick saving and loading.
//you only can join one guild per server per unique guid. Otherwise we can have spy problems. 
function serverCMDGetGuildList(%client)
{
	CommandToClient(%client, 'startGuildLoad');
	echo("loading guilds for client:" SPC %client);
	for(%i = 0; %i < GuildGroup.getCount(); %i++)
	{
		%guild = GuildGroup.GetObject(%i);
		%id = %i;
		%name = %guild.getName();
		%owner = %guild.getOwnerName();
		%description = %guild.GetDesc();
		echo("Guild" SPC %id SPC %name SPC %owner SPC %description);
		CommandToClient(%client, 'AddGuildLine', %id, %name, %owner, %description);
	}
	CommandToClient(%client, 'EndGuildLoad');
}
function ServerCMDCreateGuild(%client, %guildname)
{
	
	if(IsInWhatGuild(%client) == -1)
	{
		if(fetchdata(%client, "Coins") > 110000)
		{
			storedata(%client, "coins", fetchdata(%client, "coins") - 110000);//bye bye money!
			initGuild(%client, %guildname);
			MessageClient(%client, 'GuildCreated', "You have created a guild! Press the [Open Guild Screen key] to manage your guild");
		}
		else
			MessageClient(%client, 'FailMakeGuild', "You do not have enough Money to create a guild. (100,000 coins required)");
	}
	else
	MessageClient(%client, 'FailMakeGuild', "You are already in a guild!");
}
function ServerCMDJoinGuild(%client, %guildid)
{

	if(IsInWhatGuild(%client) == -1)
	{	
		%guild = GuildGroup.getObject(%guildid);
		%guild.addMember(%client);
		%guild.setRank(%client, 0);//put in application!
		SaveServerGuilds();
		MessageClient(%client, 'JoinedGuild', "You have joined a guild! Hit your [Open Guild Screen] key to view your guild");
	}
	else
		MessageClient(%client, 'FailJoinGuild', "You are already in a guild!");
}
function ServerCMDGetGuildInfo(%client, %guildid)
{
	if(%guildid $= "")
	{
		//user didnt click info button so show his own guild, if client is not in a guild then quit.
			//get users guild id
		%guildid = IsInWhatGuild(%client);
		if(%guildid == -1)
		return;
	}
	//ok tell client to clear existing data on his GUI, then upload the ROSTER
	commandToClient(%client, 'ClearManagementGUI');
	%guild = GuildGroup.GetObject(%guildid);
	%playerlist = %guild.getPlayerList();//obtain player list
	for(%i = 0; (%Gguid = GetWord(%playerList, %i)) != 0; %i++)
	{
		%pname = %guild.GetPlayerName(%Gguid);
		%pRank = %guild.GetGUIDRank(%Gguid);
		%prankname = %guild.getRankName(%prank);
		commandToClient(%client, 'addPlayerToGuildRoster', %Gguid, %pname, %pRankname);
		
	}
	SendGuildInformationText(%client, %guild);
	SendGuildDescription(%client, %guild);
}
function ServerCMDGuildKick(%client, %selguid)
{

	//ok this is called when the client selects a user from the roster and clicks leave or kick.
	//client can only kick people from his own guild, so we will get his guild here
	if(%selguid == -1 || %selguid $= "")
	%selguid = %client.guid;
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	if(!%guild.GUIDinGuild(%selguid)) 
	{
		MessageClient(%client, 'Error', "You are not a member of this guild");

		return false;
	

	}

	//check to see if client is kicking himself. If he is (and if he is not the owner) drop him off the guild.
	if(%guild.GetGUIDRank(%selclient) == 0)

	if(%selguid == %client.guid)
	{
		//kickin self
		if(%guild.getOwner() == %client.guid) 
		{
			//owner trying to kick self
			MessageClient(%client, 'FailKickSelf', "Guild owners cannot kick themselves.");
			return;
		}
		//ok kick player
		%Guild.RemovePlayer(%selguid);
		MessageClient(%client, 'LeaveGuild', "You have left the guild");
		
		SaveServerGuilds();
	}
	if(%client.guid == %guild.owner)
	{
		//kick player
		MessageClient(%client, 'Kickeduser', "User kicked");
		%Guild.RemovePlayer(%selguid);
		SaveServerGuilds();
	}
	if(%guild.GetGUIDRank(%selclient) == 0)
	{
		%Guild.RemovePlayer(%selguid);
		SaveServerGuilds();
	}
}
function ServerCMDGuildPlayerInfo(%client, %selguid)
{
	if(%selguid $= "" || %selguid == -1)
	%selguid = %client.guid;
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;//get guild id
	%guild = GuildGroup.GetObject(%guildid);

	if(%guild.GUIDinGuild(%selguid))
	{
		commandToClient(%client, 'setGuildPlayerInfo', %guild.getPlayerName(%selguid), %guild.getRankName(%guild.getGUIDRank(%selguid)));
	}
	else
		MessageClient(%client, 'CannotView', "You cannot view the info of another player from another guild");
}
function ServerCMDPopulateGuildRanks(%client)
{
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	for(%i = 10; %i>=1; %i--)
	{
		commandToClient(%client, 'AddEditPlayerRank', %i, %guild.getRankName(%i));
	}
	commandToClient(%client, 'EditPlayerRankDone');
}
function ServerCMDgetPlayerRank(%client, %selguid)
{
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	if(%selguid $= ""|| %selguid == -1)
	%selguid = %client.guid;
	//echo(%selguid);
	commandToClient(%client, 'FinishPlayerRank', %guild.getGUIDRank(%selguid));
}
function ServerCMDsubmitEditProfile(%client, %selguid, %rank)
{
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	if(%selguid == 0 || %selguid == -1)
	%selguid = %client.guid;
	
	if(%guild.GUIDinGuild(%selguid))
	{
		if(%guild.GetGUIDrank(%client.guid) == 10)
		{
			if(%rank == 10) return;
			if(%client.guid != %selguid)
			{
				%guild.setGUIDRank(%selguid, %rank);
				SaveServerGuilds();
				MessageClient(%client, 'GuildRankChange', "You have changes his rank to" SPC %guild.getRankName(%rank));
			}
			else
				MessageClient(%client, 'error', "You cannot change your own rank");
		}
		else 
			MessageClient(%client, 'error', "You must be the guild master in order to change ranks");
			
	}
	else
		MessageClient(%client, 'error', "You must be the owner of this guild to change this persons rank");
}
function serverCMDChangeGuildInfo(%client, %info1, %info2, %info3, %info4, %info5, %info6, %info7, %info8, %info9, %info10)
{
	//%text = %info1 @ %info2 @ %info3 @ %info4 @ %info5 @ %info6 @ %info7 @ %info8 @ %info9 @ %info10;
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.getObject(%guildid);
	if(%guild.GUIDinGuild(%client.guid))
	{
		if(%guild.GetGUIDrank(%client.guid) == 10)
		{
			%guild.setInformation(%info1, 0);
			%guild.setInformation(%info2, 1);
			%guild.setInformation(%info3, 2);
			%guild.setInformation(%info4, 3);
			%guild.setInformation(%info5, 4);
			%guild.setInformation(%info6, 5);
			%guild.setInformation(%info7, 6);
			%guild.setInformation(%info8, 7);
			%guild.setInformation(%info9, 8);
			%guild.setInformation(%info10, 9);
			SaveServerGuilds();
			SendGuildInformationText(%client, %guild);
			MessageClient(%client, 'GuildInfoChange', "Guild information has been successfully changed");
		}
		else
			messageClient(%client, 'error', "Only the guild owner can change guild information");
	
	}
	else
		messageClient(%client, 'error', "You must be a member of this guild to change its information.");

}
function ServerCMDChangeGuildShortDesc(%client, %text)
{
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	if(%guild.GUIDinGuild(%client.guid))
	{
		if(%guild.getguidrank(%client.guid) == 10)
		{
			%guild.setDesc(%text);
			SaveServerGuilds();
			SendGuildDescription(%client, %guild);
			MessageClient(%client, 'GuildDescriptionChange', "Guild Description sucessfully changed");
		}
		else
			messageclient(%client, 'error', "Only the guild owner can change this information");
	}
	else
		messageClient(%client, 'error', "You must be a member of this guild to change its description");
}
function SendGuildDescription(%Client, %guild)
{
	CommandToClient(%client, 'ReceiveGuildDescText', %guild.getDesc());
}
function ServerCMDGuildGetTerritoryInfo(%client)
{
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return;
	%guild = GuildGroup.GetObject(%guildid);
	if(%guild.GUIDinGuild(%client.guid))
	{
		if(%guild.getguidrank(%client.guid) >= 1)
		{
			%zone = %guild.getZoneList();
			for(%i = 0; GetWord(%zone, %i) !$= ""; %i++)
			{
				%zonename = GetWord(%zone, %i);
				echo("SENT: " SPC %client SPC %i SPC %zonename.description);
				commandToClient(%client, 'GuildAddTerritory', %i,  %zonename.description, %zonename.challenged);
			}
			commandToClient(%client, 'GuildTerritoryDone');
		}
	}
	
}
function SendGuildInformationText(%client, %guild)
{
	%text = %guild.getInfo();
	CommandToClient(%client, 'ReceiveInformationText', getsubStr(%text, 0, 255), getsubstr(%text, 255, 255), getsubstr(%text, 255*2, 255), getsubstr(%text, 255*3, 255), getsubstr(%text, 255*4, 255), getsubstr(%text, 255*5, 255), getsubstr(%text, 255*6, 255), getsubstr(%text, 255*7, 255) ,getsubstr(%text, 255*8, 255), getsubstr(%text, 255*9, 255));
}

function LoadServerGuilds() 
{
//called in RPGGame.cs in function  'RPGGame::missionLoadDone'
	if(isobject(guildgroup))
	GuildGroup.delete();//sometimes someone may exec this twice and... thats a bad thing
 	exec("guilds/GuildRegistry.cs");
	if(!isobject(GuildGroup))
		new SimGroup (GuildGroup);

	MissionCleanup.add(GuildGroup);	
	echo("Starting" SPC GuildGroup.GetCount() SPC "Guilds");
	for(%i = 0; GuildGroup.getCount() > %i; %i++)
	{
		Guild::onAdd(GuildGroup, GuildGroup.getObject(%i));
	}
}

function SaveServerGuilds()
{
 	GuildGroup.save("Guilds/GuildRegistry.cs");

}

function initGuild(%owner, %name)
{
	%guild = new ScriptObject() {
		class = "Guild";
		
	};
	%guild.init(%owner, %name);
	GuildGroup.add(%guild);
	return %guild;
}
function IsInWhatGuild(%client)
{
	%guid = %client.guid;
	%guildid = -1;
	for(%i = 0; %i < GuildGroup.GetCount(); %i++)
	{
		%guild = GuildGroup.GetObject(%i);
		if(%guild.GUIDinGuild(%guid))
		{
			%guildid = %i;
			break;
		}
	}

	return %guildid;
}
function Guild::Init(%this, %owner, %name)
{
	%this.owner = %owner.guid;
	%this.setName(%name);
	%this.addMember(%owner);
	//set all the rank names
	%this.EditRankNames(10, "Master");
	%this.EditRankNames(9, "Guardian");
	%this.EditRankNames(8, "Dreadnought");
	%this.EditRankNames(7, "Lord");
	%this.EditRankNames(6, "Champion");
	%this.EditRankNames(5, "Berserker");
	%this.EditRankNames(4, "Ravenger");
	%this.EditRankNames(3, "Mauler");
	%this.EditRankNames(2, "Raider");
	%this.EditRankNames(1, "Runt");
	%this.EditRankNames(0, "Applicant");
	%this.SetRank(%owner, 10);
	%this.setDesc("No description");//set the description
	%this.setInformation("");
	SaveServerGuilds();//SAVE
	return %this;
}
function Guild::AddMember(%this, %client)
{
	if(%this.playerlist $= "")
	%this.playerlist = %client.guid;
	else
	%this.playerlist = %this.playerlist SPC %client.guid;
	%this.pname[%client.guid] = %client.realname;
	storedata(%client, "inguild", true);
	return %this;
}
function Guild::EditRankNames(%this, %rank, %name)
{
	%this.ranks[%rank] = %name;
	return %this;
}
function Guild::SetRank(%this, %client, %newRank)
{
	%this.playerRank[%client.guid] = %newRank;
	return %this;
}
function Guild::SetGUIDRank(%this, %guid, %newRank)
{
	%this.playerRank[%guid] = %newRank;
	return %this;
}
function Guild::SetName(%this, %name)
{
	%this.name = %name;
	return %this;
}
function Guild::SetDesc(%this, %desc)
{
	%this.desc = %desc;
	return %this;
}
function Guild::SetInformation(%this, %text, %line)
{
	%this.info[%line] = %text;
	return %this;

}
function Guild::RemovePlayer(%this, %guid)
{
	%this.playerRank[%guid] = "";
	%this.pname[%guid] = "";
	%list = %this.getPlayerList();
	%list2 = %this.getOwner();//owner is always first
	for(%i = 1; (%id = GetWord(%list, %i)) !$= ""; %i++)
	{
		if(%id == %guid)
		{
		
		}
		else
		{
			%list2 = %list2 SPC %id;
		}
	}
	%this.playerlist = %list2;
	//player removed
	
}
function Guild::GetName(%this)
{
	return %this.name;
}
function Guild::getDesc(%this)
{
	return %this.desc;
}
function Guild::getInfo(%this)
{
	for(%i = 0; %this.info[%i] !$= ""; %i++)
		%text = %text @ %this.info[%i];
	return %text;
}
function Guild::GetOwner(%this)
{
	return %this.owner;
}
function Guild::GetOwnerName(%this)
{
	return %this.getPlayerName(%this.getOwner());
}
function Guild::GetRankName(%this, %id)
{
	return %this.ranks[%id];
}
function Guild::GetPlayerGuid(%this, %playername)
{
	//not yet
}
function Guild::GetPlayerName(%this, %guid)
{
	return %this.Pname[%guid];
}
function Guild::GetPlayerList(%this)
{
	return %this.playerList;
}
function Guild::GetGUIDRank(%this, %guid)
{
	return %this.playerRank[%guid];

}
function Guild::GUIDinGuild(%this, %guid)
{
	return !(%this.pname[%guid] $= "");
}
function Guild::AddZone(%this, %zone)
{
	if(GetWordCount(%this.zonelist) < $guildownerzonelimit )
	%this.zonelist = %this.zonelist @ %zone.getname() @ " ";
	%zone.owner = %this;
	SaveServerGuilds();
	return %this;
}
function Guild::RemoveZone(%this, %zone)
{
	%this.zonelist = ltrim(strreplace(" " @ %this.zonelist, " " @ %zone.getName() @ " ", " "));
	return %this;
}
function Guild::OwnZone(%this, %zone)
{
	return !(%this.zonelist $= ltrim(strreplace(" " @ %this.zonelist, " " @ %zone.getName() @ " ", " ")) );
}
function Guild::GetZonelist(%this)
{
	return %this.zonelist;
}
function Guild::TakeZone(%this, %from, %zone)
{
	%from.removeZone(%zone);
	%this.addzone(%zone);
	for(%icl = 0; %icl < ClientGroup.getCount(); %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.isAiControlled()) continue; //skip bots

		if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
		{
			MessageClient(%cl, 'GuildMatch', "Your guild has taken" SPC %zone.description SPC "from" SPC %from.getName() @ "!");
		}
		else
		if(%from.GUIDinGuild(%cl.guid)  && %from.getGUIDRank(%cl.guid) > 0 )
		{
			MessageClient(%cl, 'GuildMatch', %this.getName() SPC "has taken" SPC %zone.description SPC "from your guild!");
		}
		else
		{
			MessageClient(%cl, 'GuildMatch', %this.getName() SPC "has taken" SPC %zone.description SPC "from" SPC %from.getName() @ "!");
		}
	}
	
	return %this;
}
function Guild::onAdd(%this, %obj)
{
	echo("Loaded Guild:" SPC %obj.getName());
	for(%i = 0; GetWord(%obj.zonelist, %i) !$= ""; %i++)
	{
		%zone = GetWord(%obj.zonelist, %i);
		%zone.owned = true;
		%zone.owner = %obj;
		echo(%zone.description SPC "is owned by" SPC %obj.getName());
	}
}
function Guild::startChallenge(%this, %zone, %challenger)
{
	//count players currently online, schedule for 60 min if enough are on else schedule for 24 hours
	//count players online in challengers guild as a min for an instant accept
	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.isAiControlled()) continue; //skip bots

		if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
		{
			%home++;
			MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "has challenged your guild for" SPC %zone.description @ "!");
		}
		else
		if(%challenger.GUIDinGuild(%cl.guid)  && %challenger.getGUIDRank(%cl.guid) > 0 )
		{
			%away++;
			MessageClient(%cl, 'GuildMatch', "Your guild has challenged" SPC %this.getName() SPC "for" SPC %zone.description @ "!");
		}
		else
		{
			MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "has challenged" SPC %this.getName() SPC "for" SPC %zone.description @ "!");
		}
		

	}	
	%zone.minchallenger = %away;
	%zone.challenger = %challenger;
	%zone.challenged = true;
	%zone.canacceptchallenge = true;
	%this.challengedzone = %zone;
	if(%home >= %away)
		%time = 1;
	else
		%time = 24;
		%zone.challengeEvent = %this.schedule(%time*60*60*1000, "PrepareChallenge", %zone, %challenger, 24);
	//%zone.challengeEvent = %this.schedule(1000, "PrepareChallenge", %zone, %challenger, 24);
	MessageAll('GuildMatch', "Match will begin in aproximatly " SPC %time SPC "hours or until the challenged accepts.");
	//NOTIFY all guilds involved of the challenge, only the attack will be able to cancel this, but the defender can force the challenge to happen at any time, when accepted there will be a 2 min timer till the match starts for participants to get equipment
}
function Guild::PrepareChallenge(%this, %zone, %challenger, %hours)
{
	//check players again
	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.isAiControlled()) continue; //skip bots

		if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
			%home++;
		else
		if(%challenger.GUIDinGuild(%cl.guid)  && %challenger.getGUIDRank(%cl.guid) > 0 )
			%away++;
	}
	if(%zone.minchallenger <= %away)
	{
		%zone.minchallenger = "";
		//were good
	}
	else
	{
		if(%hours == 24)
		{
			for(%icl = 0; %icl < %count; %icl++)
			{
				%cl = ClientGroup.getObject(%icl);
				if(%cl.isAiControlled()) continue; //skip bots

				if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
					MessageClient(%cl, 'GuildMatchFail', "Guild match aborted, not enough challengers");
				else
				if(%challenger.GUIDinGuild(%cl.guid) && %challenger.getGUIDRank(%cl.guid) > 0 )
					MessageClient(%cl, 'GuildMatchFail', "Guild match aborted, not enough challengers");
			
			}
			ResetZoneVars(%zone);
			return;
		}
		else
		{
			//reset timer for 23 more hours and notify
			%zone.challengeEvent = %this.schedule(23*60*60*1000, "PrepareChallenge", %zone, %challenger, 24);
			for(%icl = 0; %icl < %count; %icl++)
			{
				%cl = ClientGroup.getObject(%icl);
				if(%cl.isAiControlled()) continue; //skip bots

				if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
					MessageClient(%cl, 'GuildMatchFail', "Not enough challengers on, maximum time extended 23 hours, your guild leader may #accept their challenge at any time they have at least the minimum they challenged with online.");
				else
				if(%challenger.GUIDinGuild(%cl.guid) && %challenger.getGUIDRank(%cl.guid) > 0 )
					MessageClient(%cl, 'GuildMatchFail', "Not enough challengers on, maximum time extended 23 hours");
			}
			ResetZoneVars(%zone);
			return;
		}
	}
	if(%home == 0)
	{
		//automatic loss maks sure challenger wont exceed max zones if they wont then switch it and notify
		%challenger.takezone(%this, %zone);//switched hands!
		ResetZoneVars(%zone);
		return;
	}
	//NOTIFY ALL PLAYERS THAT A CHALLENGE IS BEGINING IN 2 MIN!
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.isAiControlled()) continue; //skip bots

		if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
			MessageClient(%cl, 'GuildMatchFail', "ZONE WAR MATCH STARTING IN 2 MIN! Get your equipment!");
		else
		if(%challenger.GUIDinGuild(%cl.guid) && %challenger.getGUIDRank(%cl.guid) > 0 )
			MessageClient(%cl, 'GuildMatchFail', "ZONE WAR MATCH STARTING IN 2 MIN! Get your equipment!");
		else

			MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "attacks" SPC %this.getName() SPC "for" SPC %zone.description SPC "in 2 minutes!");
	}	
	%zone.canacceptchallenge = "";
	%zone.challengeevent = %this.schedule(60*2*1000, "BeginZoneMatch", %zone, %challenger);
}
function Guild::BeginZoneMatch(%this, %zone, %challenger)
{
	%zone.canacceptchallenge = "";

	%zone.challengeEvent = %this.schedule(60*60*1000, "EndZoneMatch", %zone, %challenger);
	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.isAiControlled()) continue; //skip bots

		if(%this.GUIDinGuild(%cl.guid) && %this.GetGUIDRank(%cl.guid) > 0  )
			%home++;
		else
		if(%challenger.GUIDinGuild(%cl.guid) && %challenger.getGUIDRank(%cl.guid) > 0 )
			%away++;
	}
	%zone.home = %home;
	%zone.away = %away;
	if(%home == 0)
	{
		//end %this loses
		//%challenger.TakeZone(%this, %zone);
		%this.endzonematch(%zone, %challenger);//better way to handle it for ingame messaging. 
		//ResetZoneVars(%zone);
		return;
	}
	if(%away == 0)
	{
		//end %away loses
		%this.endzonematch(%zone, %challenger);
		//ResetZoneVars(%zone);
		return;
	}
	//kick all players in zone, then tele the dueling guilds back in Set the PVP guildwar flag.
	%list = Zone::getPlayerList(%zone, 2);
	%flag = true;
	for(%i = 0; GetWord(%list, %i); %i++)
	{
		%id = GetWord(%list, %i);
		if(%id.isaicontrolled())
			%id.scriptKill($DamageType::Suicide);
		else
			felloffmap(%id);
	}
	
	//notify of match start
	%count = ClientGroup.getCount();
	%ctmp = 0;
	%htmp = 0;
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		//echo(%cl.rpgname);
		if(%challenger.GUIDinGuild(%cl.guid) && %challenger.GetGUIDRank(%cl.guid) > 0  )
		{
			echo("CHALLENGER" SPC %challenger SPC "PLAYER" SPC %cl SPC %cl.rpgname);
			MessageClient(%cl, 'GuildMatch', "Match has started, you are on offense!");
			if(%cl.player.getState() !$= "Dead" && %cl.player !$= "" && %cl.player.getstate() !$= "")
			{
				if(%cl.player.ismounted())
				{
					//unmount the player
					%cl.player.getobjectMount().unmountobject(%cl.player);
					%cl.player.setcontrolobject(0);
				}
				%cl.enemyguild = %this;
				%cl.guildmatchpvp = true;
				%cl.participate = true;

				%cl.player.setPosition(VectorAdd(%zone.aPosition, "0 0 " @ %ctmp*5));
				%ctmp++;
				
			}
			else
				MessageClient(%cl, 'GuildMatch', "You are automatically removed from the match, due to the fact you are dead at the time of match start");
		}
		else
		if(%this.GUIDinGuild(%cl.guid)  && %this.getGUIDRank(%cl.guid) > 0 )
		{
			echo("DEFENDER" SPC %challenger SPC "PLAYER" SPC %cl SPC %cl.rpgname);
			MessageClient(%cl, 'GuildMatch', "Match has started, you are on defense!");
			if(%cl.player.getState() !$= "Dead" && %cl.player !$= "" && %cl.player.getstate() !$= "")
			{
				if(%cl.player.ismounted())
				{
					//unmount the player
					%cl.player.getobjectMount().unmountobject(%cl.player);
					%cl.player.setcontrolobject(0);
				}
				%cl.enemyguild = %challenger;
				%cl.guildmatchpvp = true;
				%cl.participate = true;

				%cl.player.setPosition(VectorAdd(%zone.dPosition, "0 0 " @ %htmp*5));
				%htmp++;
				
			}
			else
				MessageClient(%cl, 'GuildMatch', "You are automatically removed from the match, due to the fact you are dead at the time of match start");
		}
		else
		{
			MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "is now fighting" SPC %this.getName() SPC "for" SPC %zone.description @ "!");
		}
	}
	echo("modifing data homeb homen awayb awayn" SPC %zone.home SPC %htmp SPC %zone.away SPC %ctmp);
	%zone.home = %htmp;
	%zone.away = %ctmp;
	%zone.fightinprogress = true;
	if(%zone.home == 0 || %zone.away == 0)
		%this.endzonematch(%zone, %challenger);
	

}
function Guild::EndZoneMatch(%this, %zone, %challenger)
{
	if(IsEventPending(%zone.challengeEvent))
		cancel(%zone.challengeEvent);
	if(%zone.home > 0 || (%zone.home == 0 && %zone.away == 0))
	{
		//home wins, notify players reset PVP flags
		%count = ClientGroup.getCount();
		for(%icl = 0; %icl < %count; %icl++)
		{
			%cl = ClientGroup.getObject(%icl);
			if(%challenger.GUIDinGuild(%cl.guid) && %challenger.GetGUIDRank(%cl.guid) > 0  )
			{
				MessageClient(%cl, 'GuildMatch', "Your guild has failed to take" SPC %zone.description SPC "from" SPC %this.getName() @ "!");
			}
			else
			if(%this.GUIDinGuild(%cl.guid)  && %this.getGUIDRank(%cl.guid) > 0 )
			{
				MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "has failed to take" SPC %zone.description SPC "from your guild!");
			}
			else
			{
				MessageClient(%cl, 'GuildMatch', %challenger.getName() SPC "has failed to take" SPC %zone.description SPC "from" SPC %this.getName() @ "!");
			}
		}
	}
	else
	{
		%challenger.takezone(%this, %zone);
		//away wins, notify players reset PVP flags
	}
	

	

	ResetZoneVars(%zone, %this, %challenger);
}
function ResetZoneVars(%zone, %owner, %challenger)
{
	%zone.owner.challengedzone = "";
	%zone.challenger.challengedzone = "";
	%zone.minchallenger = "";
	%zone.home = "";
	%zone.away = "";
	if(IsEventPending(%zone.challengevent))
		cancel(%zone.challengeevent);
	%zone.challenger = "";
	%zone.challenged = "";
	%zone.fightinprogress = "";
	%zone.canacceptchallenge = "";
	%count = ClientGroup.getCount();
	if(%owner || %challenger)
	for(%icl = 0; %icl < %count; %icl++)
	{
		if(%challenger.GUIDinGuild(%cl.guid) && %challenger.GetGUIDRank(%cl.guid) > 0  )
		{

			%cl.enemyguild = "";
			%cl.guildmatchpvp = "";
			%cl.participate = "";
		}
		else
		if(%owner.GUIDinGuild(%cl.guid)  && %owner.getGUIDRank(%cl.guid) > 0 )
		{

			%cl.enemyguild = "";
			%cl.guildmatchpvp = "";
			%cl.participate = "";
		}

	}
	%owner.challengedzone = "";
}