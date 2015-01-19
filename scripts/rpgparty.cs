$maxpartymembers = 4;

function CreateParty(%client)
{
	if(fetchData(%client, "partyOwned"))
	{
		DisbandParty(%client);
	}

	messageClient(%client, 'PartyCallback', "You have created a new party.");
	storeData(%client, "partyOwned", true);

	AddToParty(%client, %client);
}

function DisbandParty(%client)
{
	storeData(%client, "partyOwned", "");

	%list = fetchData(%client, "partylist");
	for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
	{
		%w = getsubstr(%list, 0, %p);
		
		RemoveFromParty(%client, %w, true);
	}

	messageClient(%client, 'PartyCallback', "Your party has been disbanded.");
}

function RemoveFromParty(%client, %id, %optional)
{
	//%id = getClientByName(%name);

	if(%id !$= "")
	{
		if(%client !$= %id)
			messageClient(%id, 'PartyCallback', "You are no longer in " @ %client.rpgname @ "'s party.");
		else
			messageClient(%id, 'PartyCallback', "You have left your party.");
	}

	storeData(%client, "partylist", RemoveFromCommaList(fetchData(%client, "partylist"), %id));

	%list = fetchData(%client, "partylist");
	for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
	{
		%cl = getsubstr(%list, 0, %p);
		//%cl = getClientByName(%w);
		if(%id !$= %cl && %id !$= %client)
			messageClient(%cl, 'PartyCallback', %id.rpgname @ " is no longer in your party.");
	}

	if(!%optional)
	{
		if(CountObjInCommaList(fetchData(%client, "partylist")) <= 0)
			DisbandParty(%client);
	}
}

function AddToParty(%client, %id)
{
	//%id = getClientByName(%name);

	if(%id !$= "")
	{
		if(%client !$= %id)
			messageClient(%id, 'PartyCallback', "You are now in " @ %client.rpgname @ "'s party.");
		else
			messageClient(%id, 'PartyCallback', "You have joined your party.");
	}

	storeData(%client, "partylist", AddToCommaList(fetchData(%client, "partylist"), %id));

	%client.invitee[%id] = "";

	%list = fetchData(%client, "partylist");
	for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
	{
		%cl = getsubstr(%list, 0, %p);
		//%w = getClientByName(%w);
		if(%id !$= %cl && %id !$= %client)
			messageClient(%cl, 'PartyCallback', %id.rpgname @ " has joined your party.");
	}
}

function IsInWhichParty(%client)
{
	//%client = getClientByName(%name);

	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%id = ClientGroup.getObject(%icl);

		if(fetchData(%id, "partyOwned"))
		{
			if(IsInCommaList(fetchData(%id, "partylist"), %client))
				return %id;
			//else
			//{
			//	if(%id.invitee[%client])
			//		return %id @ " i";
			//}
		}
	}

	return false;
}

function GetPartyListIAmIn(%client)
{
	%p = IsInWhichParty(%client);
	
	%id = firstWord(%p);
	//%inv = GetWord(%p, 1);

	if(%id !$= "")
		return fetchData(%id, "partylist");
	else
		return false;
}
