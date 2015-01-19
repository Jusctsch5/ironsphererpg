
function GetRaceZoneString(%clientEntering, %zoneEntered)
{
	%clraceId = $RaceID[fetchData(%clientEntering, "RACE")];
	%zoneraceId = %zoneEntered.RaceID;

	for(%cnt = 1; $RaceZoneStrings[%clraceId, %zoneraceId, %cnt] !$= ""; %cnt++){}

	%r = mfloor(getRandom() * (%cnt-1)) + 1;
	%s = $RaceZoneStrings[%clraceId, %zoneraceId, %r];

	return %s;
}
//---------------------------------------------------------------------------------------
function IsSameRace(%id1, %id2)
{
	if($RaceID[fetchData(%id1, "RACE")] $= $RaceID[fetchData(%id2, "RACE")])
		return true;
	else
		return false;
}


if($rules !$= "dm")
{
	DefineLoadouts();
	DefineZoneSpecifications(); //each map has a cs file....
}
else
{
$RaceDescription[1] = "Death Knight";
$RaceDescription[2] = "Human";
$RaceDescription[3] = "Orc";
$RaceDescription[4] = "Elf";
$RaceDescription[5] = "Goblin";
$RaceDescription[6] = "Gnoll";
$RaceDescription[7] = "Ogre";
$RaceDescription[8] = "Wild Orc";
$RaceDescription[9] = "Undead";
$RaceDescription[10] = "Minotaur";

$RaceID[DeathKnight] = 1;
$RaceID[MaleHuman] = 2;
$RaceID[FemaleHuman] = 2;
$RaceID[MaleOrc] = 3;
$RaceID[FemaleOrc] = 3;
$RaceID[MaleElf] = 4;
$RaceID[FemaleElf] = 4;
$RaceID[Goblin] = 5;
$RaceID[GoblinShaman] = 5;
$RaceID[Gnoll] = 6;
$RaceID[GnollShaman] = 6;
$RaceID[Ogre] = 7;
$RaceID[BigOrc] = 8;
$RaceID[Skeleton] = 9;
$RaceID[Zombie] = 9;
$RaceID[Vampire] = 9;
$RaceID[Ghoul] = 9;
$RaceID[Lich] = 9;
$RaceID[Wraith] = 9;
$RaceID[Minotaur] = 10;

$MinHP[DeathKnight] = 5000;
$MinHP[MaleHuman] = 12;
$MinHP[FemaleHuman] = 11;
$MinHP[MaleOrc] = 13;
$MinHP[FemaleOrc] = 12;
$MinHP[MaleElf] = 10;
$MinHP[FemaleElf] = 10;
$MinHP[Goblin] = 0;
$MinHP[GoblinShaman] = 5;
$MinHP[Gnoll] = 3;
$MinHP[GnollShaman] = 8;
$MinHP[Ogre] = 10;
$MinHP[BigOrc] = 16;
$MinHP[Skeleton] = 13;
$MinHP[Zombie] = 14;
$MinHP[Vampire] = 15;
$MinHP[Ghoul] = 15;
$MinHP[Lich] = 18;
$MinHP[Wraith] = 24;
$MinHP[Minotaur] = 35;

$PlayableRace[1] = true;
$PlayableRace[2] = true;
$PlayableRace[3] = true;
$PlayableRace[4] = false;
$PlayableRace[5] = false;
$PlayableRace[6] = false;
$PlayableRace[7] = false;
$PlayableRace[8] = false;
$PlayableRace[9] = false;
$PlayableRace[10] = false;



//---------------------------------------------------------------------------------------
//	RaceZoneStrings[Client entering's RaceID, Zone being entered's RaceID, n]

//DeathKnight entering zones
$RaceZoneStrings[1, 1, 1] = "This area is DeathKnight territory.";
$RaceZoneStrings[1, 2, 1] = "This area is Human territory.";
$RaceZoneStrings[1, 3, 1] = "This area is Orc territory.";
$RaceZoneStrings[1, 4, 1] = "This area is Elven territory.";
$RaceZoneStrings[1, 5, 1] = "This area is Goblin territory.";
$RaceZoneStrings[1, 6, 1] = "This area is Gnoll territory.";
$RaceZoneStrings[1, 7, 1] = "This area is Ogre territory.";
$RaceZoneStrings[1, 8, 1] = "This area is Wild Orc territory.";
$RaceZoneStrings[1, 9, 1] = "This area is Undead territory.";
$RaceZoneStrings[1, 10, 1] = "This area is Minotaur territory.";

//Human entering zones
$RaceZoneStrings[2, 1, 1] = "In this area dwell much higher powers.";
$RaceZoneStrings[2, 2, 1] = "This area feels like home.";
$RaceZoneStrings[2, 3, 1] = "This area is Orcish in nature.";
$RaceZoneStrings[2, 3, 2] = "This area is infested with Orcs.";
$RaceZoneStrings[2, 4, 1] = "Elves appear to inhabit this area.";
$RaceZoneStrings[2, 5, 1] = "This place smells of goblins.";
$RaceZoneStrings[2, 6, 1] = "It appears many gnolls wander here.";
$RaceZoneStrings[2, 7, 1] = "You can hear the sounds of hungry ogres.";
$RaceZoneStrings[2, 8, 1] = "This area is Orcish in nature, but the smell is intolerable.";
$RaceZoneStrings[2, 9, 1] = "The inhabitants of this area are void of life.";
$RaceZoneStrings[2, 10, 1] = "Here dwell the dangerous minotaur.";

//Orc entering zones
$RaceZoneStrings[3, 1, 1] = "This area be too dangerous for Orc!";
$RaceZoneStrings[3, 2, 1] = "This area smell human!";
$RaceZoneStrings[3, 3, 1] = "This area home for Orc!";
$RaceZoneStrings[3, 4, 1] = "You smell puny elves!";
$RaceZoneStrings[3, 5, 1] = "Little goblins everywhere for you to crush!";
$RaceZoneStrings[3, 6, 1] = "Gnolls here must die!";
$RaceZoneStrings[3, 7, 1] = "This area smell much Ogres...";
$RaceZoneStrings[3, 8, 1] = "Here live exiled brothers of Orcs!";
$RaceZoneStrings[3, 9, 1] = "This area death place.";
$RaceZoneStrings[3, 9, 2] = "This area death place. Brawwwr!";
$RaceZoneStrings[3, 10, 1] = "Minotaur dangerous here!";

   $EnemyProfile[Runt] = "RACE Orc LVL 1r1";
   $EnemyProfile[Thief] = "RACE Orc LVL 2r2";
   $EnemyProfile[Raider] = "RACE Orc LVL 3r5";
   $EnemyProfile[Wizard] = "RACE Orc LVL 4r8";
   
   $SpawnIndex[1] = "Runt";
   $SpawnIndex[2] = "Thief";
   $SpawnIndex[3] = "Raider";
   $SpawnIndex[4] = "Wizard";

}