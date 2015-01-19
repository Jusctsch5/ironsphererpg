// ##################################### //
// ##################################### //
//          Enemy Spawn Points           //
// ##################################### //
// ##################################### //


function DefineAISpawn()
{
   //createAISpawn(%pos, %box, %mindelay, %maxdelay, %botlist, %team, %attackId);
   //%botlist contains a list seperated by spaces of the $spawnindex values defined below
   //ex createAISpawn("-1006.14 706.277 64.804", "0 0 0", 1, 10, "1 2 3",1,0);
    //createAISpawn("-1006.14 706.277 64.804", "0 0 0", 1, 10, "1 1 1 1 2 2 3",5,0);
    //createAISpawn("-1006 706 64.804", "0 0 0", 5, 20, "1 1 1 1 2 2 3",5,0);

// Ethren Attack Army
      //  $AI::AttackPos[1,0] = "256.131 -249 53.3192";
	//removed
	
// Mine Spawn points!
//things in () means spawn index #, EX
//(7-9) means spawnindex values 7,8,9
	//$SpawnIndex[7] 		= "Pup";
	//$SpawnIndex[8] 		= "Scavenger";
	//$SpawnIndex[9] 		= "Hunter";

	//goblins (1-4)
	createAIspawn("-1083 596 119", "0 0 0", 15, 25, "1", 1, 0);
	createAIspawn("-1075.57 630.961 119", "0 0 0", 15, 25, "1", 1, 0);

	createAIspawn("-1138.26 585.48 139.881", "0 0 0", 15, 25, "1", 1, 0);
	createAIspawn("-1145.42 501.896 139.881", "0 0 0", 15, 25, "1", 1, 0);
	createAIspawn("-1184.18 542.456 139.881", "0 0 0", 15, 25, "1", 1, 0);
	createAIspawn("-1093.31 651.489 139.881", "0 0 0", 15, 25, "1", 1, 0);

	createAIspawn("-1100.48 514.105 120.34", "0 0 0 ", 15, 25, "1", 1, 0);  //pit under bridge
	createAIspawn("-1025.38 511.41 114.677", "0 0 0", 15, 25, "1", 1, 0);
	createAIspawn("-1059.84 508.23 94.0157", "0 0 0", 15, 25, "1", 1, 0);
 //
    //createAIspawn("-1083 596 119", "0 0 0", 10, 20, "1 2 3", 1, 0);
	//createAIspawn("-1075.57 630.961 119", "0 0 0", 10, 20, "1 2 3 4", 1, 0);

	//createAIspawn("-1138.26 585.48 139.881", "0 0 0", 10, 20, "1", 1, 0);
	//createAIspawn("-1145.42 501.896 139.881", "0 0 0", 10, 20, "1", 1, 0);
	//createAIspawn("-1184.18 542.456 139.881", "0 0 0", 10, 20, "1 1 2", 1, 0);
	//createAIspawn("-1093.31 651.489 139.881", "0 0 0", 10, 20, "1 1 2", 1, 0);

	//createAIspawn("-1100.48 514.105 120.34", "0 0 0 ", 10, 20, "1 2 3", 1, 0);  //pit under bridge
	//createAIspawn("-1025.38 511.41 114.677", "0 0 0", 10, 20, "1 2 3", 1, 0);
	//createAIspawn("-1059.84 508.23 94.0157", "0 0 0", 10, 20, "1 2", 1, 0);
 //

	//Gnolls (7-9, 22)

        //Large Equipment room
	createAIspawn("-1172.1 566.279 92.3973", "0 0 0", 5, 30, "7 8 22", 1, 0);
	createAIspawn("-1174.1 568.279 92.3973", "0 0 0", 5, 30, "7 8 9", 1, 0);
        //Yellow Room
	//createAIspawn("-1031.06 686.976 121.668", "0 0 0", 5, 20, "7", 1, 0);
	createAIspawn("-1034.06 686.976 121.668", "0 0 0", 5, 20, "7 8 9", 1, 0);
	createAIspawn("-1031.06 690.976 121.668", "0 0 0", 5, 20, "7 8 22", 1, 0);
 
 //bots in morgue
 createAIspawn("-1146.3 -2253.63 -315.056", "0 0 0", 5, 20, "34 35", 13, 0);
 
 //createAI

// Conscripts (5)

	$AI::AttackPos[1,0] = "-362.205 956.154 51.3047"; //keldrin
	createAISpawn("-362.205 956.154 55.3047", "0 0 0", 5, 10, "5", 1,0); //keldrin

	$AI::AttackPos[2,0] = "250.859 -249.733 53.4917"; //ethren
	createAISpawn("250.859 -249.733 53.4917", "0 0 0", 5, 10, "5",2,0); //ethren

    $AI::AttackPos[3,0] = "-1509.56 1609.42 50.0549"; //delkin
    createAISpawn("-1509.56 1609.42 50.0549","0 0 0", 5, 10,"5",3,0); //delkin

//JATEN OUTPOST
    $AI::AttackPos[4,0] = "-2770.45 1268.68 55.9228"; //jaten1
    createAISpawn("-2770.45 1268.68 55.9228","0 0 0", 5, 10,"5",4,0); //jaten1
	$AI::AttackPos[5,0] = "-2812.39 1392.54 63.5626"; //jaten2
	createAISpawn("-2812.39 1392.54 63.5626", "0 0 0", 5, 10, "5",5,0); //jaten2

//BALAN VILLAGE
    $AI::AttackPos[6,0] = "-1025.26 -2288.9 185.007"; //balan1
	createAISpawn("-1025.26 -2288.9 185.007", "0 0 0", 5, 10, "5",6,0); //balan1
    $AI::AttackPos[7,0] = "-1216.8 -2112.94 166.846"; //balan2
	createAISpawn("-1216.8 -2112.94 166.846", "0 0 0", 5, 10, "5",7,0); //balan2
    $AI::AttackPos[8,0] = "-1011.56 -2128.9 176.087"; //balan3
	createAISpawn("-1011.56 -2128.9 176.087", "0 0 0", 5, 10, "5",8,0); //balan3


// Fish Spawn points (6) DISABLED, LAGGY
	//createAISpawn("-592 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-590 857 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-594 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-598 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-600 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-602 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-604 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-606 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-608 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-610 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-612 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-614 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-616 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-618 855 10", "1 1 10", 50, 200, "6",1,0);
	//createAISpawn("-620 855 10", "1 1 10", 50, 200, "6",1,0);
	
//things in [] means raceID,then define a number for this attackpos EX
//[11,2]
//[orcs,this is the 2nd attackpos for this raceid]

// Yolanda ( 11-13, 26)
	//Orcs! Beware
	$AI::AttackPos[7,0] = "-1091.74 604.728 321.142";
    $AI::AttackPos[7,1] = "-1147.03 565.174 321.142";
    $AI::AttackPos[7,2] = "-1119.08 589.043 295.76"; //new
    $AI::AttackPos[7,3] = "-1094.18 561.766 299.24"; //new middle
  
	createAISpawn("-1091.74 604.728 321.142", "0 0 0", 5, 20, "11 12",5,7);//top
	createAISpawn("-1147.03 565.174 321.142", "0 0 0", 5, 20, "11 12",5,7);	//top
    createAISpawn("-1094.18 561.766 299.24", "0 0 0", 5,20, "13",5,7);// middle
    createAISpawn("-1119.08 589.043 295.76", "0 0 0", 5, 20, "26",5,7);//new
//ogre (28-31)
	$Ai::AttackPos[11, 0] = "-2661.32 404.086 60.9192";
	$Ai::AttackPos[11, 1] = "-2643.2 343.718 67.7022";
	createAISpawn("-2661.32 404.086 60.9192", "0 0 0", 10, 30, "28 29 30", 7, 11);
	createAISpawn("-2643.2 343.718 67.7022", "0 0 0", 12, 33, "28 29 30 31", 7, 11);

// Elementals (10)
	$AI::AttackPos[5,0] = "698.363 929.536 145.917";
	$AI::AttackPos[6,0] = "727.324 984.623 157.157";
	createAISpawn("698.363 929.536 145.917", "0 0 0", 5, 20, "10", 7, 5); // me
	createAISpawn("727.324 984.623 157.157", "0 0 0", 5, 20, "10", 7, 6);
//undead/skel (32-35)
	createAISpawn("-839.313 -1003.23 56.9435", "0 0 0", 10, 20, "32 33", 9, 0);
	createAISpawn("-848.6 -1006.72 56.9435", "0 0 0", 10, 20, "32 33", 9, 0);
	createAISpawn("-845.956 -986.872 49.4435", "0 0 0", 10, 20, "34 35", 9, 0);

//Elf (15-18, 23)
	
	$AI::AttackPos[8,0] = "-1082 2128 120";
	//$AI::AttackPos[8,1] = "-1095 2209 134";
	$AI::AttackPos[8,2] = "-1044 2163 124";
	createAISpawn("-1082 2128 120", "0 0 0", 5, 20, "15 16 17 18", 3, 8);
	//createAISpawn("-1095 2209 134", "0 0 0", 5, 20, "15 16", 3, 8);
	createAISpawn("-1044 2163 124", "0 0 0", 5, 20, "15 16 17 23", 3, 8);

//Travellers (19-21, 24)
	//$AI::AttackPos[10,0] = "1559 -48 125";
	$AI::AttackPos[10,1] = "1571 -170 125";
	$AI::AttackPos[10,2] = "1474 -226 115";
	$AI::AttackPos[10,3] = "1348 -50 105";
	//createAISpawn("1559 -48 125", "0 0 0", 5, 20, "19 20 21", 4, 10);
	createAISpawn("1571 -170 125", "0 0 0", 5, 20, "19 20 21", 4, 10);
	createAISpawn("1474 -226 115", "0 0 0", 5, 20, "19 20 21", 4, 10);
	createAISpawn("1349 -177 105", "0 0 0", 5, 20, "19 20 21 24", 4, 10);
	createAISpawn("1348 -50 105", "0 0 0", 5, 20, "19 20 21 24", 4, 10);


//minotuar!!!! (14, 25)

	createAISpawn("504.963 1415.49 65", "0 0 0", 20, 30, "14", 5, 0);
	createAISpawn("504.963 1415.49 70", "0 0 0", 20, 30, "14 25", 5, 0);

// Random World Spawns

// Enclave in the mountain between Keldrin and Ethren
createAIspawn("479 143 215", "0 0 0", 10, 20, "34 35", 9, 0);

// Mountain area behind the guild hall
createAIspawn("566 -221 190", "0 0 0", 10, 20, "1 2 3 4", 1, 0);
createAIspawn("562 -209 190", "0 0 0", 10, 20, "1 2 3 4", 1, 0);
createAIspawn("551 -236 190", "0 0 0", 10, 20, "1 2 3 4", 1, 0);

// Mountainside by Ethren near Arena
createAIspawn("198 -82 120", "0 0 0", 10, 20, "1 2 3 4", 1, 0);
createAIspawn("269 -47 120", "0 0 0", 10, 20, "1 2 3 4", 1, 0);
createAIspawn("252 -87 120", "0 0 0", 10, 20, "1 2 3 4", 1, 0);

//Keldrin <-> Ethren path
createAIspawn("-83 238 60", "0 0 0", 10, 20, "1 2", 1, 0);
createAIspawn("-75 224 60", "0 0 0", 10, 20, "1 2", 1, 0);

// Plain by the Keldrin Mines
createAIspawn("-515 308 80", "0 0 0", 10, 20, "1 2", 1, 0);
createAIspawn("-573 305 80", "0 0 0", 10, 20, "1 2 3", 1, 0);
createAIspawn("-549 368 80", "0 0 0", 10, 20, "1 2 3", 1, 0);
createAIspawn("-523 362 80", "0 0 0", 10, 20, "1 2", 1, 0);
createAIspawn("-492 405 80", "0 0 0", 10, 20, "1 2", 1, 0);
createAIspawn("-598 365 80", "0 0 0", 10, 20, "1 2", 1, 0);
createAIspawn("-553 451 80", "0 0 0", 10, 20, "1 2", 1, 0);

// Kinda behind the arenaish
createAIspawn("162 347 60", "0 0 0", 10, 20, "28 29 30 31", 7, 0);

	customAiSpawn();
}
function DefineLoadouts()
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
	$RaceDescription[11] = "Fish";
	$RaceDescription[12] = "Elemental";
	$RaceDescription[13] = "Dark Human";
	
	$RaceSkin[1] = "";//if "" it will be default, if specified the skin will be used.
	$RaceSkin[2] = "";
	$RaceSkin[3] = "";
	$RaceSkin[4] = "Elf_AM_skin";
	$RaceSkin[5] = "";
	$RaceSkin[6] = "";
	$RaceSkin[7] = "";
	$RaceSkin[8] = "";
	$RaceSkin[9] = "";
	$RaceSkin[10] = "";
	$RaceSkin[11] = "";
	$RaceSkin[12] = "";
	$RaceSkin[13] = "";
	
	
	
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
	$RaceID[Fish] = 11;
	$RaceID[Elemental] = 12;
	$RaceID[Orc] = 3;
	$RaceID[DarkHuman] = 13;

