function setupshop(%client, %aiId)
{
	commandToClient(%client, 'toggleshopHud', true);
	%client.currentai =  %aiId;
	%client.shop = true;
	%client.bank = false;
	RPGshopScreen::updateHud(1,%client, 'RPGshopScreen', %aiId);
}
function setupbank(%client, %aiid)
{
	
	%client.shop = false;
	%client.currentai = %aiid;
	%client.bank = true;
	commandToClient(%client, 'toggleBank', true);
	storedata(%client, "coins", 0, "inc");
	commandToClient(%client, 'setbankcoins', fetchData(%client, "bank"));
	
}
function RPGshopScreen::updateHud(%this, %client, %tag, %aiId)
{
	//echo("RPGshopScreen::updateHud(" @ %this @ ", " @ %client @ ", " @ %tag @ "," SPC %aiId @ ")");
	commandToClient(%client, 'ShopUpdateHud');
	//Server-side
	//%inv = fetchData(%client, "inventory");
	%coins = fetchdata(%client, "COINS");
	commandToClient(%client, 'SetCoins', %coins);
	%val = 0;
	for(%i = 0; (%item = GetWord(%aiId.shopwloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0,$ItemDesc[%item],%item);
	}
    commandToClient(%client, 'ShopAddRow',0, "***************", '');
	for(%i = 0; (%item = GetWord(%aiId.shopaloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0 ,$ItemDesc[%item],%item);
	}
    commandToClient(%client, 'ShopAddRow',0, "***************", '');
	for(%i = 0; (%item = GetWord(%aiId.shopiloadout, %i)) !$= ""; %i++)
	{
		commandToClient(%client, 'ShopAddRow',0 ,$ItemDesc[%item],%item);
	}
    commandToClient(%client, 'ShopAddRow',0, "***************", '');
	commandToClient(%client, 'ShopDone');
	//clientCmdShopAddRow(%itemId, %fullitem, %name, %amt, %type)
}
function GetItemCost(%client, %item)
{
	%skill = GetPlayerSkill( %client, $skill::haggling);
	%multi =  1 - ( ((%skill) / (%skill + 500)) / 3);
	%price = mfloor($Shop::BuyPrice[%item]*%multi);
	return %price;
}
function GetItemValue(%client, %item)
{
	%skill = GetPlayerSkill( %client, $skill::haggling);
	%multi =  1 + ( ((%skill) / (%skill + 500)) / 3);
	if($Shop::SellPrice[%item] > 0)
	%sp = $shop::SellPrice[%item]*100;
	else
	%sp = $shop::BuyPrice[%item];
	%price = mfloor(%sp/100 * %multi );
	if(%price <= 0) %price = 1;
	
	return %price;
}
function Client::OnBuy(%client,%item, %delta)
{
	
	if(!%client.shop) return;
	%dist = VectorDist(%client.player.getposition(), %client.currentai.getposition());
	if(%dist > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50))) return;//duh morons
	%delta = mfloor(%delta);
	if(%delta <= 0) return; //die
	%price = GetItemCost(%client, %item);

	%coins = fetchData(%client, "COINS");
	if(mfloor(%price * %delta) <= %coins)
	{
		%pre = 3;
		%suf = 1;
		
		Game.AddToInventory(%client, %delta, %item, %pre, %suf);
		for(%i = 0; %i < %delta; %i++)
		{
			UseSkill(%client, $skillHaggling, true, true);
		}
		%coins = mfloor(%coins - %price * %delta);
		storedata(%client, "COINS", %coins);	
		MessageClient(%client, 'ShopMessage', 'You have bought %1 %2 for %3 coins.', %delta, %item, %price * %delta);
	}
	else
		MessageClient(%client, 'ShopMessage', 'You do not have enough money to buy %3 %1 you need %2 coins.', %item, %price * %delta, %delta);
}
function Client::OnSell(%client, %item, %delta)
{
	%itemid = %item;
	%coins = fetchData(%client, "COINS");
	%suffix = game.GetSuffix(%client, %itemid);
	%prefix = game.GetPrefix(%client, %itemid);
	%item = game.GetItem(%client, %itemid);
	
	%totalitems = game.getItemCount(%client, %item, %prefix, %suffix);
	
	if(!%client.shop) return;
	%delta = mfloor(%delta);
	
	if(%delta <= 0) 
	{

		MessageClient(%client, 'ShopMessage', 'You have %1 %2.', %totalitems, game.getfullitemname(%prefix, %item, %suffix));
		return;
	}
	if(VectorDist(%client.player.getposition(), %client.currentai.getposition()) > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50)))
	{
		//clientOnLeaveShop(%client);
		return;//duh morons
	}
	
	
	if(%delta > %totalitems) %delta = %totalitems;
	if(%delta > 0)
	{
		
		//%itemname = Game.GetFullItemName(%item, %prefix, %suffix);
		%TotalSale = 0;
		%price = GetItemValue(%client,%item);
		if(%client.delta.equipped[%item] && %delta == %totalitems)
			InventoryUse(%client, %itemid);//unequip.
		Game.RemoveFromInventory(%client, %delta, %item, %prefix, %suffix);
		%totalsale = %delta*%price;
		MessageClient(%client, 'ShopMessage', 'You have sold %1 %2. You have %3 left.', %delta, game.getfullitemname(%prefix, %item, %suffix), %totalitems-%delta);
		
		

		storedata(%client, "COINS", %coins + %totalSale);
	}
}
function serverCmdBankonDeposit(%client, %amount)
{
	if(!%client.bank) return false;
	if(%amount < 0) return false;
	if(VectorDist(%client.player.getposition(), %client.currentai.getposition()) > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50)))
	{
		//clientOnLeaveShop(%client);
		return;//duh morons
	}
	if(fetchData(%client, "coins") >= %amount)
	{
		storedata(%client, "coins", %amount, "dec");
		storedata(%client, "bank", %amount, "inc");
	}
	
	commandToClient(%client, 'setbankcoins', fetchData(%client, "bank"));
}
function serverCmdBankonWithdraw(%client, %amount)
{
	if(!%client.bank) return false;
	if(%amount < 0) return false;
	if(VectorDist(%client.player.getposition(), %client.currentai.getposition()) > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50)))
	{
		//clientOnLeaveShop(%client);
		return;//duh morons
	}
	if(fetchData(%client, "bank") >= %amount)
	{
		storedata(%client, "bank", %amount, "dec");
		storedata(%client, "coins", %amount, "inc");
	}

	commandToClient(%client, 'setbankcoins', fetchData(%client, "bank"));
}
function serverCmdBankonBuy(%client, %itemid, %delta)
{
	//this gets called when a client withdraws an item from the bank.
	//echo(%client.rpgname SPC %item SPC %amount SPC "BUY");	
	%item = Game.GetItem(%client, %itemid);
	%prefix = Game.GetPrefix(%client, %itemid);
	%suffix = Game.GetSuffix(%client, %itemid);
	%fullname = Game.Getfullitemname(%prefix, %item, %suffix);

	%num = game.getBankCount(%client, %item, %prefix, %suffix);
	
	if(!%client.bank) return false;
	if(%itemid == -1) return false;
	if(%amount < 0 ) return false;
	if(%delta == 0)
	{
		MessageClient(%client, 'BankMessage', 'You have %1 %2 in the bank', %num, %fullname);
		return;
	}
	if(VectorDist(%client.player.getposition(), %client.currentai.getposition()) > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50)))
	{
		return;
	}
	
	%delta = mfloor(%delta);
	if(%delta < 1) %delta = 1;
	if(%delta > %num) %delta = %num;
	Game.AddToInventory(%client, %delta, %item, %prefix, %suffix);
	Game.RemoveFromStorage(%client, %delta, %item, %prefix, %suffix);
	MessageClient(%client, 'BankMessage', 'You withdraw %1 %2 from the bank, there are %3 remaining', %delta, %fullname, %num-%delta);
	

	
}
function serverCmdBankonSell(%client, %itemid, %delta)
{
	//this one gets called when a client deposits an item.
	%item = Game.GetItem(%client, %itemid);
	%prefix = Game.GetPrefix(%client, %itemid);
	%suffix = Game.GetSuffix(%client, %itemid);
	%fullname = Game.Getfullitemname(%prefix, %item, %suffix);
	
	%num = game.getItemCount(%client, %item, %prefix, %suffix);
	
	if(!%client.bank) return false;
	%delta = mfloor(%delta);

	if(%delta <= 0 ) %delta = 0;	
	
	if(VectorDist(%client.player.getposition(), %client.currentai.getposition()) > ($maxAIdistVec + (%client.PlayerSkill[$SkillSpeech] / 50)))
	{
		return;
	}
	if(%delta > %num) %delta = %num;
	if(%delta == 0)
	{
		MessageClient(%client, 'BankMessage', 'You have %1 %2 in your inventory.', %num, %fullname);
		return;
	}
	Game.AddToStorage(%client, %delta, %item, %prefix, %suffix);
	Game.RemoveFromInventory(%client, %delta, %item, %prefix, %suffix);
	MessageClient(%client, 'BankMessage', 'You deposit %1 %2 into the bank, you have %3 left', %delta, %fullname, %num-%delta);	

}
function ClientOnLeaveShop(%client)
{
%client.shop = false;
%client.currentai = "";

}
function serverCmdShopLeave(%client)
{
	if(%client.shop)
		ClientOnLeaveShop(%client);
	if(%client.bank)
		ClientOnLeaveBank(%client);
}
function ClientOnLeaveBank(%client)
{
	%client.bank = false;
	%client.currentai = "";
}
//%pricemulti = 1 - ( ((%skill) / (%skill + 500)) / 3); //buy
//%pricemulti = 1 + ( ((%skill) / (%skill + 500)) / 3); // sell
