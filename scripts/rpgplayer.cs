// Load dts shapes and merge animations (moved to top of player.cs for testing)
exec("shapes/base_male.cs");
//exec("shapes/light_male.cs");
exec("shapes/mino.cs");
//exec("shapes/fish.cs");  damn fish.
//rpg's default armor for now
datablock PlayerData(MaleHumanArmor) : LightPlayerDamageProfile
{
	emap = true;

	className = Armor;
	shapeFile = "base_male.dts";

	cameraMaxDist = 3;
	computeCRC = false;

	canObserve = true;
	cmdCategory = "Clients";
	cmdIcon = CMDPlayerIcon;
	cmdMiniIconName = "commander/MiniIcons/com_player_grey";

	hudImageNameFriendly[0] = "";
	hudImageNameEnemy[0] = "";
	hudRenderModulated[0] = true;
	hudRenderAlways[0] = true;

	hudImageNameFriendly[1] = "";
	hudImageNameEnemy[1] = "";
	hudRenderModulated[1] = true;
	hudRenderAlways[1] = true;
	hudRenderCenter[1] = true;
	hudRenderDistance[1] = true;

	hudImageNameFriendly[2] = "";
	hudImageNameEnemy[2] = "";
	hudRenderModulated[2] = true;
	hudRenderAlways[2] = true;
	hudRenderCenter[2] = true;
	hudRenderDistance[2] = true;

	cameraDefaultFov = 90.0;
	cameraMinFov = 5.0;
	cameraMaxFov = 120.0;

	debrisShapeName = "debris_player.dts";
	debris = playerDebris;

	aiAvoidThis = true;

	minLookAngle = -1.4;
	maxLookAngle = 1.4;
	maxFreelookAngle = 3.0;

	mass = 80;
	drag = 0.3;
	maxdrag = 0.4;
	density = 10;
	maxDamage = 1.0;
	maxEnergy = 100;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 0.01;
	jetForce = 1.0;
	underwaterJetForce = 26.21 * 100 * 1.5;//swiming?
	underwaterVertJetFactor = 10000.0;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain = 0.06;
	minJetEnergy = 1.0;
	maxJetHorizontalPercentage = 10.0;

	runForce = 48 * 120 *2;
	runEnergyDrain = 0.005;
	minRunEnergy = 0.0;
	maxForwardSpeed = 10;
	maxBackwardSpeed = 9;
	maxSideSpeed = 9;

	maxUnderwaterForwardSpeed = 6;
	maxUnderwaterBackwardSpeed = 5.4;
	maxUnderwaterSideSpeed = 5.4;

	jumpForce = 6 * 90;
	jumpEnergyDrain = 0;
	minJumpEnergy = 0;
	jumpDelay = 0;
	
	recoverDelay = 9;
	recoverRunForceScale = 0.1;

	minImpactSpeed = 30;
	speedDamageScale = 0.02;

	//jetSound = none;
	//wetJetSound = none;
	//jetEmitter = none;

	boundingBox = "1.2 1.2 2.3";
	pickupRadius = 0.75;

	// damage location details
	boxNormalHeadPercentage       = 0.83;
	boxNormalTorsoPercentage      = 0.49;
	boxHeadLeftPercentage         = 0;
	boxHeadRightPercentage        = 1;
	boxHeadBackPercentage         = 0;
	boxHeadFrontPercentage        = 1;

	//Foot Prints
	decalData   = LightMaleFootprint;
	decalOffset = 0.25;

	footPuffEmitter = LightPuffEmitter;
	footPuffNumParts = 15;
	footPuffRadius = 0.20;

	//dustEmitter = LiftoffDustEmitter;

	splash = PlayerSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = PlayerFoamDropletsEmitter;
	splashEmitter[1] = PlayerFoamEmitter;
	splashEmitter[2] = PlayerBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	// Controls over slope of runnable/jumpable surfaces
	runSurfaceAngle  = 60;
	jumpSurfaceAngle = 60;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	maxJetForwardSpeed = 30;
	horizMaxSpeed = 680;
	horizResistSpeed = 33;
	horizResistFactor = 0.35;

	upMaxSpeed = 8000;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	// heat inc'ers and dec'ers
	heatDecayPerSec      = 1.0 / 30.0; // takes 30 seconds to clear heat sig.
	heatIncreasePerSec   = 0.1; // takes 30.0 seconds of constant jet to get full heat sig.

	footstepSplashHeight = 0.35;
	//Footstep Sounds
	LFootSoftSound       = LFootLightSoftSound;
	RFootSoftSound       = RFootLightSoftSound;
	LFootHardSound       = LFootLightHardSound;
	RFootHardSound       = RFootLightHardSound;
	LFootMetalSound      = LFootLightMetalSound;
	RFootMetalSound      = RFootLightMetalSound;
	LFootSnowSound       = LFootLightSnowSound;
	RFootSnowSound       = RFootLightSnowSound;
	LFootShallowSound    = LFootLightShallowSplashSound;
	RFootShallowSound    = RFootLightShallowSplashSound;
	LFootWadingSound     = LFootLightWadingSound;
	RFootWadingSound     = RFootLightWadingSound;
	LFootUnderwaterSound = LFootLightUnderwaterSound;
	RFootUnderwaterSound = RFootLightUnderwaterSound;
	LFootBubblesSound    = LFootLightBubblesSound;
	RFootBubblesSound    = RFootLightBubblesSound;
	movingBubblesSound   = ArmorMoveBubblesSound;
	waterBreathSound     = WaterBreathMaleSound;

	impactSoftSound      = ImpactLightSoftSound;
	impactHardSound      = ImpactLightHardSound;
	impactMetalSound     = ImpactLightMetalSound;
	impactSnowSound      = ImpactLightSnowSound;

	skiSoftSound         = SkiAllSoftSound;
	skiHardSound         = SkiAllHardSound;
	skiMetalSound        = SkiAllMetalSound;
	skiSnowSound         = SkiAllSnowSound;

	impactWaterEasy      = ImpactLightWaterEasySound;
	impactWaterMedium    = ImpactLightWaterMediumSound;
	impactWaterHard      = ImpactLightWaterHardSound;

	groundImpactMinSpeed    = 10.0;
	groundImpactShakeFreq   = "4.0 4.0 4.0";
	groundImpactShakeAmp    = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	exitingWater         = ExitingWaterLightSound;

	observeParameters = "0.5 4.5 4.5";
};