//hp values of races.
	$MinHP[DeathKnight] = 5000;
	$MinHP[MaleHuman] = 15;
	$MinHP[FemaleHuman] = 15;
	$MinHP[MaleOrc] = 13;
	$MinHP[FemaleOrc] = 13;
	$MinHP[MaleElf] = 10;
	$MinHP[FemaleElf] = 10;
	$MinHP[Goblin] = 2;
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
	$MinHP[Fish] = 1;
	$MinHP[Ghost] = 100;
	$MinHP[Orc] = 2;
	$MinHP[DarkHuman] = 12;

	$PlayableRace[1] = true;
	$PlayableRace[2] = true;
	$PlayableRace[3] = false;//this is orcs;in other versions we may set this to true..
	$PlayableRace[4] = false;
	$PlayableRace[5] = false;
	$PlayableRace[6] = false;
	$PlayableRace[7] = false;
	$PlayableRace[8] = false;
	$PlayableRace[9] = false;
	$PlayableRace[10] = false;
	$PlayableRace[11] = false;
	$PlayableRace[12] = false;
	$PlayableRace[13] = false;

        //RACE [racename] LVL [XrY] CLASS [classname]  ex RACE Orc LVL 1r1 CLASS fighter

// r = diceroll, EX
//6r11 means roll from 6 to 11 ,  the results are >=6&&<=11  meaning greater than or equal to 6,AND less than or equal to 11

