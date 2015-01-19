datablock ParticleData(KeldrinFountianSpoutParticle)
{
    dragCoefficient = 2.26829;
    gravityCoefficient = 0.1;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = -1.37097;
    lifetimeMS = 2500;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -200;
    spinRandomMax = 0;
    textureName = "special/cloudflash.png";
    times[0] = 0;
    times[1] = 0.209677;
    times[2] = 1;
    colors[0] = "0.700787 0.848000 1.000000 0.000000";
    colors[1] = "0.300000 0.300000 0.300000 0.048387";
    colors[2] = "0.000000 0.000000 0.000000 0.161290";
    sizes[0] = 0;
    sizes[1] = 0;
    sizes[2] = 3.3871;
};

datablock ParticleEmitterData(KeldrinFountianSpoutEmitter)
{
    ejectionPeriodMS = 13;
    periodVarianceMS = 0;
    ejectionVelocity = 3.6129;
    velocityVariance = 1;
    ejectionOffset =   0;
    thetaMin = 0;
    thetaMax = 13.0645;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "KeldrinFountianSpoutParticle";
};
