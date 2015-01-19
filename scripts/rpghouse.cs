$HouseName[1] = "House Antiva";
$HouseName[2] = "House Fenyar";
$HouseName[3] = "House Temmin";
$HouseName[4] = "House Venk";

//$HouseStartUpEq[1] = "AntivaRobe 1";
//$HouseStartUpEq[2] = "FenyarRobe 1";
//$HouseStartUpEq[3] = "TemminRobe 1";
//$HouseStartUpEq[4] = "VenkRobe 1";
$HouseStartUpEq[1] = "";
$HouseStartUpEq[2] = "";
$HouseStartUpEq[3] = "";
$HouseStartUpEq[4] = "";

$joinHouseRankPoints = 4;

function GetHouseNumber(%n)
{
	for(%i = 1; $HouseName[%i] !$= ""; %i++)
	{
		if(stricmp($HouseName[%i], %n) $= 0)
			return %i;
	}
	return "";
}

function BootFromCurrentHouse(%client, %echo)
{
	%h = fetchData(%client, "MyHouse");

	if(%h != "")
	{
		UnequipMountedStuff(%client);

		%hn = GetHouseNumber(%h);
		if(%echo) messageClient(%client, 'BootFromCurrentHouse', "You have been booted from " @ $HouseName[%hn] @ " and have lost all rank points.");

		storeData(%client, "MyHouse", "");
		storeData(%client, "RankPoints", 0);

		return %hn;
	}
	else
		return -1;
}

function JoinHouse(%client, %hn, %echo)
{
	storeData(%client, "MyHouse", $HouseName[%hn]);
	storeData(%client, "RankPoints", $joinHouseRankPoints);

	if(%echo) messageClient(%client, 'JoinHouse', "You have joined " @ $HouseName[%hn] @ " and have been awarded " @ $joinHouseRankPoints @ " rank points.");
}