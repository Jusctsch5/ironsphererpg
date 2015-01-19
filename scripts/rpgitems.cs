//Dagger, Mace, PickAxe, Knife, Hatchet, SpikedClub, QuarterStaff, ShortSword, BroadSword, Club, WarAxe, PaddedArmor, LeatherArmor, SpikedLeatherArmor.
//BluePotion, CrystalBluePotion, EnergyVial, CrystalEnergyVial, SmallRock, Quartz, Granite, Opal, Jade, Turquoise, Ruby, Topaz, Sapphire, Silver, Gold, Keldrinite, Clay, Copper, Tin, Iron, Coal, Mithril, Diere, Adamite, Fish

//----------------------------------------
$ItemPrefix[weapon, 1] = "Broken";
$ItemPrefix[weapon, 2] = "Worn";
$ItemPrefix[weapon, 3] = "";
$ItemPrefix[weapon, 4] = "Fine";
$ItemPrefix[weapon, 5] = "Mighty";
$ItemPrefix[weapon, 6] = "Powerful";

$ItemPrefixBonus[weapon, 1] = 0.6;
$ItemPrefixBonus[weapon, 2] = 0.8;
$ItemPrefixBonus[weapon, 3] = 1.0;
$ItemPrefixBonus[weapon, 4] = 1.2;
$ItemPrefixBonus[weapon, 5] = 1.4;
$ItemPrefixBonus[weapon, 6] = 1.6;

$ItemSuffix[weapon, 1] = "";
$ItemSuffix[weapon, 2] = "of Dexterity";
$ItemSuffix[weapon, 3] = "of Strength";
$ItemSuffix[weapon, 4] = "of Slaughter";
$ItemSuffix[weapon, 5] = "of Dismay";
$ItemSuffix[weapon, 6] = "of Swiftness";

$ItemSuffixBonus[weapon, 1] = "";
$ItemSuffixBonus[weapon, 2] = "101 1 102 1 103 1 104 1";
$ItemSuffixBonus[weapon, 3] = "111 1 112 1";
$ItemSuffixBonus[weapon, 4] = "101 2 102 2 103 2 104 1";
$ItemSuffixBonus[weapon, 5] = "101 3 102 3 103 3";
$ItemSuffixBonus[weapon, 6] = "9 -30";

//----------------------------------------

$ItemPrefix[armor, 1] = "Old";
$ItemPrefix[armor, 2] = "Weak";
$ItemPrefix[armor, 3] = "";
$ItemPrefix[armor, 4] = "Hardened";
$ItemPrefix[armor, 5] = "Sturdy";
$ItemPrefix[armor, 6] = "Stalwart";

$ItemPrefixBonus[armor, 1] = -0.6;
$ItemPrefixBonus[armor, 2] = -0.8;
$ItemPrefixBonus[armor, 3] = 1.0;
$ItemPrefixBonus[armor, 4] = 1.2;
$ItemPrefixBonus[armor, 5] = 1.4;
$ItemPrefixBonus[armor, 6] = 1.8;

//$ItemSuffix[armor, 1] = "";
//$ItemSuffix[armor, 2] = "of Fire";
//$ItemSuffix[armor, 3] = "of Wind";
//$ItemSuffix[armor, 4] = "of Water";
//$ItemSuffix[armor, 5] = "of Earth";
//$ItemSuffix[armor, 6] = "of Energy";

//$ItemSuffixBonus[armor, 1] = "";
//$ItemSuffixBonus[armor, 2] = "6 1r0";
//$ItemSuffixBonus[armor, 3] = "6 -3r-3";
//$ItemSuffixBonus[armor, 4] = "5 5";
//$ItemSuffixBonus[armor, 5] = "5 -5";
//$ItemSuffixBonus[armor, 6] = "7 10r20 3 5r20";

//###########################################

//Prefix and suffix functions, to make life easier
function GetIdForItemPrefix(%type, %prefix)
{
	for(%i = 1; $ItemPrefix[%type, %i] !$= ""; %i++)
	{
		if(stricmp($ItemPrefix[%type, %i], %prefix) $= 0)
		{
			return %i;
		}
	}
}

function GetIdForItemSuffix(%type, %suffix)
{
	for(%i = 1; $ItemSuffix[%type, %i] !$= ""; %i++)
	{
		if(stricmp($ItemSuffix[%type, %i], %suffix) $= 0)
		{
			return %i;
		}
	}
}
function MeleeAttack(%client, %length, %itemid, %check)
{
	
	Game.MeleeAttack(%client, %length, %itemid, %check);
}
function RPGGame::MeleeAttack(%game, %client, %length, %item,%check)
{
	
	//%weapon = %itemId.data;
	%itemId = fetchData(%client, "weaponInHand");
	%item = %game.GetItem(%client, %itemid);
	
	%length = %Game.GetRange(%item);
	
	if(!%client.player) return;
	if(%client.lastfire == 0)
		%client.lastfire = getSimTime() - %game.GetDelay(%itemId)*1000;
	if(%client.weaponDelayFlag != 1 && getSimTime() - %client.lastfire > %game.GetDelay(%itemid)*1000)
	{

		//play swing sound
		%client.player.Play3D(Swing4);
		
		//%length = GetRange(%itemId);
		%weapon = %item;
		if(%client.currThread == 0)
		{
			cancel(%client.rootthread);
			%client.player.stopThread(%client.currThread);
			if(!%client.isaicontrolled())
			%client.player.playThread(%client.currThread, "root");
			%client.currThread = 1;
			if(!%client.isfish)
			{
				%client.player.playThread(1, "looka");
				
				%client.rootthread = Game.schedule(1250, "playroot", %client);
			}
		}
		else
		{
			cancel(%client.rootthread);
			%client.player.stopThread(%client.currThread);
			if(!%client.isaicontrolled())
			%client.player.playThread(%client.currThread, "root");
			%client.currThread = 0;
			if(!%client.isfish)
			{

				%client.player.playThread(0, "looka");
				%client.rootthread = Game.schedule(1250, "playroot", %client);
			
			}
		}
		//cancel(%client.rootthread);
		//%client.rootthread = Game.schedule(GetDelay(%itemid) * 1000+1000, "playroot", %client);
		Game.schedule(%game.GetDelay(%item) * 1000, "clearWeaponDelayFlag", %client);

		%client.lastfire = getSimTime();
		$los::object = "";
		%hitplayer = 1;
		if(getLOSinfo(%client, %length))
		{
			%pl = $los::object;
			%pos = $los::position;
			if(%pl.getClassName() $= "Player")
			{
				%cl = %pl.client;
				%damageType = $ItemDamageType[%weapon];

				%pl.getDataBlock().damageObject(%pl, %client.player, $los::position, 0, %damageType, "0 0 0");
				%hitplayer = 2;
			}
			else if(%pl.getClassName() $= "HoverVehicle")
			{
				
				%damagetype = $ItemDamageType[%weapon];
				%skilltype = $SkillType[GetItem(%itemid)];
				%damage = AddPoints(%client, 6);
				%damage = GetRpgRoll(%damage);
				
				%multi = Cap((%client.PlayerSkill[%skilltype]*getRandom() ), 1, "inf");
				
				
				if(%multi < 0) %multi = 0;
					%damage = round(%damage * (%multi/1000+1) + %client.PlayerSkill[%damagetype]*getRandom()/10);
				
				%pl.getDataBlock().damageObject(%pl, %client.player, $los::position, %damage, %damagetype, "0 0 0");
			}
			else
			{

				
				%plpos = %client.player.getPosition();
				
				%closestdist = 99999;
				InitContainerRadiusSearch(%pos, 0.1,  $TypeMasks::StaticTSObjectType | $TypeMasks::StaticShapeObjectType);
				if ((%targetObject = containerSearchNext()) != 0)
				{
					if(%closestdist > vectordist(%plpos, %targetobject.getPosition()) && %targetobject.plant)
					{
						%closest = %targetobject;
						%closestdist = vectordist(%plpos, %targetobject.getPosition());
					}
					
				}
				if(%closest)
				{
					
					//Game.HarvestPlant(%closest, %client);
					Game.RemovePlant(%closest);
				}
			
			}
			
		}

		PostAttack(%client, %weapon);
		%client.weaponDelayFlag = 1;

		return %hitplayer;
	}
	else
	return 0;
}
function RPGGame::playroot(%game, %client)
{
    // console spam fix
    if (!isobject(%client.player))
        return;
	
	%client.player.stopThread(%client.currThread);
	%client.player.playThread(%client.currThread, "root");

}
function PostAttack(%client, %weapon)
{
}
function RPGGame::GetRange(%game, %item)
{
	//bonus points work as percentages
	//%a = 0;
	%b = $ItemBaseRange[%item];

	//%c = %b * (%a / 100);
	//%d = %b + %c;

	//%e = Cap(%d, 0, "inf");

	return %b;
}
function RPGGame::GetDelay(%game, %item)
{
	//bonus points work as percentages
	//%a = AddItemSpecificPoints(%itemId, 9);
	%b = $ItemBaseDelay[%item];

	//%c = %b * (%a / 100);
	//%d = %b + %c;

	%e = Cap(%b, 0, "inf");

	return %e;
}
function RPGGame::GetSwingSound(%game, %item)
{
	return $itemSwingSound[%item];
}
function RPGGame::GetHitFleshSound(%game, %itemname)
{
	return $itemHitFlesh[%itemname];
}
function RPGGame::GetModelName(%game, %item)
{
	return $DataBlock[%item];
}
function RPGGame::GetItem(%game, %client, %itemId)
{
	return %client.data.idName[%itemid];
}

//----------------------------------------

function GetTypicalWeight(%size)
{
	switch$(strlwr(%size))
	{
		case "feather":
			return 0.05;
		case "minuscule":
			return 0.2;
		case "tiny":
			return 0.5;
		case "small":
			return 1.0;
		case "medium":
			return 2.0;
		case "large":
			return 4.0;
		case "huge":
			return 7.0;
		case "massive":
			return 10.0;
	}
	error("Size given: " @ %size @ ". -- Possible sizes: feather, minuscule, tiny, small, medium, large, huge, massive.");
}

function GetTypicalDelay(%weapon, %forcevaruse)
{
	if(%forcevaruse)
		%weight = $ItemBaseWeight[%weapon];
	else
		%weight = GetTypicalWeight($ItemSize[%weapon]);

	return Cap(%weight / 3.0, 1.0, "inf");
}

function GetTypicalATK(%weapon, %atkOverDelay, %forcevaruse)
{
	if(%forcevaruse)
		%delay = $ItemBaseDelay[%weapon];
	else
		%delay = GetTypicalDelay(%weapon);

	%atk = %atkOverDelay * %delay;

	return "6 " @ %atk;
}
function RPGGame::GetSpecialVarFromId(%game, %client, %itemId)
{
	return $ItemBaseSpecialVar[%game.GetItem(%client, %itemId)];
}
function RPGGame::GetSpecialVar(%item)
{
	return $ItemBaseSpecialVar[%item];
}
function RPGGame::GetPrefixName(%game, %client, %itemId)
{
	return $ItemPrefix[$ItemType[%game.GetItem(%client, %itemId)], %game.getPrefix(%client, %itemId)];
}
function RPGGame::GetPrefix(%game, %client, %itemId)
{
	return %client.data.prefix[%itemid];
}
function RPGGame::GetPrefixBonus(%item, %prefix)
{
	return $ItemPrefixBonus[$ItemType[%item], %prefix];
}
function RPGGame::GetSuffix(%game, %client, %itemId)
{
	return %client.data.suffix[%itemid];
}
function RPGGame::GetSuffixBonus(%game, %client, %itemId)
{
	return $ItemSuffixBonus[$ItemType[%game.GetItem(%client, %itemId)], $InvInfo[%itemId, suffix]];
}
function RPGGame::GetFullItemName(%game, %prefix, %item,  %suffix)
{
	%full = $ItemDesc[%item];
	if($ItemPrefix[$ItemType[%item], %prefix] !$= "")
	{
		%full = $ItemPrefix[$ItemType[%item], %prefix] SPC %full;
	}
	return %full;
}


function whatis(%item)
{
//ok, what is this item?
//is it a weapon
	%found = false;
	if($itemtype[%item] !$= "")
	{
		%found = true;
		if(!((%price = $Shop::SellPrice[%item]) > 0)) 
			%price = mfloor($Shop::BuyPrice[%item]/100 * 1);
		//get the skill required for the item
		%skillList = "";
		for(%i = 0; (%w = GetWord($skillRestriction[%item], %i)) !$= ""; %i = %i + 2 )
		{
			%skillList = %skillList @ $skillDesc[%w] @ ":" SPC GetWord($skillRestriction[%item], %i + 1) @ " ";
		}
		%itemline = 	"Name: " @ $itemDesc[%item] @ "\n" @
				"Type: " @ $itemType[%item] @ "\n" @
				"Skill: " @ $SkillDesc[$skilltype[%item]] @ "\n" @
				"Skill Requirements: " @ %skillList @ "\n" @
				"Size: " @ $itemSize[%item] @ "\n" @
				"Weight: " @ $itemBaseWeight[%item] @ "\n" @
				"Price: " @ $shop::BuyPrice[%item] @ "\n" @
				"Sell Value: " @ %price;
		%lines = 8;
				
	}
	if(!%found)
	{
		//not an item may be a spell
		if($spelldata[%item, Test])
		{
		%itemline = 	"Name: " @ %item @ "\n" @
				"Type: Spell\n" @
				"Skill Type: " @ $skillDesc[$spelldata[%item, Skill]] @ "\n" @
				"Skill Required: " @ GetWord($skillRestriction[%item],1) @ "\n" @
				"Element:" SPC $spelldata[%item, Element] @ "\n" @
				"Mana Cost: " @ $spelldata[%item, cost] @ "\n";		
		%lines = 6;
		%found = true;
		}
	}
	
	
	if(!%found)
		return "";
	else
		return %lines SPC %itemline;

}
//----------------------------------------

//Always do these definitions in the same order: ItemSize, ItemBaseWeight, ItemBaseDelay, and ItemBaseSpecialVar.
//If you wish to substitute typical values for your own, then make sure to put a 'true' instead of 'false' for
//GetTypicalDelay and GetTypicalATK if you end up using any.  This ensures that your values are used to determine
//other values instead of the automatically generated ones.

//Range is normally determined by guessing or actual measurement of the weapon.

function DefineItems()
{
	$minRange = 2.0;
	$ADnDdelayToRPG = 0.5;
	$numweapons = 0; //everytime you add a weapon increase this by one
	//$weaponList[1] = "handaxe";
	$weaponList[$numweapons++] = "Dagger";          //piercing
	$weaponList[$numweapons++] = "Mace";            //bludgeoning
	$weaponList[$numweapons++] = "Bardiche";        //slashing
	$weaponList[$numweapons++] = "Gladius";         //piercing
	$weaponList[$numweapons++] = "WarHammer";       //bludgeoning
	$weaponList[$numweapons++] = "WarMaul";         //bludgeoning
	$weaponList[$numweapons++] = "Claymore";        //slashing
	$weaponList[$numweapons++] = "GreatClaymore";   //slashing
	$weaponList[$numweapons++] = "Katana";          //piercing
	$weaponList[$numweapons++] = "BastardSword";    //slashing
	$weaponList[$numweapons++] = "PickAxe";         //piercing
	$weaponList[$numweapons++] = "Knife";           //piercing
	$weaponList[$numweapons++] = "Hatchet";         //slashing
	$weaponList[$numweapons++] = "SpikedClub";      //bludgeoning
	$weaponList[$numweapons++] = "QuarterStaff";    //bludgeoning
	$weaponList[$numweapons++] = "ShortSword";      //piercing
	$weaponList[$numweapons++] = "BroadSword";      //slashing
	$weaponList[$numweapons++] = "Club";            //bludgeoning
	$weaponList[$numweapons++] = "IceBroadsword";   //slashing
	$weaponList[$numweapons++] = "FireBroadsword";  //slashing
	$weaponList[$numweapons++] = "WaterBroadsword"; //slashing
	$weaponList[$numweapons++] = "WarAxe";          //slashing
	$weaponList[$numweapons++] = "Longsword";       //slashing
	$weaponlist[$numweapons++] = "spear";           //piercing
	$weaponList[$numweapons++] = "Hammerpick";      //piercing
	$weaponList[$numweapons++] = "BattleAxe";       //slashing
	$weaponList[$numweapons++] = "Sling";           //archery
	$weaponList[$numweapons++] = "Trident";         //piercing
	//$weaponList[$numweapons++] = "BastardSword";    //
	//$weaponList[$numweapons++] = "WarHammer";       //
	$weaponList[$numweapons++] = "Katar";           //piercing
	

//8 piercing
//6 bludgeoning
//12 slashing
//1 archery

	//peircing: damages
	//Knife 	-> 		1
	//pickaxe 	-> 		2
	//Dagger 	-> 		3
	//ShortSword	->		5
	//Spear 	->		9
	//Gladius	->		20
	//Trident	->		30
	
	$ItemType[Knife] = "weapon";
	$ItemSubType[Knife] = $SwordAccessoryType;
	$ItemDesc[Knife] = "Knife";
	$ItemSize[Knife] = "small";
	$ItemBaseWeight[Knife] = 0.5;
	$ItemBaseDelay[Knife] = 1.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Knife] = "6 1r1";
	$ItemBaseRange[Knife] = $minRange + 1.0;
	$ItemDamageType[Knife] = $DamageType::Piercing;
	$itemSwingSound[Knife] = "Swing4"; 
	$itemHitFlesh[Knife]   = "WeaponHit1";
	$itemHitWall[Knife]    = ""; 
	$PrefixExclusions[Knife] = ",";
	$SuffixExclusions[Knife] = "3,";
	$SkillType[Knife] = $Skill::Piercing;
	$DataBlock[Knife] = "Knife";
	$shop::BuyPrice[Knife] = 100;
	$item::smith[Knife,0] = "copper 3 tin 3"; 
	$item::smith[Knife,1] = "copper 1 tin 1"; 
	$item::smith[Knife,2] = "tin 1"; 
	$item::smith[Knife,3] = "copper 1"; 
	$item::smith[Knife,4] = "tin 1 copper 1"; 
	$item::smith[Knife,5] = "tin 1 copper 1 iron 1";

	$ItemType[PickAxe] = "weapon";
	$ItemSubType[PickAxe] = $SwordAccessoryType;
	$ItemDesc[PickAxe] = "Pick Axe";
	$ItemSize[PickAxe] = "small";
	$ItemBaseWeight[PickAxe] = 0.5;
	$ItemBaseDelay[PickAxe] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[PickAxe] = "6 1r2";
	$ItemBaseRange[PickAxe] = $minRange + 3.0;
	$ItemDamageType[PickAxe] = $DamageType::Piercing;
	$itemSwingSound[PickAxe] = "Swing4"; 
	$itemHitFlesh[PickAxe]   = "WeaponHit1";
	$itemHitWall[PickAxe]    = ""; 
	$PrefixExclusions[PickAxe] = ",";
	$SuffixExclusions[PickAxe] = "3,";
	$SkillType[PickAxe] = $Skill::Piercing;
	$DataBlock[PickAxe] = "PickAxe";
	$Shop::BuyPrice[PickAxe] = 2000;
	$item::smith[PickAxe,0] = "granite 3 tin 1 copper 1"; 
	$item::smith[PickAxe,1] = "copper 1 tin 1"; 
	$item::smith[PickAxe,2] = "granite 1"; 
	$item::smith[PickAxe,3] = "iron 4"; 
	$item::smith[PickAxe,4] = "diere 5"; 
	$item::smith[PickAxe,5] = "mithril 6"; 

	$ItemType[Dagger] = "weapon";
	$ItemSubType[Dagger] = $SwordAccessoryType;
	$ItemDesc[Dagger] = "Dagger";
	$ItemSize[Dagger] = "small";
	$ItemBaseWeight[Dagger] = 0.5;
	$ItemBaseDelay[Dagger] = 1.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Dagger] = "6 1r3";
	$ItemBaseRange[Dagger] = $minRange + 1.0;
	$ItemDamageType[Dagger] = $DamageType::Piercing;
	$itemSwingSound[Dagger] = "Swing4"; 
	$itemHitFlesh[Dagger]   = "WeaponHit1";
	$itemHitWall[Dagger]    = ""; 
	$PrefixExclusions[Dagger] = ",";
	$SuffixExclusions[Dagger] = "3,";
	$SkillType[Dagger] = $Skill::Piercing;
	$DataBlock[Dagger] = "Dagger";
	$shop::BuyPrice[Dagger] = 1000;
	$item::smith[Dagger,0] = "copper 2 tin 2 iron 1";	//NEW
	$item::smith[Dagger,1] = "copper 1 tin 1";		//FROM Rusty
	$item::smith[Dagger,2] = "tin 1";			//FROM worn
	$item::smith[Dagger,3] = "copper 1";			//FROM Normal
	$item::smith[Dagger,4] = "iron 1";			//FROM Fine
	$item::smith[Dagger,5] = "copper 1 tin 1 iron 1 coal 1";//FROM Mighty

	$ItemType[ShortSword] = "weapon";
	$ItemSubType[ShortSword] = $SwordAccessoryType;
	$ItemDesc[ShortSword] = "ShortSword";
	$ItemSize[ShortSword] = "small";
	$ItemBaseWeight[ShortSword] = 0.5;
	$ItemBaseDelay[ShortSword] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[ShortSword] = "6 1r5";
	$ItemBaseRange[ShortSword] = $minRange + 3.0;
	$ItemDamageType[ShortSword] = $DamageType::Piercing;
	$itemSwingSound[ShortSword] = "Swing4"; 
	$itemHitFlesh[ShortSword]   = "WeaponHit1";
	$itemHitWall[ShortSword]    = ""; 
	$PrefixExclusions[ShortSword] = ",";
	$SuffixExclusions[ShortSword] = "3,";
	$SkillType[ShortSword] = $Skill::Piercing;
	$DataBlock[ShortSword] = "ShortSword2";
	$shop::BuyPrice[ShortSword] = 3000;
	$item::smith[ShortSword,0] = "iron 3 copper 1 tin 1 coal 1"; 
	$item::smith[ShortSword,1] = "copper 1 tin 1"; 
	$item::smith[ShortSword,2] = "iron 1"; 
	$item::smith[ShortSword,3] = "iron 2 copper 1 tin 1"; 
	$item::smith[ShortSword,4] = "iron 2 copper 2"; 
	$item::smith[ShortSword,5] = "iron 1 coal 1 diere 1"; 
	
	$ItemType[Spear] = "weapon";
	$ItemSubType[Spear] = $SwordAccessoryType;
	$ItemDesc[Spear] = "Spear";
	$ItemSize[Spear] = "large";
	$ItemBaseWeight[Spear] = 0.5;
	$ItemBaseDelay[Spear] = 3.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Spear] = "6 1r9";
	$ItemBaseRange[Spear] = $minRange + 5.0;
	$ItemDamageType[Spear] = $DamageType::Piercing;
	$itemSwingSound[Spear] = "Swing4"; 
	$itemHitFlesh[Spear]   = "WeaponHit1";
	$itemHitWall[Spear]    = ""; 
	$PrefixExclusions[Spear] = ",";
	$SuffixExclusions[Spear] = "3,";
	$SkillType[Spear] = $Skill::Piercing;
	$DataBlock[Spear] = "Spear";
	$shop::BuyPrice[Spear] = 10000;
	$item::smith[Spear,0] = "Rod 1 iron 10 Mithril 1 Coal 8"; 
	$item::smith[Spear,1] = "iron 2 Coal 1 Rod 1"; 
	$item::smith[Spear,2] = "iron 4 Coal 2"; 
	$item::smith[Spear,3] = "iron 3 Coal 4"; 
	$item::smith[Spear,4] = "iron 5 coal 8 Mithril 2"; 
	$item::smith[Spear,5] = "iron 10 coal 9 Mithril 5";	

	$ItemType[Gladius] = "weapon";
	$ItemSubType[Gladius] = $PolearmAccessoryType;
	$ItemDesc[Gladius] = "Gladius";
	$ItemSize[Gladius] = "medium";
	$ItemBaseWeight[Gladius] = 1.0;
	$ItemBaseDelay[Gladius] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Gladius] = "6 1r20";
	$ItemBaseRange[Gladius] = $minRange + 2.0;
	$ItemDamageType[Gladius] = $DamageType::Piercing;
	$itemSwingSound[Gladius] = "Swing4"; 
	$itemHitFlesh[Gladius]   = "WeaponHit1";
	$itemHitWall[Gladius]    = ""; 
	$PrefixExclusions[Gladius] = ",";
	$SuffixExclusions[Gladius] = ",";
	$SkillType[Gladius] = $Skill::Piercing;
	$DataBlock[Gladius] = "Gladius";
	$shop::BuyPrice[Gladius] = 40000;
	$item::smith[Gladius,0] = "Rod 1 iron 15 Mithril 2 Coal 12"; 
	$item::smith[Gladius,1] = "iron 3 Coal 2 Rod 1"; 
	$item::smith[Gladius,2] = "iron 5 Coal 4"; 
	$item::smith[Gladius,3] = "iron 6 Coal 8"; 
	$item::smith[Gladius,4] = "iron 9 coal 9 Mithril 4 quartz 1"; 
	$item::smith[Gladius,5] = "iron 13 coal 12 Mithril 10 opal 1";

	$ItemType[Trident] = "weapon";
	$ItemSubType[Trident] = $PolearmAccessoryType;
	$ItemDesc[Trident] = "Trident";
	$ItemSize[Trident] = "large";
	$ItemBaseWeight[Trident] = 1.5;
	$ItemBaseDelay[Trident] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Trident] = "6 1r30";
	$ItemBaseRange[Trident] = $minRange + 2.0;
	$ItemDamageType[Trident] = $DamageType::Piercing;
	$itemSwingSound[Trident] = "Swing4"; 
	$itemHitFlesh[Trident]   = "WeaponHit1";
	$itemHitWall[Trident]    = ""; 
	$PrefixExclusions[Trident] = ",";
	$SuffixExclusions[Trident] = ",";
	$SkillType[Trident] = $Skill::Piercing;
	$DataBlock[Trident] = "Trident";
	$shop::BuyPrice[Trident] = 60000;
	$item::smith[Trident,0] = "Rod 1 iron 15 Mithril 3 Coal 12 Diere 1"; 
	$item::smith[Trident,1] = "iron 3 Coal 3 Rod 1"; 
	$item::smith[Trident,2] = "iron 6 Coal 5"; 
	$item::smith[Trident,3] = "iron 8 Coal 8 Diere 1 Mithril 1"; 
	$item::smith[Trident,4] = "iron 11 coal 9 Mithril 6 quartz 1 Diere 2"; 
	$item::smith[Trident,5] = "iron 15 coal 12 Mithril 12 opal 1 Diere 3";

	$ItemType[Katar] = "weapon";
	$ItemSubType[Katar] = $SwordAccessoryType;
	$ItemDesc[Katar] = "Katar";
	$ItemSize[Katar] = "medium";
	$ItemBaseWeight[Katar] = 1.5;
	$ItemBaseDelay[Katar] = 1.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Katar] = "6 1r30";
	$ItemBaseRange[Katar] = $minRange + 2.0;
	$ItemDamageType[Katar] = $DamageType::Piercing;
	$itemSwingSound[Katar] = "Swing4"; 
	$itemHitFlesh[Katar]   = "WeaponHit1";
	$itemHitWall[Katar]    = ""; 
	$PrefixExclusions[Katar] = ",";
	$SuffixExclusions[Katar] = ",";
	$SkillType[Katar] = $Skill::Piercing;
	$DataBlock[Katar] = "Katar";
	$shop::BuyPrice[Katar] = 60000;
	$item::smith[Katar,0] = "Rod 1 iron 15 Mithril 3 Coal 12 Diere 1 Silver 1"; 
	$item::smith[Katar,1] = "iron 3 Coal 3 Rod 1"; 
	$item::smith[Katar,2] = "iron 6 Coal 5"; 
	$item::smith[Katar,3] = "iron 8 Coal 8 Diere 2 Silver 1"; 
	$item::smith[Katar,4] = "iron 11 coal 9 Mithril 6 quartz 1 Diere 2 Silver 2"; 
	$item::smith[Katar,5] = "iron 15 coal 12 Mithril 12 opal 1 Diere 3 Silver 4";
