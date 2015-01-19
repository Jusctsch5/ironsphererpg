//=========================================
// RPG Spells (rpgspells.cs)
// Work Began:  11/22/02
//=========================================

$spelldata[Bolt, Image] = EnergyBlast;
$spelldata[Bolt, DamageMod] = 3;
$spelldata[Bolt, NumEffect] = "";
$spelldata[Bolt, Test] = 1;
$spelldata[Bolt, Delay] = 500;
$spelldata[Bolt, RecoveryTime] = 100;
$spelldata[Bolt, Element] = "Energy";
$spelldata[Bolt, Type] = EnergyProjectile;
$spelldata[bolt, Cost] = 2;
$spelldata[Bolt, Function] = "DoSpellCast";
$spelldata[Bolt, Skill] = $Skill::OffensiveCasting;

$spellData[Thorn, Image] = Thorn;
$spelldata[Thorn, DamageMod] = 15;
$spelldata[Thorn, NumEffect] = "";
$spelldata[Thorn, Test] = 1;
$spelldata[Thorn, Delay] = 10;
$spelldata[Thorn, Element] = "Earth";
$spelldata[Thorn, RecoveryTime] = 1500;
$spelldata[Thorn, Type] = TracerProjectile;
$spelldata[Thorn, Cost] = 1;
$spelldata[Thorn, Function] = "DoSpellCast";
$spelldata[Thorn, Skill] = $Skill::OffensiveCasting;

$spellData[Spikes, Image] = Thorn;
$spelldata[Spikes, DamageMod] = 15;
$spelldata[Spikes, NumEffect] = "";
$spelldata[Spikes, Test] = 1;
$spelldata[Spikes, Element] = "Earth";
$spelldata[Spikes, Delay] = 100;
$spelldata[Spikes, RecoveryTime] = 4000;
$spelldata[Spikes, Type] = TracerProjectile;
$spelldata[Spikes, Cost] = 30;
$spelldata[Spikes, Function] = "SpellSpike";
$spelldata[Spikes, Skill] = $Skill::OffensiveCasting;

$spellData[Fireball, Image] = FireBall2;
$spelldata[Fireball, DamageMod] = 25;
$spelldata[Fireball, NumEffect] = "";
$spelldata[Fireball, Test] = 1;
$spelldata[Fireball, Element] = "Fire";
$spelldata[Fireball, Delay] = 1000;
$spelldata[Fireball, RecoveryTime] = 2000;
$spelldata[Fireball, Type] = LinearFlareProjectile;
$spelldata[Fireball, Cost] = 3;
$spelldata[Fireball, Function] = "DoSpellCast";
$spelldata[Fireball, Skill] = $Skill::OffensiveCasting;

$spellData[Firebomb, Image] = Firebomb;
$spelldata[Firebomb, DamageMod] = 60;
$spelldata[Firebomb, NumEffect] = "";
$spelldata[Firebomb, Test] = 1;
$spelldata[Firebomb, Element] = "Fire";
$spelldata[Firebomb, Delay] = 1500;
$spelldata[FireBomb, RecoveryTime] = 2625;
$spelldata[Firebomb, Type] = GrenadeProjectile;
$spelldata[Firebomb, Cost] = 4;
$spelldata[Firebomb, Function] = "DoSpellCast";
$spelldata[Firebomb, Skill] = $Skill::OffensiveCasting;

$spellData[IceSpike, Image] = IceSpike;
$spelldata[IceSpike, DamageMod] = 40;
$spelldata[IceSpike, NumEffect] = "";
$spelldata[IceSpike, Test] = 1;
$spelldata[IceSpike, Delay] = 10;
$spelldata[IceSpike, Element] = "Water";
$spelldata[IceSpike, RecoveryTime] = 1500;
$spelldata[IceSpike, Type] = LinearProjectile;
$spelldata[IceSpike, Cost] = 2;
$spelldata[IceSpike, Skill] = $Skill::OffensiveCasting;
$spelldata[IceSpike, Function] = "DoSpellCast";

$spelldata[IceStorm, Image] = IceSpike;
$spelldata[IceStorm, DamageMod] = 35;
$spelldata[IceStorm, NumEffect] = "";
$spelldata[IceStorm, Test] = 1;
$spelldata[IceStorm, Delay] = 100;
$spelldata[IceStorm, Element] = "Water";
$spelldata[IceStorm, RecoveryTime] = 1500;
$spelldata[IceStorm, Type] = LinearProjectile;
$spelldata[IceStorm, Cost] = 10;
$spelldata[IceStorm, Function] = "SpellIceStorm";
$spelldata[IceStorm, Skill] = $Skill::OffensiveCasting;

$spelldata[HellStorm, Image] = HellFireball;
$spelldata[HellStorm, DamageMod] = 30;//*20
$spelldata[HellStorm, NumEffect] = "";
$spelldata[HellStorm, Test] = 1;
$spelldata[HellStorm, Element] = "Fire";
$spelldata[HellStorm, Delay] = 6000;//6000
$spelldata[HellStorm, RecoveryTime] = 10500;//10500
$spelldata[HellStorm, Type] = LinearFlareProjectile;
$spelldata[HellStorm, Cost] = 20;//20
$spelldata[HellStorm, Function] = "LaunchSpellHellStorm";
$spelldata[HellStorm, Skill] = $Skill::OffensiveCasting;

$spellData[Melt, Image] = Melt;
$spelldata[Melt, DamageMod] = 100;
$spelldata[Melt, NumEffect] = "";
$spelldata[Melt, Test] = 1;
$spelldata[Melt, Element] = "Fire";
$spelldata[Melt, Delay] = 1500;
$spelldata[Melt, RecoveryTime] = 2625;
$spelldata[Melt, Type] = LinearFlareProjectile;
$spelldata[Melt, Cost] = 15;
$spelldata[Melt, Function] = "DoSpellCast";
$spelldata[Melt, Skill] = $Skill::OffensiveCasting;

$spellData[IronFist, Image] = IronFist;
$spelldata[IronFist, DamageMod] = 100;
$spelldata[IronFist, NumEffect] = "";
$spelldata[IronFist, Test] = 1;
$spelldata[IronFist, Element] = "Gravity";
$spelldata[IronFist, Delay] = 1500;//1500
$spelldata[IronFist, RecoveryTime] = 2625;//2625
$spelldata[IronFist, Type] = LinearFlareProjectile;
$spelldata[IronFist, Cost] = 15;//15
$spelldata[IronFist, Function] = "DoSpellCast";
$spelldata[IronFist, Skill] = $Skill::OffensiveCasting;

$spellData[Dimensionrift, Image] = Drift;
$spelldata[Dimensionrift, DamageMod] = 320;
$spelldata[Dimensionrift, NumEffect] = "";
$spelldata[Dimensionrift, Test] = 1;
$spelldata[Dimensionrift, Element] = "Gravity";
$spelldata[Dimensionrift, Delay] = 9500;
$spelldata[Dimensionrift, RecoveryTime] = 25250;
$spelldata[Dimensionrift, Type] = LinearProjectile;
$spelldata[Dimensionrift, Cost] = 40;
$spelldata[Dimensionrift, Function] = "Rift";
$spelldata[Dimensionrift, Skill] = $Skill::OffensiveCasting;

$spellData[Beam, Image] = Beam;
$spelldata[Beam, DamageMod] = 180;
$spelldata[Beam, NumEffect] = "";
$spelldata[Beam, Test] = 1;
$spelldata[Beam, Delay] = 1;
$spelldata[Beam, Element] = "Energy";
$spelldata[Beam, RecoveryTime] = 15000;
$spelldata[Beam, Type] = SniperProjectile;
$spelldata[Beam, Cost] = 30;
$spelldata[Beam, Function] = "DoBeamSpellCast";
$spelldata[Beam, Skill] = $Skill::OffensiveCasting;