// Humans   
	$EnemyProfile[Conscript]    = "LVL 80r100 CLASS Fighter COINS 800r1000";
// Fish
	$EnemyProfile[Fish]         = "LVL 1r1 CLASS Fighter";
// Goblins
	$EnemyProfile[Runt]         = "LVL 1r1 CLASS Fighter COINS 10r70";
	$EnemyProfile[Thief]        = "LVL 2r3 CLASS Fighter COINS 70r140";
	$EnemyProfile[Raider]       = "LVL 3r5 CLASS Fighter COINS 80r160";
	$EnemyProfile[Wizard]       = "LVL 5r8 CLASS Mage COINS 150r240";
// Gnolls
	$EnemyProfile[Pup]          = "LVL 8r11 CLASS Fighter COINS 80r110";
	$EnemyProfile[Scavenger]    = "LVL 11r13 CLASS Fighter COINS 110r113";
	$EnemyProfile[Hunter]       = "LVL 12r15 CLASS Ranger COINS 120r150";
	$EnemyProfile[Shaman]       = "LVL 14r18 CLASS Mage COINS 140r180";
// Elemental
    $EnemyProfile[Ghost]        = "LVL 50r60 Class Fighter COINS 500r600";
// Orcs
	$EnemyProfile[Berserker]    = "LVL 18r21 Class Fighter COINS 180r210"; //11
	$EnemyProfile[Ravager]      = "LVL 19r22 Class Fighter COINS 190r220"; //12
	$EnemyProfile[Slayer]       = "LVL 25r29 Class Fighter COINS 250r290"; //13
	$EnemyProfile[WarLock]      = "LVL 27r31 Class Mage COINS 270r310"; //26
// Minotaur
	$EnemyProfile[Guardian]     = "LVL 90r99 CLASS Fighter COINS 900r990";
	$EnemyProfile[Reaper]	    = "LVL 95r101 CLASS Mage COINS 950r1010";
//Elf
	$EnemyProfile[Protector]    = "LVL 45r55 CLASS Fighter COINS 450r550";
	$EnemyProfile[Peacekeeper]  = "LVL 48r58 CLASS Fighter Coins 480r580";
	$EnemyProfile[Lord]		    = "LVL 50r60 CLASS Fighter Coins 500r600";
	$EnemyProfile[Champion]		= "LVL 52r62 CLASS Fighter Coins 520r620";
	$EnemyProfile[Conjurer]		= "LVL 53r63 CLASS Mage COINS 530r630";

//travellers
	$EnemyProfile[Birgand]		= "LVL 71r79 CLASS Fighter COINS 710r790";
	$EnemyProfile[Marauder]		= "LVL 76r83 CLASS Fighter COINS 760r830";
	$EnemyProfile[Knight]		= "LVL 79r85 CLASS Fighter COINS 790r850";
	$EnemyProfile[Paladin]		= "LVL 81r89 CLASS Mage COINS 810r890";

//ogres
	$EnemyProfile[Ruffian]		= "LVL 30r34 CLASS Fighter COINS 300r340"; //27 
	$EnemyProfile[Destroyer]	= "LVL 31r35 CLASS Fighter COINS 310r350"; //28
	$EnemyProfile[Halberdier]	= "LVL 34r37 CLASS Fighter COINS 340r370"; //29
	$EnemyProfile[Dreadnought]	= "LVL 36r40 CLASS Fighter COINS 360r400"; //30
	$EnemyProfile[Magi]		    = "LVL 38r43 CLASS Mage COINS 380r430"; //31
//undead
	$EnemyProfile[Mauler]		= "LVL 59r65 CLASS Fighter COINS 590r650";
	$EnemyProfile[Thrasher]		= "LVL 61r67 CLASS Fighter COINS 610r670";
//walking skeletons
	$EnemyProfile[Skeleton]		= "LVL 1r4 CLASS Fighter COINS 300r510";
	$EnemyProfile[Necromancer]	= "LVL 3r6 CLASS Fighter COINS 485r625";



	$aiRace[Runt] 		= "Goblin";
	$aiRace[Thief] 		= "Goblin";
	$aiRace[Raider] 	= "Goblin";
	$aiRace[Wizard] 	= "Goblin";
	$aiRace[Conscript] 	= "MaleHuman";
	$aiRace[Fish] 		= "Fish";
	$aiRace[Pup] 		= "Gnoll";
	$aiRace[Scavenger] 	= "Gnoll";
	$aiRace[Hunter] 	= "Gnoll";
	$aiRace[Ghost] 	    = "Elemental";
	$aiRace[Berserker] 	= "Orc";
	$aiRace[Ravager] 	= "Orc";
	$aiRace[Slayer] 	= "Orc";
	$aiRace[Warlock]	= "Orc";
	$aiRace[Guardian] 	= "Minotaur";
	$aiRace[Protector] 	= "MaleElf";
	$aiRace[Peacekeeper]= "MaleElf";
	$aiRace[Lord] 		= "MaleElf";
	$aiRace[Champion] 	= "MaleElf";
	$aiRace[Birgand]  	= "EvilHuman";
	$aiRace[Marauder] 	= "EvilHuman";
	$aiRace[Knight]		= "EvilHuman";
	$aiRace[Shaman]		= "Gnoll";
	$aiRace[Conjurer]	= "MaleElf";
	$aiRace[Paladin]	= "EvilHuman";
	$aiRace[Reaper]		= "Minotaur";
	$aiRace[Ruffian]	= "Ogre";
	$aiRace[Destroyer]	= "Ogre";
	$aiRace[Halberdier]	= "Ogre";
	$aiRace[Dreadnought]= "Ogre";
	$aiRace[Magi]		= "Ogre";
	$aiRace[Mauler]		= "Zombie";
	$aiRace[Thrasher]	= "Zombie";
	$aiRace[Skeleton]	= "Skeleton";
	$aiRace[Necromancer]= "Skeleton";
	$aiRace[Twister]  	= "EvilHuman";
	$aiRace[Seeker] 	= "EvilHuman";
	$aiRace[Rapier]		= "EvilHuman";