datablock PlayerData(DeathKnightArmor) : HeavyMaleHumanArmor
{
	emap = true;

	shapeFile = "Heavy_male.dts";

	mass = 180;
	drag = 1.0;
	maxdrag = 1.5;
	density = 10;
	maxDamage = 1.0;
	maxEnergy = 100;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 0.256;
	jetForce = 100 * 180;
	underwaterJetForce = 100 * 180 * 2.0;
	underwaterVertJetFactor = 1.5;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain =  0.0;
	minJetEnergy = 1;
	maxJetHorizontalPercentage = 1.0;

	runForce = 100 * 180;
	runEnergyDrain = 0;
	minRunEnergy = 0;
	maxForwardSpeed = 100;
	maxBackwardSpeed = 100;
	maxSideSpeed = 100;

	maxUnderwaterForwardSpeed = 100;
	maxUnderwaterBackwardSpeed = 100;
	maxUnderwaterSideSpeed = 100;

	recoverDelay = 1;
	recoverRunForceScale = 1.2;

	jumpForce = 20 * 180;
	jumpEnergyDrain = 0;
	minJumpEnergy = 0;
	jumpDelay = 0;
   
	// heat inc'ers and dec'ers
	heatDecayPerSec      = 1.0 / 30.0; // takes 30 seconds to clear heat sig.
	heatIncreasePerSec   = 0.0; // takes 3.0 seconds of constant jet to get full heat sig.

	// Controls over slope of runnable/jumpable surfaces
	runSurfaceAngle  = 90;
	jumpSurfaceAngle = 90;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	horizMaxSpeed = 80;
	horizResistSpeed = 20;
	horizResistFactor = 0.3;
	maxJetForwardSpeed = 180;

	upMaxSpeed = 40;
	upResistSpeed = 35;
	upResistFactor = 0.15;

	minImpactSpeed = 45;
	speedDamageScale = 0.006;

	//jetSound = none;
	//wetJetSound = none;
	//jetEmitter = none;

	boundingBox = "1.4 1.4 2.4";
   
	splash = PlayerSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = PlayerFoamDropletsEmitter;
	splashEmitter[1] = PlayerFoamEmitter;
	splashEmitter[2] = PlayerBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;   
	hardSplashSoundVelocity = 20.0;   
	exitSplashSoundVelocity = 5.0;

	observeParameters = "2.5 6.5 6.5";
};
datablock PlayerData(ElementalArmor) : LightPlayerDamageProfile
{
	emap = true;

	className = Armor;
	shapeFile = "base_male.dts";

	cameraMaxDist = 3;
	computeCRC = false;

	canObserve = true;
	cmdCategory = "Clients";
	cmdIcon = CMDPlayerIcon;
	cmdMiniIconName = "commander/MiniIcons/com_player_grey";

	hudImageNameFriendly[0] = "";
	hudImageNameEnemy[0] = "";
	hudRenderModulated[0] = true;
	hudRenderAlways[0] = true;

	hudImageNameFriendly[1] = "";
	hudImageNameEnemy[1] = "";
	hudRenderModulated[1] = true;
	hudRenderAlways[1] = true;
	hudRenderCenter[1] = true;
	hudRenderDistance[1] = true;

	hudImageNameFriendly[2] = "";
	hudImageNameEnemy[2] = "";
	hudRenderModulated[2] = true;
	hudRenderAlways[2] = true;
	hudRenderCenter[2] = true;
	hudRenderDistance[2] = true;

	cameraDefaultFov = 90.0;
	cameraMinFov = 5.0;
	cameraMaxFov = 120.0;

	debrisShapeName = "debris_player.dts";
	debris = playerDebris;

	aiAvoidThis = true;

	minLookAngle = -1.4;
	maxLookAngle = 1.4;
	maxFreelookAngle = 3.0;

	mass = 90;
	drag = 0.3;
	maxdrag = 0.4;
	density = 10;
	maxDamage = 1.0;
	maxEnergy = 100;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 7.0;
	jetForce = 150.0;
	underwaterJetForce = 26.21 * 100 * 1.5;//swiming?
	underwaterVertJetFactor = 10000.0;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain = 0.06;
	minJetEnergy = 1.0;
	maxJetHorizontalPercentage = 10.0;

	runForce = 55 * 90;
	runEnergyDrain = 0.005;
	minRunEnergy = 0.0;
	maxForwardSpeed = 25;
	maxBackwardSpeed = 15;
	maxSideSpeed = 20;

	maxUnderwaterForwardSpeed = 6;
	maxUnderwaterBackwardSpeed = 5.4;
	maxUnderwaterSideSpeed = 5.4;

	jumpForce = 6 * 90;
	jumpEnergyDrain = 1;
	minJumpEnergy = 1;
	jumpDelay = 0;
	
	recoverDelay = 9;
	recoverRunForceScale = 1.2;

	minImpactSpeed = 45;
	speedDamageScale = 0.004;

	//jetSound = none;
	//wetJetSound = none;
	//jetEmitter = none;

	boundingBox = "1.2 1.2 2.3";
	pickupRadius = 0.75;

	// damage location details
	boxNormalHeadPercentage       = 0.83;
	boxNormalTorsoPercentage      = 0.49;
	boxHeadLeftPercentage         = 0;
	boxHeadRightPercentage        = 1;
	boxHeadBackPercentage         = 0;
	boxHeadFrontPercentage        = 1;

	//Foot Prints
	decalData   = LightMaleFootprint;
	decalOffset = 0.25;

	footPuffEmitter = LightPuffEmitter;
	footPuffNumParts = 15;
	footPuffRadius = 0.20;

	//dustEmitter = LiftoffDustEmitter;

	splash = PlayerSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = PlayerFoamDropletsEmitter;
	splashEmitter[1] = PlayerFoamEmitter;
	splashEmitter[2] = PlayerBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	// Controls over slope of runnable/jumpable surfaces
	runSurfaceAngle  = 90;
	jumpSurfaceAngle = 90;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	maxJetForwardSpeed = 30;
	horizMaxSpeed = 680;
	horizResistSpeed = 33;
	horizResistFactor = 0.35;

	upMaxSpeed = 8000;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	// heat inc'ers and dec'ers
	heatDecayPerSec      = 1.0 / 30.0; // takes 3 seconds to clear heat sig.
	heatIncreasePerSec   = 0.0; // takes 3.0 seconds of constant jet to get full heat sig.

	footstepSplashHeight = 0.35;
	//Footstep Sounds
	LFootSoftSound       = LFootLightSoftSound;
	RFootSoftSound       = RFootLightSoftSound;
	LFootHardSound       = LFootLightHardSound;
	RFootHardSound       = RFootLightHardSound;
	LFootMetalSound      = LFootLightMetalSound;
	RFootMetalSound      = RFootLightMetalSound;
	LFootSnowSound       = LFootLightSnowSound;
	RFootSnowSound       = RFootLightSnowSound;
	LFootShallowSound    = LFootLightShallowSplashSound;
	RFootShallowSound    = RFootLightShallowSplashSound;
	LFootWadingSound     = LFootLightWadingSound;
	RFootWadingSound     = RFootLightWadingSound;
	LFootUnderwaterSound = LFootLightUnderwaterSound;
	RFootUnderwaterSound = RFootLightUnderwaterSound;
	LFootBubblesSound    = LFootLightBubblesSound;
	RFootBubblesSound    = RFootLightBubblesSound;
	movingBubblesSound   = ArmorMoveBubblesSound;
	waterBreathSound     = WaterBreathMaleSound;

	impactSoftSound      = ImpactLightSoftSound;
	impactHardSound      = ImpactLightHardSound;
	impactMetalSound     = ImpactLightMetalSound;
	impactSnowSound      = ImpactLightSnowSound;

	skiSoftSound         = SkiAllSoftSound;
	skiHardSound         = SkiAllHardSound;
	skiMetalSound        = SkiAllMetalSound;
	skiSnowSound         = SkiAllSnowSound;

	impactWaterEasy      = ImpactLightWaterEasySound;
	impactWaterMedium    = ImpactLightWaterMediumSound;
	impactWaterHard      = ImpactLightWaterHardSound;

	groundImpactMinSpeed    = 10.0;
	groundImpactShakeFreq   = "4.0 4.0 4.0";
	groundImpactShakeAmp    = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	exitingWater         = ExitingWaterLightSound;

	observeParameters = "0.5 4.5 4.5";
};
datablock PlayerData(ElfArmor) : LightPlayerDamageProfile
{
	emap = true;

	className = Armor;
	shapeFile = "base_male.dts";

	cameraMaxDist = 3;
	computeCRC = false;

	canObserve = true;
	cmdCategory = "Clients";
	cmdIcon = CMDPlayerIcon;
	cmdMiniIconName = "commander/MiniIcons/com_player_grey";

	hudImageNameFriendly[0] = "";
	hudImageNameEnemy[0] = "";
	hudRenderModulated[0] = true;
	hudRenderAlways[0] = true;

	hudImageNameFriendly[1] = "";
	hudImageNameEnemy[1] = "";
	hudRenderModulated[1] = true;
	hudRenderAlways[1] = true;
	hudRenderCenter[1] = true;
	hudRenderDistance[1] = true;

	hudImageNameFriendly[2] = "";
	hudImageNameEnemy[2] = "";
	hudRenderModulated[2] = true;
	hudRenderAlways[2] = true;
	hudRenderCenter[2] = true;
	hudRenderDistance[2] = true;

	cameraDefaultFov = 90.0;
	cameraMinFov = 5.0;
	cameraMaxFov = 120.0;

	debrisShapeName = "debris_player.dts";
	debris = playerDebris;

	aiAvoidThis = true;
	
	minLookAngle = -1.4;
	maxLookAngle = 1.4;
	maxFreelookAngle = 3.0;

	mass = 90;
	drag = 0.3;
	maxdrag = 0.4;
	density = 10;
	maxDamage = 1.0;
	maxEnergy = 100;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 0.2;
	jetForce = 0.0;
	underwaterJetForce = 26.21 * 100 * 1.5;//swiming?
	underwaterVertJetFactor = 10000.0;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain = 0.06;
	minJetEnergy = 1.0;
	maxJetHorizontalPercentage = 10.0;

	runForce = 50 * 90;
	runEnergyDrain = 0.005;
	minRunEnergy = 0.0;
	maxForwardSpeed = 13;
	maxBackwardSpeed = 11;
	maxSideSpeed = 11;

	maxUnderwaterForwardSpeed = 6;
	maxUnderwaterBackwardSpeed = 5.4;
	maxUnderwaterSideSpeed = 5.4;

	jumpForce = 6 * 120;
	jumpEnergyDrain = 0;
	minJumpEnergy = 1;
	jumpDelay = 0;
	
	recoverDelay = 9;
	recoverRunForceScale = 1.2;

	minImpactSpeed = 45;
	speedDamageScale = 0.004;
	


	//jetSound = none;
	//wetJetSound = none;
	//jetEmitter = none;

	boundingBox = "1.2 1.2 2.3";
	pickupRadius = 0.75;

	// damage location details
	boxNormalHeadPercentage       = 0.83;
	boxNormalTorsoPercentage      = 0.49;
	boxHeadLeftPercentage         = 0;
	boxHeadRightPercentage        = 1;
	boxHeadBackPercentage         = 0;
	boxHeadFrontPercentage        = 1;

	//Foot Prints
	decalData   = LightMaleFootprint;
	decalOffset = 0.25;

	footPuffEmitter = LightPuffEmitter;
	footPuffNumParts = 15;
	footPuffRadius = 0.20;

	//dustEmitter = LiftoffDustEmitter;

	splash = PlayerSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = PlayerFoamDropletsEmitter;
	splashEmitter[1] = PlayerFoamEmitter;
	splashEmitter[2] = PlayerBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	// Controls over slope of runnable/jumpable surfaces
	runSurfaceAngle  = 85;
	jumpSurfaceAngle = 80;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	maxJetForwardSpeed = 30;
	horizMaxSpeed = 680;
	horizResistSpeed = 33;
	horizResistFactor = 0.35;

	upMaxSpeed = 8000;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	// heat inc'ers and dec'ers
	heatDecayPerSec      = 1.0 / 30.0; // takes 3 seconds to clear heat sig.
	heatIncreasePerSec   = 0.0; // takes 3.0 seconds of constant jet to get full heat sig.

	footstepSplashHeight = 0.35;
	//Footstep Sounds
	LFootSoftSound       = LFootLightSoftSound;
	RFootSoftSound       = RFootLightSoftSound;
	LFootHardSound       = LFootLightHardSound;
	RFootHardSound       = RFootLightHardSound;
	LFootMetalSound      = LFootLightMetalSound;
	RFootMetalSound      = RFootLightMetalSound;
	LFootSnowSound       = LFootLightSnowSound;
	RFootSnowSound       = RFootLightSnowSound;
	LFootShallowSound    = LFootLightShallowSplashSound;
	RFootShallowSound    = RFootLightShallowSplashSound;
	LFootWadingSound     = LFootLightWadingSound;
	RFootWadingSound     = RFootLightWadingSound;
	LFootUnderwaterSound = LFootLightUnderwaterSound;
	RFootUnderwaterSound = RFootLightUnderwaterSound;
	LFootBubblesSound    = LFootLightBubblesSound;
	RFootBubblesSound    = RFootLightBubblesSound;
	movingBubblesSound   = ArmorMoveBubblesSound;
	waterBreathSound     = WaterBreathMaleSound;

	impactSoftSound      = ImpactLightSoftSound;
	impactHardSound      = ImpactLightHardSound;
	impactMetalSound     = ImpactLightMetalSound;
	impactSnowSound      = ImpactLightSnowSound;

	skiSoftSound         = SkiAllSoftSound;
	skiHardSound         = SkiAllHardSound;
	skiMetalSound        = SkiAllMetalSound;
	skiSnowSound         = SkiAllSnowSound;

	impactWaterEasy      = ImpactLightWaterEasySound;
	impactWaterMedium    = ImpactLightWaterMediumSound;
	impactWaterHard      = ImpactLightWaterHardSound;

	groundImpactMinSpeed    = 10.0;
	groundImpactShakeFreq   = "4.0 4.0 4.0";
	groundImpactShakeAmp    = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	exitingWater         = ExitingWaterLightSound;

	observeParameters = "0.5 4.5 4.5";
};

