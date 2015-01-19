function setMANA(%client, %val)
{
	%armor = %client.player.getDataBlock();

	if(%val $= "")
		%val = fetchData(%client, "MaxMANA");

	%a = %val * %armor.maxEnergy;
	%b = %a / fetchData(%client, "MaxMANA");

	if(%b < 0)
		%b = 0;
	else if(%b > %armor.maxEnergy)
		%b = %armor.maxEnergy;

	%client.player.setEnergyLevel(%b);
}
function refreshMANA(%client, %value)
{
	setMANA(%client, (fetchData(%client, "MANA") - %value));
}
function refreshMANAREGEN(%client)
{
	if(isobject(%client) && isobject(%client.player))
	{
		%b = 0;

		if(%client.sleepMode $= 1)
			%b = 1.0;
		else if(%client.sleepMode $= 2)
			%b = 2.25;

		%c = AddPoints(%client, 11) / 10000;
		%r = %b/20 + %c + 0.02;

		%client.player.setRechargeRate(%r);
		return true;
	}
	else
		return false;
}