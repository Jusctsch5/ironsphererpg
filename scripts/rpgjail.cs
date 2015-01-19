function RPGGame::GetRandomJailNumber(%game)
{
	return getRandom(1,JailGroup.getCount());
}
function RPGGame::ValidateJailNumber(%game, %val)
{
	if(mfloor(%val) != %val)
	return false;
	if(%val > JailGroup.getCount() || %val < 1)
	return false;
	
	return true;
}
function RPGGame::GetPositionForJailNumber(%game, %jn)
{
	return JailGroup.getObject(%jn-1).getPosition();
}
function RPGGame::Jail(%game, %client, %time, %jn)
{
	%pos = %game.GetPositionForJailNumber(%jn);
	AddBonusState(%client, "99" SPC %jn, %time/1000, "Jailed");
	%client.player.setVelocity("0 0 0");
	%client.player.setPosition(%pos);
	if(IsEventPending(%client.jailfelloffmap))
		cancel(%client.jailfelloffmap);
	if(IsEventPending(%client.spellcast))
		cancel(%client.spellcast);
		
	%client.jailfelloffmap = schedule(%time, %client, "felloffmap", %client);
	
	commandtoclient(%client, 'StartRecastDelayCountdown', %time);
	messageClient(%client, 'jail', "You have been jailed for " @ %time/1000 @ " seconds");
}
function RPGGame::IsJailed(%game, %client)
{
	return (%game.GetJailTime(%client) > 0);
}
function RPGGame::GetJailTime(%game, %client)
{
	return GetBonusTimeLeft(%client, "Jailed");
}
function RPGGame::GetJailNumber(%game, %client)
{
	return AddBonusStatePoints(%client, 99);
}
