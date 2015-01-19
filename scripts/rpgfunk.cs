function String::replaceAll(%string, %search, %replace)
{
	if(%search $= %replace)
		return %string;

	for(%i = 0; %i $= 0; %i = 0)
	{
		%stringbackup = %string;
		%string = strreplace(%string, %search, %replace);
		if(%stringbackup $= %string)
			break;
	}
	return %string;
}
function String::ofindSubStr(%s, %f, %o)
{
	return strstr(getsubstr(%s, %o, 99999), %f);
}

function SaveWorld()
{
	for(%i = 0; %i < ClientGroup.GetCount(); %i++)
	{
		SaveCharacter(ClientGroup.GetObject(%i));
	}
}
function closeBottomPrint(%client)
{
	clearBottomPrint(%client);
	%client.BPopen = "";

}
function RPGFixString(%name)
{
	for(%i = 0; %i < StrLen(%name); %i++)
	{
		%tmp = GetSubStr(%name, %i, 1);
		%notdone = false;
		switch$(%tmp)
		{
			case "{": %tmp = "c1";
			case "}": %tmp = "c2";
			case "c": %tmp = "cc";
			case "(": %tmp = "c3";
			case ")": %tmp = "c4";
			case ":": %tmp = "c5";
			case "-": %tmp = "c6";
			case "+": %tmp = "c7";
			case "=": %tmp = "c8";
			case "*": %tmp = "c9";
			case "&": %tmp = "ca";
			case "^": %tmp = "cb";
			case "$": %tmp = "cd";
			case "#": %tmp = "ce";
			case "@": %tmp = "cf";
			case "!": %tmp = "cg";
			case "`": %tmp = "ch";
			case "~": %tmp = "ci";
			case "/": %tmp = "cj";
			case "\\": %tmp = "ck";
			case "<": %tmp = "cl";
			case ">": %tmp = "cm";
			case "?": %tmp = "cn";
			case ",": %tmp = "co";
			case ".": %tmp = "cp";
			case "'": %tmp = "cq";
			case "\"": %tmp = "cr";
			case ";": %tmp = "cs";
			case " ": %tmp = "c_";
			case "[": %tmp = "ct";
			case "]": %tmp = "cu";
			case "|": %tmp = "cv";

			//if it is not alphanumeric then we use the qdt switch
			case "q": %tmp = "q1";
			case "¡": %tmp = "q2";
			case "¢": %tmp = "q3";
			case "£": %tmp = "q4";
			case "¤": %tmp = "q5";
			case "¥": %tmp = "q6";
			case "¦": %tmp = "q7";
			case "§": %tmp = "q8";
			case "¨": %tmp = "q9";
			case "©": %tmp = "qa";
			case "ª": %tmp = "qb";
			case "«": %tmp = "qc";
			case "¬": %tmp = "qd";
			case "­": %tmp = "qe";
			case "®": %tmp = "qf";
			case "¯": %tmp = "qg";
			case "°": %tmp = "qh";
			case "±": %tmp = "qi";
			case "²": %tmp = "qj";
			case "³": %tmp = "qk";
			case "´": %tmp = "ql";
			case "µ": %tmp = "qm";
			case "¶": %tmp = "qn";
			case "·": %tmp = "qo";
			case "¸": %tmp = "qp";
			case "¹": %tmp = "qq";
			case "º": %tmp = "qr";
			case "»": %tmp = "qs";
			case "¼": %tmp = "qt";
			case "½": %tmp = "qu";
			case "¾": %tmp = "qv";
			case "¿": %tmp = "qw";
			case "À": %tmp = "qx";
			case "Á": %tmp = "qy";
			case "Â": %tmp = "qz";
			case "Ã": %tmp = "q0";
			case "Ä": %tmp = "q_";
			default:
			%notdone = true;

		}
		if(%notdone)
		{
			%notdone = false;
			switch$(%tmp)
			{

				case "d": %tmp = "d0";
				case "Å": %tmp = "d1";
				case "Æ": %tmp = "d2";
				case "Ç": %tmp = "d3";
				case "È": %tmp = "d4";
				case "É": %tmp = "d5";
				case "Ê": %tmp = "d6";
				case "Ë": %tmp = "d7";
				case "Ì": %tmp = "d8";
				case "Í": %tmp = "d9";
				case "Î": %tmp = "da";
				case "Ï": %tmp = "db";
				case "Ð": %tmp = "dc";
				case "Ñ": %tmp = "dd";
				case "Ò": %tmp = "de";
				case "Ó": %tmp = "df";
				case "Ô": %tmp = "dg";
				case "Õ": %tmp = "dh";
				case "Ö": %tmp = "di";
				case "×": %tmp = "dj";
				case "Ø": %tmp = "dk";
				case "Ù": %tmp = "dl";
				case "Ú": %tmp = "dm";
				case "Û": %tmp = "dn";
				case "Ü": %tmp = "do";
				case "Ý": %tmp = "dp";
				case "Þ": %tmp = "dq";
				case "à": %tmp = "dr";
				case "á": %tmp = "ds";
				case "â": %tmp = "dt";
				case "ã": %tmp = "du";
				case "ä": %tmp = "dv";
				case "å": %tmp = "dw";
				case "æ": %tmp = "dx";
				case "ç": %tmp = "dy";
				case "è": %tmp = "dz";
				case "é": %tmp = "d_";

				case "t": %tmp = "t0";
				case "ê": %tmp = "t1";
				case "ë": %tmp = "t2";
				case "ì": %tmp = "t3";
				case "í": %tmp = "t4";
				case "î": %tmp = "t5";
				case "ï": %tmp = "t6";
				case "ð": %tmp = "t7";
				case "ñ": %tmp = "t8";
				case "ò": %tmp = "t9";
				case "ó": %tmp = "ta";
				case "ô": %tmp = "tb";
				case "õ": %tmp = "tc";
				case "ö": %tmp = "td";
				case "÷": %tmp = "te";
				case "ø": %tmp = "tf";
				case "ù": %tmp = "tg";
				case "ú": %tmp = "th";
				case "û": %tmp = "ti";
				case "ü": %tmp = "tj";
				case "ý": %tmp = "tk";
				case "þ": %tmp = "tl";
				default: %notdone = true;
			}
		}
		%newstr = %newstr @ %tmp;
	
	
	}
	return %newstr;//wow but here is our fixed string!

}
function syntaxstring(%str)
{
	//function fixes syntax errors relating to strings when outputted to savecharacter files.
	for(%i = 0; %i < StrLen(%str); %i++)
	{
		%tmp = GetSubStr(%str, %i, 1);
		switch$(%tmp)
		{
			case "\"": %tmp = "\\\"";
			case "\'": %tmp = "\\\'";
		}
		%newstr = %newstr @ %tmp;
	}
	return %newstr;
}
function filefriendly(%str)
{
	for(%i = 0; %i < StrLen(%str); %i++)
	{
		%tmp = GetSubStr(%str, %i, 1);
		switch$(%tmp)
		{
			case "\"": %tmp = "_dq_";
			case "\'": %tmp = "_sq_";
			case "/": %tmp = "_fs_";
			case "\\": %tmp = "_bs_";
			
			case "|": if(isWriteableFilename("characters/test/" @ %tmp @ ".cs")) 
					%tmp = "_vs_";//some servers this character isnt a problem so we will only fix it on servers that have a problem with it.
		}
		%newstr = %newstr @ %tmp;
	}
	return %newstr;
}
function LoadCharacter(%client)
{
	Game.LoadCharacter(%client);
}
function RPGGame::LoadCharacter(%game, %client)
{
	
	//prepare to load the character file
	//lets see, how do we load a saved file...
	if($rules $= "dm")
	return true;//dm doesnt need a char load...
	if(!%client.guid)
	%client.guid = 0;
	//%filename = "characters/rpg2/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs";
    %filename = "characters/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs";
    %oldfilename = "characters/rpg2/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs";
	//%oldfilename = "characters/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs";//new filename -cleaner and hopefully better for some invalid char names. also should accomidate character name changes.
//	%roldfilename = "characters/" @ %client.realname @ "/" @ %client.namebase @ ".cs";// old filename
	if(!isWriteableFileName(%filename))
		return false; //bah someone set the file to readonly, avoid the UE 
			      //dont allow the player to join because we cant save it....
	echo("Unique Client ID:" SPC %client.guid);
 
//don't need right now... uncomment this block if you want to use roldfile stuff
//	if(isfile(%roldfilename) || isfile(%roldfilename @ ".dso"))
//	{
//		%deleteroldfiles=true;
//		%oldfilename = %roldfilename;
//	}

	if(isfile(%oldfilename) || (isfile(%oldfilename @ ".dso")))
	{
		%client.weight = 0;
		//load the character
		exec(%oldfilename);
		%file = new FileObject();
		%newname = RPGFixString(%client.namebase);
		%fname = "temp/" @ %client.namebase @ ".cs";//our eval workaround
		%file.openforwrite(%fname);
		%file.writeline("loadcharacter::d" @ %newname @ "(" @ %client @ ");");
		%file.writeline("echo(\"Character " @ %client.namebase @ " loaded.\");");
		%file.close();
		compile(%fname);
		%client.data = new ScriptObject(CDATA @ %client.guid);
		exec(%fname);
		%file.delete();//delete the file object...
		deletefile(%fname);//bye bye temp file, save memory...
		deletefile(%fname @ ".dso");
		%d = FetchData(%client, "ZoneMusic");
		if (%d $= "") %d = "Wilderness";
		CommandToclient(%client, 'RPGPlayMusic', %d);
		transformolddata(%client);
		%deleteoldfiles = true;

	}
	else
	if(isfile(%filename))
	{
		exec(%filename);
		%client.data = CDATA @ %client.guid;
		%client.choosingteam = false;
		for(%i = 0; (%str = %client.data.invl[%i]) !$= ""; %i++)
		{
			%client.data.itemlist = %client.data.itemlist @ %client.data.invl[%i];
			//echo(%i SPC %client.data.invl[%i]);
			%client.data.invl[%i] = "";

		}
		for(%i = 0; (%str = %client.data.bankl[%i]) !$= ""; %i++)
		{
			%client.data.banklist = %client.data.banklist @ %client.data.bankl[%i];
			//echo(%i SPC %client.data.invl[%i]);
			%client.data.bankl[%i] = "";

		}	
		for(%i = 0; %i < QuestGroup.getCount(); %i++)
		{
			%obj = QuestGroup.getObject(%i);
			%obj.Load(%client);

		}
		%game.ReCreateItemList(%client);
		%weaponid = fetchdata(%client, "WeaponInHand");
		%game.inventoryuse(%client, %weaponid);
		%game.inventoryuse(%client, %weaponid);
	}
	else
	{

		//create a new character! welcome buddy!
		%client.data = new ScriptObject(CDATA @ %client.guid);
		GiveDefaults(%client);
		%client.clan = "";
	}
 
//don't need right now... uncomment this block if you want to use roldfile stuff
//	if(%deleteroldfiles)
//	{
//		deletefile(%roldfilename);
//		deletefile(%roldfilename @ ".dso");
//		Savecharacter(%client);
//	}

	if(%deleteoldfiles)
	{
		deletefile(%oldfilename);
		deletefile(%oldfilename @ ".dso");
		Game.SaveCharacter(%client);
	}
	weightcall(%client);
	
	// Reset stuff that shouldn't be persistent.
	storeData(%client, "tmprecall", false);
	storedata(%client, "SpellCastStep", 0);
	
	return true;
}
function transformolddata(%client)
{
	
		for(%i = 1; $SkillDesc[%i] !$= ""; %i++)
		{
			
			%client.data.PlayerSkill[%i] = %client.PlayerSkill[%i];
			%client.data.SkillCounter[%i] = $SkillCounter[%client, %i];
			//%file.writeline("$SkillCounter[%client, "@ %i @"] = " @ $SkillCounter[%client, %skilltype]+1-1 @ ";");
		}

}
function SaveCharacter(%client, %status)
{
	Game.SaveCharacter(%client, %status);
}

