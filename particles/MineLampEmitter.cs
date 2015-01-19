datablock ParticleData(MineLampParticle)
{
    dragCoefficient = 1.02439;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0.5;
    constantAcceleration = 0;
    lifetimeMS = 645;
    lifetimeVarianceMS = 40;
    useInvAlpha = 0;
    spinRandomMin = -79.0323;
    spinRandomMax = 175.403;
    textureName = "special/cloudflash2.png";
    times[0] = 0;
    times[1] = 0.25;
    times[2] = 1;
    colors[0] = "1.000000 0.624000 0.000000 0.250000";
    colors[1] = "1.000000 0.632000 0.000000 0.169355";
    colors[2] = "1.000000 1.000000 1.000000 0.411290";
    sizes[0] = 0.169355;
    sizes[1] = 0.33871;
    sizes[2] = 0;
};

datablock ParticleEmitterData(MineLampEmitter)
{
    ejectionPeriodMS = 23;
    periodVarianceMS = 1;
    ejectionVelocity = 1;
    velocityVariance = 0;
    ejectionOffset =   0;
    thetaMin = 15.2419;
    thetaMax = 10.1613;
    phiReferenceVel = 92.9032;
    phiVariance = 182.903;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "MineLampParticle";
};
