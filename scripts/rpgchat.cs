function RPGchat(%client, %team, %message, %senderName)
{
	if(%client.IsInvalid)
		return;
	if(isobject(%client) == false)
	return;
	//-------------------------//
	%TrueClientId = %client;
	if(%senderName !$= "")
	{
		%client = 2048;
		%clientToServerAdminLevel = $BlockOwnerAdminLevel[%senderName];
	}
	else
	{
		%senderName = %client.nameBase;
		%clientToServerAdminLevel = mfloor(%client.adminLevel);
	}
	if(%client.isAIControlled())
		%clientToServerAdminLevel = 3;

	if(%TrueClientId $= 2048)
		%echoOff = true;
	else
		%echoOff = %TrueClientId.echoOff;

	%TCsenderName = %TrueClientId.rpgName;

	//If %senderName is empty, the rest of this function will continue normally, as both %TrueClientId and %client
	//are identical.  However, if %senderName is NOT empty, messages that the server should hear will be under %client,
	//and messages that the client RUNNING the script needs to hear will be under %TrueClientId.
	//During %senderName being NOT empty, basic player command messages are sent to the server.  These commands shouldn't
	//normally be invoked anyway, unless the scripter forces it somehow.  Block management commands should use
	//%TrueClientId because they can only be run WHILE the client is in-game, so the messages should be sent to him.
	//The rest of the commands should use %client because those are the ones that the server will be calling.

	//An easy to way to distinguish the tasks between client and server is that the client runs the commands that
	//manage, while the server runs the commands that do actions.

	//- %TrueClientId should be assigned to things that require client access, and need to send a message
	//  (like a confirmation or error message) to someone.
	//- %client should be assigned to things that do actions.

	//%TrueClientId will only become 2048 if the client leaves the game.

	//Remember that if a client disconnects, the %TrueClientId will become 2048, the same as %client.  This means
	//that the server will then be receiving all these messages.

	//I had to write this little commentary because I was getting confused myself...

	//NEW:
	//- %TrueClientId should be assigned to things that involve the player at hand
	//- %client should be assigned to things that involve control
	//-------------------------//

	%time = getTime();
	if(%time - %client.lastSayTime <= $sayDelay && !(%clientToServerAdminLevel >= 1))
		return;
	%client.lastSayTime = %time;

	//check for a bulknum-type of message
	if(%message $= mfloor(%message))
	{
		if(%client.currentShop !$= "" || %client.currentBank !$= "")
		{
			if(%message < 1)
				%message = 1;
			if(%message > 100)
				%message = 100;
		}
		%TrueClientId.bulkNum = %message;
	}

	//parse message
	%botTalk = false;
	%isCommand = false;

	if(getsubstr(%message, 0, 1) !$= "#")
	{
		if(%team $= 1)
			%message = "#zone " @ %message;
		else
			%message = fetchData(%TrueClientId, "defaultTalk") @ " " @ %message;
	}
	if(getsubstr(%message, 0, 1) $= "#")
		%isCommand = true;

	if($exportChat)
	{
		%ip = %TrueClientId.getAddress();
		if(%TrueClientId.doExport)
		{
			$log::msg["[\"" @ %TCsenderName @ "\"]"] = %message;
			export("log::msg[\"" @ %TCsenderName @ "\"*", "temp\\log-" @ %TCsenderName @ ".cs", true);
			$log::msg["[\"" @ %TCsenderName @ "\"]"] = "";//memory
		}
	}

	%w1 = GetWord(%message, 0);

	//========== Redirect block commands into memory =============================================
	if(fetchData(%TrueClientId, "BlockInputFlag") !$= "" && stricmp(%w1, "#endblock") !$= 0 && %w1 !$= -1 && %message !$= "")
	{
		//Entering block information into memory
		%tmpBlockCnt = fetchData(%TrueClientId, "tmpBlockCnt") + 1;
		storeData(%TrueClientId, "tmpBlockCnt", %tmpBlockCnt);
		$BlockData[%TCsenderName, fetchData(%TrueClientId, "BlockInputFlag"), %tmpBlockCnt] = %message;
		return 0;
	}
	//============================================================================================

	%cropped = getsubstr(%message, (strlen(%w1)+1), 99999);

	if(%isCommand)
	{
		switch$(%w1)
		{
			case "#say":
				if(SkillCanUse(%TrueClientId, "#say"))
				{
					if(%TrueClientID.player)// does player exist
					{
						
						%count = ClientGroup.getCount();
						for(%icl = 0; %icl < %count; %icl++)
						{
							%cl = ClientGroup.getObject(%icl);
							if(isobject(%cl.player))
							{
								%talkingPos = %TrueClientId.player.getPosition();
								%receivingPos = %cl.player.getPosition();
								%distVec = VectorDist(%talkingPos, %receivingPos);
								if(%distVec <= $maxSAYdistVec)
								{
									//%newmsg = FadeMsg(%cropped, %distVec, $maxSAYdistVec);
									%newmsg = %cropped;

									if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
										messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " says, \"" @ %newmsg @ "\"");
								}
							}
						}
						messageClient(%TrueClientId, 'RPGchatCallback', "You say, \"" @ %cropped @ "\"");
						UseSkill(%TrueClientId, $SkillSpeech, true, true);

						%botTalk = true;
					}
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
	
			case "#shout":
				if(SkillCanUse(%TrueClientId, "#shout"))
				{
					%count = ClientGroup.getCount();
					for(%icl = 0; %icl < %count; %icl++)
					{
						%cl = ClientGroup.getObject(%icl);

						%talkingPos = %TrueClientId.player.getPosition();
						%receivingPos = %cl.player.getPosition();
						%distVec = VectorDist(%talkingPos, %receivingPos);
						if(%distVec <= $maxSHOUTdistVec)
						{
							//%newmsg = FadeMsg(%cropped, %distVec, $maxSHOUTdistVec);
							%newmsg = %cropped;
	
							if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
								messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " shouts, \"" @ %newmsg @ "\"");
						}
					}
					messageClient(%TrueClientId, 'RPGchatCallback', "You shouted, \"" @ %cropped @ "\"");
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
	
					%botTalk = true;
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}

			case "#whisper":
				if(SkillCanUse(%TrueClientId, "#whisper"))
				{
					%count = ClientGroup.getCount();
					for(%icl = 0; %icl < %count; %icl++)
					{
						%cl = ClientGroup.getObject(%icl);

						%talkingPos = %TrueClientId.player.getPosition();
						%receivingPos = %cl.player.getPosition();
						%distVec = VectorDist(%talkingPos, %receivingPos);
						if(%distVec <= $maxWHISPERdistVec)
						{
							//%newmsg = FadeMsg(%cropped, %distVec, $maxWHISPERdistVec);
							%newmsg = %cropped;
	
							if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
								messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " whispers, \"" @ %newmsg @ "\"");
						}
					}
					messageClient(%TrueClientId, 'RPGchatCallback', "You whisper, \"" @ %cropped @ "\"");
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
	
					%botTalk = true;
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
		}

		if(Game.IsJailed(%TrueClientId) && %clienttoServeradminlevel < 5)
			return;

		switch$(%w1)
		{
			case "#tell":
				if(SkillCanUse(%TrueClientId, "#tell"))
				{
					if(%cropped $= "")
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #tell whoever, message");
					}
					else
					{
						%pos1 = 0;
						%pos2 = strstr(%cropped, ",");
						%name = getsubstr(%cropped, %pos1, %pos2-%pos1);
						%final = getsubstr(%cropped, %pos2 + 2, strlen(%cropped)-%pos2-2);
						%cl = getClientByName(%name);
			
						if(%cl !$= -1)
						{
							%n = %cl.nameBase;	//capitalize the name properly
							if(!%cl.muted[%TrueClientId])
							{
								messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " tells you, \"" @ %final @ "\"");
								if(%cl !$= %TrueClientId)
									messageClient(%TrueClientId, 'RPGchatCallback', "You tell " @ %n @ ", \"" @ %final @ "\"");
								%cl.replyTo = %TCsenderName;
	
								UseSkill(%TrueClientId, $SkillSpeech, true, true);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', %n @ " has muted you.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
			
					%botTalk = true;
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}

			case "#r":
				if(SkillCanUse(%TrueClientId, "#tell"))
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #r message");
					else
					{
						%name = %TrueClientId.replyTo;
						if(%name !$= "")
						{
							%cl = getClientByName(%name);
				
							if(%cl !$= -1)
							{
								if(!%cl.muted[%TrueClientId])
								{
									messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " replies, \"" @ %cropped @ "\"");
									if(%cl !$= %TrueClientId)
										messageClient(%TrueClientId, 'RPGchatCallback', "You reply to " @ %name @ ", \"" @ %cropped @ "\"");
									%cl.replyTo = %TCsenderName;
			
									//UseSkill(%TrueClientId, $SkillSpeech, true, true);
								}
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				
							%botTalk = true;
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "You haven't received a #tell to reply to yet.");
					}
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#global" or "#g":
				if(SkillCanUse(%TrueClientId, "#global"))
				{
					if(!fetchData(%TrueClientId, "ignoreGlobal"))
					{
						%count = ClientGroup.getCount();
						for(%icl = 0; %icl < %count; %icl++)
						{
							%cl = ClientGroup.getObject(%icl);
				                  if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && !fetchData(%cl, "ignoreGlobal"))
				                        messageClient(%cl, 'RPGchatCallback', "[GLBL] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
						}
						messageClient(%TrueClientId, 'RPGchatCallback', "[GLBL] \"" @ %cropped @ "\"");
	
						//UseSkill(%TrueClientId, $SkillSpeech, true, true);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't send a Global message when ignoring other Global messages.");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;
			case "#guild" or "#gu":
				if(SkillCanUse(%TrueClientId, "#guild"))
				{
					%guildid = IsInWhatGuild(%client);
					%guild = GuildGroup.GetObject(%guildid);
					if(%guildid != -1 || %guild.GetGUIDRank(%TrueClientId.guid) == 0)
					{
						//ok user is in a guild so we get the guild and obtain the list.
						
						
						
						%count = ClientGroup.getCount();
						for(%icl = 0; %icl < %count; %icl++)
						{
							%cl = ClientGroup.getObject(%icl);
							if(%cl.isAiControlled()) continue; //skip bots
							
							if(%guild.GUIDinGuild(%cl.guid) && %guild.GetGUIDRank(%cl.guid) > 0 && (!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId) )
								messageClient(%cl, 'RPGchatCallback'  ,"[GUILD] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
							
						}
						messageClient(%TrueClientID, 'RPGchatCallback', "[GUILD] \"" @ %cropped @ "\"");
						UseSkill(%TrueClientId, $SkillSpeech, true, true);
					}
					else
						messageClient(%TrueClientID, 'RPGchatcallback', "You are not in a guild.");
				
				
				}
				else
					messageClient(%TrueClientID, 'RPGchatCallback', "You lack the necessary skills to use this command.");
			
				return;
			case "#zone" or "#z":
				if(SkillCanUse(%TrueClientId, "#zone"))
				{
					%count = ClientGroup.getCount();
					for(%icl = 0; %icl < %count; %icl++)
					{
						%cl = ClientGroup.getObject(%icl);
						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && fetchData(%cl, "zone") $= fetchData(%TrueClientId, "zone"))
				      		messageClient(%cl, 'RPGchatCallback', "[ZONE] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					}
					messageClient(%TrueClientId, 'RPGchatCallback', "[ZONE] \"" @ %cropped @ "\"");
	
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#group":
				if(SkillCanUse(%TrueClientId, "#group"))
				{
					%count = ClientGroup.getCount();
					for(%icl = 0; %icl < %count; %icl++)
					{
						%cl = ClientGroup.getObject(%icl);

						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && IsInGroupList(%TrueClientId, %cl))
						{
							if(IsInGroupList(%cl, %TrueClientId))
								messageClient(%cl, 'RPGchatCallback', "[GRP] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
							else
								messageClient(%TrueClientId, 'RPGchatCallback', %cl.nameBase @ " does not have you on his/her group-list.");
						}
					}
	
					messageClient(%TrueClientId, 'RPGchatCallback', "[GRP] \"" @ %cropped @ "\"");
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#party" or "#p":
				if(SkillCanUse(%TrueClientId, "#party"))
				{
					%list = GetPartyListIAmIn(%TrueClientId);
					
					for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
					{
						%cl = getsubstr(%list, 0, %p);
						
						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
							messageClient(%cl, 'RPGchatCallback', "[PRTY] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					}
	
					messageClient(%TrueClientId, 'RPGchatCallback', "[PRTY] \"" @ %cropped @ "\"");
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;
			case "#leavearena":
				if(inArenaroster(%TrueClientId))
				{
					leaveArena(%TrueClientId);
				
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You must be in the arena wait room in order to leave it");
		}

		if(IsDead(%TrueClientId) && %TrueClientId !$= 2048)
			return;

		//check for onHear events
		if(%botTalk)
		{
			%count = ClientGroup.getCount();
			for(%icl = 0; %icl < %count; %icl++)
			{
				%oid = ClientGroup.getObject(%icl);

				%time = getTime();
				if(%time - fetchData(%oid, "nextOnHear") > 0.05)
				{
					storeData(%oid, "nextOnHear", %time);
	
					%oname = %oid.nameBase;
	
					%index = GetEventCommandIndex(%oid, "onHear");
					if(%index !$= -1)
					{
						for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
						{
							%ec = $EventCommand[%oid, %index2];
		
							%hearName = GetWord(%ec, 2);
							%radius = GetWord(%ec, 3);
							if(VectorDist(%oid.player.getPosition(), %TrueClientId.player.getPosition()) <= %radius)
							{
								%targetname = GetWord(%ec, 5);
								if(stricmp(%targetname, "all") !$= 0)
									%targetId = getClientByName(%targetname);
		
								if(stricmp(%targetname, "all") $= 0 || %targetId $= %TrueClientId)
								{
									%sname = GetWord(%ec, 0);
									%type = GetWord(%ec, 1);
									%keep = GetWord(%ec, 4);
									%var = GetWord(%ec, 6);
									if(stricmp(%var, "var") $= 0)
										%var = true;
									else
									{
										%div1 = strstr(%ec, "|");
										%div2 = String::ofindSubStr(%ec, "|", %div1+1);
										%text = getsubstr(%ec, %div1+1, %div2);
										%oec = getsubstr(%ec, %div1+%div2+2, 99999);
									}
		
									if(stricmp(%cropped, %text) $= 0 || %var)
									{
										if((%cl = getClientByName(%sname)) $= -1)
											%cl = 2048;

										%cmd = getsubstr($EventCommand[%oid, %index2], strstr($EventCommand[%oid, %index2], ">")+1, 99999);
										if(%var)
											%cmd = strreplace(%cmd, "^var", %cropped);
		
										%pcmd = ParseBlockData(%cmd, %TrueClientId, "");
										if(!%keep)
											$EventCommand[%oid, %index2] = "";
										RPGchat(%cl, 0, %pcmd); //, %sname);
									}
								}
							}
						}
					}
				}
			}
		}

		//=================================================
		// Beginning of commands
		// (player can't use any of these while dead)
		//=================================================

		switch$(%w1)
		{
			case "#steal":
				%time = getTime();
				if(%time - %TrueClientId.lastStealTime > $stealDelay)
				{
					%TrueClientId.lastStealTime = %time;
		
					if((%reason = AllowedToSteal(%TrueClientId)) $= "true")
					{
						if(SkillCanUse(%TrueClientId, "#steal"))
						{
							if(getLOSinfo(%TrueClientId, 1))
							{
								%id = $los::object.client;
								if($los::object.getClassName() $= "Player")
								{
									%victimName = %id.nameBase;
									%stealerName = %TCsenderName;
									%victimCoins = fetchData(%id, "COINS");
									%fail = false;
									if(%victimCoins > 0)
									{
										%r1 = GetRpgRoll("1r" @ (%TrueClientId.PlayerSkill[$SkillStealing] * (4/5)));
										%r2 = GetRpgRoll("1r" @ %id.PlayerSkill[$SkillStealing]);
										%a = %r1 - %r2;
										if(%a > 0)
										{
											%amount = mfloor(%a * getRandom() * 1.2);
											if(%amount > %victimCoins)
												%amount = %victimCoins;
		
											if(%amount > 0)
											{
												storeData(%TrueClientId, "COINS", %amount, "inc");
												storeData(%id, "COINS", %amount, "dec");
												PerhapsPlayStealSound(%TrueClientId, 0);

												messageClient(%TrueClientId, 'RPGchatCallback', "You successfully stole " @ %amount @ " coins from " @ %victimName @ "!");
				
												RefreshAll(%TrueClientId);
												RefreshAll(%id);
		
												UseSkill(%TrueClientId, $SkillStealing, true, true);
												PostSteal(%TrueClientId, true, 0);
											}
											else
												%fail = true;
										}
										else
											%fail = true;
		
										if(%fail)
										{
											messageClient(%TrueClientId, 'RPGchatCallback', "You failed to steal from " @ %victimName @ "!");
											messageClient(%id, 'RPGchatCallback', %stealerName @ " just failed to steal from you!");
		
											UseSkill(%TrueClientId, $SkillStealing, false, true);
											PostSteal(%TrueClientId, false, 0);
										}
									}
									else
									{
										messageClient(%TrueClientId, 'RPGchatCallback', %victimName @ " doesn't appear to be carrying any coins...");
									}
								}
							}
						}
						else
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You can't steal because you lack the necessary skills.");
							UseSkill(%TrueClientId, $SkillStealing, false, true);
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', %reason);
				}
				return;

			case "#savecharacter":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
					{
						%r = SaveCharacter(%TrueClientId);
						messageClient(%TrueClientId, 'RPGchatCallback', "Saving self (" @ %TrueClientId @ "): success = " @ %r);
					}
					else
					{
						%id = getClientByName(%cropped);
						if(%id)
						{
							%r = SaveCharacter(%id);
							messageClient(%TrueClientId, 'RPGchatCallback', "Saving " @ %id.nameBase @ " (" @ %id @ "): success = " @ %r);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				else
				{
					%time = getTime();
					if(%time - %TrueClientId.lastSaveCharTime > 10)
					{
						%TrueClientId.lastSaveCharTime = %time;
		
						%r = SaveCharacter(%TrueClientId);
						messageClient(%TrueClientId, 'RPGchatCallback', "Saving self (" @ %TrueClientId @ "): success = " @ %r);
					}
				}
				return;

			case "#whatismyclientid":
				messageClient(%TrueClientId, 'RPGchatCallback', "Your clientId is " @ %TrueClientId);
				return;
			case "#whatismyuniqueid":
				messageClient(%TrueClientId, 'RPGchatCallback', "Your Global Unique ID is" SPC %TrueClientId.guid);
			case "#whatismyplayerid":
				messageClient(%TrueClientId, 'RPGchatCallback', "Your playerId is " @ %TrueClientId.player);
				return;

			case "#dropcoins":
				%cropped = GetWord(%cropped, 0);

				if(%cropped $= "all")
					%cropped = fetchData(%TrueClientId, "COINS");
				else
					%cropped = mfloor(%cropped);

				if(fetchData(%TrueClientId, "COINS") >= %cropped || %clientToServerAdminLevel >= 4)
				{
					if(%cropped > 0)
					{
						if( !(%clientToServerAdminLevel >= 4) )
							storeData(%TrueClientId, "COINS", %cropped, "dec");

						%toss = GetTypicalTossStrength(%TrueClientId);

						TossLootbag(%TrueClientId, "", %cropped, 0);
						RefreshAll(%TrueClientId);
		
						messageClient(%TrueClientId, 'RPGchatCallback', "You dropped " @ %cropped @ " coins.");
						%TrueClientId.player.playAudio(0, SoundMoney1);
					}
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You don't even have that many coins!");
				}
				return;

			case "#compass":
				
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Use #compass town or #compass dungeon. (Do not specify which, simply write town or dungeon)");
				else
				{
					if(SkillCanUse(%TrueClientId, "#compass"))
					{
						%mpos = GetNearestZone(%TrueClientId, %cropped, 4);
		
						if(%mpos !$= false && ( %cropped $= "town" || %cropped $= "dungeon" ) ) // only allow town and dungeon queries.
						{
							%d = GetNESW(%TrueClientId.player.getPosition(), %mpos);
							UseSkill(%TrueClientId, $SkillSenseHeading, true, true);
		
							messageClient(%TrueClientId, 'RPGchatCallback', "The nearest " @ %cropped @ " is " @ %d @ " of here.");
						}
						else
							messageClient(%TrueClientId, 1, "Error finding a zone!");
					}
					else
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't use your compass because you lack the necessary skills.");
						UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
					}
				}
				return;

           case "#wat":
                realtest();      //ancient magic of some sort...
           return;

			case "#getinfo":
				%cropped = %cropped;
				
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a name.");
				else
				{
					%id = getClientByName(%cropped);
					if(%id !$= -1)
						DisplayGetInfo(%TrueClientId, %id);
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#setinfo":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify text.");
				else
				{
					storeData(%TrueClientId, "PlayerInfo", %cropped);
					messageClient(%TrueClientId, 'RPGchatCallback', "Info set.  Use #getinfo [name] to retrieve this type of information.");
				}
				return;

			case "#addinfo":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify text.");
				else
				{
					storeData(%TrueClientId, "PlayerInfo", %cropped, "strinc");
					messageClient(%TrueClientId, 'RPGchatCallback', "Info added to the end of previous info.");
				}
				return;

			case "#w" or "#whatis":
				
				%item = %cropped;
		
				if(%item $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify an item (ex: Small Rock = SmallRock).");
				else
				{
					%msg = WhatIs(%item);
					
					if(%msg !$= "")
					sendRPGbottomprint(%TrueClientId, mfloor(strlen(%msg) / 10)+1, Getword(%msg, 0), getwords(%msg, 1));
				}
				return;

			case "#spell" or "#cast":
				
				if(fetchData(%TrueClientId, "SpellCastStep") $= 1)
					messageClient(%TrueClientId, 'RPGchatCallback', "You are already casting a spell!");
				else if(fetchData(%TrueClientId, "SpellCastStep") $= 2)
					messageClient(%TrueClientId, 'RPGchatCallback', "You are still recovering from your last spell cast.");
				else if(%TrueClientId.sleepMode !$= "" && %TrueClientId.sleepMode !$= false)
					messageClient(%TrueClientId, 'RPGchatCallback', "You can not cast a spell while sleeping or meditating.");
				else if(IsDead(%TrueClientId))
					messageClient(%TrueClientId, 'RPGchatCallback', "You can not cast a spell when dead.");
				else if(inArenaRoster(%TrueClientID))
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "Some force is preventing you to cast a spell in this area. Wait till you enter the arena!");
				}
				else
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Specify a spell.");
					else
					{
						if(fetchdata(%client, "Mana") >= $spelldata[%cropped, Cost])
						{
							if(SkillCanUse(%TrueclientId, getword(%cropped, 0)))
								BeginCastSpell(%TrueClientId, %cropped);
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "You do not have the required skill for this spell. " @ $skillDesc[$spelldata[GetWord(%cropped, 0), Skill]] @ ":" @ GetWord($skillRestriction[GetWord(%cropped, 0)],1));
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "You do not have the required amount of mana for this spell");
					}
				}
				return;

			case "#recall":
				if(inarena(%Trueclientid))
				return;
				%zvel = mfloor(getWord(%TrueClientId.player.getVelocity, 2));
				messageClient(%TrueClientId, 'RPGchatCallback', "ATTEMPTING RECALL");
				if(%zvel <= -350 || %zvel >= 350)
				{
					FellOffMap(%TrueClientId);
					CheckAndBootFromArena(%TrueClientId);
		
					%zv = "PASS";
				}
				else
					%zv = "FAIL";
				
				messageClient(%TrueClientId, 'RPGchatCallback', "Z-Velocity check: " @ %zv);
		
				if(%zv !$= "PASS" && !fetchData(%TrueClientId, "tmprecall"))
				{
					%seconds = $recallDelay;
					storeData(%TrueClientId, "tmprecall", true);
					%client.recall = schedule(%seconds * 1000, 0, "checkrecall", %TrueClientId, %TrueClientID.player.GetPosition());
					messageClient(%TrueClientId, 'RPGchatCallback', "Stay at your current position for the next " @ %seconds @ " seconds to recall.");
				
				}
				return;

			case "#track":
				
				//%cropped = GetWord(%cropped, 0);

				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a name.");
				else
				{
					if(SkillCanUse(%TrueClientId, "#track"))
					{
						%id = getClientByName(%cropped);
						if(%id !$= -1)
						{
							%clientpos = %TrueClientId.player.getPosition();
							%idpos = %id.player.getPosition();

							%dist = round(VectorDist(%clientpos, %idpos));
		
							if(Cap(%TrueClientId.PlayerSkill[$Skill::SenseHeading] * 7.5, 100, "inf") >= %dist)
							{
								%d = GetNESW(%clientpos, %idpos);
								messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " is " @ %d @ " of here, " @ %dist @ " meters away.");
								UseSkill(%TrueClientId, $Skill::SenseHeading, true, true);
								if(%dist <= 10)
								Client::unhide(%id);//counter counter!
							}
							else
							{
								messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " is too far from you to track with your current sense heading skills.");
								UseSkill(%TrueClientId, $Skill::SenseHeading, false, true);
							}
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't track because you lack the necessary skills.");
						UseSkill(%TrueClientId, $Skill::SenseHeading, false, true);
					}
				}
				return;

			case "#trackpack":
				return;
				%cropped = GetWord(%cropped, 0);
		
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a name.");
				else
				{
					if(SkillCanUse(%TrueClientId, "#trackpack"))
					{
						%id = getClientByName(%cropped);
						if(%id !$= -1)
						{
							%cropped = %id.nameBase;	//properly capitalize name
		
							%closest = 5000000;
							%closestId = -1;
							%clientpos = %TrueClientId.player.getPosition();
							%list = fetchData(%id, "lootbaglist");
							for(%i = strstr(%list, ","); strstr(%list, ",") !$= -1; %list = getsubstr(%list, %i+1, 99999))
							{
								%id = getsubstr(%list, 0, %i);
								%idpos = %id.player.getPosition();
								%dist = round(VectorDist(%clientpos, %idpos));
								if(%dist < %closest)
								{
									%closest = %dist;
									%closestId = %id;
								}
							}
							if(%closestId !$= -1)
							{
								%idpos = %closestId.player.getPosition();
		
								if(Cap(%TrueClientId.PlayerSkill[$SkillSenseHeading] * 15, 100, "inf") >= %closest)
								{
									%d = GetNESW(%clientpos, %idpos);
									messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s nearest backpack is " @ %d @ " of here, " @ %closest @ " meters away.");
									UseSkill(%TrueClientId, $SkillSenseHeading, true, true);
								}
								else
								{
									messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s nearest backpack is too far from you to track with your current sense heading skills.");
									UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
								}
							}
							else
							{
								messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " doesn't have any dropped backpacks.");
								UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
							}
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't track a backpack because you lack the necessary skills.");
						UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
					}
				}
				return;

			case "#sharepack":
				return;
				%time = getTime();
				if(%time - %TrueClientId.lastSharePackTime > 5)
				{
					%TrueClientId.lastSharePackTime = %time;
		
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);
			
						if(%id !$= -1 && %id.nameBase !$= %senderName)
						{
							%c1 = %id.nameBase;	//properly capitalize name
							if(mfloor(%c2) !$= 0 || %c2 $= "*")
							{
								%flag = "";
								%cnt = 0;
								%list = fetchData(%TrueClientId, "lootbaglist");
								for(%i = strstr(%list, ","); strstr(%list, ",") !$= -1; %list = getsubstr(%list, %i+1, 99999))
								{
									%cnt++;
									%bid = getsubstr(%list, 0, %i);
			
									if(%cnt $= %c2 || %c2 $= "*")
									{
										%flag++;
			
										%nl = GetWord($loot[%bid], 1);
										if(%nl !$= "*")
										{
											$loot[%bid] = strreplace($loot[%bid], %nl, AddToCommaList(%nl, %c1));
											messageClient(%TrueClientId, 'RPGchatCallback', "Adding " @ %c1 @ " to backpack #" @ %cnt @ " (" @ %bid @ ")'s share list.");
											messageClient(%id, 'RPGchatCallback', %TCsenderName @ " is sharing his/her backpack #" @ %cnt @ " with you.");
										}
										else
											messageClient(%TrueClientId, 'RPGchatCallback', "Backpack #" @ %cnt @ " is already publicly available.");
									}
								}
								
								if(%flag $= "")
									messageClient(%TrueClientId, 'RPGchatCallback', "Invalid backpack number.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a backpack number (1, 2, 3, etc, or * for all)");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name or same player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#unsharepack":
				return;
				%c1 = GetWord(%cropped, 0);
				%c2 = GetWord(%cropped, 1);
		
				if(%c1 !$= -1 && %c2 !$= -1)
				{
					%id = getClientByName(%c1);
		
					if(%id !$= -1 && %id.nameBase !$= %senderName)
					{
						%c1 = %id.nameBase;	//properly capitalize name
						if(mfloor(%c2) !$= 0 || %c2 $= "*")
						{
							%flag = "";
							%cnt = 0;
							%list = fetchData(%TrueClientId, "lootbaglist");
							for(%i = strstr(%list, ","); strstr(%list, ",") !$= -1; %list = getsubstr(%list, %i+1, 99999))
							{
								%cnt++;
								%bid = getsubstr(%list, 0, %i);
		
								if(%cnt $= %c2 || %c2 $= "*")
								{
									%flag++;
		
									%nl = GetWord($loot[%bid], 1);
									if(%nl !$= "*")
									{
										$loot[%bid] = strreplace($loot[%bid], %nl, RemoveFromCommaList(%nl, %c1));
										messageClient(%TrueClientId, 'RPGchatCallback', "Removing " @ %c1 @ " from backpack #" @ %cnt @ " (" @ %bid @ ")'s share list.");
										messageClient(%id, 'RPGchatCallback', %TCsenderName @ " has removed you from his/her backpack #" @ %cnt @ " share list.");
									}
									else
										messageClient(%TrueClientId, 'RPGchatCallback', "Backpack #" @ %cnt @ " is already publicly available.  Its share list can not be changed.");
								}
							}
							
							if(%flag $= "")
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid backpack number.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a backpack number (1, 2, 3, etc, or * for all)");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name or same player name.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
		
				return;

			case "#packsummary":
				return;
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);
						%cropped = %id.nameBase;	//properly capitalize name
		
						%cnt = mfloor(CountObjInCommaList(fetchData(%id, "lootbaglist")));
						messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " has " @ %cnt @ " dropped backpacks.");
					}
					else
					{
						%list = GetPlayerIdList();
						for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
						{
							%cnt = CountObjInCommaList(fetchData(%id, "lootbaglist"));
							if(%cnt > 0)
								messageClient(%TrueClientId, 'RPGchatCallback', %id.nameBase @ " has " @ %cnt @ " dropped backpacks.");
						}
					}
				}
				else if(%cropped $= "")
				{
					%cnt = mfloor(CountObjInCommaList(fetchData(%TrueClientId, "lootbaglist")));
					messageClient(%TrueClientId, 'RPGchatCallback', "You have a total of " @ %cnt @ " currently dropped backpacks.");
				}
				return;

			case "#mypassword":
				return; //no password for characters!
				%c1 = GetWord(%cropped, 0);
		
				if(%c1 !$= -1)
				{
					storeData(%TrueClientId, "password", %c1);
					messageClient(%TrueClientId, 'RPGchatCallback', "Changed personal password to " @ fetchData(%TrueClientId, "password") @ ".");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a one-word password.");
		
				return;

			case "#sleep":
				//return; //BROKEN will be fixed later
				if(fetchData(%TrueClientId, "InSleepZone") == true && %TrueClientId.sleepMode $= "" && !IsDead(%TrueClientId))
					%flag = true;
				echo(fetchData(%trueclientid, "InSleepZone"));
				if(%flag)
				{
					%TrueClientId.sleepMode = 1;
					%TrueClientId.camera.setTransform(%transform);
					%TrueClientId.camera.client = %clientid; 
					%TrueClientId.camera.setOrbitMode(%TrueClientId.player, %TrueClientId.player.getTransform(), 0.5, 4.5, 4.5);
					%TrueClientId.setControlObject(%TrueClientId.camera);
					refreshHPREGEN(%TrueClientId);
					refreshMANAREGEN(%TrueClientId);
		
					messageClient(%TrueClientId, 'RPGchatCallback', "You fall asleep...  Use #wake to wake up.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't seem to fall asleep here.");
		
				return;

			case "#meditate":
				if(%TrueClientId.sleepMode $= "" && !IsDead(%TrueClientId) && $possessedBy[%TrueClientId].possessId !$= %TrueClientId)
				{
					%TrueClientId.sleepMode = 2;
					%transform = %TrueClientId.player.getTransform();

					//note, AI's don't have a camera...
					if (isObject(%TrueClientId.camera))
					{
						%TrueClientId.camera.setTransform(%transform);
						%TrueClientId.camera.client = %clientid; 
						%TrueClientId.camera.setOrbitMode(%TrueClientId.player, %TrueClientId.player.getTransform(), 0.5, 4.5, 4.5);
						%TrueClientId.setControlObject(%TrueClientId.camera);
					}
					//Client::setControlObject(%TrueClientId, Client::getObserverCamera(%TrueClientId));
					//Observer::setOrbitObject(%TrueClientId, Client::getOwnedObject(%TrueClientId), 30, 30, 30);
					refreshHPREGEN(%TrueClientId);
					refreshMANAREGEN(%TrueClientId);
		
					messageClient(%TrueClientId, 'RPGchatCallback', "You begin to meditate.  Use #wake to stop meditating.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't seem to meditate.");
		
				return;

			case "#wake":
				if(%TrueClientId.sleepMode !$= "")
				{
					%TrueClientId.sleepMode = "";
					%TrueClientId.setControlObject(%TrueClientId.player);
					refreshHPREGEN(%TrueClientId);
					refreshMANAREGEN(%TrueClientId);
		
					messageClient(%TrueClientId, 'RPGchatCallback', "You awaken.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You are not sleeping or meditating.");
		
				return;

			case "#roll":
				%c1 = GetWord(%cropped, 0);
		
				if(%c1 !$= -1)
					messageClient(%TrueClientId, 'RPGchatCallback', %c1 @ ": " @ GetRpgRoll(%c1));
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a roll (example: 1d6)");
		
				return;

			case "#hide":
				
				if(SkillCanUse(%TrueClientId, "#hide"))
				{
					if(!fetchData(%TrueClientId, "invisible") && !fetchData(%TrueClientId, "blockHide") && %Trueclientid.lasthide + 5000 < getSimTime())
					{
						%closeEnoughToWall = Cap(%TrueClientId.PlayerSkill[$SkillHiding] / 125, 3.5, 8);
		
						%pos = %TrueClientId.player.getPosition();
		
						%closest = 10000;
						for(%i = 0; %i <= 6.283; %i+= 0.52)
						{
							getLOSinfo(%TrueClientId, 25, "", "0 0 " @ %i);
							%dist = VectorDist(%pos, $los::position);
							if(%dist < %closest && $los::position !$= "0 0 0" && $los::position !$= "")
								%closest = %dist;
						}
		
						if(%closest <= %closeEnoughToWall)
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You are successful at Hide In Shadows.");
		
							Client::hide(%TrueClientID);
							//GameBase::startFadeOut(%TrueClientId);
							//storeData(%TrueClientId, "invisible", true);
		
							%grace = Cap(%TrueClientId.PlayerSkill[$SkillHiding] / 20, 5, 100);
							WalkSlowInvisLoop(%TrueClientId, 5, %grace);
							%Trueclientid.lasthide = getSimTime();
							UseSkill(%TrueClientId, $Skill::Hiding, true, true);
						}
						else
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You were unsuccessful at Hide In Shadows.");
							UseSkill(%TrueClientId, $Skill::Hiding, false, true);
						}
					}
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't hide because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $Skill::Hiding, false, true);
				}
				return;

			case "#bash":

				if(SkillCanUse(%TrueClientId, "#bash"))
				{
					if(!(fetchdata(%trueClientId, "NextHitBash") || fetchdata(%trueClientId, "blockBash")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to bash!");
						storeData(%TrueClientId, "NextHitBash", true);
						storeData(%TrueClientId, "blockBash", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitBash"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to bash");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to bash again, wait a bit");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't bash because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $SkillBashing, false, true);
				}

				return;
			case "#disrupt":
				if(SkillCanUse(%TrueClientId, "#disrupt"))
				{
					if(!(fetchdata(%TrueClientId, "NextHitDisrupt") || fetchdata(%TrueClientId, "BlockDisrupt")))
					{
					
						storedata(%TrueClientId, "NextHitDisrupt", true);
						storedata(%TrueClientId, "blockdisrupt", true);
						messageClient(%TrueClientID, 'RPGchatCallback', "You are ready to disrupt!");
					
					}
					else
						if(fetchdata(%TrueClientId, "NextHitDisrupt"))
							messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to disrupt.");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Disrupt again, wait a bit");
				
				
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't Disrupt because you lack the necessary skills.");
			case "#focus":

				if(SkillCanUse(%TrueClientId, "#focus"))
				{
					if(!(fetchdata(%trueClientId, "NextHitFocus") || fetchdata(%trueClientId, "blockFocus")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You focus your energy!");
						storeData(%TrueClientId, "NextHitFocus", true);
						storeData(%TrueClientId, "blockFocus", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitFocus"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already focused.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too strained to focus, wait a bit.");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't focus because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $SkillBashing, false, true);
				}

				return;
			case "#cleave":
				
				if(SkillCanUse(%TrueClientId, "#cleave"))
				{
					if(!(fetchdata(%trueClientId, "NextHitCleave") || fetchdata(%trueClientId, "blockCleave")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Cleave!");
						storeData(%TrueClientId, "NextHitCleave", true);
						storeData(%TrueClientId, "blockCleave", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitCleave"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to cleave.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to cleave again, wait a bit");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't cleave because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $SkillBashing, false, true);
				}

				return;
			case "#shock":
				if(SkillCanUse(%TrueClientId, "#shock"))
				{
				
				
				
				
				}
				else
				{
					messageClient(%TrueClientID, 'RPGchatCallback', "You can't shock because you lack the necessary skills.");
				}
				
			case "#stun":
				if(SkillCanUse(%TrueClientId, "#stun"))
				{
					if(!(fetchdata(%trueClientId, "NextHitStun") || fetchdata(%trueClientId, "blockStun")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Stun!");
						storeData(%TrueClientId, "NextHitStun", true);
						storeData(%TrueClientId, "blockStun", true);
					}
					else
					if(fetchdata(%TrueClientId, "NextHitStun"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Stun.");
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Stun again, wait a bit");
				
				}
				else
				{
					messageClient(%TrueClientID, 'RPGchatCallback', "You can't stun because you lack the necessary skills.");
				}
			case "#targetleg":
				if(SkillCanUse(%TrueClientId, "#targetleg"))
				{
					if(!(fetchdata(%trueClientId, "NextHitLeg") || fetchdata(%trueClientId, "blockLeg")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Target Leg!");
						storeData(%TrueClientId, "NextHitLeg", true);
						storeData(%TrueClientId, "blockLeg", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitLeg"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Target Leg.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Target Leg again, wait a bit");					
				}
				else
				{
					MessageClient(%trueClientId, 'RPGchatCallBack', "You can't target leg because you lack the necessary skills.");
				}
			case "#encumber":

				if(SkillCanUse(%TrueClientId, "#encumber"))
				{
					if(!(fetchdata(%trueClientId, "NextHitencumber") || fetchdata(%trueClientId, "blockEncumber")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Encumber!");
						storeData(%TrueClientId, "NextHitencumber", true);
						storeData(%TrueClientId, "blockencumber", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitEncumber"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Encumber.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Encumber again, wait a bit");					
				}
				else
				{
					MessageClient(%trueClientId, 'RPGchatCallBack', "You can't encumber because you lack the necessary skills.");
				}
			case "#backstab":

				if(SkillCanUse(%TrueClientId, "#backstab"))
				{
					if(!(fetchdata(%trueClientId, "NextHitBackStab") || fetchdata(%trueClientId, "BlockBackStab")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to backstab!");
						storeData(%TrueClientId, "NextHitBackStab", true);
						storeData(%TrueClientId, "blockBackStab", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitBackStab"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to backstab.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to backstab again, wait a bit");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't backstab because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $SkillBashing, false, true);
				}

				return;
			case "#surge":
				if(SkillCanUse(%TrueClientId, "#surge"))
				{
					if( !(fetchdata(%TrueClientId, "blockSurge")  ) )
					{
						//storedata(%TrueClientId, "Surge", true);
						AddBonusState(%TrueClientId, "12 10", 5, "Surge");
						storedata(%TrueClientId, "BlockSurge", true);
						weightcall(%TrueClientId, false);
						debugBonusState(%TrueClientId);
						schedule(5000, %client, "endsurge", %client);
						messageClient(%TrueClientid, 'RPGchatCallback', "You begin to surge!");
					
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to boost your speed.");
				
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't surge because you lack the necessary skills.");
				
			case "#berserk":
				if(SkillCanuse(%trueclientid, "#berserk"))
				{
					if( !(fetchdata(%TrueClientId, "blockberserk") || fetchdata(%TrueClientId, "Surge") ) )
					{
						storedata(%TrueClientId, "Surge", true);
						weightcall(%TrueClientId, false);
						storedata(%TrueClientId, "BlockBerserk", true);
						schedule(5000, %client, "endberserk", %client);
						messageClient(%TrueClientId, 'RPGchatCallBack', "You go Berserk!");
					}
				
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't berserk because you lack the necessary skills.");
			case "#ignite":

				if(SkillCanUse(%TrueClientId, "#ignite"))
				{
					if(!(fetchdata(%trueClientId, "NextHitignite") || fetchdata(%trueClientId, "Blockignite")))
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You light your projectile!");
						storeData(%TrueClientId, "NextHitignite", true);
						storeData(%TrueClientId, "blockignite", true);
					}
					else
						if(fetchdata(%TrueClientId, "NextHitignite"))
						messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to ignite.");
						else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to ignite again, wait a bit");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't ignite arrows because you lack the necessary skills.");
					//UseSkill(%TrueClientId, $SkillBashing, false, true);
				}

				return;
			case "#shove":
				%time = getTime();
				if(%time - %TrueClientId.lastShoveTime > 1.5)
				{
					%TrueClientId.lastShoveTime = %time;
					if(SkillCanUse(%TrueClientId, "#shove"))
					{
						%player = %trueClientId.player;
						if(getLOSinfo(%TrueClientId, 5))
						{
							%pl = $los::object;
							%id = %pl.client;
							if(%TrueClientId.adminLevel > %id.adminLevel || %id.adminLevel < 1 && %id != 0)
							{
								%b = %TrueClientId.player.rotation;
								%c1 = Cap(20 + fetchData(%TrueClientId, "LVL"), 0, 250);
								%c2 = %c1 / 4;
								%muzzlevec = %client.player.getMuzzleVector(1);
								%vel = %id.player.getVelocity();
								%mom = GetWord(%muzzlevec, 0)*20+GetWord(%vel, 0) SPC GetWord(%muzzlevec, 1)*20+GetWord(%vel, 1) SPC GetWord(%muzzlevec, 2)*20+GetWord(%vel, 2);	
								%id.player.setVelocity(%mom);				
								
							}
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't shove because you lack the necessary skills.");
				}
				return;

			case "#defaulttalk":
				if(%cropped !$= "")
				{
					storeData(%TrueClientId, "defaultTalk", %cropped);
					messageClient(%TrueClientId, 'RPGchatCallback', "Changed Default Talk to " @ fetchData(%TrueClientId, "defaultTalk") @ ".");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify what will be added to the beginning of each of your messages.");
		
				return;

			case "#zonelist":
				if(SkillCanUse(%TrueClientId, "#zonelist"))
				{
					%c1 = GetWord(%cropped, 0);
		
					if(%c1 !$= -1)
					{
						if(stricmp(%c1, "all") $= 0)
							%t = 1;
						else if(stricmp(%c1, "players") $= 0)
							%t = 2;
						else if(stricmp(%c1, "enemies") $= 0)
							%t = 3;
		
						%list = Zone::getPlayerList(fetchData(%TrueClientId, "zone"), %t);
		
						if(%list !$= "")
						{
							for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
								messageClient(%TrueClientId, 'RPGchatCallback', %id.nameBase);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "[none]");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify 'players', 'enemies', or 'all'");
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You can't zonelist because you lack the necessary skills.");
					UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
				}
				return;

			case "#pickpocket":
			
				%time = getTime();//ok pickpocket will only steal small items, user really wont have a choice to what they steal either unlike tribes rpg. 
				if(%time - %TrueClientId.lastStealTime > $stealDelay)
				{
					%TrueClientId.lastStealTime = %time;
		
					if((%reason = AllowedToSteal(%TrueClientId)) $= "true")
					{
						if(SkillCanUse(%TrueClientId, "#pickpocket"))
						{
							if(getLOSinfo(%TrueClientId, 1))
							{
								%id = $los::object.client;
								%pos = $los::position;
								
								if($los::object.getClassName() $= "Player")
								{
									%damloc = %id.player.getDamageLocation(%pos);
									%tciskill = %TrueClientID.PlayerSkill[$SkillStealing] * 4/5;
									%idskill = %id.playerSkill[$skillStealing];
									
									//echo(%damloc);
									//%TrueClientId.stealType = 1;
									//SetupInvSteal(%TrueClientId, %id);
									//attempt to steal
									%inv = fetchdata(%id, "inventory");
									%num = getwordcount(%inv);
									//randomly select an item
									%itemid = GetWord(%inv, getrandom(0,%num-1));
									
									if(%itemid)
									{
										%item = getItem(%itemid);
										if($itemtype[%item] $= "armor" || $itemType[%item] $= "weapon")
										{
											//error cannot steal
											%fail = true;
										}
										else
										{
											%backfront = getword(%damloc, 1);
											if(%backfront $= "back_right" || %backfront $= "back" || %backfront $= "back_left" || %backfront $= "middle_back")
												%multi = 1;
											else
												%multi = 0.5;//odds of stealing 1/2
											if(!id.isAIControlled())
											%oddcalc = %num - 10;
											else
											%oddcalc = %num;
											if(%oddcalc < 0)
												%oddcalc = 0;
											%ooddcalc = %oddcalc * %tciskill * %multi;
											%doddcalc = 35 * %idskill + 10;
											%success = getRandom(-%doddcalc, %ooddcalc);
											
											if(%success > 0)
											%fail = false;
											else
											%fail = true;
										}
										echo("PICKPOCKET:" SPC -%doddcalc SPC %ooddcalc SPC %success SPC %tciskill SPC %idskill);
										if(!%fail)
										{
											RemoveFromInventory(%id, %itemid);
											AddToInventory(%TrueClientId, %itemid);
											messageClient(%TrueClientId, 'RPGchatCallback', "You sucessfully stole a" SPC GetFullItemName(%itemid));
											MessageClient(%TrueClientID, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
										}
										else
										{
											messageClient(%TrueClientId, 'RPGchatCallback', "You failed to steal from" SPC %id.rpgname @ "!");
											MessageClient(%TrueClientID, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
											messageClient(%id, 'RPGchatCallback', %TrueClientid.rpgname SPC "failed to steal from you!");
											//failed to steal, notify victim? lashback?
										}
										
									}
									else
										messageClient(%TrueClientId, 'RPGchatCallback', "Your target has no items on him");
								}
							}
						}
						else
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You can't pickpocket because you lack the necessary skills.");
							UseSkill(%TrueClientId, $SkillStealing, false, true);
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', %reason);
				}
				return;

			case "#mug":
			
			//can steal ANYTHING unequiped at a higher success rate, however player must be in your target list to attempt!
				%time = getTime();
				if(%time - %TrueClientId.lastStealTime > $stealDelay)
				{
					%TrueClientId.lastStealTime = %time;
					if(skillcanuse(%TrueClientId, "#mug") )
					{
						if(fetchdata(%TrueClientId, "NextHitMug") )
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to mug");
						}
						else if( fetchdata(%TrueClientId, "blockHitMug") )
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You are not ready to mug");
						}
						else
						{
							storedata(%TrueClientId, "blockhitmug", true);
							storedata(%TrueClientId, "NextHitMug", true);
							messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to mug.");
							
						}
						
							
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You do not have enough skill to mug");
					
				}
				return;

			case "#createpack":
			return;
				if(fetchData(%TrueClientId, "TempPack") !$= "")
				{
					if(HasThisStuff(%TrueClientId, fetchData(%TrueClientId, "TempPack")))
					{
						TakeThisStuff(%TrueClientId, fetchData(%TrueClientId, "TempPack"));
						%namelist = %TCsenderName @ ",";
						TossLootbag(%TrueClientId, fetchData(%TrueClientId, "TempPack"), 5, %namelist, 0);
						RefreshAll(%TrueClientId);
		
						remotePlayMode(%TrueClientId);
					}
				}
				return;

			case "#camp":
			return;
				if(Player::getItemCount(%TrueClientId, Tent))
				{
					%camp = nameToId("MissionCleanup/Camp" @ %TrueClientId);
					if(%camp $= -1)
					{
						if(fetchData(%TrueClientId, "zone") $= "")
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "Setting up camp...");
				
							%pos = %TrueClientId.player.getPosition();
				
							Player::decItemCount(%TrueClientId, Tent);
							RefreshAll(%TrueClientId);
							%group = newObject("Camp" @ %TrueClientId, SimGroup);
							addToSet("MissionCleanup", %group);
			
							schedule(2000, "DoCampSetup(" @ %TrueClientId @ ", 1, \"" @ %pos @ "\");");
							schedule(10000, "DoCampSetup(" @ %TrueClientId @ ", 2, \"" @ %pos @ "\");");
							schedule(17000, "DoCampSetup(" @ %TrueClientId @ ", 3, \"" @ %pos @ "\");");
							schedule(20000, "DoCampSetup(" @ %TrueClientId @ ", 4, \"" @ %pos @ "\");");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "You can't set up a camp here.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You already have a camp setup somewhere.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You aren't carrying a tent.");
		
				return;

			case "#uncamp":
			return;
				%camp = nameToId("MissionCleanup/Camp" @ %TrueClientId);
				if(%camp !$= -1)
				{
					%obj = nameToId("MissionCleanup/Camp" @ %TrueClientId @ "/woodfire");
					if(VectorDist(%TrueClientId.player.getPosition(), %obj.getPosition()) <= 10)
					{
						DoCampSetup(%TrueClientId, 5);
						messageClient(%TrueClientId, 'RPGchatCallback', "Camp has been packed up.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "You are too far from your camp.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You don't have a camp.");
		
				return;

			case "#advcompass":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Use #advcompass zone keyword");
				else
				{
					if(SkillCanUse(%TrueClientId, "#advcompass"))
					{
						%obj = GetZoneByKeywords(%TrueClientId, %cropped, 3);
		
						if(%obj !$= false)
						{
							%mpos = Zone::getMarker(%obj);
		
							%d = GetNESW(%TrueClientId.player.getPosition(), %mpos);
							UseSkill(%TrueClientId, $SkillSenseHeading, true, true);
		
							messageClient(%TrueClientId, 'RPGchatCallback', Zone::getDesc(%obj) @ " is " @ %d @ " of here.");
						}
						else
							messageClient(%TrueClientId, 1, "Couldn't fine a zone to match those keywords.");
					}
					else
					{
						messageClient(%TrueClientId, 'RPGchatCallback', "You can't use #advcompass because you lack the necessary skills.");
						UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
					}
				}
				return;
			case "#smith":
				%ret = getLOSInfo(%TrueClientId, 10);
				%obj = GetWord(%ret, 0);
				if(%obj.special !$= "Smith")
				{
					MessageClient(%TrueClientId, 'RPGchatCallback', "You need to be looking at an anvil to smith");
					return false;
				}
				if(!%TrueClientId.IsSmithing)
				{
					%TrueClientId.IsSmithing = true;
					%command = GetWord(%cropped, 0);
					%nitem = GetWord(%cropped, 1);
					%from = GetWord(%cropped, 2);
					Game.smith(%TrueClientID, %command, %nitem, %from);
					%TrueClientId.IsSmithing = false;
				}
			case "#myexp":
				messageClient(%TrueClientId, 'RPGchatCallback', "Your EXP:" SPC fetchdata(%TrueClientId, "EXP"));
			case "#mylevel":
				messageClient(%TrueClientId, 'RPGchatCallback', "Your Level:" SPC fetchdata(%TrueClientId, "LVL"));
			case "#quickbind":
				if(%cropped !$= "")
				{
					%number = GetWord(%cropped, 0);
					%rest = GetWords(%cropped, 1, 99);
					%number = mfloor(%number);
					if(%number > 0 && %number <= 10)
					{
						%number--;
						storedata(%client, "QuickBind" @ %number, %rest);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallBack', "Incorrect usage, syntax: #QuickBind [number 1-10] [#command...] example #quickbind 1 #cast thorn");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallBack', "Incorrect usage, syntax: #QuickBind [number 1-10] [#command...] example #quickbind 1 #cast thorn");

			case "#resetplayer":
				%race = fetchdata(%TrueClientID, "RACE");

				%race = "MaleHuman";//until femalehuman is put in
				%apm = "Armor";
				%dbname = %race @ %apm @ %TrueClientID.cweight;
				%e = %TrueClientID.player.getEnergyLevel();
				%d = %TrueClientID.player.getDamageLevel();
				%TrueClientID.player.setDataBlock(MaleHuman);
				%TrueClientID.player.setdatablock(%dbname);
				%TrueClientID.player.setEnergyLevel(%e);
				%TrueClientID.player.setDamageLevel(%d);
				MessageClient(%TrueClientId, 'RPGchatCallBack', "Player Reset");
			case "#mount":
				//getlosinfo
				%obj = getword(getlosinfo(%TrueClientId, 10), 0);
				%dataBlock = %obj.getDataBlock();
   				%className = %dataBlock.className;
   				%player = %TrueClientID.player;
   				if (%forceVehicleNode !$= "" || (%className $= WheeledVehicleData || %className $= FlyingVehicleData || %className $= HoverVehicleData) &&
        				 %player.mountVehicle && %player.getState() $= "Move" && %obj.getDamageState() !$= "Destroyed") 
        			{
					if (%TrueClientID.isAIControlled())
					{
					 %transform = %col.getTransform();   

					 //either the AI is *required* to pilot, or they'll pick the first available passenger seat
					 if (%client.pilotVehicle)
					 {
					    //make sure the bot is in light armor
					    if (%player.getArmorSize() $= "Light")
					    {
					       //make sure the pilot seat is empty
					       if (!%obj.getMountNodeObject(0))
						  %node = 0;
					    }
					 }
					 else
					    %node = findAIEmptySeat(%obj, %player);
					}
					else
					 %node = findEmptySeat(%obj, %player);
					
					//now mount the player in the vehicle
					if(%node >= 0)
					{

					 %obj.mountObject(%player,%node);
					 %obj.playAudio(0, MountVehicleSound);
					 %player.mVehicle = %obj;
					 //%player.mountnode = %node;
							// if player is repairing something, stop it
							if(%player.repairing)
								stopRepairing(%player);

					 //this will setup the huds as well...
					// %dataBlock.playerMounted(%obj,%player, %node);
					}		
        			}
        			else
        				messageClient(%TrueClientId, 'RPGchatCallback', "You cannot mount this object");
			}
			//another break...
//============================
//Guild commands==============
//============================
			switch$(%w1)
			{
				case "#claim":
					%guildid = IsInWhatGuild(%TrueClientId);
					%guild = GuildGroup.getObject(%guildid);
					if(%guildid == -1 || %guild.getGUIDRank(%TrueClientId.guid) < 1)
					{
						MessageClient(%TrueClientId, 'MessageClaimFail', "You have to be in a guild to claim land.");
						return;
					}
					%zone = fetchdata(%TrueClientId, "zone");
					
					if(%zone.type $= "GuildZone")
					{
						if(%zone.owned)
						{
							if(%zone.owner == %guild)
							{
								MessageClient(%TrueClientId, 'MessageClaimFail', "Your guild already owns this zone.");	
							}
							else
							{
								MessageClient(%TrueClientId, 'MessageClaimFail', "You cannot claim this zone because it is owned by a guild, in order to obtain it you must #challenge the current owner.");
							}
						}
						else
						{
							%list = Zone::getPlayerList(fetchData(%TrueClientId, "zone"), 2);
							%flag = true;
							for(%i = 0; GetWord(%list, %i); %i++)
							{
								%id = GetWord(%list, %i);
								if(%id != %TrueClientId)
								{
									%gid = IsInWhatGuild(%id);
									if(%gid != -1 && %gid != %guildid)
									{
									
										%flag = false;
										break;
									}
								}
							}
							if(%flag)
							{
								if(GetWordCount(%guild.getzonelist()) < $guildownerzonelimit )
								{
									//check to see if there are any other guild members in this zone.
									MessageClient(%TrueClientId, 'MessageClaim', "You claim this zone in the name of" SPC %guild.getName() @ "!");
									%guild.addZone(%zone);
									%zone.owned = true;
									%zone.owner = %guild;
									SaveServerGuilds();
								}
								else
									MessageClient(%TrueClientId, 'MessageClaim', "Your guild already owns too many zones");
							}
							else
							MessageClient(%TrueClientId, 'MessageClaimFail', "You cannot claim this zone until you are the only guild in this zone.");
						}
					}
					else
						MessageClient(%TrueClientId, 'MessageClaimFail', "You must be in a guild zone to claim it.");
				case "#challenge":
					%guildid = IsInWhatGuild(%Trueclientid);
					%guild = GuildGroup.getObject(%guildid);
					%zone = fetchdata(%TrueClientId, "Zone");
					if(%guildid == -1 || %guild.getGUIDRank(%TrueClientId.guid) <= 1)
					{
						if(%guildid == -1)
							MessageClient(%TrueClientId, 'RPGchatcallback', "Error, you must be in a guild to use this command");
						else
							MessageClient(%TrueClientId, 'RPGchatCallback', "You are not of sufficent rank to start a challenge with another guild");
						return;
					}
					if(%zone.type $= "GuildZone")
					{
						if(%zone.owned)
						{
							if(%zone.owner == %guild)
							{
								MessageClient(%TrueClientId, 'RPGchatcallback', "Error, your guild already owns this zone!");
							}
							else
							{
								if(%zone.challenged)
								{
									MessageClient(%TrueClientId, 'RPGchatCallback', "Error, someone has already challenged this zone");
								}
								else
								{
									if(GetWordCount(%guild.getzonelist()) < $guildownerzonelimit )
									{
									//success
									MessageClient(%TrueClientId, 'RPGchatcallback', "Success, registering challenge...");
									%zone.owner.startChallenge(%zone, %guild);

									}
									else
										MessageClient(%TrueClientId, 'RPGchatcallback', "Your guild already has too many zones");
								}
							}
						
						}
						else
							MessageClient(%TrueClientId, 'RPGchatCallback', "Zone is unclaimed, #claim to gain possession of it");
					
					}
					else
						MessageClient(%TrueClientId, 'RPGchatCallback', "You must be in an enemy guild's zone to challenge it.");
				case "#accept":
					%guildid = IsInWhatGuild(%Trueclientid);
					%guild = GuildGroup.getObject(%guildid);
					%zone = %guild.challengedzone;
					if(%guildid == -1 || %guild.getGUIDRank(%TrueClientId.guid) <= 1)
					{
						if(%guildid == -1)
							MessageClient(%TrueClientId, 'RPGchatcallback', "Error, you must be in a guild to use this command");
						else
							MessageClient(%TrueClientId, 'RPGchatCallback', "You are not of sufficent rank to accept a challenge with another guild");
						return;
					}
					//find state...
					if(%zone.owner == %guild && %zone.canacceptchallenge)
					{
						%count = ClientGroup.getCount();
						for(%icl = 0; %icl < %count; %icl++)
						{
							%cl = ClientGroup.getObject(%icl);
							if(%cl.isAiControlled()) continue; //skip bots

							if(%guild.GUIDinGuild(%cl.guid) && %guild.GetGUIDRank(%cl.guid) > 0  )
								%home++;
							else
							if(%zone.challenger.GUIDinGuild(%cl.guid)  && %zone.challenger.getGUIDRank(%cl.guid) > 0 )
								%away++;
						}
						
						if(IsEventPending(%zone.challengeEvent) && %away >= %zone.minchallenger)
						{
							cancel(%zone.challengeEvent);
						%guild.PrepareChallenge(%zone, %zone.challenger, 0);
						}
					}
					else
						MessageClient(%TrueClientId, 'RPGchatcallback', "There is no challenge to accept");
			}
			switch$(%w1)
			{
//============================
//ADMIN COMMANDS =============
//============================

			case "#anon":
				if(%clientToServerAdminLevel >= 3)
				{
					%aname = GetWord(%cropped, 0);
					%cn = mfloor(GetWord(%cropped, 1));
					if(%cn !$= -1 && %aname !$= -1)
					{
						%anonmsg = getsubstr(%cropped, strstr(%cropped, %cn)+strlen(%cn)+1, 99999);
						if(%aname $= "all")
						{
							%count = ClientGroup.getCount();
							for(%icl = 0; %icl < %count; %icl++)
							{
								%cl = ClientGroup.getObject(%icl);

								if(mfloor(%cl.adminLevel) >= mfloor(%clientToServerAdminLevel))
									messageClient(%cl, %cn, "[ANON] " @ %TCsenderName @ ": " @ %anonmsg);
								else
									messageClient(%cl, %cn, %anonmsg);
							}
						}
						else
						{
							%id = getClientByName(%aname);
							if(%id !$= -1)
							{
								if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel))
									messageClient(%id, %cn, "[ANON] " @ %TCsenderName @ ": " @ %anonmsg);
								else
									messageClient(%id, %cn, %anonmsg);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Syntax: #anon name/all colorNumber message");
				}
				return;

			case "#fw":
				%c1 = GetWord(%cropped, 0);
		
				if(%c1 !$= -1)
				{
					%id = getClientByName(%c1);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id !$= %TrueClientId && mfloor(%id.adminLevel) !$= 0)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						if(%clientToServerAdminLevel >= 3)
						{
							%rest = getsubstr(%cropped, (strlen(%c1)+1), strlen(%cropped)-(strlen(%c1)+1));
							RPGchat(%id, 0, %rest);	//, %senderName);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Sent a forwarded message to " @ %id @ ".");
						}
					}
					else
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name, or name is of a superAdmin.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify name, command and text.");
		
				return;

			case "#forcespawn":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
						
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(IsDead(%id))
							{
								Game.playerSpawn(%id, true);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Forced " @ %cropped @ " to spawn.");
							}
							else
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " isn't dead.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#attacklos":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a bot name.");
				else
				{
					%event = strstr(%cropped, ">");
					if(%event !$= -1)
					{
						%info = getsubstr(%cropped, 0, %event);
						%cmd = getsubstr(%cropped, %event, 99999);
					}
					else
						%info = %cropped;
		
					%c1 = getWord(%info, 0);
					%ox = GetWord(%info, 1);
					%oy = GetWord(%info, 2);
					%oz = GetWord(%info, 3);
					%id = getClientByName(%c1);
		
					if(%id !$= -1)
					{
						if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
						{
							if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id !$= %TrueClientId && mfloor(%id.adminLevel) !$= 0)
								messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
							else if(%id.isAiControlled())
							{
								%player = %TrueClientId.player;
		
								if(%ox $= -1 && %oy $= -1 && %oz $= -1)
								{
									getLOSinfo(%TrueClientId, 50000);
									%pos = $los::position;
								}
								else
									%pos = %ox @ " " @ %oy @ " " @ %oz;
		
								if(%event !$= -1)
									AddEventCommand(%id, %senderName, "onPosCloseEnough " @ %pos, %cmd);
		
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %c1 @ " (" @ %id @ ") is attacking position " @ %pos @ ".");
								storeData(%id, "botAttackMode", 3);
								storeData(%id, "tmpbotdata", %pos);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Player must be a bot.");
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
		            }
				return;

			case "#botnormal":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a bot name.");
				else
				{
					%id = getClientByName(%cropped);
		
					if(%id !$= -1)
					{
						if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
						{
							if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id !$= %TrueClientId && mfloor(%id.adminLevel) !$= 0)
								messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
							else if(%id.isAiControlled())
							{
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Bot is now in normal attack mode.");
								storeData(%id, "botAttackMode", 1);
								AI::newDirectiveRemove(fetchData(%id, "BotInfoAiName"), 99);
								storeData(%id, "tmpbotdata", "");
		
								if(fetchData(%id, "petowner") !$= "")
								{
									storeData(%id, "botAttackMode", 2);
									storeData(%id, "tmpbotdata", fetchData(%id, "petowner"));
								}
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Player must be a bot.");
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#createbotgroup":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a one-word BotGroup name.");
					else
					{
						if(GetWord(%cropped, 1) $= "")
						{
							%g = GetWord(%cropped, 0);
							%n = AI::CountBotGroupMembers(%g);
							if(!AI::BotGroupExists(%g))
							{
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Created BotGroup '" @ %g @ "'.");
								AI::CreateBotGroup(%g);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "BotGroup already exists and contains " @ %n @ " members.  Use #discardbotgroup to delete a BotGroup.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a ONE-WORD BotGroup name.");
					}
				}
				return;

			case "#discardbotgroup":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a one-word BotGroup name.");
					else
					{
						if(GetWord(%cropped, 1) $= "")
						{
							%g = GetWord(%cropped, 0);
							if(AI::BotGroupExists(%g))
							{
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Discarded BotGroup '" @ %g @ "'.");
								AI::DiscardBotGroup(%g);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "BotGroup does not exist.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a ONE-WORD BotGroup name.");
					}
				}
				return;

			case "#getbotgroupleader":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a one-word BotGroup name.");
					else
					{
						if(GetWord(%cropped, 1) $= "")
						{
							%g = GetWord(%cropped, 0);
							if(AI::BotGroupExists(%g))
							{
								%tl = GetWord($tmpBotGroup[%g], 0);
								%tln = %tl.nameBase;
								messageClient(%TrueClientId, 'RPGchatCallback', "BotGroup leader is " @ %tln @ " (" @ %tl @ ").");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "BotGroup does not exist.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a ONE-WORD BotGroup name.");
					}
				}
				return;

			case "#botgroup":
				if(%clientToServerAdminLevel >= 1)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);
						if(%id !$= -1)
						{
							if(%id.isAiControlled())
							{
								if(AI::BotGroupExists(%c2))
								{
									%b = AI::IsInWhichBotGroup(%id);
									if(%b $= -1)
									{
										if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Adding minion " @ %c1 @ " (" @ %id @ ") to BotGroup '" @ %c2 @ "'.");
										AI::AddBotToBotGroup(%id, %c2);
									}
									else
										messageClient(%TrueClientId, 'RPGchatCallback', "This bot already belongs to the BotGroup '" @ %b @ "'.  Use #rbotgroup to remove a bot from a BotGroup.");
								}
								else
									messageClient(%TrueClientId, 'RPGchatCallback', "BotGroup '" @ %c2 @ "' does not exist.  Use #createbotgroup to create a BotGroup.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Name must be a bot.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#rbotgroup":
				if(%clientToServerAdminLevel >= 1)
				{
					%c1 = GetWord(%cropped, 0);
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
						if(%id !$= -1)
						{
							if(%id.isAiControlled())
							{
								%b = AI::IsInWhichBotGroup(%id);
								if(%b !$= -1)
								{
									if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Removing minion " @ %c1 @ " (" @ %id @ ") from BotGroup '" @ %b @ "'.");
									AI::RemoveBotFromBotGroup(%id, %b);
								}
								else
									messageClient(%TrueClientId, 'RPGchatCallback', "This bot does not belong to a BotGroup.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Name must be a bot.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#listbotgroups":
				if(%clientToServerAdminLevel >= 1)
				{
					messageClient(%TrueClientId, 'RPGchatCallback', $BotGroups);
				}
				return;

			case "#setupai":
				if(%clientToServerAdminLevel >= 5)
				{
					for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
						deleteObject(%id);
					InitTownBots();
				}
				return;

			case "#getadmin":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
						
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%a = mfloor(%id.adminLevel);
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s Admin Clearance Level: " @ %a);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;
	
			case "#setadmin":
				if(%clientToServerAdminLevel >= 1)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%a = mfloor(%c2);
							if(%a < 0)
								%a = 0;
							if(%a > %clientToServerAdminLevel)
								%a = %clientToServerAdminLevel;
		
							%id.adminLevel = %a;
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") Admin Clearance Level to " @ %id.adminLevel @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#eyes":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				else
				{
					%id = getClientByName(%cropped);
		
					if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
					{
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id !$= %TrueClientId && mfloor(%id.adminLevel) !$= 0)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(!IsDead(%id))
							{
								if(%clientToServerAdminLevel >= 1)
								{
									Revert(%TrueClientId);
				
									//eyes
									//note, AI's don't have a camera...
									if(isObject(%TrueClientId.camera))
									{
										%transform = %id.player.getTransform();
										%TrueClientId.camera.setTransform(%transform);
										%TrueClientId.camera.setOrbitMode(%id.player, %transform, 0.5, 4.5, 4.5);
										%TrueClientId.setControlObject(%TrueClientId.camera);
									}
								}
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Target client is dead.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#possess":
				if(%cropped $= "")
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				else
				{
					%id = getClientByName(%cropped);
		
					if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
					{
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id !$= %TrueClientId && mfloor(%id.adminLevel) !$= 0)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(!IsDead(%id))
							{
								if(%clientToServerAdminLevel >= 4)
								{
									Revert(%TrueClientId);
			
									//possess
									%TrueClientId.possessId = %id;
									%id.possessedBy = %TrueClientId;
									%TrueClientId.setControlObject(%id.player);
								}
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Target client is dead.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#revert":
				if(%TrueClientId.sleepMode $= "")
				{
					Revert(%TrueClientId);
				}
				return;

			case "#fixspellflag":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
	
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "SpellCastStep", "");
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Spell flag reset.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#fixbashflag":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "blockBash", "");
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Bash flag reset.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#kick":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
					if(%c2 $= -1)
						%c2 = false;
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							{
								 %id.setDisconnectReason( "You have been kicked out of the game." );
								 savecharacter(%id);
								 %id.schedule(700, "delete");
							}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#kickid":
				if(%clientToServerAdminLevel >= 2)
				{
					%id = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
					if(%c2 $= -1)
						%c2 = false;
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
						Admin::Kick(%TrueClientId, %id, %c2);
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify clientId & data.");
				}
				return;
			case "#ban":
				if(%clienttoserverAdminLevel >= 3)
				{
					%absolute = false;
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
					if(%c2 $= "absolute" && %clientToServerAdminLevel >= 5)
					%absolute = true;
					if(%c2 <= 0 ) 
						%c2 = $host::bantime;
					if(%c2 > $Host::MaxBanTime)
						%c2 = $Host::MaxBanTime;
					%bantime = %c2;
					echo("absolute:" SPC %absolute);
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							{
								 %id.setDisconnectReason( "You have been banned." );
								 savecharacter(%id);
								 %id.schedule(700, "delete");
								
								 if(%absolute)
								 {
								 BanList::Addabsolute(%id.guid, %id.getAddress(), mpow(2,32)-1);
								 BanList::Add(%id.guid, %id.getAddress(), 60*60*24*7);
								 echo("ABSOLUTE BAN");
								 }
								 else
								  BanList::add(%id.guid, %id.getAddress(),%bantime);
							}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");				
				
				}
			case "#admin":
				if($Host::UseSanctionedAdmin)
				{
					for(%ii = 0; (%cguid = GetWord($SanctionedAdminList,%ii) ) !$= ""; %ii = %ii+2 )
					{
						if(%cguid == %TrueClientId.guid)
						{
							%TrueClientId.adminLevel = GetWord($SanctionedAdminList,%ii+1);

							if(%TrueClientId.adminLevel >= 4)
								ChangeRace(%TrueClientId, "DeathKnight");

							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Admin Clearance Level " @ %TrueClientId.adminLevel @ " Granted.");
						
						}
					}
				}
				else
				{
					for(%i = 1; %i <= 8; %i++)
					{
						if(%cropped $= $AdminPassword[%i] && $AdminPassword[%i] !$= "")
						{
							%TrueClientId.adminLevel = %i;

							if(%TrueClientId.adminLevel >= 4)
								ChangeRace(%TrueClientId, "DeathKnight");

							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Password accepted for Admin Clearance Level " @ %TrueClientId.adminLevel @ ".");

							break;
						}
					}
				}
				return;

			case "#human":
				if(%clientToServerAdminLevel >= 4)
					ChangeRace(%TrueClientId, "Human");
				%TrueClientId.adminLevel = 0;
				return;

			case "#loadworld":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped $= "")
						LoadWorld();
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Do not use parameters for this function call.");
				}
				return;

			case "#saveworld":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
						SaveWorld();
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Do not use parameters for this function call.");
				}
				return;

			case "#loadcharacter":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify clientId.");
					else
						LoadCharacter(%cropped);
				}
				return;

			case "#item":
			return;
				if(%clientToServerAdminLevel >= 2)
				{
					%name = GetWord(%cropped, 0);

					%id = getClientByName(%name);

					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						Player::setItemCount(%id, GetWord(%cropped, 1), GetWord(%cropped, 2));
						RefreshAll(%id);
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Set " @ %name @ " (" @ %id @ ") " @ GetWord(%cropped, 1) @ " count to " @ GetWord(%cropped, 2));
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#getitemcount":
				if(%clientToServerAdminLevel >= 1)
				{
					%id = getClientByName(GetWord(%cropped, 0));

					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						%c = Player::getItemCount(%id, GetWord(%cropped, 1));
						messageClient(%TrueClientId, 'RPGchatCallback', "Item count for (" @ %id @ ") " @ GetWord(%cropped, 1) @ " is " @ %c);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#myitem":
				if(%clientToServerAdminLevel >= 2)
				{
					if(GetWord(%cropped, 0) !$= "")
					{
					%item = GetWord(%cropped, 0);
					%amt = GetWord(%cropped, 1);
					%prefix = GetWord(%cropped, 2);
					%suffix = GetWord(%cropped, 3);
					if(%prefix == 0) %prefix = 3;
					if(%suffix == 0) %suffix = 1;
					if(%amt == 0) %amt = 1;
					//Player::setItemCount(%TrueClientId, GetWord(%cropped, 0), GetWord(%cropped, 1));
					//AddToInventory(%TrueClientID, CreateItem( GetWord(%cropped, 0), (GetWord(%cropped, 1) $= "" ? 3 : GetWord(%cropped, 1) ), ( GetWord(%cropped, 2) $= "" ? 1 : GetWord(%cropped, 2) )));
					Game.AddToInventory(%TrueClientID, %amt, %item, %prefix, %suffix);
					RefreshAll(%TrueClientId);
					if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Gave " @ %TCsenderName @ " (" @ %TrueClientId @ ") " @ %amt SPC %item SPC "Prefix:" SPC %prefix SPC "Suffix:" SPC %suffix @ ".");
					}
					else
						messageClient(%TrueCientId, 'RPGchatCallback', "Useage: #myitem itemname [amt] [prefix] [suffix]. Prefix and suffix are a number 1-6. (3 is normal for prefix 1 is normal for suffix)");
				}
				return;

			case "#arenacutshort":
				if(%clientToServerAdminLevel >= 1)
				{
					$IsABotMatch = true;
					$ArenaBotMatchTicker = $ArenaBotMatchLengthInTicks;
				}
				return;

			case "#teleport":
				if(%clientToServerAdminLevel >= 2)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							getLOSinfo(%TrueClientId, 50000);
							if(%id.player.ismounted())
							{
								//unmount the player
								%id.player.getobjectMount().unmountobject(%id.player);
								%id.player.setcontrolobject(0);
							}
							%id.player.setPosition($los::position);

							CheckAndBootFromArena(%id);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Teleporting " @ %cropped @ " (" @ %id @ ") to " @ $los::position @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#teleport2":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id1 = getClientByName(%c1);
						%id2 = getClientByName(%c2);
		
						if(mfloor(%id1.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id1 !$= %TrueClientId)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id1 !$= -1 && %id2 !$= -1)
						{
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Teleporting " @ %c1 @ " (" @ %id1 @ ") to " @ %c2 @ " (" @ %id2 @ ").");
							%id1.player.setPosition(%id2.player.getPosition());

							CheckAndBootFromArena(%id1);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name(s).");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#follow":
				if(%clientToServerAdminLevel >= 1)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id1 = getClientByName(%c1);
						%id2 = getClientByName(%c2);
						if(%id1 !$= -1 && %id2 !$= -1)
						{
							if(%id1.isAiControlled())
							{
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Making " @ %c1 @ " (" @ %id1 @ ") follow " @ %c2 @ " (" @ %id2 @ ").");
		
								%event = strstr(%cropped, ">");
								if(%event !$= -1)
								{
									%cmd = getsubstr(%cropped, %event, 99999);
									AddEventCommand(%id1, %senderName, "onIdCloseEnough " @ %id2, %cmd);
								}
		                                    
								storeData(%id1, "tmpbotdata", %id2);
								storeData(%id1, "botAttackMode", 2);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "First name must be a bot.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name(s).");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#cancelfollow":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);
						if(%id !$= -1)
						{
							if(%id.isAiControlled())
							{
								AI::newDirectiveRemove(fetchData(%id, "BotInfoAiName"), 99);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") has stopped following its target.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Player must be a bot.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#freeze":
				if(%cropped !$= -1)
				{
					%id = getClientByName(%cropped);
					if(%id !$= -1)
					{
						if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
						{
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Freezing " @ %cropped @ " (" @ %id @ ").");
							%id.setControlObject(%id.camera);
							storeData(%id, "frozen", true);

							if(%id.isAiControlled())
							{
								//bot hack
								%tmp = %id.player;
								%client.setControlObject(%id.player);
								%client.setControlObject(%client.player);
								%id.player = %tmp;
							}
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				return;

			case "#cancelfreeze":
				if(%cropped !$= -1)
				{
					%id = getClientByName(%cropped);
					if(%id !$= -1)
					{
						if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1)
						{
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") is no longer frozen.");
							%id.setControlObject(%id.player);
							storeData(%id, "frozen", "");
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");

				return;

			case "#kill":
				if(%clientToServerAdminLevel >= 2)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%id.player.scriptKill(0);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") was executed.");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#clearchar":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%id.player.scriptKill(0);
							ResetPlayer(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") profile was RESET.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;
		}
		//another break...
		switch$(%w1)
		{
			case "#spawn":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #spawn botType displayName loadout [team] [x] [y] [z]");
					else
					{
						%event = strstr(%cropped, ">");
						if(%event !$= -1)
						{
							%info = getsubstr(%cropped, 0, %event);
							%cmd = getsubstr(%cropped, %event, 99999);
						}
						else
							%info	= %cropped;

						%c1 = GetWord(%info, 0);
						%c2 = GetWord(%info, 1);
						%loadout = GetWord(%info, 2);
						%team = GetWord(%info, 3);
						%ox = GetWord(%info, 4);
						%oy = GetWord(%info, 5);
						%oz = GetWord(%info, 6);
		
						if(%c1 !$= -1 && %c2 !$= -1 && %loadout !$= -1)
						{
							if(getClientByName(%c2) $= -1)
							{
								if(%ox $= -1 && %oy $= -1 && %oz $= -1)
								{
									%player = %TrueClientId.player;
									getLOSinfo(%TrueClientId, 50000);
									%lospos = $los::position;
								}
								else
									%lospos = %ox @ " " @ %oy @ " " @ %oz;
			
								if(%team $= -1) %team = 1;
								//%n = AI::helper(%c1, %c2, "TempSpawn " @ %lospos @ " " @ %team, %loadout);
								//%id = AI::getId(%n);
								%id = aiconnect(%c2, %team);
			
								if(%event !$= -1)
									AddEventCommand(%id, %senderName, "onkill", %cmd);
			
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Spawned " @ %c2 @ " (" @ %id @ ") at " @ %id.player.getPosition() @ ".");
							}
							else
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %c2 @ " already exists.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #spawn botType displayName loadout [team] [x] [y] [z]");
					}
				}
				return;
			case "#playanim":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify animation.");
					else
					{
						%anim = GetWord(%cropped, 0);
						%sound = GetWord(%cropped, 1);
						if(%sound != -1 && %sound !$= "")
						{
						
						
						}
						
					}
				}
				return;
			case "#spawntestplayer":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify animation.");
					else
					{
						%tplayer = %cropped;

						
					}
				}
				return;
			case "#gettransform":
				if(%clienttoserverAdminLevel >=1 )
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Transform" SPC %TrueClientID.player.getTransform());
					
				}
				return;

		}
		//there appears to be a limit of ~74 cases per switch
		switch$(%w1)
		{
			case "#fell":
				if(%clientToServerAdminLevel >= 2)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Processing fell-off-map for " @ %cropped @ " (" @ %id @ ")");
							//Zone::handleTeleport(fetchdata(%id, "zone"), %id);
							FellOffMap(%id);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#getstorage":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							messageClient(%TrueClientId, 'RPGchatCallback', %id @ ": " @ fetchData(%id, "BankStorage"));
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#clearstorage":
				if(%clientToServerAdminLevel >= 4)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "BankStorage", "");
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %id @ " bank storage cleared.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#setstorage":
				if(%clientToServerAdminLevel >= 4)
				{
					%name = GetWord(%cropped, 0);

					%id = getClientByName(%name);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						storeData(%id, "BankStorage", SetStuffString(fetchData(%id, "BankStorage"), GetWord(%cropped, 1), GetWord(%cropped, 2)));
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %id @ " bank storage modified. Use #getstorage [name] to view.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#addsp":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "SP", %c2, "inc");
							RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") SP credits to " @ fetchData(%id, "SP") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#setsp":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "SP", %c2);
							RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") SP credits to " @ fetchData(%id, "SP") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#addlck":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= "" && %c2 !$= "")
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "LCK", %c2, "inc");
							RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") base LCK to " @ fetchData(%id, "LCK") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#sethp":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%max = fetchData(%id, "MaxHP");
							if(%c2 < 1)
								%c2 = 1;
							else if(%c2 > %max)
								%c2 = %max;
		
							setHP(%id, %c2);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") HP to " @ fetchData(%id, "HP") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#setmana":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%max = fetchData(%id, "MaxMANA");
							if(%c2 < 0)
								%c2 = 0;
							else if(%c2 > %max)
								%c2 = %max;

							setMANA(%id, %c2);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") MANA to " @ fetchData(%id, "MANA") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#addexp":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "EXP", %c2, "inc");
							if(%id.isAiControlled())
								HardcodeAIskills(%id);
							RefreshExp(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") EXP to " @ fetchData(%id, "EXP") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#setexp":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "EXP", %c2);
							RefreshExp(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") EXP to " @ fetchData(%id, "EXP") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#addcoins":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "COINS", %c2, "inc");
							RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") COINS to " @ fetchData(%id, "COINS") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#addbank":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "BANK", %c2, "inc");
							RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") BANK to " @ fetchData(%id, "BANK") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#setteam":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
				
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
	
							%id.team = %c2;
							//RefreshAll(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") team to " @ %id.team @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#setrace":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= "DeathKnight" && %clientToServerAdminLevel >= 4 || %c2 !$= "DeathKnight")
								ChangeRace(%id, %c2, %clientToServerAdminLevel);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") race to " @ fetchData(%id, "RACE") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;
			case "#setinvis":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= 0)
							{
								if(fetchData(%id, "invisible"))
									UnHide(%id);
							}
							else if(%c2 $= 1)
							{
								if(!fetchData(%id, "invisible"))
									GameBase::startFadeOut(%id);
								storeData(%id, "invisible", true);
							}
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") invisible state to " @ %c2 @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#dumbai":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= 0)
								storeData(%id, "dumbAIflag", "");
							else if(%c2 $= 1)
								storeData(%id, "dumbAIflag", true);

							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") dumb AI flag state to '" @ fetchData(%id, "dumbAIflag") @ "'.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#getlck":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") base LCK is " @ fetchData(%id, "LCK") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#gethp":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") HP is " @ fetchData(%id, "HP") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getmana":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") MANA is " @ fetchData(%id, "MANA") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getmaxhp":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") max HP is " @ fetchData(%id, "MaxHP") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getmaxmana":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") max MANA is " @ fetchData(%id, "MaxMANA") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getexp":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%cl = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %cl @ ") EXP is " @ fetchData(%cl, "EXP") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getcoins":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") COINS is " @ fetchData(%id, "COINS") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getbank":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") BANK is " @ fetchData(%id, "BANK") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getteam":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") team is " @ %id.team @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getclientid":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " clientId is " @ %id @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;
			case "#getuniqueid":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " Global Unique ID is " @ %id.guid @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getplayerid":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " playerId is " @ %id.player @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getname":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%n = %cropped.nameBase;

						if(%n !$= "")
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " name is " @ %n @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid clientId.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a clientId.");
				}
				return;

			case "#getpassword":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " password[" @ %id @ "] is " @ fetchData(%id, "password") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getotherinfo":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " $Client::info[" @ %id @ ", 5] is " @ $Client::info[%id, 5] @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getlvl":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") LEVEL is " @ fetchData(%id, "LVL") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getfinallck":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") final LCK is " @ fetchData(%id, "LCK") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getfinaldef":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") max DEF roll is " @ fetchData(%id, "DEF") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#getfinalatk":
				if(%clientToServerAdminLevel >= 1)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") max ATK roll is " @ fetchData(%id, "ATK") @ ".");
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#exportchat":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped !$= "")
					{
						if(%cropped $= "0")
							$exportChat = false;
						else if(%cropped $= "1")
							$exportChat = true;
		
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "exportChat set to " @ $exportChat @ ".");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Specify 1 or 0 (1 = true, 0 = false).");
				}
				return;

			case "#doexport":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= 0)
								%id.doExport = false;
							else if(%c2 $= 1)
								%id.doExport = true;
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") doExport to " @ %id.doExport @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#getip":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") IP is " @ %id.getAddress());
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#spawnpack":
				return;
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%event = strstr(%cropped, ">");
						if(%event !$= -1)
						{
							%info = getsubstr(%cropped, 0, %event);
							%cmd = getsubstr(%cropped, %event, 99999);
						}
						else
							%info = %cropped;
		
						%div = strstr(%info, "|");
		
						if(%div !$= -1)
						{
							%a = getsubstr(%info, 0, %div-1);
							%tag = GetWord(%a, 0);
							%ox = GetWord(%a, 1);
							%oy = GetWord(%a, 2);
							%oz = GetWord(%a, 3);
							if(%ox $= -1 && %oy $= -1 && %oz $= -1)
							{
								//didn't enter coordinates.
								getLOSinfo(%TrueClientId, 50000);
								%pos = $los::position;
							}
							else
								%pos = %ox @ " " @ %oy @ " " @ %oz;
		
							if(!IsInCommaList($SpawnPackList, %tag))
							{
								%pack = getsubstr(%info, %div+1, 99999);
								%pid = DeployLootbag(%pos, "0 0 0", %pack);
								$SpawnPackList = AddToCommaList($SpawnPackList, %tag);
								$tagToObjectId[%tag] = %pid;
								%pid.tag = %tag;
			
								if(%event !$= -1)
									AddEventCommand(%pid, %senderName, "onpickup", %cmd);
			
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Spawned pack (" @ %pid @ ") at position " @ %pos @ ".");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Tagname " @ %tag @ " already exists.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Divider not found. Type #spawnpack with no parameters to get a quick overview.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#spawnpack tagname [x] [y] [z] | packstring. Use this command only if you know what you're doing.");
				}
				return;

			case "#delpack":
				return;
				if(%clientToServerAdminLevel >= 3)
				{
					%tag = GetWord(%cropped, 0);
		
					if(%cropped !$= -1)
					{
						if($tagToObjectId[%tag] !$= "")
						{
							%object = $tagToObjectId[%tag];
							ClearEvents(%object);
							deleteObject(%object);
							$tagToObjectId[%tag] = "";
							$SpawnPackList = RemoveFromCommaList($SpawnPackList, %tag);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Deleted " @ %tag @ " (" @ %object @ ")");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid tagname.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#delpack tagname.");
				}
				return;

			case "#spawndis" or "#spawndif":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%f = GetWord(%cropped, 0);
						%tag = GetWord(%cropped, 1);
						%x = GetWord(%cropped, 2);
						%y = GetWord(%cropped, 3);
						%z = GetWord(%cropped, 4);
						%r1 = GetWord(%cropped, 5);
						%r2 = GetWord(%cropped, 6);
						%r3 = GetWord(%cropped, 7);
						%r4 = GetWord(%cropped, 8);
						%s1 = GetWord(%cropped, 9);
						%s2 = GetWord(%cropped, 10);
						%s3 = GetWord(%cropped, 11);
						
						
						if(getwords(%cropped, 2, 4) $= "")
						{
						//getLOSinfo(%client, %searchRange, %mask)
						
							%d = getLOSinfo(%TrueClientId, 50000);
							%pos =getwords(%d, 1,3);
							
						}
						else
							%pos = %x @ " " @ %y @ " " @ %z;
		
						if(getwords(%cropped, 5, 7) $= "")
							%rot = "0 0 0 1";
						else
							%rot = %r1 @ " " @ %r2 @ " " @ %r3 SPC %r4;
						if(GetWords(%cropped, 8, 10) $= "")
						{
							%scale = "1 1 1";
						}
						else
							%scale = %s1 SPC %s2 SPC %s3;//scale
						%fname = %f @ ".dif";
						
						
						//%object = newObject(%tag, InteriorShape, %fname);
						if(%tag !$= "" && isfile("interiors/" @ %fname))
						%object = new InteriorInstance()
						   	{
						      		position = %pos;
						      		rotation = %rot;
						      		scale = %scale;
						      		interiorFile = %fname;
  						 	};
						if(%object != 0 && %tag !$= "")
						{
							if( $tagToObjectId[%tag] !$= "")
							{
								%o = $tagToObjectId[%tag];
								//deleteobject(%o);
								%o.delete();
								
								$tagToObjectId[%tag] = "";
								%w = "Replaced";
							}
							else
							{
								$DISlist = AddToCommaList($DISlist, %tag);
								%w = "Spawned";
							}
							
							MissionCleanup.add(%object);
							$tagToObjectId[%tag] = %object;
							%object.nametag = %tag;
		
							//%object.position = %pos;
							//if(%rot !$= -1)
							//	%object.rotation = %rot;
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %w @ " " @ %tag @ " (" @ %object @ ") at pos " @ %pos);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid DIF filename or tagname.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#spawndis filename tagname [x] [y] [z] [r1] [r2] [r3] [r4] [s1] [s2] [s3]. Do not specify .dis, this will automatically be added.");
				}
				return;

			case "#deldis" or "#deldif":
				if(%clientToServerAdminLevel >= 3)
				{
					%tag = GetWord(%cropped, 0);
		
					if(%cropped !$= -1)
					{
						if($tagToObjectId[%tag] !$= "")
						{
							%object = $tagToObjectId[%tag];
							ClearEvents(%object);
							%object.delete();
							$tagToObjectId[%tag] = "";
							$DISlist = RemoveFromCommaList($DISlist, %tag);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Deleted " @ %tag @ " (" @ %object @ ")");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid tagname.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#deldis tagname.");
				}
				return;

			case "#listdis" or "#listdis":
				if(%clientToServerAdminLevel >= 1)
				{
					messageClient(%TrueClientId, 'RPGchatCallback', $DISlist);
				}
				return;

			case "#listpacks":
				return;//add later
				if(%clientToServerAdminLevel >= 1)
				{
					messageClient(%TrueClientId, 'RPGchatCallback', $SpawnPackList);
				}
				return;

			case "#deleteobject":
				if(%clientToServerAdminLevel >= 5)
				{
					%c1 = GetWord(%cropped, 0);
					if(%c1 !$= -1)
					{
						if(%c1.tag !$= "")
						{
							$tagToObjectId[%c1.tag] = "";
							if(IsInCommaList($DISlist, %c1.tag))
								$DISlist = RemoveFromCommaList($DISlist, %c1.tag);
							else if(IsInCommaList($SpawnPackList, %c1.tag))
								$SpawnPackList = RemoveFromCommaList($SpawnPackList, %c1.tag);
						}
						deleteObject(%c1);
						ClearEvents(%c1);
		
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Attempted to deleteObject(" @ %c1 @ ")");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#deleteobject [objectId].  Be careful with this command.");
				}
				return;

			case "#getposition":
				if(%clientToServerAdminLevel >= 1)
				{
					%player = %TrueClientId.player;
					getLOSinfo(%TrueClientId, 50000);
		
					messageClient(%TrueClientId, 'RPGchatCallback', "Position at LOS is " @ $los::position);
				}
				return;

			case "#deathmsg":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%msg = getsubstr(%cropped, (strlen(%c1)+1), 99999);
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "deathmsg", %msg);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") deathmsg to " @ fetchData(%id, "deathmsg"));
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				else
				{
					storeData(%TrueClientId, "deathmsg", %cropped);
					messageClient(%TrueClientId, 'RPGchatCallback', "Changed your death message to: " @ fetchData(%TrueClientId, "deathmsg"));
				}
				return;

			case "#block":
				if(%clientToServerAdminLevel >= 3)
				{
					%bname = GetWord(%cropped, 0);
					if(%bname !$= -1)
					{
						//Always clear the blockdata
						ClearBlockData(%senderName, %bname);
			
						if(!IsInCommaList($BlockList[%senderName], %bname))
							$BlockList[%senderName] = AddToCommaList($BlockList[%senderName], %bname);
			
						storeData(%TrueClientId, "BlockInputFlag", %bname);
						storeData(%TrueClientId, "tmpBlockCnt", "");
		
						ManageBlockOwnersList(%senderName);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect syntax for #block [blockname]");
				}
				return;

			case "#endblock":
				if(%clientToServerAdminLevel >= 3)
				{
					if(fetchData(%TrueClientId, "BlockInputFlag") !$= "")
					{
						storeData(%TrueClientId, "BlockInputFlag", "");
						storeData(%TrueClientId, "tmpBlockCnt", "");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "No block to end!");
				}
				return;

			case "#delblock":
				if(%clientToServerAdminLevel >= 3)
				{
					%bname = GetWord(%cropped, 0);
					if(%bname !$= -1)
					{
						if(IsInCommaList($BlockList[%senderName], %bname))
						{
							ClearBlockData(%senderName, %bname);
							$BlockList[%senderName] = RemoveFromCommaList($BlockList[%senderName], %bname);
		
							ManageBlockOwnersList(%senderName);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Block " @ %bname @ " deleted.");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Block does not exist!");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect syntax for #delblock [blockname]");
				}
				return;

			case "#clearblocks":
				if(%clientToServerAdminLevel >= 5)
				{
					%targetName = GetWord(%cropped, 0);
					%id = getClientByName(%targetName);
				}
				else if(%clientToServerAdminLevel >= 3)
				{
					%targetName = %senderName;
					%id = %TrueClientId;
				}
		
				if(%id !$= -1)
				{
					if($BlockList[%targetName] !$= "")
					{
						%list = $BlockList[%targetName];
						$BlockList[%targetName] = "";
						for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
						{
							%w = getsubstr(%list, 0, %p);
							ClearBlockData(%targetName, %w);
						}
						ManageBlockOwnersList(%targetName);
		
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Deleted ALL of " @ %targetName @ "'s blocks.");
					}
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
		
				return;

			case "#clearallblocks":
				if(%clientToServerAdminLevel >= 5)
				{
					%bname = GetWord(%cropped, 0);
					if(%bname $= "confirm")
					{
						%blist = $BlockOwnersList;
						for(%bp = strstr(%blist, ","); (%bp = strstr(%blist, ",")) !$= -1; %blist = getsubstr(%blist, %bp+1, 99999))
						{
							%name = getsubstr(%blist, 0, %bp);
		
							if($BlockList[%name] !$= "")
							{
								%list = $BlockList[%name];
								$BlockList[%name] = "";
								for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
								{
									%w = getsubstr(%list, 0, %p);
									ClearBlockData(%name, %w);
								}
							}
							ManageBlockOwnersList(%name);
						}
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Deleted EVERYONE's blocks.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Type #clearallblocks confirm to clear EVERYONE's blocks.");
				}
				return;

			case "#listblocks":
				if(%clientToServerAdminLevel >= 5)
				{
					if(%cropped !$= "")
					{
						if(IsInCommaList($BlockOwnersList, %cropped))
							messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s BlockList: " @ $BlockList[%cropped]);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				else if(%clientToServerAdminLevel >= 3)
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "Your BlockList: " @ $BlockList[%senderName]);
				}
				return;

			case "#echo":
				if(stricmp(%cropped, "off") $= 0)
					%TrueClientId.echoOff = true;
				else if(stricmp(%cropped, "on") $= 0)
					%TrueClientId.echoOff = "";
				else
					messageClient(%TrueClientId, 'RPGchatCallback', %cropped);
		
				return;

			case "#call":
				if(%clientToServerAdminLevel >= 3)
				{
					%bname = GetWord(%cropped, 0);
					
					if(%bname !$= -1)
					{
						%list = getsubstr(%cropped, (strlen(%bname)+1), 99999);
						for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
							%a[%c++] = getsubstr(%list, 0, %p);
		
						if(%c <= 8)
						{
							if(IsInCommaList($BlockList[%senderName], %bname))
							{
								%TrueClientId.echoOff = true;
			
								for(%i = 1; (%bd = $BlockData[%senderName, %bname, %i]) !$= ""; %i++)
								{
									if(%a[1] !$= "")
										%bd = nsprintf(%bd, %a[1], %a[2], %a[3], %a[4], %a[5], %a[6], %a[7], %a[8]);
		
									RPGchat(%client, 0, %bd);	//, %senderName);
								}
			
								%TrueClientId.echoOff = "";
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Block does not exist!");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Too many parameters for #call (max of 8)");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect syntax for #call [blockname]");
				}
				return;

			case "#givethisstuff":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%stuff = getsubstr(%cropped, (strlen(%c1)+1), 99999);
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							GiveThisStuff(%id, %stuff, true);
							if(%id.isAiControlled())
								HardcodeAIskills(%id);
								
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Gave " @ %c1 @ " (" @ %id @ "): " @ %stuff);
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#takethisstuff":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%stuff = getsubstr(%cropped, (strlen(%c1)+1), 99999);
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(HasThisStuff(%id, %stuff))
							{
								TakeThisStuff(%id, %stuff);
								if(%id.isAiControlled())
									HardcodeAIskills(%id);
								RefreshAll(%id);
		
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Took " @ %c1 @ " (" @ %id @ "): " @ %stuff);
							}
							else
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Could not take stuff.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#refreshbotskills":
				if(%clientToServerAdminLevel >= 2)
				{
					if(%cropped $= "")
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
					else
					{
						%id = getClientByName(%cropped);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							HardcodeAIskills(%id);
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Refreshed skills for " @ %cropped @ " (" @ %id @ ").");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
				}
				return;

			case "#listblockowners":
				if(%clientToServerAdminLevel >= 5)
				{
					messageClient(%TrueClientId, 'RPGchatCallback', $BlockOwnersList);
				}
				return;

			case "#nodroppack":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= 0)
								storeData(%id, "noDropLootbagFlag", "");
							else if(%c2 $= 1)
								storeData(%id, "noDropLootbagFlag", true);

							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") noDropLootbagFlag to '" @ fetchData(%id, "noDropLootbagFlag") @ "'.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#playsound":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%pos = getsubstr(%cropped, (strlen(%c1)+1), 99999);
		
					if(%c1 !$= -1)
					{
						if(GetWord(%pos, 0) $= "")
						{
							if(getLOSinfo(%TrueClientId, 50000))
								%pos = $los::position;
							else
								%pos = %TrueClientId.player.getPosition();
						}
						serverPlay3D(%c1, %pos);
		
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Playing sound " @ %c1 @ " at pos " @ %pos);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify nsound & position.");
				}
				return;

			case "#delbot":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= -1)
					{
						%id = getClientByName(%cropped);

						if(%id !$= -1)
						{
							if(%id.isAiControlled())
							{
								storeData(%id, "noDropLootbagFlag", true);
								ClearEvents(%id);
								%id.player.scriptKill(0);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " (" @ %id @ ") was deleted.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "This command only works on bots.");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#loadout":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%stuff = getsubstr(%cropped, (strlen(%c1)+1), 99999);
		
					if(%c1 !$= -1)
					{
						if(!IsInCommaList($LoadOutList, %c1))
						{
							$LoadOutList = AddToCommaList($LoadOutList, %c1);
							$LoadOut[%c1] = %stuff;
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Loadout " @ %c1 @ " defined.");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Loadout tagname already exists.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify tagname & data.");
				}
				return;

			case "#delloadout":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
		
					if(%c1 !$= -1)
					{
						if(IsInCommaList($LoadOutList, %c1))
						{
							$LoadOutList = RemoveFromCommaList($LoadOutList, %c1);
							$LoadOut[%c1] = "";
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Loadout " @ %c1 @ " deleted.");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Loadout tagname does not exist.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify tagname.");
				}
				return;

			case "#clearloadouts":
				if(%clientToServerAdminLevel >= 4)
				{
					%list = $LoadOutList;
					$LoadOutList = "";
					for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
					{
						%w = getsubstr(%list, 0, %p);
						$LoadOut[%w] = "";
					}
		
					if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Deleted ALL loadouts.");
				}
				return;

			case "#showloadout":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
		
					if(%c1 !$= -1)
					{
						if(IsInCommaList($LoadOutList, %c1))
							messageClient(%TrueClientId, 'RPGchatCallback', %c1 @ ": " @ $LoadOut[%c1]);
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Loadout tagname does not exist.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify tagname.");
				}
				return;

			case "#listloadouts":
				if(%clientToServerAdminLevel >= 3)
				{
					%list = $LoadOutList;
					for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
					{
						%w = getsubstr(%list, 0, %p);
						messageClient(%TrueClientId, 'RPGchatCallback', %w @ ": " @ $LoadOut[%w]);
					}
				}
				return;

			case "#nobotsniff":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							if(%c2 $= 0)
								storeData(%id, "noBotSniff", "");
							else if(%c2 $= 1)
								storeData(%id, "noBotSniff", true);
		
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Changed " @ %c1 @ " (" @ %id @ ") noBotSniff flag to " @ %c2 @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;

			case "#addrankpoints":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);

					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							storeData(%id, "RankPoints", %c2, "inc");
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") RankPoints to " @ fetchData(%id, "RankPoints") @ ".");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;
		}
		//more breaks...this sucks
		switch$(%w1)
		{
			
			case "#delchar": 
				if(%clientToServerAdminLevel >= 5)
				{
					%name = %cropped;
					%dclient = getClientByName(%name);
					//clear all skills.
					//ok FIRST delete the character files.
					%filename = "characters/" @ %dclient.realname @ "/" @ %dclient.namebase @ ".cs";
					deletefile(%filename);
					deletefile(%filename @ ".dso");
					
					%dclient.data.delete();
					%dclient.data = new ScriptObject(CDATA @ %client.guid);
					GiveDefaults(%dclient);
					%dclient.clan = "";
					
					%dclient.choosingGroup = true;
					%dclient.game = 'RPGgame';
					%dclient.camera.getDataBlock().setMode( %client.camera, "observer");
       		%dclient.setControlObject( %dclient.camera );
       		%dclient.player.delete(); //bye bye player
					commandToClient(%dclient, 'OpenISMenu');
					commandToClient(%dclient, 'RPGplayMusic', "SoftTown");
					BuildMenu(%client, 0, 0);
				}
			case "#sethouse":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
		
					if(%c1 !$= -1 && %c2 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%hn = "";
							if(stricmp(%c2, "null") $= 0)
								%hn = 0;
							else
							{
								for(%i = 1; $HouseName[%i] !$= ""; %i++)
								{
									if(strstr($HouseName[%i], %c2) !$= -1)
										%hn = %i;
								}
							}
		
							if(%hn !$= "")
							{
								%hname = $HouseName[%hn];
								storeData(%id, "MyHouse", %hname);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Setting " @ %c1 @ " (" @ %id @ ") House to " @ fetchData(%id, "MyHouse") @ ".");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid House.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & house (to clear house, use: #sethouse name NULL).");
				}
				return;

			case "#setspawnmultiplier":
				if(%clientToServerAdminLevel >= 5)
				{
					%c1 = GetWord(%cropped, 0);
		
					if(%c1 !$= -1)
					{
						$spawnMultiplier = Cap(%c1, 0, "inf");
						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "spawnMultiplier set to " @ $spawnMultiplier @ ".");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a number (normal should be 1. 0 will cease spawning.)");
				}
				return;

			case "#jail":
				if(%clientToServerAdminLevel >= 3)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
					%c3 = GetWord(%cropped, 2);
		
					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);
		
						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%c1 = %id.nameBase;
							if(%c2 $= "")
								%c2 = 300;
							if(%c3 $= "")
								%c3 = Game.GetRandomJailNumber();
							
							%val = Game.ValidateJailNumber(%c3);
							if(%val)
							{
								Game.Jail(%id, %c2 * 1000, %c3);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %c1 @ " has been jailed for " @ %c2 @ " seconds in Jail #" @ %c3 @ ".");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid jail number.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name, time, and jail number.");
				}
				return;

			case "#beg":
				if(%clientToServerAdminLevel >= 2)
				{
					%c1 = GetWord(%cropped, 0);
					%c2 = GetWord(%cropped, 1);
					if(%c2 $= -1)
						%c2 = false;

					if(%c1 !$= -1)
					{
						%id = getClientByName(%c1);

						if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
							messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
						else if(%id !$= -1)
						{
							%ip = %id.getAddress();
							BanList::add(%ip, 300);
							Net::kick(%id, "Do not beg from an admin! The next time you might be banned, so quit your begging.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name & data.");
				}
				return;
		}
		//another break due to the switch limitation
		switch$(%w1)
		{
			case "#onhear":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%event = strstr(%cropped, ">");
						if(%event !$= -1)
						{
							%info = getsubstr(%cropped, 0, %event);
							%cmd = getsubstr(%cropped, %event, 99999);
						}
						else
							%info	= %cropped;

						%var = GetWord(%info, 4);
						if(stricmp(%var, "var") $= 0)
							%var = "var";
						else
						{
							%var = "";
							%quote1 = strstr(%info, "\"");
							%quote2 = String::ofindSubStr(%info, "\"", %quote1+1);
						}
						if(%quote1 !$= -1 && %quote2 !$= -1 || %var !$= "")
						{
							%pname = GetWord(%info, 0);
							%id = getClientByName(%pname);

							if(%id !$= -1)
							{
								%pname = %id.nameBase;	//properly capitalize name
								%radius = GetWord(%info, 1);
								%keep = GetWord(%info, 2);

								if(%keep $= "true" || %keep $= "false")
								{
									%targetname = GetWord(%info, 3);
									%tid = getClientByName(%targetname);
									if(stricmp(%targetname, "all") $= 0 || %tid !$= -1)
									{
										if(%var !$= "")
										{
											%vtxt = %var;
											%text = "var";
										}
										else
										{
											%text = getsubstr(%info, %quote1+1, %quote2);
											%vtxt = "|" @ %text @ "|";
										}

										if(%text !$= "")
										{
											if(%event !$= -1)
											{
												AddEventCommand(%id, %senderName, "onHear " @ %pname @ " " @ %radius @ " " @ %keep @ " " @ %targetname @ " " @ %vtxt, %cmd);
												if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "onHear event set for " @ %pname @ "(" @ %id @ ") with text: \"" @ %text @ "\"");
											}
											else
												messageClient(%TrueClientId, 'RPGchatCallback', "onHear event definition failed.");
										}
										else
											messageClient(%TrueClientId, 'RPGchatCallback', "Invalid text.");
									}
									else
										messageClient(%TrueClientId, 'RPGchatCallback', "Invalid name. Please specify 'all' or target's name.");
								}
								else
									messageClient(%TrueClientId, 'RPGchatCallback', "Specify 'true' or 'false'. 'true' means that the onHear event won't be deleted after use. 'false' is recommended to keep things clean.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Invalid name.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Quotes for text not found.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#onhear name radius keep all/targetname \"text\"/var.");
				}
				return;

			case "#if":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%info = %cropped;

						%para1 = strstr(%info, "{");
						%para2 = String::ofindSubStr(%info, "}", %para1+1);
						if(%para1 !$= -1 && %para2 !$= -1)
						{
							%expression = getsubstr(%info, %para1+1, %para2);
							if((%pw = CheckForProtectedWords(%expression)) $= "")
							{
								%command = getsubstr(%info, %para1+%para2+3, 99999);
								%retval = eval("%x = (" @ %expression @ ");");

								if(%retval $= 0)
									%r = false;
								else
									%r = true;
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "(" @ %expression @ ") = " @ %r);
		
								if(%retval && %command !$= "")
									RPGchat(%client, 0, %command);	//, %senderName);
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Protected word '" @ %pw @ "' can't be used in the #if statement.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "{ and } found.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#if {expression} command");
				}
				return;

			case "#addskill":
				if(%clientToServerAdminLevel >= 3)
				{
					%name = GetWord(%cropped, 0);
		
					%id = getClientByName(%name);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						%sid = GetWord(%cropped, 1);
						if($SkillDesc[%sid] !$= "")
						{
							%sn = mfloor(GetWord(%cropped, 2));
							if(%sn !$= 0)
							{
								%id.PlayerSkill[%sid] += %sn;
								RefreshAll(%id);
								if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Set " @ %name @ " (" @ %id @ ") " @ $SkillDesc[%sid] @ " to " @ %id.PlayerSkill[%sid]);
							}
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#setvelocity":
				if(%clientToServerAdminLevel >= 2)
				{
					%name = GetWord(%cropped, 0);

					%id = getClientByName(%name);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						%max = 5000;
						%x = Cap(mfloor(GetWord(%cropped, 1)), -%max, %max);
						%y = Cap(mfloor(GetWord(%cropped, 2)), -%max, %max);
						%z = Cap(mfloor(GetWord(%cropped, 3)), -%max, %max);

						%vel = %x @ " " @ %y @ " " @ %z;
						%id.player.setVelocity(%vel);

						if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Set " @ %name @ " (" @ %id @ ") velocity to " @ %vel);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#getskill":
				if(%clientToServerAdminLevel >= 2)
				{
					%name = GetWord(%cropped, 0);
		
					%id = getClientByName(%name);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						%sid = GetWord(%cropped, 1);
						if($SkillDesc[%sid] !$= "")
							messageClient(%TrueClientId, 'RPGchatCallback', %name @ " (" @ %id @ ") " @ $SkillDesc[%sid] @ " is " @ %id.PlayerSkill[%sid]);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#scheduleblock":
				if(%clientToServerAdminLevel >= 3)
				{
					%bname = GetWord(%cropped, 0);
					if(%bname !$= -1)
					{
						if(IsInCommaList($BlockList[%senderName], %bname))
						{
							%delay = GetWord(%cropped, 1);
							if(%delay >= 0.05)
							{
								%repeat = mfloor(GetWord(%cropped, 2));
								if(%repeat >= 0)
								{
									%rp = (%repeat+1);

									%arglist = getsubstr(%cropped, (strlen(%bname @ %delay @ %repeat @ "  ")+1), 99999);
									if(GetWord(%arglist, 0) !$= "")
										%txt = "#call " @ %bname @ " " @ %arglist;
									else
										%txt = "#call " @ %bname;

									for(%sbi = 1; %sbi <= %rp; %sbi++)
										schedule(%delay * %sbi * 1000, "RPGchat(" @ %client @ ", 0, \"" @ %txt @ "\");");	//, \"" @ %senderName @ "\");");
									if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Block " @ %bname @ " scheduled for " @ %repeat @ " repeats at " @ %delay @ " second intervals.");
								}
								else
									messageClient(%TrueClientId, 'RPGchatCallback', "Schedule repeat too low, minimum is 0");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Schedule delay too low, minimum is 0.05");
						}
						else
							if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "Block does not exist!");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect syntax for #scheduleblock blockName delay numRepeat");
				}
				return;

			case "#listonhear":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%id = getClientByName(%cropped);

						if(%id !$= -1)
						{
							%index = GetEventCommandIndex(%id, "onHear");
							if(%index !$= -1)
							{
								for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
									messageClient(%TrueClientId, 'RPGchatCallback', %id.nameBase @ " onHear " @ %index2 @ ": " @ $EventCommand[%id, %index2]);
							}
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Please specify player name.");
				}
				return;

			case "#clearonhear":
				if(%clientToServerAdminLevel >= 3)
				{
					%name = GetWord(%cropped, 0);
					%oindex = GetWord(%cropped, 1);

					if(%name !$= -1)
					{
						%id = getClientByName(%name);

						if(%id !$= -1)
						{
							%index = GetEventCommandIndex(%id, "onHear");
							if(%index !$= -1)
							{
								for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
								{
									if(mfloor(%index2) $= mfloor(%oindex) || %oindex $= -1)
									{
										$EventCommand[%id, %index2] = "";
										if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %id.nameBase @ " onHear " @ %index2 @ " cleared.");
									}
								}
							}
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect syntax for #clearonhear name [index]. If index is missing or -1, all onHears for name are cleared.");
				}
				return;

			case "#getvelocity":
				if(%clientToServerAdminLevel >= 2)
				{
					%name = GetWord(%cropped, 0);
		
					%id = getClientByName(%name);
		
					if(mfloor(%id.adminLevel) >= mfloor(%clientToServerAdminLevel) && %id.nameBase !$= %senderName)
						messageClient(%TrueClientId, 'RPGchatCallback', "Could not process command: Target admin clearance level too high.");
					else if(%id !$= -1)
					{
						%vel = %id.player.getVelocity();
						messageClient(%TrueClientId, 'RPGchatCallback', %name @ " (" @ %id @ ") velocity: " @ %vel);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
				}
				return;

			case "#onconsider":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%event = strstr(%cropped, ">");
						if(%event !$= -1)
						{
							%info = getsubstr(%cropped, 0, %event);
							%cmd = getsubstr(%cropped, %event, 99999);
						}
						else
							%info	= %cropped;

						%tag = GetWord(%info, 0);
						%object = $tagToObjectId[%tag];

						if(%object !$= "")
						{
							%radius = GetWord(%info, 1);
							%keep = GetWord(%info, 2);

							if(%keep $= "true" || %keep $= "false")
							{
								%targetname = GetWord(%info, 3);
								%tid = getClientByName(%targetname);
								if(stricmp(%targetname, "all") $= 0 || %tid !$= -1)
								{
									if(%event !$= -1)
									{
										AddEventCommand(%object, %senderName, "onConsider " @ %radius @ " " @ %keep @ " " @ %targetname, %cmd);
										if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', "onConsider event set for tagname " @ %tag @ "(" @ %object @ ") for radius " @ %radius);
									}
									else
										messageClient(%TrueClientId, 'RPGchatCallback', "onConsider event definition failed.");
								}
								else
									messageClient(%TrueClientId, 'RPGchatCallback', "Invalid name. Please specify 'all' or target's name.");
							}
							else
								messageClient(%TrueClientId, 'RPGchatCallback', "Specify 'true' or 'false'. 'true' means that the onConsider event won't be deleted after use.");
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid tagname.");
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "#onconsider tagname radius keep all/targetname");
				}
				return;

			case "#listonconsider":
				if(%clientToServerAdminLevel >= 3)
				{
					if(%cropped !$= "")
					{
						%tag = GetWord(%cropped, 0);
						%object = $tagToObjectId[%tag];

						if(%object !$= "")
						{
							%index = GetEventCommandIndex(%object, "onConsider");
							if(%index !$= -1)
							{
								for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
									messageClient(%TrueClientId, 'RPGchatCallback', %tag @ " (" @ %object @ ") onConsider " @ %index2 @ ": " @ $EventCommand[%object, %index2]);
							}
						}
						else
							messageClient(%TrueClientId, 'RPGchatCallback', "Invalid tagname.");
					}
					else
					{
						%list = $DISlist;
						for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
						{
							%w = getsubstr(%list, 0, %p);
							%object = $tagToObjectId[%w];

							%index = GetEventCommandIndex(%object, "onConsider");
							if(%index !$= -1)
								messageClient(%TrueClientId, 'RPGchatCallback', %w @ ": " @ %index);
						}
					}
				}
				return;

			case "#clearonconsider":
				if(%clientToServerAdminLevel >= 3)
				{
					%tag = GetWord(%cropped, 0);
					%object = $tagToObjectId[%tag];

					if(%object !$= "")
					{
						%oindex = GetWord(%cropped, 1);

						%index = GetEventCommandIndex(%object, "onConsider");
						if(%index !$= -1)
						{
							for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
							{
								if(mfloor(%index2) $= mfloor(%oindex) || %oindex $= -1)
								{
									$EventCommand[%object, %index2] = "";
									if(!%echoOff) messageClient(%TrueClientId, 'RPGchatCallback', %tag @ " (" @ %object @ ") onConsider " @ %index2 @ " cleared.");
								}
							}
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Incorrect tagname for #clearonconsider tagname [index]. If index is missing or -1, all onConsiders for name are cleared.");
				}
				return;
		}
	}
	
	//========== BOT TALK ======================================================================================

	if(%botTalk)
	{
		//process TownBot talk

		%initTalk = "";
		for(%i = 0; (%w = GetWord("hail hello hi greetings yo hey sup salutations g'day howdy", %i)) !$= ""; %i++)
			if(stricmp(%cropped, %w) $= 0)
				%initTalk = true;

		%clientPos = %TrueClientId.player.getTransform();
		%closest = 5000000;

		for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
		{
			
			%botPos = %id.getTransform();
			%dist = VectorDist(%clientPos, %botPos);
	
			if(%dist < %closest)
			{
				%closest = %dist;
				%closestId = %id;
				%closestPos = %botPos;
			}
		}
		

		%aiName = %closestId.name;//hrm
		%aiType = %closestID.mtype;
		%displayName = %aiName;

		if(%closest <= ($maxAIdistVec + (%TrueClientId.PlayerSkill[$SkillSpeech] / 50)) && %TrueClientId.team $= %closestId.team)
		{
			
			if(%aitype $= "merchant")
			{
				//process merchant code
				%trigger[2] = "buy";
                %trigger[3] = "yes";
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						AI::sayLater(%TrueClientId, %closestId, "Did you come to see what items you can BUY?", true);
						$state[%closestId, %TrueClientId] = 1;
					}
				}
				else if($state[%closestId, %TrueClientId] $= 1)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						$state[%closestId, %TrueClientId] = "";
						SetupShop(%TrueClientId, %closestId);

						AI::sayLater(%TrueClientId, %closestId, "Take a look at what I have.", true);
					}
                    if(strstr(%message, %trigger[3]) !$= -1)
					{
						$state[%closestId, %TrueClientId] = "";
						SetupShop(%TrueClientId, %closestId);

						AI::sayLater(%TrueClientId, %closestId, "Take a look at what I have.", true);
					}
				}
				return;
			}

			else
			if(%aitype $= "bank")
			{
				//process banker code
				%trigger[2] = "yes";
				%trigger[3] = "no";
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						AI::sayLater(%TrueClientId, %closestId, "Would you like to access your bank account? (Yes or No)", true);
						$state[%closestId, %TrueClientId] = 1;
					}
				}
				if($state[%closestId, %TrueClientId] $= 1)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						//deposit question
						AI::sayLater(%TrueClientId, %closestId, "Ok, here is what you have stored here.", true);
						setupbank(%TrueClientId, %closestId);
						$state[%closestId, %TrueClientId] = "";
					}
					if(strstr(%message, %trigger[3]) !$= -1)
					{
						//withdraw question
						AI::sayLater(%TrueClientId, %closestId, "Good day, sir.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
			}
			
			if(%aitype $= "assassin")
			{
				//process assassin code
				%trigger[2] = "yes";
				%trigger[3] = "no";
				%trigger[4] = "buy";
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						%highest = -1;
						%list = GetPlayerIdList();
						for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
						{
							if(fetchData(%id, "bounty") $= "")
								storeData(%id, "bounty", 0);
							if(fetchData(%id, "bounty") > %highest)
							{
								%h = %id;
								%highest = fetchData(%id, "bounty");
							}
						}
						%n = %h.nameBase;
						%c = fetchData(%h, "bounty");

						AI::sayLater(%TrueClientId, %closestId, "The highest bounty is currently on " @ %n @ " for $" @ %c @ ". Give me someone's name and I'll tell you their bounty, unless you want to BUY something." , true);

						$state[%closestId, %TrueClientId] = 1;
					}
				}
				else if($state[%closestId, %TrueClientId] $= 1)
				{
					if(strstr(%message, %trigger[4]) !$= -1)
					{
						%cost = GetLCKcost(%TrueClientId);

						AI::sayLater(%TrueClientId, %closestId, "I will sell you one LCK point for $" @ %cost @ ". (YES/NO)", true);
						$state[%closestId, %TrueClientId] = 2;
					}
					else
					{
						%lowest = 99999;
						%h = "";
						%list = GetPlayerIdList();
						for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
						{
							%comp = stricmp(%cropped, %id.nameBase);
							if(%comp < 0) %comp = -%comp;

							if(%comp < %lowest)
							{
								%h = %id;
								%lowest = %comp;
							}
						}
						if(%h !$= "")
						{
							%l = fetchData(%h, "LVL");
							%c = getFinalCLASS(%h);
							AI::sayLater(%TrueClientId, %closestId, "Are you talking about " @ %h.nameBase @ " the Level " @ %l @ " " @ %c @ "?", true);
							storeData(%TrueClientId, "tmpdata", %h);
							$state[%closestId, %TrueClientId] = 3;
						}
						else
						{
							AI::sayLater(%TrueClientId, %closestId, "I have no idea who you are talking about. Goodbye.", true);
							$state[%closestId, %TrueClientId] = "";
						}
					}
				}
				else if($state[%closestId, %TrueClientId] $= 2)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						%cost = GetLCKcost(%TrueClientId);

						if(fetchData(%TrueClientId, "COINS") >= %cost)
						{
							AI::sayLater(%TrueClientId, %closestId, "Here's your LCK point, thanks for your business.", true);
							GiveThisStuff(%TrueClientId, "LCK 1", true);
							storeData(%TrueClientId, "COINS", %cost, "dec");
							RefreshAll(%TrueClientId);
						}
						else
							AI::sayLater(%TrueClientId, %closestId, "You can't afford this.", true);

						$state[%closestId, %TrueClientId] = "";
					}
					else if(strstr(%message, %trigger[3]) !$= -1)
					{
						AI::sayLater(%TrueClientId, %closestId, "See ya.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
				else if($state[%closestId, %TrueClientId] $= 3)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						%id = fetchData(%TrueClientId, "tmpdata");
						if(%id !$= %TrueClientId)
						{
							%n = %id.nameBase;
							if(IsInCommaList(fetchData(%TrueClientId, "TempKillList"), %n))
							{
								storeData(%TrueClientId, "TempKillList", RemoveFromCommaList(fetchData(%TrueClientId, "TempKillList"), %n));
								AI::sayLater(%TrueClientId, %closestId, "I see you've killed " @ %n @ ". Here's your reward... " @ fetchData(%id, "bounty") @ " coins. Goodbye.", true);
								storeData(%TrueClientId, "COINS", fetchData(%id, "bounty"), "inc");
								storeData(%id, "bounty", 0);
	
								%TrueClientId.player.playAudio(0, SoundMoney1);
								RefreshAll(%TrueClientId);
							}
							else
								AI::sayLater(%TrueClientId, %closestId, %n @ "'s bounty is currently at " @ fetchData(%id, "bounty") @ " coins. Goodbye.", true);
						}
						else
							AI::sayLater(%TrueClientId, %closestId, "You can't get a reward for killing yourself... idiot.", true);

						$state[%closestId, %TrueClientId] = "";
					}
					else if(strstr(%message, %trigger[3]) !$= -1)
					{
						AI::sayLater(%TrueClientId, %closestId, "Well then, I have no idea who you are talking about. Goodbye.", true);
						storeData(%TrueClientId, "tmpdata", "");
						$state[%closestId, %TrueClientId] = "";
					}
				}
			}
			else if(%aitype $= "quest")
			{
				Schedule(0, game, "QUEST" @ %closestid.cFunction, %closestid.qobj,  %closestid, %TrueClientId, %cropped);
			}
			else if(%aitype $= "Porter")
			{
				//process manager code
				%trigger[2] = "fight";
				%trigger[3] = "refuse";
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						
						//AI::sayLater(%TrueClientId, %closestId, "Hail. Welcome to the arena, where the strongest warriors in the land compete for dominance! You may chose to FIGHT or you may REFUSE.", true);
						AI::sayLater(%TrueClientId, %closestId, "Sorry, arena is currently closed. Come back later!", true);
						$state[%closestId, %TrueClientId] = 0;
					}
				}
				else if($state[%closestId, %TrueClientId] $= 1)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						//FIGHT
						%x = AddToRoster(%TrueClientId);
						if(%x !$= 0)
						{
							//TeleportToMarker(%TrueClientId, "TheArena/WaitingRoomMarkers", 0, 1);

							$state[%closestId, %TrueClientId] = "";
							MessageClient(%TrueClientId, 'RPGchatcallback', "You have entered the waiting room. To leave the wait room type #leavearena");
						}
						else
						{
							//arena is full
							AI::sayLater(%TrueClientId, %closestId, "Sorry, the arena roster is full right now.", true);
							$state[%closestId, %TrueClientId] = "";
						}
					}
					else if(strstr(%message, %trigger[3]) !$= -1)
					{
					
	
							AI::sayLater(%TrueClientId, %closestId, "That is too bad. However, we plan on some major improvements in the future, so I hope you choose to come fight later.", true);
					
					}
				}
			}
			else if(%aitype $= "botmaker")
			{
				//process botmaker code
				%trigger[2] = "yes";
				%trigger[3] = "no";
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						if(CountObjInCommaList($PetList) >= $maxPets)
						{
							AI::sayLater(%TrueClientId, %closestId, "I'm sorry but all my helpers are already on duty.", true);
							$state[%closestId, %TrueClientId] = "";
						}
						else if(CountObjInCommaList(fetchData(%TrueClientId, "PersonalPetList")) >= $maxPetsPerPlayer)
						{
							AI::sayLater(%TrueClientId, %closestId, "I'm sorry but you have too many helpers currently at your disposal.", true);
							$state[%closestId, %TrueClientId] = "";
						}
						else
						{
							AI::sayLater(%TrueClientId, %closestId, "I have all sorts of helpers at my disposal. Tell me which class you are interested in.", true);
							$state[%closestId, %TrueClientId] = 1;
						}
					}
				}
				else if($state[%closestId, %TrueClientId] $= 1)
				{
					%class = GetWord(%cropped, 0);
					%gender = GetWord(%cropped, 1);
					%defaults = $BotInfo[%aiName, DEFAULTS, %class];
					if(%gender $= -1)
						%gender = "Male";

					if(stricmp(%gender, "male") $= 0)
					{
						%gender = "Male";
						%gflag = true;
					}
					else if(stricmp(%gender, "female") $= 0)
					{
						%gender = "Female";
						%gflag = true;
					}

					if(stricmp(%class, "mage") $= 0)
						%class = "Mage";
					else if(stricmp(%class, "fighter") $= 0)
						%class = "Fighter";
					else if(stricmp(%class, "paladin") $= 0)
						%class = "Paladin";
					else if(stricmp(%class, "thief") $= 0)
						%class = "Thief";
					else if(stricmp(%class, "bard") $= 0)
						%class = "Bard";
					else if(stricmp(%class, "ranger") $= 0)
						%class = "Ranger";
					else if(stricmp(%class, "cleric") $= 0)
						%class = "Cleric";
					else if(stricmp(%class, "druid") $= 0)
						%class = "Druid";

					if(%defaults !$= "")
					{
						if(%gflag)
						{
							%lvl = GetStuffStringCount(%defaults, "LVL");
							%nc = mpow(%lvl, 2) * 3;
							$tmpdata[%TrueClientId, 1] = %class;
							$tmpdata[%TrueClientId, 2] = %gender;
							$tmpdata[%TrueClientId, 3] = %nc;	//just so the equation is only in one place.

							AI::sayLater(%TrueClientId, %closestId, "My " @ %class @ "s are Level " @ %lvl @ ", and will cost you " @ %nc @ " coins. [yes/no]", true);
							$state[%closestId, %TrueClientId] = 2;
						}
						else
						{
							AI::sayLater(%TrueClientId, %closestId, "Invalid gender. Use 'male' or 'female'.", true);
							$state[%closestId, %TrueClientId] = "";
						}
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "Invalid class. Use any of the following: mage fighter paladin ranger thief bard cleric druid.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
				else if($state[%closestId, %TrueClientId] $= 2)
				{
					if(strstr(%message, %trigger[2]) !$= -1)
					{
						%nc = $tmpdata[%TrueClientId, 3];

						if(%nc <= 0)
						{
							AI::sayLater(%TrueClientId, %closestId, "Invalid request.  Your transaction has been cancelled.~wError_Message.wav", true);
							$state[%closestId, %TrueClientId] = "";
						}
						else if(%nc <= fetchData(%TrueClientId, "COINS"))
						{
							%class = $tmpdata[%TrueClientId, 1];
							%gender = $tmpdata[%TrueClientId, 2];
							%defaults = $BotInfo[%aiName, DEFAULTS, %class];
							%lvl = GetStuffStringCount(%defaults, "LVL");
	
							storeData(%TrueClientId, "COINS", %nc, "dec");
							%closestId.player.playAudio(0, SoundMoney1);
							RefreshAll(%TrueClientId);
	
							%n = "";
							for(%i = 0; (%a = GetWord($BotInfo[%aiName, NAMES], %i)) !$= ""; %i++)
							{
								if(getClientByName(%a) $= -1)
								{
									%n = %a;
									break;
								}
							}
							if(%n $= "")
								%n = "generic";

							$BotEquipment[generic] = "CLASS " @ %class @ " " @ %defaults;
							%an = AI::helper("generic", %n, "TempSpawn " @ $BotInfo[%aiName, DESTSPAWN].player.getPosition() @ " " @ GameBase::getTeam(%TrueClientId));
							%id = AI::getId(%an);
							%id.sex = %gender;
							ChangeRace(%id, "Human");
							storeData(%id, "tmpbotdata", %TrueClientId);
							storeData(%id, "botAttackMode", 2);

							schedule(55*60*1000, "Pet::BeforeTurnEvil(" @ %id @ ");");
							schedule(60*60*1000, "Pet::TurnEvil(" @ %id @ ");");

							$PetList = AddToCommaList($PetList, %id);
							storeData(%TrueClientId, "PersonalPetList", AddToCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id));
							storeData(%id, "petowner", %TrueClientId);
						
							AI::sayLater(%TrueClientId, %closestId, "This is " @ %n @ ", a Level " @ %lvl @ " " @ %class @ "! He is at your disposal. He will follow you around and fight for you for the next hour.", true);
							$state[%closestId, %TrueClientId] = "";
						}
						else
						{
							AI::sayLater(%TrueClientId, %closestId, "You don't have enough coins. Goodbye.", true);
							$state[%closestId, %TrueClientId] = "";
						}

					}
					else if(strstr(%message, %trigger[3]) !$= -1)
					{
						AI::sayLater(%TrueClientId, %closestId, "As you wish. Goodbye.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
			}
			else if(%aitype$= "Boat")
			{
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%inittalk)
					{
						AI::sayLater(%TrueClientId, %closestId, "Hello traveller! Welcome to my boat shop, would you like to RENT a boat for 100 coins?");
					}
					else if(strstr(%message, "rent") !$= -1)
					{
						if(fetchdata(%TrueClientId, "COINS") >= 100)
						{
							%obj = Boat @ %closestid.shop;
							if(isobject(%obj))
							{
		
								%flag = false;
								for(%i = 0; %i < %obj.getCount(); %i++)
								{
									
									%zobj = %obj.getObject(%i);
									InitContainerRadiusSearch(VectorAdd(%zobj.getWorldBoxCenter(), "-2 -2 -2"), "4 4 4", $TypeMasks::VehicleObjectType );
									%ret = containerSearchNext();
									if(%ret == 0)
									{
										%flag = true;
										break;
									}
								}
								if(%flag)
								{
									if(%TrueClientId.boat)
									{
										%TrueClientId.Boat.delete();
									}
									AI::sayLater(%TrueClientId, %closestId, "Ok here you go!");
									%spawnpos = %zobj.getWorldBoxCenter();
									
									%boat = new HoverVehicle() 
									{
									         dataBlock  = RPGBoat;
					
     									};
     									%waterheight = getword(VectorAdd(GlobalWater.position, GlobalWater.scale) , 2);
     									%spawnpos = GetWord(%spawnpos, 0) SPC Getword(%spawnpos, 1) SPC %waterheight+3;
     									%ztran = %zobj.getTransform();
     									//%zrot = getword(%ztran, 3) SPC getword(%ztran, 4) SPC getword(%ztran, 5) SPC getword(%ztran, 6);
					
         								%zrot = getWords(%ztran, 3, 5);
         								%angle =  1;
									
									%boat.setTransform(%spawnpos SPC %zrot SPC %angle);
									MissionCleanup.add(%boat);
									
									%boat.owner = %trueclientid;
									
									%zobj.full = true;
									%trueclientid.boat = %boat;
									StoreData(%TrueClientId, "COINS", 100, "dec");
								}
								else
									AI::sayLater(%TrueClientId, %closestId, "Sorry I do not have any spots availible, come back later.");
							}
							else
								AI::sayLater(%TrueClientId, %closestId, "Sorry I do not have any spots availible, come back later. [ERROR]");
						}
						else 
							AI::sayLater(%TrueClientId, %closestId, "Come back when you have enough money!");
					}
				
				}
			}
			else if(%aiType $= "Guild")
			{
				
				//process guildmaster code
				if($state[%closestId, %TrueClientId] $= "")
				{
					if(%initTalk)
					{
						if(fetchData(%TrueClientId, "LVL") >= 25)
						{
								AI::sayLater(%TrueClientId, %closestId, "Hello adventurer. Here is a list of the guilds currently registered in this land.", true);
								$state[%closestId, %TrueClientId] = "";
								CommandToClient(%TrueClientId, 'OpenGuildGUI');
						}
						else
						{
							AI::sayLater(%TrueClientId, %closestId, "Come back when you are at least level 25. Goodbye.", true);
							$state[%closestId, %TrueClientId] = "";
						}
					}
				}
				
			}
		}
		else
		{
			//This condition occurs when you are talking from too far of any TownBot.  All states are cleared here.
			//This means that potentially, you could initiate a conversation with the banker, travel for an hour
			//WITHOUT saying a word, come back and continue the conversation.  As soon as you speak in a way that
			//townbots hear you (#say, #shout, #tell) and are too far from them, all conversations are reset.

			for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
				$state[%id, %TrueClientId] = "";
		}
	}
}
function AI::sayLater(%clientID, %aiId, %message, %all)
{
	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);

		%talkingPos = %aiId.getPosition();
		if(!isobject(%cl.player)) continue;
		%receivingPos = %cl.player.getPosition();
		%distVec = VectorDist(%talkingPos, %receivingPos);
		if(%distVec <= $maxSAYdistVec+5)
		{
			//%newmsg = FadeMsg(%cropped, %distVec, $maxSAYdistVec);
			%newmsg = %cropped;
			messageClient(%cl, 'RPGchatCallback', %aiID.name @ " says, \"" @ %message @ "\"");
		}
	}
	//messageClient(%TrueClientId, 'RPGchatCallback', "You say, \"" @ %cropped @ "\"");
	//UseSkill(%TrueClientId, $SkillSpeech, true, true);

	//%botTalk = true;

}
function radiusAllExcept(%except1, %except2, %message)
{
	%epos1 = %except1.player.getPosition();
	%epos2 = %except2.player.getPosition();

	%count = ClientGroup.getCount();
	for(%icl = 0; %icl < %count; %icl++)
	{
		%cl = ClientGroup.getObject(%icl);
		if(%cl.player)
		{
		%clpos = %cl.player.getPosition();
		%dist1 = VectorDist(%clpos, %epos1);
		%dist2 = VectorDist(%clpos, %epos2);
			if(%cl !$= %except1 && %cl !$= %except2 && !IsDead(%cl))
			{
				if(%dist1 <= $maxSAYdistVec || %dist2 <= $maxSAYdistVec)
					messageClient(%cl, 'radiusAllExcept', %message);
			}
		}
	}
}

function FadeMsg(%txt, %dist, %max)
{
	if(%dist <= %max)
		return %txt;
	else
	{
		for(%i = 0; (%z = GetWord(%txt, %i)) !$= ""; %i++)
			%ntxt = %ntxt @ %z;
		%lntxt = strlen(%ntxt);

		%x = %dist - %max;
		%amt = round((%x / %max) * %lntxt);

		%txt = BuildDotString(%txt, %amt);
		
		return %txt;
	}
}

function BuildDotString(%txt, %n)
{
	%len = strlen(%txt);

	//i currently dont really know any other way to put a certain amount of characters in a string in a random fashion
	//other than to "sprinkle" them on until the count is correct.  Maybe someday someone will decide to rework this
	//function and make it more CPU friendly.  Right now this method sucks.

	%retry = 0;
	for(%i = %n; %i > 0; %i)
	{
		%p = mfloor(getRandom() * %len);
		%a = getsubstr(%txt, %p, 1);
		if(%a !$= " " && %a !$= ".")
		{
			%txt = getsubstr(%txt, 0, %p) @ "." @ getsubstr(%txt, %p+1, 99999);
			%i--;
			%retry = 0;
		}
		else
			%retry++;

		if(%retry > 10)
			break;
	}
	return %txt;
}

function ClearBlockData(%name, %block)
{
	for(%i = 1; $BlockData[%name, %block, %i] !$= ""; %i++)
		$BlockData[%name, %block, %i] = "";
}

function ManageBlockOwnersList(%name)
{
	%client = getClientByName(%name);

	if(CountObjInCommaList($BlockList[%name]) > 0)
	{
		if(!IsInCommaList($BlockOwnersList, %name))
		{
			$BlockOwnersList = AddToCommaList($BlockOwnersList, %name);
			if(%name !$= "Server")
				$BlockOwnerAdminLevel[%name] = mfloor(%client.adminLevel);
		}
	}
	else
	{
		$BlockOwnersList = RemoveFromCommaList($BlockOwnersList, %name);
		if(%name !$= "Server")
			$BlockOwnerAdminLevel[%name] = "";
	}

	return $BlockOwnersList;
}

function ParseBlockData(%bd, %victimId, %killerId)
{
	//the passed variables MUST BE IN COMMALIST FORMAT!

	%vtype[1] = "^victimName";
	%vtype[2] = "^victimId";
	%vtype[3] = "^victimPos";
	%vtype[4] = "^victimRot";
	%vtype[5] = "^victimZoneId";
	%vtype[6] = "^victimZoneType";
	%vtype[7] = "^victimZoneDesc";
	%vtype[8] = "^victimClass";
	%vtype[9] = "^victimLevel";
	%vtype[10] = "^victimX";
	%vtype[11] = "^victimY";
	%vtype[12] = "^victimZ";
	%vtype[13] = "^victimR1";
	%vtype[14] = "^victimR2";
	%vtype[15] = "^victimR3";
	%vtype[16] = "^victimR4";
	%vtype[17] = "^victimCoins";
	%vtype[18] = "^victimBank";
	%vtype[19] = "^victimVelX";
	%vtype[20] = "^victimVelY";
	%vtype[21] = "^victimVelZ";

	%vtype[22] = "^killerName";
	%vtype[23] = "^killerId";
	%vtype[24] = "^killerPos";
	%vtype[25] = "^killerRot";
	%vtype[26] = "^killerZoneId";
	%vtype[27] = "^killerZoneType";
	%vtype[28] = "^killerZoneDesc";
	%vtype[29] = "^killerClass";
	%vtype[30] = "^killerLevel";
	%vtype[31] = "^killerX";
	%vtype[32] = "^killerY";
	%vtype[33] = "^killerZ";
	%vtype[34] = "^killerR1";
	%vtype[35] = "^killerR2";
	%vtype[36] = "^killerR3";
	%vtype[37] = "^killerR4";
	%vtype[38] = "^killerCoins";
	%vtype[39] = "^killerBank";
	%vtype[40] = "^killerVelX";
	%vtype[41] = "^killerVelY";
	%vtype[42] = "^killerVelZ";

	if(%victimId !$= "")
	{
		%vpos = %victimId.player.getPosition();
		%vrot = %victimId.player.rotation;
		%vvel = %victimId.player.getVelocity();

		%var[1] = %victimId.nameBase;
		%var[2] = %victimId;
		%var[3] = %vpos;
		%var[4] = %vrot;
		%var[5] = fetchData(%victimId, "zone");
		%var[6] = Zone::getType(fetchData(%victimId, "zone"));
		%var[7] = Zone::getDesc(fetchData(%victimId, "zone"));
		%var[8] = fetchData(%victimId, "CLASS");
		%var[9] = fetchData(%victimId, "LVL");
		%var[10] = GetWord(%vpos, 0);
		%var[11] = GetWord(%vpos, 1);
		%var[12] = GetWord(%vpos, 2);
		%var[13] = GetWord(%vrot, 0);
		%var[14] = GetWord(%vrot, 1);
		%var[15] = GetWord(%vrot, 2);
		%var[16] = GetWord(%vrot, 3);
		%var[17] = fetchData(%victimId, "COINS");
		%var[18] = fetchData(%victimId, "BANK");
		%var[19] = GetWord(%vvel, 0);
		%var[20] = GetWord(%vvel, 1);
		%var[21] = GetWord(%vvel, 2);
	}
	if(%killerId !$= "")
	{
		%kpos = %killerId.player.getPosition();
		%krot = %killerId.player.rotation;
		%kvel = %killerId.player.getVelocity();

		%var[22] = %killerId.nameBase;
		%var[23] = %killerId;
		%var[24] = %kpos;
		%var[25] = %krot;
		%var[26] = fetchData(%killerId, "zone");
		%var[27] = Zone::getType(fetchData(%killerId, "zone"));
		%var[28] = Zone::getDesc(fetchData(%killerId, "zone"));
		%var[29] = fetchData(%killerId, "CLASS");
		%var[30] = fetchData(%killerId, "LVL");
		%var[31] = GetWord(%kpos, 0);
		%var[32] = GetWord(%kpos, 1);
		%var[33] = GetWord(%kpos, 2);
		%var[34] = GetWord(%krot, 0);
		%var[35] = GetWord(%krot, 1);
		%var[36] = GetWord(%krot, 2);
		%var[37] = GetWord(%krot, 3);
		%var[38] = fetchData(%killerId, "COINS");
		%var[39] = fetchData(%killerId, "BANK");
		%var[40] = GetWord(%kvel, 0);
		%var[41] = GetWord(%kvel, 1);
		%var[42] = GetWord(%kvel, 2);
	}

	for(%i = 1; %vtype[%i] !$= ""; %i++)
		%bd = strreplace(%bd, %vtype[%i], %var[%i], true);

	return %bd;
}
function checkrecall(%client, %oldpos)
{
	
	if(%oldpos $= %client.player.getposition())
	FellOffMap(%client);
	storeData(%client, "tmprecall", false);
	
}
function endsurge(%client)
{
	CalculateBonusState(%client);
	debugBonusState(%client);//2
	//storedata(%client, "surge", false);
	schedule(15000, %client, "resetblocksurge", %client);
	weightcall(%client, false);
	MessageClient(%client, 'SurgeOff', "You are no longer surging.");
}
function resetblocksurge(%client)
{
	storedata(%client, "blocksurge", false);
}
function endberserk(%client)
{
	storedata(%client, "surge", false);
	schedule(15000, %client, "resetblockberserk", %client);
	weightcall(%client, false);
	MessageClient(%client, 'BerserkOff', "You are no longer berserk.");
}
function resetblockberserk(%client)
{
	storedata(%client, "blockberserk", false);
}

function domug(%client, %id,  %damloc)
{
	%fail = true;
	if((%reason = AllowedToSteal(%client)) $= "true")
	{
		if(SkillCanUse(%client, "#mug"))
		{
			
				if(%id.player.getClassName() $= "Player" )
				{
					//%damloc = %id.player.getDamageLocation(%pos);
					%tciskill = %client.PlayerSkill[$SkillStealing];
					%idskill = %id.playerSkill[$skillStealing];

					
					//%TrueClientId.stealType = 1;
					//SetupInvSteal(%TrueClientId, %id);
					//attempt to steal
					%inv = fetchdata(%id, "inventory");
					%num = getwordcount(%inv);
					//randomly select an item
					%itemid = GetWord(%inv, getrandom(0,%num-1));

					if(%itemid)
					{
						if($InvInfo[%itemid, equipped])
							%fail = true;
						else
						{
							%item = getItem(%itemid);

							%backfront = getword(%damloc, 1);
							if(%backfront $= "back_right" || %backfront $= "back" || %backfront $= "back_left" || %backfront $= "middle_back")
								%multi = 1;
							else
								%multi = 0.5;//odds of stealing 1/2

							if(!id.isAIControlled())
							%oddcalc = %num - 10;
							else
							%oddcalc = %num;
							if(%oddcalc < 0)
								%oddcalc = 0;
							%ooddcalc = %oddcalc * %tciskill * %multi;
							%doddcalc = 35 * %idskill + 10;
							%success = getRandom(-%doddcalc, %ooddcalc);

							if(%success > 0)
							%fail = false;
							else
							%fail = true;
						}

						//echo("MUG:" SPC -%doddcalc SPC %ooddcalc SPC %success SPC %tciskill SPC %idskill);
						if(!%fail)
						{
							RemoveFromInventory(%id, %itemid);
							AddToInventory(%client, %itemid);
							messageClient(%client, 'RPGchatCallback', "You sucessfully stole a" SPC GetFullItemName(%itemid));
							//MessageClient(%client, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
						}
						else
						{
							messageClient(%client, 'RPGchatCallback', "You failed to steal from" SPC %id.rpgname @ "!");
							//MessageClient(%client, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
							messageClient(%id, 'RPGchatCallback', %TrueClientid.rpgname SPC "failed to steal from you!");
							//failed to steal, notify victim? lashback?
						}

					}
					else
						messageClient(%client, 'RPGchatCallback', "Your target has no items on him");
				}
		}
		else
		{
			messageClient(%client, 'RPGchatCallback', "You can't pickpocket because you lack the necessary skills.");
			UseSkill(%TrueClientId, $SkillStealing, false, true);
		}
	}
	else
		messageClient(%client, 'RPGchatCallback', %reason);
	return !%fail;
}
