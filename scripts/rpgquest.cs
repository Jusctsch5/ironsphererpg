//Quest file, contains all quest support functions
function spawnquestbot(%pos, %team, %race, %functioncall, %name, %qname)
{
	//note when anyone says anything within the radius of this bot, functioncall will be called like this
	//function(%botid, %client, %text), from there the quest bot will operate. All questing functions should be defined in the mission cs file.
	//over time i will create a bunch of simple support functions for these bots.
	//NOTE: all functions called by quest bots will always start with QUEST see example
	%armor = "MaleHumanArmor";
	%player = new Player()
	{
	
		datablock = %armor;
		//class = quest;//change class maybe.
	};
	$townbotlist = %player SPC $townbotlist;
	%player.setTransform(%pos);
	%player.name = %name;
	%player.namebase = %name;
	%player.istownbot = true;
	%player.team = %team;
	%player.mtype = "Quest";
	%player.cfunction = %functioncall;
	if(!isobject(QuestGroup))

	{
		new SimGroup(QuestGroup) {
		
		};
		MissionCleanup.add(QuestGroup);
	}
	%questobj = Quest @ %qname;
	if(!isobject(%questobj))
	{
		new ScriptObject(%questobj) {
			class = "quest";
		};
		QuestGroup.add(%questobj);
		
		
	}
	%questobj.addbot(%player);
	%player.qobj = %questobj;
	%questobj.init(Quest @ %qname);
	return %player;
}

//example
//spawnquestbot( "1032 309 123", 1, "MaleHuman", "newbiebottalk", "helper")
//function Questnewbiebottalk(%botid, %client, %text)

function isgreeting(%cropped)
{
	%inittalk = false;
	for(%i = 0; (%w = GetWord("hail hello hi greetings yo hey sup salutations g'day howdy", %i)) !$= ""; %i++)
			if(stricmp(%cropped, %w) $= 0)
				%initTalk = true;
	return %inittalk;
}

function Quest::addbot(%this, %player)
{
	%this.botlist = %this.botlist @ %player @ " ";
}
function Quest::SetState(%this, %botid, %client, %state)
{
	%this.state[%botid, %client] = %state;
}
function Quest::GetState(%this, %botid, %client)
{
	if (%this.state[%botid, %client] > 0)
	return %this.state[%botid, %client];
	else
	return 0;
}
function Quest::Respond(%this, %botid, %client, %text)
{
	MessageClient(%client, 'BotTalk', %botid.name @ " says \"" @ %text @ "\"");
}
function Quest::SaveClient(%this, %client, %fileobj)
{
	//for saving to file
	for(%i = 0; GetWord(%this.botlist, %i) !$= ""; %i++)
	{
		%botid = GetWord(%this.botlist, %i);
		%client.data.questdata[$currentmission, %this.objname, %i] = %this.getstate(%botid, %client);

	}
	return;

	//%fileobj.writeline(%this.objname @ ".loadclient(%client, " @ %i @ ", " @ %this.getState(GetWord(%this.botlist, %i), %client) @ ");");
}
function Quest::Load(%this, %client)
{
	for(%i = 0; GetWord(%this.botlist, %i) !$= ""; %i++)
	{
		%botid = GetWord(%this.botlist, %i);
		%this.LoadClient(%client, %i, %client.data.questdata[$currentmission, %this.objname,%i]);
	}
}
function Quest::LoadClient(%this, %client, %botnum, %state)
{
	%this.state[GetWord(%this.botlist, %botnum), %client] = %state;
}
function Quest::giveReward(%this, %botid, %client, %reward)
{
	GiveThisStuff(%client, %reward, true);
}
function Quest::ClientHasItem(%quest, %client, %item, %amount, %prefix, %suffix)
{
	if(%prefix == 0) %prefix = 3;
	if(%suffix == 0) %suffix = 1;
	return %client.data.invcount[%item, %prefix, %suffix] >= %amount;
}
function Quest::TakeItem(%quest, %botid, %client, %item, %amount, %prefix, %suffix)
{
	if(%prefix == 0) %prefix = 3;
	if(%suffix == 0) %prefix = 1;
	Game.RemoveFromInventory(%client, %amount, %item, %prefix, %suffix);
	
	MessageClient(%client, 'BotTalk', "You give" SPC %botid.name SPC %amount SPC %item @ ".");
	return;
}
function Quest::GiveItem(%quest, %botid, %client, %item, %prefix, %suffix, %amount)
{
	AddToInventory(%client, %amount, %item, %prefix, %suffix);
	MessageClient(%client, 'BotTalk', %botid.name SPC "gave you" SPC %amount SPC %item @ ".");
	return;
}
function Quest::init(%this, %objname)
{
	%this.objname = %objname;
}
function Quest::GetBotByIndex(%this, %index)
{
	return GetWord(%this.botlist, %index-1);
}