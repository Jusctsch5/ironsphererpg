

function findEmptySeat(%vehicle, %player, %forceNode)
{
   %node = -1;
   %dataBlock = %vehicle.getDataBlock();
   %dis = 25;
   %playerPos = getWords(%player.getTransform(), 0, 2);
   %message = "";
   %minNode = 0;
 if(%forceNode !$= "")
      %node = %forceNode;
   else
   {
      for(%i = 0; %i < %dataBlock.numMountPoints; %i++)
         if(!%vehicle.getMountNodeObject(%i))
         {
            %seatPos = getWords(%vehicle.getSlotTransform(%i), 0, 2);
            %disTemp = VectorLen(VectorSub(%seatPos, %playerPos));
            if(%disTemp <= %dis && ( %datablock.isProtectedMountPoint[%i] == false || %vehicle.owner == %player.client ))
            {
               %node = %i;
               %dis = %disTemp;
            }
         }
    }
   if(%node != -1 && %node < %minNode)
   {
      if(%message $= "")
      {
         if(%node == 0)
            %message = '\c2No node found.~wfx/misc/misc.error.wav';
         else
            %message = '\c2Only Scout or Assault Armors can use that position.~wfx/misc/misc.error.wav';
      }
      
      if(!%player.noSitMessage)
      {
         %player.noSitMessage = true;
         %player.schedule(2000, "resetSitMessage");
         messageClient(%player.client, 'MsgArmorCantMountVehicle', %message);   
      }
      %node = -1;
   }
   return %node;
}