//slashing
//hatchet		->		1
//WarAxe		->		4
//broadsword		->		7
//longsword 		->		10
//IceBroadsword		->		20
//FireBroadSword	->		20
//WaterBroadSword	->		20
//BattleAxe		->		25
//BastardSword		->		35

	$ItemType[Hatchet] = "weapon";
	$ItemSubType[Hatchet] = $AxeAccessoryType;
	$ItemDesc[Hatchet] = "Hatchet";
	$ItemSize[Hatchet] = "small";
	$ItemBaseWeight[Hatchet] = 0.5;
	$ItemBaseDelay[Hatchet] = 1.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Hatchet] = "6 1r1";
	$ItemBaseRange[Hatchet] = $minRange + 3.0;
	$ItemDamageType[Hatchet] = $DamageType::Slashing;
	$itemSwingSound[Hatchet] = "Swing4"; 
	$itemHitFlesh[Hatchet]   = "WeaponHit1";
	$itemHitWall[Hatchet]    = ""; 
	$PrefixExclusions[Hatchet] = ",";
	$SuffixExclusions[Hatchet] = "3,";
	$SkillType[Hatchet] = $Skill::Slashing;
	$DataBlock[Hatchet] = "hatchet";
	$shop::BuyPrice[Hatchet] = 100;
	$item::smith[Hatchet,0] = "copper 2 tin 2 granite 2"; 
	$item::smith[Hatchet,1] = "copper 1 tin 1"; 
	$item::smith[Hatchet,2] = "granite 2"; 
	$item::smith[Hatchet,3] = "copper 1"; 
	$item::smith[Hatchet,4] = "tin 2 copper 1"; 
	$item::smith[Hatchet,5] = "iron 1 granite 1"; 

	$ItemType[WarAxe] = "weapon";
	$ItemSubType[WarAxe] = $AxeAccessoryType;
	$ItemDesc[WarAxe] = "WarAxe";
	$ItemSize[WarAxe] = "small";
	$ItemBaseWeight[WarAxe] = 0.5;
	$ItemBaseDelay[WarAxe] = 2.2 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[WarAxe] = "6 1r4";
	$ItemBaseRange[WarAxe] = $minRange + 3.0;
	$ItemDamageType[WarAxe] = $DamageType::Slashing;
	$itemSwingSound[WarAxe] = "Swing4"; 
	$itemHitFlesh[WarAxe]   = "WeaponHit1";
	$itemHitWall[WarAxe]    = ""; 
	$PrefixExclusions[WarAxe] = ",";
	$SuffixExclusions[WarAxe] = "3,";
	$SkillType[WarAxe] = $Skill::Slashing;
	$DataBlock[WarAxe] = "hatchet";
	$shop::BuyPrice[WarAxe] = 1000;
	$item::smith[WarAxe,0] = "copper 2 tin 2 iron 3"; 
	$item::smith[WarAxe,1] = "copper 1 tin 1"; 
	$item::smith[WarAxe,2] = "copper 2 tin 2"; 
	$item::smith[WarAxe,3] = "iron 1"; 
	$item::smith[WarAxe,4] = "iron 2"; 
	$item::smith[WarAxe,5] = "iron 3 coal 1"; 	

	$ItemType[BroadSword] = "weapon";
	$ItemSubType[BroadSword] = $SwordAccessoryType;
	$ItemDesc[BroadSword] = "BroadSword";
	$ItemSize[BroadSword] = "small";
	$ItemBaseWeight[BroadSword] = 0.5;
	$ItemBaseDelay[BroadSword] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[BroadSword] = "6 1r7";
	$ItemBaseRange[BroadSword] = $minRange + 3.0;
	$ItemDamageType[BroadSword] = $DamageType::Slashing;
	$itemSwingSound[BroadSword] = "Swing4"; 
	$itemHitFlesh[BroadSword]   = "WeaponHit1";
	$itemHitWall[BroadSword]    = ""; 
	$PrefixExclusions[BroadSword] = ",";
	$SuffixExclusions[BroadSword] = "3,";
	$SkillType[BroadSword] = $Skill::Slashing;
	$DataBlock[BroadSword] = "BroadSword";
	$shop::BuyPrice[BroadSword] = 3000;
	$item::smith[BroadSword,0] = "iron 3 coal 2 granite 2"; 
	$item::smith[BroadSword,1] = "copper 1 tin 1"; 
	$item::smith[BroadSword,2] = "iron 2 granite 1"; 
	$item::smith[BroadSword,3] = "iron 1 coal 1 granite 1"; 
	$item::smith[BroadSword,4] = "iron 2 coal 2"; 
	$item::smith[BroadSword,5] = "iron 2 coal 2 diere 1"; 

	$ItemType[LongSword] = "weapon";
	$ItemSubType[LongSword] = $SwordAccessoryType;
	$ItemDesc[LongSword] = "LongSword";
	$ItemSize[LongSword] = "small";
	$ItemBaseWeight[LongSword] = 0.5;
	$ItemBaseDelay[LongSword] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[LongSword] = "6 1r10";
	$ItemBaseRange[LongSword] = $minRange + 3.0;
	$ItemDamageType[LongSword] = $DamageType::Slashing;
	$itemSwingSound[LongSword] = "Swing4"; 
	$itemHitFlesh[LongSword]   = "WeaponHit1";
	$itemHitWall[LongSword]    = ""; 
	$PrefixExclusions[LongSword] = ",";
	$SuffixExclusions[LongSword] = "3,";
	$SkillType[LongSword] = $Skill::Slashing;
	$DataBlock[LongSword] = "LongSword";
	$shop::BuyPrice[LongSword] = 10000;
	$item::smith[LongSword,0] = "iron 6 coal 4 granite 2 Mithril 1"; 
	$item::smith[LongSword,1] = "copper 1 tin 1"; 
	$item::smith[LongSword,2] = "iron 2 granite 1"; 
	$item::smith[LongSword,3] = "iron 1 coal 4 granite 1"; 
	$item::smith[LongSword,4] = "iron 2 coal 8 Mithril 2"; 
	$item::smith[LongSword,5] = "iron 2 coal 8 diere 1 Mithril 3"; 	

	$ItemType[IceBroadsword] = "weapon";
	$ItemSubType[IceBroadsword] = $SwordAccessoryType;
	$ItemDesc[IceBroadsword] = "Ice Broadsword";
	$ItemSize[IceBroadsword] = "small";
	$ItemBaseWeight[IceBroadsword] = 0.5;
	$ItemBaseDelay[IceBroadsword] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[IceBroadsword] = "6 1r20";
	$ItemBaseRange[IceBroadsword] = $minRange + 2.0;
	$ItemDamageType[IceBroadsword] = $DamageType::Slashing;
	$itemSwingSound[IceBroadsword] = "Swing4"; 
	$itemHitFlesh[IceBroadsword]   = "WeaponHit1";
	$itemHitWall[IceBroadsword]    = ""; 
	$PrefixExclusions[IceBroadsword] = ",";
	$SuffixExclusions[IceBroadsword] = "3,";
	$SkillType[IceBroadsword] = $Skill::Slashing;
	$DataBlock[IceBroadsword] = "IceBroadsword";
	$shop::BuyPrice[IceBroadsword] = 20000;

	$ItemType[FireBroadsword] = "weapon";
	$ItemSubType[FireBroadsword] = $SwordAccessoryType;
	$ItemDesc[FireBroadsword] = "Fire Broadsword";
	$ItemSize[FireBroadsword] = "small";
	$ItemBaseWeight[FireBroadsword] = 0.5;
	$ItemBaseDelay[FireBroadsword] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[FireBroadsword] = "6 1r20";
	$ItemBaseRange[FireBroadsword] = $minRange + 2.0;
	$ItemDamageType[FireBroadsword] = $DamageType::Slashing;
	$itemSwingSound[FireBroadsword] = "Swing4"; 
	$itemHitFlesh[FireBroadsword]   = "WeaponHit1";
	$itemHitWall[FireBroadsword]    = ""; 
	$PrefixExclusions[FireBroadsword] = ",";
	$SuffixExclusions[FireBroadsword] = "3,";
	$SkillType[FireBroadsword] = $Skill::Slashing;
	$DataBlock[FireBroadsword] = "FireBroadsword";
	$shop::BuyPrice[FireBroadsword] = 20000;
	
	$ItemType[WaterBroadsword] = "weapon";
	$ItemSubType[WaterBroadsword] = $SwordAccessoryType;
	$ItemDesc[WaterBroadsword] = "Water Broadsword";
	$ItemSize[WaterBroadsword] = "small";
	$ItemBaseWeight[WaterBroadsword] = 0.5;
	$ItemBaseDelay[WaterBroadsword] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[WaterBroadsword] = "6 1r20";
	$ItemBaseRange[WaterBroadsword] = $minRange + 2.0;
	$ItemDamageType[WaterBroadsword] = $DamageType::Slashing;
	$itemSwingSound[WaterBroadsword] = "Swing4"; 
	$itemHitFlesh[WaterBroadsword]   = "WeaponHit1";
	$itemHitWall[WaterBroadsword]    = ""; 
	$PrefixExclusions[WaterBroadsword] = ",";
	$SuffixExclusions[WaterBroadsword] = "3,";
	$SkillType[WaterBroadsword] = $Skill::Slashing;
	$DataBlock[WaterBroadsword] = "WaterBroadsword";
	$shop::BuyPrice[WaterBroadsword] = 20000;

	$ItemType[BattleAxe] = "weapon";
	$ItemSubType[BattleAxe] = $SwordAccessoryType;
	$ItemDesc[BattleAxe] = "BattleAxe";
	$ItemSize[BattleAxe] = "small";
	$ItemBaseWeight[BattleAxe] = 0.5;
	$ItemBaseDelay[BattleAxe] = 3.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[BattleAxe] = "6 1r25";
	$ItemBaseRange[BattleAxe] = $minRange + 6.0;
	$ItemDamageType[BattleAxe] = $DamageType::Slashing;
	$itemSwingSound[BattleAxe] = "Swing4"; 
	$itemHitFlesh[BattleAxe]   = "WeaponHit1";
	$itemHitWall[BattleAxe]    = ""; 
	$PrefixExclusions[BattleAxe] = ",";
	$SuffixExclusions[BattleAxe] = "3,";
	$SkillType[BattleAxe] = $Skill::Slashing;
	$DataBlock[BattleAxe] = "Bardiche";
	$shop::BuyPrice[BattleAxe] = 40000;
	$item::smith[BattleAxe,0] = "iron 6 coal 4 granite 2 Mithril 1 Topaz 1 Rod 1"; 
	$item::smith[BattleAxe,1] = "copper 1 tin 1 Rod 1 iron 5 coal 2"; 
	$item::smith[BattleAxe,2] = "iron 2 granite 1 coal 1"; 
	$item::smith[BattleAxe,3] = "iron 1 coal 4 granite 1"; 
	$item::smith[BattleAxe,4] = "iron 5 coal 8 Mithril 2"; 
	$item::smith[BattleAxe,5] = "iron 7 coal 8 diere 1 Mithril 3 Topaz 1";
	
	$ItemType[BastardSword] = "weapon";
	$ItemSubType[BastardSword] = $SwordAccessoryType;
	$ItemDesc[BastardSword] = "Bastard Sword";
	$ItemSize[BastardSword] = "small";
	$ItemBaseWeight[BastardSword] = 0.5;
	$ItemBaseDelay[BastardSword] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[BastardSword] = "6 1r35";
	$ItemBaseRange[BastardSword] = $minRange + 2.0;
	$ItemDamageType[BastardSword] = $DamageType::Slashing;
	$itemSwingSound[BastardSword] = "Swing4"; 
	$itemHitFlesh[BastardSword]   = "WeaponHit1";
	$itemHitWall[BastardSword]    = ""; 
	$PrefixExclusions[BastardSword] = ",";
	$SuffixExclusions[BastardSword] = "3,";
	$SkillType[BastardSword] = $Skill::Slashing;
	$DataBlock[BastardSword] = "BastardSword";
	$shop::BuyPrice[BastardSword] = 60000;
	$item::smith[BastardSword,0] = "iron 10 coal 5 Mithril 2 Topaz 1 Rod 1 Diere 1"; 
	$item::smith[BastardSword,1] = "copper 1 tin 1 Rod 1 iron 6 coal 4"; 
	$item::smith[BastardSword,2] = "iron 4 granite 1 coal 2"; 
	$item::smith[BastardSword,3] = "iron 3 coal 4 granite 1 Diere 1"; 
	$item::smith[BastardSword,4] = "iron 5 coal 8 Mithril 2 Diere 2"; 
	$item::smith[BastardSword,5] = "iron 7 coal 8 diere 13 Mithril 3";
//bludgeoning
//club			->		1
//quarterstaff		->		3
//spikedclub		->		5
//mace			->		7
//hammerpick		->		12 <- swings 3x faster than the battleaxe. 2x faster than the pickaxe
//warhammer		->		30
	$ItemType[Club] = "weapon";
	$ItemSubType[Club] = $BludgeonAccessoryType;
	$ItemDesc[Club] = "Club";
	$ItemSize[Club] = "small";
	$ItemBaseWeight[Club] = 0.5;
	$ItemBaseDelay[Club] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Club] = "6 1r1";
	$ItemBaseRange[Club] = $minRange + 3.0;
	$ItemDamageType[Club] = $DamageType::Bludgeoning;
	$itemSwingSound[Club] = "Swing4"; //swing sound
	$itemHitFlesh[Club]   = "WeaponHit1"; // hit a player or bot
	$itemHitWall[Club]    = ""; // hit a wall
	$PrefixExclusions[Club] = ",";
	$SuffixExclusions[Club] = "3,";
	$SkillType[Club] = $Skill::Bludgeoning;
	$DataBlock[Club] = "club";
	$shop::BuyPrice[Club] = 100;
	$item::smith[Club,0] = "Rod 1 Clay 5 tin 1 copper 1"; 
	$item::smith[Club,1] = "Clay 1"; 
	$item::smith[Club,2] = "Clay 2"; 
	$item::smith[Club,3] = "Clay 3 tin 1 copper 1"; 
	$item::smith[Club,4] = "Clay 4 tin 2 copper 2"; 
	$item::smith[Club,5] = "Clay 5 iron 2"; 

	$ItemType[QuarterStaff] = "weapon";
	$ItemSubType[QuarterStaff] = $SwordAccessoryType;
	$ItemDesc[QuarterStaff] = "QuarterStaff";
	$ItemSize[QuarterStaff] = "small";
	$ItemBaseWeight[QuarterStaff] = 0.5;
	$ItemBaseDelay[QuarterStaff] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[QuarterStaff] = "6 1r3";
	$ItemBaseRange[QuarterStaff] = $minRange + 3.0;
	$ItemDamageType[QuarterStaff] = $DamageType::Bludgeoning;
	$itemSwingSound[QuarterStaff] = "Swing4"; 
	$itemHitFlesh[QuarterStaff]   = "WeaponHit1";
	$itemHitWall[QuarterStaff]    = ""; 
	$PrefixExclusions[QuarterStaff] = ",";
	$SuffixExclusions[QuarterStaff] = "3,";
	$SkillType[QuarterStaff] = $Skill::Bludgeoning;
	$DataBlock[QuarterStaff] = "Quarterstaff";
	$shop::BuyPrice[QuarterStaff] = 1000;
	$item::smith[QuarterStaff,0] = "Rod 1 iron 5"; 
	$item::smith[QuarterStaff,1] = "iron 1"; 
	$item::smith[QuarterStaff,2] = "iron 2"; 
	$item::smith[QuarterStaff,3] = "iron 3"; 
	$item::smith[QuarterStaff,4] = "iron 5 coal 1"; 
	$item::smith[QuarterStaff,5] = "iron 10 coal 2";	

	$ItemType[SpikedClub] = "weapon";
	$ItemSubType[SpikedClub] = $BludgeonAccessoryType;
	$ItemDesc[SpikedClub] = "Spiked Club";
	$ItemSize[SpikedClub] = "small";
	$ItemBaseWeight[SpikedClub] = 0.5;
	$ItemBaseDelay[SpikedClub] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[SpikedClub] = "6 1r5";
	$ItemBaseRange[SpikedClub] = $minRange + 3.0;
	$ItemDamageType[SpikedClub] = $DamageType::Bludgeoning;
	$itemSwingSound[SpikedClub] = "Swing4"; 
	$itemHitFlesh[SpikedClub]   = "WeaponHit1";
	$itemHitWall[SpikedClub]    = ""; 
	$PrefixExclusions[SpikedClub] = ",";
	$SuffixExclusions[SpikedClub] = "3,";
	$SkillType[SpikedClub] = $Skill::Bludgeoning;
	$DataBlock[SpikedClub] = "spikedclub";
	$shop::BuyPrice[SpikedClub] = 3000;
	$item::smith[SpikedClub,0] = "granite 6 tin 2 copper 2 iron 1 Rod 1"; 
	$item::smith[SpikedClub,1] = "tin 1 copper 1"; 
	$item::smith[SpikedClub,2] = "iron 1"; 
	$item::smith[SpikedClub,3] = "iron 1 granite 1"; 
	$item::smith[SpikedClub,4] = "iron 2 granite 2 tin 1 copper 1"; 
	$item::smith[SpikedClub,5] = "iron 2 coal 2 granite 1"; 

	$ItemType[Mace] = "weapon";
	$ItemSubType[Mace] = $BludgeonAccessoryType;
	$ItemDesc[Mace] = "Mace";
	$ItemSize[Mace] = "small";
	$ItemBaseWeight[Mace] = 1.0;
	$ItemBaseDelay[Mace] = 2 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Mace] = "6 1r7";
	$ItemBaseRange[Mace] = $minRange + 2.0;
	$ItemDamageType[Mace] = $DamageType::Bludgeoning;
	$itemSwingSound[Mace] = "Swing4"; 
	$itemHitFlesh[Mace]   = "WeaponHit1";
	$itemHitWall[Mace]    = ""; 
	$PrefixExclusions[Mace] = ",";
	$SuffixExclusions[Mace] = ",";
	$SkillType[Mace] = $Skill::Bludgeoning;
	$DataBlock[Mace] = "Mace";
	$shop::BuyPrice[Mace] = 10000;
	$item::smith[Mace,0] = "granite 5 iron 2"; 
	$item::smith[Mace,1] = "tin 1 copper 1"; 
	$item::smith[Mace,2] = "iron 1 granite 1"; 
	$item::smith[Mace,3] = "iron 1 coal 1"; 
	$item::smith[Mace,4] = "iron 2 coal 1 granite 1"; 
	$item::smith[Mace,5] = "coal 1 diere 2"; 

	$ItemType[HammerPick] = "weapon";
	$ItemSubType[HammerPick] = $BludgeonAccessoryType;
	$ItemDesc[HammerPick] = "Hammer Pick";
	$ItemSize[HammerPick] = "small";
	$ItemBaseWeight[HammerPick] = 1.0;
	$ItemBaseDelay[HammerPick] = 1 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[HammerPick] = "6 1r12";
	$ItemBaseRange[HammerPick] = $minRange + 2.0;
	$ItemDamageType[HammerPick] = $DamageType::Bludgeoning;
	$itemSwingSound[HammerPick] = "Swing4"; 
	$itemHitFlesh[HammerPick]   = "WeaponHit1";
	$itemHitWall[HammerPick]    = ""; 
	$PrefixExclusions[HammerPick] = ",";
	$SuffixExclusions[HammerPick] = ",";
	$SkillType[HammerPick] = $Skill::Bludgeoning;
	$DataBlock[HammerPick] = "Pickaxe";
	$shop::BuyPrice[HammerPick] = 40000;
	$item::smith[HammerPick,0] = "granite 5 iron 2 Rod 1 coal 6"; 
	$item::smith[HammerPick,1] = "tin 1 copper 1 Rod 1"; 
	$item::smith[HammerPick,2] = "iron 1 granite 1"; 
	$item::smith[HammerPick,3] = "iron 1 coal 2"; 
	$item::smith[HammerPick,4] = "iron 2 coal 5 granite 5"; 
	$item::smith[HammerPick,5] = "iron 3 coal 6 Jade 1";


	$ItemType[WarHammer] = "weapon";
	$ItemSubType[WarHammer] = $BludgeonAccessoryType;
	$ItemDesc[WarHammer] = "WarHammer";
	$ItemSize[WarHammer] = "small";
	$ItemBaseWeight[WarHammer] = 1.0;
	$ItemBaseDelay[WarHammer] = 3 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[WarHammer] = "6 1r30";
	$ItemBaseRange[WarHammer] = $minRange + 2.0;
	$ItemDamageType[WarHammer] = $DamageType::Bludgeoning;
	$itemSwingSound[WarHammer] = "Swing4"; 
	$itemHitFlesh[WarHammer]   = "WeaponHit1";
	$itemHitWall[WarHammer]    = ""; 
	$PrefixExclusions[WarHammer] = ",";
	$SuffixExclusions[WarHammer] = ",";
	$SkillType[WarHammer] = $Skill::Bludgeoning;
	$DataBlock[WarHammer] = "WarHammer";
	$shop::BuyPrice[WarHammer] = 80000;	
	$item::smith[WarHammer,0] = "granite 5 iron 2 Rod 1 coal 6 Diere 1"; 
	$item::smith[WarHammer,1] = "tin 1 copper 1 Rod 1"; 
	$item::smith[WarHammer,2] = "iron 1 granite 1 Diere 1"; 
	$item::smith[WarHammer,3] = "iron 1 coal 2 Diere 1"; 
	$item::smith[WarHammer,4] = "iron 2 coal 5 granite 5 Diere 2"; 
	$item::smith[WarHammer,5] = "iron 3 coal 6 Jade 1 Diere 3";
