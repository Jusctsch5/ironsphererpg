datablock ParticleData(ThornParticle)
{
    dragCoefficient = 1;
    gravityCoefficient = 0.2;
    windCoefficient = 1;
    inheritedVelFactor = 0.2;
    constantAcceleration = -1.14516;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "special/crescent3.png";
    times[0] = 0;
    times[1] = 0.01;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 1.000000 0.000000";
    colors[1] = "1.000000 1.000000 0.500000 0.411290";
    colors[2] = "0.000000 0.000000 0.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0.169355;
    sizes[2] = 0;
};

datablock ParticleEmitterData(ThornEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2.30645;
    velocityVariance = 1;
    ejectionOffset =   0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 60;
    orientParticles= 1;
    orientOnVelocity = 1;
    particles = "ThornParticle";
};
