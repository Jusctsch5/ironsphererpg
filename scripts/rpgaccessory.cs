//When adding a new accessory, follow these steps:
//-(if it has a new accessory type, fill in the stuff here)
//-add the actual itemdata here

//current item method involves having two ItemData's for each item, where one differs from the
//other by category.  One is Accessory, the other is Equipped.

//=========================
//  $SpecialVar list:
//=========================
//1:
//2: Weapon range
//3: MDEF
//4: HP
//5: Mana
//6: ATK
//7: DEF
//8: Internal armor switching variable
//9: Weapon delay
//10: HP regen
//11: Mana regen
//12: speed modification
//100 + $Skillxxx: Skill Bonus

$SpecialVarDesc[1] = "";
$SpecialVarDesc[2] = "";
$SpecialVarDesc[3] = "MDEF (Magical)";
$SpecialVarDesc[4] = "HP";
$SpecialVarDesc[5] = "Mana";
$SpecialVarDesc[6] = "ATK";
$SpecialVarDesc[7] = "DEF";
$SpecialVarDesc[8] = "[Internal]";
$SpecialVarDesc[9] = "Weapon Delay";	//bonus by percentage
$SpecialVarDesc[10] = "HP Regen";
$SpecialVarDesc[11] = "Mana Regen";
$SpecialVarDesc[12] = "Speed";//player speed modification + is up - is down.
$SpecialVarDesc[13] = "Fire Element Resistance";//note: resistance is used in temp bonuses only!
$SpecialVarDesc[14] = "Water Element Resistance";
$SpecialVarDesc[15] = "Earth Element Resistance";
$SpecialVarDesc[16] = "Gravity Element Resistance";
$SpecialVarDesc[17] = "Energy Element Resistance";
$SpecialVarDesc[18] = "Wind Element Resistance";
$SpecialVarDesc[19] = "Magic Resistance";//resistance is temp, and also goes down as you get hit.
$SpecialVarDesc[20] = "Fire Element Defense";//use defense for items and such, reduces damage. var is 0r0 etc.
$SpecialVarDesc[21] = "Water Element Defense";
$SpecialVarDesc[22] = "Earth Element Defense";
$SpecialVarDesc[23] = "Gravity Element Defense";
$SpecialVarDesc[24] = "Energy Element Defense";
$SpecialVarDesc[25] = "Wind Element Defense";
$SpecialVarDesc[26] = "Damage Resistance";//protection from damage used in temp skill only


for(%i = 1; $SkillDesc[%i] !$= ""; %i++)
	$SpecialVarDesc[100 + %i] = $SkillDesc[%i];

$RingAccessoryType = 1;
$BodyAccessoryType = 2;
$BootsAccessoryType = 3;
$BackAccessoryType = 4;
$ShieldAccessoryType = 5;
$TalismanAccessoryType = 6;
$SwordAccessoryType = 7;
$AxeAccessoryType = 8;
$PolearmAccessoryType = 9;
$BludgeonAccessoryType = 10;
$RangedAccessoryType = 11;
$ProjectileAccessoryType = 12;
$BeltAccessoryType = 13;

$LocationDesc[$RingAccessoryType] = "Ring";
$LocationDesc[$BodyAccessoryType] = "Body";
$LocationDesc[$BootsAccessoryType] = "Feet";
$LocationDesc[$BackAccessoryType] = "Back";
$LocationDesc[$ShieldAccessoryType] = "LHand";
$LocationDesc[$TalismanAccessoryType] = "Talisman";
$LocationDesc[$SwordAccessoryType] = "Hand";
$LocationDesc[$AxeAccessoryType] = "Hand";
$LocationDesc[$PolearmAccessoryType] = "Hand";
$LocationDesc[$BludgeonAccessoryType] = "Hand";
$LocationDesc[$RangedAccessoryType] = "Hand";
$LocationDesc[$ProjectileAccessoryType] = "Projectile";
$LocationDesc[$BeltAccessoryType] = "Item";

$maxAccessory[$RingAccessoryType] = 2;
$maxAccessory[$BodyAccessoryType] = 1;
$maxAccessory[$BootsAccessoryType] = 1;
$maxAccessory[$BackAccessoryType] = 1;
$maxAccessory[$ShieldAccessoryType] = 1;
$maxAccessory[$TalismanAccessoryType] = 1;

//these are used for $AccessoryVar
$AccessoryType = 1;
$SpecialVar = 2;
$Weight = 3;
$ShopIndex = 4;
$MiscInfo = 5;

//=====================
// ACCESSORY FUNCTIONS
//=====================

