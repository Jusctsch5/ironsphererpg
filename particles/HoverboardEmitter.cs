datablock ParticleData(HoverboardParticle)
{
    dragCoefficient = 2.4878;
    gravityCoefficient = 0.1;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = -1.25806;
    lifetimeMS = 725;
    lifetimeVarianceMS = 48;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "dust10.png";
    times[0] = 0;
    times[1] = 0.330645;
    times[2] = 1;
    colors[0] = "0.000000 1.000000 0.000000 1.000000";
    colors[1] = "0.740157 0.744000 1.000000 0.500000";
    colors[2] = "0.700000 0.800000 1.000000 0.000000";
    sizes[0] = 0.790323;
    sizes[1] = 0.5;
    sizes[2] = 0.5;
};

datablock ParticleEmitterData(HoverboardEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 6;
    velocityVariance = 10;
    ejectionOffset =   1.29032;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 100;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "HoverboardParticle";
};