//spawnindexes

	$SpawnIndex[1] 		= "Runt";       //goblin
	$SpawnIndex[2] 		= "Thief";      //goblin
	$SpawnIndex[3] 		= "Raider";     //goblin
	$SpawnIndex[4] 		= "Wizard";     //goblin
	$SpawnIndex[5] 		= "Conscript";  //Malehuman
	$SpawnIndex[6] 		= "Fish";       //Fish
	$SpawnIndex[7] 		= "Pup";        //Gnoll
	$SpawnIndex[8] 		= "Scavenger";  //Gnoll
	$SpawnIndex[9] 		= "Hunter";     //Gnoll
    $SpawnIndex[10] 	= "Ghost";      //Elemental
    $spawnIndex[11] 	= "Berserker";  //Orc
    $spawnIndex[12] 	= "Ravager";    //Orc
    $SpawnIndex[13] 	= "Slayer";     //Orc
    $SpawnIndex[14] 	= "Guardian";   //Minotaur
    $SpawnIndex[15] 	= "Protector";  //MaleElf
    $SpawnIndex[16] 	= "Lord";       //MaleElf
    $SpawnIndex[17] 	= "Peacekeeper";//MaleElf
    $SpawnIndex[18] 	= "Champion";   //MaleElf
    $SpawnIndex[19] 	= "Birgand";    //EvilHuman
    $SpawnIndex[20]		= "Marauder";   //EvilHuman
    $SpawnIndex[21]		= "Knight";     //EvilHuman
    $SpawnIndex[22]		= "Shaman";     //Gnoll
    $SpawnIndex[23]		= "Conjurer";   //MaleElf
	$SpawnIndex[24] 	= "Paladin";    //EvilHuman
	$SpawnIndex[25]		= "Reaper";     //Minotaur
	$SpawnIndex[26] 	= "Warlock";    //Orc
	$SpawnIndex[27] 	= "Ruffian";    //Ogre
	$SpawnIndex[28]		= "Destroyer";  //Ogre
	$SpawnIndex[29]	 	= "Halberdier"; //Ogre
	$SpawnIndex[30]		= "Dreadnought";//Ogre
	$SpawnIndex[31] 	= "Magi";       //Ogre
	$SpawnIndex[32] 	= "Mauler";     //Zombie
	$SpawnIndex[33] 	= "Thrasher";   //Zombie
	$SpawnIndex[34]		= "Skeleton";   //Skeleton
	$SpawnIndex[35]		= "Necromancer";//Skeleton
	$SpawnIndex[36] 	= "Twister";    //SkyGuardian
	$SpawnIndex[37]		= "Seeker";     //SkyGuardian
	$SpawnIndex[38]		= "Rapier";     //SkyGuardian
	// "0 0 0", 5, 20, "19 20 21", 4, 10);
	//attack behaviors
	//0 is default
	//1 is only attack enemies in its own zone, or enemies that hit him
	$attb[Runt] 		= 0;
	$attb[Thief] 		= 0;
	$attb[Raider] 		= 0;
	$attb[Wizard] 		= 0;
	$attb[Conscript] 	= 1;
	$attb[Fish] 		= 0;
	$attb[Pup] 		    = 0;
	$attb[Scavenger] 	= 0;
	$attb[Hunter] 		= 0;
	$attb[Ghost] 	    = 0;
	$attb[Berserker] 	= 0;
	$attb[Ravager]	 	= 0;
	$attb[Slayer] 		= 0;
	$attb[warlock]		= 0;
	$attb[Guardian] 	= 0;
	$attb[Protector] 	= 0;
	$attb[Peacekeeper] 	= 0;
	$attb[Lord] 		= 0;
	$attb[Champion] 	= 0;
	$attb[Birgand]		= 0;
	$attb[Marauder]		= 0;
	$attb[Knight]		= 0;
	$attb[Shaman]		= 0;
	$attb[Conjurer]		= 0;
	$attb[Paladin]		= 0;
	$attb[Reaper]		= 0;
	$attb[Ruffian] 		= 0;
	$attb[Destroyer]	= 0;
	$attb[Halberdier]	= 0;
	$attb[Dreadnought]	= 0;
	$attb[Magi] 		= 0;
	$attb[Mauler]		= 0;
	$attb[Thrasher]		= 0;
	$attb[Skeleton]		= 0;
	$attb[Necromancer]	= 0;
	$attb[Twister]		= 0;
	$attb[Seeker]		= 0;
	$attb[Rapier]	    = 0;
	
	//uses magic!
	$Bot::Magic[Runt] 	    = 0;
	$Bot::Magic[Thief] 	    = 0;
	$Bot::Magic[Raider] 	= 0;
	$Bot::Magic[Wizard] 	= 1;
	$Bot::Magic[Conscript] 	= 0;
	$Bot::Magic[Fish] 	    = 0;
	$Bot::Magic[Pup] 	    = 0;
	$Bot::Magic[Scavenger] 	= 0;
	$Bot::Magic[Hunter] 	= 0;
	$Bot::Magic[Ghost] 	    = 0;
	$Bot::Magic[Berserker] 	= 0;
	$Bot::Magic[Ravager]	= 0;
	$Bot::Magic[Slayer] 	= 0;
	$Bot::Magic[Guardian] 	= 0;
	$Bot::Magic[Protector] 	= 0;
	$Bot::Magic[Peacekeeper]= 0;
	$Bot::Magic[Lord] 	    = 0;
	$Bot::Magic[Champion] 	= 0;
	$Bot::Magic[Birgand]	= 0;
	$Bot::Magic[Marauder]	= 0;
	$Bot::Magic[Knight]     = 0;
	$Bot::Magic[Shaman]	    = 1;
	$Bot::Magic[Conjurer]	= 1;
	$Bot::Magic[Paladin]	= 1;
	$Bot::Magic[Reaper] 	= 1;
	$Bot::Magic[Warlock]	= 1;
	$Bot::Magic[Ruffian] 	= 0;
	$Bot::Magic[Destroyer]	= 0;
	$Bot::Magic[Halberdier]	= 0;
	$Bot::Magic[Dreadnought]= 0;
	$Bot::Magic[Magi] 	    = 1;
	$Bot::Magic[Mauler]	    = 0;
	$Bot::Magic[Thrasher]	= 0;
	$Bot::Magic[Skeleton]	= 0;
	$Bot::Magic[Necromancer]= 1;
	$Bot::Magic[Twister]	= 0;
	$Bot::Magic[Seeker] 	= 0;
	$Bot::Magic[Rapier]     = 1;

//$ItemPrefix[weapon, 1] = "Broken";
//$ItemPrefix[weapon, 2] = "Worn";
//$ItemPrefix[weapon, 3] = "";
//$ItemPrefix[weapon, 4] = "Fine";
//$ItemPrefix[weapon, 5] = "Mighty";
//$ItemPrefix[weapon, 6] = "Powerful";

//$ItemPrefixBonus[weapon, 1] = 0.6;
//$ItemPrefixBonus[weapon, 2] = 0.8;
//$ItemPrefixBonus[weapon, 3] = 1.0;
//$ItemPrefixBonus[weapon, 4] = 1.2;
//$ItemPrefixBonus[weapon, 5] = 1.4;
//$ItemPrefixBonus[weapon, 6] = 1.6;

//$ItemSuffix[weapon, 1] = "";
//$ItemSuffix[weapon, 2] = "of Dexterity";
//$ItemSuffix[weapon, 3] = "of Strength";
//$ItemSuffix[weapon, 4] = "of Slaughter";
//$ItemSuffix[weapon, 5] = "of Dismay";
//$ItemSuffix[weapon, 6] = "of Swiftness";

//$ItemSuffixBonus[weapon, 1] = "";
//$ItemSuffixBonus[weapon, 2] = "101 1 102 1 103 1 104 1";
//$ItemSuffixBonus[weapon, 3] = "111 1 112 1";
//$ItemSuffixBonus[weapon, 4] = "101 2 102 2 103 2 104 1";
//$ItemSuffixBonus[weapon, 5] = "101 3 102 3 103 3";
//$ItemSuffixBonus[weapon, 6] = "9 -30";

//----------------------------------------