$spelldata[Cloud, Image] = Cloud;
$spelldata[Cloud, DamageMod] = 85;
$spelldata[Cloud, NumEffect] = "";
$spelldata[Cloud, Test] = 1;
$spelldata[Cloud, Element] = "Wind";
$spelldata[Cloud, Delay] = 1500;
$spelldata[Cloud, RecoveryTime] = 2625;
$spelldata[Cloud, Type] = LinearProjectile;
$spelldata[Cloud, Cost] = 10;
$spelldata[Cloud, Function] = "DoSpellCast";
$spelldata[Cloud, Skill] = $Skill::OffensiveCasting;

$spelldata[PowerCloud, Image] = PowerCloud;
$spelldata[PowerCloud, DamageMod] = 170;
$spelldata[PowerCloud, NumEffect] = "";
$spelldata[PowerCloud, Test] = 1;
$spelldata[PowerCloud, Delay] = 1500;
$spelldata[PowerCloud, Element] = "Wind";
$spelldata[PowerCloud, RecoveryTime] = 7500;
$spelldata[PowerCloud, Type] = LinearProjectile;
$spelldata[PowerCloud, Cost] = 20;
$spelldata[PowerCloud, Function] = "DoSpellCast";
$spelldata[PowerCloud, Skill] = $Skill::OffensiveCasting;

$spelldata[Snowstorm, Image] = IceSpike2;
$spelldata[Snowstorm, DamageMod] = 40;
$spelldata[Snowstorm, NumEffect] = "";
$spelldata[Snowstorm, Test] = 1;
$spelldata[Snowstorm, Delay] = 100;
$spelldata[Snowstorm, Element] = "Water";
$spelldata[Snowstorm, RecoveryTime] = 4000;
$spelldata[Snowstorm, Type] = LinearProjectile;
$spelldata[Snowstorm, Cost] = 10;
$spelldata[Snowstorm, Function] = "SpellSnowStorm";
$spelldata[Snowstorm, Skill] = $Skill::OffensiveCasting;
//Defensive casting

$spelldata[Heal, Image] = HealEmitter;
$spelldata[Heal, DamageMod] = -6;
$spelldata[Heal, NumEffect] = "";
$spelldata[Heal, Test] = 1;
$spelldata[Heal, Delay] = 1500;
$spelldata[Heal, RecoveryTime] = 2250;
$spelldata[Heal, Type] = Emitter;
$spelldata[Heal, Cost] = 2;
$spelldata[Heal, MinSkill] = 120;
$spelldata[Heal, Function] = "DoDSpellCast";
$spelldata[Heal, Skill] = $Skill::DefensiveCasting;

$spelldata[StrongHeal, Image] = HealAuraEmitter2;
$spelldata[StrongHeal, DamageMod] = -12;
$spelldata[StrongHeal, NumEffect] = "";
$spelldata[StrongHeal, Test] = 1;
$spelldata[StrongHeal, Delay] = 1500;
$spelldata[StrongHeal, RecoveryTime] = 3250;
$spelldata[StrongHeal, Type] = Emitter;
$spelldata[StrongHeal, Cost] = 3;
$spelldata[StrongHeal, Function] = "DoDSpellCastLOS";
$spelldata[StrongHeal, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvHeal, Image] = HealAuraEmitter2;
$spelldata[AdvHeal, DamageMod] = -25;
$spelldata[AdvHeal, NumEffect] = "";
$spelldata[AdvHeal, Test] = 1;
$spelldata[AdvHeal, Delay] = 1500;
$spelldata[AdvHeal, RecoveryTime] = 4000;
$spelldata[AdvHeal, Type] = Emitter;
$spelldata[AdvHeal, Cost] = 4;
$spelldata[AdvHeal, Function] = "DoDSpellCastLOS";
$spelldata[AdvHeal, Skill] = $Skill::DefensiveCasting;

$spelldata[ExpertHeal, Image] = HealAuraEmitter2;
$spelldata[ExpertHeal, DamageMod] = -80;
$spelldata[ExpertHeal, NumEffect] = "";
$spelldata[ExpertHeal, Test] = 1;
$spelldata[ExpertHeal, Delay] = 1500;
$spelldata[ExpertHeal, RecoveryTime] = 4750;
$spelldata[ExpertHeal, Type] = Emitter;
$spelldata[ExpertHeal, Cost] = 5;
$spelldata[ExpertHeal, Function] = "DoDSpellCastLOS";
$spelldata[ExpertHeal, Skill] = $Skill::DefensiveCasting;

$spelldata[GodlyHeal, Image] = HealAuraEmitter2;
$spelldata[GodlyHeal, DamageMod] = -120;
$spelldata[GodlyHeal, NumEffect] = "";
$spelldata[GodlyHeal, Test] = 1;
$spelldata[GodlyHeal, Delay] = 1500;
$spelldata[GodlyHeal, RecoveryTime] = 6000;
$spelldata[GodlyHeal, Type] = Emitter;
$spelldata[GodlyHeal, Cost] = 15;
$spelldata[GodlyHeal, Function] = "DoDSpellCastLOS";
$spelldata[GodlyHeal, Skill] = $Skill::DefensiveCasting;

$spelldata[Shield, Image] = HealAuraEmitter2;
$spelldata[Shield, DamageMod] = "";
$spelldata[Shield, NumEffect] = "19 25";//protects 25 points of damage
$spelldata[Shield, Test] = 1;
$spelldata[Shield, Delay] = 1000;
$spelldata[Shield, Element] = "Generic";
$spelldata[Shield, RecoveryTime] = 2000;
$spelldata[Shield, Type] = Emitter;
$spelldata[Shield, cost] = 3;
$spelldata[Shield, Duration] = 30;
$spelldata[Shield, Special] = "GShield";
$spelldata[Shield, Function] = "DoShieldSpellCast";
$spelldata[Shield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvShield, Image] = HealAuraEmitter2;
$spelldata[AdvShield, DamageMod] = "";
$spelldata[AdvShield, NumEffect] = "19 100";//protects 100 points of damage
$spelldata[AdvShield, Test] = 1;
$spelldata[AdvShield, Delay] = 1500;
$spelldata[AdvShield, Element] = "Generic";
$spelldata[AdvShield, RecoveryTime] = 6000;
$spelldata[AdvShield, Type] = Emitter;
$spelldata[AdvShield, cost] = 10;
$spelldata[AdvShield, Duration] = 60;
$spelldata[AdvShield, Special] = "GLShield";
$spelldata[AdvShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvShield, Skill] = $Skill::DefensiveCasting;

$spelldata[FireShield, Image] = HealAuraEmitter2;
$spelldata[FireShield, DamageMod] = "";
$spelldata[FireShield, NumEffect] = "13 200";
$spelldata[FireShield, Test] = 1;
$spelldata[FireShield, Delay] = 1500;
$spelldata[FireShield, Element] = "Generic";
$spelldata[FireShield, RecoveryTime] = 4000;
$spelldata[FireShield, Type] = Emitter;
$spelldata[FireShield, cost] = 5;
$spelldata[FireShield, Duration] = 60;
$spelldata[FireShield, Special] = "GLShield";
$spelldata[FireShield, Function] = "DoShieldSpellCast";
$spelldata[FireShield, Skill] = $Skill::DefensiveCasting;