function RPGGame::SaveCharacter(%game, %client)
{
	if(%client.isaicontrolled()) return;
	if(inarena(%client)) return;//temp.
	%itemlist = %client.data.itemlist;
	%banklist = %client.data.banklist;
	
	
	%client.data.itemlist = "";
	//dump inventory string
	for(%i = 0; (%str = getsubStr(%itemlist, %i*255, (%i+1)*255)) !$= ""; %i++)
	{
		%client.data.invl[%i] = %str;
		
	}
	for( %ii = %i ; (%str = %client.data.invl[%ii]) !$= ""; %i++)
	{
		//kill!
		%client.data.invl[%ii] = "";
	}
	%client.data.banklist = "";
	
	for(%i = 0; (%str = getsubStr(%banklist, %i*255, (%i+1)*255)) !$= ""; %i++)
	{
		%client.data.bankl[%i] = %str;
		
	}
	for(%ii = %i ; (%str = %client.data.bankl[%ii]) !$= ""; %i++)
	{
		//kill!
		%client.data.bankl[%ii] = "";
	}	

	%game.storedata(%client, "camppos", %client.player.getTransform());
	//dump quests into the data file
	for(%i = 0; %i < QuestGroup.getCount(); %i++)
	{
		%obj = QuestGroup.getObject(%i);
		%obj.saveclient(%client);

	}
//	%client.data.save("characters/rpg2/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs");
    %client.data.save("characters/" @ %client.guid @ "/" @ FileFriendly(%client.namebase) @ ".cs");
	%client.data.itemlist = %itemlist;
	%client.data.banklist = %banklist;//dont want to save this, AVOID ERRORS!.
	return true;
}
function GiveThisStuff(%client, %list, %echo, %multiplier)
{
	if(%multiplier $= "" || %multiplier <= 0)
		%multiplier = 1;

	%cntindex = 0;

	for(%i = 0; GetWord(%list, %i) !$= ""; %i+=2)
	{
		%w = GetWord(%list, %i);
		%w2 = GetWord(%list, %i+1);

		//if there is a / in %w2, then what trails after the / is the minimum random number between 0 and 100 which
		//is applied as a percentage to the starting number of %w2
		%spos = strstr(%w2, "/");
		if(%spos > 0)
		{
			%original = getSubStr(%w2, 0, %spos);
			%perc = getSubStr(%w2, %spos+1, 99999);

			%r = mfloor(getRandom() * (100-%perc))+%perc+1;
			if(%r > 100) %r = 100;

			%w2 = round(%original * (%r/100));
			if(%w2 < 0) %w2 = 0;
		}

		//if there is a r in %w2 AND it has a number on either side, then it's a dice roll
		%dpos = strstr(%w2, "r");
		if(%dpos !$= -1)
		{
			%l1 = getSubStr(%w2, %dpos-1, 1);
			%l2 = mfloor(%l1);
			%r1 = getSubStr(%w2, %dpos+1, 1);
			%r2 = mfloor(%r1);
			if(%dpos > 0 && %l1 $= %l2 && %r1 $= %r2)
			{
				%w2 = GetRpgRoll(%w2);
				if(%w2 < 1) %w2 = 1;
			}
		}

		%tw2 = %w2 * 1;
		if(%tw2 $= %w2)
			%w2 *= %multiplier;

		if(%w $= "COINS")
		{
			storeData(%client, "COINS", %w2, "inc");
			if(%echo) messageClient(%client, 'GiveThisStuff', "You received " @ %w2 @ " coins.");
		}
		else if(%w $= "EXP")
		{
			storeData(%client, "EXP", %w2, "inc");
			if(%echo) messageClient(%client, 'GiveThisStuff', "You received " @ %w2 @ " experience.");
		}
		else if(%w $= "LCK")
		{
			storeData(%client, "LCK", %w2, "inc");
			if(%echo) messageClient(%client, 'GiveThisStuff', "You received " @ %w2 @ " LCK.");
		}
		else if(%w $= "SP")
		{
			storeData(%client, "SP", %w2, "inc");
			if(%echo) messageClient(%client, 'GiveThisStuff', "You received " @ %w2 @ " Skill Points.");
		}
		else if(%w $= "CLASS")
		{
			storeData(%client, "CLASS", %w2);
			storeData(%client, "GROUP", $ClassGroup[fetchData(%client, "CLASS")]);
		}
		else if(%w $= "LVL")
		{
			//note: the class MUST be specified in %stuff prior to this call
			storeData(%client, "EXP", GetExp(%w2, %client) + 100);
		}
		else if(%w $= "TEAM")
		{
			%client.team = %w2;
			if(%echo) messageClient(%client, 'GiveThisStuff', "Team set to " @ %w2 @ ".");
		}
		else if(%w $= "RACE")
		{
			ChangeRace(%client, %w2);
			if(%echo) messageClient(%client, 'GiveThisStuff', "Race set to " @ %w2 @ ".");
		}
		else if(%w $= "RankPoints")
		{
			storeData(%client, "RankPoints", %w2, "inc");
			if(%echo) messageClient(%client, 'GiveThisStuff', "You received " @ %w2 @ " Rank Points.");
		}
		else if(%w $= "CNT")
		{
			%cntindex++;
			%tmpcnt[%cntindex] = %w2;
		}
		else if(%w $= "CNTAFFECTS")
		{
			%tmpcntaffects[%cntindex] = %w2;
		}
		else
		{
			%client.player.incInventory(%w, %w2);
		}
	}

	RefreshAll(%client);

	//Process the counter data, if any
	for(%i = 1; %tmpcnt[%i] !$= ""; %i++)
	{
		if(%tmpcnt[%i] !$= "" && %tmpcntaffects[%i] !$= "")
		{
			%first = getSubStr(%tmpcnt[%i], 0, 1);
			if(%first $= "+" || %first $= "-")
				$QuestCounter[%client.nameBase, %tmpcntaffects[%i]] += mfloor(%tmpcnt[%i]);
			else
				$QuestCounter[%client.nameBase, %tmpcntaffects[%i]] = mfloor(%tmpcnt[%i]);
		}
	}
}