//$ItemPrefix[armor, 1] = "Old";
//$ItemPrefix[armor, 2] = "Weak";
//$ItemPrefix[armor, 3] = "";
//$ItemPrefix[armor, 4] = "Hardened";
//$ItemPrefix[armor, 5] = "Sturdy";
//$ItemPrefix[armor, 6] = "Stalwart";

//$ItemPrefixBonus[armor, 1] = -0.6;
//$ItemPrefixBonus[armor, 2] = -0.8;
//$ItemPrefixBonus[armor, 3] = 1.0;
//$ItemPrefixBonus[armor, 4] = 1.2;
//$ItemPrefixBonus[armor, 5] = 1.4;
//$ItemPrefixBonus[armor, 6] = 1.8;

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

// r = diceroll, EX
//6r11 means roll from 6 to 11 ,  the results are >=6&&<=11  meaning greater than or equal to 6,AND less than or equal to 11

	//Runt
        $EnemyWeapons[1] 	= "Knife 3 1";//first value item, then prefix then suffix 1 prefix is rusty,while 3 is normal
        $EnemyArmor[1] 		= "";//same type as weapons.... no randomness to actually having it...
        $EnemyItems[1] 		= "";// first value is how many second value is odds... for items the values must be a roll... 1r1 will always give an item...
	//Thief
	$EnemyWeapons[2] 	= "Knife 1r3 1 Sling 2 1";
	$EnemyArmor[2] 		= "";
	$EnemyItems[2] 		= "BluePotion 1r1 1r20 String 1r1 1r25 SmallRock 10r15 1r1";
	//Raider
	$EnemyWeapons[3] 	= "PickAxe 1r3 1";
	$EnemyArmor[3] 		= "";
	$EnemyItems[3] 		= "BluePotion 1r2 1r10 Rod 1r1 1r20";
	//Wizard
	$EnemyWeapons[4] 	= "Knife 1r3 1";
	$EnemyArmor[4] 		= "";
	$EnemyItems[4] 		= "BluePotion 1r1 1r2";
	//Conscript
	$EnemyWeapons[5] 	= "Spear 4 1r3";
	$EnemyArmor[5] 		= "";
	$EnemyItems[5] 		= "BluePotion 1r1 1r10 ";
	//Fish
	$EnemyWeapons[6] 	= "";
	$EnemyArmor[6] 		= "";
	$EnemyItems[6] 		= "Fish 1r1 1r1";
	//Pup
	$EnemyWeapons[7] 	= "WarAxe 2r3 1";
	$EnemyArmor[7] 		= "";
	$EnemyItems[7] 		= "CrystalBluePotion 1r1 1r3 GnollHide 1r1 1r3";
	//Scavenger
	$EnemyWeapons[8] 	= "WarAxe 2r3 1";
	$EnemyArmor[8] 		= "";
	$EnemyItems[8] 		= "BluePotion 1r1 1r5 GnollHide 1r1 1r2";
	//Hunter
	$EnemyWeapons[9] 	= "WarAxe 2r3 1 Sling 3 1";
	$EnemyArmor[9] 		= "";
	$EnemyItems[9] 		= "BluePotion 1r1 1r3 GnollHide 1r1 1r1 SmallRock 20r25 1r1";
	//Ghost
	$EnemyWeapons[10] 	= "Broadsword 3r4 1";
	$EnemyArmor[10] 	= "";
	$EnemyItems[10] 	= "CrystalBluePotion 1r3 1r2";
	//berserker
	$EnemyWeapons[11] 	= "BroadSword 2r3 1";
	$EnemyArmor[11] 	= "";
	$EnemyItems[11] 	= "BluePotion 1r2 1r5 Rod 1r1 1r12";
	//ravanger
	$EnemyWeapons[12] 	= "BroadSword 3 1";
	$EnemyArmor[12] 	= "";
	$EnemyItems[12] 	= "String 1r1 1r10";
	//slayer
	$EnemyWeapons[13] 	= "BroadSword 3 1 ShortBow 3 1";
	$EnemyArmor[13] 	= "";
	$EnemyItems[13] 	= "BluePotion 1r3 1r2 Opal 1r4 1r100 BasicArrow 10r15 1r1";
	//guardian
	$EnemyWeapons[14] 	= "BattleAxe 3 1";
	$EnemyArmor[14] 	= "";
	$enemyItems[14] 	= "DragonScale 1r1 1r10";
	//protector
	$EnemyWeapons[15] 	= "LongSword 3 1";
	$EnemyArmor[15] 	= "";
	$EnemyItems[15] 	= "BluePotion 1r2 1r10 Rod 1r1 1r20 Coal 1r1 1r5 Iron 1r4 1r5";
	//peacekeeper
	$EnemyWeapons[16] 	= "LongSword 4 1";
	$EnemyArmor[16] 	= "";
	$EnemyItems[16] 	= "BluePotion 1r1 1r5 Coal 1r4 1r10";
	//lord
	$EnemyWeapons[17] 	= "LongSword 4 1 ShortBow 3 1";
	$EnemyArmor[17] 	= "";
	$EnemyItems[17] 	= "BluePotion 1r3 1r10 Iron 1r1 1r2 BasicArrow 20r30 1r1";
	//champion
	$EnemyWeapons[18] 	= "LongSword 5 1 LightCrossbow 2 1";
	$EnemyArmor[18] 	= "";
	$EnemyItems[18] 	= "CrystalBluePotion 1r1 1r10 Jade 1r1 1r10 Diamond 1r1 1r500 ShortQuarrel 20r30 1r1";
	//brigand
	$EnemyWeapons[19] 	= "Mace 2 1";
	$EnemyArmor[19] 	= "";
	$EnemyItems[19] 	= "BluePotion 1r1 1r10 Topaz 1r2 1r20 Diamond 1r1 1r100 Iron 1r5 1r10";
	//marauder
	$EnemyWeapons[20] 	= "Mace 3 1";
	$EnemyArmor[20] 	= "";
	$EnemyItems[20]		= "Coal 1r1 1r2";
	//knight
	$EnemyWeapons[21]	= "LongSword 5 1 LightCrossbow 3 1";
	$EnemyArmor[21]		= "";
	$EnemyItems[21]		= "Coal 1r1 1r1 Iron 1r2 1r1 ShortQuarrel 30r35 1r1";
	//shaman
	$EnemyWeapons[22]	= "Dagger 3 1";
	$EnemyArmor[22]		= "";
	$EnemyItems[22]		= "bluepotion 1r2 1r10 GnollHide 1r1 1r1";
	//conjurer
	$EnemyWeapons[23]	= "ShortSword 3 1";
	$EnemyArmor[23]		= "";
	$EnemyItems[23]		= "bluepotion 1r2 1r10";
	//paladin
	$EnemyWeapons[24]	= "Spear 3 1";
	$EnemyArmor[24]		= "";
	$EnemyItems[24]		= "bluepotion 1r2 1r10 energyvial 1r1 1r10";
	//reaper
	$EnemyWeapons[25]	= "Gladius 3 1";
	$EnemyArmor[25]		= "";
	$EnemyItems[25]		= "bluepotion 1r2 1r1 crystalenergyvial 1r1 1r5";
	//warlock
	$EnemyWeapons[26]	= "ShortSword 3 1";
	$EnemyArmor[26] 	= "";
	$EnemyItems[26]		= "ruby 1r1 1r200 EnergyVial 1r1 1r2";
	//Ruffian
	$EnemyWeapons[27]	= "BroadSword 2 1";
	$EnemyArmor[27]		= "";
	$EnemyItems[27]		= "Quartz 1r1 1r200";
	//Destroyer
	$EnemyWeapons[28]	= "SpikedClub 3 1";
	$EnemyArmor[28]		= "";
	$EnemyItems[28]		= "iron 1r1 1r1";
	//Halberdier
	$enemyWeapons[29]	= "BroadSword 3 1";
	$EnemyArmor[29]		= "";
	$EnemyItems[29]		= "BluePotion 1r3 1r10";
	//Dreadnought
	$enemyWeapons[30]	= "LongSword 2 1 ShortBow 3 1";
	$enemyArmor[30]		= "";
	$EnemyItems[30]		= "BasicArrow 10r20 1r1 coal 1r1 1r1";
	//Magi
	$EnemyWeapons[31]	= "ShortSword 2 1";
	$EnemyArmor[31]		= "";
	$EnemyItems[31]		= "Emerald 1r1 1r6000 Quartz 1r10 1r200";
	//Mauler
	$EnemyWeapons[32]	= "Mace 3 1";
	$EnemyArmor[32]		= "";
	$EnemyItems[32]		= "Granite 1r10 1r5";
	//Thrasher
	$enemyWeapons[33]	= "LongSword 3 1";
	$EnemyArmor[33] 	= "";
	$EnemyItems[33] 	= "Turquoise 1r1 1r300 iron 1r3 1r2";
	//Skeleton
	$EnemyWeapons[34]	= "LongSword 4 1";
	$EnemyArmor[34]		= "";
	$EnemyItems[34]		= "Opal 1r1 1r50 Iron 1r5 1r1 coal 1r1 1r5";
	//NecroMancer
	$EnemyWeapons[35]	= "Sling 3 1 Dagger 3 1";
	$EnemyArmor[35]		= "";
	$EnemyItems[35]		= "SmallRock 35r40 1r1 Diamond 1r1 1r3000 coal 1r2 1r1";
	//Twister
	$enemyWeapons[36]	= "Claymore 3r6 1r3";
	$EnemyArmor[36] 	= "SpikedLeatherArmor 3r6 1";
	$EnemyItems[36] 	= "Adamite 1r1 1r3 Diere 1r3 1r2";
	//Seeker
	$EnemyWeapons[37]	= "IceBroadsword 4 1r3";
	$EnemyArmor[37]		= "SpikedLeatherArmor 3r6 1";
	$EnemyItems[37]		= "Adamite 1r1 1r3 Diere 1r3 1r2";
	//Rapier
	$EnemyWeapons[38]	= "Katana 1r6 1r3";
	$EnemyArmor[38]		= "HolyRobe 1r3 1";
	$EnemyItems[38]		= "Adamite 1r1 1r3 Diere 1r3 1r2 CrystalEnergyVial 1r1 1r2";

	customLoadouts(38); //35 just tells your custom function how many bots the base game has defined. Make absolutely sure you dont overwrite any, unless you really want to change some of their loadouts. I recommend that you pick numbers that would be high up so you wont have to come back and change them if more bot types are added to this def file.
	
}

