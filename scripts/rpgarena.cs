//RPG Arena system!

function AddToRoster(%client)
{
MessageClient(%client, 'ArenaMessage', "Sorry, arena is closed at the moment.");
return;//arena closed atm.

	if(!Isobject(Arena))
		initArena();
	
	if(!%client.player)
		return 0;
	if(iseventpending(%client.spellcast))
		cancel(%client.spellcast);
	%client.player.unmountImage($weaponSlot);
	if(Arena.Wait1 == 0)
	{
		UnequipEverything(%client);
		storedata(%client, "tmpInv", fetchdata(%client, "inventory"));
		storedata(%client, "inventory", "");
		if(!%client.isaiControlled())
		commandToClient(%client, 'InventoryUpdateHud');
		//teleport user to ArenaWait1
		%trans = WaitRoom1.getTransform();
		%client.player.setPosition(%trans);
		//storedata(%client, "inarena", true);
		Arena.wait1 = %client;

		
		if(Arena.Wait2 != 0)

		{
			ArenaReadyMatch();
		}
		return 1;
	}
	if(Arena.wait2 == 0)
	{
		UnequipEverything(%client);
		storedata(%client, "tmpInv", fetchdata(%client, "inventory"));
		storedata(%client, "inventory", "");
		if(!%client.isaiControlled())
		commandToClient(%client, 'InventoryUpdateHud');
		%trans = WaitRoom2.getTransform();
		%client.player.setPosition(%trans);
		//storedata(%client, "inarena", true);
		Arena.wait2 = %client;
		if(Arena.Wait1 != 0)
		{
			ArenaReadyMatch();
		}		

		return 2; 
	}
	
	return 0;
}

function UnequipEverything(%client)
{
	%inv = fetchdata(%client, "inventory");
	for(%i = 0; GetWord(%inv, %i) !$= ""; %i++)
	{
		$InvInfo[GetWord(%inv, %i), equipped] = false;
	}
}
function RemoveFromRoster(%client)
{
	if(Arena.wait1 == %client)
	Arena.wait1 = 0;
	if(Arena.wait2 == %client)
	Arena.wait2 = 0;

}
function inarenabattle(%client)
{
	if(Arena.battle1 == %client || Arena.battle2 == %client)
		return true;
	return false;
}
function inArenaroster(%client)
{
	if(Arena.wait1 == %client || Arena.wait2 == %client)
		return true;
	return false;
}
function inArena(%client)
{
	return (inarenabattle(%client) || inarenaroster(%client));
}
function LeaveArena(%client)
{
	if(Arena.wait1 == %client)
	{
		Arena.wait1 = 0;
		storedata(%client, "inventory", fetchdata(%client, "tmpinv"));
		storedata(%client, "tmpinv", "");
		commandToClient(%client, 'InventoryUpdateHud');
		if(iseventPending(Arena.startmatch))
		{
			cancel(Arena.startmatch);
			MessageClient(Arena.wait2, 'ArenaMsg', "Your opponent has chickened out, waiting for a new opponent");
		}
		if(isEventPending(Arena.readymatch))
		{
			cancel(Arena.readymatch);
		}
		%flag = true;
		%marker = 1;
	}
	if(Arena.wait2 == %client)
	{
		Arena.wait2 = 0;
		storedata(%client, "inventory", fetchdata(%client, "tmpinv"));
		storedata(%client, "tmpinv", "");
		commandToClient(%client, 'InventoryUpdateHud');
		if(iseventPending(Arena.startmatch))
		{
			cancel(Arena.startmatch);
			MessageClient(Arena.wait1, 'ArenaMsg', "Your opponent has chickened out, waiting for a new opponent");
		}
		if(isEventPending(Arena.readymatch))
		{
			cancel(Arena.readymatch);
		}
		%flag = true;
		%marker = 2;
	}
	if(Arena.battle1 == %client)
	{
		BroadcastToArena( Arena.battle2.rpgname SPC "has defeated" SPC Arena.battle1.rpgname @ "!", %client);
		RemoveArenaItems(Arena.battle1);
		
		RemoveArenaItems(Arena.battle2);
		Arena.battle2.player.unmountImage($weaponSlot);
		Arena.battle1.player.unmountImage($weaponSlot);
		storedata(%client, "inventory", fetchdata(%client, "tmpinv"));
		storedata(%client, "tmpinv", "");
		commandToClient(%client, 'InventoryUpdateHud');
		storedata(Arena.battle2, "inventory", fetchdata(Arena.battle2, "tmpinv"));
		storedata(Arena.battle2, "tmpinv", "");
		commandToClient(Arena.battle2, 'InventoryUpdateHud');
		%flag = true;
		%marker = 1;
		Arena.Battle2.player.setPosition(ArenaExit2.getTransform());
		Arena.battle2 = 0;
		Arena.battle1 = 0;
	}
	if(Arena.battle2 == %client)
	{
		BroadcastToArena( Arena.battle1.rpgname SPC "has defeated" SPC Arena.battle2.rpgname @ "!", %client);
		
		RemoveArenaItems(Arena.battle1);
		
		RemoveArenaItems(Arena.battle2);
		Arena.battle2.player.unmountImage($weaponSlot);
		Arena.battle1.player.unmountImage($weaponSlot);
		storedata(%client, "inventory", fetchdata(%client, "tmpinv"));
		storedata(%client, "tmpinv", "");
		commandToClient(%client, 'InventoryUpdateHud');
		storedata(Arena.battle1, "inventory", fetchdata(Arena.battle1, "tmpinv"));
		storedata(Arena.battle1, "tmpinv", "");
		commandToClient(Arena.battle1, 'InventoryUpdateHud');
		%flag = true;
		%marker = 2;
		Arena.Battle1.player.setPosition(ArenaExit1.getTransform());
		Arena.battle2 = 0;
		Arena.battle1 = 0;//clear!
	}	
	if(%flag)
	{
		if(%marker == 1)
		%client.player.setTransform(ArenaExit1.getTransform());
		if(%marker == 2)
		%client.player.setTransform(ArenaExit2.getTransform());
	}
}
function ArenaReadyMatch()
{
	if(Arena.battle1 == 0 && Arena.battle2 == 0)
	{
		MessageClient(Arena.wait1, 'ArenaReady', "Get Ready to FIGHT!");
		MessageClient(Arena.wait2, 'ArenaReady', "Get Ready to FIGHT!");
		Arena.startmatch = schedule(15000, Arena, "TeleportinArena");
		commandtoclient(Arena.wait1, 'StartRecastDelayCountdown', 15000);
		commandtoclient(Arena.wait2, 'StartRecastDelayCountdown', 15000);
		
	}
	else
	Arena.readymatch = schedule(5000, Arena, "ArenaReadyMatch");
}