//**************************************************************
// SOUNDS
//**************************************************************
datablock EffectProfile(ScoutEngineEffect)
{
   effectname = "vehicles/outrider_engine";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(ScoutThrustEffect)
{
   effectname = "vehicles/outrider_boost";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock AudioProfile(ScoutSqueelSound)
{
   filename    = "fx/vehicles/outrider_skid.wav";
   description = ClosestLooping3d;
   preload = true;
};

// Scout
datablock AudioProfile(ScoutEngineSound)
{
   filename    = "fx/vehicles/outrider_engine.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ScoutEngineEffect;
};

datablock AudioProfile(ScoutThrustSound)
{
   filename    = "fx/vehicles/outrider_boost.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ScoutThrustEffect;
};

//**************************************************************
// LIGHTS
//**************************************************************
datablock RunningLightData(WildcatLight1)
{
   radius = 1.0;
   color = "1.0 1.0 1.0 0.3";
   nodeName = "Headlight_node01";
   direction = "-1.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(WildcatLight2)
{
   radius = 1.0;
   color = "1.0 1.0 1.0 0.3";
   nodeName = "Headlight_node02";
   direction = "1.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(WildcatLight3)
{
   type = 2;
   radius = 100.0;
   color = "1.0 1.0 1.0 1.0";
   offset = "0.0 0.0 0.0";
   direction = "0.0 1.0 0.0";
   texture = "special/projheadlight";
};
datablock HoverVehicleData(RPGBoat) : WildcatDamageProfile
{
   spawnOffset = "0 0 2";

   floatingGravMag = 1.0;

   catagory = "Vehicles";
   shapeFile = "FishingBoat.dts";
   computeCRC = true;

   //debrisShapeName = "vehicle_grav_scout_debris.dts";
   //debris = ShapeDebris;
   renderWhenDestroyed = true;
   cantAbandon = true;
   drag = 0.01;
   density = 0.9;

   mountPose[0] = scoutRoot;
   //mountPose[1] = Root;
   cameraMaxDist = 5.0;
   cameraOffset = 0.7;
   cameraLag = 0.5;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = false;
   //explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   hitpoints = 600;
   maxDamage = 0.60;
   destroyedLevel = 0.60;

   isShielded = false;
   rechargeRate = 0.7;
   energyPerDamagePoint = 75;
   maxEnergy = 150;
   minJetEnergy = 15;
   jetEnergyDrain = 1.3;

   // Rigid Body
   mass = 600;
   bodyFriction = 200.0;
   bodyRestitution = 500.5;  
   softImpactSpeed = 20;       // Play SoftImpact Sound
   hardImpactSpeed = 28;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 100;
   speedDamageScale = 0.010;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23;
   collDamageMultiplier   = 0.030;

   dragForce            = 25 / 45.0;
   vertFactor           = 50.0;
   floatingThrustFactor = 0.01;// bound by [0, 1]

   mainThrustForce    = 10;
   reverseThrustForce = 6;
   strafeThrustForce  = 0.5;
   turboFactor        = 1.0;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 0.80;//minimum height off the ground
   stabLenMax = 0.80;//max height off the ground
   stabSpringConstant  = 30;//spring = up
   stabDampingConstant = 16;//damp = down =/

   gyroDrag = 16;
   normalForce = 30;
   restorativeForce = 50;
   steeringForce = 10;
   rollForce  = 1;//side to side / \
   pitchForce = 1;// up and down

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 0.0;
   dustHeight = 0.1;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   //jetSound         = ScoutSqueelSound;
   //engineSound      = ScoutEngineSound;
  // floatSound       = ScoutThrustSound;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0; 
   mediumSplashSoundVelocity = 20.0;   
   hardSplashSoundVelocity = 30.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterSoftSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeSoftSplashSound; 

   minMountDist = 4;

  // damageEmitter[0] = SmallLightDamageSmoke;
  // damageEmitter[1] = SmallHeavyDamageSmoke;
  // damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 0.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   //splashEmitter[0] = VehicleFoamDropletsEmitter;
   //splashEmitter[1] = VehicleFoamEmitter;
   
   //shieldImpact = VehicleShieldImpact;
   
   //forwardJetEmitter = WildcatJetEmitter;

   cmdCategory = Tactical;
   cmdIcon = CMDHoverScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
   targetNameTag = 'Boat';
   targetTypeTag = '';
   sensorData = VehiclePulseSensor;

   checkRadius = 1.7785;
   observeParameters = "1 10 10";

	DamageScale[$DamageType::Landing] = 1.0;
	DamageScale[$DamageType::Ground] = 1.0;
	DamageScale[$DamageType::Piercing] = 0.5;
	DamageScale[$DamageType::Slashing] = 1.0;
	DamageScale[$DamageType::Bludgeoning] = 2.0;
	DamageScale[$DamageType::Archery] = 0.0;
	DamageScale[$DamageType::Spell] = 1.0;
   //runningLight[0] = WildcatLight1;
   //runningLight[1] = WildcatLight2;
   //runningLight[2] = WildcatLight3;

   shieldEffectScale = "0.9375 1.125 0.6";
};

function RPGBoat::onadd(%this, %obj)
{
	Parent::onadd(%this, %obj);
	%obj.mountable = true;
	%obj.hp = %this.hitpoints;
}
function VehicleData::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType, %momVec, %theClient, %proj)
{

   %clAttacker = %sourceObject.client;
   if(%clAttacker $= "")
   {
   	%clAttacker = %sourceObject.sourceObject.client;
   }
   %value = %amount;
   if(%damagetype == $DamageType::Spell)
   {
	 if(%clAttacker.focus == true)
	{
		%focused = true;
		%clAttacker.focus = false;
	}
	%spell = %sourceobject.spell; //workaround

	if(%spell $= "")
	%spell = %clAttacker.lastspell;
	
	%clAttacker.lastspell = "";
	%weapon = %spell;
	%value = $spelldata[%spell, DamageMod];//booom!
	%element = $spelldata[%spell, Element];
	//For the case of SPELLS, the initial damage has already been determined before calling this function
	%skilltype = $Skill::OffensiveCasting;

	%dmg = %value;
	%value = (%dmg / 100) * GetPlayerSkill(%clAttacker, %skilltype) + %dmg/10;
	if(%clAttacker.isAiControlled())
		%value /= 2;//drop damage by 2 for bots, bots dont have to worry about mana.

	%value /= 10;
   }
   %amount = round(%value) / %data.hitpoints;

   %sourceClient = %clattacker;

 
    if(%sourceObject)
    {
        %targetObject.lastDamagedBy = %sourceObject;
        %targetObject.lastDamageType = %damageType;
    }
    else 
        %targetObject.lastDamagedBy = 0;


   // Scale damage type & include shield calculations...
   if (%data.isShielded)
      %amount = %data.checkShields(%targetObject, %position, %amount, %damageType);
   
   
   %damageScale = %data.damageScale[%damageType];
   if(%damageScale !$= "")
      %amount *= %damageScale;
      
   if(%amount != 0)
      %targetObject.applyDamage(%amount);
      
   if(%targetObject.getDamageState() $= "Destroyed" )
   {
      if( %momVec !$= "")
         %targetObject.setMomentumVector(%momVec);
   }
   if(%value > 0)
   {
   	MessageClient(%sourceClient, 'DamageBoat', $MsgLtBlue @ "You hit the boat for" SPC round(%value) SPC "points of damage!");
   }
   else
   	MessageClient(%sourceClient, 'DamageBoat', $MsgLtBlue @ "You hit the boat for no damage!");
}