//bows
//sling 			-> 1
//shortbow			-> 3
//lightcrossbow			-> 5
//longbow			-> 7
//compositebow			-> 11

	$ItemType[Sling] = "weapon";
	$ItemSubType[Sling] = $BludgeonAccessoryType;
	$ItemDesc[Sling] = "Sling";
	$ItemSize[Sling] = "small";
	$ItemBaseWeight[Sling] = 1.0;
	$ItemBaseDelay[Sling] = 2 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Sling] = "6 1r1";
	$ItemBaseRange[Sling] = $minRange + 50.0;
	$ItemDamageType[Sling] = $DamageType::Archery;
	$itemSwingSound[Sling] = "Swing4"; 
	$itemHitFlesh[Sling]   = "WeaponHit1";
	$itemHitWall[Sling]    = ""; 
	$itemAmmo[Sling] = "SmallRock";
	$PrefixExclusions[Sling] = ",";
	$SuffixExclusions[Sling] = ",";
	$SkillType[Sling] = $SkillArchery;
	$DataBlock[Sling] = "Sling";
	$shop::BuyPrice[Sling] = 100;
	$item::smith[Sling,0] = "Rod 1 Copper 2 Tin 2 String 1"; 
	$item::smith[Sling,1] = "Clay 1 Rod 1"; 
	$item::smith[Sling,2] = "Clay 5"; 
	$item::smith[Sling,3] = "Copper 1 Tin 1"; 
	$item::smith[Sling,4] = "Copper 2 Tin 2"; 
	$item::smith[Sling,5] = "Copper 3 Tin 3 Clay 5 Iron 1"; 

	$ItemType[Shortbow] = "weapon";
	$ItemSubType[Shortbow] = $BludgeonAccessoryType;
	$ItemDesc[Shortbow] = "Shortbow";
	$ItemSize[Shortbow] = "small";
	$ItemBaseWeight[Shortbow] = 1.0;
	$ItemBaseDelay[Shortbow] = 2 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Shortbow] = "6 1r3";
	$ItemBaseRange[Shortbow] = $minRange + 100.0;
	$ItemDamageType[Shortbow] = $DamageType::Archery;
	$itemSwingSound[Shortbow] = "Swing4"; 
	$itemHitFlesh[Shortbow]   = "WeaponHit1";
	$itemHitWall[Shortbow]    = ""; 
	$itemAmmo[ShortBow] = "BasicArrow";
	$PrefixExclusions[Shortbow] = ",";
	$SuffixExclusions[Shortbow] = ",";
	$SkillType[Shortbow] = $SkillArchery;
	$DataBlock[Shortbow] = "Shortbow";
	$shop::BuyPrice[Shortbow] = 1000;
	$item::smith[Shortbow,0] = "Rod 1 Iron 1 Copper 2 Tin 2 String 1"; 
	$item::smith[Shortbow,1] = "Clay 5 Rod 1"; 
	$item::smith[Shortbow,2] = "Copper 1 Tin 1 Clay 5"; 
	$item::smith[Shortbow,3] = "Iron 1"; 
	$item::smith[Shortbow,4] = "Copper 2 Tin 2 Iron 1"; 
	$item::smith[Shortbow,5] = "Copper 3 Tin 3 Clay 5 Iron 2"; 
	
	$ItemType[LightCrossbow] = "weapon";
	$ItemSubType[LightCrossbow] = $BludgeonAccessoryType;
	$ItemDesc[LightCrossbow] = "Light Crossbow";
	$ItemSize[LightCrossbow] = "small";
	$ItemBaseWeight[LightCrossbow] = 1.0;
	$ItemBaseDelay[LightCrossbow] = 2 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[LightCrossbow] = "6 1r5";
	$ItemBaseRange[LightCrossbow] = $minRange + 150.0;
	$ItemDamageType[LightCrossbow] = $DamageType::Archery;
	$itemSwingSound[LightCrossbow] = "Swing4"; 
	$itemHitFlesh[LightCrossbow]   = "WeaponHit1";
	$itemHitWall[LightCrossbow]    = ""; 
	$itemAmmo[LightCrossbow] = "ShortQuarrel";
	$PrefixExclusions[LightCrossbow] = ",";
	$SuffixExclusions[LightCrossbow] = ",";
	$SkillType[LightCrossbow] = $SkillArchery;
	$DataBlock[LightCrossbow] = "LightCrossbow";
	$shop::BuyPrice[LightCrossbow] = 3000;
	$item::smith[LightCrossbow,0] = "Rod 1 Iron 5 Copper 2 Tin 2 String 1"; 
	$item::smith[LightCrossbow,1] = "Clay 5 Rod 1 iron 2"; 
	$item::smith[LightCrossbow,2] = "Copper 1 Tin 1 Clay 5 iron 1"; 
	$item::smith[LightCrossbow,3] = "Iron 3"; 
	$item::smith[LightCrossbow,4] = "Copper 2 Tin 2 Iron 4"; 
	$item::smith[LightCrossbow,5] = "Copper 3 Tin 3 Clay 5 Iron 8"; 
	//$ItemType[Longbow] = "weapon";
	//$ItemSubType[Longbow] = $BludgeonAccessoryType;
	//$ItemDesc[Longbow] = "Longbow";
	//$ItemSize[Longbow] = "small";
	//$ItemBaseWeight[Longbow] = 1.0;
	//$ItemBaseDelay[Longbow] = 2 * $ADnDdelayToRPG;
	//$ItemBaseSpecialVar[Longbow] = "6 1r5";
	//$ItemBaseRange[Longbow] = $minRange + 2.0;
	//$ItemDamageType[Longbow] = $DamageType::Bludgeoning;
	//$itemSwingSound[Longbow] = "Swing4"; 
	//$itemHitFlesh[Longbow]   = "WeaponHit1";
	//$itemHitWall[Longbow]    = ""; 
	//$itemAmmo[Longbow] = "BasicArrow";
	//$PrefixExclusions[Longbow] = ",";
	//$SuffixExclusions[Longbow] = ",";
	//$SkillType[Longbow] = $SkillArchery;
	//$DataBlock[Longbow] = "Longbow";
	//$shop::BuyPrice[Longbow] = 3000;
