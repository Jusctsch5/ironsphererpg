//exec("gui/ISGameMenu.gui");
if(!isObject(ISGameMenu))
exec("gui/ISGameMenu.gui");

function toggleInventoryHud( %val )
{
   rpgtoggle();
   echo("toggleGameMenuHud");
   if ( %val )
   {
      Canvas.pushDialog(ISGameMenu);
      CommandToServer('OnOpenMenu');
   }
      //toggleCursorHuds('ISGameMenu');
}

//------------------------------------------------------------------------------

function ISPlayerList::onAdd(%this)
{

	%this.clear();
   	%this.clearColumns();
	%this.addColumn( 0, "Player", 120, 50, 200 );
	%this.addColumn( 1, "LVL", 50, 25, 200, "numeric center"  );
	
	%this.setSortColumn(0);
	CommandToServer('RequestPlayerList');
}
function ISGameMenu::setupHud( %this, %tag )
{

	
}
function ISPlayerList::onSelect(%this, %row, %text)
{
	%id = ISPlayerList.getSelectedId();
	if($lastid !=  %id)//spam for some reason.. ergy.

	{
		$lastid = %id;
		commandToServer('ISPlayerListOnSelect', %id, "");
	}

}
function ISPlayerList::onRightMouseDown( %this, %column, %row, %mousePos )
{
	%id = %this.getRowId( %row );
	$lastid = %id;
	commandToServer('ISPlayerListOnSelect', %id, "");
}
function ISPlayerList::onMouseDown(%this)
{



}
function ISPlayerList::onColumnResize( %this, %column, %newSize, %key )
{
  
}

function ISPlayerList::onColumnRepositioned( %this, %oldColumn, %newColumn )
{

}
function isMenu::onSelect(%this, %itemId, %text)
{

	$lastid = 0;
	isMenu.SelectText = %text;
	commandToServer('ISMenuOnSelect', %itemid, %text);
}
function clientCmdISGameMenuUpdatePlayer()
{
	ISPlayerList.clear();
}
function clientCmdISGameMenuUpdateHud()
{
	isMenu.clear();

	//deletevariables("$menu::*");
}
function clientCmdisMenuAddRow(%itemId, %text)
{
	
	isMenu.addRow(%itemId, %text);

}
function clientCmdISPlayerListAddRow(%itemId, %text, %lvl)
{

	ISPlayerList.addRow(%itemId, %text TAB %lvl);
	ISPlayerList.sort();
}
function clientCmdISGameMenuDone()
{


	ISPlayerList.sort();


}
function clientCmdISPlayerListOnSelect()
{
	//Client-side



}
function clientCmdCloseISMenu()
{
	ISGameMenu.onDone();
}	
function clientCmdOpenIsMenu()
{
	//toggleCursorHuds('ISGameMenu');
	Canvas.pushDialog(ISGameMenu);
}
//------------------------------------------------------------------------------
function ISGameMenu::onDone( %this )
{
	if(isObject( hudMap))
	{
	hudMap.pop();
	//hudMap.delete();
	}
	//toggleCursorHuds( 'ISGameMenu' );
	Canvas.popDialog(ISGameMenu);
	
	
	
}
function ISGameMenu::onWake(%this)
{
   if ( $HudHandle['ISGameMenu'] !$= "" )
      alxStop( $HudHandle['ISGameMenu'] );
   alxPlay(HudInventoryActivateSound, 0, 0, 0);
   $HudHandle['ISGameMenu'] = alxPlay(HudInventoryHumSound, 0, 0, 0);
ISPlayerList.onAdd();

}

//------------------------------------------------------------------------------
function ISGameMenu::onSleep()
{
 
	alxStop($HudHandle['ISGameMenu']);
	alxPlay(HudInventoryDeactivateSound, 0, 0, 0);
	$HudHandle['ISGameMenu'] = "";
	ISPlayerList.clear();
	commandToServer('ClientCloseISMenu');
}

//------------------------------------------------------------------------------

$guiVer["ISGameMenu"] = 1.0;