datablock PlayerData(MonsterArmor) : MaleHumanArmor
{
	emap = true;

	className = Armor;
	shapeFile = "bioderm_light.dts";
	cameraMaxDist = 3;
	computeCRC = false;

	rechargeRate = 5.01;
	jetForce = 1.0;
	underwaterJetForce = 26.21 * 100 * 1.5;//swiming?
	underwaterVertJetFactor = 10000.0;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain = 0.06;
	minJetEnergy = 1.0;
	maxJetHorizontalPercentage = 50.0;

	runForce = 48 * 90;
	runEnergyDrain = 0.005;
	minRunEnergy = 0.0;
	maxForwardSpeed = 10;
	maxBackwardSpeed = 9;
	maxSideSpeed = 9;

	maxUnderwaterForwardSpeed = 60;
	maxUnderwaterBackwardSpeed = 50.4;
	maxUnderwaterSideSpeed = 50.4;
	runSurfaceAngle  = 85;
	jumpSurfaceAngle = 80;
};
datablock PlayerData(GnollArmor) : MonsterArmor
{
	emap = true;
	className = Armor;
};
datablock PlayerData(MinotaurArmor) : MonsterArmor
{
	emap = true;
	className = Armor;
	shapeFile = "Mino.dts";
};
datablock PlayerData(OrcArmor) : MonsterArmor
{
	emap = true;
	className = Armor;
	shapeFile = "bioderm_medium.dts";
};
datablock PlayerData(OgreArmor) : MonsterArmor
{
	emap = true;
	className = Armor;
	shapeFile = "bioderm_heavy.dts";
};
datablock PlayerData(GoblinArmor) : MonsterArmor
{
	emap = true;
	className = Armor;
};
datablock PlayerData(MaleOrcArmor) : MonsterArmor
{
className = Armor;
	shapeFile = "bioderm_light.dts";
	cameraMaxDist = 3;
	computeCRC = false;
	emap = true;
};
datablock PlayerData(FishArmor) : LightPlayerDamageProfile
{
	emap = true;

	className = Armor;
	shapeFile = "fish.dts";

	cameraMaxDist = 3;
	computeCRC = false;

	canObserve = true;
	cmdCategory = "Clients";
	cmdIcon = CMDPlayerIcon;
	cmdMiniIconName = "commander/MiniIcons/com_player_grey";

	hudImageNameFriendly[0] = "";
	hudImageNameEnemy[0] = "";
	hudRenderModulated[0] = true;
	hudRenderAlways[0] = true;

	hudImageNameFriendly[1] = "";
	hudImageNameEnemy[1] = "";
	hudRenderModulated[1] = true;
	hudRenderAlways[1] = true;
	hudRenderCenter[1] = true;
	hudRenderDistance[1] = true;

	hudImageNameFriendly[2] = "";
	hudImageNameEnemy[2] = "";
	hudRenderModulated[2] = true;
	hudRenderAlways[2] = true;
	hudRenderCenter[2] = true;
	hudRenderDistance[2] = true;

	cameraDefaultFov = 90.0;
	cameraMinFov = 5.0;
	cameraMaxFov = 120.0;

	debrisShapeName = "debris_player.dts";
	debris = playerDebris;

	aiAvoidThis = true;

	minLookAngle = -1.4;
	maxLookAngle = 1.4;
	maxFreelookAngle = 3.0;

	mass = 10;
	drag = 0.3;
	maxdrag = 0.4;
	density = 2;
	maxDamage = 1.0;
	maxEnergy = 100;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 0.51;
	jetForce = 5.0;
	underwaterJetForce = 26.21 * 100 * 1.5;
	underwaterVertJetFactor = 80.0;
	jetEnergyDrain =  0.0;
	underwaterJetEnergyDrain = 0.01;
	minJetEnergy = 1.0;
	maxJetHorizontalPercentage = 1000.0;

	runForce = 48 * 90;
	runEnergyDrain = 0.005;
	minRunEnergy = 0.0;
	maxForwardSpeed = 0;
	maxBackwardSpeed = 0;
	maxSideSpeed = 0;

	maxUnderwaterForwardSpeed = 20;
	maxUnderwaterBackwardSpeed = 15.4;
	maxUnderwaterSideSpeed = 15.4;

	jumpForce = 100;
	jumpEnergyDrain = 0;
	minJumpEnergy = 1;
	jumpDelay = 1;
	
	recoverDelay = 9;
	recoverRunForceScale = 1.2;

	minImpactSpeed = 45;
	speedDamageScale = 0.004;

	//jetSound = none;
	//wetJetSound = none;
	//jetEmitter = none;

	boundingBox = "1.2 1.2 1.3";
	pickupRadius = 0.75;

	// damage location details
	boxNormalHeadPercentage       = 0.83;
	boxNormalTorsoPercentage      = 0.49;
	boxHeadLeftPercentage         = 0;
	boxHeadRightPercentage        = 1;
	boxHeadBackPercentage         = 0;
	boxHeadFrontPercentage        = 1;

	//Foot Prints
	decalData   = LightMaleFootprint;
	decalOffset = 0.25;

	footPuffEmitter = LightPuffEmitter;
	footPuffNumParts = 15;
	footPuffRadius = 0.20;

	dustEmitter = LiftoffDustEmitter;

	splash = PlayerSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = PlayerFoamDropletsEmitter;
	splashEmitter[1] = PlayerFoamEmitter;
	splashEmitter[2] = PlayerBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	// Controls over slope of runnable/jumpable surfaces
	runSurfaceAngle  = 70;
	jumpSurfaceAngle = 80;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	maxJetForwardSpeed = 30;
	horizMaxSpeed = 68;
	horizResistSpeed = 33;
	horizResistFactor = 0.35;

	upMaxSpeed = 8000;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	// heat inc'ers and dec'ers
	heatDecayPerSec      = 1.0 / 30.0; // takes 3 seconds to clear heat sig.
	heatIncreasePerSec   = 0.0; // takes 3.0 seconds of constant jet to get full heat sig.

	footstepSplashHeight = 0.35;
	//Footstep Sounds
	LFootSoftSound       = LFootLightSoftSound;
	RFootSoftSound       = RFootLightSoftSound;
	LFootHardSound       = LFootLightHardSound;
	RFootHardSound       = RFootLightHardSound;
	LFootMetalSound      = LFootLightMetalSound;
	RFootMetalSound      = RFootLightMetalSound;
	LFootSnowSound       = LFootLightSnowSound;
	RFootSnowSound       = RFootLightSnowSound;
	LFootShallowSound    = LFootLightShallowSplashSound;
	RFootShallowSound    = RFootLightShallowSplashSound;
	LFootWadingSound     = LFootLightWadingSound;
	RFootWadingSound     = RFootLightWadingSound;
	LFootUnderwaterSound = LFootLightUnderwaterSound;
	RFootUnderwaterSound = RFootLightUnderwaterSound;
	LFootBubblesSound    = LFootLightBubblesSound;
	RFootBubblesSound    = RFootLightBubblesSound;
	movingBubblesSound   = ArmorMoveBubblesSound;
	waterBreathSound     = WaterBreathMaleSound;

	impactSoftSound      = ImpactLightSoftSound;
	impactHardSound      = ImpactLightHardSound;
	impactMetalSound     = ImpactLightMetalSound;
	impactSnowSound      = ImpactLightSnowSound;

	skiSoftSound         = SkiAllSoftSound;
	skiHardSound         = SkiAllHardSound;
	skiMetalSound        = SkiAllMetalSound;
	skiSnowSound         = SkiAllSnowSound;

	impactWaterEasy      = ImpactLightWaterEasySound;
	impactWaterMedium    = ImpactLightWaterMediumSound;
	impactWaterHard      = ImpactLightWaterHardSound;

	groundImpactMinSpeed    = 10.0;
	groundImpactShakeFreq   = "4.0 4.0 4.0";
	groundImpactShakeAmp    = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	exitingWater         = ExitingWaterLightSound;

	observeParameters = "0.5 4.5 4.5";
};

