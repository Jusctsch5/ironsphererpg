datablock ParticleData(KeldrinFountianSplash2Particle)
{
    dragCoefficient = 1.26829;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1209;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -162.903;
    spinRandomMax = 90.7258;
    textureName = "special/cloudflash.png";
    times[0] = 0;
    times[1] = 0.129032;
    times[2] = 1;
    colors[0] = "0.700787 0.848000 1.000000 0.403226";
    colors[1] = "0.300000 0.300000 0.300000 0.403226";
    colors[2] = "0.000000 0.000000 0.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0.620968;
    sizes[2] = 1.69355;
};

datablock ParticleEmitterData(KeldrinFountianSplash2Emitter)
{
    ejectionPeriodMS = 3;
    periodVarianceMS = 0;
    ejectionVelocity = 2.8871;
    velocityVariance = 1.50806;
    ejectionOffset =   0.564516;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 1;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "KeldrinFountianSplash2Particle";
};
