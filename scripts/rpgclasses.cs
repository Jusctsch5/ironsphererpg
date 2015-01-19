//There are FOUR hard-coded groups:
//-Priest
//-Rogue
//-Warrior
//-Wizard

//Each of these has classes.  They are specified in here.
//Anything that does NOT have to do with visuals when it comes to classes should ALWAYS use the 0 offset in $ClassName.

$initcoins[Priest] = "3d6x10";
$initcoins[Rogue] = "2d6x10";
$initcoins[Warrior] = "5d4x10";
$initcoins[Wizard] = "1d4+1x10";

$group[0] = "Priest";
$group[1] = "Rogue";
$group[2] = "Warrior";
$group[3] = "Wizard";

$class[0, 0] = "Cleric";
$class[0, 1] = "Druid";
$class[1, 0] = "Thief";
$class[1, 1] = "Bard";
$class[2, 0] = "Fighter";
$class[2, 1] = "Ranger";
$class[2, 2] = "Paladin";
$class[3, 0] = "Mage";
$class[3, 1] = "Conjurer";


function getFinalCLASS(%client)
{
	for(%i = 1; $Class[%i, 0] !$= ""; %i++)
	{
		if(stricmp($Class[%i, 0], fetchData(%client, "CLASS")) $= 0)
			return $Class[%i, 0];
	}
	return -1;
}

function IsAClass(%class)
{
	for(%i = 1; $Class[%i, 0] !$= ""; %i++)
	{
		if(stricmp(%class, $Class[%i, 0]) $= 0)
			return true;
	}

	return false;
}