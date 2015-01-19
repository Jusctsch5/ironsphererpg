datablock ParticleData(ShieldDomeParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1290;
    lifetimeVarianceMS = 40;
    useInvAlpha = 0;
    spinRandomMin = -79.0323;
    spinRandomMax = 175.403;
    textureName = "special/lightning2frame2.PNG";
    times[0] = 0;
    times[1] = 0.701613;
    times[2] = 1;
    colors[0] = "0.000000 0.000000 0.000000 0.000000";
    colors[1] = "0.000000 0.000000 1.000000 0.209677";
    colors[2] = "1.000000 1.000000 1.000000 0.000000";
    sizes[0] = 0;
    sizes[1] = 0;
    sizes[2] = 1.29839;
};

datablock ParticleEmitterData(ShieldDomeEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 0;
    ejectionOffset =   0.806452;
    thetaMin = 90;
    thetaMax = 0;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 631;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "ShieldDomeParticle";
};