function round(%n)
{
	if(%n < 0)
	{
		%t = -1;
		%n = -%n;
	}	
	else if(%n >= 0)
		%t = 1;

	%f = mfloor(%n);
	%a = %n - %f;
	if(%a < 0.5)
		%b = 0;
	else if(%a >= 0.5)
		%b = 1;

	return mfloor((%f + %b) * %t);
}

function GetRoll(%roll, %optionalMinMax)
{
	//this function accepts the following syntax, where N is any positive number NOT containing a +:
	//NdN
	//NdN+N
	//NdN-N
	//NdNxN
	//NdN+NxN
	//NdN-NxN

	%d = strstr(%roll, "d");
	%p = strstr(%roll, "+");
	if(%p $= -1)
		%m = strstr(%roll, "-");
	%x = strstr(%roll, "x");

	if(%d $= -1)
		return %roll;

	if(%x $= -1)
		%x = strlen(%roll);

	%numDice = mfloor(getsubstr(%roll, 0, %d));
	if(%p !$= -1)
	{
		%diceFaces = getSubStr(%roll, %d+1, %p-%d-1);
		%bonus = getSubStr(%roll, %p+1, %x-1);
	}
	else if(%p $= -1 && %m !$= -1)
	{
		%diceFaces = getSubStr(%roll, %d+1, %m-%d-1);
		%bonus = -getSubStr(%roll, %m+1, %x-1);
	}
	else
		%diceFaces = getSubStr(%roll, %d+1, 99999);

	%total = 0;
	for(%i = 1; %i <= %numDice; %i++)
	{
		if(%optionalMinMax $= "min")
			%r = 1;
		else if(%optionalMinMax $= "max")
			%r = %diceFaces;
		else
			%r = mfloor(getRandom() * %diceFaces)+1;

		%total += %r;
	}

	if(%bonus !$= "")
		%total += %bonus;

	if(%x !$= strlen(%roll))
		%total *= getSubStr(%roll, %x+1, 99999);

	return %total;
}

function GetRpgRoll(%roll, %optionalMinMax)
{
	//This is a more solid alternative to the AdB notation by AD&D (1d8 for example, A = 1, B = 8)
	//To convert from AdB to NrM, do the following:
	//N = A
	//M = B * A

	//accepts syntax: NrM, where N and M will never be below 0

	%r = strstr(%roll, "r");

	if(%r $= -1)
		return %roll;

	%lb = Cap(mfloor(getsubstr(%roll, 0, %r)), 0, "inf");
	%ub = Cap(mfloor(getsubstr(%roll, %r+1, 99999)), 0, "inf");

	if(%lb > %ub)
		%lb = %ub;

	if(%optionalMinMax $= "min")
		return %lb;
	else if(%optionalMinMax $= "max")
		return %ub;

	%n = round(getRandom() * (%ub - %lb)) + %lb;

	return %n;
}