// ##################################### //
// ##################################### //
//               Shop NPCs               //
// ##################################### //
// ##################################### //

function DefineTownBots()
{

	//###################loadouts##################################### DEFINE BEFORE DEFINEING THE SPAWN!
	//$shop::loadout*[number (used for %shop above)] = "itemlist... ...";

// Keldrin Town Weapons shop
	$shop::loadoutweapons[0] 	= "Pickaxe Knife Dagger ShortSword Club QuarterStaff SpikedClub Hatchet WarAxe BroadSword Sling ShortBow";//weapons this merchant sells
	$shop::loadoutArmors[0] 	= "PaddedArmor LeatherArmor SpikedLeatherArmor StuddedLeatherArmor BasicRobe LeatherBoots";//armor the merchant sells...
	$shop::loadoutItems[0]		= "BluePotion EnergyVial BasicArrow SmallRock";//items the merchant sells + misc stuff...

// Ethren Weapon Merchant
	$shop::loadoutweapons[1] 	= "Pickaxe Bardiche WarHammer WarMaul Claymore Katana BastardSword";
    $shop::loadoutArmors[1]     ="";
    $shop::loadoutItems[1]      ="";

// Ethren Armor Merchant
	$shop::loadoutweapons[2] 	= "HammerPick BattleAxe Gladius WarHammer Trident BastardSword";
    $shop::loadoutArmors[2] 	= "BronzePlate HalfPlate FieldPlate FullPlate";
	$shop::loadoutItems[2]		= "BluePotion CrystalBluePotion EnergyVial CrystalEnergyVial";

// Mystery Merchant
	$shop::loadoutweapons[3] 	= "GreatClaymore";
    $shop::loadoutArmors[3]     ="";
    $shop::loadoutItems[3]      ="";

// Barracks
	$shop::loadoutweapons[4] 	= "Gladius";
    $shop::loadoutArmors[4]     ="";
    $shop::loadoutItems[4]      ="";

//Delkin merchant
	$shop::loadoutweapons[5] 	= "ShortSword Spear SpikedClub Mace BroadSword Longsword ShortBow LightCrossbow";
	$shop::loadoutArmors[5]		= "ScaleMailBody BrigandineBody";
	$shop::loadoutItems[5]		= "BluePotion CrystalBluePotion EnergyVial";

//jaten merchant

	$shop::loadoutweapons[6] 	= "HammerPick BattleAxe Gladius";
	$shop::loadoutArmors[6]		= "HideArmor ChainMailBody RingMailBody BandedMailArmor SplintMailBody";
	$shop::loadoutItems[6]		= "BluePotion EnergyVial";
	
// Apprentice
	$shop::loadoutweapons[7] 	= "";
	$shop::loadoutArmors[7]		= "ApprenticeRobe LightRobe FineRobe";
	$shop::loadoutItems[7] 		= "";

// Mage Guild merchant
    $shop::loadoutweapons[8]	= "";
    $shop::loadoutArmors[8]		= "BloodRobe AdvisorRobe";
    $shop::loadoutItems[8] 		= "";

//balanvillage merchant
    $shop::loadoutweapons[9]    ="Pickaxe Knife Dagger ShortSword Club QuarterStaff SpikedClub Hatchet WarAxe BroadSword Sling ShortBow";
    $shop::loadoutArmors[9]     ="PaddedArmor LeatherArmor SpikedLeatherArmor StuddedLeatherArmor BasicRobe LeatherBoots";
    $shop::loadoutItems[9]      ="BluePotion EnergyVial BasicArrow SmallRock";
    

	//spawnTownBot(%pos, %team, %type,%race , %shop , %name)
	//team 1 is human...
	//type 
	//Merchant is a merchant
	//bank is the banker
	//Porter is the arena porter
	//Guild is the guild registar. 
	//%transform is the x y z rx ry nsz rz coord of the merchant rx ry and rz is the rotation you want to set the z axis rotation... nsz is something weird also used in rotation ... rotation is in radians
	//use the admin command #gettransform to figure out the rotation..
	
	
//=======BANKERS========
//Keldrin Banker
	spawnTownbot("-323.278 951.544 51.6752 0 0 1 3", 1, "Bank", "MaleHuman", 0, "Keldrin Banker");
//Ethren Banker
    spawnTownBot("281.703 -271.054 74.0168 0 0 1 0", 1, "Bank", "MaleHuman", 0, "Ethren Banker");
//Delkin Banker
	spawnTownBot("-1495.14 1534.54 91.5668 0 0 -1 1.4342", 1, "Bank", "MaleHuman", 0, "Delkin Banker");
//Jaten Banker
	spawnTownBot("-2742.07 1355.07 62.4689 0 0 1 0", 1, "Bank", "MaleHuman", 0, "Jaten Banker");
//Balan Banker
    spawnTownBot("-1055.44 -2152.29 171.694 0 0 1 2.97899",1,"Bank","MaleHuman",0,"Balan Banker"); //new!

//*******MERCHANTS********
//Keldrin Town Merchant
	spawnTownBot("-332.496 952.588 51.6751 0 0 1 171.887", 1, "Merchant", "MaleHuman",0,"Keldrin Merchant");
//Jaten merchant
	spawnTownBot("-2834.62 1227.5 68.8091 0 0 1 0", 1, "Merchant", "MaleHuman", 6, "Jaten Merchant");	
//Barracks Merchant
	spawnTownBot("1519.94 1220.9 56.8257 0 0 -1 1.05709", 1, "Merchant", "MaleHuman",4,"Old Warrior"); //ex
//Delkin Merchant
	spawnTownBot("-1539 1545.22 92 0 0 1 1.91409", 1, "Merchant", "MaleHuman", 5, "Delkin Merchant");
//Ethren Weapon Merchant
	spawnTownBot("286.335 -256.684 46.0276 0 0 1 3.49617", 1, "Merchant", "MaleHuman",1,"Ethren Weapon Merchant"); //ex
//Ethren Armor Merchant
	spawnTownBot("283.866 -268.232 46.0249 0 0 1 0.347734", 1, "Merchant", "MaleHuman",2,"Ethren Armor Merchant");
//Guild Mage Merchant
	spawnTownBot("2132.84 92.9041 93.3237 0 0 -1 0.0337519", 1, "Merchant", "MaleHuman", 8, "Guild Mage Merchant");
// Mystery Merchant
	spawnTownBot("-2373.21 2027.34 170.471 0 0 1 3.13814", 1, "Merchant", "MaleHuman",3,"Mysterious Man"); //ex
// Balan Merchant
    spawnTownBot("-1067.85 -2148.69 171.675 0 0 1 3.03594",1,"Merchant","MaleHuman",9,"Balan Merchant"); //new!

//@@@@@@@BOATMASTER@@@@@@@
//Ktown Boat Dude
	spawnTownBot("-534.049 1156.74 49.5689 0 0 1 1.98489", 1, "Boat", "MaleHuman", "Keldrin", "George");
//Ethren Boat Dude
	spawnTownBot("-111.542 -430.296 49.6485 0 0 1 1.20955", 1, "Boat", "MaleHuman", "Ethren", "Tom");
//Delkin Boat Dude
	spawnTownBot("-1519.24 1520.42 91.565 0 0 0.999994 0.131746", 1, "Boat", "MaleHuman", "Delkin", "Bill");
//Jaten Boat Dude
	spawnTownBot("-2664.55 1183.64 49.9608 0 0 -1 0.813393", 1, "Boat", "MaleHuman", "Jaten", "Chris");
//Balan boat dude
	spawnTownBot("-1516.72 -2523.04 51.5207 0 0 0.999999 0.118598", 1, "Boat" ,"MaleHuman", "Balan", "Matt");

//~~~~~~~~~MISC~~~~~~~~~~
//Guild Register
	spawnTownBot("414.512 -160.199 180.377 0 0 1 2.14815", 1, "Guild", "MaleHuman", 0, "Guild Register");
//Arena Attendant
	spawnTownBot("47.0899 107.787 68.7241 0 0 -1 1.59045", 1, "Porter", "MaleHuman", 0, "Arena Attendant");
//Apprentice
	spawnTownBot("-345.534 207.509 134.986 0 0 1 3.25385", 1, "Merchant", "MaleHuman", 7, "Apprentice");

	//-2665.44 1184.03 49.9734 0 0 -1 0.974081
	//QUESTS!.
	//note when anyone says anything within the radius of this bot, functioncall will be called like this
	//quest[function](%quest, %botid, %client, %text), from there the quest bot will operate. All questing functions should be defined in the mission cs file.
	//over time i will create a bunch of simple support functions for these bots.
	//NOTE: all functions called by quest bots will always start with QUEST see example	
	//example
	//spawnquestbot( "1032 309 123", 1, "MaleHuman", "newbiebottalk", "helper", "FirstQuest"); //syntax note spawnquestbot(%transform, %team, %race, %function, %botname, %questname); note: %questname must be one word.
	//function Questnewbiebottalk(%quest, %botid, %client, %text)	

	//quest specific functions. Please only use these functions and the quest object when creating your quests. More functions will be added at a later date. 
	//isgreeting(%text) returns either true or false if the text message is a greeting.
	//Quest Object:
	//Functions (%this is just the quest object, when calling these functions use the (.) operator example %quest.respond(%botid, %client, "something");, note how you dont specify the %this.
	//Quest::GetState(%this, %botid, %client);
	//Quest::setState(%this, %botid, %client, %state);
	//Quest::Respond(%this, %botid, %client, %message);
	//Quest::ClientHasItem(%this, %client, %item, %amount);
	//Quest::TakeItem(%this, %botid, %client, %item, %amount);
	//Quest::GiveItem(%this, %botid, %client, %item, %prefix, %suffix, %amount); 
	//Quest::GiveReward(%this, %botid, %client, %reward);//%reward is a stuffstring.Note: Do not give exp as an award.
	//Quest::GetBotByIndex(%this, %index); // returns the botid of the quest bot in the same group. The first one spawned will have an index of 1, second one spawned will have an index of 2. This is useful if you have a quest with multiple questbots.
	
spawnquestbot("-307.007 944.572 57 0 0 -1 0.0967409", 1, "MaleHuman", "Test", "Old Man", "FirstQuest");
	spawnquestbot("-342.667 963.046 52 0 0 1 1.5663", 1, "MaleHuman", "Message", "Sean", "DeliveryQuest");
	spawnquestbot("-2846.8 1366.72 65 0 0 1 3.72164", 1, "MaleHuman", "Message2", "Garren", "DeliveryQuest");
	customTownBots(); //loaded in custom file
}

