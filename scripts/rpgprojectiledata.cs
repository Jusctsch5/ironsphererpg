$spelldamagetype = 8;
$DamageType::Spell = 8;
exec("scripts/weapons/chaingun.cs");
exec("scripts/weapons/plasma.cs");
exec("scripts/weapons/gernadelauncher.cs");
exec("scripts/weapons/SniperRifle.cs");
function ProjectileData::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal)
{

   %targetObject.damage(%projectile, %position, %data.directDamage * %modifier, %data.directDamageType);

}
function RadiusExplosion(%explosionSource, %position, %radius, %damage, %impulse, %sourceObject, %damageType)
{
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType      |
                                                 $TypeMasks::VehicleObjectType     |
                                                 $TypeMasks::StaticShapeObjectType |
                                                 $TypeMasks::TurretObjectType      |
                                                 $TypeMasks::ItemObjectType);

   %numTargets = 0;
   %targetlist = "";
   while ((%targetObject = containerSearchNext()) != 0)
   {
      %dist = containerSearchCurrRadDamageDist();

      if (%dist > %radius)
         continue;
	if(%targetObject.istownbot)
		continue;
      if (%targetObject.isMounted())
      {
         %mount = %targetObject.getObjectMount();
         %found = -1;
         for (%i = 0; %i < %mount.getDataBlock().numMountPoints; %i++)
         {
            if (%mount.getMountNodeObject(%i) == %targetObject)
            {
               %found = %i;
               break;
            }
         }
      
         if (%found != -1)
         {
            if (%mount.getDataBlock().isProtectedMountPoint[%found])
            {
               continue;
            }
         }
      }

      %targets[%numTargets]     = %targetObject;
     
      %targetDists[%numTargets] = %dist;
      %numTargets++;
   }

   for (%i = 0; %i < %numTargets; %i++)
   {
      %targetObject = %targets[%i];
      %dist = %targetDists[%i];

      %coverage = calcExplosionCoverage(%position, %targetObject,
                                        ($TypeMasks::InteriorObjectType |
                                         $TypeMasks::TerrainObjectType |
                                         $TypeMasks::ForceFieldObjectType |
                                         $TypeMasks::VehicleObjectType));
      if (%coverage == 0)
         continue;
 %targetlist = %targetObject SPC %targetlist;
      //if ( $splashTest )
         %amount = (1.0 - ((%dist / %radius) * 0.88)) * %coverage * %damage;
      //else
         //%amount = (1.0 - (%dist / %radius)) * %coverage * %damage;

      //error( "damage: " @ %amount @ " at distance: " @ %dist @ " radius: " @ %radius @ " maxDamage: " @ %damage );
      
      %data = %targetObject.getDataBlock();
      %className = %data.className;

      if (%impulse && %data.shouldApplyImpulse(%targetObject))
      {
         %p = %targetObject.getWorldBoxCenter();
         %momVec = VectorSub(%p, %position);
         %momVec = VectorNormalize(%momVec);
         %impulseVec = VectorScale(%momVec, %impulse * (1.0 - (%dist / %radius)));
         %doImpulse = true;
      }
      // ---------------------------------------------------------------------------
      // z0dd - ZOD, 5/8/02. Removed Wheeled Vehicle to eliminate the flying MPB bug 
      // caused by tossing concussion grenades under a deployed MPB.
      //else if( %className $= WheeledVehicleData || %className $= FlyingVehicleData || %className $= HoverVehicleData )
      else if( %className $= FlyingVehicleData || %className $= HoverVehicleData )
      {
         %p = %targetObject.getWorldBoxCenter();
         %momVec = VectorSub(%p, %position);
         %momVec = VectorNormalize(%momVec);
         
         %impulseVec = VectorScale(%momVec, %impulse * (1.0 - (%dist / %radius)));
         
         if( getWord( %momVec, 2 ) < -0.5 )
            %momVec = "0 0 1";
            
         // Add obj's velocity into the momentum vector
         %velocity = %targetObject.getVelocity();
         //%momVec = VectorNormalize( vectorAdd( %momVec, %velocity) );
         %doImpulse = true;
      }
      else
      {   
         %momVec = "0 0 1";
         %doImpulse = false;
      }
      %data2 = %explosionSource.getDataBlock();
      if(%amount > 0)
      {
      	//echo("spell:" @ %explosionsource.spell);
      	 %targetObject.damage(%explosionsource, %position, %data2.directDamage * (1.0 - (%dist / %radius)), %DamageType);
         //%data.damageObject(%targetObject, %sourceObject, %position, %amount, %damageType, %momVec, %explosionSource.theClient, %explosionSource);
      }
      else if( %explosionSource.getDataBlock().getName() $= "ConcussionGrenadeThrown" && %data.getClassName() $= "PlayerData" )
	  {
         %data.applyConcussion( %dist, %radius, %sourceObject, %targetObject );
 	  	
 	  	if(!$teamDamage && %sourceObject != %targetObject && %sourceObject.client.team == %targetObject.client.team)
 	  	{
			messageClient(%targetObject.client, 'msgTeamConcussionGrenade', '\c1You were hit by %1\'s concussion grenade.', getTaggedString(%sourceObject.client.name));
		}
	  }
      
      if( %doImpulse )
         %targetObject.applyImpulse(%position, %impulseVec);
   }
   return %targetlist;
}

