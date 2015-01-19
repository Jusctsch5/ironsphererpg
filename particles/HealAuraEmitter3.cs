datablock ParticleData(HealAuraEmitter3Particle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0.053871;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = 0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
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

datablock ParticleEmitterData(HealAuraEmitter3)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 4.84677;
    velocityVariance = 1;
    ejectionOffset =   0.645161;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 157.767;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "HealAuraEmitter3Particle";
};