//questtest
function QuestTest(%quest, %botid, %client, %text)
{
	if(%quest.getState(%botid, %client) == 0)
	{
		if(isgreeting(%text))
		{
			%quest.respond(%botid, %client, "Hello adventurer! Are you looking for a quest? [yes or no]");
			%quest.setState(%botid, %client, 1);
		}
	}
	else if(%quest.getstate(%botid, %client) == 1)
	{
		if(%text $= "yes")
		{
			%quest.respond(%botid, %client, "Good! I used to be a miner back in my day, however... my age has not been kind to me as of late. Could you retrieve one quartz and one opal for me please? [yes or no]");
			%quest.setState(%botid, %client, 2);
		}
		else if(%text $= "no")
		{
			%quest.respond(%botid, %client, "Get out of my sight then!");
			%quest.setState(%botid, %client, 0);
		}
		else if(isgreeting(%text))
		{
			%quest.respond(%botid, %client, "Hello adventurer! Are you looking for a quest? [yes or no]");
		}
	}
	else if(%quest.getState(%botid, %client) == 2)
	{
		if(%text $= "yes")
		{
			%quest.respond(%botid, %client, "YES! Thank you very much, come say hi to me when you get the items!");
			%quest.setstate(%botid, %client, 3);
		}
		else if( %text $= "no")
		{
			%quest.setstate(%botid, %client, 0);
			%quest.respond(%botid, %client, "How dare you waste my time! Away with you!");
		}
		else if(isgreeting(%text))
		{
			//it's a good idea to allow the bot to repeat the last thing he said if the user logs off and logs back on and has no clue where he left off.
			%quest.respond(%botid, %client, "Will you get one opal and one quartz for me please? [yes or no]");
		}
	}
	else if(%quest.getstate(%botid, %client) == 3)
	{
		if(isgreeting(%text))
		{
			if( %quest.clienthasitem(%client, "Quartz", 1) && %quest.clienthasitem(%client, "Opal", 1) )
			{
				%quest.TakeItem(%botid, %client, "Quartz", 1);
				%quest.TakeItem(%botid, %client, "Opal", 1);
				%quest.respond(%botid, %client, "Wow! You got it! Thank you very much. Take this as a reward!");
				%quest.givereward(%botid, %client, "SP 10");
				%quest.setstate(%botid, %client, 4);
			}
			else
			{
				%quest.respond(%botid, %client, "I am still waiting for the Opal and Quartz!");
			}
		
		}
	}
	else if(%quest.getstate(%botid, %client) == 4)
	{
		if(isgreeting(%text))
		{
			%quest.respond(%botid, %client, "Sorry, I do not need your help anymore.");
		}
	}
}