datablock ParticleData(GrenadeSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;   // rises slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 700;  // lasts 2 second
   lifetimeVarianceMS   = 150;   // ...more or less

   textureName          = "particleTest";

   useInvAlpha = true;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.9 0.9 0.9 1.0";
   colors[1]     = "0.6 0.6 0.6 1.0";
   colors[2]     = "0.4 0.4 0.4 0.0";

   sizes[0]      = 0.25;
   sizes[1]      = 1.0;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(GrenadeSmokeEmitter)
{
   ejectionPeriodMS = 15;
   periodVarianceMS = 5;

   ejectionVelocity = 1.25;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = "GrenadeSmokeParticle";
};

datablock EnergyProjectileData(EnergyBlast)
{
   emitterDelay        = -1;
   directDamage        = 0.01;
   directDamageType    = $DamageType::Spell;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = BlasterProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "BlasterExplosion";
   splash              = BlasterSplash;


   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 15.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 200.0;
   wetVelocity       = 150.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "0.5 0.0 0.0";

   scale = "0.3 10.1 0.4";
   crossViewAng = 0.99;
   crossSize = 0.55;

   lifetimeMS     = 9000;
   blurLifetime   = 0.3;
   blurWidth      = 0.2;
   blurColor = "0.4 0.0 0.0 1.0";

   texture[0] = "special/blasterBolt";
   texture[1] = "special/blasterBoltCross";
};

// #######################
// AUDIO DATABLOCK - THORN
// #######################

datablock AudioProfile(ThornProjectile)
{
   filename    = "Spell_Ice.wav";
   description = ProjectileLooping3d;
   preload = true;
};


datablock TracerProjectileData(Thorn)
{
   doDynamicClientHits = true;

   directDamage        = 0.01;
   directDamageType    = $DamageType::Spell;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 		= ThornProjectile;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};
datablock LinearFlareProjectileData(FireBall2)
{
   projectileShapeName = "plasmabolt.dts";
   scale               = "2.0 2.0 2.0";
   faceViewer          = true;
   directDamage        = 0.00;
   hasDamageRadius     = true;
   indirectDamage      = 0.04;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $DamageType::Spell;
   
   explosion           = "PlasmaBoltExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 55.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;
   
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};
//ironfist



datablock ExplosionData(IronFistExplosion)
{
   soundProfile   = GrenadeExplosionSound;

   faceViewer           = true;
   explosionScale = "0.8 0.8 0.8";

   debris = GrenadeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 50;
   debrisNum = 8;
   debrisVelocity = 26.0;
   debrisVelocityVariance = 7.0;

   emitter[0] = GrenadeDustEmitter;
   emitter[1] = GExplosionSmokeEmitter;
   emitter[2] = GrenadeSparksEmitter;

   shakeCamera = true;
   camShakeFreq = "5.0 3.0 6.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 10;
   camShakeRadius = 10.0;
};

datablock LinearFlareProjectileData(IronFist)
{
   projectileShapeName = "ironfist.dts";
   scale               = "2.0 2.0 2.0";
   //faceViewer          = true;
   directDamage        = 0;
   hasDamageRadius     = true;
   indirectDamage      = 0.04;
   damageRadius        = 4.0;
   kickBackStrength    = 2500.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $DamageType::Spell;
   
   explosion           = "IronFistExplosion";
   splash              = GrenadeSplash;

   dryVelocity       = 55.0;
   wetVelocity       = 55.0;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "0 0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;
   
   hasLight    = false;
};


//Firebomb
//---------------------------------------------------------------


datablock GrenadeProjectileData(FireBomb)
{
   projectileShapeName = "plasmabolt.dts";
   faceViewer          = true;
   emitterDelay        = 0.1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.40;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Spell;
   kickBackStrength    = 10;
   bubbleEmitTime      = 1.0;

   sound               = PlasmaProjectileSound;
   explosion           = "PlasmaBoltExplosion";
   velInheritFactor    = 0.5;
   splash              = PlasmaSplash;

   explodeOnWaterImpact= true;
   baseEmitter         = "GrenadeSmokeEmitter";
  
   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 250;
   muzzleVelocity    = 47.00;
   drag = 0.1;
  
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";  
   
};


// #######################
// AUDIO DATABLOCK - ICE SPIKE
// #######################

datablock AudioProfile(IceProjectile)
{
   filename    = "spell_beam.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(IceSpikeImpact)
{
   filename    = "iceburst.wav";
   description = AudioClosest3d;
   preload = true;
};


//icespike
//--------------------


datablock ParticleData(IceSpikeParticle)
{
    dragCoefficient = 3;
    gravityCoefficient = 0.1;
    windCoefficient = 1;
    inheritedVelFactor = 0.2;
    constantAcceleration = 0;
    lifetimeMS = 2500;
    lifetimeVarianceMS = 56;
    useInvAlpha = 0;
    spinRandomMin = -51.6129;
    spinRandomMax = 205.645;
    textureName = "special/Ice2.png";
    times[0] = 0;
    times[1] = 0.177419;
    times[2] = 1;
    colors[0] = "0.149606 0.360000 0.560000 1.000000";
    colors[1] = "0.236220 0.360000 0.568000 0.588710";
    colors[2] = "0.157480 0.360000 1.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0.451613;
    sizes[2] = 1.18548;
};

datablock ParticleEmitterData(IceSpikeEmitter)
{
    ejectionPeriodMS = 2;
    periodVarianceMS = 0;
    ejectionVelocity = 2.74194;
    velocityVariance = 2;
    ejectionOffset =   0;
    thetaMin = 0;
    lifetimeMS = 120;
    thetaMax = 72.5806;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "IceSpikeParticle";
};


datablock ParticleData(IceSparks2Particle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.01;
    windCoefficient = 1;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1000;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "special/crescent3.png";
    times[0] = 0;
    times[1] = 0.508065;
    times[2] = 1;
    colors[0] = "0.500000 0.500000 1.000000 1.000000";
    colors[1] = "0.500000 0.500000 1.000000 0.500000";
    colors[2] = "0.500000 0.500000 1.000000 0.000000";
    sizes[0] = 0.395161;
    sizes[1] = 0.33871;
    sizes[2] = 0.508065;
};

datablock ParticleEmitterData(IceSparks2)
{
    ejectionPeriodMS = 2;
    periodVarianceMS = 0;
    ejectionVelocity = 4.55645;
    velocityVariance = 1;
    ejectionOffset =   0.806452;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 60.6796;
    orientParticles= 1;
    orientOnVelocity = 0;
    particles = "IceSparks2Particle";
};




datablock ExplosionData(IceSpikeExplosion)
{
   soundProfile   = IceSpikeImpact;

   emitter[0] = IceSpikeEmitter;
   emitter[1] = IceSparks2;

   faceViewer           = false;
};

datablock LinearProjectileData(IceSpike)
{
   projectileShapeName = "disc.dts";
   scale 	       = "0.1 1.0 0.2";
   emitterDelay        = -1;
   directDamage        = 0.1;
   hasDamageRadius     = false;
   indirectDamage      = 0.00;
   damageRadius        = 0.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $damageType::spell;
   kickBackStrength    = 0;

   sound               = IceProjectile;
   explosion           = "IceSpikeExplosion";
   underwaterExplosion = "IceSpikeExplosion";
   splash              = ChaingunSplash;

   dryVelocity       = 70;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 2.0;
   lightColor  = "0.175 0.175 0.9";
};
datablock LinearProjectileData(IceSpike2)
{
   projectileShapeName = "disc.dts";
   scale 	       = "0.1 1.0 0.2";
   emitterDelay        = -1;
   directDamage        = 0.1;
   hasDamageRadius     = false;
   indirectDamage      = 0.00;
   damageRadius        = 0.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $damageType::spell;
   kickBackStrength    = 0;

   sound               = IceProjectile;
   explosion           = "IceSpikeExplosion";
   underwaterExplosion = "IceSpikeExplosion";
   splash              = ChaingunSplash;

   dryVelocity       = 70;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 500;
   lifetimeMS        = 500;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 500;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 2.0;
   lightColor  = "0.175 0.175 0.9";
};

//datablock GrenadeProjectileData(IceSpike2)
//{
//  projectileShapeName = "disc.dts";
//    scale 	       = "0.1 1.0 0.2";
//    emitterDelay        = -1;
//    directDamage        = 0.1;
//    hasDamageRadius     = false;
//    indirectDamage      = 0.00;
//    damageRadius        = 0.0;
//    radiusDamageType    = $DamageType::Spell;
//    directDamageType    = $damageType::spell;
//    kickBackStrength    = 0;
//
//   bubbleEmitTime      = 1.0;
//
//   sound               = ChaingunProjectile;
//   explosion           = "ChaingunExplosion";
//   velInheritFactor    = 0.5;
//   splash              = ChaingunSplash;
//
//   explodeOnWaterImpact= true;
//   
//
//   grenadeElasticity = 0.1;
//   grenadeFriction   = 0.1;
//   armingDelayMS     = 250;
//   muzzleVelocity    = 47.00;
//   drag = 0.1;
//   lifetimeMS        = 5000;
//};




//melt

datablock ExplosionData(MeltExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "5.0 5.0 5.0";
   sizes[1] = "5.0 5.0 5.0";
   times[0] = 0.0;
   times[1] = 1.5;
};
datablock LinearFlareProjectileData(Melt)
{
   projectileShapeName = "plasmabolt.dts";
   scale               = "10.0 10.0 10.0";
   faceViewer          = true;
   directDamage        = 0.00;
   hasDamageRadius     = true;
   indirectDamage      = 0.04;
   damageRadius        = 15.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $DamageType::Spell;

   explosion           = "MeltExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 55.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;
   
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearFlareProjectileData(HellFireball)
{
   projectileShapeName = "plasmabolt.dts"; 
   scale               = "10.0 10.0 10.0";
   faceViewer          = true;
   directDamage        = 0.00;
   hasDamageRadius     = true;
   indirectDamage      = 0.04;
   damageRadius        = 15.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Spell;
   directDamageType    = $DamageType::Spell;

   explosion           = "MeltExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 30.0;
   wetVelocity       = 30;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;
   
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};
//dimension rift
//Credit goes to MostLikely for this bit.
datablock ParticleData(DHHoleParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 6000;
   lifetimeVarianceMS   = 0;
   spinSpeed = "0";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetpackFlare";
   UseInvAlpha = True;
   colors[0]     = "0.0 0.0 0.0 0.0";
   colors[1]     = "0.0 0.0 0.0 1.0";
   colors[2]     = "0.0 0.0 0.0 1.0";
   colors[3]     = "0.0 0.0 0.0 0.0";

   sizes[0]      = 800;
   sizes[1]      = 800;
   sizes[2]      = 800;
   sizes[3]      = 800;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};



datablock ParticleEmitterData(DHHoleEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.1;
   ejectionoffset = 10;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "DHHoleParticle";
   predietime = 6000;
};




datablock ShockwaveData(RiftShockwave)
{
   width = 5;
   numSegments = 20;
   numVertSegments = 4;
   velocity = 100;
   acceleration = -10.0; 
   lifetimeMS = 5200;
   height = 2.0;  
   verticalCurve = 0.5;

   mapToTerrain = false;
   renderBottom = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0]     = "1.0 0.7 0.5 1.0";
   colors[1]     = "1.0 0.5 0.2 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

};
datablock AudioProfile(RiftExplosionSound)
{
   filename    = "fx/vehicles/bomber_bomb_impact.wav";
   description = AudioBIGExplosion3d;
   preload = true;
   
};
datablock ExplosionData(RiftExplosion)
{
   shockwave = RiftShockwave;
   shockwaveOnTerrain = false;

   explosionShape = "effect_plasma_explosion.dts";

   playSpeed = 1.0;
   soundProfile   = RiftExplosionSound;
   faceViewer = true;

   sizes[0] = "10.0 10.0 10.0";

   shakeCamera = true;
   camShakeFreq = "6.0 7.0 7.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 1.0;
   camShakeRadius = 7.0;
};

datablock LinearProjectileData(Drift) {

	className = "LinearProjectileData";
	projectileShapeName = "turret_muzzlepoint.dts";
	baseEmitter = "DHHoleEmitter";
	
	directDamage        = 0.1;
	hasDamageRadius     = true;
	indirectDamage      = 0.1;
	damageRadius        = 75.0;
	radiusDamageType    = $DamageType::Spell;
	directDamageType    = $DamageType::spell;
	kickBackStrength    = -7500.0;
	delayEmitter = "DHHoleEmitter";
	hasLight = "1";
	lightRadius = "20";
	lightColor = "0.500000 0.500000 1.000000 1.000000";	
	dryVelocity = "12";
	wetVelocity = "12";
	fizzleTimeMS = "12000";
	lifetimeMS = "12000";
	explodeOnDeath = "1";
	
	explosion           = "RiftExplosion";
};

datablock SniperProjectileData(Beam)
{
   directDamage        = 0.4;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   velInheritFactor    = 1.0;
   sound 	       = SniperRifleProjectileSound;
   explosion           = "SniperExplosion";
   splash              = SniperSplash;
   directDamageType    = $DamageType::Spell;

   maxRifleRange       = 1000;
   rifleHeadMultiplier = 1.3;
   beamColor           = "1 0.1 0.1";
   fadeTime            = 10.0;

   startBeamWidth		  = 0.145;
   endBeamWidth 	     = 0.25;
   pulseBeamWidth 	  = 0.5;
   beamFlareAngle 	  = 3.0;
   minFlareSize        = 0.0;
   maxFlareSize        = 400.0;
   pulseSpeed          = 6.0;
   pulseLength         = 0.150;

   lightRadius         = 10.0;
   lightColor          = "0.3 0.0 0.0";

   textureName[0]      = "special/flare";
   textureName[1]      = "special/nonlingradient";
   textureName[2]      = "special/laserrip01";
   textureName[3]      = "special/laserrip02";
   textureName[4]      = "special/laserrip03";
   textureName[5]      = "special/laserrip04";
   textureName[6]      = "special/laserrip05";
   textureName[7]      = "special/laserrip06";
   textureName[8]      = "special/laserrip07";
   textureName[9]      = "special/laserrip08";
   textureName[10]     = "special/laserrip09";
   textureName[11]     = "special/sniper00";

};
//cloud

datablock ParticleData(CloudParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   spinSpeed = "0";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetpackFlare";
   UseInvAlpha = True;
   colors[0]     = "0.5 0.5 0.5 0.0";
   colors[1]     = "0.8 0.7 0.7 1.0";
   colors[2]     = "0.7 0.8 0.7 1.0";
   colors[3]     = "0.7 0.7 0.8 0.0";

   sizes[0]      = 10;
   sizes[1]      = 10;
   sizes[2]      = 10;
   sizes[3]      = 10;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};


datablock ParticleEmitterData(CloudEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.1;
   ejectionoffset = 2;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "CloudParticle";
   predietime = 500;
};

datablock LinearProjectileData(Cloud) {

	className = "LinearProjectileData";
	projectileShapeName = "turret_muzzlepoint.dts";
	baseEmitter = "CloudEmitter";
	
	directDamage        = 0.1;
	hasDamageRadius     = true;
	indirectDamage      = 0.1;
	faceviewer          = true;
	damageRadius        = 10.0;
	radiusDamageType    = $DamageType::Spell;
	directDamageType    = $DamageType::spell;
	
	delayEmitter = "CloudEmitter";
	
	dryVelocity = "35";
	wetVelocity = "35";
	fizzleTimeMS = "12000";
	lifetimeMS = "12000";
	explodeOnDeath = "1";
	
	explosion           = "PlasmaBoltExplosion";
};

datablock ParticleData(PCloudParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   spinSpeed = "0";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetpackFlare";
   UseInvAlpha = True;
   colors[0]     = "1.0 0.3 0.1 0.0";
   colors[1]     = "1.0 0.3 0.3 1.0";
   colors[2]     = "1.0 0.3 0.5 1.0";
   colors[3]     = "1.0 0.1 0.5 0.0";

   sizes[0]      = 10;
   sizes[1]      = 10;
   sizes[2]      = 10;
   sizes[3]      = 10;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};


datablock ParticleEmitterData(PCloudEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.1;
   ejectionoffset = 2;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "PCloudParticle";
   predietime = 500;
};

datablock LinearProjectileData(PowerCloud) {

	className = "LinearProjectileData";
	projectileShapeName = "turret_muzzlepoint.dts";
	baseEmitter = "PCloudEmitter";
	
	directDamage        = 0.1;
	hasDamageRadius     = true;
	indirectDamage      = 0.1;
	faceviewer          = true;
	damageRadius        = 10.0;
	radiusDamageType    = $DamageType::Spell;
	directDamageType    = $DamageType::spell;
	
	delayEmitter = "PCloudEmitter";
	
	dryVelocity = "40";
	wetVelocity = "40";
	fizzleTimeMS = "12000";
	lifetimeMS = "12000";
	explodeOnDeath = "1";
	
	explosion           = "PlasmaBoltExplosion";
};

//Defensive Magic

datablock ParticleData(HealAuraEmitter2Particle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0.053871;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = 0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 300;
    useInvAlpha = 0;
    spinRandomMin = -200;
    spinRandomMax = 0;
    textureName = "special/blasterBoltCross.PNG";
    times[0] = 0;
    times[1] = 0.01;
    times[2] = 1;
    colors[0] = "0.000000 0.256000 1.000000 1.000000";
    colors[1] = "0.000000 0.528000 1.000000 0.500000";
    colors[2] = "0.000000 0.784000 1.000000 0.000000";
    sizes[0] = 1;
    sizes[1] = 1;
    sizes[2] = 1.18548;
};

datablock ParticleEmitterData(HealAuraEmitter2)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 4.84677;
    velocityVariance = 4.70161;
    ejectionOffset =   0.645161;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";
    overrideAdvances = 0;
	   lifeTimeMS = 206.311;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "HealAuraEmitter2Particle";
};


datablock ParticleData(HealParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0.053871;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = 0;
    lifetimeMS = 100;
    lifetimeVarianceMS = 10;
    useInvAlpha = 0;
    spinRandomMin = -200;
    spinRandomMax = 0;
    textureName = "special/blasterBoltCross.PNG";
    times[0] = 0;
    times[1] = 0.01;
    times[2] = 1;
    colors[0] = "0.000000 0.256000 0.000000 1.000000";
    colors[1] = "0.000000 0.528000 0.000000 0.500000";
    colors[2] = "0.000000 0.784000 0.000000 0.000000";
    sizes[0] = 1;
    sizes[1] = 1;
    sizes[2] = 1.18548;
};

datablock ParticleEmitterData(HealEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 4.84677;
    velocityVariance = 4.70161;
    ejectionOffset =   0.645161;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";
    overrideAdvances = 0;
	   lifeTimeMS = 206.311;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "HealParticle";
};

//Neutral Magic

// #######################
// AUDIO DATABLOCK - FLARE
// #######################

datablock AudioProfile(FlareGrenadeBurnSound)
{
   filename = "Flame4.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(FlareGrenadeExplosionSound)
{
   filename = "Spell_Flame.wav";
   description = AudioClosest3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------
datablock ParticleData(FlareParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.15;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 1800;
   lifetimeVarianceMS   = 200;

   textureName          = "special/flareSpark";

   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};
datablock ParticleData(FlareParticleg)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.15;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 1800;
   lifetimeVarianceMS   = 200;

   textureName          = "special/flareSparkg";

   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};
datablock ParticleData(FlareParticleb)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.15;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 1800;
   lifetimeVarianceMS   = 200;

   textureName          = "special/flareSparkb";

   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};
datablock ParticleEmitterData(FlareEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "FlareParticle";
};

datablock ParticleEmitterData(FlareEmitterg)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "FlareParticleg";
};
datablock ParticleEmitterData(FlareEmitterb)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "FlareParticleb";
};
//--------------------------------------------------------------------------
// Explosion - Flare Grenade
//--------------------------------------
datablock ExplosionData(FlareGrenadeExplosion)
{
   explosionShape = "energy_explosion.dts";
   soundProfile   = FlareGrenadeExplosionSound;
   faceViewer           = true;
   explosionScale = "0.1 0.1 0.1";
};