function CombineRpgRolls(%r1, %r2, %min, %max)
{
	if(%min $= "")
		%min = 0;
	if(%max $= "")
		%max = 100;

	//just to be sure
	%r[1] = %r1;
	%r[2] = %r2;

	for(%i = 1; %r[%i] !$= ""; %i++)
	{
		%find = strstr(%r[%i], "r");
		if(%find $= -1)
		{
			%r[%i] = "0r0";
			%find = strstr(%r[%i], "r");
		}	

		%lb = mfloor(getsubstr(%r[%i], 0, %find));
		%ub = mfloor(getsubstr(%r[%i], %find+1, 99999));

		%flb += %lb;
		%fub += %ub;
	}

	%flb = Cap(%flb, %min, %max);
	%fub = Cap(%fub, %min, %max);

	if(%flb > %fub)
		%flb = %fub;

	return %flb @ "r" @ %fub;
}

function Cap(%n, %lb, %ub)
{
	if(%lb !$= "inf")
	{
		if(%n < %lb)
			%n = %lb;
	}

	if(%ub !$= "inf")
	{
		if(%n > %ub)
			%n = %ub;
	}

	return %n;
}

function GetNESW(%pos1, %pos2)
{
	%v1 = VectorSub(%pos1, %pos2);
	%a = vectorAngle(%v1);
	
	if(%a >= 2.7475 && %a <= 3.15 || %a >= -3.15 && %a <= -2.7475)
		%d = "North";
	else if(%a >= 1.9625 && %a <= 2.7475)
		%d = "North East";
	else if(%a >= 1.1775 && %a <= 1.9625)
		%d = "East";
	else if(%a >= 0.3925 && %a <= 1.1775)
		%d = "South East";
	else if(%a >= -0.3925 && %a <= 0.3925)
		%d = "South";
	else if(%a >= -1.1775 && %a <= -0.3925)
		%d = "South West";
	else if(%a >= -1.9625 && %a <= -1.1775)
		%d = "West";
	else if(%a >= -2.7475 && %a <= -1.9625)
		%d = "North West";

	return %d;
}

function RandomPositionXY(%minrad, %maxrad)
{
	%diff = %maxrad - %minrad;

	%tmpX = mfloor(getRandom() * (%diff*2)) - %diff;
	if(%tmpX < 0)
		%tmpX -= %minrad;
	else
		%tmpX += %minrad;

	%tmpY = mfloor(getRandom() * (%diff*2)) - %diff;
	if(%tmpY < 0)
		%tmpY -= %minrad;
	else
		%tmpY += %minrad;

	return %tmpX @ " " @ %tmpY @ " ";
}

function OddsAre(%n)
{
	%a = mfloor(getRandom() * %n);
	if(%a $= %n-1)
		return true;
	else
		return false;
}

function SetStuffString(%stuff, %item, %amount)
{
	//replaces both Add and Remove stuff string functions by enabling negative values for %amount

	%stuff = FixStuffString(%stuff);

	%pos = strstr(%stuff, " " @ %item @ " ");

	if(%pos !$= -1)
	{
		%a = getSubStr(%stuff, %pos+1, 99999);
		%amt = GetWord(%a, 1);	//getword 0 would be the item, so getword 1 is the amount (which follows the item)

		%part1 = getSubStr(%stuff, 0, %pos+1);
		%part2 = getSubStr(%stuff, %pos+strlen(%item)+strlen(%amt)+3, 99999);

		%b = %amt + %amount;
		if(%b <= 0)
			%part3 = "";
		else
			%part3 = %item @ " " @ %b @ " ";

		%final = %part1 @ %part2 @ %part3;
	}
	else
		%final = %stuff @ %item @ " " @ %amount @ " ";

	return %final;
}

function GetStuffStringCount(%stuff, %item)
{
	%stuff = FixStuffString(%stuff);

	%pos = strstr(%stuff, " " @ %item @ " ");

	if(%pos !$= -1)
	{
		%a = getSubStr(%stuff, %pos+1, 99999);
		%amt = GetWord(%a, 1);

		return %amt;
	}

	return 0;
}

function FixStuffString(%stuff)
{
	%nstuff = " ";
	for(%i = 0; GetWord(%stuff, %i) !$= ""; %i++)
	{
		%w = GetWord(%stuff, %i);
		%nstuff = %nstuff @ %w @ " ";
	}

	return %nstuff;
}

function IsStuffStringEquiv(%s1, %s2, %dblCheck)
{
	//this function COULD be laggy, it all depends on how many items are in %s1.  Below 5, IMO, should be just fine

	%s1 = " " @ %s1;
	%s2 = " " @ %s2;
	for(%x = 0; (%w = GetWord(%s1, %x)) !$= ""; %x+=2)
	{
		%w2 = GetWord(%s1, %x+1);

		if(strstr(%s2, " " @ %w @ " " @ %w2) $= -1)
			return false;
	}
	if(%x $= 0)			//do a dblCheck if %s1 is null.
		%dblCheck = true;

	if(%dblCheck)
	{
		//This will slow down the function, but will get a more accurate reading.
		//If you do NOT do a dblCheck, then %s2 could contain additional items that %s1 does not contain, and still
		//return true.  If this is not a concern, then you don't have to do a dblCheck
		for(%x = 0; (%w = GetWord(%s2, %x)) !$= ""; %x+=2)
		{
			%w2 = GetWord(%s2, %x+1);
	
			if(strstr(%s1, " " @ %w @ " " @ %w2) $= -1)
				return false;
		}
	}

	return true;
}

function GetCombo(%n)
{
	//--- This is used so ComboTables don't get overwritten by simultaneous calls ---
	$w++;
	if($w > 20) $w = 1;
	//-------------------------------------------------------------------------------

	for(%i = 1; $ComboTable[$w, %i] !$= ""; %i++)
		$ComboTable[$w, %i] = "";

	%cnt = 0;

	while(%i !$= -1)
	{
		for(%i = 0; mpow(2, %i) <= %n; %i++){}
		%i--;

		if(%i >= 0)
		{
			$ComboTable[$w, %cnt++] = mpow(2, %i);
			%n -= mpow(2, %i);
		}
	}

	return $w;
}