$spelldata[EarthShield, Image] = HealAuraEmitter2;
$spelldata[EarthShield, DamageMod] = "";
$spelldata[EarthShield, NumEffect] = "15 200";
$spelldata[EarthShield, Test] = 1;
$spelldata[EarthShield, Delay] = 1500;
$spelldata[EarthShield, Element] = "Generic";
$spelldata[EarthShield, RecoveryTime] = 4000;
$spelldata[EarthShield, Type] = Emitter;
$spelldata[EarthShield, cost] = 5;
$spelldata[EarthShield, Duration] = 60;
$spelldata[EarthShield, Special] = "GLShield";
$spelldata[EarthShield, Function] = "DoShieldSpellCast";
$spelldata[EarthShield, Skill] = $Skill::DefensiveCasting;

$spelldata[WaterShield, Image] = HealAuraEmitter2;
$spelldata[WaterShield, DamageMod] = "";
$spelldata[WaterShield, NumEffect] = "14 200";
$spelldata[WaterShield, Test] = 1;
$spelldata[WaterShield, Delay] = 1500;
$spelldata[WaterShield, Element] = "Generic";
$spelldata[WaterShield, RecoveryTime] = 4000;
$spelldata[WaterShield, Type] = Emitter;
$spelldata[WaterShield, cost] = 5;
$spelldata[WaterShield, Duration] = 60;
$spelldata[WaterShield, Special] = "GLShield";
$spelldata[WaterShield, Function] = "DoShieldSpellCast";
$spelldata[WaterShield, Skill] = $Skill::DefensiveCasting;

$spelldata[WindShield, Image] = HealAuraEmitter2;
$spelldata[WindShield, DamageMod] = "";
$spelldata[WindShield, NumEffect] = "18 200";
$spelldata[WindShield, Test] = 1;
$spelldata[WindShield, Delay] = 1500;
$spelldata[WindShield, Element] = "Generic";
$spelldata[WindShield, RecoveryTime] = 4000;
$spelldata[WindShield, Type] = Emitter;
$spelldata[WindShield, cost] = 5;
$spelldata[WindShield, Duration] = 60;
$spelldata[WindShield, Special] = "GLShield";
$spelldata[WindShield, Function] = "DoShieldSpellCast";
$spelldata[WindShield, Skill] = $Skill::DefensiveCasting;

$spelldata[EnergyShield, Image] = HealAuraEmitter2;
$spelldata[EnergyShield, DamageMod] = "";
$spelldata[EnergyShield, NumEffect] = "17 200";
$spelldata[EnergyShield, Test] = 1;
$spelldata[EnergyShield, Delay] = 1500;
$spelldata[EnergyShield, Element] = "Generic";
$spelldata[EnergyShield, RecoveryTime] = 4000;
$spelldata[EnergyShield, Type] = Emitter;
$spelldata[EnergyShield, cost] = 5;
$spelldata[EnergyShield, Duration] = 120;
$spelldata[EnergyShield, Special] = "GLShield";
$spelldata[EnergyShield, Function] = "DoShieldSpellCast";
$spelldata[EnergyShield, Skill] = $Skill::DefensiveCasting;

$spelldata[GravityShield, Image] = HealAuraEmitter2;
$spelldata[GravityShield, DamageMod] = "";
$spelldata[GravityShield, NumEffect] = "16 200";
$spelldata[GravityShield, Test] = 1;
$spelldata[GravityShield, Delay] = 1500;
$spelldata[GravityShield, Element] = "Generic";
$spelldata[GravityShield, RecoveryTime] = 4000;
$spelldata[GravityShield, Type] = Emitter;
$spelldata[GravityShield, cost] = 5;
$spelldata[GravityShield, Duration] = 120;
$spelldata[GravityShield, Special] = "GLShield";
$spelldata[GravityShield, Function] = "DoShieldSpellCast";
$spelldata[GravityShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvFireShield, Image] = HealAuraEmitter2;
$spelldata[AdvFireShield, DamageMod] = "";
$spelldata[AdvFireShield, NumEffect] = "13 500";
$spelldata[AdvFireShield, Test] = 1;
$spelldata[AdvFireShield, Delay] = 1500;
$spelldata[AdvFireShield, Element] = "Generic";
$spelldata[AdvFireShield, RecoveryTime] = 4000;
$spelldata[AdvFireShield, Type] = Emitter;
$spelldata[AdvFireShield, cost] = 15;
$spelldata[AdvFireShield, Duration] = 120;
$spelldata[AdvFireShield, Special] = "AGLShield";
$spelldata[AdvFireShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvFireShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvEarthShield, Image] = HealAuraEmitter2;
$spelldata[AdvEarthShield, DamageMod] = "";
$spelldata[AdvEarthShield, NumEffect] = "15 500";
$spelldata[AdvEarthShield, Test] = 1;
$spelldata[AdvEarthShield, Delay] = 1500;
$spelldata[AdvEarthShield, Element] = "Generic";
$spelldata[AdvEarthShield, RecoveryTime] = 4000;
$spelldata[AdvEarthShield, Type] = Emitter;
$spelldata[AdvEarthShield, cost] = 15;
$spelldata[AdvEarthShield, Duration] = 120;
$spelldata[AdvEarthShield, Special] = "AGLShield";
$spelldata[AdvEarthShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvEarthShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvWaterShield, Image] = HealAuraEmitter2;
$spelldata[AdvWaterShield, DamageMod] = "";
$spelldata[AdvWaterShield, NumEffect] = "14 500";
$spelldata[AdvWaterShield, Test] = 1;
$spelldata[AdvWaterShield, Delay] = 1500;
$spelldata[AdvWaterShield, Element] = "Generic";
$spelldata[AdvWaterShield, RecoveryTime] = 4000;
$spelldata[AdvWaterShield, Type] = Emitter;
$spelldata[AdvWaterShield, cost] = 15;
$spelldata[AdvWaterShield, Duration] = 120;
$spelldata[AdvWaterShield, Special] = "AGLShield";
$spelldata[AdvWaterShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvWaterShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvWindShield, Image] = HealAuraEmitter2;
$spelldata[AdvWindShield, DamageMod] = "";
$spelldata[AdvWindShield, NumEffect] = "18 500";
$spelldata[AdvWindShield, Test] = 1;
$spelldata[AdvWindShield, Delay] = 1500;
$spelldata[AdvWindShield, Element] = "Generic";
$spelldata[AdvWindShield, RecoveryTime] = 4000;
$spelldata[AdvWindShield, Type] = Emitter;
$spelldata[AdvWindShield, cost] = 15;
$spelldata[AdvWindShield, Duration] = 120;
$spelldata[AdvWindShield, Special] = "AGLShield";
$spelldata[AdvWindShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvWindShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvEnergyShield, Image] = HealAuraEmitter2;
$spelldata[AdvEnergyShield, DamageMod] = "";
$spelldata[AdvEnergyShield, NumEffect] = "17 500";
$spelldata[AdvEnergyShield, Test] = 1;
$spelldata[AdvEnergyShield, Delay] = 1500;
$spelldata[AdvEnergyShield, Element] = "Generic";
$spelldata[AdvEnergyShield, RecoveryTime] = 4000;
$spelldata[AdvEnergyShield, Type] = Emitter;
$spelldata[AdvEnergyShield, cost] = 15;
$spelldata[AdvEnergyShield, Duration] = 120;
$spelldata[AdvEnergyShield, Special] = "AGLShield";
$spelldata[AdvEnergyShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvEnergyShield, Skill] = $Skill::DefensiveCasting;

