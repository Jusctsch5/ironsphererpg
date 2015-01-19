function setHP(%client, %val)
{
	if(!%client.player)
	return;
	%armor = %client.player.getDataBlock();

	if(%val < 0)
		%val = 0;
	if(%val $= "")
		%val = fetchData(%client, "MaxHP");

	%a = %val * %armor.maxDamage;
	%b = %a / fetchData(%client, "MaxHP");
	%c = %armor.maxDamage - %b;

	if(%c < 0)
		%c = 0;
	else if(%c > %armor.maxDamage)
		%c = %armor.maxDamage;
  
    // <signal360>
    if(%c $= %armor.maxDamage)
	{
        if (fetchData(%client, "LCK") >= 1)
        {
            if (fetchData(%client, "LCKconsequence") $= "miss")
    		{
                %c = %client.player.getDamageLevel();
                %val = -1;
    		}
            storeData(%client, "LCK", 1, "dec");
            messageclient(%client, 'LCKgone', "You have lost an LCK point!");
        }
	}
    // </signal360>
	%client.player.setDamageLevel(%c);

	return %val;
}
function refreshHP(%client, %value)
{
	return setHP(%client, fetchData(%client, "HP") - round(%value * $TribesDamageToNumericDamage));
}
function refreshHPREGEN(%client)
{
	if(!%client.player)
	return;
	//%a = %client.PlayerSkill[$SkillHealing] / 25000000;
	//if(%client.sleepMode $= 1)
	//	%b = %a + 0.002;
	//else if(%client.sleepMode $= 2)
	//	%b = %a;
	//else
	//	%b = %a;

	//%c = AddPoints(%client, 10) / 2000;

	//%r = %b + %c;

	%client.player.setRepairRate(0);
	if(!isEventPending(%client.healfunction))
	{
		%client.lasthittime = 0;
		%client.heathticker = 0;
		HealRegain(%client);
	}
}

function HealRegain(%client)
{
	if(%client.player)
	%client.healfunction = schedule(1000, %client, "healregain", %client);
	%time = getSimTime();
	if(%client.lasthittime + 15 < %time)
	{
		//heal rate will be for each skill point 1 hp over 3 min
		%skill = GetPlayerSkill(%client, $Skill::Healing);
		%a = 0;
		if(%client.sleepMode $= 1)
		{
			%a = fetchdata(%client, "maxHP")/30;
			%a = mfloor(%a);
			if(%a < 1) %a = 1;
		}
		%client.healthticker += %skill / (60*3) + %a;
		
		%rem = mfloor(%client.healthTicker);
		%client.healthticker -= %rem;
		if(%rem > 0)
		{
			refreshHP(%client, - (%rem / $TribesDamageToNumericDamage) );
		}
		
	}

}