// not yet used

	$ItemType[WarMaul] = "weapon";
	$ItemSubType[WarMaul] = $BludgeonAccessoryType;
	$ItemDesc[WarMaul] = "WarMaul";
	$ItemSize[WarMaul] = "small";
	$ItemBaseWeight[WarMaul] = 0.5;
	$ItemBaseDelay[WarMaul] = 5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[WarMaul] = "6 1r150";
	$ItemBaseRange[WarMaul] = $minRange + 3.0;
	$ItemDamageType[WarMaul] = $DamageType::Bludgeoning;
	$itemSwingSound[WarMaul] = "Swing4"; 
	$itemHitFlesh[WarMaul]   = "WeaponHit1";
	$itemHitWall[WarMaul]    = ""; 
	$PrefixExclusions[WarMaul] = ",";
	$SuffixExclusions[WarMaul] = "3,";
	$SkillType[WarMaul] = $Skill::Bludgeoning;
	$DataBlock[WarMaul] = "WarMaul";
	$shop::BuyPrice[WarMaul] = 60000;

	$ItemType[Katana] = "weapon";
	$ItemSubType[Katana] = $SwordAccessoryType;
	$ItemDesc[Katana] = "Katana";
	$ItemSize[Katana] = "small";
	$ItemBaseWeight[Katana] = 0.5;
	$ItemBaseDelay[Katana] = 2.0 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Katana] = "6 1r160";
	$ItemBaseRange[Katana] = $minRange + 3.0;
	$ItemDamageType[Katana] = $DamageType::Piercing;
	$itemSwingSound[Katana] = "Swing4"; 
	$itemHitFlesh[Katana]   = "WeaponHit1";
	$itemHitWall[Katana]    = ""; 
	$PrefixExclusions[Katana] = ",";
	$SuffixExclusions[Katana] = "3,";
	$SkillType[Katana] = $SkillPiercing;
	$DataBlock[Katana] = "Katana";
	$shop::BuyPrice[katana] = 80000;

	
	$ItemType[GreatClaymore] = "weapon";
	$ItemSubType[GreatClaymore] = $SwordAccessoryType;
	$ItemDesc[GreatClaymore] = "Great Claymore";
	$ItemSize[GreatClaymore] = "small";
	$ItemBaseWeight[GreatClaymore] = 0.5;
	$ItemBaseDelay[GreatClaymore] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[GreatClaymore] = "6 1r200";
	$ItemBaseRange[GreatClaymore] = $minRange + 4.0;
	$ItemDamageType[GreatClaymore] = $DamageType::Slashing;
	$itemSwingSound[GreatClaymore] = "Swing4"; 
	$itemHitFlesh[GreatClaymore]   = "WeaponHit1";
	$itemHitWall[GreatClaymore]    = ""; 
	$PrefixExclusions[GreatClaymore] = ",";
	$SuffixExclusions[GreatClaymore] = "3,";
	$SkillType[GreatClaymore] = $Skill::Slashing;
	$DataBlock[GreatClaymore] = "GreatClaymore";
	$shop::BuyPrice[GreatClaymore] = 100000;

	$ItemType[Bardiche] = "weapon";
	$ItemSubType[Bardiche] = $PolearmAccessoryType;
	$ItemDesc[Bardiche] = "Bardiche";
	$ItemSize[Bardiche] = "small";
	$ItemBaseWeight[Bardiche] = 0.5;
	$ItemBaseDelay[Bardiche] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Bardiche] = "6 1r100";
	$ItemBaseRange[Bardiche] = $minRange + 2.0;
	$ItemDamageType[Bardiche] = $DamageType::Slashing;
	$itemSwingSound[Bardiche] = "Swing4"; 
	$itemHitFlesh[Bardiche]   = "WeaponHit1";
	$itemHitWall[Bardiche]    = ""; 
	$PrefixExclusions[Bardiche] = ",";
	$SuffixExclusions[Bardiche] = "3,";
	$SkillType[Bardiche] = $Skill::Slashing;
	$DataBlock[Bardiche] = "Bardiche";
	$shop::BuyPrice[Bardiche] = 50000;

	$ItemType[Claymore] = "weapon";
	$ItemSubType[Claymore] = $SwordAccessoryType;
	$ItemDesc[Claymore] = "Claymore";
	$ItemSize[Claymore] = "small";
	$ItemBaseWeight[Claymore] = 0.5;
	$ItemBaseDelay[Claymore] = 2.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[Claymore] = "6 1r190";
	$ItemBaseRange[Claymore] = $minRange + 2.0;
	$ItemDamageType[Claymore] = $DamageType::Slashing;
	$itemSwingSound[Claymore] = "Swing4"; 
	$itemHitFlesh[Claymore]   = "WeaponHit1";
	$itemHitWall[Claymore]    = ""; 
	$PrefixExclusions[Claymore] = ",";
	$SuffixExclusions[Claymore] = "3,";
	$SkillType[Claymore] = $Skill::Slashing;
	$DataBlock[Claymore] = "Claymore";
	$shop::BuyPrice[Claymore] = 90000;



	//############################################################
	$numarmor = 0;
	$armorlist[$numarmor++] = "PaddedArmor";
	$armorlist[$numarmor++] = "LeatherArmor";
	$armorlist[$numarmor++] = "SpikedLeatherArmor";
	$armorlist[$numarmor++] = "hidearmor";
	$armorlist[$numarmor++] = "BasicRobe";
	$ArmorList[$numarmor++] = "ApprenticeRobe";
	$ArmorList[$numarmor++] = "StuddedLeatherArmor";
	$ArmorList[$numarmor++] = "ScaleMailBody";
	$ArmorList[$numarmor++] = "BrigandineBody";
	$ArmorList[$numarmor++] = "ChainMailBody";
	$ArmorList[$numarmor++] = "RingMailBody";
	$ArmorList[$numarmor++] = "BandedMailArmor";
	$ArmorList[$numarmor++] = "SplintMailBody";
	$ArmorList[$numarmor++] = "BronzePlate";
	$ArmorList[$numarmor++] = "HalfPlate";
	$ArmorList[$numarmor++] = "FieldPlate";
	$ArmorList[$numarmor++] = "DragonMail";
	$ArmorList[$numarmor++] = "FullPlate";
	$ArmorList[$numarmor++] = "KeldrinitePlate";
	$ArmorList[$numarmor++] = "ApprenticeRobe";
	$ArmorList[$numarmor++] = "LightRobe";
	$ArmorList[$numarmor++] = "FineRobe";
	$ArmorList[$numarmor++] = "BloodRobe";
	$ArmorList[$numarmor++] = "AdvisorRobe";
	$ArmorList[$numarmor++] = "ElvenRobe";
	$ArmorList[$numarmor++] = "PhensRobe";
	$ArmorList[$numarmor++] = "HolyRobe";
	
	$ItemType[PaddedArmor] = "armor";
	$ItemSubType[PaddedArmor] = $BodyAccessoryType;
	$ItemDesc[PaddedArmor] = "Padded Armor";
	$ItemSize[PaddedArmor] = "medium";
	$ItemBaseWeight[PaddedArmor] = 3.0;
	$ItemBaseSpecialVar[PaddedArmor] = "7 1r6";
	$PrefixExclusions[PaddedArmor] = ",";
	$SuffixExclusions[PaddedArmor] = ",";
	$DataBlock[PaddedArmor] = "armor1";
	$shop::BuyPrice[PaddedArmor] = 100;
	$item::smith[PaddedArmor,0] = "GnollHide 3 Copper 1 Tin 1 Clay 15"; 
	$item::smith[PaddedArmor,1] = "GnollHide 1 Copper 1 Tin 1 Clay 5"; 
	$item::smith[PaddedArmor,2] = "GnollHide 2 Copper 1 Tin 1 Clay 5"; 
	$item::smith[PaddedArmor,3] = "GnollHide 3 Copper 1 Tin 1 Clay 5"; 
	$item::smith[PaddedArmor,4] = "GnollHide 4 Copper 3 Tin 3 Clay 10"; 
	$item::smith[PaddedArmor,5] = "GnollHide 7 tin 3 Copper 3 Clay 15"; 

	$ItemType[LeatherArmor] = "armor";
	$ItemSubType[LeatherArmor] = $BodyAccessoryType;
	$ItemDesc[LeatherArmor] = "Leather Armor";
	$ItemSize[LeatherArmor] = "medium";
	$ItemBaseWeight[LeatherArmor] = 3.0;
	$ItemBaseSpecialVar[LeatherArmor] = "7 2r7";
	$PrefixExclusions[LeatherArmor] = ",";
	$SuffixExclusions[LeatherArmor] = ",";
	$DataBlock[LeatherArmor] = "armor1";
	$shop::BuyPrice[LeatherArmor] = 1000;
	$item::smith[LeatherArmor,0] = "GnollHide 5 Iron 1 Clay 10 Copper 1 Tin 1"; 
	$item::smith[LeatherArmor,1] = "GnollHide 2 Clay 4 Copper 1 Tin 1"; 
	$item::smith[LeatherArmor,2] = "GnollHide 3 Clay 6 Copper 1 Tin 1"; 
	$item::smith[LeatherArmor,3] = "GnollHide 4 Clay 8 Copper 1 Tin 1"; 
	$item::smith[LeatherArmor,4] = "GnollHide 5 tin 3 Clay 12 Copper 3"; 
	$item::smith[LeatherArmor,5] = "GnollHide 8 tin 4 copper 4 Clay 15 Iron 1"; 
		
	$ItemType[SpikedLeatherArmor] = "armor";
	$ItemSubType[SpikedLeatherArmor] = $BodyAccessoryType;
	$ItemDesc[SpikedLeatherArmor] = "Spiked Leather Armor";
	$ItemSize[SpikedLeatherArmor] = "medium";
	$ItemBaseWeight[SpikedLeatherArmor] = 3.0;
	$ItemBaseSpecialVar[SpikedLeatherArmor] = "7 3r8";
	$PrefixExclusions[SpikedLeatherArmor] = ",";
	$SuffixExclusions[SpikedLeatherArmor] = ",";
	$DataBlock[SpikedLeatherArmor] = "armor1";
	$shop::BuyPrice[SpikedLeatherArmor] = 2000;
	$item::smith[SpikedLeatherArmor,0] = "GnollHide 10 tin 10 copper 10 Iron 6"; 
	$item::smith[SpikedLeatherArmor,1] = "GnollHide 3 Clay 5 tin 3 copper 3 "; 
	$item::smith[SpikedLeatherArmor,2] = "GnollHide 4 Clay 5 Iron 1 Tin 4 Copper 4"; 
	$item::smith[SpikedLeatherArmor,3] = "GnollHide 5 Clay 8 Iron 3 tin 3 copper 3"; 
	$item::smith[SpikedLeatherArmor,4] = "GnollHide 6 Clay 10 iron 1 Tin 4 Copper 4"; 
	$item::smith[SpikedLeatherArmor,5] = "GnollHide 10 iron 4 Copper 5 Tin 5 Clay 15";

	$ItemType[StuddedLeatherArmor] = "armor";
	$ItemSubType[StuddedLeatherArmor] = $BodyAccessoryType;
	$ItemDesc[StuddedLeatherArmor] = "Studded Leather Armor";
	$ItemSize[StuddedLeatherArmor] = "medium";
	$ItemBaseWeight[StuddedLeatherArmor] = 3.0;
	$ItemBaseSpecialVar[StuddedLeatherArmor] = "7 4r9";
	$PrefixExclusions[StuddedLeatherArmor] = ",";
	$SuffixExclusions[StuddedLeatherArmor] = ",";
	$DataBlock[StuddedLeatherArmor] = "armor1";
	$shop::BuyPrice[StuddedLeatherArmor] = 4000;
	$item::smith[StuddedLeatherArmor,0] = "GnollHide 16 tin 15 copper 10 Iron 10"; 
	$item::smith[StuddedLeatherArmor,1] = "GnollHide 5 Clay 10 tin 6 Copper 6"; 
	$item::smith[StuddedLeatherArmor,2] = "GnollHide 4 Clay 10 Iron 1 Tin 4 Copper 4"; 
	$item::smith[StuddedLeatherArmor,3] = "GnollHide 7 Clay 12 Iron 2 tin 3 copper 3"; 
	$item::smith[StuddedLeatherArmor,4] = "GnollHide 8 Clay 18 Iron 5 Tin 8 Copper 8"; 
	$item::smith[StuddedLeatherArmor,5] = "GnollHide 14 Iron 15 Copper 15 Tin 15 Clay 25";

	$ItemType[HideArmor] = "armor";
	$ItemSubType[HideArmor] = $BodyAccessoryType;
	$ItemDesc[HideArmor] = "Hide Armor";
	$ItemSize[HideArmor] = "medium";
	$ItemBaseWeight[HideArmor] = 3.0;
	$ItemBaseSpecialVar[HideArmor] = "7 2r12 24 6r12";
	$PrefixExclusions[HideArmor] = ",";
	$SuffixExclusions[HideArmor] = ",";
	$DataBlock[HideArmor] = "armor1";
	$shop::BuyPrice[HideArmor] = 5000;
	$item::smith[HideArmor,0] = "GnollHide 20 tin 10 copper 10 Iron 6"; 
	$item::smith[HideArmor,1] = "GnollHide 6 Clay 15 tin 3 copper 3 "; 
	$item::smith[HideArmor,2] = "GnollHide 8 Clay 15 Iron 1 Tin 4 Copper 4"; 
	$item::smith[HideArmor,3] = "GnollHide 10 Clay 18 Iron 4 tin 3 copper 3"; 
	$item::smith[HideArmor,4] = "GnollHide 12 Clay 20 iron 2 Tin 4 Copper 4"; 
	$item::smith[HideArmor,5] = "GnollHide 20 iron 4 Copper 5 Tin 5 Clay 25 Coal 1";

	$ItemType[ScaleMailBody] = "armor";
	$ItemSubType[ScaleMailBody] = $BodyAccessoryType;
	$ItemDesc[ScaleMailBody] = "Scale Mail Body";
	$ItemSize[ScaleMailBody] = "medium";
	$ItemBaseWeight[ScaleMailBody] = 3.0;
	$ItemBaseSpecialVar[ScaleMailBody] = "7 3r16 20 8r16";//fire defense
	$PrefixExclusions[ScaleMailBody] = ",";
	$SuffixExclusions[ScaleMailBody] = ",";
	$DataBlock[ScaleMailBody] = "armor1";
	$shop::BuyPrice[ScaleMailBody] = 10000;
	$itemLongDesc[ScaleMailBody] = "Strong Iron Scales also provide protection against fire damage";
	$item::smith[ScaleMailBody,0] = "Iron 15 Tin 5 Copper 5 Clay 10 Granite 5"; 
	$item::smith[ScaleMailBody,1] = "Iron 5 Tin 3 Copper 3 Clay 15 Granite 1"; 
	$item::smith[ScaleMailBody,2] = "Iron 7 Tin 1 Copper 1 Clay 5 Granite 2"; 
	$item::smith[ScaleMailBody,3] = "Iron 10 Tin 4 Copper 4 Clay 4 Granite 3"; 
	$item::smith[ScaleMailBody,4] = "Iron 15 Tin 5 Copper 5 Clay 5 Granite 5"; 
	$item::smith[ScaleMailBody,5] = "Iron 25 Tin 10 Copper 10 Clay 10 Granite 7 Coal 3";	

	$ItemType[BrigandineBody] = "armor";
	$ItemSubType[BrigandineBody] = $BodyAccessoryType;
	$ItemDesc[BrigandineBody] = "Brigandine Armor";
	$ItemSize[BrigandineBody] = "medium";
	$ItemBaseWeight[BrigandineBody] = 3.0;
	$ItemBaseSpecialVar[BrigandineBody] = "7 3r20 21 8r18";//water defense
	$PrefixExclusions[BrigandineBody] = ",";
	$SuffixExclusions[BrigandineBody] = ",";
	$DataBlock[BrigandineBody] = "armor1";
	$shop::BuyPrice[BrigandineBody] = 15000;
	$itemLongDesc[BrigandineBody] = "";
	$item::smith[BrigandineBody,0] = "Iron 30 Tin 10 Copper 10 Clay 25"; 
	$item::smith[BrigandineBody,1] = "Iron 10 Tin 5 Copper 5 Clay 10"; 
	$item::smith[BrigandineBody,2] = "Iron 15 Tin 3 Copper 3 Clay 5"; 
	$item::smith[BrigandineBody,3] = "Iron 20 Tin 5 Copper 5 Clay 6"; 
	$item::smith[BrigandineBody,4] = "Iron 25 Tin 10 Copper 10 Clay 10"; 
	$item::smith[BrigandineBody,5] = "Iron 35 Tin 12 Copper 12 Clay 15 Coal 5 Diere 1 Opal 1";

	$ItemType[ChainMailBody] = "armor";
	$ItemSubType[ChainMailBody] = $BodyAccessoryType;
	$ItemDesc[ChainMailBody] = "Chain Mail Body";
	$ItemSize[ChainMailBody] = "medium";
	$ItemBaseWeight[ChainMailBody] = 3.0;
	$ItemBaseSpecialVar[ChainMailBody] = "7 4r30";
	$PrefixExclusions[ChainMailBody] = ",";
	$SuffixExclusions[ChainMailBody] = ",";
	$DataBlock[ChainMailBody] = "armor1";
	$shop::BuyPrice[ChainMailBody] = 20000;
	$itemLongDesc[ChainMailBody] = "";
	$item::smith[ChainMailBody,0] = "Iron 25 Coal 10 Tin 10 Copper 10 Clay 15"; 
	$item::smith[ChainMailBody,1] = "Iron 5 Coal 5 Tin 5 Copper 5 Clay 5"; 
	$item::smith[ChainMailBody,2] = "Iron 10 Coal 5 Tin 4 Copper 4 Clay 10"; 
	$item::smith[ChainMailBody,3] = "Iron 25 Coal 7 Tin 8 Copper 8 Clay 15"; 
	$item::smith[ChainMailBody,4] = "Iron 30 COal 10 Tin 10 Copper 10 Clay 20"; 
	$item::smith[ChainMailBody,5] = "Iron 35 Coal 15 Tin 15 Copper 13 Clay 25 Diere 3 Jade 1";
	
	$ItemType[RingMailBody] = "armor";
	$ItemSubType[RingMailBody] = $BodyAccessoryType;
	$ItemDesc[RingMailBody] = "Ring Mail Body";
	$ItemSize[RingMailBody] = "medium";
	$ItemBaseWeight[RingMailBody] = 3.0;
	$ItemBaseSpecialVar[RingMailBody] = "7 4r38";
	$PrefixExclusions[RingMailBody] = ",";
	$SuffixExclusions[RingMailBody] = ",";
	$DataBlock[RingMailBody] = "armor1";
	$shop::BuyPrice[RingMailBody] = 25000;
	$itemLongDesc[RingMailBody] = "";
	$item::smith[RingMailBody,0] = "Iron 35 Coal 20 Copper 15 Tin 15"; 
	$item::smith[RingMailBody,1] = "Iron 15 Coal 5 Copper 5 Tin 5 Clay 10"; 
	$item::smith[RingMailBody,2] = "Iron 10 Coal 10 Copper 5 Tin 5 Clay 15"; 
	$item::smith[RingMailBody,3] = "Iron 20 Coal 15 Copper 10 Tin 10 Clay 10"; 
	$item::smith[RingMailBody,4] = "Iron 30 Coal 25 Copper 15 Tin 15 Clay 15"; 
	$item::smith[RingMailBody,5] = "Iron 45 Coal 35 Copper 20 TIn 20 Clay 30 Diere 5";
	
	$ItemType[BandedMailArmor] = "armor";
	$ItemSubType[BandedMailArmor] = $BodyAccessoryType;
	$ItemDesc[BandedMailArmor] = "Banded Mail Body";
	$ItemSize[BandedMailArmor] = "medium";
	$ItemBaseWeight[BandedMailArmor] = 3.0;
	$ItemBaseSpecialVar[BandedMailArmor] = "7 4r40 24 10r23 21 6r12";
	$PrefixExclusions[BandedMailArmor] = ",";
	$SuffixExclusions[BandedMailArmor] = ",";
	$DataBlock[BandedMailArmor] = "armor1";
	$shop::BuyPrice[BandedMailArmor] = 35000;
	$itemLongDesc[BandedMailArmor] = "";
	$item::smith[BandedMailArmor,0] = "Diere 5 Coal 20 Iron 20 Copper 5 Tin 5"; 
	$item::smith[BandedMailArmor,1] = "Diere 2 Coal 12 Iron 10 Copper 1 Tin 1"; 
	$item::smith[BandedMailArmor,2] = "Diere 1 Coal 6 Iron 5 Copper 3 Tin 3"; 
	$item::smith[BandedMailArmor,3] = "Diere 4 Coal 15 Iron 15 Copper 6 Tin 6"; 
	$item::smith[BandedMailArmor,4] = "Diere 6 Coal 25 Iron 25 Copper 10 Tin 10"; 
	$item::smith[BandedMailArmor,5] = "Diere 10 Coal 30 Iron 30 Copper 15 Tin 15 Mithril 1 Topaz 1";
	
	$ItemType[SplintMailBody] = "armor";
	$ItemSubType[SplintMailBody] = $BodyAccessoryType;
	$ItemDesc[SplintMailBody] = "Splint Mail Body";
	$ItemSize[SplintMailBody] = "medium";
	$ItemBaseWeight[SplintMailBody] = 3.0;
	$ItemBaseSpecialVar[SplintMailBody] = "7 4r45 24 10r35 21 6r14";
	$PrefixExclusions[SplintMailBody] = ",";
	$SuffixExclusions[SplintMailBody] = ",";
	$DataBlock[SplintMailBody] = "armor1";
	$shop::BuyPrice[SplintMailBody] = 50000;
	$itemLongDesc[SplintMailBody] = "";
	$item::smith[BandedMailArmor,0] = "Rod 5 Diere 10 Copper 20 Tin 20 Iron 20 Coal 5"; 
	$item::smith[BandedMailArmor,1] = "Rod 2 Diere 4 Copper 8 Tin 8 Iron 10 Coal 2"; 
	$item::smith[BandedMailArmor,2] = "Rod 3 Diere 5 Copper 10 Tin 10 Iron 5 Coal 1"; 
	$item::smith[BandedMailArmor,3] = "Rod 2 Diere 8 Copper 15 Tin 15 Iron 15 Coal 10"; 
	$item::smith[BandedMailArmor,4] = "Diere 12 Copper 20 Tin 20 Iron 20 Coal 15"; 
	$item::smith[BandedMailArmor,5] = "Diere 16 Copper 25 Tin 25 Clay 5 Iron 25 Coal 20 Mithril 3 Turquoise 1";
	
	$ItemType[BronzePlate] = "armor";
	$ItemSubType[BronzePlate] = $BodyAccessoryType;
	$ItemDesc[BronzePlate] = "Bronze Plate";
	$ItemSize[BronzePlate] = "medium";
	$ItemBaseWeight[BronzePlate] = 3.0;
	$ItemBaseSpecialVar[BronzePlate] = "7 10r80";
	$PrefixExclusions[BronzePlate] = ",";
	$SuffixExclusions[BronzePlate] = ",";
	$DataBlock[BronzePlate] = "armor1";
	$shop::BuyPrice[BronzePlate] = 55000;
	$itemLongDesc[BronzePlate] = "";
	$item::smith[BandedMailArmor,0] = "Mithril 1 Diere 10 copper 40 Tin 40 Iron 10 Coal 5"; 
	$item::smith[BandedMailArmor,1] = "Diere 2 Copper 10 Tin 10 Iron 2 Coal 2"; 
	$item::smith[BandedMailArmor,2] = "Diere 4 Copper 15 Tin 15 Iron 5 Coal 3"; 
	$item::smith[BandedMailArmor,3] = "Diere 6 Copper 20 Tin 20 Iron 15 Coal 3"; 
	$item::smith[BandedMailArmor,4] = "Diere 12 Iron 20 Tin 40 Copper 40"; 
	$item::smith[BandedMailArmor,5] = "Diere 20 iron 40 Copper 50 Tin 50 Clay 25 Coal 10 Mithril 5";
	
	$ItemType[HalfPlate] = "armor";
	$ItemSubType[HalfPlate] = $BodyAccessoryType;
	$ItemDesc[HalfPlate] = "Half Plate Body";
	$ItemSize[HalfPlate] = "medium";
	$ItemBaseWeight[HalfPlate] = 3.0;
	$ItemBaseSpecialVar[HalfPlate] = "7 12r90";
	$PrefixExclusions[HalfPlate] = ",";
	$SuffixExclusions[HalfPlate] = ",";
	$DataBlock[HalfPlate] = "armor1";
	$shop::BuyPrice[HalfPlate] = 90000;
	$itemLongDesc[HalfPlate] = "";
	$item::smith[HalfPlate,0] = "Mithril 5 Coal 20 Iron 10 Diere 10"; 
	$item::smith[HalfPlate,1] = "Mithril 2 Coal 5 Iron 2 Diere 2"; 
	$item::smith[HalfPlate,2] = "Mithril 4 Coal 10 Iron 4 Diere 4"; 
	$item::smith[HalfPlate,3] = "Mithril 6 Coal 15 Iron 10 Diere 7"; 
	$item::smith[HalfPlate,4] = "Mithril 8 Coal 25 Iron 15 Diere 10"; 
	$item::smith[HalfPlate,5] = "Mithril 12 Coal 30 Iron 20 Diere 15 Sapphire 1";

	$ItemType[FieldPlate] = "armor";
	$ItemSubType[FieldPlate] = $BodyAccessoryType;
	$ItemDesc[FieldPlate] = "Field Plate Body";
	$ItemSize[FieldPlate] = "medium";
	$ItemBaseWeight[FieldPlate] = 3.0;
	$ItemBaseSpecialVar[FieldPlate] = "7 10r80";
	$PrefixExclusions[FieldPlate] = ",";
	$SuffixExclusions[FieldPlate] = ",";
	$DataBlock[FieldPlate] = "armor1";
	$shop::BuyPrice[FieldPlate] = 110000;
	$itemLongDesc[FieldPlate] = "";
	$item::smith[FieldPlate,0] = "Mithril 10 Gold 2 Diere 10 Coal 10 Iron 10"; 
	$item::smith[FieldPlate,1] = "Mithril 5 Gold 5 Diere 5 Coal 2 Iron 5"; 
	$item::smith[FieldPlate,2] = "Mithril 6 Gold 4 Diere 5 Coal 5 Iron 6"; 
	$item::smith[FieldPlate,3] = "Mithril 8 Gold 6 Silver 5 Diere 8 Coal 10 Iron 10"; 
	$item::smith[FieldPlate,4] = "Mithril 10 Gold 10 Silver 10 Diere 10 Coal 15 Iron 15"; 
	$item::smith[FieldPlate,5] = "Mithril 20 Gold 15 Silver 20 Diere 20 Coal 20 Iron 25 Emerald 1";
	
	$ItemType[DragonMail] = "armor";
	$ItemSubType[DragonMail] = $BodyAccessoryType;
	$ItemDesc[DragonMail] = "Dragon Mail";
	$ItemSize[DragonMail] = "medium";
	$ItemBaseWeight[DragonMail] = 3.0;
	$ItemBaseSpecialVar[DragonMail] = "7 10r100 20 100r400";
	$PrefixExclusions[DragonMail] = ",";
	$SuffixExclusions[DragonMail] = ",";
	$DataBlock[DragonMail] = "armor1";
	$shop::BuyPrice[DragonMail] = 250000;
	$itemLongDesc[DragonMail] = "";
	$item::smith[DragonMail,0] = "DragonScale 50 Adamite 5 Mithril 10 Gold 10 Silver 10 Sapphire 1"; 
	$item::smith[DragonMail,1] = "DragonScale 15 Adamite 2 Mithril 2 Gold 2 Silver 2"; 
	$item::smith[DragonMail,2] = "DragonScale 20 Adamite 2 Mithril 3 Gold 4 Silver 5"; 
	$item::smith[DragonMail,3] = "DragonScale 45 Adamite 6 Mithril 15 Gold 10 Silver 15 Jade 5"; 
	$item::smith[DragonMail,4] = "DragonScale 60 Adamite 8 Mithril 25 Gold 25 Silver 25 Jade 10"; 
	$item::smith[DragonMail,5] = "DragonScale 100 Adamite 13 Mithril 35 Gold 30 Silver 30 Jade 10 Ruby 5 Sapphire 5";
	
	$ItemType[FullPlate] = "armor";
	$ItemSubType[FullPlate] = $BodyAccessoryType;
	$ItemDesc[FullPlate] = "Full Plate Body";
	$ItemSize[FullPlate] = "medium";
	$ItemBaseWeight[FullPlate] = 3.0;
	$ItemBaseSpecialVar[FullPlate] = "7 30r200";
	$PrefixExclusions[FullPlate] = ",";
	$SuffixExclusions[FullPlate] = ",";
	$DataBlock[FullPlate] = "armor1";
	$shop::BuyPrice[FullPlate] = 150000;
	$itemLongDesc[FullPlate] = "";
	$item::smith[FullPlate,0] = "Adamite 10 Mithril 30 Gold 10 Silver 5 Ruby 1"; 
	$item::smith[FullPlate,1] = "Adamite 5 Mithril 15 Gold 5 Silver 2"; 
	$item::smith[FullPlate,2] = "Adamite 4 Mithril 10 Gold 15 Silver 5"; 
	$item::smith[FullPlate,3] = "Adamite 7 Mithril 35 Gold 25 Silver 7 Ruby 1"; 
	$item::smith[FullPlate,4] = "Adamite 10 Mithril 35 Gold 35 Silver 15 Ruby 2"; 
	$item::smith[FullPlate,5] = "Adamite 20 Mithril 40 Gold 40 Silver 20 Ruby 5 Keldrinite 1";
	
	$ItemType[KeldrinitePlate] = "armor";//obtained by smithing only!
	$ItemSubType[KeldrinitePlate] = $BodyAccessoryType;
	$ItemDesc[KeldrinitePlate] = "Keldrinite Plate Body";
	$ItemSize[KeldrinitePlate] = "medium";
	$ItemBaseWeight[KeldrinitePlate] = 3.0;
	$ItemBaseSpecialVar[KeldrinitePlate] = "7 30r400 3 40r100";
	$PrefixExclusions[KeldrinitePlate] = ",";
	$SuffixExclusions[KeldrinitePlate] = ",";
	$DataBlock[KeldrinitePlate] = "armor1";
	$shop::BuyPrice[KeldrinitePlate] = 500000;
	$itemLongDesc[KeldrinitePlate] = "";
	$item::smith[KeldrinitePlate,0] = "Keldrinite 10 Adamite 30 Mithril 10 Gold 5 Silver 2 Coal 50 Iron 20 Diamond 1 Ruby 3"; 
	$item::smith[KeldrinitePlate,1] = "Keldrinite 2 Adamite 5 Mithril 5 Gold 2 Silver 1 Coal 5 Iron 10 Ruby 2"; 
	$item::smith[KeldrinitePlate,2] = "Keldrinite 5 Adamite 15 Mithril 5 Gold 4 Silver 3 Coal 25 Ruby 1"; 
	$item::smith[KeldrinitePlate,3] = "Keldrinite 6 Adamite 25 Mithril 25 Ruby 2 Gold 12"; 
	$item::smith[KeldrinitePlate,4] = "Keldrinite 10 Adamite 35 Mithril 40 Ruby 4 Diamond 1 Gold 20 Silver 10"; 
	$item::smith[KeldrinitePlate,5] = "Keldrinite 15 Adamite 45 Mithril 55 Ruby 10 Diamond 5 Gold 45 Silver 25";

	$ItemType[BasicRobe] = "armor";
	$ItemSubType[BasicRobe] = $BodyAccessoryType;
	$ItemDesc[BasicRobe] = "Basic Robe";
	$ItemSize[BasicRobe] = "medium";
	$ItemBaseWeight[BasicRobe] = 3.0;
	$ItemBaseSpecialVar[BasicRobe] = "7 1r1 11 100";
	$PrefixExclusions[BasicRobe] = ",";
	$SuffixExclusions[BasicRobe] = ",";
	$DataBlock[BasicRobe] = "armor1";
	$shop::BuyPrice[BasicRobe] = 200;
	$item::smith[BasicRobe,0] = "GnollHide 5 Clay 20 Quartz 1"; 
	$item::smith[BasicRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	$item::smith[BasicRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	$item::smith[BasicRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3"; 
	$item::smith[BasicRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Quartz 1"; 
	$item::smith[BasicRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Opal 1";

	$ItemType[ApprenticeRobe] = "armor";
	$ItemSubType[ApprenticeRobe] = $BodyAccessoryType;
	$ItemDesc[ApprenticeRobe] = "Apprentice Robe";
	$ItemSize[ApprenticeRobe] = "medium";
	$ItemBaseWeight[ApprenticeRobe] = 3.0;
	$ItemBaseSpecialVar[ApprenticeRobe] = "7 1r2 11 200";
	$PrefixExclusions[ApprenticeRobe] = ",";
	$SuffixExclusions[ApprenticeRobe] = ",";
	$DataBlock[ApprenticeRobe] = "armor1";
	$shop::BuyPrice[ApprenticeRobe] = 2000;
	$item::smith[ApprenticeRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	$item::smith[ApprenticeRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	$item::smith[ApprenticeRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	$item::smith[ApprenticeRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	$item::smith[ApprenticeRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	$item::smith[ApprenticeRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[LightRobe] = "armor";
	$ItemSubType[LightRobe] = $BodyAccessoryType;
	$ItemDesc[LightRobe] = "Light Robe";
	$ItemSize[LightRobe] = "medium";
	$ItemBaseWeight[LightRobe] = 3.0;
	$ItemBaseSpecialVar[LightRobe] = "7 1r3 11 210 3 1r5";
	$PrefixExclusions[LightRobe] = ",";
	$SuffixExclusions[LightRobe] = ",";
	$DataBlock[LightRobe] = "armor1";
	$shop::BuyPrice[LightRobe] = 4000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[FineRobe] = "armor";
	$ItemSubType[FineRobe] = $BodyAccessoryType;
	$ItemDesc[FineRobe] = "Fine Robe";
	$ItemSize[FineRobe] = "medium";
	$ItemBaseWeight[FineRobe] = 3.0;
	$ItemBaseSpecialVar[FineRobe] = "7 1r4 11 230 3 1r10";
	$PrefixExclusions[FineRobe] = ",";
	$SuffixExclusions[FineRobe] = ",";
	$DataBlock[FineRobe] = "armor1";
	$shop::BuyPrice[FineRobe] = 8000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[BloodRobe] = "armor";
	$ItemSubType[BloodRobe] = $BodyAccessoryType;
	$ItemDesc[BloodRobe] = "Blood Robe";
	$ItemSize[BloodRobe] = "medium";
	$ItemBaseWeight[BloodRobe] = 3.0;
	$ItemBaseSpecialVar[BloodRobe] = "7 1r5 11 280 3 1r30";
	$PrefixExclusions[BloodRobe] = ",";
	$SuffixExclusions[BloodRobe] = ",";
	$DataBlock[BloodRobe] = "armor1";
	$shop::BuyPrice[BloodRobe] = 20000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[AdvisorRobe] = "armor";
	$ItemSubType[AdvisorRobe] = $BodyAccessoryType;
	$ItemDesc[AdvisorRobe] = "Advisor Robe";
	$ItemSize[AdvisorRobe] = "medium";
	$ItemBaseWeight[AdvisorRobe] = 3.0;
	$ItemBaseSpecialVar[AdvisorRobe] = "7 1r6 11 300 3 1r40";
	$PrefixExclusions[AdvisorRobe] = ",";
	$SuffixExclusions[AdvisorRobe] = ",";
	$DataBlock[AdvisorRobe] = "armor1";
	$shop::BuyPrice[AdvisorRobe] = 40000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[ElvenRobe] = "armor";
	$ItemSubType[ElvenRobe] = $BodyAccessoryType;
	$ItemDesc[ElvenRobe] = "Elven Robe";
	$ItemSize[ElvenRobe] = "medium";
	$ItemBaseWeight[ElvenRobe] = 3.0;
	$ItemBaseSpecialVar[ElvenRobe] = "7 1r8 11 300 3 1r45 20 100r200 21 100r200";
	$PrefixExclusions[ElvenRobe] = ",";
	$SuffixExclusions[ElvenRobe] = ",";
	$DataBlock[ElvenRobe] = "armor1";
	$shop::BuyPrice[ElvenRobe] = 80000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[PhensRobe] = "armor";
	$ItemSubType[PhensRobe] = $BodyAccessoryType;
	$ItemDesc[PhensRobe] = "Phens Robe";
	$ItemSize[PhensRobe] = "medium";
	$ItemBaseWeight[PhensRobe] = 3.0;
	$ItemBaseSpecialVar[PhensRobe] = "7 1r10 11 400 3 1r80";
	$PrefixExclusions[PhensRobe] = ",";
	$SuffixExclusions[PhensRobe] = ",";
	$DataBlock[PhensRobe] = "armor1";
	$shop::BuyPrice[PhensRobe] = 100000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	$ItemType[HolyRobe] = "armor";
	$ItemSubType[HolyRobe] = $BodyAccessoryType;
	$ItemDesc[HolyRobe] = "Holy Robe";
	$ItemSize[HolyRobe] = "medium";
	$ItemBaseWeight[HolyRobe] = 3.0;
	$ItemBaseSpecialVar[HolyRobe] = "7 1r12 11 500 3 1r80 23 200r300 24 200r300 25 200r300";
	$PrefixExclusions[HolyRobe] = ",";
	$SuffixExclusions[HolyRobe] = ",";
	$DataBlock[HolyRobe] = "armor1";
	$shop::BuyPrice[HolyRobe] = 140000;
	//$item::smith[LightRobe,0] = "GnollHide 5 Clay 20 Opal 1"; 
	//$item::smith[LightRobe,1] = "GnollHide 1 Clay 5 tin 1 "; 
	//$item::smith[LightRobe,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	//$item::smith[LightRobe,3] = "GnollHide 3 Clay 8 tin 3 copper 3 Quartz 1"; 
	//$item::smith[LightRobe,4] = "GnollHide 4 Clay 10 Tin 4 Copper 4 Iron 1 Opal 1"; 
	//$item::smith[LightRobe,5] = "GnollHide 8 Copper 5 Tin 5 Clay 15 Iron 2 Jade 1";
	
	//############################################################
	//boots
	$armorlist[$numArmor++] = "LeatherBoots";
	
	$ItemType[LeatherBoots] = "armor";
	$ItemSubType[LeatherBoots] = $BootsAccessoryType;
	$ItemDesc[LeatherBoots] = "Leather Boots";
	$ItemSize[LeatherBoots] = "medium";
	$ItemBaseWeight[LeatherBoots] = 3.0;
	$ItemBaseSpecialVar[LeatherBoots] = "7 0r1 12 10";
	$PrefixExclusions[LeatherBoots] = ",";
	$SuffixExclusions[LeatherBoots] = ",";
	$DataBlock[LeatherBoots] = "armor1";
	$shop::BuyPrice[LeatherBoots] = 5000;
	$item::smith[LeatherBoots,0] = "GnollHide 5 Clay 20 Copper 1 Tin 1 String 2"; 
	$item::smith[LeatherBoots,1] = "GnollHide 1 Clay 5 tin 1 String 1"; 
	$item::smith[LeatherBoots,2] = "GnollHide 2 Clay 5 Tin 4 Copper 4"; 
	$item::smith[LeatherBoots,3] = "GnollHide 5 Clay 8 tin 3 copper 3"; 
	$item::smith[LeatherBoots,4] = "GnollHide 7 Clay 10 Tin 4 Copper 4"; 
	$item::smith[LeatherBoots,5] = "GnollHide 9 Copper 5 Tin 5 Clay 15";
	
	//############################################################
	$numItems = 0;
	$ItemList[$numItems++] = "BluePotion";
	$ItemList[$numItems++] = "CrystalBluePotion";
	$ItemList[$numItems++] = "EnergyVial";
	$ItemList[$numItems++] = "CrystalEnergyVial";
	$ItemList[$numItems++] = "SmallRock";
	$ItemList[$numItems++] = "Quartz";
	$ItemList[$numItems++] = "Granite";
	$ItemList[$numItems++] = "Opal";
	$ItemList[$numItems++] = "Jade";
	$ItemList[$numItems++] = "Turquoise";
	$ItemList[$numItems++] = "Ruby";
	$ItemList[$numItems++] = "Topaz";
	$ItemList[$numItems++] = "Sapphire";
	$ItemList[$numItems++] = "Silver";
	$ItemList[$numItems++] = "Gold";
	$ItemList[$numItems++] = "Keldrinite";
	$ItemList[$numItems++] = "Clay";
	$ItemList[$numItems++] = "Copper";
	$ItemList[$numItems++] = "Tin";
	$ItemList[$numItems++] = "Iron";
	$ItemList[$numItems++] = "Coal";
	$ItemList[$numItems++] = "Mithril";
	$ItemList[$numItems++] = "Diere";
	$ItemList[$numItems++] = "Adamite";
	$ItemList[$numItems++] = "Fish";
	$itemList[$numItems++] = "BasicArrow";
	$itemList[$numItems++] = "GnollHide";
	$itemList[$numItems++] = "Rod";
	$itemList[$numItems++] = "String";
	$itemList[$numItems++] = "Emerald";
	$itemList[$numItems++] = "ShortQuarrel";
	$itemList[$numItems++] = "DragonScale";
		
	$ItemType[BluePotion] = "potion";
	$ItemSubType[BluePotion] = $BeltAccessoryType;
	$ItemDesc[BluePotion] = "Blue Potion";
	$ItemSize[BluePotion] = "tiny";
	$ItemBaseWeight[BluePotion] = 0.5;
	$ItemBaseSpecialVar[BluePotion] = "4 20";
	$PrefixExclusions[BluePotion] = ",";
	$SuffixExclusions[BluePotion] = ",";
	$DataBlock[BluePotion] = "BluePotion";
	$shop::BuyPrice[BluePotion] = 10;
	
	$ItemType[CrystalBluePotion] = "potion";
	$ItemSubType[CrystalBluePotion] = $BeltAccessoryType;
	$ItemDesc[CrystalBluePotion] = "Crystal Blue Potion";
	$ItemSize[CrystalBluePotion] = "small";
	$ItemBaseWeight[CrystalBluePotion] = 1.0;
	$ItemBaseSpecialVar[CrystalBluePotion] = "4 100";
	$PrefixExclusions[CrystalBluePotion] = ",";
	$SuffixExclusions[CrystalBluePotion] = ",";
	$DataBlock[CrystalBluePotion] = "CrystalBluePotion";
	$shop::BuyPrice[CrystalBluePotion] = 50;

	$ItemType[Fish] = "potion";
	$ItemSubType[Fish] = $BeltAccessoryType;
	$ItemDesc[Fish] = "Fish";
	$ItemSize[Fish] = "small";
	$ItemBaseWeight[Fish] = 1.0;
	$ItemBaseSpecialVar[Fish] = "4 30";
	$PrefixExclusions[Fish] = ",";
	$SuffixExclusions[Fish] = ",";
	$DataBlock[Fish] = "Fish";
	$shop::BuyPrice[Fish] = 20;
	
	$ItemType[EnergyVial] = "potion";
	$ItemSubType[EnergyVial] = $BeltAccessoryType;
	$ItemDesc[EnergyVial] = "Energy Vial";
	$ItemSize[EnergyVial] = "tiny";
	$ItemBaseWeight[EnergyVial] = 0.5;
	$ItemBaseSpecialVar[EnergyVial] = "5 20";
	$PrefixExclusions[EnergyVial] = ",";
	$SuffixExclusions[EnergyVial] = ",";
	$DataBlock[EnergyVial] = "EnergyVial";
	$shop::BuyPrice[EnergyVial] = 50;
	
	$ItemType[CrystalEnergyVial] = "potion";
	$ItemSubType[CrystalEnergyVial] = $BeltAccessoryType;
	$ItemDesc[CrystalEnergyVial] = "Crystal Energy Vial";
	$ItemSize[CrystalEnergyVial] = "small";
	$ItemBaseWeight[CrystalEnergyVial] = 1.0;
	$ItemBaseSpecialVar[CrystalEnergyVial] = "5 100";
	$PrefixExclusions[CrystalEnergyVial] = ",";
	$SuffixExclusions[CrystalEnergyVial] = ",";
	$DataBlock[CrystalEnergyVial] = "CrystalEnergyVial";
	$shop::BuyPrice[CrystalEnergyVial] = 250;
	
	$ItemType[LootBag] = "LootBag";
	$ItemSubType[LootBag] = $BeltAccessoryType;
	$ItemDesc[LootBag] = "LootBag";
	$ItemSize[LootBag] = "small";
	$ItemBaseWeight[LootBag] = 1.0;
	$ItemBaseSpecialVar[LootBag] = "";
	$PrefixExclusions[LootBag] = ",";
	$SuffixExclusions[LootBag] = ",";
	$DataBlock[LootBag] = "LootBag";
	$shop::BuyPrice[LootBag] = 0;
	//##################################Arrows############################
	//arrow list
	$numarrows = 0;
	$ArrowList[$numarrows++] = "BasicArrow";
	$ArrowList[$numArrows++] = "SmallRock";
	$ArrowList[$numArrows++] = "ShortQuarrel";
	
	$ItemType[BasicArrow] = "Item";
	$ItemSubType[BasicArrow] = $BeltAccessoryType;
	$ItemDesc[BasicArrow] = "Basic Arrow";
	$ItemSize[BasicArrow] = "tiny";
	$ItemBaseWeight[BasicArrow] = 0.01;
	$ItemBaseSpecialVar[BasicArrow] = "6 1r2";
	$PrefixExclusions[BasicArrow] = ",";
	$SuffixExclusions[BasicArrow] = ",";
	$itemIsArrow[BasicArrow] = true;
	$DataBlock[BasicArrow] = "basicarrow";
	$shop::BuyPrice[BasicArrow] = 5;
	
	$ItemType[ShortQuarrel] = "Item";
	$ItemSubType[ShortQuarrel] = $BeltAccessoryType;
	$ItemDesc[ShortQuarrel] = "Short Quarrel";
	$ItemSize[ShortQuarrel] = "tiny";
	$ItemBaseWeight[ShortQuarrel] = 0.01;
	$ItemBaseSpecialVar[ShortQuarrel] = "6 1r2";
	$PrefixExclusions[ShortQuarrel] = ",";
	$SuffixExclusions[ShortQuarrel] = ",";
	$itemIsArrow[ShortQuarrel] = true;
	$DataBlock[ShortQuarrel] = "basicarrow";
	$shop::BuyPrice[ShortQuarrel] = 10;

	//##################################Misc Items########################
	$ItemType[GnollHide] = "item";
	$ItemSubType[GnollHide] = $BeltAccessoryType;
	$ItemDesc[GnollHide] = "Gnoll Hide";
	$ItemSize[GnollHide] = "small";
	$ItemBaseWeight[GnollHide] = 1.0;
	$ItemBaseSpecialVar[GnollHide] = "";
	$PrefixExclusions[GnollHide] = ",";
	$SuffixExclusions[GnollHide] = ",";
	$DataBlock[GnollHide] = "gnollhide";
	$shop::BuyPrice[GnollHide] = 150;
	
	$ItemType[Rod] = "item";
	$ItemSubType[Rod] = $BeltAccessoryType;
	$ItemDesc[Rod] = "Rod";
	$ItemSize[Rod] = "small";
	$ItemBaseWeight[Rod] = 1.0;
	$ItemBaseSpecialVar[Rod] = "";
	$PrefixExclusions[Rod] = ",";
	$SuffixExclusions[Rod] = ",";
	$DataBlock[Rod] = "Quarterstaff";
	$shop::BuyPrice[Rod] = 150;
	
	$ItemType[String] = "item";
	$ItemSubType[String] = $BeltAccessoryType;
	$ItemDesc[String] = "String";
	$ItemSize[String] = "small";
	$ItemBaseWeight[String] = 1.0;
	$ItemBaseSpecialVar[String] = "";
	$PrefixExclusions[String] = ",";
	$SuffixExclusions[String] = ",";
	$DataBlock[String] = "LootBag";
	$shop::BuyPrice[String] = 10;	
	
	$ItemType[DragonScale] = "item";
	$ItemSubType[DragonScale] = $BeltAccessoryType;
	$ItemDesc[DragonScale] = "Dragon Scale";
	$ItemSize[DragonScale] = "small";
	$ItemBaseWeight[DragonScale] = 1.0;
	$ItemBaseSpecialVar[DragonScale] = "";
	$PrefixExclusions[DragonScale] = ",";
	$SuffixExclusions[DragonScale] = ",";
	$DataBlock[DragonScale] = "armor1";
	$shop::BuyPrice[DragonScale] = 1000;	
	//##################################Mining Items######################
	//Mining rocks in order of rarity, least to most
	$numstone = 0;
	$MiningList[$numstone++] = "SmallRock";
	$MiningList[$numstone++] = "Clay";
	$MiningList[$numstone++] = "Granite";
	$MiningList[$numstone++] = "Copper";
	$MiningList[$numstone++] = "Tin";
	$MiningList[$numstone++] = "Iron";
	$MiningList[$numstone++] = "Coal";
	$MiningList[$numstone++] = "Diere";
	$MiningList[$numstone++] = "Silver";
	$MiningList[$numstone++] = "Gold";
	$MiningList[$numstone++] = "Mithril";
	$MiningList[$numstone++] = "Adamite";
	$MiningList[$numstone++] = "Keldrinite";
	
	$ItemType[SmallRock] = "item";
	$ItemSubType[SmallRock] = $BeltAccessoryType;
	$ItemDesc[SmallRock] = "Small Rock";
	$ItemSize[SmallRock] = "small";
	$ItemBaseWeight[SmallRock] = 0.1;
	$ItemBaseSpecialVar[SmallRock] = "6 1r1";
	$PrefixExclusions[SmallRock] = ",";
	$SuffixExclusions[SmallRock] = ",";
	$DataBlock[SmallRock] = "SmallRock";
	$shop::BuyPrice[SmallRock] = 10;
	$shop::SellPrice[SmallRock] = 1;
	$mine::skillreq[SmallRock] = 0;
	
	$ItemType[Clay] = "item";
	$ItemSubType[Clay] = $BeltAccessoryType;
	$ItemDesc[Clay] = "Clay";
	$ItemSize[Clay] = "small";
	$ItemBaseWeight[Clay] = 0.5;
	$ItemBaseSpecialVar[Clay] = "";
	$PrefixExclusions[Clay] = ",";
	$SuffixExclusions[Clay] = ",";
	$DataBlock[Clay] = "OreClay";
	$shop::BuyPrice[Clay] = 20;//sells for 2 coins
	$shop::SellPrice[Clay] = 2;
	$mine::skillreq[Clay] = 10;


	$ItemType[Granite] = "item";
	$ItemSubType[Granite] = $BeltAccessoryType;
	$ItemDesc[Granite] = "Granite";
	$ItemSize[Granite] = "small";
	$ItemBaseWeight[Granite] = 1.0;
	$ItemBaseSpecialVar[Granite] = "";
	$PrefixExclusions[Granite] = ",";
	$SuffixExclusions[Granite] = ",";
	$DataBlock[Granite] = "OreGranite";
	$shop::BuyPrice[Granite] = 100;
	$shop::SellPrice[Granite] = 10;
	$mine::skillreq[Granite] = 20;
	
	$ItemType[Copper] = "item";
	$ItemSubType[Copper] = $BeltAccessoryType;
	$ItemDesc[Copper] = "Copper";
	$ItemSize[Copper] = "small";
	$ItemBaseWeight[Copper] = 1.0;
	$ItemBaseSpecialVar[Copper] = "";
	$PrefixExclusions[Copper] = ",";
	$SuffixExclusions[Copper] = ",";
	$DataBlock[Copper] = "OreCopper";
	$shop::BuyPrice[Copper] = 400;
	$shop::SellPrice[Copper] = 40;
	$mine::skillreq[Copper] = 40;
	
	$ItemType[Tin] = "item";
	$ItemSubType[Tin] = $BeltAccessoryType;
	$ItemDesc[Tin] = "Tin";
	$ItemSize[Tin] = "small";
	$ItemBaseWeight[Tin] = 1.0;
	$ItemBaseSpecialVar[Tin] = "";
	$PrefixExclusions[Tin] = ",";
	$SuffixExclusions[Tin] = ",";
	$DataBlock[Tin] = "OreTin";
	$shop::BuyPrice[Tin] = $shop::BuyPrice[Copper];
	$shop::SellPrice[Tin] = $shop::SellPrice[Copper];
	$mine::skillreq[Tin] = 40;

	$ItemType[Iron] = "item";
	$ItemSubType[Iron] = $BeltAccessoryType;
	$ItemDesc[Iron] = "Iron";
	$ItemSize[Iron] = "small";
	$ItemBaseWeight[Iron] = 1.0;
	$ItemBaseSpecialVar[Iron] = "";
	$PrefixExclusions[Iron] = ",";
	$SuffixExclusions[Iron] = ",";
	$DataBlock[Iron] = "OreIron";
	$shop::BuyPrice[Iron] = 800;
	$shop::SellPrice[Iron] = 80;
	$mine::skillreq[Iron] = 80;

	$ItemType[Coal] = "item";
	$ItemSubType[Coal] = $BeltAccessoryType;
	$ItemDesc[Coal] = "Coal";
	$ItemSize[Coal] = "small";
	$ItemBaseWeight[Coal] = 1.0;
	$ItemBaseSpecialVar[Coal] = "";
	$PrefixExclusions[Coal] = ",";
	$SuffixExclusions[Coal] = ",";
	$DataBlock[Coal] = "OreCoal";
	$shop::BuyPrice[Coal] = 1000;
	$shop::SellPrice[Coal] = 100;
	$mine::skillreq[Coal] = 160;

	$ItemType[Diere] = "item";
	$ItemSubType[Diere] = $BeltAccessoryType;
	$ItemDesc[Diere] = "Diere";
	$ItemSize[Diere] = "small";
	$ItemBaseWeight[Diere] = 1.0;
	$ItemBaseSpecialVar[Diere] = "";
	$PrefixExclusions[Diere] = ",";
	$SuffixExclusions[Diere] = ",";
	$DataBlock[Diere] = "OreDiere";
	$shop::BuyPrice[Diere] = 1200;
	$shop::SellPrice[Diere] = 120;
	$mine::skillreq[Diere] = 250;

	$ItemType[Silver] = "item";
	$ItemSubType[Silver] = $BeltAccessoryType;
	$ItemDesc[Silver] = "Silver";
	$ItemSize[Silver] = "small";
	$ItemBaseWeight[Silver] = 1.0;
	$ItemBaseSpecialVar[Silver] = "";
	$PrefixExclusions[Silver] = ",";
	$SuffixExclusions[Silver] = ",";
	$DataBlock[Silver] = "OreSilver";
	$shop::BuyPrice[Silver] = 1500;
	$shop::SellPrice[Silver] = 150;
	$mine::skillreq[Silver] = 350;

	$ItemType[Gold] = "item";
	$ItemSubType[Gold] = $BeltAccessoryType;
	$ItemDesc[Gold] = "Gold";
	$ItemSize[Gold] = "small";
	$ItemBaseWeight[Gold] = 1.0;
	$ItemBaseSpecialVar[Gold] = "";
	$PrefixExclusions[Gold] = ",";
	$SuffixExclusions[Gold] = ",";
	$DataBlock[Gold] = "OreGold";
	$shop::BuyPrice[Gold] = 3000;
	$shop::SellPrice[Gold] = 300;
	$mine::skillreq[Gold] = 500;	

	$ItemType[Mithril] = "item";
	$ItemSubType[Mithril] = $BeltAccessoryType;
	$ItemDesc[Mithril] = "Mithril";
	$ItemSize[Mithril] = "small";
	$ItemBaseWeight[Mithril] = 1.0;
	$ItemBaseSpecialVar[Mithril] = "";
	$PrefixExclusions[Mithril] = ",";
	$SuffixExclusions[Mithril] = ",";
	$DataBlock[Mithril] = "OreMithril";
	$shop::BuyPrice[Mithril] = 5000;
	$shop::SellPrice[Mithril] = 500;
	$mine::skillreq[Mithril] = 640;

	$ItemType[Adamite] = "item";
	$ItemSubType[Adamite] = $BeltAccessoryType;
	$ItemDesc[Adamite] = "Adamite";
	$ItemSize[Adamite] = "small";
	$ItemBaseWeight[Adamite] = 1.0;
	$ItemBaseSpecialVar[Adamite] = "";
	$PrefixExclusions[Adamite] = ",";
	$SuffixExclusions[Adamite] = ",";
	$DataBlock[Adamite] = "OreAdamite";
	$shop::BuyPrice[Adamite] = 8000;
	$shop::SellPrice[Adamite] = 800;
	$mine::skillreq[Adamite] = 790;

	$ItemType[Keldrinite] = "item";
	$ItemSubType[Keldrinite] = $BeltAccessoryType;
	$ItemDesc[Keldrinite] = "Keldrinite";
	$ItemSize[Keldrinite] = "small";
	$ItemBaseWeight[Keldrinite] = 1.0;
	$ItemBaseSpecialVar[Keldrinite] = "";
	$PrefixExclusions[Keldrinite] = ",";
	$SuffixExclusions[Keldrinite] = ",";
	$DataBlock[Keldrinite] = "OreKeldrinite";
	$shop::BuyPrice[Keldrinite] = 100000;
	$shop::SellPrice[Keldrinite] = 10000;
	$mine::skillreq[Keldrinite] = 950;

	//#################################Gems##########################
	//Gems are rarely obtained when mining and quite profitable
	$numgem =0;
	$GemList[$numgem++] = "Quartz";
	$GemList[$numgem++] = "Opal";
	$GemList[$numgem++] = "Jade";
	$GemList[$numgem++] = "Turquoise";
	$GemList[$numgem++] = "Topaz";
	$GemList[$numgem++] = "Sapphire";
	$GemList[$numgem++] = "Emerald";
	$GemList[$numgem++] = "Ruby";
	$GemList[$numgem++] = "Diamond";


	$ItemType[Quartz] = "item";
	$ItemSubType[Quartz] = $BeltAccessoryType;
	$ItemDesc[Quartz] = "Quartz";
	$ItemSize[Quartz] = "small";
	$ItemBaseWeight[Quartz] = 1.0;
	$ItemBaseSpecialVar[Quartz] = "";
	$PrefixExclusions[Quartz] = ",";
	$SuffixExclusions[Quartz] = ",";
	$DataBlock[Quartz] = "GemQuartz";
	$shop::BuyPrice[Quartz] = 1000;
	$shop::SellPrice[Quartz] = 100;
	$mine::skillreq[Quartz] = 1;

	$ItemType[Opal] = "item";
	$ItemSubType[Opal] = $BeltAccessoryType;
	$ItemDesc[Opal] = "Opal";
	$ItemSize[Opal] = "small";
	$ItemBaseWeight[Opal] = 1.0;
	$ItemBaseSpecialVar[Opal] = "";
	$PrefixExclusions[Opal] = ",";
	$SuffixExclusions[Opal] = ",";
	$DataBlock[Opal] = "GemOpal";
	$shop::BuyPrice[Opal] = 4000;
	$shop::SellPrice[Opal] = 400;
	$mine::skillreq[Opal] = 35;

	$ItemType[Jade] = "item";
	$ItemSubType[Jade] = $BeltAccessoryType;
	$ItemDesc[Jade] = "Jade";
	$ItemSize[Jade] = "small";
	$ItemBaseWeight[Jade] = 1.0;
	$ItemBaseSpecialVar[Jade] = "";
	$PrefixExclusions[Jade] = ",";
	$SuffixExclusions[Jade] = ",";
	$DataBlock[Jade] = "GemJade";
	$shop::BuyPrice[Jade] = 5000;
	$shop::SellPrice[Jade] = 500;
	$mine::skillreq[Jade] = 75;

	$ItemType[Turquoise] = "item";
	$ItemSubType[Turquoise] = $BeltAccessoryType;
	$ItemDesc[Turquoise] = "Turquoise";
	$ItemSize[Turquoise] = "small";
	$ItemBaseWeight[Turquoise] = 1.0;
	$ItemBaseSpecialVar[Turquoise] = "";
	$PrefixExclusions[Turquoise] = ",";
	$SuffixExclusions[Turquoise] = ",";
	$DataBlock[Turquoise] = "GemTurquoise";
	$shop::BuyPrice[Turquoise] = 10000;
	$shop::SellPrice[Turquoise] = 1000;
	$mine::skillreq[Turquoise] = 125;

	$ItemType[Topaz] = "item";
	$ItemSubType[Topaz] = $BeltAccessoryType;
	$ItemDesc[Topaz] = "Topaz";
	$ItemSize[Topaz] = "small";
	$ItemBaseWeight[Topaz] = 1.0;
	$ItemBaseSpecialVar[Topaz] = "";
	$PrefixExclusions[Topaz] = ",";
	$SuffixExclusions[Topaz] = ",";
	$DataBlock[Topaz] = "GemTopaz";
	$shop::BuyPrice[Topaz] = 50000;
	$shop::SellPrice[Topaz] = 5000;
	$mine::skillreq[Topaz] = 500;

	$ItemType[Sapphire] = "item";
	$ItemSubType[Sapphire] = $BeltAccessoryType;
	$ItemDesc[Sapphire] = "Sapphire";
	$ItemSize[Sapphire] = "small";
	$ItemBaseWeight[Sapphire] = 1.0;
	$ItemBaseSpecialVar[Sapphire] = "";
	$PrefixExclusions[Sapphire] = ",";
	$SuffixExclusions[Sapphire] = ",";
	$DataBlock[Sapphire] = "GemSapphire";
	$shop::BuyPrice[Sapphire] = 80000;
	$shop::SellPrice[Sapphire] = 8000;
	$mine::skillreq[Sapphire] = 650;

	$ItemType[Emerald] = "item";
	$ItemSubType[Emerald] = $BeltAccessoryType;
	$ItemDesc[Emerald] = "Emerald";
	$ItemSize[Emerald] = "small";
	$ItemBaseWeight[Emerald] = 1.0;
	$ItemBaseSpecialVar[Emerald] = "";
	$PrefixExclusions[Emerald] = ",";
	$SuffixExclusions[Emerald] = ",";
	$DataBlock[Emerald] = "MineGem";
	$shop::BuyPrice[Emerald] = 100000;
	$shop::SellPrice[Emerald] = 10000;
	$mine::skillreq[Emerald] = 700;

	$ItemType[Ruby] = "item";
	$ItemSubType[Ruby] = $BeltAccessoryType;
	$ItemDesc[Ruby] = "Ruby";
	$ItemSize[Ruby] = "small";
	$ItemBaseWeight[Ruby] = 1.0;
	$ItemBaseSpecialVar[Ruby] = "";
	$PrefixExclusions[Ruby] = ",";
	$SuffixExclusions[Ruby] = ",";
	$DataBlock[Ruby] = "GemRuby";
	$shop::BuyPrice[Ruby] = 120000;
	$shop::SellPrice[Ruby] = 12000;
	$mine::skillreq[Ruby] = 900;
	
	$ItemType[Diamond] = "item";
	$ItemSubType[Diamond] = $BeltAccessoryType;
	$ItemDesc[Diamond] = "Diamond";
	$ItemSize[Diamond] = "small";
	$ItemBaseWeight[Diamond] = 1.0;
	$ItemBaseSpecialVar[Diamond] = "";
	$PrefixExclusions[Diamond] = ",";
	$SuffixExclusions[Diamond] = ",";
	$DataBlock[Diamond] = "MineGem";
	$shop::BuyPrice[Diamond] = 2000000;
	$shop::SellPrice[Diamond] = 200000;
	$mine::skillreq[Diamond] = 1000;

	Custom::Items();
}


//======== DATABLOCKS ==============================================================================================

function DefineItemDatas()
{
	//----------------------------------------
	// WarMaul
	//----------------------------------------
	datablock ShapeBaseImageData(WarMaulImage)
	{
		className = WeaponImage;
		shapeFile = "WarMaul.dts";
		item = WarMaul;
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	datablock ItemData(WarMaul)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "WarMaul.dts";
		image = WarMaulImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "WarMaul_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// QuarterStaff
	//----------------------------------------
	datablock ShapeBaseImageData(QuarterstaffImage)
	{
		className = WeaponImage;
		shapeFile = "Quarterstaff.dts";
		item = Quarterstaff;
		offset = "0 0 -0.6";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	datablock ItemData(Quarterstaff)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Quarterstaff.dts";
		image = QuarterstaffImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Quarterstaff_model";
		computeCRC = false;
		emap = true;
	};
	//############################
	//Spear
	//############################
	datablock ShapeBaseImageData(SpearImage)
	{
		className = WeaponImage;
		shapeFile = "Spear.dts";
		item = Spear;
		offset = "0 0 1.1";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	datablock ItemData(Spear)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Spear.dts";
		image = SpearImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Spear_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// BastardSword
	//----------------------------------------
	datablock ShapeBaseImageData(BastardSwordImage)
	{
		className = WeaponImage;
		shapeFile = "BastardSword.dts";
		item = BastardSword;
		//offset = "-0.02 -0.01 0.2";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";

	};
	datablock ItemData(BastardSword)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "BastardSword.dts";
		image = BastardSwordImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "BastardSword_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Katana
	//----------------------------------------
	datablock ShapeBaseImageData(KatanaImage)
	{
		className = WeaponImage;
		shapeFile = "katana.dts";
		item = Katana;
		//offset = "-0.02 -0.01 0.2";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";

	};
	datablock ItemData(Katana)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "katana.dts";
		image = KatanaImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Katana_model";
		computeCRC = false;
		emap = true;
	};

	//----------------------------------------
	// Knife
	//----------------------------------------
	datablock ShapeBaseImageData(KnifeImage)
	{
		className = WeaponImage;
		shapeFile = "Dagger.dts";
		item = Knife;
		 offset = "0 0 0.2";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Knife)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Dagger.dts";
		image = KnifeImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Dagger1_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Dagger
	//----------------------------------------
	datablock ShapeBaseImageData(DaggerImage)
	{
		className = WeaponImage;
		shapeFile = "Fina_Dagger.dts";
		item = Dagger;
		 offset = "0 0 0.0";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Dagger)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Fina_Dagger.dts";
		image = DaggerImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Dagger1_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Dagger
	//----------------------------------------
	datablock ShapeBaseImageData(KatarImage)
	{
		className = WeaponImage;
		shapeFile = "Katar.dts";
		item = Katar;
		 offset = "0 0 0.0";
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Katar)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Katar.dts";
		image = KatarImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Dagger1_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Great Claymore
	//----------------------------------------
	datablock ShapeBaseImageData(GreatClaymoreImage)
	{
		className = WeaponImage;
		shapeFile = "GreatClaymore.dts";
		item = GreatClaymore;
		  

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(GreatClaymore)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "GreatClaymore.dts";
		image = GreatClaymoreImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Claymore1_model";
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Pickaxe model
	//----------------------------------------	
	datablock ShapeBaseImageData(PickAxeImage)
	{
		className = WeaponImage;
		shapeFile = "PickAxe.dts";
		item = PickAxe;


		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(PickAxe)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "PickAxe.dts";
		image = PickAxeImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "PickAxe_model";

		computeCRC = false;
		emap = true;
	};

	//----------------------------------------
	// Bardiche model
	//----------------------------------------

	datablock ShapeBaseImageData(BardicheImage)
	{
		className = WeaponImage;
		shapeFile = "Bardiche.dts";
		item = Bardiche;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Bardiche)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Bardiche.dts";
		image = BardicheImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Bardiche_model";

		computeCRC = false;
		emap = true;
	};

	//----------------------------------------
	// Trident model
	//----------------------------------------

	datablock ShapeBaseImageData(TridentImage)
	{
		className = WeaponImage;
		shapeFile = "Trident.dts";
		item = Trident;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Trident)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "Trident.dts";
		image = TridentImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "Bardiche_model";

		computeCRC = false;
		emap = true;
	};



	//----------------------------------------
	// WarHammer model
	//----------------------------------------
	datablock ShapeBaseImageData(WarHammerImage)
	{
		className = WeaponImage;
		shapeFile = "WarHammer.dts";
		item = WarHammer;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	
	datablock ItemData(WarHammer)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "WarHammer.dts";
		image = WarHammerImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "WarHammer_model";
		
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Mace model
	//----------------------------------------
	datablock ShapeBaseImageData(MaceImage)
	{
		className = WeaponImage;
		shapeFile = "mace.dts";
		item = Mace;
		offset = "0 0 -0.05";
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	
	datablock ItemData(Mace)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "mace.dts";
		image = MaceImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "mace_model";
		
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// club model
	//----------------------------------------
	datablock ShapeBaseImageData(ClubImage)
	{
		className = WeaponImage;
		shapeFile = "club.dts";
		item = Club;
		offset = "0 0 -0.05";

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	
	datablock ItemData(club)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "club.dts";
		image = clubImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "club_model";
		
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// SpikedClub model
	//----------------------------------------
	datablock ShapeBaseImageData(SpikedClubImage)
	{
		className = WeaponImage;
		shapeFile = "spikedclub.dts";
		item = SpikedClub;
		offset = "0 0 -0.05";
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	
	datablock ItemData(SpikedClub)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "SpikedClub.dts";
		image = SpikedClubImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "SpikedClub_model";
		
		computeCRC = false;
		emap = true;
	};
	//----------------------------------------
	// Claymore model
	//----------------------------------------
	datablock ShapeBaseImageData(ClaymoreImage)
	{
		className = WeaponImage;
		shapeFile = "claymore.dts";
		item = Claymore;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	
	datablock ItemData(Claymore)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "claymore.dts";
		image = ClaymoreImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "claymore_model";
		computeCRC = false;
		emap = true;
	};
	
	//----------------------------------------
	// Gladius model
	//----------------------------------------

	datablock ShapeBaseImageData(GladiusImage)
	{
		className = WeaponImage;
		shapeFile = "gladius.dts";
		item = Gladius;
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Gladius)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "gladius.dts";
		image        = GladiusImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "gladius_model";

		computeCRC = false;
		emap = true;
	};
	
	//---------------------------------------------
	//broadsword model
	//---------------------------------------------
	
	datablock ShapeBaseImageData(BroadSwordImage)
	{
		className = WeaponImage;
		shapeFile = "BroadSword.dts";
		item = BroadSword;
		offset = "0 0 -0.1";
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(BroadSword)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "BroadSword.dts";
		image        = BroadSwordImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "broadsword_model";

		computeCRC = false;
		emap = true;
	};

	//---------------------------------------------
	//longsword model
	//---------------------------------------------
	
	datablock ShapeBaseImageData(LongswordImage)
	{
		className = WeaponImage;
		shapeFile = "Longsword.dts";
		item = Longsword;
		offset = "0 0 -0.1";
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(Longsword)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "longsword.dts";
		image        = LongswordImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "longsword_model";

		computeCRC = false;
		emap = true;
	};	
	//----------------
	//ICE Broadsword
	//----------------
	datablock ParticleData(IceSpikeParticle6)
	{
	    dragCoefficient = 1;
	    gravityCoefficient = 0;
	    windCoefficient = 0;
	    inheritedVelFactor = 0.5;
	    constantAcceleration = 0;
	    lifetimeMS = 500;
	    lifetimeVarianceMS = 560;
	    useInvAlpha = 0;
	    spinRandomMin = -51.6129;
	    spinRandomMax = 205.645;
	    textureName = "special/Ice2.png";
	    times[0] = 0;
	    times[1] = 0.177419;
	    times[2] = 1;
	    colors[0] = "0.149606 0.360000 0.560000 0.000000";
	    colors[1] = "0.236220 0.360000 0.568000 0.588710";
	    colors[2] = "0.157480 0.360000 1.000000 0.000000";
	    sizes[0] = 0.2;
	    sizes[1] = 0.2;
	    sizes[2] = 0.2;
	};

	datablock ParticleEmitterData(IceSpikeEmitter6)
	{
	    ejectionPeriodMS = 5;
	    periodVarianceMS = 1;
	    ejectionVelocity = 2;
	    velocityVariance = 1;
	    ejectionOffset =   0;
	    
	    lifetimeMS = 0;
	    thetaMin = 0;
	    thetaMax = 360;
	    phiReferenceVel = 0;
    	phiVariance = 360;
	    overrideAdvances = 0;
	    orientParticles= 0;
	    orientOnVelocity = 0;
	    particles = "IceSpikeParticle6";
	};
	
	datablock ShapeBaseImageData(IceBroadSwordImage)
	{
		className = WeaponImage;
		shapeFile = "BroadSword.dts";
		item = IceBroadSword;
		offset = "0 0 -0.1";
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
		delayEmitter = "IceSpikeEmitter6";
		//baseEmitterNode = Muzzlepoint1;
		
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawLongSword;
	
		stateName[1] = "ActivateReady";
		//stateEmitter[1] = "IceSpikeEmitter6";
		//stateEmitterNode[1] = Muzzlepoint1;
		//stateEmitterTime[1] = 10000000;
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateEmitter[2] = "IceSpikeEmitter6";
		stateEmitterNode[2] = Muzzlepoint1;
		stateEmitterTime[2] = 500;
		stateTimeoutValue[2] = 0.5;
		stateTransitionOnTimeout[2] = "Ready2";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Ready2";
		stateEmitter[3] = "IceSpikeEmitter6";
		stateEmitterNode[3] = Muzzlepoint1;
		stateEmitterTime[3] = 500;
		stateTimeoutValue[3] = 0.5;
		stateTransitionOnTimeout[3] = "Ready";
		stateTransitionOnTriggerDown[3] = "Fire";
	
		stateName[4] = "Fire";
		stateTransitionOnTimeout[4] = "Ready";
		stateTimeoutValue[4] = 0.1;
		stateFire[4] = true;
		stateEmitter[4] = "IceSpikeEmitter6";
		stateEmitterTime[4] = 100;
		stateEmitterNode[4] = Muzzlepoint1;
		stateRecoil[4] = NoRecoil;
		stateAllowImageChange[4] = false;
		stateSequence[4] = "Fire";
		stateScript[4] = "onFire";
	};

	datablock ItemData(IceBroadSword)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "BroadSword.dts";
		image        = IceBroadSwordImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "broadsword_model";

		computeCRC = false;
		emap = true;
	};
	
	//------------------------------------------------
	//Fire Broadsword
	//------------------------------------------------
		datablock ParticleData(MineLampParticle2)
		{
		    dragCoefficient = 0;
		    gravityCoefficient = 0;
		    windCoefficient = 0;
		    inheritedVelFactor = 0.2;
		    constantAcceleration = -2;
		    lifetimeMS = 10000;
		    lifetimeVarianceMS = 400;
		    useInvAlpha = 0;
		    spinRandomMin = -79.0323;
		    spinRandomMax = 175.403;
		    textureName = "special/cloudflash2.png";
		    times[0] = 0;
		    times[1] = 0.25;
		    times[2] = 1;
		    colors[0] = "1.000000 0.624000 0.000000 0.250000";
		    colors[1] = "1.000000 0.632000 0.000000 0.169355";
		    colors[2] = "1.000000 1.000000 1.000000 0.411290";
		    sizes[0] = 0.169355;
		    sizes[1] = 0.33871;
		    sizes[2] = 0;
		};

		datablock ParticleEmitterData(FireEmitter6)
		{
		    ejectionPeriodMS = 5;
		    periodVarianceMS = 1;
		    ejectionVelocity = 10;
		    velocityVariance = 5;
		    ejectionOffset =   0;

		    lifetimeMS = 0;
		    thetaMin = 0;
		    thetaMax = 360;
		    phiReferenceVel = 0;
		    phiVariance = 360;
		    overrideAdvances = 0;
		    orientParticles= 0;
		    orientOnVelocity = 0;
		    particles = "MineLampParticle2";
		};

		datablock ShapeBaseImageData(FireBroadSwordImage)
		{
			className = WeaponImage;
			shapeFile = "BroadSword.dts";
			item = FireBroadSword;
			offset = "0 0 -0.1";
			//extension = $ItemBaseRange[Dagger];
	
			//usesEnergy = true;
	
			//projectile = EnergyBolt;
			//projectileType = EnergyProjectile;
			//fireEnergy = 4;
			//minEnergy = 4;
			delayEmitter = "IceSpikeEmitter6";
			//baseEmitterNode = Muzzlepoint1;
			
			stateName[0] = "Activate";
			stateTransitionOnTimeout[0] = "ActivateReady";
			stateTimeoutValue[0] = 0.5;
			stateSequence[0] = "Activate";
			stateSound[0] = DrawLongSword;
		
			stateName[1] = "ActivateReady";
			//stateEmitter[1] = "IceSpikeEmitter6";
			//stateEmitterNode[1] = Muzzlepoint1;
			//stateEmitterTime[1] = 10000000;
			stateTransitionOnLoaded[1] = "Ready";
		
			stateName[2] = "Ready";
			stateEmitter[2] = "FireEmitter6";
			stateEmitterNode[2] = Muzzlepoint1;
			stateEmitterTime[2] = 5000000;
			//stateTimeoutValue[2] = 5;
			//stateTransitionOnTimeout[2] = "Ready";
			stateTransitionOnTriggerDown[2] = "Fire";
		
			stateName[3] = "Fire";
			stateTransitionOnTimeout[3] = "Ready";
			stateTimeoutValue[3] = 0.1;
			stateFire[3] = true;
			//stateEmitter[3] = "IceSpikeEmitter6";
			//stateEmitterTime[3] = 500;
			//stateEmitterNode[3] = Muzzlepoint1;
			stateRecoil[3] = NoRecoil;
			stateAllowImageChange[3] = false;
			stateSequence[3] = "Fire";
			stateScript[3] = "onFire";
		};
	
		datablock ItemData(FireBroadSword)
		{
			className    = Weapon;
			catagory     = "Spawn Items";
			shapeFile    = "BroadSword.dts";
			image        = FireBroadSwordImage;
			mass         = 1;
			elasticity   = 0.2;
			friction     = 0.6;
			pickupRadius = 2;
			pickUpPrefix = "a";
			description  = "broadsword_model";
			baseemitter = FireEmitter6;
			
			computeCRC = false;
			emap = true;
		};
	//------------------------------------------------
	//Water Broadsword
	//------------------------------------------------
		datablock ParticleData(KeldrinFountianSpoutParticle2)
		{
		    dragCoefficient = 0;
		    gravityCoefficient = 0;
		    windCoefficient = 0;
		    inheritedVelFactor = 0.2;
		    constantAcceleration = -2;
		    lifetimeMS = 10000;
		    lifetimeVarianceMS = 400;
		    useInvAlpha = 0;
		    spinRandomMin = -200;
		    spinRandomMax = 0;
		    textureName = "special/cloudflash.png";
		    times[0] = 0;
		    times[1] = 0.209677;
		    times[2] = 1;
		    colors[0] = "0.700787 0.848000 1.000000 0.500000";
		    colors[1] = "0.300000 0.300000 0.300000 0.648387";
		    colors[2] = "0.000000 0.000000 0.000000 0.761290";
		    sizes[0] = 0.1;
		    sizes[1] = 0.2;
		    sizes[2] = 0.3;
		};

		datablock ParticleEmitterData(WaterEmitter6)
		{
		    ejectionPeriodMS = 5;
		    periodVarianceMS = 1;
		    ejectionVelocity = 10;
		    velocityVariance = 5;
		    ejectionOffset =   0;

		    lifetimeMS = 0;
		    thetaMin = 0;
		    thetaMax = 360;
		    phiReferenceVel = 0;
		    phiVariance = 360;
		    overrideAdvances = 0;
		    orientParticles= 0;
		    orientOnVelocity = 0;
		    particles = "KeldrinFountianSpoutParticle2";
		};

		datablock ShapeBaseImageData(WaterBroadSwordImage)
		{
			className = WeaponImage;
			shapeFile = "BroadSword.dts";
			item = WaterBroadSword;
			offset = "0 0 -0.1";
			//extension = $ItemBaseRange[Dagger];
	
			//usesEnergy = true;
	
			//projectile = EnergyBolt;
			//projectileType = EnergyProjectile;
			//fireEnergy = 4;
			//minEnergy = 4;
			delayEmitter = "IceSpikeEmitter6";
			//baseEmitterNode = Muzzlepoint1;
			
			stateName[0] = "Activate";
			stateTransitionOnTimeout[0] = "ActivateReady";
			stateTimeoutValue[0] = 0.5;
			stateSequence[0] = "Activate";
			stateSound[0] = DrawLongSword;
		
			stateName[1] = "ActivateReady";
			//stateEmitter[1] = "IceSpikeEmitter6";
			//stateEmitterNode[1] = Muzzlepoint1;
			//stateEmitterTime[1] = 10000000;
			stateTransitionOnLoaded[1] = "Ready";
		
			stateName[2] = "Ready";
			stateEmitter[2] = "WaterEmitter6";
			stateEmitterNode[2] = Muzzlepoint1;
			stateEmitterTime[2] = 5000000;
			//stateTimeoutValue[2] = 5;
			//stateTransitionOnTimeout[2] = "Ready";
			stateTransitionOnTriggerDown[2] = "Fire";
		
			stateName[3] = "Fire";
			stateTransitionOnTimeout[3] = "Ready";
			stateTimeoutValue[3] = 0.1;
			stateFire[3] = true;
			//stateEmitter[3] = "IceSpikeEmitter6";
			//stateEmitterTime[3] = 500;
			//stateEmitterNode[3] = Muzzlepoint1;
			stateRecoil[3] = NoRecoil;
			stateAllowImageChange[3] = false;
			stateSequence[3] = "Fire";
			stateScript[3] = "onFire";
		};
	
		datablock ItemData(WaterBroadSword)
		{
			className    = Weapon;
			catagory     = "Spawn Items";
			shapeFile    = "BroadSword.dts";
			image        = WaterBroadSwordImage;
			mass         = 1;
			elasticity   = 0.2;
			friction     = 0.6;
			pickupRadius = 2;
			pickUpPrefix = "a";
			description  = "broadsword_model";
			baseemitter = FireEmitter6;
			
			computeCRC = false;
			emap = true;
		};
	//------------------------------------------------
	//ShortSword
	//------------------------------------------------

	datablock ShapeBaseImageData(ShortSwordImage)
	{
		className = WeaponImage;
		shapeFile = "ShortSword.dts";
		item = ShortSword;
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(ShortSword)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "ShortSword.dts";
		image        = ShortSwordImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "ShortSword_model";

		computeCRC = false;
		emap = true;
	};
	
	//-------------------------------
	//ShortSwort2
	//-------------------------------
	
	datablock ShapeBaseImageData(ShortSword2Image)
	{
		className = WeaponImage;
		shapeFile = "ShortSword2.dts";
		item = ShortSword2;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(ShortSword2)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "ShortSword2.dts";
		image        = ShortSword2Image;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "ShortSword_model";

		computeCRC = false;
		emap = true;
	};

	//-------------------------------
	//hatchet
	//-------------------------------
	
	datablock ShapeBaseImageData(hatchetImage)
	{
		className = WeaponImage;
		shapeFile = "hatchet.dts";
		item = hatchet;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";
	
		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};

	datablock ItemData(hatchet)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "hatchet.dts";
		image        = hatchetImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "hatchet_model";

		computeCRC = false;
		emap = true;
	};
	//-----------
	//shortbow
	//-----------
	datablock ShapeBaseImageData(ShortbowImage)
	{
		className = WeaponImage;
		shapeFile = "ShortBow.dts";
		item = ShortBow;
		offset = "0.05 0.05 -0.35";
		rotation = "-10 0 0 1";
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Draw";
	
		stateName[3] = "Draw";
		stateTransitionOnTimeout[3] = "Hold";
		stateTimeoutValue[3] 	    = 0.9;
		stateSequence[3] = "drawback";
		//stateSpinThread[3] = "drawback";
		
		stateName[4] = "Hold";
		stateTransitionOnTriggerDown[4] = "readyFire";
		stateSequence[4] = "hold";
		stateScript[4] = "load";
		stateAllowImageChange[4] = false;
		//stateSpinThread[4] = "hold";
		
		stateName[5] = "Fire";
		stateTransitionOnTimeout[5] = "Ready";
		stateTimeoutValue[5] = 0.1;
		stateFire[5] = true;
		stateRecoil[5] = HeavyRecoil;
		stateAllowImageChange[5] = false;
		stateSequence[5] = "Fire";
		stateScript[5] = "onFire";
		stateSequence[5] = "release";
		//stateSpinThread[5] = "release";
		
		stateName[6] = "readyFire";
		stateSequence[6] = "hold";
		stateAllowImageChange[6] = false;
		//stateSpinThread[6] = "hold";
		stateTransitionOnTimeout[6] = "Fire";
		stateTimeoutValue[6] = 0.5;
	};
	datablock ItemData(ShortBow)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "Shortbow.dts";
		image        = ShortBowImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "ShortBow_model";

		computeCRC = false;
		emap = true;
	};
	//-----------
	//LightCrossbow
	//-----------
	datablock ShapeBaseImageData(LightCrossbowImage)
	{
		className = WeaponImage;
		shapeFile = "ShortBow.dts";
		item = LightCrossbow;
		offset = "0.05 0.05 -0.0";
		rotation = "-100 0 0 1";
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Draw";
	
		stateName[3] = "Draw";
		stateTransitionOnTimeout[3] = "Hold";
		stateTimeoutValue[3] 	    = 0.9;
		stateSequence[3] = "drawback";
		//stateSpinThread[3] = "drawback";
		
		stateName[4] = "Hold";
		stateTransitionOnTriggerDown[4] = "readyFire";
		stateSequence[4] = "hold";
		stateScript[4] = "load";
		stateAllowImageChange[4] = false;
		//stateSpinThread[4] = "hold";
		
		stateName[5] = "Fire";
		stateTransitionOnTimeout[5] = "Ready";
		stateTimeoutValue[5] = 0.1;
		stateFire[5] = true;
		stateRecoil[5] = HeavyRecoil;
		stateAllowImageChange[5] = false;
		stateSequence[5] = "Fire";
		stateScript[5] = "onFire";
		stateSequence[5] = "release";
		//stateSpinThread[5] = "release";
		
		stateName[6] = "readyFire";
		stateSequence[6] = "hold";
		stateAllowImageChange[6] = false;
		//stateSpinThread[6] = "hold";
		stateTransitionOnTimeout[6] = "Fire";
		stateTimeoutValue[6] = 0.5;
	};
	datablock ItemData(LightCrossbow)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "Shortbow.dts";
		image        = LightCrossbowImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "LightCrossbow_model";

		computeCRC = false;
		emap = true;
	};	

	//-----------
	//LightCrossbow
	//-----------
	datablock ShapeBaseImageData(SlingImage)
	{
		className = WeaponImage;
		shapeFile = "ShortBow.dts";
		item = Sling;
		offset = "0.05 0.05 -0.0";
		scale = "0.2 0.2 0.2";
		rotation = "-100 0 0 1";
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Draw";
	
		stateName[3] = "Draw";
		stateTransitionOnTimeout[3] = "Hold";
		stateTimeoutValue[3] 	    = 0.9;
		stateSequence[3] = "drawback";
		//stateSpinThread[3] = "drawback";
		
		stateName[4] = "Hold";
		stateTransitionOnTriggerDown[4] = "readyFire";
		stateSequence[4] = "hold";
		stateScript[4] = "load";
		stateAllowImageChange[4] = false;
		//stateSpinThread[4] = "hold";
		
		stateName[5] = "Fire";
		stateTransitionOnTimeout[5] = "Ready";
		stateTimeoutValue[5] = 0.1;
		stateFire[5] = true;
		stateRecoil[5] = HeavyRecoil;
		stateAllowImageChange[5] = false;
		stateSequence[5] = "Fire";
		stateScript[5] = "onFire";
		stateSequence[5] = "release";
		//stateSpinThread[5] = "release";
		
		stateName[6] = "readyFire";
		stateSequence[6] = "hold";
		stateAllowImageChange[6] = false;
		//stateSpinThread[6] = "hold";
		stateTransitionOnTimeout[6] = "Fire";
		stateTimeoutValue[6] = 0.5;
	};
	datablock ItemData(Sling)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "Shortbow.dts";
		image        = SlingImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "Sling_model";

		computeCRC = false;
		emap = true;
	};	
	
	//--------
	//longbow
	//--------
	datablock ShapeBaseImageData(LongbowImage)
	{
		className = WeaponImage;
		shapeFile = "LongBow.dts";
		item = LongBow;
		offset = "0.05 0.05 -0.35";
		rotation = "-10 0 0 1";
		//extension = $ItemBaseRange[Dagger];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;
	
		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = DrawKnife;
	
		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";
	
		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Draw";
	
		stateName[3] = "Draw";
		stateTransitionOnTimeout[3] = "Hold";
		stateTimeoutValue[3] 	    = 0.9;
		stateSequence[3] = "drawback";
		//stateSpinThread[3] = "drawback";
		
		stateName[4] = "Hold";
		stateTransitionOnTriggerDown[4] = "readyFire";
		stateSequence[4] = "hold";
		stateScript[4] = "load";
		stateAllowImageChange[4] = false;
		//stateSpinThread[4] = "hold";
		
		stateName[5] = "Fire";
		stateTransitionOnTimeout[5] = "Ready";
		stateTimeoutValue[5] = 0.1;
		stateFire[5] = true;
		stateRecoil[5] = HeavyRecoil;
		stateAllowImageChange[5] = false;
		stateSequence[5] = "Fire";
		stateScript[5] = "onFire";
		stateSequence[5] = "release";
		//stateSpinThread[5] = "release";
		
		stateName[6] = "readyFire";
		stateSequence[6] = "hold";
		stateAllowImageChange[6] = false;
		//stateSpinThread[6] = "hold";
		stateTransitionOnTimeout[6] = "Fire";
		stateTimeoutValue[6] = 0.5;
	};	
	
	datablock ItemData(LongBow)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "longbow.dts";
		image        = LongBowImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "LongBow_model";

		computeCRC = false;
		emap = true;
	};
	//############################################################
	datablock ShapeBaseImageData(BasicArrowImage)
	{
		className = WeaponImage;
		shapeFile = "BasicArrow.dts";
		item = BasicArrow;
		offset = "0.7 0 0";
		//rotation = "-10 0 0 1";
	};
	datablock ItemData(BasicArrow)
	{
		className    = Weapon;
		catagory     = "Spawn Items";
		shapeFile    = "BasicArrow.dts";
		image        = BasicArrowImage;
		mass         = 1;
		elasticity   = 0.2;
		friction     = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description  = "BasicArrow_model";
		rotation = "0 0 0";
		computeCRC = false;
		emap = true;
	};	
	
	datablock ShapeBaseImageData(SmallRockImage)
	{
		className = WeaponImage;
		shapeFile = "smallrock.dts";
		item = SmallRock;
		offset = "0.7 0 0";
		//rotation = "-10 0 0 1";
	};

	//############################################################
	 
	datablock ItemData(armor1)
	{
		className     = Weapon;
		catagory      = "Misc";
		shapeFile     = "repair_kit.dts";
		mass          = 1;
		elasticity    = 0.2;
		friction      = 0.6;
		pickupRadius  = 2;
		pickUpPrefix  = "a";
		alwaysAmbient = true;
		description   = "armor1_model";

		computeCRC = false;
		emap = true;
	};
	
	//############################################################
	datablock ItemData(Potion)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "repair_kit.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "potion_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(BluePotion)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "HealthPotion.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "BluePotion_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(CrystalBluePotion)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "AdvHealthPotion.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "CrystalBluePotion_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(EnergyVial)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "ManaPotion.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "EnergyVial_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(CrystalEnergyVial)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "AdvManaPotion.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "CrystalEnergyVial_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(Lootbag)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "moneybag.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 50;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "lootbag_model";
	
			computeCRC = false;
			emap = true;
	};

	//datablock ItemData(MineRock)
	//{
	//		className     = Weapon;
	//		catagory      = "Misc";
	//		shapeFile     = "ammo_disc.dts";
	//		mass          = 1;
	//		elasticity    = 0.2;
	//		friction      = 0.6;
	//		pickupRadius  = 2;
	//		pickUpPrefix  = "a";
	//		alwaysAmbient = true;
	//		description   = "MineRock_model";
	//		cannotpickup = false;
	//		computeCRC = false;
	//		emap = true;
	//};

	datablock ItemData(SmallRock)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapefile 	= "smallrock.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "SmallRock_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreClay)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Clay.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreClay_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};

	datablock ItemData(OreCopper)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Copper.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreCopper_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};

	datablock ItemData(OreGranite)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Granite.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreGranite_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};

	datablock ItemData(OreTin)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Tin.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "Tin_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreIron)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Iron.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreIron_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreCoal)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Coal.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreCoal_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreDiere)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Diere.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreDiere_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreSilver)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Silver.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "Silver_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreGold)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Gold.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreGold_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreMithril)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Mithril.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreMithril_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreAdamite)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Adamite.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreAdamite_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(OreKeldrinite)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Keldrinite.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "OreKeldrinite_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemQuartz)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Quartz.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "GemQuartz_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemOpal)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Opal.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "GemOpal_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};

	datablock ItemData(GemJade)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Jade.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "GemJade_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemTurquoise)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Turqoise.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "Turquoise_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemTopaz)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Topaz.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "Topaz_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemSapphire)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Sapphire.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "Sapphire_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GemRuby)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Ruby.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "GemRuby_model";
			cannotpickup = false;
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(MineStone)//this is the unmined model that will be spawned on the walls and floor for players to mine!
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "Ore_Rough.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "MineStone_model";
			cannotpickup = true;
			computeCRC = false;
			emap = true;
	};
	
	datablock ItemData(MineGem)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "smallcrystal.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "MineRock_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(GnollHide)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "gnollhide.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "MineRock_model";
	
			computeCRC = false;
			emap = true;
	};
	datablock ItemData(Fish)
	{
			className     = Weapon;
			catagory      = "Misc";
			shapeFile     = "fish.dts";
			mass          = 1;
			elasticity    = 0.2;
			friction      = 0.6;
			pickupRadius  = 2;
			pickUpPrefix  = "a";
			alwaysAmbient = true;
			description   = "fish_model";
	
			computeCRC = false;
			emap = true;
	};

	Custom::ItemDatas();


}
function ShortBowImage::Load(%this, %obj, %slot)
{
	%weaponid = fetchdata(%obj.client, "weaponinhand");
	%ammo = $itemAmmo[Game.getItem(%obj.client, %weaponid)];
	
	if(Game.GetItemCount(%obj.client, %ammo, 3, 1) > 0)
	%obj.mountImage(BasicArrowimage, 1);
}
function ShortBowImage::onFire(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	%client = %obj.client;
	%weaponid = fetchdata(%client, "weaponinhand");
	%ammo = $itemAmmo[game.getItem(%client, %weaponid)];
	
	if(Game.GetItemCount(%client, %ammo, 3, 1) > 0)
	{
		
		//time to fire!!
		//get the basic arrow id out of his inv...
		%client.player.throwarrow(%ammo, %slot, %this);
		Game.RemoveFromInventory(%client, 1, %ammo, 3, 1);
	}
}
function shortbowimage::onUnmount(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	Parent::onUnmount(%this, %obj, %slot);
}
function SlingImage::Load(%this, %obj, %slot)
{
	
	%weaponid = fetchdata(%obj.client, "weaponinhand");
		%ammo = $itemAmmo[Game.getItem(%obj.client, %weaponid)];
	
}
function SlingImage::onFire(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	%client = %obj.client;
	%weaponid = fetchdata(%client, "weaponinhand");
	%ammo = $itemAmmo[game.getItem(%client, %weaponid)];
	
	if(Game.GetItemCount(%client, %ammo, 3, 1) > 0)
	{
		
		//time to fire!!
		//get the basic arrow id out of his inv...
		%client.player.throwarrow(%ammo, %slot, %this);
		Game.RemoveFromInventory(%client, 1, %ammo, 3, 1);
	}
}
function Slingimage::onUnmount(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	Parent::onUnmount(%this, %obj, %slot);
}
function LightCrossbowImage::Load(%this, %obj, %slot)
{
	%weaponid = fetchdata(%obj.client, "weaponinhand");
	%ammo = $itemAmmo[Game.getItem(%obj.client, %weaponid)];
	if(%obj.client.data.invcount[%ammo, 3, 1] > 0)
		%obj.mountImage(BasicArrowimage, 1);
}
function LightCrossbowImage::onFire(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	%client = %obj.client;
	%weaponid = fetchdata(%client, "weaponinhand");
	%ammo = $itemAmmo[game.getItem(%client, %weaponid)];
	
	if(Game.GetItemCount(%client, %ammo, 3, 1) > 0)
	{
		
		//time to fire!!
		//get the basic arrow id out of his inv...
		%client.player.throwarrow(%ammo, %slot, %this);
		Game.RemoveFromInventory(%client, 1, %ammo, 3, 1);
	}
}
function LightCrossbowimage::onUnmount(%this, %obj, %slot)
{
	%obj.unmountImage(1);
	Parent::onUnmount(%this, %obj, %slot);
}
function LongBowImage::Load(%this, %obj, %slot)
{

	//%obj.mountImage(basicarrowimage, 1);
}
function LongBowImage::onFire(%this, %obj, %slot)
{

	//%obj.unmountImage(1);
	//%this.setThread("drawback", 1);
}
function Longbowimage::onUnmount(%this, %obj, %slot)
{
	//%obj.unmountImage(1);
	//Parent::onUnmount(%this, %obj, %slot);
}

