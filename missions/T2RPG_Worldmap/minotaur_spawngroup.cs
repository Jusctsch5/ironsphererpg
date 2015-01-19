// Spawn the reaper
createAIspawn("-1070 1183 180", "0 0 0", 40, 60, "25", 7, 0);

datablock ParticleData(EvilLampParticle)
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
    textureName = "special/blueimpact";
    times[0] = 0;
    times[1] = 0.25;
    times[2] = 1;
    colors[0] = "1.0 1.0 0.2 0.0";
    colors[1] = "1.0 1.0 0.2 0.5";
    colors[2] = "1.0 1.0 0.2 0.0";
    sizes[0] = 0.169355;
    sizes[1] = 0.33871;
    sizes[2] = 0;
};

datablock ParticleEmitterData(EvilLampEmitter)
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
    particles = "EvilLampParticle";
};

datablock ParticleData(EvilSpireParticle)
{

    dragCoefficient = 0;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 4032;
    lifetimeVarianceMS = 48;
    useInvAlpha = 0;
    spinRandomMin = -200;
    spinRandomMax = 750;
    textureName = "special/blueimpact";
    times[0] = 0;
    times[1] = 0.2;
    times[2] = 1;
    colors[0] = "1.0 1.0 0.2 0.0";
    colors[1] = "1.0 1.0 0.2 0.5";
    colors[2] = "1.0 1.0 0.2 0.0";
    sizes[0] = 0.564516;
    sizes[1] = 0.564516;
    sizes[2] = 0.395161;
};



datablock ParticleEmitterData(EvilSpireEmitter)
{
    ejectionPeriodMS = 75;
    periodVarianceMS = 0;
    ejectionVelocity = 1.79839;
    velocityVariance = 0;
    ejectionOffset =   4.51613;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 0;

    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "EvilSpireParticle";
};

//--- OBJECT WRITE BEGIN ---
new SimGroup(MinotaurSpawnStuff) {
	new InteriorInstance() {
		position = "-1111.23 1145.78 167.646";
		rotation = "0 0 -1 38.9611";
		scale = "1 1 1";
		interiorFile = "bspir5.dif";
		showTerrainInside = "0";
	};
	new InteriorInstance() {
		position = "-1129.14 1165.11 168.127";
		rotation = "0 0 1 138.656";
		scale = "1 1 1";
		interiorFile = "bspir5.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy() {
		position = "-1128.88 1165.22 202.145";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilSpireEmitter";
		velocity = "1";
	};
	new ParticleEmissionDummy() {
		position = "-1111.25 1145.95 201.41";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilSpireEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-845.433 1202.61 92.0958";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-845.433 1202.61 97.6958";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-842.793 1187.01 92.3081";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-842.793 1187.01 97.9081";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-1108.91 1096.51 143.686";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-1108.91 1096.51 149.286";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-1138.21 1075.47 150.617";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-1138.4 1157.57 178.189";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new InteriorInstance() {
		position = "-1123.95 1133.51 172.583";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-1123.95 1133.51 178.183";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-1158.86 1116.4 159.227";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-1158.86 1116.4 164.827";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
	new InteriorInstance() {
		position = "-1138.21 1075.47 145.017";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		interiorFile = "keldrin_lamp.dif";
		showTerrainInside = "0";
	};
	new ParticleEmissionDummy(MinotaurFlame) {
		position = "-1138.4 1157.57 183.789";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "EvilLampEmitter";
		velocity = "1";
	};
};
//--- OBJECT WRITE END ---
