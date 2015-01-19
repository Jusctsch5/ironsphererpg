function CreateMiningSpot(%pos, %modifier)
{
	if($debugMode $= TRUE) echo("CreateMiningSpot(" @ %pos @ ", " @ %modifier @ ");");
	//spawn the camera at the current position
	%camera = new camera() { 
		Datablock = Observer;
		};
	MissionCleanup.add(%camera);
	%transform = %pos SPC "0" SPC "0" SPC "0" SPC "1";
	%camera.setTransform(%transform);
	%camera.modifier = %modifier;
	if(!isObject(RockGroup))
		new SimGroup (RockGroup);
	spawnRandomItem(%camera);
	//$SOMECAMERAOBJ = %camera;

}

function spawnrandomitem(%camera)
{
	if($debugMode $= TRUE) echo("spawnrandomitem(" @ %camera @ ");");
	//this is the camera object which we will 'randomly' rotate then spawn a 'gem' at the los!
	
	// get the transform of the camera and create a vector
	
	%eyeTrans = %camera.getTransform();
	%eyeVec =  getRandom()*2-1 SPC getRandom()*2-1 SPC -getRandom();
	// extract the position of the player's camera from the eye transform (first 3 words)
	%eyePos = posFromTransform(%eyeTrans);

	// normalize the eye vector
	%nEyeVec = VectorNormalize(%eyeVec);
	
	// scale (lengthen) the normalized eye vector according to the search range
	%scEyeVec = VectorScale(%nEyeVec, 500);
	
	// add the scaled & normalized eye vector to the position of the camera
	%eyeEnd = VectorAdd(%eyePos, %scEyeVec);
	
	// see if anything gets hit
	%searchResult = containerRayCast(%eyePos, %eyeEnd, $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType, %camera);
	
	if(%searchResult $= 0)
	{
		//echo("Error, mining camera could not identify a LOS");
	}
	else
	{
		if(getRandom() > 0.05)
		{
			%selected = "SmallRock";
			%gem = false;
			for(%i = $numstone; %i > 0; %i--)
			{
				//echo(%i);
				if(getRandom() > 0.66)
				{
					//echo("true");
					if(%i == 4 && %selected $= "Tin")
					{
					}
					else
					{
					%selected = $MiningList[%i];
					%ii = %i;
					}
				}
			}
		}
		else
		{
			//GEM!!!
			%gem= true;
			%selected = "Quartz";
			for(%i = $numgem; %i > 0; %i--)
			{
				if(getRandom() > 0.50)
				{
					%selected = $GemList[%i];
					%ii = %i;
				}
			}
		}
		%reward = %selected;
		%pos = getWords(%searchResult, 1, 3);
		%obj = new Item() {
			datablock = MineStone;
			rotation = "0 0 1 " @ (getRandom() * 360);
			reward = %reward;
			isminerock = true;
			gem = %gem;
			iteration = %ii;
		};
		schedule(50*1000,%obj, "removerock", %obj);
		%obj.setTransform(%pos SPC "0 0 1 " @ (getRandom() * 360));
		MissionCleanup.add(%obj);
		RockGroup.add(%obj);
		//echo("Rock spawned!" @ %reward);
	}			
	schedule(10*1000*%camera.modifier,%camera, "spawnrandomitem", %camera);
}
function removerock(%this)
{
	if($debugMode $= TRUE) echo("removerock(" @ %this @ ");");
	RockGroup.remove(%this);
	%this.delete();
}
function hasskilltomine(%client, %rock)
{
	if($debugMode $= TRUE) echo("hasskilltomine(" @ %client @ ", " @ %rock @ ");");
	%skillreq = $mine::skillreq[%rock];
	//echo(%skillreq SPC GetPlayerSkill(%client, $SkillMining));
	if(%skillreq > GetPlayerSkill(%client, $Skill::Mining)) return false;
	return true;
}
function SearchForRock(%pos)
{

	if(isobject(RockGroup))
	{
		%closest = 0;
		for(%i = 0; %i < RockGroup.getCount(); %i++)
		{
			%rock = RockGroup.getObject(%i);
			%check = false;
			%trans = %rock.getTransform();
			if(getword(%trans, 0)-1 < GetWord(%pos, 0) && getword(%trans,0)+1 > GetWord(%pos, 0))
				%check = true;
			if(%check && getword(%trans, 1)-1 < GetWord(%pos, 1) && getword(%trans,1)+1 > GetWord(%pos, 1))
				%check = true;
			else
				%check = false;

			if(%check && getword(%trans, 2)-1 < GetWord(%pos, 2) && getword(%trans,2)+1 > GetWord(%pos, 2))
				%check = true;
			else
				%check = false;
			if(%check)
			{
				%closest = %rock;
				break;
			}
		}
		if(%closest)
		{
			//do stuff here maybe not
		}
	}
	return %closest;
}