function RPGGame::GetLocation(%game, %accessoryType)
{
	return $LocationDesc[%accessoryType];
}
//function GetAccessoryVar(%item, %type)
//{
//	%nitem = getCroppedItem(%item);
//
//	return $AccessoryVar[%nitem, %type];
//}
//
function RPGGame::GetItemList(%game, %client, %type, %filter)
{
	if(IsDead(%client) || !fetchData(%client, "HasLoadedAndSpawned") || %client.IsInvalid)
		return "";

	if(%type $= "")
		%type = -1;
	if(%filter $= "")
		%filter = -1;

	%wlist = "";
	for( %i = 1; $locationDesc[%i] !$= ""; %i++)
	{
		if(!%done[$locationDesc[%i]] )
		{
			%done[$locationDesc[%i]] = true;
			if( fetchdata(%client, "WeaponIn" @ %game.getLocation(%i)) )
			{
				%wlist = %wlist @ fetchdata(%client, "WeaponIn" @ %game.getLocation(%i)) @ " ";
			}
			if( fetchdata(%client, "ArmorIn" @ %game.getLocation(%i)) )
			{
				%wlist = %wlist @ fetchdata(%client, "ArmorIn" @ %game.getLocation(%i)) @ " ";
			}
		}
	}
	
	for(%i = 0; (%itemId = GetWord(%wlist, %i)) !$= ""; %i++)
	{
		%item = %game.GetItem(%client, %itemId);

		%flag = "";
		if(%type $= 1)
		{
			if(!%client.data.equipped[%itemid])
				%flag = true;
		}
		else if(%type $= 2)
		{
			if(!%client.data.equipped[%itemid])
				%flag = true;
		}
		else if(%type $= 3)
		{
			%flag = true;
		}
		else if(%type $= 4)
		{
			if(%client.data.equipped[%itemid] || fetchData(%client, "weaponInHand") $= %itemId)
				%flag = true;
		}
		else if(%type $= 5)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $SwordAccessoryType)
				%flag = true;
		}
		else if(%type $= 6)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $AxeAccessoryType)
				%flag = true;
		}
		else if(%type $= 7)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $PolearmAccessoryType)
				%flag = true;
		}
		else if(%type $= 8)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $BludgeonAccessoryType)
				%flag = true;
		}
		else if(%type $= 9)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $RangedAccessoryType)
				%flag = true;
		}
		else if(%type $= 10)
		{
			if($AccessoryVar[%item, $AccessoryType] $= $ProjectileAccessoryType)
				%flag = true;
		}
	
		else if(%type $= -1)
			%flag = true;
		
		if(%flag)
		{
			
			
			if(%filter !$= -1)
			{
				%flag2 = "";
				
				
				%stats = %game.GetTotalItemBonus(%client, %itemId, false);
			
				for(%j = 0; (%w = GetWord(%stats, %j)) !$= ""; %j+=2)
				{
					
					
					if(%filter $= %w)
					{
						%flag2 = true;
						break;
					}
				}
			}
			if(%filter $= -1 || %flag2)
				%list = %list @ %itemId @ " ";
		}
	}
	return %list;
}
function GetItemList(%client, %type, %filter)
{
	return Game.GetItemList(%client, %type, %filter);
}
function RPGGame::AddPoints(%game, %client, %char)
{
	if($debugMode $= TRUE) echo("RPGGame::AddPoints(" @ %game @ "," SPC %client @ "," SPC %filter @ ");");
	%add = AddBonusStatePoints(%client, %char);
	%list = GetItemList(%client, 4, %char);
	
	for(%i = 0; (%w = GetWord(%list, %i)) !$= ""; %i++)
	{
		%aisp = %game.AddItemSpecificPoints(%w, %char, %client);

		if(%char $= 3 || %char $= 7 || %char $= 6 || %char $= 20 || %char $= 21 || %char $= 22 || %char $= 23 || %char $= 24 || %char $= 25)		//MDEF, DEF, ATK
			%add = CombineRpgRolls(%add, %aisp, 0, "inf");
		else
			%add += %aisp;
	}

	return %add;

}
function AddPoints(%client, %char)
{
	return Game.AddPoints(%client, %char);

}
function RPGGame::AddItemSpecificPoints(%game, %itemid, %char, %client)
{
	if(%char $= 3 || %char $= 7 || %char $= 6 || %char $= 20 || %char $= 21 || %char $= 22 || %char $= 23 || %char $= 24 || %char $= 25)
		%p = "0r0";
	else
		%p = 0;

	%tmp = %game.GetTotalItemBonus(%client, %itemId);
	
	for(%j = 0; (%e = GetWord(%tmp, %j)) !$= ""; %j+=2)
	{
		if(%e $= %char)
		{
			%v = GetWord(%tmp, %j+1);
			if(%char $= 3 || %char $= 7 || %char $= 6 || %char $= 20 || %char $= 21 || %char $= 22 || %char $= 23 || %char $= 24 || %char $= 25)
				%p = CombineRpgRolls(%p, %v, 0, "inf");
			else
				%p += %v;
		}
	}

	return %p;
}
function AddItemSpecificPoints(%itemId, %char)
{
	return Game.AddItemSpecificPoints(%game, %itemid, %char);
}
function RPGGame::AddItemPoints(%game, %itemid)
{
	return Game.AddItemPoints(%game, %itemid);
}
function AddItemPoints(%itemId)
{
	%tmp = GetTotalItemBonus(%itemId, false);

	%p = 0;
	for(%j = 0; GetWord(%tmp, %j) !$= ""; %j+=2)
	{
		%e = GetWord(%tmp, %j);
		%v = GetWord(%tmp, %j+1);
		if(%char $= 3 || %char $= 7 || %char $= 6)	//MDEF, DEF, ATK
			%p = CombineRpgRolls(%p, %v, 0, "inf");
		else
			%p += %v;
	}

	return %p;
}
function RPGGame::GetTotalItemBonus(%game, %client, %itemid, %noRepetitions)
{
//return;
	%a = %game.GetSpecialVarFromId(%client, %itemId);
	%b = %game.GetPrefixBonus(%client.data.idname[%itemid], %game.getPrefix(%client, %itemid));

	if(%b <= 0) %b = 1;

	//scan list
	%cnt = 0;
	%final = "";
	for(%j = 0; GetWord(%a, %j) !$= ""; %j+=2)
	{
		if(%itemid == $debugitem)
		echo("%j = " @ %j );
		%e = GetWord(%a, %j);
		%v = GetWord(%a, %j+1);
		if(%itemid == $debugitem)
			echo("%e = " @ %e SPC "%v = " @ %v);
		if(strreplace(%v, "r", " ") $= %v && strreplace(%v, "R", " ") $= %v)
		{
			if(%itemid == $debugitem)
			echo("Inside, single");
			//normal
			%v = mfloor(%v*%b);
			if(%v < 1)
			%v = 1;//cant get any worse 
			if(%itemid == $debugitem)
				echo("%v = " @ %v );			
		}
		else
		{
			if(%itemid == $debugitem)
			echo("Inside, random");
			%tmp = strreplace(%v, "r", " ");
			if(%tmp $= %v)
				%tmp = strreplace(%v, "R", " ");
			if(%itemid == $debugitem)
				echo("%tmp = " @ %tmp);
			%g = mfloor(getword(%tmp, 0) * %b / 2);
			%f = mfloor(getword(%tmp, 1) * %b);
			if(%g < 0) %g = 0;
			if(%f < 1) %f = 1;
			if(%itemid == $debugitem)
				echo("%g = " @ %g SPC "%f = " @ %f);
			%v = %g @ "r" @ %f;
		}
		%final = %final @ %e SPC %v @ " ";
		if(%itemid == $debugitem)
		echo("%final = " @ %final );
	}
		return rtrim(%final);	
}
function GetTotalItemBonus(%itemId, %noRepetitions)
{
	return Game.GetTotalItemBonus(%itemid, %noRepetitions);
}
function RPGGame::WhatSpecialVars(%game, %thing)
{
	%tmp = GetAccessoryVar(%thing, $SpecialVar);

	%t = "";
	for(%i = 0; GetWord(%tmp, %i) !$= ""; %i+=2)
	{
		%s = GetWord(%tmp, %i);
		%n = GetWord(%tmp, %i+1);

		%t = %t @ $SpecialVarDesc[%s] @ ": " @ %n @ ", ";
	}
	if(%t $= "")
		%t = "None";
	else
		%t = getsubstr(%t, 0, strlen(%t)-2);
		
	return %t;
}
function WhatSpecialVars(%thing)
{
	return Game.WhatSpecialVars(%thing);
}

function NullItemList(%client, %type, %msgcolor, %msg)
{
	return;
	for(%z = 1; $ItemList[%type, %z] !$= ""; %z++)
	{
		%item = $ItemList[%type, %z];
		if(Player::getItemCount(%client, %item))
		{
			Player::setItemCount(%client, %item, 0);

			%newmsg = nsprintf(%msg, %item.description);
			messageClient(%client, 'NullItemList', %msgcolor @ %newmsg);
		}
	}
}

function GetCurrentlyWearingArmor(%client)
{
	return;
	//the $ArmorList is present only for this function so far, in order to speed things up and not have to cycle thru
	//each and every item in the game
	for(%i = 1; $ArmorList[%i] !$= ""; %i++)
	{
		if(Player::getItemCount(%client, $ArmorList[%i] @ "0"))
			return $ArmorList[%i];
	}
	return "";
}

