function fetchdata(%client, %type)
{
	return game.fetchdata(%client, %type);
}

function RPGGame::fetchData(%game, %client, %type)
{
//param is optional.
	if(%type $= "LVL")
	{
		%a = GetLevel(fetchData(%client, "EXP"), %client);
		return %a;
	}
	else if(%type $= "DEF")
	{
		%a = AddPoints(%client, 7);
		//%b = AddBonusStatePoints(%client, "DEF");
		//%c = CombineRpgRolls(%a, %b, 0, "inf");
		
		return %a;
	}
	else if(%type $= "MDEF")
	{
		%a = AddPoints(%client, 3);
		//%b = AddBonusStatePoints(%client, "MDEF");
		//%c = CombineRpgRolls(%a, %b, 0, "inf");

		//%d = (fetchData(%client, "OverweightStep") * 7.0) / 100;
		//%e = Cap(%c - (%c * %d), 0, "inf");
		
		return %a;
	}
	else if(%type $= "ATK")
	{
		%a = AddPoints(%client, 6);
		//%b = AddBonusStatePoints(%client, "ATK");
		//%c = CombineRpgRolls(%a, %b, 0, "inf");

		return %a;

		%weapon = (%client.player.getMountedImage($WeaponSlot) == 0) ? "" : %client.player.getMountedImage($WeaponSlot).getName().item;

		if(%weapon !$= "")
		{
			//%a = AddBonusStatePoints(%client, "ATK");

			if(GetAccessoryVar(%weapon, $AccessoryType) $= $RangedAccessoryType)
				%weapon = fetchData(%client, "LoadedProjectile " @ %weapon);

			%b = GetRpgRoll(GetWord(GetAccessoryVar(%weapon, $SpecialVar), 1));

			return %a + %b;
		}
		else
			return 0;
	}
	else if(%type $= "MaxHP")
	{
		%a = $MinHP[fetchData(%client, "RACE")];
		%b = AddPoints(%client, 4);
		if(!%client.isaicontrolled())
		%c = fetchData(%client, "LVL") * $HPPerLvl[fetchdata(%client, "class")];
		//%d = $minHP[fetchdata(%client, "RACE")];
		%e = %client.data.PlayerSkill[$SkillEndurance] * 0.5;
		
		%total = mfloor(%a + %b + %c + %d + %e);
		if(%total <= 1) %total = 1;
		
		return %total;
	}
	else if(%type $= "HP")
	{
		%armor = %client.player.getDataBlock();

		%c = %armor.maxDamage - %client.player.getDamageLevel();
		%a = %c * fetchData(%client, "MaxHP");
		%b = %a / %armor.maxDamage;

		return round(%b);
	}
	else if(%type $= "MaxMANA")
	{
		%a = 8 + round( ((%client.data.PlayerSkill[$skill::OffensiveCasting] + %client.data.PlayerSkill[$skill::DefensiveCasting] + %client.data.PlayerSkill[$skill::NeutralCasting] )/2 + (%client.data.PlayerSkill[$skill::Focus] + %client.data.PlayerSkill[$skill::bashing] + %client.data.playerSkill[$skill::backstabbing] + %client.data.playerSkill[$skill::cleaving] )/12 ) * (1/3) );
		%b = AddPoints(%client, 5);
		//%c = AddBonusStatePoints(%client, "MaxMANA");

		return %a + %b;
	}
	else if(%type $= "MANA")
	{
		%armor = %client.player.getDataBlock();

		%a = %client.player.getEnergyLevel() * fetchData(%client, "MaxMANA");
		%b = %a / %armor.maxEnergy;

		return round(%b);
	}
	else if(%type $= "MaxWeight")
	{
		%a = 50 + %client.data.PlayerSkill[$SkillWeightCapacity];
		//%b = AddPoints(%client, 9);
		//%c = AddBonusStatePoints(%client, "MaxWeight");

		return %a;
	}
	else if(%type $= "Weight")
	{
		return GetWeight(%client);
	}
	else if(%type $= "RankPoints")
	{
		return Cap(mfloor(%client.data.ClientData[%type]), 0, "inf");
	}
	else if(%type $= "OverweightStep")
	{
		return Cap(mfloor(%client.data.ClientData[%type]), 0, "inf");
	}
	else if(%type $= "inventory")
	{
		return %client.data.itemlist;
	}
	else
		return %client.data.ClientData[%type];

	return false;
}
function storedata(%client, %type, %amt, %special)
{
	return Game.storedata(%client, %type, %amt, %special);
}
function RPGGame::storeData(%game, %client, %type, %amt, %special)
{
	if(%type $= "HP")
	{
		setHP(%client, %amt);
	}
	else if(%type $= "MANA")
	{
		setMANA(%client, %amt);
	}
	else if(%type $= "MaxHP" || %type $= "MaxMANA" || %type $= "MaxWeight" || %type $= "Weight")
	{
		echo("Invalid call to storeData for " @ %type @ " : Can't manually set this variable.");
		return false;
	}
	else
	{
		if(%special $= "inc")
			%client.data.ClientData[%type] += %amt;
		else if(%special $= "dec")
			%client.data.ClientData[%type] -= %amt;
		else if(%special $= "strinc")
			%client.data.ClientData[%type] = %client.data.ClientData[%type] @ %amt;
		else
			%client.data.ClientData[%type] = %amt;

		if(GetWord(%special, 1) $= "cap")
			%client.data.ClientData[%type] = Cap(%client.data.ClientData[%type], GetWord(%special, 2), GetWord(%special, 3));
		if(%type $= "Coins") commandToClient(%client, 'SetCoins', %client.data.ClientData["coins"]);
	}
	return true;
}
function Client::storedata(%client, %type, %amt, %special)
{
	return storedata(%client, %type, %amt, %special);
}
function ServerCmdFetchData(%client, %type)
{
	//max out the calls to 30 every min. This is to prevent people from accidentally lagging out the server. 
	%max = 30;

	if(%client.fetchqueuee == 0)

	{
		%client.fetchqueuee = 1;
		%client.fetchqueue = initqueue(%max);
		for(%i = 0; %i<%max; %i++)
			%client.fetchqueue.push(0);//zero it out
		
	}//init the time queue
	%time = getTime();
	if(%time - %client.fetchqueue.get() <= 60)
	return;
	%client.fetchqueue.pop();//pop one out of the queue. 
	%client.fetchqueue.push(%time);//push another on. 
	commandToClient(%client, 'fetchdata', fetchdata(%client, %type));
}
//Due to problems with decimal precision, a mathematical way of calculating levels is not satisfactory.
//I came up with a geometric series of which I have not yet been able to find an algorithm for.
//The series is as follows:
//0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610
//You will notice that any number can be determined by adding the two previous ones together.  It seems
//an algorithm would be simple to find since there is a relation between the numbers, but it's still a
//tough one that I haven't been able to crack.

