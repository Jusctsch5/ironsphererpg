datablock ParticleData(KeldrinFountianSplashParticle)
{
    dragCoefficient = 1.82927;
    gravityCoefficient = 0.1;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1774;
    lifetimeVarianceMS = 120;
    useInvAlpha = 0;
    spinRandomMin = -162.903;
    spinRandomMax = 90.7258;
    textureName = "special/bubbles.PNG";
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
    colors[0] = "0.212598 0.632000 1.000000 0.403226";
    colors[1] = "0.300000 0.300000 0.300000 1.000000";
    colors[2] = "0.000000 0.000000 0.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0.451613;
    sizes[2] = 0.903226;
};

datablock ParticleEmitterData(KeldrinFountianSplashEmitter)
{
    ejectionPeriodMS = 5;
    periodVarianceMS = 5;
    ejectionVelocity = 2.8871;
    velocityVariance = 1.50806;
    ejectionOffset =   0.403226;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "KeldrinFountianSplashParticle";
};