%file = new FileObject();
%fname = "temp/playerload.cs";//our eval workaround
//Load the player tree
%file.openforwrite(%fname);
for($maxarmor = 0; $maxarmor <= 100; $maxarmor++)
{
//time to hack around the game 

	%factor = (100-$maxarmor)/50;
	%file.writeline("datablock PlayerData(MaleHumanArmor" @ $maxarmor @ ") : MaleHumanArmor ");
	%file.writeline("{");
	%file.writeline("runForce = 48 * 90 *" @ %factor @ ";");
	%file.writeline("runEnergyDrain = " @ 0.005 + (1 - %factor) / 1000 @ ";");
	%file.writeline("minRunEnergy = 0.0 ;");
	%file.writeline("maxForwardSpeed = " @ 10 * %factor @  ";");
	%file.writeline("maxBackwardSpeed = " @ 5 * %factor @  ";");
	%file.writeline("maxSideSpeed = " @ 7 * %factor @  ";");
	%file.writeline("jumpForce = " @ 6 * 90 * %factor @  ";");
	%file.writeline("jumpEnergyDrain = " @ 2 - %factor @  ";");
	%file.writeline(" emap = true;");
	%file.writeline("};");
}

//Notes: MaleHumanArmor70 is speed 0
//	MaleHumanArmor20 is normal speed
//	MaleHumanArmor0 is faster than normal.
%file.close();
$tmp = "";
compile(%fname);
exec(%fname);
%file.delete();//delete the file object...
deletefile(%fname);//bye bye temp file, save memory...
deletefile(%fname @ ".dso");
function Armor::onCollision(%this,%obj,%col,%forceVehicleNode)
{

   if (%obj.getState() $= "Dead")
      return;

   %dataBlock = %col.getDataBlock();
   %className = %dataBlock.className;
   %client = %obj.client;

   // player collided with a vehicle?
   %node = -1;
   if (%forceVehicleNode !$= "" || (%className $= WheeledVehicleData || %className $= FlyingVehicleData || %className $= HoverVehicleData) &&
         %obj.mountVehicle && %obj.getState() $= "Move" && %col.getDamageState() !$= "Destroyed") {
	return;
      
   }
   else if (%className $= "Armor") {

   }
}