//I created a look-up table that used this series, but unfortunately the numbers grew too large at around
//level 40.  I will be using the old method suggested by Rykoffe a few years ago in T1RPG (long before the
//TRPG AD&D days).  This method is mathematical and uses the y = ax + ax^2 formula, but I will be using
//a look-up table nonetheless in order to conserve precision.  I programmed the GetLevel function to
//hop around the table in an efficient enough way (always 1 hop, otherwise the table is looked at from the beginning).

//(later date) My dad (a research scientist) did some research on the initial series metionned in the first paragraph
//for me and it turns out it's a famous sequence that dates back to 1202 called the Fibonnaci sequence (quite
//appropriate considering this game is based around then).  Only much later did anyone figure out the equation:
//a(k) = (1 / msqrt(5)) * [ mpow(((1 + msqrt(5)) / 2), k) - mpow(((1 - msqrt(5)) / 2), k) ]
//Unfortunately, Tribes2 math doesn't support numbers above or equal to one million, so I'll have to stick with the
//other formula (y = ax + ax^2)

//Generate look-up table:
$MaxLevel = 110;

//%exptablea = 100;
//for(%i = 1; %i <= $MaxLevel; %i++)
//	$ExpTable[%i] = mfloor(%exptablea * (%i-1) + (%exptablea * mpow((%i-1), 2)));


function GetLevel(%ex, %client)
{
	if(%ex < (100)*1000 || %client.isaicontrolled())
		return mfloor(%ex/1000)+1;
	else
	{
		%ex -= 99000;
		%ex /= 1000;
		%lvl = (msqrt(8*%ex+1)-1)/2;
		
		return mfloor(%lvl)+100;
	}
	return mfloor(%ex/1000)+1;

}

function GetEXP(%level, %client)
{
	if(%level <= 100 || %client.isaicontrolled())
	return ((%level-1)*1000);
	else
	{
		%l2 = %level - 100;
		%totalexp = 99*1000;
		%totalexp += (%l2*(%l2+1)/2)*1000;
		
		
		return %totalexp;
	}
}

