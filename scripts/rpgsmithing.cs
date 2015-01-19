function RPGGame::Smith(%game, %client, %command, %item, %from)
{

	if(%command $= "New" || %command $= "Make")
	{
		%smithlist = $item::smith[%item, 0];
		%nprefix = 3;
		%upgrade = false;
	}
	else if(%command $= "upgrade")
	{
		%upgrade = true;
		%nprefix = 4;
		
		if( %from $= "" || %from $= "normal")
		%nprefix = 4;
		else if(%from $= "Worn" || %from $= "Weak")
		%nprefix = 3;
		else if(%from $= "Broken" || %from $= "Old")
		%nprefix = 2;
		else if(%from $= "Fine" || %from $= "Hardened")
		%nprefix = 5;
		else if(%from $= "Mighty" || %from $= "Sturdy")
		%nprefix = 6;
		%smithlist = rtrim($item::smith[%item, %nprefix-1]) @ " " @ %nprefix-1 @ "%" @ %item @ "%1 1";
		
		//%nprefix++;
	}
	if(%smithlist $= "" || (%upgrade && $item::smith[%item, %nprefix-1] $= "" ))
	{
		MessageClient(%client, 'FailSmith', "Invalid item, or invalid parameters: #smith [New or Upgrade] [ItemName] ([item prefix]), item prefix is used only with the upgrade command");
		return false;
	}
	%check = false;
	%flag = true;

	%totalitems = 0;
	for(%i = 0; %i < CountWords(%smithlist); %i = %i+2)
	{
		%iteml = strreplace(getWord(%smithlist, %i), "%", " ");
		%amt = getWord(%smithlist, %i+1);

		%pre = getWord(%iteml, 0);
		%niteml = getWord(%iteml, 1);
		%suf = getword(%iteml, 2);

		if( %niteml $= "")
		{
			%niteml = %pre;
			%pre = 3;
			%suf = 1;
		}
		if(%game.getItemCount(%client, %niteml, %pre, %suf) < %amt)
		{
			%flag = false;
		}
	}
	if(%flag)
	{
		if(%upgrade)
		{
			%full = Game.getFullItemName(%nprefix, %item, 1);
			%uid = %client.data.count[strreplace(%full, " ", "x")];
			if(%client.data.equipped[%uid])
				Game.InventoryUse(%client, %uid);
			
		}	
		for(%i = 0; %i < CountWords(%smithlist); %i = %i+2)
		{
			%iteml = strreplace(getWord(%smithlist, %i), "%", " ");
			%amt = getWord(%smithlist, %i+1);

			%pre = getWord(%iteml, 0);
			%niteml = getWord(%iteml, 1);
			%suf = getWord(%iteml, 2);
			if( %niteml $= "")
			{
				%niteml = %pre;
				%pre = 3;
				%suf = 1;
			}
			%game.removeFromInventory(%client, %amt, %niteml, %pre, %suf);
		}



		%id = %game.AddToInventory(%client, 1, %item, %nprefix, 1, false);
		MessageClient(%client, 'RPGchatCallback', 'You successfully smithed a %2 %1 %3', GetPrefixName(%client, %id), %item, "");
	}
	else
	{
		if(!%upgrade)
			MessageClient(%client, 'RPGchatCallback', 'You do not have the required items to smith, you need: %1', strreplace(%smithlist, "%", " "));
		else
			MessageClient(%client, 'RPGchatCallback', 'You do not have the required items to upgrade, you need: %1', strreplace(strreplace(strreplace(strreplace(strreplace(strreplace(%smithlist, "3%", ""), "2%", "Worn " ), "1%", "Broken "), "4%", "Fine "), "5%", "Mighty "), "%1", ""));
	}


}