function Player::onEndSequence(%data, %obj, %thread)
{
	//echo("%data: " @ %data);
	//echo("%obj: " @ %obj);
	//echo("%thread: " @ %thread);
}
function playDeathAnimation(%player, %damageLocation, %type)
{

   %player.stopThread(%client.currThread);
   cancel(%client.rootthread);
   %vertPos = firstWord(%damageLocation);
   %quadrant = getWord(%damageLocation, 1);
   
   //echo("vert Pos: " @ %vertPos);
   //echo("quad: " @ %quadrant);
   
   if( %type == $DamageType::Explosion || %type == $DamageType::Mortar || %type == $DamageType::Grenade) 
   {
      if(%quadrant $= "front_left" || %quadrant $= "front_right") 
         %curDie = $PlayerDeathAnim::ExplosionBlowBack;
      else
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if(%vertPos $= "head") 
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "front_right" ) 
         %curDie = $PlayerDeathAnim::HeadFrontDirect;
      else 
         %curDie = $PlayerDeathAnim::HeadBackFallForward;
   }
   else if(%vertPos $= "torso") 
   {
      if(%quadrant $= "front_left" ) 
         %curDie = $PlayerDeathAnim::TorsoLeftSpinDeath;
      else if(%quadrant $= "front_right") 
         %curDie = $PlayerDeathAnim::TorsoRightSpinDeath;
      else if(%quadrant $= "back_left" ) 
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
      else if(%quadrant $= "back_right") 
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if (%vertPos $= "legs") 
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "back_left") 
         %curDie = $PlayerDeathAnim::LegsLeftGimp;
      if(%quadrant $= "front_right" || %quadrant $= "back_right") 
         %curDie = $PlayerDeathAnim::LegsRightGimp;
   }
   
   if(%curDie $= "" || %curDie < 1 || %curDie > 11)
      %curDie = 1;
   if(!%player.client.isfish)
   %player.setActionThread("Death" @ %curDie);
}
function Armor::setActionThread(%this, %thread)
{
	//die!
}
function Armor::onMount(%this,%obj,%vehicle,%node)
{
   if(%this.client.isfish) return;
   if (%node == 0)
   {
      // Node 0 is the pilot's pos.
      %obj.setTransform("0 0 0 0 0 1 0");
      %obj.setActionThread(%vehicle.getDatablock().mountPose[%node],true,true);
   
      if(!%obj.inStation)
         %obj.lastWeapon = (%obj.getMountedImage($WeaponSlot) == 0 ) ? "" : %obj.getMountedImage($WeaponSlot).getName().item;
         
       //%obj.unmountImage($WeaponSlot);
   
      if(!%obj.client.isAIControlled())
      {
         %obj.setControlObject(%vehicle);
         %obj.client.setObjectActiveImage(%vehicle, 2);
      }
      
      //E3 respawn...
 
      if(%obj == %obj.lastVehicle.lastPilot && %obj.lastVehicle != %vehicle)
      {
         schedule(15000, %obj.lastVehicle,"vehicleAbandonTimeOut", %obj.lastVehicle);
          %obj.lastVehicle.lastPilot = "";
      }
      if(%vehicle.lastPilot !$= "" && %vehicle == %vehicle.lastPilot.lastVehicle)
            %vehicle.lastPilot.lastVehicle = "";
            
      %vehicle.abandon = false;
      %vehicle.lastPilot = %obj;
      %obj.lastVehicle = %vehicle;

      // update the vehicle's team
      if((%vehicle.getTarget() != -1) && %vehicle.getDatablock().cantTeamSwitch $= "")
      {   
         setTargetSensorGroup(%vehicle.getTarget(), %obj.client.getSensorGroup());
         if( %vehicle.turretObject > 0 )
            setTargetSensorGroup(%vehicle.turretObject.getTarget(), %obj.client.getSensorGroup());
      }

      // Send a message to the client so they can decide if they want to change view or not:
      commandToClient( %obj.client, 'VehicleMount' );

   }
   else
   {
      // tailgunner/passenger positions
      if(%vehicle.getDataBlock().mountPose[%node] !$= "")
         %obj.setActionThread(%vehicle.getDatablock().mountPose[%node]);
      else
         %obj.setActionThread("root", true);
   }
   // -------------------------------------------------------------------------
   // z0dd - ZOD, 10/06/02. announce to any other passengers that you've boarded
   if(%vehicle.getDatablock().numMountPoints > 1)
   {
      %nodeName = findNodeName(%vehicle, %node); // function in vehicle.cs
      for(%i = 0; %i < %vehicle.getDatablock().numMountPoints; %i++)
      {
         if (%vehicle.getMountNodeObject(%i) > 0)
         {
            if(%vehicle.getMountNodeObject(%i).client != %obj.client)
            {
               //%team = (%obj.team == %vehicle.getMountNodeObject(%i).client.team ? 'Teammate' : 'Enemy');
               //messageClient( %vehicle.getMountNodeObject(%i).client, 'MsgShowPassenger', '\c2%3: \c3%1\c2 has boarded in the \c3%2\c2 position.', %obj.client.name, %nodeName, %team );
            }
            commandToClient( %vehicle.getMountNodeObject(%i).client, 'showPassenger', %node, true);
         }
      }
   }
   //make sure they don't have any packs active
//    if ( %obj.getImageState( $BackpackSlot ) $= "activate")
//       %obj.use("Backpack");
   if ( %obj.getImageTrigger( $BackpackSlot ) )
      %obj.setImageTrigger( $BackpackSlot, false );

   //AI hooks
   %obj.client.vehicleMounted = %vehicle;
   AIVehicleMounted(%vehicle);
   if(%obj.client.isAIControlled())
      %this.AIonMount(%obj, %vehicle, %node);
}
function Armor::onEnterLiquid(%data, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0:
         //Water
      case 1:
         //Ocean Water
      case 2:
         //River Water
      case 3:
         //Stagnant Water
      case 4:
         //Lava
         %obj.liquidDamage(%data, $DamageLava, $DamageType::Lava);
      case 5:
         //Hot Lava
         %obj.liquidDamage(%data, $DamageHotLava, $DamageType::Lava);
      case 6:    
         //Crusty Lava
         %obj.liquidDamage(%data, $DamageCrustyLava, $DamageType::Lava);
      case 7:
         //Quick Sand
   }
   if(isobject(%obj.client))
  	 if(%obj.client.isaicontrolled())
  	 {
  	 	Game.onAIEnterLiquid(%data, %obj, %type);
  	 
  	 }
}

