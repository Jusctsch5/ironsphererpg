datablock ParticleData(FlareDieParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 564;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -161.29;
    spinRandomMax = 635.081;
    textureName = "special/cloudflash2.png";
    times[0] = 0;
    times[1] = 0.459677;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 1.000000 1.000000";
    colors[1] = "1.000000 0.656000 0.216000 0.346774";
    colors[2] = "0.000000 0.000000 0.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0.33871;
    sizes[2] = 1.8629;
};

datablock ParticleEmitterData(FlareDie)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2.59677;
    velocityVariance = 1.43548;
    ejectionOffset =   0;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 266.99;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "FlareDieParticle";
};