function ShapeBase::throwarrow(%this, %item, %slot, %image)
{

	%client = %this.client;
	if(fetchdata(%client, "nextHitignite") == true)
	{
		%ignite = true;
		storedata(%client, "nextHitIgnite", false);
	}
	else
		%ignite = false;
	
	if($datablock[%item] !$= "")
	{
		%item = new Item()
		{
			dataBlock = $datablock[%item];
			item = %item;
			projectile = true;
			owner = %client;
			ignite = %ignite;
			prefix = 3;
			suffix = 1;
		};
		if(%ignite)
		schedule(10000, %client, "resetignite", %client);
		MissionCleanup.add(%item);
		%eye = %this.getEyeVector();
		%vec = vectorScale(%eye, 60);
		%pos = getBoxCenter(%this.getWorldBox());
		%pos = getword(%pos, 0) SPC getword(%pos, 1) SPC getword(%pos, 2) + 0.75;
		%item.setTransform(%pos SPC vectorScale(%this.getEyeVector(), 180) SPC 360);
		%item.applyImpulse(%pos,%vec);
		%item.setCollisionTimeout(%this);
		//RemoveFromInventory(%client, %itemId);
		%item.schedule(120*1000, "delete");
		//%this.throwItem(%itemId);
	}

	
}
function resetignite(%client)
{
	storedata(%client, "blockignite", false);
}
function ShapeBaseImageData::onFire(%data, %obj, %slot)
{
	%client = %obj.client;
	//%weapon = %data.getName().item;
	%itemId = fetchData(%client, "weaponInHand");
	%val = MeleeAttack(%client, Game.GetRange(Game.GetItem(%client, %itemId)), %itemId);

	return %val;
}