function IsPartOfCombo(%combo, %n)
{
	%w = GetCombo(%combo);

	%flag = false;

	for(%i = 1; $ComboTable[%w, %i] !$= ""; %i++)
	{
		if(%n $= $ComboTable[%w, %i])
			%flag = true;

		//It's a good idea to clean up after oneself, especially with all the ComboTables that would be floating around
		$ComboTable[%w, %i] = "";
	}

	return %flag;
}
function AddToGroupList(%client, %cl)
{
	if(!IsInGroupList(%client, %cl))
	{
		storeData(%client, "grouplist", AddToCommaList(fetchdata(%client, "grouplist"), %cl.rpgname));
		
		messageClient(%cl, 'AddToGroupList', %client.namebase @ " has added you to his or her group.");
		messageClient(%client, 'AddToGroupList', %cl.namebase @ " has been added to your group.");
	}

}
function RemoveFromGroupList(%client, %cl)
{
	if(IsInGroupList(%client, %cl))
	{
		storeData(%client, "grouplist", RemoveFromCommaList(fetchData(%client, "grouplist"), %cl.rpgname));

		messageClient(%cl, 'RemoveFromGroupList', %client.nameBase @ " has removed you from his or her group.");
		messageClient(%client, 'RemoveFromGroupList', %cl.nameBase @ " has been removed from your group.");
	}

}
function IsInGroupList(%client, %cl)
{
	return IsInCommaList(fetchdata(%client, "grouplist"), %cl.rpgname);
}
function AddToPartyList(%client, %cl, %message)
{
	if(!IsInPartyList(%client, %cl))
	{
		storeData(%client, "partylist", AddToCommaList(fetchdata(%client, "partylist"), %cl));
		if(%message)
		{
			messageClient(%cl, 'AddToPartyList', "You have joined" SPC %client.namebase @ "'s party.");
			messageClient(%client, 'AddToPartyList', %cl.namebase @ " has joined your party.");
		}
	}

}
function RemoveFromPartyList(%client, %cl, %message)
{
	//%client is the owner of the party, %cl is the joinee/leavee
	if(IsInPartyList(%client, %cl))
	{
		storeData(%client, "partylist", RemoveFromCommaList(fetchData(%client, "partylist"), %cl));
		if(%message)
		{
			messageClient(%cl, 'RemoveFrompartyList', "You left the party.");
			messageClient(%client, 'RemoveFrompartyList', %cl.nameBase @ " has left the party.");
		}
	}

}
function IsInPartyList(%client, %cl)
{
	return IsInCommaList(fetchdata(%client, "partylist"), %cl);
}
function AddToTargetList(%client, %cl)
{
	%client.targeting = "";
	if(!IsInTargetList(%client, %cl))
	{
		storeData(%client, "targetlist", AddToCommaList(fetchData(%client, "targetlist"), %cl.rpgname));

		messageClient(%cl, 'AddToTargetList', %client.rpgname @ " wants you dead!  Travel carefully!");
		messageClient(%client, 'AddToTargetList', %cl.rpgname @ " has been notified of your intentions.");

		//schedule(10*60*1000, %client, 'RemoveFromTargetList', %client, %cl, 1);
		//schedule("RemoveFromTargetList(" @ %client @ ", " @ %cl @ ", 1);", 10 * 60);
	}
}
function RemoveFromTargetList(%client, %cl, %forced)
{
	if(IsInTargetList(%client, %cl))
	{
		storeData(%client, "targetlist", RemoveFromCommaList(fetchData(%client, "targetlist"), %cl.rpgname));
		if(%forced)
		{
		messageClient(%cl, 'RemoveFromTargetList', %client.rpgname @ " was forced to declare a truce.");		
		messageClient(%client, 'RemoveFromTargetList', %cl.rpgname @ " has expired on your target-list.");
		}
		else
		{
		messageClient(%cl, 'RemoveFromTargetList', %client.rpgname @ " declared a truce.");		
		messageClient(%client, 'RemoveFromTargetList', %cl.rpgname @ " has been removed from your target-list.");		
		}
	}
}
function IsInTargetList(%client, %cl)
{
	return IsInCommaList(fetchdata(%client, "targetlist"), %cl.rpgname);

}
function AddToCommaList(%list, %item)
{
	%item = RPGFixString(%item);

	%list = %list @ %item @ $sepchar;

	return %list;
}
function RemoveFromCommaList(%list, %item)
{
	%item = RPGFixString(%item);
	%a = $sepchar @ %list;
	%a = strreplace(%a, $sepchar @ %item @ $sepchar, $sepchar);
	
	%a = strreplace(%a, $sepchar @ $sepchar, $sepchar);

	%list = getSubStr(%a, 1, 99999);
	//echo(%list);
	return %list;
}
function IsInCommaList(%list, %item)
{
	%item = RPGFixString(%item);//names might have whatever is used as the sepchar so the fixstring should fix the problem
	
	// = $sepchar @ %list;
	for(%i = 0; GetWordInCommaList(%list, %i) !$= ""; %i++)
		if(GetWordInCommaList(%list, %i) $= %item)
		{
			
			return true;
		}
	return false;
}
function CountObjInCommaList(%list)
{
	for(%i = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getSubStr(%list, %p+1, 99999))
		%cnt++;
	return %cnt;
}
function GetWordInCommaList(%list, %n)
{
	//%list = String::replaceAll(%list, " ", "`");
	%list = String::replaceAll(%list, ",", " ");
	//echo(%list);
	//comma lists dont have spaces anymore =)
	return GetWord(%list, %n);
}

function GetEventCommandIndex(%object, %type)
{
	%list = "";

	//$maxEvents event commands max. per object
	for(%i = 1; %i <= $maxEvents; %i++)
	{
		%t = GetWord($EventCommand[%object, %i], 1);
		if(stricmp(%t, %type) $= 0)
			%list = %list @ %i @ " ";
	}

	if(%list !$= "")
		return getSubStr(%list, 0, strlen(%list)-1);
	else
		return -1;
}

function AddEventCommand(%object, %senderName, %type, %cmd)
{
	for(%i = 1; %i <= $maxEvents; %i++)
	{
		if($EventCommand[%object, %i] $= "" || stricmp(GetWord($EventCommand[%object, %i], 1), %type) $= 0)
		{
			$EventCommand[%object, %i] = %senderName @ " " @ %type @ " " @ %cmd;
			return %i;
		}
	}
	return -1;
}

function ClearEvents(%id)
{
	for(%i = 1; %i <= $maxEvents; %i++)
		$EventCommand[%id, %i] = "";
}

function selectRandomMarker(%groupName)
{
	%group = nameToID(%groupName);
	if(%group !$= -1)
	{
		%markerPos = "0 0 160 1 0 0 0";
		%count = %group.getCount();
		if(%count !$= 0)
			%markerPos = %group.getObject(mfloor(getRandom() * %count)).getTransform();
	}
	return %markerPos;
}
function selectRandomObject(%groupName)
{
	%group = nameToID(%groupName);

	%id = -1;
	if(%group !$= -1)
	{
		%count = %group.getCount();
		if(%count !$= 0)
			%id = %group.getObject(mfloor(getRandom() * (%count - 1)));
	}
	return %id;
}
function selectRandomObjectWithRaceID(%groupName, %raceID)
{
	%group = nameToID(%groupName);

	%id = -1;
	if(%group !$= -1)
	{
		%count = %group.getCount();

		//build index list
		%c = -1;
		%r = mfloor(getRandom() * %count);
		for(%i = %r; %i < %count; %i++)
			%indexList[%c++] = %i;
		for(%i = 0; %i < %r; %i++)
			%indexList[%c++] = %i;

		//parse thru index list and pick the first that matches the raceid
		for(%i = 0; %i < %count; %i++)
		{
			%id = %group.getObject(%indexList[%i]);
			if(%id.RaceID $= %raceID)
				break;
		}
	}

	return %id;
}
function RPGGAME::ClearInventory(%game, %client)
{
	storedata(%client, "inventory", "");
	%client.weight = 0;
}
function ClearInventory(%client)
{
	game.ClearInventory(%client);
}
function RandomRaceSound(%item, %item2, %item3)
{

	return false;
}
function AddToStorage(%client, %id)
{
	return game.AddToStorage(%client, %id);
}
function onAddToStorage(%client, %id)
{
	game.onAddToStorage(%client, %id);
}
function serverCmdRefreshBank(%client)
{
	game.refreshBank(%client);
}
function RemoveFromStorage(%client, %id)
{
	return game.removeFromStorage(%client, %id);
}
function onRemoveFromStorage(%client, %id)
{
	game.onRemoveFromStorage(%client, %id);
}

