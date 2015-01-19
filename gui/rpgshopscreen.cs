if(!isObject(RPGshopScreen))
exec("gui/RPGshopScreen.gui");
//------------------------------------------------------------------------------
function RPGshopScreen::loadHud( %this, %tag )
{
	echo("RPGshopScreen::loadHud(" @ %this @ ", " @ %tag @ ")");

	$sExeced = true;
	$Hud[%tag] = RPGshopScreen;
	$Hud[%tag].childGui = RPGSHOP_ROOT;
	$Hud[%tag].parent = RPGSHOP_ROOT;
}

//------------------------------------------------------------------------------
function RPGshopScreen::setupHud( %this, %tag )
{
	echo("RPGshopScreen::setupHud(" @ %this @ ", " @ %tag @ ")");

	exec("gui/rpgshopscreen.gui");
}
//---------------------------------------------------------

function ClientCmdtoggleshopHud( %val )
{
	RPGSHOP_ROOT.setValue("Shop");	
	$clshop::shop = true;
	$clshop::bank = false;
	Bank_cash.setvisible(false);
	B_withdraw.setvisible(false);
	B_Camount.setvisible(false);
	B_deposit.setvisible(false);
	B_coin_label.setvisible(false);
	Total_cost.setvisible(true);
	Total_cost_label.setvisible(true);
	Item_sell_price_label.setvisible(true);
	Sell_Price.setvisible(true);
   if ( %val )
   {
      //toggleCursorHuds('RPGshopScreen');
      Canvas.PushDialog(RPGshopScreen);
   if($inv::items == 0) commandToServer('RequestInvList');
   }
}
function ClientCmdtogglebank( %val )
{
	RPGSHOP_ROOT.setValue("Bank");
	$clshop::bank = true;
	$clshop::shop = false;
	Bank_cash.setvisible(true);
	B_withdraw.setvisible(true);
	B_Camount.setvisible(true);
	B_deposit.setvisible(true);
	B_coin_label.setvisible(true);
	Total_cost.setvisible(false);
	Total_cost_label.setvisible(false);
	Item_sell_price_label.setvisible(false);
	Sell_Price.setvisible(false);
	
	if ( %val )
	{
		deletevariables("$clshop::*");
		$clshop::bank = true;
		$clshop::shop = false;
		//toggleCursorHuds('RPGshopScreen');
		 Canvas.PushDialog(RPGshopScreen);
		if($inv::items == 0) commandToServer('RequestInvList');
		ShopList.renew();
	}

}
function clientCmdShopUpdateHud()
{
	
	ShopList.clear();
	$client::invstemp = "";
	%shop = $clshop::shop;
	%bank = $clshop::bank;
	deletevariables("$clshop::*");
	$clshop::shop = %shop;
	$clshop::bank = %bank;
}