$spelldata[AdvGravityShield, Image] = HealAuraEmitter2;
$spelldata[AdvGravityShield, DamageMod] = "";
$spelldata[AdvGravityShield, NumEffect] = "16 500";
$spelldata[AdvGravityShield, Test] = 1;
$spelldata[AdvGravityShield, Delay] = 1500;
$spelldata[AdvGravityShield, Element] = "Generic";
$spelldata[AdvGravityShield, RecoveryTime] = 4000;
$spelldata[AdvGravityShield, Type] = Emitter;
$spelldata[AdvGravityShield, cost] = 15;
$spelldata[AdvGravityShield, Duration] = 120;
$spelldata[AdvGravityShield, Special] = "AGLShield";
$spelldata[AdvGravityShield, Function] = "DoShieldSpellCastLOS";
$spelldata[AdvGravityShield, Skill] = $Skill::DefensiveCasting;

$spelldata[GodlyShield, Image] = HealAuraEmitter2;
$spelldata[GodlyShield, DamageMod] = "";
$spelldata[GodlyShield, NumEffect] = "19 400";
$spelldata[GodlyShield, Test] = 1;
$spelldata[GodlyShield, Delay] = 1500;
$spelldata[GodlyShield, Element] = "Generic";
$spelldata[GodlyShield, RecoveryTime] = 4000;
$spelldata[GodlyShield, Type] = Emitter;
$spelldata[GodlyShield, cost] = 35;
$spelldata[GodlyShield, Duration] = 120;
$spelldata[GodlyShield, Special] = "GOShield";
$spelldata[GodlyShield, Function] = "DoShieldSpellCastLOS";
$spelldata[GodlyShield, Skill] = $Skill::DefensiveCasting;

$spelldata[HeavenlyShield, Image] = HealAuraEmitter2;
$spelldata[HeavenlyShield, DamageMod] = "";
$spelldata[HeavenlyShield, NumEffect] = "19 300 13 600 14 600 15 600 17 600 18 600 16 600";
$spelldata[HeavenlyShield, Test] = 1;
$spelldata[HeavenlyShield, Delay] = 5000;
$spelldata[HeavenlyShield, Element] = "Generic";
$spelldata[HeavenlyShield, RecoveryTime] = 15000;
$spelldata[HeavenlyShield, Type] = Emitter;
$spelldata[HeavenlyShield, cost] = 100;
$spelldata[HeavenlyShield, Duration] = 180;
$spelldata[HeavenlyShield, Special] = "HShield";
$spelldata[HeavenlyShield, Function] = "DoShieldSpellCastLOS";
$spelldata[HeavenlyShield, Skill] = $Skill::DefensiveCasting;
//Neutral Spells

$spelldata[Flare, Image] = Flare;
$spelldata[Flare, DamageMod] = 0;
$spelldata[Flare, NumEffect] = "";
$spelldata[Flare, Test] = 1;
$spelldata[Flare, Delay] = 500;
$spelldata[Flare, RecoveryTime] = 1500;
$spelldata[Flare, Type] = FlareProjectile;
$spelldata[Flare, Cost] = 1;
$spelldata[Flare, Function] = "DofSpellCast";
$spelldata[Flare, Skill] = $Skill::NeutralCasting;

$spelldata[SignalFlare, Image] = SignalFlare;
$spelldata[SignalFlare, DamageMod] = 0;
$spelldata[SignalFlare, NumEffect] = "";
$spelldata[SignalFlare, Test] = 1;
$spelldata[SignalFlare, Delay] = 500;
$spelldata[SignalFlare, RecoveryTime] = 10000;
$spelldata[SignalFlare, Type] = FlareProjectile;
$spelldata[SignalFlare, Cost] = 1;
$spelldata[SignalFlare, Function] = "DosfSpellCast";
$spelldata[SignalFlare, Skill] = $Skill::NeutralCasting;

$spelldata[AdminSignalFlare, Image] = SignalFlare;
$spelldata[AdminSignalFlare, DamageMod] = 0;
$spelldata[AdminSignalFlare, NumEffect] = "";
$spelldata[AdminSignalFlare, Test] = 1;
$spelldata[AdminSignalFlare, Delay] = 500;
$spelldata[AdminSignalFlare, RecoveryTime] = 1;
$spelldata[AdminSignalFlare, Type] = FlareProjectile;
$spelldata[AdminSignalFlare, Cost] = 0;
$spelldata[AdminSignalFlare, Function] = "DosfSpellCast";
$spelldata[AdminSignalFlare, Skill] = $Skill::NeutralCasting;


$spelldata[Teleport, Image] = "";
$spelldata[Teleport, DamageMod] = 0;
$spelldata[Teleport, NumEffect] = "";
$spelldata[Teleport, Test] = 1;
$spellData[Teleport, Delay] = 3500;
$spelldata[Teleport, RecoveryTime] = 16500;
$spelldata[Teleport, Cost] = 8;
$spelldata[Teleport, Function] = "DoSpellTeleport";
$spelldata[Teleport, Skill] = $Skill::NeutralCasting;

$spelldata[Transport, Image] = "";
$spelldata[Transport, DamageMod] = 0;
$spelldata[Transport, NumEffect] = "";
$spelldata[Transport, Test] = 1;
$spellData[Transport, Delay] = 4000;
$spelldata[Transport, RecoveryTime] = 23000;
$spelldata[Transport, Cost] = 12;
$spelldata[Transport, Function] = "DoSpellTransport";
$spelldata[Transport, Skill] = $Skill::NeutralCasting;

$spelldata[GuildTeleport, Image] = "";
$spelldata[GuildTeleport, DamageMod] = 0;
$spelldata[GuildTeleport, NumEffect] = "";
$spelldata[GuildTeleport, Test] = 1;
$spelldata[GuildTeleport, Delay] = 4500;
$spelldata[GuildTeleport, RecoveryTime] = 25000;
$spelldata[GuildTeleport, Cost] = 10;
$spelldata[GuildTeleport, Function] = "DoSpellGuildTeleport";
$spelldata[GuildTeleport, Skill] = $Skill::NeutralCasting;

$spelldata[AdvGuildTeleport, Image] = "";
$spelldata[AdvGuildTeleport, DamageMod] = 0;
$spelldata[AdvGuildTeleport, NumEffect] = "";
$spelldata[AdvGuildTeleport, Test] = 1;
$spelldata[AdvGuildTeleport, Delay] = 4500;
$spelldata[AdvGuildTeleport, RecoveryTime] = 25000;
$spelldata[AdvGuildTeleport, Cost] = 10;
$spelldata[AdvGuildTeleport, Function] = "DoSpellGuildTeleportLOS";
$spelldata[AdvGuildTeleport, Skill] = $Skill::NeutralCasting;

//Element to stat bonus declaration:
$Spell::ElementResistance[Fire] = 13;//Fire Resistance format x where x is a number.
$Spell::ElementResistance[Water] = 14;
$Spell::ElementResistance[Earth] = 15;
$Spell::ElementResistance[Gravity] = 16;
$Spell::ElementResistance[Energy] = 17;
$Spell::ElementResistance[Wind] = 18;
$Spell::ElementResistance[Generic] = 19;//all
$Spell::ElementDefense[Fire] = 20;// format XrX where X is a number
$Spell::ElementDefense[Water] = 21;
$Spell::ElementDefense[Earth] = 22;
$Spell::ElementDefense[Gravity] = 23;
$Spell::ElementDefense[Energy] = 24;
$Spell::ElementDefense[Wind] = 25;
$Spell::ElementDefense[Generic] = 3;

