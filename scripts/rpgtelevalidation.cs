function ValidateBasicDestination(%params) {
    %params = strlwr(%params);
	return (%params $= "town" || %params $= "dungeon");
}

function ValidateDestination(%params) {
    %params = strlwr(%params);
	%tele = 0;
	for(%i = 0; %i < towns.GetCount(); %i++) {
		%obj = towns.getObject(%i);
		if(strlwr(%obj.transname) $= %params)
		   %tele = %obj;
        //missed here, check for re-validated
        if(strlwr(%obj.transname) $= $TransConv[%params])
           %tele = %obj;
	}
	for(%i = 0; %i < dungeons.GetCount(); %i++) {
		%obj = dungeons.getObject(%i);
		if(strlwr(%obj.transname) $= %params)
		   %tele = %obj;
        //missed here, check for re-validated
        if(strlwr(%obj.transname) $= $TransConv[%params])
           %tele = %obj;
	}
	if(isobject(customtele)) //for custom maps that want teleports where the teleport spell cannot go.
	for(%i = 0; %i < customtele.GetCount(); %i++) {
		%obj = customtele.getObject(%i);
		if(strlwr(%obj.transname) $= %params)
		   %tele = %obj;
        //missed here, check for re-validated
        if(strlwr(%obj.transname) $= $TransConv[%params])
           %tele = %obj;
	}
	if(%tele == 0)
		return false;
	else
		return true;
}

function ValidateGuildDestination(%client, %params) {
    %params = strlwr(%params);
	%guildid = IsInWhatGuild(%client);
	if(%guildid == -1) return false;
	%guild = GuildGroup.GetObject(%guildid);
	%tele = 0;
	for(%i = 0; %i < guildtele.getCount(); %i++) {
		%obj = guildtele.getObject(%i);
		if(strlwr(%obj.transname) $= %params)
		   %tele = %obj;
        //missed here, check for re-validated
        if(strlwr(%obj.transname) $= $TransConv[%params])
           %tele = %obj;
		%zone = %tele.zoneobjname;
	}
	if(%tele == 0)
		return false;
	if(%guild.GUIDinGuild(%client.guid) && %guild.getguidrank(%client.guid) >= 1 && %guild.ownZone(%zone))
		return %tele;
	return false;
}

//globals
$TransConv["keldrin town"] = "keldrintown";
$TransConv["keldrin"] = "keldrintown";
$TransConv["town"] = "keldrintown";     //we assume it's the well known "first" town
$TransConv["jaten"] = "jatenoutpost";
$TransConv["jaten outpost"] = "jatenoutpost";
$TransConv["outpost"] = "jatenoutpost";
$TransConv["ethren"] = "ethrenkeep";
$TransConv["ethren keep"] = "ethrenkeep";
$TransConv["keep"] = "ethrenkeep";
$TransConv["delkin"] = "delkinport";
$TransConv["delkin port"] = "delkinport";
$TransConv["port"] = "delkinport";
$TransConv["balan village"] = "balanvillage";
$TransConv["balan"] = "balanvillage";
$TransConv["village"] = "balanvillage";
//
$TransConv["mines"] = "keldrinmines";
$TransConv["keldrin mines"] = "keldrinmines";
$TransConv["ogre forest"] = "ogreforest";
$TransConv["elven forest"] = "elvenforest";
$TransConv["mino lair"] = "minotaurslair";
$TransConv["minolair"] = "minotaurslair";
$TransConv["lair"] = "minotaurslair";
$TransConv["minotaurs lair"] = "minotaurslair";
$TransConv["travelers den"] = "travelersden";
$TransConv["den"] = "travelersden";
//
