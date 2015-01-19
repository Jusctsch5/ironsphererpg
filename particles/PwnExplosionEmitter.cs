datablock ParticleData(PwnExplosionParticle)
{
    dragCoefficient = 1.46341;
    gravityCoefficient = 0.2;
    windCoefficient = 1;
    inheritedVelFactor = 0.2;
    constantAcceleration = -0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "special/flare.PNG";
    times[0] = 0;
    times[1] = 0.556452;
    times[2] = 1;
    colors[0] = "0.000000 0.000000 1.000000 0.774194";
    colors[1] = "0.622047 0.632000 1.000000 0.524194";
    colors[2] = "1.000000 1.000000 1.000000 0.000000";
    sizes[0] = 1.41129;
    sizes[1] = 0.3;
    sizes[2] = 0;
};

datablock ParticleEmitterData(PwnExplosionEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    velocityVariance = 1;
    ejectionOffset =   0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 72.8155;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "PwnExplosionParticle";
};