function NumericalSpellFX(%client, %effect, %value, %specialvar)
{
	//-------------------
	// Spell Effects
	//-------------------
	// You can use plain text
	// or numerical input to
	// specify which effect
	// you want to use.
	// 0 - Heal
	// 1 - Damage
	// 2 - AddMana
	// 3 - TakeMana
	// 4 - AddDEF
	// 5 - TakeDEF
	// 6 - AddMDEF
	// 7 - TakeMDEF
	// --
	// Special Variable
	// --
	// 0 - Trickle Time
	//--------------------

	// The way this is going to work is you will call the function
	// and it will perform the task during DoSpellCast.  You need
	// to specify a trickle time in the Special Variable place if
	// you want to drag the heal out.  This could make a heal spell
	// take three seconds to completely heal a person for example.
	// Ex:  Three Second 50pt Heal
	// NumericalSpellFX(%client, 0, 50, 3);
	// (or)
	// NumericalSpellFX(%client, "Heal", 50, 3);
	// Have fun!

	if(%effect $= "Heal" || %effect == 0)
		{
			%curhp = fetchData(%client, "HP");
			%healvalue = %value + %curhp;
			setHP(%client, %healvalue);//sethp can kill the person without calling the correct functions, use the  %Object.damage function instead. make sure you specify the damagetype as 8, for healing spells refreshhp and sethp are fine
		}
	if(%effect $= "Damage" || %effect == 1)
		{
			//%curhp = fetchData(%client, "HP");
			//%damagevalue = %curhp - %value;
			//setHP(%client, %damagevalue);
		}
	if(%effect $= "AddMana" || %effect == 2)
		{
			%curmp = fetchData(%client, "MANA");
			%restvalue = %curmp + %value;
			setMANA(%client, %restvalue);
		}
	if(%effect $= "TakeMana" || %effect == 3)
		{
			%curmp = fetchData(%client, "MANA");
			%actionvalue = %curmp - %value;
			setMANA(%client, %actionvalue);
		}
	if(%effect $= "AddDEF" || %effect == 4)
		{
			UpdateBonusState(%client, "6 " @ %value, %ticks);
		}
	if(%effect $= "TakeDEF" || %effect == 5)
		{
		}
	if(%effect $= "AddMDEF" || %effect == 6)
		{
			UpdateBonusState(%client, %type, %ticks);
		}
	if(%effect $= "TakeMDEF" || %effect == 7)
		{
		}

	
}

function BeginCastSpell(%client, %spell)
{
	%client.lastspellpos = %client.player.getPosition();
	storedata(%client, "SpellCastStep", 1);
	%res = 0;
	%params = getwords(%spell, 1, 9999);

	%spell = getword(%spell, 0);
	if ( $spelldata[%spell, Test] )
	{
		%nparams = %params;
		if(GetWord(%params, GetWordCount(%params)-1) $= "self")
		{
			%self = true;
			%nparams = GetWords(%params, 0, GetWordCount(%params)-2);
		}
		
		if(%spell $= "teleport")
		{
			if(!ValidateBasicDestination(%nparams))
			{
				MessageClient(%client, 'FailedCast', "Invalid Destination, please specify town or dungeon");
				storedata(%client, "spellcaststep", 0);
				return;
			}
		}
		else if(%spell $= "transport")
		{
			if(!ValidateDestination(%nparams))
			{
				MessageClient(%client, 'failedcast', "Invalid Destination.");
				storedata(%client, "spellcaststep", 0);
				return;
			}
		}
		else if(%spell $= "GuildTeleport" || %spell $= "AdvGuildTeleport")
		{
			if(!ValidateGuildDestination(%client, %nparams))
			{
				MessageClient(%client, 'failedcast', "Invalid Destination or your guild does not own that territory");
				storedata(%client, "spellcaststep", 0);
				return;
			}
		}
		%mod = 1;
		if(fetchdata(%client, "NextHitFocus"))
		{
			%mod = 1.25;
			storedata(%client, "NextHitFocus", false);
			//storedata(%client, "blockFocus", true);
			schedule(15000, %client, "unblockfocus", %client);
			%client.focus = true;
		}
		if(!%client.isAicontrolled())
		refreshMana(%client, $spelldata[%spell, Cost]);
		%client.spellcast = schedule($spelldata[%spell, Delay]*%mod,%client,$spelldata[%spell, Function],%client, $spelldata[%spell, Image], %spell, %params);
	
		schedule($spelldata[%spell, RecoveryTime]*%mod, %client, "spellrecover", %client);
		commandtoclient(%client, 'StartRecastDelayCountdown', $spelldata[%spell, RecoveryTime]*%mod);
	
	}
	else
	{
		messageClient(%client, 'RPGchatCallback', "You do not know such a spell.");
		storedata(%client, "SpellCastStep", 0);
	}
	return %res;
}
function spellrecover(%client)
{
	storedata(%client, "spellcaststep", 0);
	if(fetchdata(%client, "castingreadymessage"))
	MessageClient(%client, 'RPGchatCallback', "You are ready to cast");
}
function unblockfocus(%client)
{
	storedata(%client, "blockFocus", false);
	%client.focus = false;
}
function DoSpellCast(%client,%spell,%sdata, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);


	%p = new ($spelldata[%sdata, Type])() {
	  dataBlock        	= %spell;
	  initialDirection 	= %vector;
	  initialPosition  	= %pos;
	  sourceObject     	= %client.player;
	  sourceSlot       	= 1;
	  vehicleObject    	= 0;
	  spell			= %sdata;
	};
}