//--------------------------------------------------------------------------
// Projectile - Flare Grenade
//--------------------------------------
datablock FlareProjectileData(Flare)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 10000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.5 0.1 0.1 0.1";
};
datablock FlareProjectileData(Flareg)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3g";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitterg;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 10000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.1 0.5 0.1 0.1";
};
datablock FlareProjectileData(Flareb)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3b";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitterb;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 10000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.1 0.1 0.5 0.1";
};
//--------------------------------------------------------------------------
// Projectile - Flare Grenade
//--------------------------------------
datablock FlareProjectileData(sFlare)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   //sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3";
   texture[1]          = "special/LensFlare/flare00";
   size                = 1.0;

   baseEmitter         = FlareEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 50;
   muzzleVelocity    = 10.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.5 0.1 0.1 0.1";
};
datablock FlareProjectileData(sFlareg)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   //sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3g";
   texture[1]          = "special/LensFlare/flare00";
   size                = 1.0;

   baseEmitter         = FlareEmitterg;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 50;
   muzzleVelocity    = 10.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.1 0.5 0.1 0.1";
};
datablock FlareProjectileData(sFlareb)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   //sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3b";
   texture[1]          = "special/LensFlare/flare00";
   size                = 1.0;

   baseEmitter         = FlareEmitterb;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 50;
   muzzleVelocity    = 10.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.1 0.1 0.5 0.1";
};
//Signal Flares
datablock ExplosionData(SignalFlareGrenadeExplosion)
{
   explosionShape = "energy_explosion.dts";
   soundProfile   = RiftExplosionSound;
   faceViewer           = true;
   explosionScale = "0.1 0.1 0.1";
};
datablock FlareProjectileData(SignalFlare)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = SignalFlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 25000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.5 0.1 0.1 0.1";
};
datablock FlareProjectileData(SignalFlareg)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = SignalFlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3g";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitterg;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 25000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.1 0.5 0.1 0.1";
};
datablock FlareProjectileData(SignalFlareb)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = SignalFlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3b";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitterb;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 25000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
 
   hasLight    = true;
   lightRadius = 19.0;
   lightColor  = "0.1 0.1 0.5 0.1";
};
function createEmitter(%pos, %emitter, %rot)
{
    %dummy = new ParticleEmissionDummy()
    {
          position = %pos;
          rotation = %rot;
          scale = "1 1 1";
          dataBlock = defaultEmissionDummy;
          emitter = %emitter;
          velocity = "1";
    };
    MissionCleanup.add(%dummy);
    %pos = getWords(%dummy.getTransform(), 0, 2);
    %trans = %pos@" "@%rot;
    %dummy.setTransform(%trans);

    if(isObject(%dummy))
       return %dummy;
}


function pos(%object)
{
	%trans = %object.getTransform();
	%pos = getword(%trans, 0) @ " " @ getword(%trans, 1) @ " " @ getword(%trans, 2);
	return %pos;
}
//darkhole("-524 1026 150", "",13501,"-524 1026 61",0);


//%p1 =CreateEmitter(-524 1026 150,DHDiskEmitter,"0 0 0");
//CreateEmitter("-524 1026 50",DHDustEmitter,"1 0 0 3.14");
//CreateEmitter("-510 911 50",DHHoleEmitter,"0 0 0");
//CreateEmitter("-510 911 50",ArcEmitter,"0 0 0");