function CreateItem(%item, %prefix, %suffix)
{
	return game.CreateItem(%item, %prefix, %suffix);
}
function addToInventory(%client, %id, %item, %prefix, %suffix, %equipped)
{
	return game.AddToInventory(%client, %id, %item, %prefix, %suffix, %equipped);
}
function onAddInventory(%client, %id, %item, %equipped)
{
	game.onAddInventory(%client, %id, %item, %equipped);
}
function RemoveFromInventory(%client, %id)
{
	return game.RemoveFromInventory(%client, %id);
}
function onRemoveInventory(%client, %id)
{
	game.onRemoveInventory(%client, %id);
}
function AddToEquipList(%client, %itemid)
{
	game.AddToEquipList(%client, %itemid);
}
function RemoveFromEquipList(%client, %itemid)
{
	game.RemoveFromEquipList(%client, %itemid);
}
function GetEquipList(%client)
{
	return Game.GetEquipList(%client);
}
function getItemCount(%client, %item)
{
	return game.getItemCount(%client, %item);
}
function ClearInvInfo(%itemid)
{
	game.ClearInvInfo(%itemid);
}
function RPGGAME::AddToEquipList(%game, %client, %itemid)
{
	if(ltrim(strreplace(" " @ %client.equiplist, " " @ %itemid @ " ", " ")) $= %client.equiplist) //item already in equip list error
	%client.equiplist = %client.equiplist @ %itemid @ " ";
	else
	error("Item already in equip list" SPC %client.rpgname SPC %client);
}

function RPGGAME::RemoveFromEquipList(%game, %client, %itemid)
{
	%client.equiplist = ltrim(strreplace(" " @ %client.equiplist, " " @ %itemid @ " ", " "));
}
function RPGGAME::GetEquipList(%game, %client)
{
	return %client.equiplist;
}


function GenerateUniqueId()
{
	//when a player connects and his/her inventory is loaded, each item in his/her possession must be reassigned a
	//new id using this function, and all properties for this item are assigned using this new id.  Upon being saved,
	//the id is not conserved as it changes from match to match.
	return 1;
	//return $tmpuniqueid++;
}

function IsDead(%client)
{
	if(%client.client !$= "")
		%client = %client.client;	//in case we passed %player
	if(isobject(%client.player))
	{
		if(%client.player.getState() $= "Dead" )
			return true;
	}
	else
		return true;
	return false;
}

function FellOffMap(%client)
{
	RefreshAll(%client);

	if(%client.isAiControlled())
	{
		storeData(%client, "noDropLootbagFlag", true);
		//kill ai somehow
	}
	else
	{
		//CheckAndBootFromArena(%client);
		%client.player.setVelocity("0 0 0");
		%spawnPoint = RPGGame::pickPlayerSpawn('rpggame', %client);
		%client.player.setTransform(%spawnPoint);

		messageClient(%client, 'FellOffMap', "You were restored to the starting spawn point.");
	}
}

function ClearVariables(%client)
{
	%name = %client.nameBase;

	//clear variables

	//ClearFunkVar(%name);

	for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
	{
		$state[%id, %client] = "";
		if($QuestCounter[%name, %id.name] !$= "")
			$QuestCounter[%name, %id.name] = "";
	}

	for(%i = 1; %i <= $maxDamagedBy; %i++)
		$damagedBy[%name, %i] = "";

	SetAllSkills(%client, "");

	ClearEvents(%client);

	deleteVariables("BonusState" @ %client @ "*");
	deleteVariables("BonusStateCnt" @ %client @ "*");
}
function GiveDmEquipment(%client)
{	

	for(%i = 0; %i < 5; %i = %i + 1)
	{
		%randno = mfloor(getrandom()*$numweapons)+1;
		AddToInventory(%client, GenerateUniqueId(), $weaponList[%randno], mfloor(getrandom()*4)+1, mfloor(getrandom()*5)+1, false);
	}
	AddToInventory(%client, GenerateUniqueId(), PaddedArmor, mfloor(getrandom()*4)+1, mfloor(getrandom()*5)+1, false);

}
function isnull(%test)
{
return (%test $= "" || ( %test !$= "" && %test == 0));
}
function GiveDefaults(%client)
{
	Game.GiveDefaults(%client);
}	
function RPGGame::GiveDefaults(%game, %client)
{
	echo("Giving defaults to " @ %client);

	%name = %client.rpgname;

	AddToInventory(%client, 1, "PickAxe", 3, 1, false);//normal pickaxe
	AddToInventory(%client, 3, "BluePotion", 3, 1, false);
    AddToInventory(%client, 1, "Club", 3, 1, false);//club
	AddToInventory(%client, 2, "CrystalBluePotion", 3, 1, false);
	storeData(%client, "RACE", %client.sex @ "Human");
	storeData(%client, "COINS", $initcoins);

	storeData(%client, "SP", $initsp);
	storeData(%client, "EXP", 0);
	storeData(%client, "campPos", "");
	storeData(%client, "BANK", $initbankcoins);
	storeData(%client, "grouplist", "");
	storeData(%client, "defaultTalk", "#global");
	storeData(%client, "LCK", $initLCK);
	storeData(%client, "PlayerInfo", "");
	storeData(%client, "ignoreGlobal", "");
	storeData(%client, "LCKconsequence", "death");
	storeData(%client, "tmphp", "");
	storeData(%client, "tmpmana", "");
	storeData(%client, "tmpname", %name);
	storeData(%client, "tmpLastSaveVer", $rpgver);
	storeData(%client, "bounty", 0);
	storeData(%client, "isMimic", "");
	storeData(%client, "MyHouse", "");
	storeData(%client, "RankPoints", 0);

	%client.choosingGroup = true;

	SetAllSkills(%client, 0);
	
}
function GiveAIDefaults(%client)
{
	//temp

	echo("Giving defaults to " @ %client);

	%name = %client.nameBase;
	if($rules $= "dm")
	for(%i = 0; %i < 5; %i++)
	{
		%randno = mfloor(getrandom()*$numweapons)+1;
			AddToInventory(%client, 1, $weaponList[%randno], mfloor(getrandom()*4)+1, mfloor(getrandom()*5)+1, false);
	}
	AddToInventory(%client, 1, PaddedArmor, mfloor(getrandom()*4)+1, mfloor(getrandom()*5)+1, false);
	StoreData(%client, "RACE", %client.sex @ %client.race);

	//storeData(%client, "GROUP", "Warrior");
	//storeData(%client, "CLASS", "Fighter");

	storeData(%client, "EXP", 0);
	storeData(%client, "campPos", "");
	storeData(%client, "BANK", $initbankcoins);
	storeData(%client, "grouplist", "");
	//storeData(%client, "defaultTalk", "#say");
	//storeData(%client, "password", $Client::info[%client, 5]);
	storeData(%client, "LCK", 0);
	storeData(%client, "PlayerInfo", "");
	storeData(%client, "ignoreGlobal", "");
	storeData(%client, "LCKconsequence", "miss");
	storeData(%client, "tmphp", "");
	storeData(%client, "tmpmana", "");
	storeData(%client, "tmpname", %name);
	storeData(%client, "tmpLastSaveVer", $rpgver);
	storeData(%client, "bounty", 0);
	storeData(%client, "isMimic", "");
	storeData(%client, "MyHouse", "");
	storeData(%client, "RankPoints", 0);

	%client.choosingGroup = true;

	SetAllSkills(%client, 0);
	SetSkillsToMulti(%client);
}

