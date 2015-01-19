

if(!isObject(RPGInventoryScreen))
exec("gui/RPGinventoryScreen.gui");
if(!isObject(InventoryList))
new ShellTextList(InventoryList) {
	profile = "ShellTextArrayProfile";
	horizSizing = "right";
	vertSizing = "bottom";
	position = "0 0";
	extent = "297 8";
	minExtent = "8 8";
	visible = "0";
	helpTag = "0";
	enumerate = "1";
	resizeCell = "1";
	columns = "0";
	fitParentWidth = "1";
	clipColumnText = "0";
};
function toggleCommanderMap( %val )
{

   if ( %val )
   {
   
      canvas.pushDialog(RPGinventoryScreen);
	   if($inv::items == 0) commandToServer('RequestInvList');
		else
	   InventoryList.RefreshInventory();
   }

}





//------------------------------------------------------------------------------




//------------------------------------------------------------------------------
function RPGInventoryScreen::addLine( %this, %tag, %lineNum, %type, %count )
{
	echo("RPGInventoryScreen::addLine(" @ %this @ ", " @ %tag @ ", " @ %lineNum @ ", " @ %type @ ", " @ %count @ ")");
	return;
}
   
//------------------------------------------------------------------------------

function InvList::onSelect(%this, %itemId, %text)
{
	//Client-side


	InvList.SelectText = %text;

	commandToServer('InventoryListOnSelect', %itemId);
}
function ShopInvList::onSelect(%this, %itemId, %text)
{
	//Client-side


	ShopInvList.SelectText = %text;

	commandToServer('InventoryListOnSelect', %itemId);
}

function clientCmdInventoryUpdateHud()
{

	deletevariables("$inv::*");
	$inv::items = 0;
	//start
	InventoryList.clear();
	ShopInvList.clear();
	InvList.clear();


}
function clientCmdRemoveFromInventory(%itemId, %fullitem)
{
	if(!%itemid) return;
	%found = false;
	

	for(%i = 0; %i < $inv::list[%fullitem]; %i++)
	{
		if(!%found)
		{
			//item not found lets find it!
			if($inv::val[%fullitem, %i] == %itemId)
			{
				%found = true;

			}
		
		}
		if(%found)
		{
			$inv::val[%fullitem, %i] = $inv::val[%fullitem, %i+1];
		}
			
	}
	if(%found)
	{
		$inv::list[%fullitem]--;
		$inv::count[%fullitem] = 0;
		InventoryList.RefreshInventory();

	}
	
}
function clientCmdAddToInventory(%itemId, %fullitem, %type, %model, %count)
{

	if(!%itemid) return;
	if($inv::have[%fullitem] == 1)
	{
		$inv::val[%fullitem, $inv::list[%fullitem]] = %itemId;
		$inv::list[%fullitem]++;
		$inv::count[%fullitem] = %count;

	}
	else
	{
		if($inv::items == 0) $inv::items = 0;//doh not exactly equivilents remember strings are = to 0
		//first one!
		//InventoryList.addRow(%itemId, %fullitem);
		$inv::numtolist[$inv::items] = %fullitem;
		$inv::type[$inv::items] = %type;
		$inv::items++;
		$inv::val[%fullitem, 0] = %itemid;
		$inv::list[%fullitem] = 1;
		$inv::have[%fullitem] = 1;
		$inv::count[%fullitem] = %count;
	}
	InventoryList.Sort(0);
	
	InventoryList.RefreshInventory();
}
function InventoryList::RefreshInventory(%this)
{
	
	%selshopinv = ShopInvList.SelectText;
	%selinvlist = InvList.SelectText;
	InventoryList.clear();
	ShopInvList.clear();
	InvList.clear();
	for(%i = 0; %i < $inv::items; %i++)
	{
		%item = $inv::numtolist[%i];
		if($inv::list[%item] > 0)
		{
			//%type = $inv::type[%i];
			
				InventoryList.addRow($inv::val[%item, 0], %item);
				ShopInvList.addRow($inv::val[%item, 0], %item);
				
				InvList.addRow($inv::val[%item, 0], %item);
				 
		}
	}
	InventoryList.Sort(1);
	ShopInvList.Sort(1);
	InvList.Sort(1);
	ShopInvList.setSelectedbyId($inv::Val[%selshopinv, 0]);
	InvList.setSelectedbyId($inv::Val[%selinvlist, 0]);

}
function clientCmdInventoryAddRow(%itemId, %fullitem, %type, %model, %count)
{
	if(!%itemid) return;
	$inv::model[%itemId] = %model;
	clientCmdAddToInventory(%itemId, %fullitem, %type, %model, %count);
	return;

}
function clientCmdInventoryDone()
{
//finish
	if(isobject(InventoryList))
	InventoryList.RefreshInventory();
}
function clientCmdInventoryListOnSelect(%i1, %i2, %i3, %i4, %i5, %i6, %i7, %i8, %i9, %i10)
{
	//Client-side
	//echo("clientCmdInventoryListOnSelect");
	//For some reason I have to do this because if I don't do an echo, they are supposedly not equivalents.
	%info[1] = %i1;
	%info[2] = %i2;
	%info[3] = %i3 @ " ";
	%info[4] = %i4;
	%info[5] = %i5 @ " ";
	%info[6] = %i6;
	%info[7] = %i7;
	%info[8] = %i8;
	%info[9] = %i9;
	%info[10] = %i10;
	//%info[11] = %i11;//unused (no invdeschud)
	%text = %info[1] NL %info[2] NL %info[3] NL %info[4] NL %info[5] NL %info[6] NL %info[7] NL %info[8] NL %info[9] NL %info[10] NL %info[11];
	RPGInventoryTextBox.setValue(%text);


}