function DofSpellCast(%client,%spell,%sdata, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);
	%d = mfloor(getrandom()*3+1);

	switch (%d)
	{
		case 1: %spell = "flare";
		case 2: %spell = "flareb";
		case 3: %spell = "flareg";
	
	}
	switch$(%params)

	{
		case "blue": %spell = "flareb";
		case "red": %spell = "flare";
		case "green": %spell = "flareg";
	}
	%p = new ($spelldata[%sdata, Type])() {
	  dataBlock        	= %spell;
	  initialDirection 	= %vector;
	  initialPosition  	= %pos;
	  sourceObject     	= %client.player;
	  sourceSlot       	= 1;
	  vehicleObject    	= 0;
	  spell			= %sdata;
	};
}
function DosfSpellCast(%client,%spell,%sdata, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);
	%d = mfloor(getrandom()*4+1);
	echo(%params);
	%time = getword(%params, 1);
	if(%time <= 0 || %time > 20000) %time = 10000;

	%params = getword(%params, 0);
	switch (%d)
	{
		case 1: %spell = "signalflare";
			%flares= "sflare";
		case 2: %spell = "signalflareb";
			%flares= "sflareb";
		case 3: %spell = "signalflareg";
			%flares= "sflareg";

	
	}
	switch$(%params)

	{
		case "blue": %spell = "signalflareb";
				%flares = "sflareb";
		case "red": %spell = "signalflare";
				%flares = "sflare";
		case "green": %spell = "signalflareg";
				%flares = "sflareg";
		case "random":%k = mfloor(getrandom()*3+1);
			%flares = "random";
			switch(%k)
			{
			case 1: %spell = "signalflare";
			case 2: %spell = "signalflareb";
			case 3: %spell = "signalflareg";			
			}
		case "redblue":
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflare";
			case 2: %spell = "signalflareb";
			}
			%flares = "bluered";
		case "bluered":
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflare";
			case 2: %spell = "signalflareb";
			}
			%flares = %params;
		case "redgreen":
			%flares = "greenred";
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflareg";
			case 2: %spell = "signalflare";
			}
		case "greenred":
			%flares = %params;
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflareg";
			case 2: %spell = "signalflare";
			}
		case "bluegree":
			%flares = "greenblue";
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflareg";
			case 2: %spell = "signalflareb";
			}		
		case "greenblue":
			%flares = %params;
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %spell = "signalflareg";
			case 2: %spell = "signalflareb";
			}
			
		
	}
	  %svector = %vector;
      %vector = 0 SPC 0 SPC 1;
	%p = new ($spelldata[%sdata, Type])() {
	  dataBlock        	= %spell;
	  initialDirection 	= %vector;
	  initialPosition  	= %pos;
	  sourceObject     	= %client.player;
	  sourceSlot       	= 1;
	  vehicleObject    	= 0;
	  svector 		= %svector;
	  flares			= %flares;
	  spell			= %sdata;
	};
	schedule(%time, %p, "flareexplode",%p );
}
function flareexplode(%p)
{
      for(%i = 0; %i<25;%i++)
      {     
	schedule(%i*10, %p, "flareloop",%p);
	}
	schedule((%i+1)*10, %p, "flaredelete", %p);
}
function flareloop(%p)
{
SignalFlare::onExplode(%p.getDatablock(), %p, %p.getTransform());
}
function flaredelete(%p)
{
ServerPlay3D(RiftExplosionSound,%p.getTransform());

  %p.delete();
}
function SignalFlare::onExplode(%data, %proj, %pos, %mod)
{

	%client = %proj.sourceobject.client;
	%svector = %proj.svector;
	%flare = %proj.flares;
	switch$(%flare)
	{
		case "random":
		%k = mfloor(getrandom()*3+1);
		switch(%k)
		{
		case 1: %flare = "sflare";
		case 2: %flare = "sflareb";
		case 3: %flare = "sflareg";			
		}	
		case "bluered":
			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %flare = "sflare";
			case 2: %flare = "sflareb";
			}

		case "greenred":

			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %flare = "sflareg";
			case 2: %flare = "sflare";
			}
		case "greenblue":

			%k = mfloor(getrandom()*2+1);
			switch(%k)
			{
			case 1: %flare = "sflareg";
			case 2: %flare = "sflareb";
			}	
	}
	%sdata = "flare";
	%vector = %svector;

	%x = (getRandom() - 0.5) * 2 * 3.1415926 * 1000/1000;
	%y = (getRandom() - 0.5) * 2 * 3.1415926 * 1000/1000;
	%z = (getRandom() - 0.5) * 2 * 3.1415926 * 1000/1000;
	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	%vector2 = MatrixMulVector(%mat, %vector);

	%pos = Getword(%pos, 0) SPC getword(%pos, 1) SPC (getword(%pos, 2) + 0.5);

	%p = new ($spelldata[%sdata, Type])() {
		dataBlock        = %flare;
		initialDirection = %vector2;
		initialPosition  = %pos;
		sourceObject     = %client.player;
		sourceSlot       = 1;
		vehicleObject    = 0;
		spell 	  	  = %sdata;
	};
   
      
}
function SignalFlareg::onExplode(%data, %proj, %pos, %mod)
{
	SignalFlare::onExplode(%data, %proj, %pos, %mod);
}
function SignalFlareb::onExplode(%data, %proj, %pos, %mod)
{
	SignalFlare::onExplode(%data, %proj, %pos, %mod);
}
function DoBeamSpellCast(%client,%spell,%sdata, %params)
{
	//%vector = %client.player.getMuzzleVector(1);
	//%pos = %client.player.getMuzzlePoint(2);
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);

	%client.lastspell = %sdata;
	%p = new ($spelldata[%sdata, Type])() {
	  dataBlock        	= %spell;
	  initialDirection 	= %vector;
	  initialPosition  	= %pos;
	  sourceObject     	= %client.player;
	  sourceSlot       	= 1;
	  vehicleObject    	= 0;
	  spell			= %sdata;
	};
}
function DoDSpellCast(%client,%spell,%sdata, %params)
{

 	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);


      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);

      %z = %z + 0;
      %pos = %x SPC %y SPC %z;
     
      %em = createEmitter(%pos, $spelldata[%sdata, Image], "0 0 0");
      schedule(1000, 0, "removeExpo", %em);
      //refreshHP(%client, $spelldata[%sdata, damageMod] , true);
      %beforehp = fetchdata(%client, "HP");
       setHp(%client, fetchData(%client, "HP") - $spelldata[%sdata, damageMod], true);
      //self  
      if(%beforehp != fetchdata(%client,"HP"))
      UseSkill(%client, $skill::DefensiveCasting, true, true, 40, false);
      MessageClient(%client, 'Healingmessage', "You have healed yourself for" SPC $spelldata[%sdata, damageMod]*-1 SPC "health points");
}

function DoDSpellCastLOS(%client,%spell,%sdata, %params)
{
      %sr = getLOSinfo(%client, 50, $TypeMasks::PlayerObjectType);
   
      %tpos = getWords(%sr, 1, 3);
      if(%tpos $= "" || %params $= "self")
      {
      	%pos = %client.player.getMuzzlePoint(2);
      	%target = %client;
      }
      else
      {
      	%target = GetWord(%sr, 0);
      	%pos = %target.getMuzzlePoint(2);
      	%target = %target.client;
      }
   
      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);

      %z = %z + 1.6;
      %pos = %x SPC %y SPC %z;
     
      %em = createEmitter(%pos, %spell, "0 0 0");
      schedule(1000, 0, "removeExpo", %em);
      %beforehp = fetchdata(%target, "HP");
      setHp(%target, fetchData(%target, "HP") - $spelldata[%sdata, damageMod], true);
	if(%beforehp != fetchdata(%target,"HP"))
      UseSkill(%client, $skill::DefensiveCasting, true, true, 30, false);
      if(%target == %client)
      {
      MessageClient(%client, 'Healingmessage', "You have healed yourself for" SPC $spelldata[%sdata, damageMod]*-1 SPC "health points");
      }
      else
      {
      MessageClient(%target, 'Healingmessage', "You have been healed by" SPC %client.rpgname SPC "for" SPC $spelldata[%sdata, damageMod]*-1 SPC "health points");
      MessageClient(%client, 'Healingmessage', "You have healed" SPC  %target.rpgname SPC "for" SPC $spelldata[%sdata, damageMod]*-1 SPC "health points.");
      }
}
function SpellIceStorm(%client,%spell,%sdata, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);


      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);
	  //NumericalSpellFX(%client, 1, 30, 0);
      %z = %z + 1.6;
      //%pos = %x SPC %y SPC %z;
     
      %x = GetWord(%vector, 0);
      %y = GetWord(%vector, 1);
      %z = GetWord(%vector, 2);
      %z = %z - 0.05;
      %vector = %x SPC %y SPC %z;
      
      
      for(%i = 0; %i<5;%i++)
      {     
		%x = (getRandom() - 0.5) * 2 * 3.1415926 * 25/1000;
	      	%y = (getRandom() - 0.5) * 2 * 3.1415926 * 25/1000;
	      	%z = (getRandom() - 0.5) * 2 * 3.1415926 * 25/1000;
	      	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	      	%vector2 = MatrixMulVector(%mat, %vector);
	      	%p = new ($spelldata[%sdata, Type])() {
		 	dataBlock        = %spell;
		 	initialDirection = %vector2;
		 	initialPosition  = %pos;
		 	sourceObject     = %client.player;
		 	sourceSlot       = 1;
		 	vehicleObject    = 0;
		 	spell 	  	  = %sdata;
	      	};
      }
}

function SpellSpike(%client,%spell,%sdata, %params)
{
      for(%i = 0; %i<20;%i++)
      {   
      	schedule(%i*50, %client, "launchSpike", %client, %spell, %sdata, %params );
      }
}