function ShapeBaseImageData::onUnmount(%data, %obj, %slot)
{
	if(%data.deleteLastProjectile && isObject(%obj.lastProjectile))
	{
		%obj.lastProjectile.delete();
		%obj.lastProjectile = "";
	}
}
//special abilities
function PickAxeImage::onFire(%this, %obj, %slot)
{
	//just an example in case a special ability is needed for a specific weapon model
	//see if there is ore infront of the player!
	//do radius swing if player is not infront of range.
	//ok... lets see...
	//GetLosInfo(%client, 5);
	%val = Parent::onFire(%this, %obj, %slot);//fire away!
	if(%val == 1 && !%obj.client.isaiControlled())
	{
		%muzzlePos = %obj.getMuzzlePoint(%slot);
		%muzzleVec = %obj.getMuzzleVector(%slot);

		%endPos    = VectorAdd(%muzzlePos, VectorScale(%muzzleVec, 5));

		%Masks = $TypeMasks::TurretObjectType*2*2*2*2-1;//everything =)
		// did I miss anything? players, vehicles, stations, gens, sensors, turrets
		%hit = ContainerRayCast(%muzzlePos, %endPos, %masks, %obj);
		if(%hit != 0)
			%rock = SearchForRock(GetWord(%hit,1) SPC Getword(%hit, 2) SPC Getword(%hit, 3));


		if(!%rock || %hit == 0) 
			return;
		if(!hasskilltomine(%obj.client, %rock.reward)) 
			%rock.reward = $MiningList[1];
		MessageClient(%obj.client,'MineRock', "You obtained a " @ $ItemDesc[%rock.reward] @ ".");
		AddToInventory(%obj.client, GenerateUniqueID(), %rock.reward, 0, 0, 0);
		%rock.delete();
		UseSkill(%obj.client, $SkillMining, true, true, 1/10);
	}
	
}