function TossLootBag(%client, %loot, %coins, %timer)
{
	return Game.TossLootBag(%client, %loot, %coins, %timer);
}
function DeleteLootBag(%this)
{
	%this.delete();
}
function ChangeRace(%client, %race)
{
	if(%client.isaiControlled())
		storedata(%client, "RACE", %race);	
	else if($RaceID[%race] $= 1)
		storeData(%client, "RACE", "DeathKnight");
	else
		storeData(%client, "RACE", %client.sex @ %race);

	setHP(%client, fetchData(%client, "MaxHP"));
	setMANA(%client, fetchData(%client, "MaxMANA"));

	RefreshAll(%client);
}

function RefreshAll(%client)
{

	UpdateTeam(%client);
	UpdateAppearance(%client);
	UpdateTeam(%client);
	refreshHPREGEN(%client);
	refreshMANAREGEN(%client);
	RefreshExp(%client);
	weightstep(%client);
}

function UpdateAppearance(%client)
{
	if(!isobject(%client)) return;
    if(!isobject(%client.player)) return;
	if(%client.isaicontrolled()) return;

	//Determine armor from shields
	%armor = -1;
	%shield = -1;

	%player = %client.player;
	%race = fetchData(%client, "RACE");
	%model = %client.player.getDataBlock().getName();
	%cw = getsubstr(%model, strstr(%model, "Armor"), 99999);
	if(%race $= "fish") return;
	%skinbase = %client.skin;
	%apm = $ArmorPlayerModel[%armor];
	//echo(%cw);
	//=================================
	// Update skin
	//=================================
	if($RaceID[%race] !$= 1)
	{
		%skinbase = 'base';

		if(%armor !$= -1)
			%skinbase = $ArmorSkin[%armor];
		%race = "MaleHuman";//till we get female model
		%p = %race @ %apm @ %cw;

		%armor = 0;
	}
	else
	{
		//DeathKnight
		%skinbase = 'cotp';
		%apm = "";
		%cw = "Armor";
		%armor = 0;
	}

	if(%client.skin !$= %skinbase)
		setTargetSkin(%client.target, %skinbase);

	//=================================
	// Update player model
	//=================================
	if(%armor !$= -1)
		%p = %race @ %apm @ %cw;

	%e = %client.player.getEnergyLevel();
	%d = %client.player.getDamageLevel();
	if(%client.player.getDataBlock().getName() !$= %p && %p !$= "")
	{
		%client.player.setDataBlock(%p);
		%client.player.setEnergyLevel(%e);
		%client.player.setDamageLevel(%d);
	}

	return;

	//=================================
	// Update shields and Orb
	//=================================
	if(%shield !$= -1)
	{
		if(Player::getMountedItem(%client, 2) !$= %shield)
		{
			Player::unmountItem(%client, 2);
			Player::mountItem(%client, %shield, 2);
		}
	}
	else
	{
		if(Player::getMountedItem(%client, 2) !$= -1)
			Player::unmountItem(%client, 2);

		for(%i = 1; $ItemList[Orb, %i] !$= ""; %i++)
		{
			if(Player::getItemCount(%client, $ItemList[Orb, %i] @ "0"))
				Player::mountItem(%client, $ItemList[Orb, %i] @ "0", 2);
		}
	}
}

function UpdateTeam(%client)
{
	%client.team = 1;
}

//I couldn't find a function that did this in T2...
function getClientByName(%name)
{
	%count = ClientGroup.getCount();
	%name = strreplace(%name, "%SP%", " ");
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(stricmp(%cl.nameBase, %name) $= 0 && %cl.player.getstate() !$= "Dead")
			return %cl;
	}
	return -1;
}

function getLOSinfo(%client, %searchRange, %mask)
{
	%player = %client.player;

	if(%mask $= "")
	{
		%mask =	$TypeMasks::VehicleObjectType			| $TypeMasks::MoveableObjectType	|
				$TypeMasks::StaticShapeObjectType	| $TypeMasks::StaticTSObjectType	|
				$TypeMasks::ForceFieldObjectType	| $TypeMasks::ItemObjectType		|
				$TypeMasks::PlayerObjectType		| $TypeMasks::TurretObjectType	|
				$TypeMasks::InteriorObjectType		| $TypeMasks::SensorObjectType	|
				$TypeMasks::TerrainObjectType;
	}

	// get the eye vector and eye transform of the player
	%eyeVec = %player.getEyeVector();
	%eyeTrans = %player.getEyeTransform();

	// extract the position of the player's camera from the eye transform (first 3 words)
	%eyePos = posFromTransform(%eyeTrans);

	// normalize the eye vector
	%nEyeVec = VectorNormalize(%eyeVec);

	// scale (lengthen) the normalized eye vector according to the search range
	%scEyeVec = VectorScale(%nEyeVec, %searchRange);

	// add the scaled & normalized eye vector to the position of the camera
	%eyeEnd = VectorAdd(%eyePos, %scEyeVec);

	// see if anything gets hit
	%searchResult = containerRayCast(%eyePos, %eyeEnd, %mask, %player);

	if(%searchResult $= 0)
		return false;

	$los::object = firstWord(%searchResult);
	$los::position = getWords(%searchResult, 1, 3);
	$los::rotation = getWords(%searchResult, 4, 6);

	return %searchResult;
}

function Player::setPosition(%player, %pos)
{
	%trans = %player.getTransform();
	%newtrans = %pos @ " " @ GetWord(%trans, 3) @ " " @ GetWord(%trans, 4) @ " " @ GetWord(%trans, 5) @ " " @ GetWord(%trans, 6);
	%player.setTransform(%newtrans);
}

function getTime()
{
	return getSimTime() / 1000;
}

function AllowedToSteal(%client)
{
	if(fetchData(%client, "InSleepZone") == true)
		return "You can't steal inside a sleeping area.";
	//else if(Zone::getType(fetchData(%client, "zone")) $= "PROTECTED")
	//	return "You can't steal from someone in protected territory.";

	return "true";
}

function Player::clearDamagedBy(%player, %index)
{
	%player.client.damagedBy[%index] = "";
}

function RPGGame::clearWeaponDelayFlag(%game, %client)
{
	%client.weaponDelayFlag = "";
}

function Revert(%client)
{
	if(%client.possessId !$= "")
		%client.possessId.setControlObject(%client.possessId.player);

	%client.setControlObject(%client.player);
	%client.possessId.possessedBy = "";
	%client.possessId = "";
}