function launchspike(%client, %spell, %sdata, %params)
{
%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);


      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);
	  //NumericalSpellFX(%client, 1, 30, 0);
      %z = %z + 1.6;
      //%pos = %x SPC %y SPC %z;
     
      %x = GetWord(%vector, 0);
      %y = GetWord(%vector, 1);
      %z = GetWord(%vector, 2);
      %z = %z - 0.05;
      %vector = %x SPC %y SPC %z;
      
	%x = (getRandom() - 0.5) * 2 * 3.1415926 * 30/1000;
	%y = (getRandom() - 0.5) * 2 * 3.1415926 * 30/1000;
	%z = (getRandom() - 0.5) * 2 * 3.1415926 * 30/1000;
	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	%vector2 = MatrixMulVector(%mat, %vector);
	%p = new ($spelldata[%sdata, Type])() {
		dataBlock        = %spell;
		initialDirection = %vector2;
		initialPosition  = %pos;
		sourceObject     = %client.player;
		sourceSlot       = 1;
		vehicleObject    = 0;
		spell 	  	  = %sdata;
	};
}
function SpellHellStorm(%client,%svector,%sdata, %pos, %params)
{
      %vector = %svector;
      getLOSinfo(%client, 5000);
      %lospos = $los::position;
      if(%lospos !$= "")
      %vector =  VectorNormalize( VectorSub( %lospos , %pos ) );
      for(%i = 0; %i<20;%i++)
      {     
		%x = (getRandom() - 0.5) * 2 * 3.1415926 * 50/1000;
	      	%y = (getRandom() - 0.5) * 2 * 3.1415926 * 50/1000;
	      	%z = (getRandom() - 0.5) * 2 * 3.1415926 * 50/1000;
	      	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	      	%vector2 = MatrixMulVector(%mat, %vector);

	      	%pos = Getword(%pos, 0) SPC getword(%pos, 1) SPC (getword(%pos, 2) - 0.5);

	      	%p = new (GrenadeProjectile)() {
		 	dataBlock        = FireBomb;
		 	initialDirection = %vector2;
		 	initialPosition  = %pos;
		 	sourceObject     = %client.player;
		 	sourceSlot       = 1;
		 	vehicleObject    = 0;
		 	spell 	  	  = %sdata;
	      	};
      }
}

function LaunchSpellHellStorm(%client,%spell,%sdata, %pos, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);
	//get the los position and we will get the vector from that
     
      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);
	  //NumericalSpellFX(%client, 1, 30, 0);
      %z = %z + 1.6+2;
      %pos = %x SPC %y SPC %z;
     
      %x = GetWord(%vector, 0);
      %y = GetWord(%vector, 1);
      %z = GetWord(%vector, 2);
      %z = %z - 0.15;

      %svector = %x SPC %y SPC %z;
      %vector = 0 SPC 0 SPC 1;
      %p = new ($spelldata[%sdata, Type])() {
         dataBlock        = %spell;
         initialDirection = %vector;
         initialPosition  = %pos;
         sourceObject     = %client.player;
         sourceSlot       = 1;
         vehicleObject    = 0;
         storevector	  = %svector;
    	 spell 	  	  = %sdata;
      };      
      

}
function SpellSnowStorm(%client,%spell,%sdata, %pos, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);
     
      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);
	  //NumericalSpellFX(%client, 1, 30, 0);
      %z = %z + 1.6+2;
      %pos = %x SPC %y SPC %z;
     
      %x = GetWord(%vector, 0);
      %y = GetWord(%vector, 1);
      %z = GetWord(%vector, 2);
      %z = %z - 0.15;

      %svector = %x SPC %y SPC %z;
      %vector = 0 SPC 0 SPC 1;
      %p = new ($spelldata[%sdata, Type])() {
         dataBlock        = %spell;
         initialDirection = %vector;
         initialPosition  = %pos;
         sourceObject     = %client.player;
         sourceSlot       = 1;
         vehicleObject    = 0;
         storevector	  = %svector;
    	 spell 	  	  = %sdata;
      };      
      

}
function FireSnowStorm(%client, %vector, %sdata, %pos)
{
	%pos = GetWord(%pos, 0) SPC GetWOrd(%pos, 1) SPC GetWord(%pos, 2)-1;
	InitContainerRadiusSearch(%pos, 150, $TypeMasks::PlayerObjectType);
 	%num = 0;
 	while ((%targetObject = containerSearchNext()) != 0)
   	{
   		 %dist = containerSearchCurrRadDamageDist();
   		
		if(%targetObject.istownbot)
		continue;
		if(%targetobject.client.rpgname $= "") continue;
		if(%targetobject.client == %client) continue;
		%num++;
   		%targetlist = %targetlist @ %targetobject @ " ";
   		%targetdist = %targetdist @ %dist @ " ";
   	}
   	//ok now to fire the snowstorm.
   	for(%i = 0; %i < 50 && %num > 0; %i++)
   	{
   		%target =  GetWord(%targetlist, GetRandom(0,%num-1));
   		schedule(200*%i,0, "launchSnowStorm", %client, %target, %sdata, %pos);
   	}
}
function launchSnowStorm(%client, %target, %sdata, %pos)
{
	%vector =  VectorNormalize( VectorSub( VectorAdd( %target.getPosition(), %target.getVelocity() ) , %pos ) );
	%x = (getRandom() - 0.5) * 0.2 * 3.1415926 * 30/1000;
	%y = (getRandom() - 0.5) * 0.2 * 3.1415926 * 30/1000;
	%z = (getRandom() - 0.5) * 0.2 * 3.1415926 * 30/1000;
	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%vector = MatrixMulVector(%mat, %vector);
      %p = new LinearProjectile() {
	 dataBlock        = IceSpike;
	 initialDirection = %vector;
	 initialPosition  = %pos;
	 sourceObject     = %client.player;
	 sourceSlot       = 1;
	 vehicleObject    = 0;
	 spell 	  	  = %sdata;
      }; 
}
function IceSpike2::onExplode(%data, %proj, %pos, %mod)
{
	FireSnowStorm(%proj.sourceobject.client, %proj.storevector, "SnowStorm", %pos);
}
function Rift(%client,%spell,%sdata, %params)
{
	%vector = %client.player.getEyeVector(); // lets try this
	%eyeTrans = %client.player.getEyeTransform();
	%pos = posFromTransform(%eyeTrans);

      %x = GetWord(%pos, 0);
      %y = GetWord(%pos, 1);
      %z = GetWord(%pos, 2);
	  //NumericalSpellFX(%client, 1, 30, 0);
      %z = %z + 1.6;
      %pos = %x SPC %y SPC %z;
     
      %x = GetWord(%vector, 0);
      %y = GetWord(%vector, 1);
      %z = GetWord(%vector, 2);
      %z = %z - 0.05;
      %vector = %x SPC %y SPC %z;
     
      %p = new ($spelldata[%sdata, Type])() {
         dataBlock        = %spell;
         initialDirection = %vector;
         initialPosition  = %pos;
         sourceObject     = %client.player;
         sourceSlot       = 1;
         vehicleObject    = 0;
    	 spell 	  	  = %sdata;
      };
}
function HellFireball::onExplode(%data, %proj, %pos, %mod)
{

	SpellHellStorm(%proj.sourceobject.client, %proj.storevector, "HellStorm", %pos, %svector);

}
function Drift::onExplode(%data, %proj, %pos, %mod)
{
	%p = CreateEmitter(%pos, DHHoleEmitter, "0 0 0");
	schedule(20000,0,"removeExpo",%p);
 if (%data.hasDamageRadius)
     RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
 InitContainerRadiusSearch(%pos, %data.damageRadius*2, $TypeMasks::PlayerObjectType);

   %numTargets = 0;
   %list = "";
   while ((%targetObject = containerSearchNext()) != 0)
   {
      %dist = containerSearchCurrRadDamageDist();
	if(%targetObject.istownbot)
		continue;
      if (%dist > %data.damageRadius*1.5)
         continue;
	for(%i = 1; %i <= 20; %i++)
		schedule(1000 * %i, %targetObject, "periodicPull", %targetObject, %pos, %data.kickBackStrength, (21-%i)/20);
    

   }

//schedule(10000, 0, "radiusExplosion", %proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType); 

}
function periodicPull(%targetObject, %pos, %impulse, %cof)
{
	
	 %data = %targetObject.getDataBlock();
	 if(%data.shouldApplyImpulse(%targetObject))
	 {
	// echo("hi!");
		 %p = %targetObject.getWorldBoxCenter();
		 %momVec = VectorSub(%p, %pos);
		 %momVec = VectorNormalize(%momVec);
		 %impulseVec = VectorScale(%momVec, -7500 * (%cof));
		%targetObject.applyImpulse(%pos, %impulseVec);
 	}
}
function removeExpo(%obj)
{
	%obj.delete();
}