//questmessage
function QuestMessage(%quest, %botid, %client, %text)
{
	if(%Quest.getstate(%botid, %client) == 0)
	{
		if(isgreeting(%text))
			%quest.respond(%botid, %client, "Hello traveller! I am Sean. I am charge of the stores for our SHOPS.");
		if(%text $= "shops" || %text $= "shops?")
		{
			%quest.setstate(%botid, %client, 1);
			%quest.respond(%botid, %client, "Yes, the Keldrin item shop over there. Which reminds me, I need to order a shipment of cargo and there are no messangers around to deliver the message. Do you think you can help me?");
		}
	}
	else if(%quest.getstate(%botid, %client) == 1)
	{
		if(isgreeting(%text))
			%quest.respond(%botid, %client, "Do you think you can help me? [yes or no]");
		if(%text $= "yes")
		{
			%quest.setstate(%botid, %client, 2);
			%quest.respond(%botid, %client, "Oh good! Take this note to Garren in Jaten.");
		}
		else if (%text $= "no")
		{
			%quest.respond(%botid, %client, "Thats too bad, I guess I have to find someone else.");
			%quest.setstate(%botid, %client, 0);
		}
	}
	else if(%quest.getstate(%botid, %client) == 2)
	{
		if(%quest.getstate(%quest.getbotbyindex(2), %client) == 1)
		{
			
			%quest.respond(%botid, %client, "Thank you very much! Here is your payment!");
			%quest.givereward(%botid, %client, "COINS 100000");
			%quest.setstate(%botid, %client, 3);
		}
		else
		{
			if(isgreeting(%text))
				%quest.respond(%botid, %client, "Please deliver the note I gave you to Garren in Jaten");
		}
	}
	else if(%quest.getstate(%botid, %client) == 3)
	{
		if(isgreeting(%text))
			%quest.respond(%botid, %client, "Sorry, I have no more messages for you to deliver. Maybe some other time");
	}
}

//questmessage2
function QuestMessage2(%quest, %botid, %client, %text)
{
	if(%quest.getstate(%quest.getbotbyindex(1), %client) == 2)
	{
		if(%quest.getstate(%botid, %client) == 0)
		{
			if(isgreeting(%text))
				%quest.respond(%botid, %client, "Hello! I see you have a note, is it for me?");
			if(%text $= "yes")
			{
				%quest.respond(%botid, %client, "Thanks. Hrm...");
				%quest.respond(%botid, %client, "Thank you, take this note back to Sean to prove to him you gave me his note");
				%quest.setstate(%botid, %client, 1);
			}
			else if(%text $= "no")
			{
				%quest.respond(%botid, %client, "That is unfortunate, I think that note is for me...");
			}
		}
		else
		{
			if(isgreeting(%text))
				%quest.respond(%botid, %client, "Hello, welcome to my home, you may rest here if you like.");
		}
	}
	else
	{
		if(isgreeting(%text))
			%quest.respond(%botid, %client, "Hello, welcome to my home, you may rest here if you like.");
	}
}

	//---------------------------------------------------------------------------------------
	//	RaceZoneStrings[Client entering's RaceID, Zone being entered's RaceID, n]

function DefineZoneSpecifications()
{

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



	customZoneSpecifications();


}

function DefineMiningPoints()
{
	//CreateMiningSpot("x y z", [Multiplier]) the x y z is the pos and the multiplyer determines how fast it will spawn rocks, 0 is increadably fast dont use that
	//1 is normal, .5 is twice as fast .25 is four times as fast and so on. 2 is twice as long, 4 is 4x long and so on. 
	CreateMiningSpot("-1037.09 495.774 139.376",1.5);
	CreateMiningSpot("-1169.51 588.677 105",1.5);
	CreateMiningSpot("-1075.57 630.961 133",1.5);
	CreateMiningSpot("-1083 596 133", 1.5);
	CreateMiningSpot("670 2307 275", 1.5);
	CustomMiningPoints();

}
//This adds Env mapping to the Minotaur's Lair marble textures
addMaterialMapping("MinoLair/Mino_Marble", "environment: Minolair/Mino_Emap 0.20"); 
addMaterialMapping("MinoLair/Mino_OldRoof", "environment: Minolair/Mino_Emap 0.20"); 
addMaterialMapping("MinoLair/Mino_OldRoof2", "environment: Minolair/Mino_Emap 0.20"); 
addMaterialMapping("MinoLair/Mino_MossMarble", "environment: Minolair/Mino_Emap 0.20"); 
addMaterialMapping("ISTextures/F_KWater", "environment: RPGSky/RPG_emap 0.20");
addMaterialMapping("ISTextures/F_Bars", "environment: Minolair/Mino_Emap 0.30");
addMaterialMapping("terrain/RPG_grass1", "color: 0.4 0.325 0.243 1.0 0.0"); 
exec("scripts/rpgpropmap.cs");
//custom quests should be loaded in this file! if the file does not exist go ahead and create it. This way when the map updates clients wont have to restart custom quests.
//also you can put the following functions in this file to add on things to the map and keep them when the dev team updates this file: customzonespecifications, customloadouts, customaispawn, customtownbots, customminingpoints. Put quests in customtownbots

if (isFile("missions/custom/T2RPG_Worldmap_custom.cs"))
exec("missions/custom/T2RPG_Worldmap_custom.cs");

exec("missions/T2RPG_Worldmap/minotaur_spawngroup.cs");