function RPGGame::AddToStorage(%game, %client, %num, %item ,%prefix, %suffix)
{
	if(getWord(%num, 0) == -1)
	{
		%item   = getWord(%num, 1);
		%prefix = getWord(%num, 2);
		%suffix = getWord(%num, 3);
		%num = 1;
		
	}
	%num = mfloor(%num);
	if(%item $= "") return;
	if(%suffix == 0) %suffix = 1;
	if(%prefix == 0) %prefix = 3;
	%full = %game.GetFullItemName(%prefix, %item);
	if(%client.data.bankcount[%item, %prefix, %suffix] == 0 && %num > 0)
	{
		
		%client.data.banklist = %client.data.banklist @ %prefix @ "%" @ %item @ "%" @ %suffix @ " ";
		%add = true;
	}
	%client.data.bankcount[%item, %prefix, %suffix] += %num;
	

	if(%client.data.count[strreplace(%full, " ", "x")] == 0)//if undefined define an item id for this client so the inventory system will accept it.
	{
		%client.data.randcount++;
		%id = %client.data.randcount;
		while(%client.data.idcount[%id] !$= "")
		{
			%client.data.randcount++;
			%id = %client.data.randcount;

		}
		%client.data.count[strreplace(%full, " ", "x")] = %id;
		%client.data.idname[%id] = %item;
		%client.data.prefix[%id] = %prefix;
		%client.data.suffix[%id] = %suffix;
		%client.data.idcount[%id] = %full;
		
		%add = true;
	}
	else
		%id = %client.data.count[strreplace(%full, " ", "x")];
	
	
	if(%add)
		game.onaddtostorage(%client, %id, %num, %item, %full, %prefix, suffix);
	%client.weight += $ItemBaseWeight[%item]*%num;
	weightstep(%client);
	return %id;	
	
}
function RPGGame::onAddToStorage(%game, %client, %itemid, %amt, %name, %fullitem,%prefix,  %suffix)
{
	commandToClient(%client, 'ShopAddRow', %itemId, %fullitem, %name, %amt, %type);
}
function RPGGame::RefreshBank(%game, %client)
{
	//echo("RPGGame::RefreshBank(" SPC %game SPC %client SPC ")");
	//the purpose of this function is to update the clients bank screen (on his end) after his character is loaded. Also it will cut down the item list in order to save space.
	%banklist = %client.data.banklist;
	%client.data.banklist = "";
	//lets go.
	commandToClient(%client, 'ShopUpdateHud');
	
	for(%i; getword(%banklist, %i) !$= ""; %i++)
	{
		%iteml = getword(%banklist, %i);
		%prefix = GetWord(strreplace(%iteml, "%", " "), 0);
		%item = GetWord(strreplace(%iteml, "%", " "), 1);
		%suffix = GetWord(strreplace(%iteml, "%", " "), 2);
		%full = %game.GetFullItemName(%prefix, %item, %suffix);
		%id = %client.data.count[strreplace(%full, " ", "x")];
		%num = %client.data.bankcount[%item, %prefix, %suffix];
		
		if (%num >= 1)
		{
			%ol = %nbanklist;
			%nbanklist = strreplace(%nbanklist, %prefix @ "%" @ %item @ "%" @ %suffix @ " ", "") @ %prefix @ "%" @ %item @ "%" @ %suffix @ " ";
			if(!%temp[%prefix @ "%" @ %item @ "%" @ %suffix])
			{
				
				%game.onAddToStorage(%client, %id, %num, %item, %full, %prefix, %suffix);
				%temp[%prefix @ "%" @ %item @ "%" @ %suffix] = true;
			}
			
		}
	}
	
	%client.data.banklist = %nbanklist;
	
}
function RPGGame::RemoveFromStorage(%game, %client, %num, %item, %prefix, %suffix)
{
	%prefix += 0;
	%suffix += 0;
	%full = %game.GetFullItemName(%prefix, %item, %suffix);
	
	if(%client.data.bankcount[%item, %prefix, %suffix] < %num) 
	{
		%num = %client.data.bankcount[%item, %prefix, %suffix];
		error("Attempted to remove more items from storage than client had! Check for duping! Client Name:" SPC %client.rpgname);
	}
	%client.data.bankcount[%item, %prefix, %suffix] -= %num;
	%dataname = %prefix @ "%" @ %item @ "%" @ %suffix;
	//if(%client.invlistsel == %client.data.count[strreplace(%full, " ", "x")])
	//{
	//	%game.InventoryListOnSelect(%client, %client.invlistsel);
	//}	
	if(%client.data.bankcount[%item, %prefix, %suffix] == 0)
	{
		%client.data.banklist = strreplace(%client.data.banklist, %prefix @ "%" @ %item @ "%" @ %suffix @ " ", "");
		if(!%client.isaicontrolled())
		game.onRemoveFromStorage(%client, %client.data.count[strreplace(%full, " ", "x")], %full);
	}

	%client.weight -= $ItemBaseWeight[%item]*%num;
	if(%client.weight < 0) 
	{
		%client.weight = 0;// double check, shouldnt happen either generate error
		error("ERROR: Client weight below 0, ClientId:" SPC %client);
	}
	weightstep(%client);
	return %id;
}
function RPGGame::onRemoveFromStorage(%game, %client, %id, %full)
{
	
	commandToClient(%client, 'RemoveFromBank', %id, %full);
}
function RPGGame::GetBankCount(%game, %client, %item, %prefix, %suffix)
{
	return %client.data.bankcount[%item, %prefix, %suffix];
}
function RPGGame::CreateItem(%game, %item, %prefix, %suffix)
{
	return -1 SPC %item SPC %prefix SPC %suffix;//this should do it.
}
function RPGGame::ClearInventory(%game, %client)
{
	//storedata(%client, "inventory", "");
	for(%i = 0; GetWord(%client.data.itemlist, %i) !$= ""; %i++)
	{
		%item = GetWord(%client.data.itemlist, %i);
		for(%ii = 1; %ii <= 6; %ii++)
		{
			if(%client.data.invcount[%item, %ii, 1])
			%client.data.invcount[%item, %ii, 1] = "";
		}
	
	}
	%client.data.itemlist = "";
	commandToClient(%client, 'InventoryUpdateHud');
	commandToClient(%client, 'InventoryDone');
	%client.weight = 0;
	//%client.randcount = 0;
	
}
function RPGGame::AddToInventory(%game, %client, %num, %item, %prefix, %suffix, %equipped)
{
	if(%item $= "") return;
	
	if(%suffix == 0 || $itemtype[%item] $= "item" || $itemtype[%item] $= "potion") %suffix = 1;
	if(%prefix == 0 || $itemtype[%item] $= "item" || $itemtype[%item] $= "potion") %prefix = 3;
	%full = %game.GetFullItemName(%prefix, %item, %suffix);
	
	if(%client.data.invcount[%item, %prefix, %suffix] == 0 && %num > 0)
	{
		
		%client.data.itemlist = %client.data.itemlist @ %prefix @ "%" @ %item @ "%" @ %suffix @ " ";
		%add = true;
	}
	%client.data.invcount[%item, %prefix, %suffix] += %num;
	if($itemtype[%item] $= "item" || $itemtype[%item] $= "potion")
	{
		if(%client.data.count[strreplace(%full, " ", "x")] != 0)
		{
			%id = %client.data.count[strreplace(%full, " ", "x")];
			%client.data.prefix[%id] = 3;
			%client.data.suffix[%id] = 1;
		}
	}

	if(%client.data.count[strreplace(%full, " ", "x")] == 0)//if undefined define an item id for this client so the inventory system will accept it.
	{
		%client.data.randcount++;
		%id = %client.data.randcount;
		while(%client.data.idcount[%id] !$= "")
		{
			%client.data.randcount++;
			%id = %client.data.randcount;
		}
		%client.data.count[strreplace(%full, " ", "x")] = %id;
		%client.data.idname[%id] = %item;
		%client.data.prefix[%id] = %prefix;
		%client.data.suffix[%id] = %suffix;
		%client.data.idcount[%id] = %full;
		
		%add = true;
	}
	else
		%id = %client.data.count[strreplace(%full, " ", "x")];
	
	
	if(%add)
		game.onaddinventory(%client, %full, %item, %id, %client.data.invcount[%item, %prefix, %suffix]);
	%client.weight += $ItemBaseWeight[%item]*%num;
	weightstep(%client);
	return %id;
}
function RPGGame::onAddInventory(%game, %client, %full, %item, %id, %count)
{
	
	commandToClient(%client, 'AddToInventory', %id, %full,  $itemType[%item],"", %count);
	
}