function Armor::onLeaveLiquid(%data, %obj, %type)
{
   switch(%type)
   {
      case 0:
         //Water
      case 1:
         //Ocean Water
      case 2:
         //River Water
      case 3:
         //Stagnant Water
      case 4:
         //Lava
      case 5:
         //Hot Lava
      case 6:
         //Crusty Lava
      case 7:
         //Quick Sand
   }

   if(%obj.lDamageSchedule !$= "")
   {
      cancel(%obj.lDamageSchedule);
      %obj.lDamageSchedule = "";
   }
     if(isobject(%obj.client))
    	 if(%obj.client.isaicontrolled())
    	 {
    	 	Game.onAILeaveLiquid(%data, %obj, %type);
    	 
  	 }
}
function Player::use( %this,%data )
{
   // If player is in a station then he can't use any items
   if(%this.station !$= "")
      return false;

   // Convert the word "Backpack" to whatever is in the backpack slot.
   if ( %data $= "Backpack" ) 
   {
      if ( %this.inStation )
         return false;

      if ( %this.isPilot() )
      {
        
       
        if(%this.getControlObject() == 0)
        {
        	
        	%vehicle = %this.lastVehicle;
        	echo(%vehicle SPC %vehicle.getDatablock().mountPose[0]);
		 %this.setTransform("0 0 0 0 0 1 0");
     		 %this.setActionThread(%vehicle.getDatablock().mountPose[0],true, true);
		%this.setControlObject(%vehicle);
        }
        else
        {
        	%this.setActionThread("root", true, true);
        	%this.setControlObject(0);
        }
      }
      //else if ( %this.isWeaponOperator() )
      //{
      //   messageClient( %this.client, 'MsgCantUsePack', '\c2You can\'t use your pack while in a weaponry position.~wfx/misc/misc.error.wav' );
      //   return( false );
      //}
      
      %image = %this.getMountedImage( $BackpackSlot );
      if ( %image )
         %data = %image.item;
   }

   // Can't use some items when piloting or your a weapon operator
   //if ( %this.isPilot() || %this.isWeaponOperator() ) 
   //   if ( %data.getName() !$= "RepairKit" )
   //      return false;
   
   return ShapeBase::use( %this, %data );
}