function DoSpellTeleport(%client, %spell, %sdata, %params)
{
	if(inarena(%client))
	return;
	%ctrans = %client.player.getTransform();
	%cxy = getwords(%ctrans, 0, 1);
	if(%params !$= "dungeon")
	%closest = towns.getObject(0);
	else
	%closest = dungeons.getObject(0);
	%mxy = getwords(%closest.getTransform(), 0 , 1);

	%cdist = mpow(mpow(getword(%mxy, 0)-getword(%cxy, 0), 2) + mpow(getword(%mxy, 1) - getword(%cxy, 1), 2),0.5);


	//get closest town
	if(%params !$= "dungeon")
	{
		for(%i = 1; %i < towns.GetCount(); %i++)
		{

			//only need to examine X and Y cords to see what is closest
			%obj = towns.getObject(%i);
			%mxy = getwords(%obj.getTransform(), 0 , 1);

			%mdist = mpow(mpow(getword(%mxy, 0)-getword(%cxy, 0), 2) + mpow(getword(%mxy, 1) - getword(%cxy, 1), 2), 0.5);

			if(%mdist < %cdist)
			{
			%cdist = %mdist;
			%closest = %obj;
			}
		}
	}
	else
	{
		for(%i = 1; %i < dungeons.GetCount(); %i++)
		{

			//only need to examine X and Y cords to see what is closest
			%obj = dungeons.getObject(%i);
			%mxy = getwords(%obj.getTransform(), 0 , 1);

			%mdist = mpow(mpow(getword(%mxy, 0)-getword(%cxy, 0), 2) + mpow(getword(%mxy, 1) - getword(%cxy, 1), 2), 0.5);

			if(%mdist < %cdist)
			{
			%cdist = %mdist;
			%closest = %obj;
			}
		}	
	}
    //
    %posRnd = getRandomPosition(10, 0);
    %posT = vectorAdd(%posRnd, %closest.getTransform());
    %zLyr = getTerrainHeight(%posT);
    %final = getWords(%posT, 0, 1) SPC %zLyr;
    //
	%client.player.setPosition(%final);
	UseSkill(%client, $skill::NeutralCasting, true, true, 10, false);
	
}

function DoSpellTransport(%client, %spell, %sdata, %params)
{
	if(inArena(%client))
	return;
	%tele = 0;

	for(%i = 0; %i < towns.GetCount(); %i++)
	{
		%obj = towns.getObject(%i);

		if(%obj.transname $=  %params)
		%tele = %obj;
	}
	for(%i = 0; %i < dungeons.GetCount(); %i++)
	{
		%obj = dungeons.getObject(%i);
		if(%obj.transname $=  %params)
		%tele = %obj;
	}
	if(isobject(customtele)) //for custom maps that want teleports where the teleport spell cannot go. 
	for(%i = 0; %i < customtele.GetCount(); %i++)
	{
		%obj = customtele.getObject(%i);
		if(%obj.transname $=  %params)
		%tele = %obj;
	}
	if(%tele != 0)
	{
        //
        %posRnd = getRandomPosition(10, 0);
        %posT = vectorAdd(%posRnd, %tele.getTransform());
        %zLyr = getTerrainHeight(%posT);
        %final = getWords(%posT, 0, 1) SPC %zLyr;
        //
		%client.player.setPosition(%final);
		UseSkill(%client, $skill::NeutralCasting, true, true, 2, false);
	}
	else
	{
		MessageClient(%client, 'Teleport Fail', "No such destination");
	}
}

function DoShieldSpellCast(%client,%spell,%sdata, %params)
{
	
      %pos = %client.player.getPosition();
      %em = createEmitter(%pos, $spelldata[%sdata, Image], "0 0 0");
      schedule(1000, 0, "removeExpo", %em);
      AddBonusState(%client, $spelldata[%sdata, numEffect], $spelldata[%sdata, Duration], $spelldata[%sdata, Special]);
}

function DoShieldSpellCastLOS(%client,%spell,%sdata, %params)
{
	
	%sr = getLOSinfo(%client, 50, $TypeMasks::PlayerObjectType);
   
      %tpos = getWords(%sr, 1, 3);
      if(%tpos $= "" || %params $= "self")
      {
      	%pos = %client.player.getMuzzlePoint(2);
      	%target = %client;
      }
      else
      {
      	%target = GetWord(%sr, 0);
      	%pos = %target.getMuzzlePoint(2);
      	%target = %target.client;
      }
      //%pos = %client.player.getPosition();
      %em = createEmitter(%pos, $spelldata[%sdata, Image], "0 0 0");
      schedule(1000, 0, "removeExpo", %em);
      AddBonusState(%target, $spelldata[%sdata, numEffect], $spelldata[%sdata, Duration], $spelldata[%sdata, Special]);
}

function DoSpellGuildTeleport(%client, %spell, %sdata, %params)
{
	%tele = ValidateGuildDestination(%client, %params);
	if(%tele != false)
	{
        //
        %posRnd = getRandomPosition(10, 0);
        %posT = vectorAdd(%posRnd, %tele.getTransform());
        %zLyr = getTerrainHeight(%posT);
        %final = getWords(%posT, 0, 1) SPC %zLyr;
        //
		%client.player.setPosition(%final);
		UseSkill(%client, $skill::NeutralCasting, true, true, 2, false);
	}
	else
	{
		MessageClient(%client, 'Teleport Fail', "No such destination");
	}	

}

function DoSpellGuildTeleportLOS(%client, %spell, %sdata, %params)
{

	if(GetWord(%params, GetWordCount(%params)-1) $= "self")
	{

		%self = true;
		%params = GetWords(%params, 0, GetWordCount(%params)-2);
		echo(%params);
	}
	%tele = ValidateGuildDestination(%client, %params);
	%sr = getLOSinfo(%client, 50, $TypeMasks::PlayerObjectType);
   
      %tpos = getWords(%sr, 1, 3);
      if(%tpos $= "" || %self)
      {
      	%pos = %client.player.getMuzzlePoint(2);
      	%target = %client;
      }
      else
      {
      	%target = GetWord(%sr, 0);
      	%pos = %target.getMuzzlePoint(2);
      	%target = %target.client;
      }	
      if(%tele != false)
      %tele = ValidateGuildDestination(%target, %params);//guild members ONLY!!
	if(%tele != false)
	{
        //
        %posRnd = getRandomPosition(10, 0);
        %posT = vectorAdd(%posRnd, %tele.getTransform());
        %zLyr = getTerrainHeight(%posT);
        %final = getWords(%posT, 0, 1) SPC %zLyr;
        //
		%target.player.setPosition(%final);
		UseSkill(%client, $skill::NeutralCasting, true, true, 2, false);
	}
	else
	{
		MessageClient(%client, 'Teleport Fail', "No such destination, or your guild does not own that territory.");
	}	

}
