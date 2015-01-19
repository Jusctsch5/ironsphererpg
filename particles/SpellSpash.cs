datablock ParticleData(SpellSpashParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0.2;
    windCoefficient = 1;
    inheritedVelFactor = 0.2;
    constantAcceleration = -0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -93.5484;
    spinRandomMax = 447.581;
    textureName = "special/droplet.PNG";
    times[0] = 0;
    times[1] = 0.01;
    times[2] = 1;
    colors[0] = "0.700000 0.800000 1.000000 0.774194";
    colors[1] = "0.700000 0.800000 1.000000 0.379032";
    colors[2] = "0.700000 0.800000 1.000000 0.000000";
    sizes[0] = 0.225806;
    sizes[1] = 0.3;
    sizes[2] = 0.620968;
};

datablock ParticleEmitterData(SpellSpash)
{
    ejectionPeriodMS = 3;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    velocityVariance = 1;
    ejectionOffset =   0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 121.359;
    orientParticles= 1;
    orientOnVelocity = 0;
    particles = "SpellSpashParticle";
};