function RefreshExp(%client)
{
	if(fetchData(%client, "HasLoadedAndSpawned"))
	{
		if(fetchData(%client, "templvl") !$= "")
		{
			if(GetLevel(fetchData(%client, "EXP"), %client) !$= fetchData(%client, "templvl"))
			{
				//client has leveled up
				%lvls = (GetLevel(fetchData(%client, "EXP"), %client) - fetchData(%client, "templvl"));
	
				storeData(%client, "SP", (%lvls * $SPgainedPerLevel), "inc");
	
				if(%lvls > 0)
				{
					if(%lvls $= 1)
						messageClient(%client, 'RefreshExp', $MsgOlive @ "You have gained a level!");		
					else
						messageClient(%client, 'RefreshExp', $MsgOlive @ "You have gained " @ %lvls @ " levels!");

					messageClient(%client, 'RefreshExp', $MsgWhite @ "Welcome to level " @ fetchData(%client, "LVL"));
					%client.player.playAudio(0, LevelUp);
				}
				else if(%lvls < 0)
				{
					if(%lvls $= -1)
						messageClient(%client, 'RefreshExp', $MsgRed @ "You have lost a level...");		
					else
						messageClient(%client, 'RefreshExp', $MsgRed @ "You have lost " @ -%lvls @ " levels...");

					messageClient(%client, 'RefreshExp', $MsgDarkGrey @ "You are now level " @ fetchData(%client, "LVL"));
				}
			}
			storeData(%client, "templvl", GetLevel(fetchData(%client, "EXP"), %client));
		}
	}
}

function HardcodeAIskills(%client)
{
	SetAllSkills(%client, 0);
	
	%ns = getNumSkills();
	%a = $autoStartupSP + round($initSPcredits / %ns) + round(((fetchData(%client, "LVL")-1) * $SPgainedPerLevel) / %ns);
	for(%i = 1; %i <= %ns; %i++)
		AddSkillPoint(%client, %i, %a);

	//==== HARDCODED SKILLS TO ENSURE CHALLENGING BOTS ============
	%client.data.PlayerSkill[$SkillSlashing] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillPiercing] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillBludgeoning] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillDodging] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillArchery] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillOffensiveCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillDefensiveCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillNeutralCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillEnergy] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillSpeech] = $SkillCap;
	%client.data.PlayerSkill[$SkillWeightCapacity] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)+10;
	%client.data.PlayerSkill[$SkillEndurance] = ( (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)/2 );

	%a = (  (getRandom() * $SkillRangePerLevel) + ((fetchData(%client, "LVL")-1) * $SkillRangePerLevel)  ) / 2;
	%sr = round(%a * GetSkillMultiplier(%client, $SkillOffensiveCasting));
	%client.data.PlayerSkill[$SkillSpellResistance] = %sr;
	//=============================================================
}

function vectorAngle(%v)
{
	//Resembles the discontinued T1 function Vector::getRotation
	%a = mATan(GetWord(%v, 0), GetWord(%v, 1));
	return -%a;
}

function whichWord(%text, %sub)
{
	%sub = Trim(%sub);
	for(%i = 0; (%w = Trim(GetWord(%text, %i))) !$= ""; %i++)
	{
		if(stricmp(%w, %sub) $= 0)
			return %i;
	}
	return "";
}
function lastWord(%text)
{
	return GetWord(%text, getWordCount(%text)-1);
}

function countWords(%list)
{
	for(%i = 0; GetWord(%list, %i) !$= ""; %i++){}

	return %i;
}

function ParseInventory(%list, %type)
{
	%flist = "";

	for(%i = 0; (%w = GetWord(%list, %i)) !$= ""; %i++)
	{
		if($ItemType[GetItem(%w)] $= %type)
			%flist = %flist @ %w @ " ";
	}

	return %flist;
}

function isnumeric(%val)
{
	if(%val * 1 == 0 && %val !$= "")
		return false;
	else
		return true;
}
function DisplayGetInfo(%toclient, %infoclient)
{
	%time = 15;
	%line = "<COLOR:FF0000>Name:<COLOR:FFFFFF>" SPC %infoclient.rpgname SPC "<COLOR:FF0000>LVL:<COLOR:FFFFFF>" SPC fetchdata(%infoclient, "LVL") SPC "<COLOR:FF0000>GROUP:<COLOR:FFFFFF>" SPC fetchdata(%infoclient, "GROUP") SPC "<COLOR:FF0000>CLASS:<COLOR:FFFFFF>" SPC fetchdata(%infoclient, "CLASS") SPC "\n<COLOR:FF0000>Info:<COLOR:FFFFFF>" SPC fetchData(%infoclient, "PlayerInfo");
	SendRPGBottomPrint(%toclient, %time, 2, %line);

}

function DoConsider(%client) {
	%retval = false;
	if(%client.isaiControlled()) return false;
	%ret = getLOSInfo(%client, 20);
	%obj = GetWord(%ret, 0);
	
	if(!isObject(%obj)) {
       return;
	}
	else if(%obj.pDesc !$= "")
	{
		
		MessageClient(%client, 'Description', 'You think: %1', %obj.pDesc);
		//messageclient(%client, "Hrm");
		%retval = true;
	}
	else if (%obj.getDataBlock().classname $= Armor)
	{
		
		DisplayGetInfo(%client, %obj.client);
		%retval = true;
	}
	else if (%obj.getDatablock().getName() $= "RPGBoat")
	{
		RPGchat(%client, 0, "#mount", %client.rpgname);
	}
	if(!%retval)
	{
		messageClient(%client, 'FailedDescription', 'You find nothing of interest here');
	}
	return %retval;
}

function Client::Hide(%client)
{
	storedata(%client, "invisible", true);
	storedata(%client, "blockhide", true);
	%client.player.setCloaked(true);
}
function Client::unHide(%client)
{
	storedata(%client, "invisible", false);
	storedata(%client, "blockhide", false);
	%client.player.setCloaked(false);
}

function WalkSlowInvisLoop(%client, %time, %grace)
{

	if(fetchdata(%client, "invisible") == false)
	{
		cancel(%client.invisloop);

		%client.lastinpos = "";
		return;
	}
	
	if(%client.lastinpos !$= "")
	{
		if(round(VectorDist(%client.lastinpos, %client.player.getPosition())) > %grace)
		{
			//echo FAIL, unhide
			Client::unHide(%client);
		}
	}
	else
	{
		//first call... do nothing.
	}
	%client.lastinpos = %client.player.getPosition();
	%client.invisloop = schedule(%time*1000, %client, "walkslowinvisloop", %client, %time, %grace);
}
function get3dDistance(%tran1, %tran2)
{

	%x1 = getword(%tran1, 0);
	%y1 = getword(%tran1, 1);
	%z1 = getword(%tran1, 2);
	%x2 = getword(%tran2, 0);
	%y2 = getword(%tran2, 1);
	%z2 = getword(%tran2, 2);

	%d1 = mpow(%x1*%x1+%y1*%y1+%z1*%z1, 0.5);
	%d2 = mpow(%x2*%x2+%y2*%y2+%z2*%z2, 0.5);

	return %d1-%d2;
}
function newHasLOStoClient(%client, %target)
{
	//echo(%client);
	getLOSinfo(%client, 5000);

	%pl = $los::object;
	if(%pl.getClassName() $= "Player")
	{
		return true;
	}
		
	return false;
}

//t^2+y^2 = s^2
//t = (x^2 + z^2)^.5