function RPGGame::RemoveFromInventory(%game, %client, %num, %item, %prefix, %suffix)
{
	%prefix += 0;
	//%suffix += 0;
	%full = %game.GetFullItemName(%prefix, %item);
	%client.data.invcount[%item, %prefix, %suffix] -= %num;
	%dataname = %prefix @ "%" @ %item @ "%" @ %suffix;
	if(%client.invlistsel == %client.data.count[strreplace(%full, " ", "x")])
	{
		%game.InventoryListOnSelect(%client, %client.invlistsel);
	}	
	if(%client.data.invcount[%item, %prefix, %suffix] == 0)
	{
		%client.data.itemlist = strreplace(%client.data.itemlist, %prefix @ "%" @ %item @ "%" @ %suffix @ " ", "");
		if(!%client.isaicontrolled())
		game.onRemoveInventory(%client,  %full);
	}

	%client.weight -= $ItemBaseWeight[%item]*%num;
	if(%client.weight < 0) 
	{
		%client.weight = 0;// double check, shouldnt happen either generate error
		error("ERROR: Client weight below 0, ClientId:" SPC %client);
	}
	weightstep(%client);
	return %id;
}
function RPGGame::onRemoveInventory(%game, %client,  %full)
{
	
	commandToClient(%client, 'RemoveFromInventory', %client.data.count[strreplace(%full, " ", "x")], %full);
	
}

function RPGGame::AddToEquipList(%game, %client, %itemid)
{
	//if(ltrim(strreplace(" " @ %client.equiplist, " " @ %itemid @ " ", " ")) $= %client.equiplist) //item already in equip list error
	//%client.equiplist = %client.equiplist @ %itemid @ " ";
	//else
	//error("Item already in equip list" SPC %client.rpgname SPC %client);
}

function RPGGame::RemoveFromEquipList(%game, %client, %itemid)
{
	//%client.equiplist = ltrim(strreplace(" " @ %client.equiplist, " " @ %itemid @ " ", " "));
}
function RPGGame::GetEquipList(%game, %client)
{
	//return %client.equiplist;
}
function RPGGame::getItemCount(%game, %client, %item, %prefix, %suffix)
{
	
	return %client.data.invcount[%item, %prefix, %suffix];
}	
function RPGGame::ClearInvInfo(%game, %itemId)
{
	//$InvInfo[%itemId, item] = "";
	//$InvInfo[%itemId, equipped] = "";
	//$InvInfo[%itemId, prefix] = "";
	//$InvInfo[%itemId, suffix] = "";
}
function RPGGame::ReCreateItemList(%game, %client)
{
	//the purpose of this function is to update the clients inventory screen (on his end) after his character is loaded. Also it will cut down the item list in order to save space.
	%itemlist = %client.data.itemlist;
	%client.data.itemlist = "";
	//lets go.
	commandToClient(%client, 'InventoryUpdateHud');
	%client.weight = 0;
	for(%i; getword(%itemlist, %i) !$= ""; %i++)
	{
		%iteml = getword(%itemlist, %i);
		%prefix = GetWord(strreplace(%iteml, "%", " "), 0);
		%item = GetWord(strreplace(%iteml, "%", " "), 1);
		%suffix = GetWord(strreplace(%iteml, "%", " "), 2);
		%full = %game.GetFullItemName(%prefix, %item);
		%id = %client.data.count[strreplace(%full, " ", "x")];
		%num = %client.data.invcount[%item, %prefix, 1];
		%client.weight += $ItemBaseWeight[%item]*%num;
		if (%num >= 1)
		{
			%nitemlist = strreplace(%nitemlist, %prefix @ "%" @ %item @ "%" @ %suffix @ " ", "") @ %prefix @ "%" @ %item @ "%" @ %suffix @ " ";
			%game.onAddInventory(%client, %full, %item, %id);
		}
	}
	
	%client.data.itemlist = %nitemlist;
	

}
function RPGGame::ShapeBasethrowItem(%game, %this, %id)
{
	//uhhh small issue here, maybe itemid, and we can back it up.
	%client = %this.client;
	%item = new Item()
	{
		dataBlock =	$DataBlock[%client.data.idname[%id]];
		rotation = "0 0 1 " @ (getRandom() * 360);
		item = %client.data.idname[%id];
		prefix = %client.data.prefix[%id];
		itemid = %id;
		suffix = 1;
	};
	
	%item.schedule(5 * 60 * 1000, delete);

	MissionCleanup.add(%item);
	%game.ShapeBasethrowObject(%this, %item);
	%game.RemoveFromInventory(%client, 1, %client.data.idname[%id], %client.data.prefix[%id], 1);
    //
    DoItemGlowEffect(%item);
    //
    return %item;
}

function RPGGame::ShapeBasethrowObject(%game, %this,%obj)
{

   //-------------------------------------------
   // z0dd - ZOD, 5/27/02. Fixes flags hovering
   // over friendly player when collision occurs
   if(%obj.getDataBlock().getName() $= "Flag")
      %obj.static = false;
   //-------------------------------------------

   //if the object is being thrown by a corpse, use a random vector
   if (%this.getState() $= "Dead")
   {
      %vec = (-1.0 + getRandom() * 2.0) SPC (-1.0 + getRandom() * 2.0) SPC getRandom();
      %vec = vectorScale(%vec, 10);
   }

   // else Initial vel based on the dir the player is looking
   else
   {
      %eye = %this.getEyeVector();
      %vec = vectorScale(%eye, 20);
   }

   // Add a vertical component to give the item a better arc
   %dot = vectorDot("0 0 1",%eye);
   if (%dot < 0)
      %dot = -%dot;
   %vec = vectorAdd(%vec,vectorScale("0 0 8",1 - %dot));

   // Add player's velocity
   %vec = vectorAdd(%vec,%this.getVelocity());
   %pos = getBoxCenter(%this.getWorldBox());

   //since flags have a huge mass (so when you shoot them, they don't bounce too far)
   //we need to up the %vec so that you can still throw them...
   if (%obj.getDataBlock().getName() $= "Flag")
      %vec = vectorScale(%vec, 40);

   //
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos,%vec);
   %obj.setCollisionTimeout(%this);
   %data = %obj.getDatablock();
   %data.onThrow(%obj,%this);

   //call the AI hook
   AIThrowObject(%obj);
}
function RPGGame::ShapeBaseThrow(%game, %this, %itemid)
{
	%client = %this.client;//no client?? eh?
	if(%client == 0)
	return;
	%item = %client.data.idname[%itemid];
	%prefix = %client.data.prefix[%itemid];
	%nodrop = false;
	
	if(%client.data.equipped[%itemId])
	{
		if($ItemType[%item] $= "armor")
			%nodrop = true;
		else if($ItemType[%item] $= "weapon")
			%nodrop = false;	//you can drop an equipped weapon
	}
	if(!%nodrop)
	{
		// Throw item first...
		if(%itemid == fetchdata(%client, "WeaponInHand"))
		{
			%game.inventoryUse(%client, %itemid);//toggle off!
		}
		%id =  %game.ShapeBasethrowItem(%this, %itemId);
		//%game.RemoveFromInventory(%client, 1, %item, %prefix, 1);
		return %id;
	}
	else
	{
		messageClient(%client, 'ShapeBaseThrow', $MsgRed @ "You can't drop an equipped item!");
	}
}
function RPGGame::InventoryDrop(%game, %client, %itemId)
{
	//Server-side
	%item = %client.data.idname[%itemid];
	%prefix = %client.data.prefix[%itemid];
	%suffix = %client.data.suffix[%itemid];

	if(%game.getItemCount(%client, %item, %prefix, %suffix) >= 1)
	{
		
		%obj = %client.player.throw(%itemId);
		
		if($ItemDropFunction[%item] !$= "")
			%game.schedule($ItemDropFuncDelay[%item]*1000, $ItemDropFunction[%item], %obj);
		commandToClient(%client, 'SetCoins', fetchdata(%client, "COINS"));
	}
}
function RPGGame::ItemDataonCollision(%game, %data, %obj, %col)
{

	if($debugMode $= TRUE) echo("ItemData::onCollision(" @ %data @ ", " @ %obj @ ", " @ %col @ ")");
	// Default behavior for items is to get picked 
	// by the colliding object.
	if(%data.cannotpickup) return;
	if(%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead")
	{
		if(%col.client)
		{
			if(%obj.pack)
			{
                // <signal360>
                if( ( %col.client == %obj.dropid || %col.client.rpgname $= %obj.dropname ) ||
                    (ruby_greaterThan(EpochTime(),%obj.protect) || %obj.lck_when_dropped < 1))
				{
					storedata(%col.client, "COINS", %obj.coins, "inc");
					for (%i = 0; ( %iteml = GetWord(%obj.loot, %i) ) !$= ""; %i++)
					{
						%iteml = strreplace(%iteml, "%", " ");
      
						%prefix = GetWord(%iteml, 0);
						%item = GetWord(%iteml, 1);
						%suffix = GetWord(%iteml, 2);
						%num = GetWord(%iteml, 3);
			
						%game.AddToInventory(%col.client, %num, %item, %prefix, %suffix, false);
						%lootlist = %lootlist @ %num SPC
                                    %game.getfullitemname(%prefix, %item, %suffix) @
                                    ((%i == countwords(%obj.loot)-1) ? "" : ", ");
					}
					MessageClient(%col.client, 'lootmessage', 'You picked up %1 from %2 pack!', %lootlist, %obj.dropname @ "'s");
				}
                else
                {
                    BottomPrint(%col.client, "You can't pick this up.\nThis loot bag is reserved for " @ %obj.dropname @ ".",2,3);
                    return;
                }
			}
			else
			{
				%data = %obj.getDatablock();
				%item = %obj.item;
				%prefix = %obj.prefix;
				%suffix = %obj.suffix;
				%itemid = %obj.itemid;
				%fullitemname = %game.GetFullItemName(%prefix, %item, %suffix);
				
				if(!%obj.projectile)
				{
					%game.AddToInventory(%col.client, 1, %item, %prefix, %suffix, "false");
					messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1 %2. (ID#%3)', %data.pickUpPrefix, %fullitemname, %itemId);
				}
				else
				{
					if(%obj.getVelocity() $= "0 0 0")
					{
						AddToInventory(%col.client, 1, %item, %prefix, %suffix, "false");
						messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1 %2. (ID#%3)', %data.pickUpPrefix, %fullitemname, %itemId);
						serverPlay3D(ItemPickupSound, %col.getTransform());
					}
					else
					{
						%obj.owner.hitignite = %obj.ignite;
						%col.getDataBlock().damageObject( %col, %obj.owner.player, %obj.getTransform(), 0, $DamageType::Archery, %obj.getVelocity());
					}
				}

			}
            %obj.delete();
			serverPlay3D(ItemPickupSound, %col.getTransform());
		}
        // </signal360>
	}
}
function RPGGame::TossLootBag(%game, %client, %loot, %coins, %timer)
{
	//%id = GenerateUniqueId();
	%item = new Item()
	{
		dataBlock = LootBag;
		rotation = "0 0 1 " @ (getRandom() * 360);
		itemId = %id;
		pack = true;
	};
    // <signal360>
    %item.protect = (!%client.isAIControlled()) ? ruby_add(epochTime(), %timer) : "0";
    
	%item.loot = %loot;
	%item.dropid = %client;
	%item.dropname = %client.rpgname;
	%item.coins  = %coins;
 
    %item.lck_when_dropped = fetchData(%client, "LCK");
    // </signal360>
	if(%client.isaicontrolled() || %client.player.getState !$= "Dead")
		schedule(5*60*1000, %item, 'DeleteLootBag', %item);

    // phantom
    DoItemGlowEffect(%item);

	MissionCleanup.add(%item);
	%obj = %item;
	%this = %client.player;
	

   //-------------------------------------------

   //if the object is being thrown by a corpse, use a random vector
   if (%this.getState() $= "Dead")
   {
      %vec = (-1.0 + getRandom() * 2.0) SPC (-1.0 + getRandom() * 2.0) SPC getRandom();
      %vec = vectorScale(%vec, 0);
   }

   // else Initial vel based on the dir the player is looking
   else
   {
      %eye = %this.getEyeVector();
      %vec = vectorScale(%eye, 20);
   }

   // Add a vertical component to give the item a better arc
   %dot = vectorDot("0 0 1",%eye);
   if (%dot < 0)
      %dot = -%dot;
   %vec = vectorAdd(%vec,vectorScale("0 0 8",1 - %dot));

   // Add player's velocity ed if not dead
   if(%this.getState() !$= "Dead")
   %vec = vectorAdd(%vec,%this.getVelocity());
   %pos = getBoxCenter(%this.getWorldBox());



   //
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos,%vec);
   %obj.setCollisionTimeout(%this);
  // %data = %obj.getDatablock();
   %obj.fromai = %client.isaicontrolled();
 
}
function RPGGame::InventoryListOnSelect(%game, %client, %itemid)
{
	//Server-side
	if(%client.shop)
	{
		//Server-side
		%skill = GetPlayerSkill( %client, $skill::haggling);
		%multi =  1 + ( ((%skill) / (%skill + 500)) / 3);
		if((%price = mfloor($shop::sellprice[%game.GetItem(%client, %itemId)]*%multi)) == 0  ) 
		%price = mfloor($Shop::BuyPrice[%game.GetItem(%client, %itemId)]/100 * %multi);//one will be skill haggling
		if(%price < 1) %price = 1;

		commandToClient(%client, 'ShopInvListOnSelect', %price);
	}
	else
	{
		%client.invlistsel = %itemid;
		%item = %client.data.idname[%itemid];
		%prefix = %client.data.prefix[%itemid];
		%suffix = %client.data.suffix[%itemid];
		
		%info[1] = " ";
		%info[2] = "Item ID: " @ %itemId;
		%info[3] = "Amount: " @ %game.getitemCount(%client, %item, %prefix, %suffix);
		if(%client.data.equipped[%itemid])
		%info[4] = "Equipped: Yes";
		else
		%info[4] = "Equipped: No";

		commandToClient(%client, 'InventoryListOnSelect', %info[1], %info[2], %info[3], %info[4]);
	}
}
function RPGGame::ShapeBasecycleWeapon(%game, %this, %data)
{
//tofix
	%client = %this.client;
	if(%client == 0)
	return;
	//%wlist = ParseInventory(fetchData(%client, "inventory"), "weapon");
	//lets create our own item list

	%wlist = %client.data.itemlist;
	%wid = %game.fetchData(%client, "weaponInHand");
	%wname = %client.data.idname[%wid];
	%wpre = %client.data.prefix[%wid];
	%wsuf = %client.data.suffix[%wid];
	//%pre = %client.data.idprefix[%wid];
	//%twhep = %pre @ %it;
	%twhep = %wpre @ "%" @ %wname @ "%" @ %wsuf;
	%n = whichWord(%wlist, %twhep);
	%weapon = GetWord(%wlist, %n);
	%itemname = %wname;
	// ok if the weapon is exactly the SAME, we should switch the weapon again, store z value to prevent infinite looping
	//%fullitemname = GetFullItemName(%weapon);
	//%nextweapon = %fullitemname;
	%loop = 0;
	%i = %n;
	%pre = %wpre;
	%suf = %wsuf;
	%itemno = countwords(%wlist);
	
	while( %loop <= %itemno)
	{
		
		if(%data $= "prev")
		{
			%i = %i - 1;
			if(%i < 0) %i = %itemno;
		}
		else
		{
			%i = %i + 1;
			if(%i >= %itemno) %i = 0;
			
		}
		%loop++;
		%w = GetWord(%wlist, %i);
		
		
		//ok we have our new item, check if its a weapon...
		%w = strreplace(%w, "%", " ");
		%nitem =  GetWord(%w, 1);
		%npre = GetWord(%w, 0);
		%nsuf = GetWord(%w, 2);
		%full = %game.GetFullItemName(%npre, %nitem, %nsuf);
		%nid = %client.data.count[strreplace(%full, " ", "x")];
		
		if($ItemType[%nitem] $= "Weapon" && skillcanuse(%client, %nitem))
		{
			%nw = %nid;
			break;
		}
	}

	
	if(%nw !$= "")
		serverCmdInventoryUse(%client, %nw);
	commandToClient(%this.client, 'setReticle', "gui/ret_blaster" , true);//bugfix
	return;
}
function RPGGame::InventoryUse(%game, %client, %itemid)
{
	if($debugmode) echo("RPGGAME::InventoryUse(" SPC %game SPC %client SPC %itemId SPC ")");
	//Server-side
	
	%player = %client.player;
	%item = Game.GetItem(%client, %itemId);
	
	%data = Game.GetModelName(%item);
	
	%prefix = %client.data.prefix[%itemid];
	%suffix = %client.data.suffix[%itemid];
	%fullitem = Game.GetFullItemName(%prefix, %item, %suffix);
	if(%game.getItemCount(%client, %item, %prefix, %suffix) >= 1)
	{
		if($ItemType[%item] $= "weapon")
		{
			if(skillcanuse(%client, %item) || fetchData(%client, "weaponInHand") $= %itemid)
			{
				if(fetchData(%client, "weaponInHand") !$= %itemid)
				{
					%player.unmountImage($WeaponSlot);
					%client.data.equipped[fetchdata(%client, "weaponInHand")] = false;
					//$InvInfo[fetchData(%client, "weaponInHand"), equipped] = false;
					//RemoveFromEquipList(%client, fetchdata(%client, "weaponinhand"));
					//storeData(%client, "weaponInHand", "");
					//AddToEquipList(%client, %itemid);
					%player.mountImage(%data.image, $WeaponSlot);
					//$InvInfo[%itemId, equipped] = true;
					%client.data.equipped[%itemid] = true;

					storeData(%client, "weaponInHand", %itemId);

					if(%client.isAiControlled())
					{

						%client.setWeaponInfo("BasicShocker", 0.05, %game.getRange(%item), 1);

					}
				}
				else
				{
					%player.unmountImage($WeaponSlot);
					//$InvInfo[fetchData(%client, "weaponInHand"), equipped] = false;
					//RemoveFromEquipList(%client, fetchdata(%client, "weaponinhand"));
					storeData(%client, "weaponInHand", "");
					%client.data.equipped[%itemid] = false;
					//AddToEquipList(%client, %itemid);
				}



				serverCmdInventoryListOnSelect(%client, %itemId);
			}
		}
		else if($ItemType[%item] $= "armor")
		{


			%itemex = fetchdata(%client, "ArmorIn" @ %game.GetLocation($ItemSubType[%item]));
			if(Game.SkillCanUse(%client, %item))
			{
				if( %itemex !$= %itemid)
				{
					//messageClient(%client, 'InventoryUse', $MsgBeige @ "You equipped " @ GetFullItemName(%itemId) @ ".");
					%client.data.equipped[%itemId] = true;
					%client.data.equipped[%itemex] = false;
					storedata(%client, "ArmorIn" @ %game.GetLocation($ItemSubType[%item]), %itemid);
					//AddToEquipList(%client, %itemid);
					messageClient(%client, 'InventoryUse', "You equipped" SPC %fullitem @ ",");
				}
				else
				{
					%client.data.equipped[%itemid] = false;
					storedata(%client, "ArmorIn" @ %game.GetLocation($itemSubType[%item]), "");
					messageClient(%client, 'InventoryUse', "You unequipped" SPC %fullitem @ ",");
				}
			}
			else
				messageClient(%client, 'InventoryUse', $MsgRed @ "You can't equip this item because you lack the necessary skills.");

			WeightStep(%client, false);
			serverCmdInventoryListOnSelect(%client, %itemId);
		}
		else if($itemType[%item] $= "potion" || $itemType[%item] $= "food")
		{
			Game.UsePotion(%client, %itemId);
		}
	}
	commandToClient(%client, 'SetCoins', fetchdata(%client, "COINS"));
	RefreshAll(%client);

}

//Item glow
datablock ItemData(ItemLightGlow) {
	shapeFile	= "turret_muzzlepoint.dts";
	hasLight	= true;
	lightType	= "ConstantLight";
	lightColor	= "1.0 1.0 1.0 1.0";  //change as needed
	lightTime	= "1000";
	lightRadius	= "2";
    cannotpickup = true;
};

datablock ParticleData(ItemParticle)
{
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.15;   // rises slowly
	inheritedVelFactor   = 0.0;
	windCoefficient = 0.0;

	lifetimeMS           = 1200;  // lasts 2 second
	lifetimeVarianceMS   = 0;   // ...more or less

	textureName          = "special/Explosion/exp_0036";

	//useInvAlpha = true;
	spinRandomMin = -300.0;
	spinRandomMax = 300.0;

	constantAcceleration = 0;

	colors[0]     = "1.0 0.8 0.3 0.0";
	colors[1]     = "1.0 0.8 0.3 0.5";
	colors[2]     = "1.0 0.8 0.3 0.5";
	colors[3]     = "1.0 0.8 0.3 0.0";

	sizes[0]      = 0.1;
	sizes[1]      = 0.15;
	sizes[2]      = 0.2;
	sizes[3]      = 0.25;

	times[0]      = 0.0;
	times[1]      = 0.33;
	times[2]      = 0.66;
	times[3]      = 1.0;
};

datablock ParticleEmitterData(ItemParticleEmitter)
{
	ejectionPeriodMS = 30;
	periodVarianceMS = 0;

	ejectionOffset = 0.4;

	ejectionVelocity = 0;
	velocityVariance = 0;

	thetaMin         = 0.0;
	thetaMax         = 180.0;

	phiReferenceVel  = 0.0;
	phiVariance      = 360.0;

	particles = "ItemParticle";
};

function DoItemGlowEffect(%item, %LE) {
	if(!isObject(%item)) {
		if(isObject(%LE)) {
			%LE.delete();
		}
		return;
	}
	if(isObject(%item.lightEffect))
		%item.lightEffect.delete();
	%item.lightEffect = new ParticleEmissionDummy()
	{
		position = VectorAdd(%item.getTransform(), "0 0 0.1");
		scale = "1 1 1";
		dataBlock = defaultEmissionDummy;
		emitter = ItemParticleEmitter;
		velocity = "1";
	};
	MissionCleanup.add(%item.lightEffect);
	schedule(75, 0, "DoItemGlowEffect", %item, %item.lightEffect);
}