function clientCmdShopAddRow(%itemId, %fullitem, %name, %amt, %type)
{
	if($clshop::bank)
	{

		if(!$clshop::has[%itemId])
		{
			ShopList.addRow(%itemId, %fullitem);
		}
		
		if(!$clshop::item[%fullitem]) $clshop::item[%fullitem] = 0;
		$clshop::itemlist[%fullitem, $clshop::item[%fullitem]] = %itemid;
		$clshop::item[%fullitem]++;
		$clshop::num++;
		
		$clshop::banklist[$clshop::num] = %itemId;
		$clshop::banklistfull[%itemId] = %fullitem;
		$clshop::type[%itemId] = %type;
		$clshop::name[%itemId] = %name;
		$clshop::have[%itemId] = 1;
		
	}
	else
		ShopList.addRow(%itemId, %name);
	
	echo(%name);
	
	$clshop::cvar[%name] = %fullitem;
	$clshop::has[%itemId] = true;
	if($clshop::bank)
		ShopList.Refresh();


}
function clientCmdRemoveFromBank(%itemId, %fullitem)
{
	//ShopList.renew();
	//return;
	
	if(!%itemid) return;
	%found = false;
	if($clshop::has[%itemid])
	{
		$clshop::have[%itemid] = false;
		$clshop::has[%itemid] = false;
		$clshop::item[%fullitem] = 0;
		
	}
	ShopList.Refresh();

	
}
function ShopList::renew(%this)
{
	//clientCmdShopUpdateHud();
	ShopList.clear();
	commandToServer('RefreshBank');
}
function ShopList::Refresh(%this)
{
	ShopList.clear();
	
	//deletevariables("$clshop::tempn*");
	for(%i = 1; $clshop::num+1 > %i; %i++)
	{		
		%item = $clshop::banklist[%i];
		
		if($clshop::has[%item])
		{			
				//clientCmdShopAddRow(%item, $clshop::banklistfull[%item], $clshop::name[%item], 1, $clshop::type[%item]);
			//%type = $clshop::Type[%item];			
			%fullname = $clshop::banklistfull[%item];							
			if(%tempn[%item] != 1)
				ShopList.addRow(%item, %fullname);
			%tempn[%item] = 1;
		}
	}


}
function clientCmdAddToBank(%itemid, %fullitem, %type, %et)
{
	clientCmdShopAddRow(%itemId, %fullitem, "", 1, %type);


}
function clientCmdSetBankCoins(%amount)
{
	Bank_Cash.setValue("Bank: " SPC %amount);
	Bank_Cash.amount = %amount;
}
function RPGshopScreen::onDeposit(%this)
{
	%amt = B_Camount.getValue();
	
	if(%amt > GetWord(scoins.getValue(),1))
		%amt = GetWord(scoins.getValue(),1);
	if(%amt < 0) return;	
	B_Camount.setValue(%amt);
	commandToServer('BankonDeposit',%amt);
}
function RPGshopScreen::onWithdraw(%this)
{
	%amt = B_Camount.getValue();
	if(%amt > Bank_Cash.amount)
		%amt = Bank_Cash.amount;
	if(%amt < 0) return;
	B_Camount.setValue(%amt);
	commandToServer('BankonWithdraw',%amt);
}
function RPGshopScreen::onSetB_Camount(%this)
{
	%amt = B_Camount.getValue();
	B_Camount.setValue(mfloor(%amt));
}
function RPGshopScreen::onbuy(%this)
{
	%amount = Samount.getValue();
	
	if($clshop::shop)
	{
	%item = $Client::invstemp;
	
	if(%item $= "") return;


	commandToServer('ShopOnBuy',$clshop::cvar[%item],%amount);
	}
	else
	{
	
	%itemid = ShopList.getselectedid();
	
	
	commandToServer('BankOnBuy', %itemid, %amount);
	}

}

function RPGshopScreen::onDone( %this )
{

	ShopList.clear();
	commandToServer('ShopLeave');
	//toggleCursorHuds( 'RPGshopScreen' );
	Canvas.popDialog(RPGshopScreen);
}
function RPGshopScreen::onSell(%this)
{
	%itemid = ShopInvList.getSelectedId();
	if(%itemid == 0 || %itemid $= "") return;
	%amount = Iamount.getValue();
	
	if($clshop::shop)
	{
		commandToServer('ShopOnSell',%itemid,%amount);
	}
	else
	{
		commandToServer('BankOnSell',%itemid,%amount);
	}

}
function RPGshopScreen::onSetIamount(%this)
{
	%fullitem = ShopInvList.SelectText;
	%amt = Iamount.getValue();
	Iamount.setValue(mfloor(Iamount.getValue()));
	if($clshop::shop)
	{
	//%fullitem = ShopInvList.SelectText;
	Sell_Price.setValue(%amt * Sell_Price.tmpprice);
	
	}
	if($clshop::bank)
	{
	
	}
}
function RPGshopScreen::onSetSamount(%this)
{
	if($clshop::shop)
	{
		%amt = Samount.getValue();
		Total_Cost.setValue(%amt * Total_Cost.tmpprice);
	}
	if($clshop::bank)
	{
		
	}
	Samount.setValue(mfloor(Samount.getValue()));
}
function clientCmdSetCoins(%amount)
{
	coins.setvalue("Coins:" SPC GetWord(%amount,0));
	scoins.setvalue("Coins:" SPC GetWord(%amount,0));
}
function ShopList::onSelect(%this, %itemId, %text)
{
	//Client-side
	$Client::invstemp = %text;//pass the itemname for later use
	commandToServer('ShopListOnSelect', $clshop::cvar[%text]);
}

function clientCmdShopDone()
{
	InventoryList.refreshInventory();
}
function clientCmdShopListOnSelect(%price)
{
	//Client-side
	
	Total_Cost.tmpprice = %price;
	RPGshopScreen::onSetSamount();
}
function clientCmdShopInvListOnSelect(%price)
{
	//Client-side
	
	Sell_Price.tmpprice = %price;
	RPGshopScreen::onSetIamount();
}

$guiVer["RPGshopScreen"] = 1.4;
InventoryList::RefreshInventory(InventoryList);