//------------------------------------------------------------------------------
function RPGInventoryScreen::onDrop(%this)
{
	//Client-side
	%itemId = InvList.getSelectedId();
	commandToServer('InventoryDrop', %itemId);
}


//------------------------------------------------------------------------------
function RPGInventoryScreen::onUse(%this)
{
	//Client-side
	echo("RPGInventoryScreen::onUse("SPC %this SPC ");");
	%itemid = InvList.getSelectedId();

	commandToServer('InventoryUse', %itemId);
}

//------------------------------------------------------------------------------
function RPGInventoryScreen::onDone( %this )
{
	
	canvas.popDialog(RPGInventoryScreen);
}
function RPGInventoryScreen::onWake(%this)
{
   if ( $HudHandle['inventoryScreen'] !$= "" )
      alxStop( $HudHandle['RPGInventoryScreen'] );
   alxPlay(HudInventoryActivateSound, 0, 0, 0);
   $HudHandle['inventoryScreen'] = alxPlay(HudInventoryHumSound, 0, 0, 0);

   if ( isObject( hudMap ) )
   {
      hudMap.pop();
      hudMap.delete();
   }
   new ActionMap( hudMap );
   hudMap.blockBind( moveMap, toggleScoreScreen );
   hudMap.blockBind( moveMap, toggleCommanderMap );
   
   hudMap.bindCmd( keyboard, escape, "", "RPGInventoryScreen.onDone();" );
   hudMap.bindCmd( keyboard, c, "RPGInventoryScreen.onDone();", "");
   hudMap.push();
}

//------------------------------------------------------------------------------
function RPGInventoryScreen::onSleep()
{
	hudMap.pop();
	hudMap.delete();   
	alxStop($HudHandle['inventoryScreen']);
	alxPlay(HudInventoryDeactivateSound, 0, 0, 0);
	$HudHandle['inventoryScreen'] = "";
}

//------------------------------------------------------------------------------
function RPGInventoryScreen::onTabSelect( %this, %favId )
{
	//loadFavorite( %favId, 0 );
}
$guiVer["RPGInventoryScreen"] = 1.2;