function DistributeExpForKilling(%damagedClient)
{
	%ai = %damagedClient.isAiControlled();

	if(%damagedClient.despawning)
		return;
	
	%dname = %damagedClient.nameBase;
	%dlvl = fetchData(%damagedClient, "LVL");
	%dexp = fetchData(%damagedClient, "EXP");
	if(!%ai)
	return;
	%count = 0;

	//parse .damagedBy and create %finalDamagedBy
	%nameCount = 0;
	%listCount = 0;
	%total = 0;
	%tmpl = "";
	for(%i = 1; %i <= $maxDamagedBy; %i++)
	{
		if(%damagedClient.damagedBy[%i] !$= "")
		{
			%listCount++;

			%n = firstWord(%damagedClient.damagedBy[%i]);
			%d = GetWord(%damagedClient.damagedBy[%i], 1);

			%flag = 0;
			for(%z = 1; %z <= %nameCount; %z++)
			{
				if(%finalDamagedBy[%z] $= %n)
				{
					%flag = 1;
					%dCounter[%n] += %d;
				}
			}
			if(%flag $= 0)
			{
				%nameCount++;
				%finalDamagedBy[%nameCount] = %n;
				%dCounter[%n] = %d;

				%p = IsInWhichParty(%n);
				if(%p != false)
				{
					%id = firstWord(%p);
					//%inv = GetWord(%p, 1);
					if(%id != false)
					{
						%tmppartylist[%id] = %tmppartylist[%id] @ %n @ " ";
						if(strstr(%tmpl, %id @ " ") $= -1)
							%tmpl = %tmpl @ %id @ " ";
					}
				}
			}
			%total += %d;
		}
	}

	//clear .damagedBy
	for(%i = 1; %i <= $maxDamagedBy; %i++)
		%damagedClient.damagedBy[%i] = "";

	//parse thru all tmppartylists and determine the number of same party members involved in exp split
	for(%w = 0; (%a = GetWord(%tmpl, %w)) !$= ""; %w++)
	{
		%n = countWords(%tmppartylist[%a]);
		for(%ww = 0; (%aa = GetWord(%tmppartylist[%a], %ww)) !$= ""; %ww++)
			%partyFactor[%aa] = %n;
	}

	//distribute exp
	for(%i = 1; %i <= %nameCount; %i++)
	{
		if(%finalDamagedBy[%i] !$= "")
		{
			%listClientId = %finalDamagedBy[%i];//should be this way, dont store the name in the damaged by list

			%slvl = fetchData(%listClientId, "LVL");
			
			if(%slvl > 0)
				%sfactor = 4 / %slvl+1; 
			else
				%sfactor = 5;
			%value = 60 - (%slvl - %dlvl)*10;
			
			
			%value *= 2;
			%value *= %sfactor;
			
			if(%value < 2) %value = 2;
			 //if it goes as planned, we will mutliply value by sfactor (which is a value always greater than one.)

			%perc = %dCounter[%finalDamagedBy[%i]] / %total;
			//if(!%listClientId.isAiControlled())
			//echo(%damagedclient.rpgname SPC %sfactor SPC %sfactor*%value SPC %value SPC %perc SPC %value*%perc);
			%final = round( %value * %perc );

			//determine party exp
	
			%pf = %partyFactor[%finalDamagedBy[%i]]-1;
			
			if(%pf >= 1)
				%pvalue = round(%final * (1.0 * (%pf * 0.1)));
			else
				%pvalue = 0;
			%final = round(%final/1.5);
			storeData(%listClientId, "EXP", %final, "inc");
			if(%final > 0)
				messageClient(%listClientId, 'DistributeExp', $MsgBlue @ %dname @ " has died and you gained " @ %final @ " experience!");
			else if(%final < 0)
				messageClient(%listClientId, 'DistributeExp', $MsgRed @ %dname @ " has died and you lost " @ -%final @ " experience.");
			else if(%final $= 0)
				messageClient(%listClientId, 'DistributeExp', $MsgLightGray @ %dname @ " has died.");

			if(%pvalue > 0)
			{
				storeData(%listClientId, "EXP", %pvalue, "inc");
				messageClient(%listClientId, 'DistributeExp', $MsgBlue @ "You have gained " @ %pvalue @ " party experience!");
				
			}
			RefreshExp(%listClientId);
		}
	}

	RefreshExp(%damagedClient);
}
/// weight functions

function GetWeight(%client)
{
	return %client.weight;
}
function WeightStep(%client, %skill)
{
	
	
	if(!%client.player || %client.isaicontrolled() || !fetchdata(%client, "HasLoadedAndSpawned")) return;
	

	%weightfactor = Game.fetchdata(%client, "Weight") / Game.fetchdata(%client, "MaxWeight");
	if(%weightfactor < 1) %weightfactor = 1;
	%penalty = (%weightfactor-1)*200; //1=1 1.5 = 0
	%speedfactor = (100-%penalty)/100;
	if(%speedfactor < 0) %speedfactor = 0;
	%sf = round((1-%speedfactor)*50);
	%sf += 50 - AddPoints(%client, 12);
	if(fetchdata(%client, "surge"))
		%sf -= 10;
	if(%sf < 0) %sf = 0;
	if(%sf > $maxarmor-1) %sf = $maxarmor-1;
	//echo(%sf SPC %client.rpgname);
	%client.cweight = %sf;
	//echo(%sf SPC %client);
	if(%client.lastweight != %sf)
	{

		%race = fetchdata(%client, "RACE");

		%race = "MaleHuman";//until femalehuman is put in
		%apm = "Armor";
		%dbname = %race @ %apm @ %sf;
		%e = %client.player.getEnergyLevel();
		%d = %client.player.getDamageLevel();
		%client.player.setdatablock(%dbname);
		%client.player.setEnergyLevel(%e);
		%client.player.setDamageLevel(%d);
	}
	%client.lastweight = %sf;
	if(%skill)
	{
	 UseSkill(%client, $Skill::WeightCapacity, true, true, 50, true);
	}
}
function weightcall(%client)
{
	
	if(!%client.isaicontrolled())
	{
	
		cancel(%client.weightstepcall);//incase of irriterations
		WeightStep(%client, true);
		%client.weightstepcall = schedule(25000, %client, "WeightCall", %client, 1);
	}
}