function TeleportinArena()
{
	%trans1 = BattleEnter1.getTransform();
	%trans2 = BattleEnter2.getTransform();
	Arena.wait1.player.setPosition(%trans1);
	Arena.wait2.player.setPosition(%trans2);
	MessageClient(Arena.wait1, 'ArenaMessage', "FIGHT!!");
	MessageClient(Arena.wait2, 'ArenaMessage', "FIGHT!!");
	BroadcastToArena(Arena.wait1.rpgname SPC "and" SPC Arena.wait2.rpgname SPC "have started their duel!",Arena.wait1 );
	AddArenaItems(Arena.Wait1, 1);
	AddArenaItems(Arena.wait2, 2);
	SetHP(Arena.wait1, fetchdata(Arena.wait1, "MaxHP"));
	SetMana(Arena.wait1, fetchdata(Arena.wait1, "MaxMana"));
	SetHP(Arena.wait2, fetchdata(Arena.wait2, "MaxHP"));
	SetMana(Arena.wait2, fetchdata(Arena.wait2, "MaxMana"));
	ShapeBase::cycleWeapon(Arena.wait2.player, "next");
	ShapeBase::cycleWeapon(Arena.wait1.player, "next");

	Arena.battle1 = Arena.wait1;
	Arena.battle2 = Arena.wait2;
	Arena.wait1 = 0;
	Arena.wait2 = 0;
}
function BroadcastToArena(%msg, %base)
{
	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(fetchData(%cl, "zone") $= fetchData(%base, "zone"))
		messageClient(%cl, 'RPGchatCallback', "[Arena] " @ %msg @ "\"");
	}
}
function AddArenaItems(%client, %val)
{
	if(%val == 1)
	{
		AddToInventory(%client, Arena.weapon1_1);
		AddToInventory(%client, Arena.weapon1_2);
		AddToInventory(%client, Arena.weapon1_3);
		AddToInventory(%client, Arena.weapon1_4);
		AddToInventory(%client, Arena.weapon1_5);
		AddToInventory(%client, Arena.weapon1_6);
		AddToInventory(%client, Arena.weapon1_7);
		AddToInventory(%client, Arena.weapon1_8);
		AddToInventory(%client, Arena.weapon1_9);
		AddToInventory(%client, Arena.weapon1_10);
		AddToInventory(%client, Arena.weapon1_11);
		AddToInventory(%client, Arena.weapon1_12);
		AddToInventory(%client, Arena.weapon1_13);
		AddToInventory(%client, Arena.weapon1_14);
		AddToInventory(%client, Arena.weapon1_15);
	}
	if(%val == 2)
	{
		AddToInventory(%client, Arena.weapon2_1);
		AddToInventory(%client, Arena.weapon2_2);
		AddToInventory(%client, Arena.weapon2_3);
		AddToInventory(%client, Arena.weapon2_4);
		AddToInventory(%client, Arena.weapon2_5);
		AddToInventory(%client, Arena.weapon2_6);
		AddToInventory(%client, Arena.weapon2_7);
		AddToInventory(%client, Arena.weapon2_8);
		AddToInventory(%client, Arena.weapon2_9);
		AddToInventory(%client, Arena.weapon2_10);
		AddToInventory(%client, Arena.weapon2_11);
		AddToInventory(%client, Arena.weapon2_12);
		AddToInventory(%client, Arena.weapon2_13);
		AddToInventory(%client, Arena.weapon2_14);
		AddToInventory(%client, Arena.weapon2_15);
	}

}
function RemoveArenaItems(%client)
{
	%inv = fetchdata(%client, "inventory");
	
	for(%i = 1; GetWord(%inv, %i) !$= ""; %i++)
	{
	
	RemoveFromInventory(%client,GetWord(%inv, %i));
	}

}
function initArena()
{
	if(!Isobject(Arena))
	{
		new ScriptObject(Arena) {
			class = "Arena";
			WaitRoom1 = 0;
			WaitRoom2 = 0;
			Battle1 = 0;
			Battle2 = 0;
			weapon1_1 = CreateItem("Club", 6, 1);
			weapon1_2 = CreateItem("Knife", 6, 1);
			weapon1_3 = CreateItem("Hatchet", 6, 1);
			weapon1_4 = CreateItem("Sling", 6, 1);
			weapon1_5 = CreateItem("SmallRock", 0, 0);
			weapon1_6 = CreateItem("SmallRock", 0, 0);
			weapon1_7 = CreateItem("SmallRock", 0, 0);
			weapon1_8 = CreateItem("SmallRock", 0, 0);
			weapon1_9 = CreateItem("SmallRock", 0, 0);
			weapon1_10 = CreateItem("SmallRock", 0, 0);
			weapon1_11 = CreateItem("SmallRock", 0, 0);
			weapon1_12 = CreateItem("SmallRock", 0, 0);
			weapon1_13 = CreateItem("SmallRock", 0, 0);
			weapon1_14 = CreateItem("SmallRock", 0, 0);
			weapon1_15 = CreateItem("SmallRock", 0, 0);
			
			weapon2_1 = CreateItem("Club", 6, 0);
			weapon2_2 = CreateItem("Knife", 6, 0);
			weapon2_3 = CreateItem("Hatchet", 6, 0);
			weapon2_4 = CreateItem("Sling", 6, 1);
			weapon2_5 = CreateItem("SmallRock", 0, 0);
			weapon2_6 = CreateItem("SmallRock", 0, 0);
			weapon2_7 = CreateItem("SmallRock", 0, 0);
			weapon2_8 = CreateItem("SmallRock", 0, 0);
			weapon2_9 = CreateItem("SmallRock", 0, 0);
			weapon2_10 = CreateItem("SmallRock", 0, 0);
			weapon2_11 = CreateItem("SmallRock", 0, 0);
			weapon2_12 = CreateItem("SmallRock", 0, 0);
			weapon2_13 = CreateItem("SmallRock", 0, 0);
			weapon2_14 = CreateItem("SmallRock", 0, 0);
			weapon2_15 = CreateItem("SmallRock", 0, 0);

		};
		MissionCleanup.add(Arena);
	}
}