//######################################################################################
// Skills
//######################################################################################

$SkillSlashing = 1;
$SkillPiercing = 2;
$SkillBludgeoning = 3;
$SkillArchery = 4;
$SkillOffensiveCasting = 5;
$SkillDefensiveCasting = 6;
$SkillNeutralCasting = 7;
$SkillWeightCapacity = 8;
$SkillEndurance = 9;
$SkillStealing = 10;
$SkillHiding = 11;

$SkillBashing = 12;
$SkillCleaving = 13;
$SkillBackstabbing = 14;
$SkillIgniteArrow = 15;
$SkillFocus = 16;

$SkillHealing = 17;
$SkillSenseHeading = 18;
$SkillMining = 19;
$SkillSpeech = 20;
$SkillHaggling = 21;
$SkillDodgeing = 22;

$Skill::Slashing = 1;
$Skill::Piercing = 2;
$Skill::Bludgeoning = 3;
$Skill::Archery = 4;
$Skill::OffensiveCasting = 5;
$Skill::DefensiveCasting = 6;
$Skill::NeutralCasting = 7;
$Skill::WeightCapacity = 8;
$Skill::Endurance = 9;
$Skill::Stealing = 10;
$Skill::Hiding = 11;

$Skill::Bashing = 12;
$Skill::Cleaving = 13;
$Skill::Backstabbing = 14;
$Skill::IgniteArrow = 15;
$Skill::Focus = 16;

$Skill::Healing = 17;
$Skill::SenseHeading = 18;
$Skill::Mining = 19;
$Skill::Speech = 20;
$Skill::Haggling = 21;
$Skill::Dodgeing = 22;

$MinLevel = "L";
$MinGroup = "G";
$MinClass = "C";
$MinAdmin = "A";
$MinHouse = "H";
$Magic = "M";


$SkillDesc[$Skill::Slashing] = "Slashing";
$SkillDesc[$Skill::Piercing] = "Piercing";
$SkillDesc[$Skill::Bludgeoning] = "Bludgeoning";
$SkillDesc[$Skill::Archery] = "Archery";
$SkillDesc[$Skill::OffensiveCasting] = "Offensive Casting";
$SkillDesc[$Skill::DefensiveCasting] = "Defensive Casting";
$SkillDesc[$Skill::NeutralCasting] = "Neutral Casting";
$SkillDesc[$Skill::WeightCapacity] = "Weight Capacity";
$SkillDesc[$Skill::Endurance] = "Endurance";
$SkillDesc[$Skill::Stealing] = "Stealing";
$SkillDesc[$Skill::Hiding] = "Hiding";
$SkillDesc[$Skill::Bashing] = "Bashing";
$SkillDesc[$Skill::Cleaving] = "Cleaving";
$SkillDesc[$Skill::Backstabbing] = "Backstabbing";
$SkillDesc[$Skill::IgniteArrow] = "IgniteArrow";
$SkillDesc[$Skill::Focus] = "Focus";
$SkillDesc[$Skill::Healing] = "Healing";
$SkillDesc[$Skill::SenseHeading] = "Sense Heading";
$SkillDesc[$Skill::Mining] = "Mining";
$SkillDesc[$Skill::Speech] = "Speech";
$SkillDesc[$Skill::Haggling] = "Haggling";
$SkillDesc[$MinLevel] = "Level";
$SkillDesc[$MinGroup] = "Group";
$SkillDesc[$MinClass] = "Class";
$SkillDesc[$MinAdmin] = "Admin Level";
$SkillDesc[$MinHouse] = "House";
$SkillDesc[$Magic] = "Magic Skill";

$SkillData[$Skill::Slashing] = "This skill is used with Slashing weapons.";
$SkillData[$Skill::Piercing] = "This skill is used with Piercing weapons.";
$SkillData[$Skill::Bludgeoning] = "This skill is used with Bludgeoning weapons";
$SkillData[$Skill::Archery] = "This skill is used with Projectile weapons.";
$SkillData[$Skill::OffensiveCasting] = "This skill is used for Offensive magic.";
$SkillData[$Skill::DefensiveCasting] = "This skill is used for Defensive magic";
$SkillData[$Skill::NeutralCasting] = "This skill is used for Neutral indirect magic.";
$SkillData[$Skill::WeightCapacity] = "This skill determines how much weight your character can hold.";
$SkillData[$Skill::Endurance] = "This skill affects your players HP and armor class";
$SkillData[$Skill::Stealing] = "This skill shows how effectivly you can use the dark art of thievery.";
$SkillData[$Skill::Hiding] = "This skill shows how well you can evade the light.";
$SkillData[$Skill::Bashing] = "This skill is used with Bludgeoning weapons and can send people flying.";
$SkillData[$Skill::Cleaving] = "This skill is used with Slashing weapons!";
$SkillData[$Skill::Backstabbing] = "This skill is used when you are hiding and you hit someone in the back with a piercing weapon.";
$SkillData[$Skill::IgniteArrow] = "This is used with your archery skill and increases the power of your arrows.";
$SkillData[$Skill::Focus] = "This is used with your magic and increases the power of your spells.";
$SkillData[$Skill::Healing] = "This affects the rate you heal over time.";
$SkillData[$Skill::SenseHeading] = "This skill covers all your general tracking and hunting skills.";
$SkillData[$Skill::Mining] = "This skill affects how well you can identify gems and minerals.";
$SkillData[$Skill::Speech] = "This skill shows how well you can organize thoughts into words.";
$SkillData[$Skill::Haggling] = "This skill is used to barter a better price with local merchants.";

$CategorySlashing = 1;
$CategoryPiercing = 2;
$CategoryBludgeoning = 3;
$CategoryArchery = 4;
$CategorySpells = 5;
$CategoryThieving = 6;
$CategoryStrength = 7;
$CategoryIntelligence = 8;

$CategoryDesc[$CategorySlashing] = "Slashing";
$CategoryDesc[$CategoryPiercing] = "Piercing";
$CategoryDesc[$CategoryBludgeoning] = "Bludgeoning";
$CategoryDesc[$CategoryArchery] = "Archery";
$CategoryDesc[$CategorySpells] = "Spell Casting";
$CategoryDesc[$CategoryThieving] = "Thieving";
$CategoryDesc[$CategoryStrength] = "Strength";
$CategoryDesc[$CategoryIntelligence] = "Intelligence";

$CategoryID[$Skill::Slashing] = $CategorySlashing;
$CategoryID[$Skill::Cleaving] = $CategorySlashing;
$CategoryID[$Skill::Piercing] = $CategoryPiercing;
$CategoryID[$Skill::Backstabbing] = $CategoryPiercing;
$CategoryID[$Skill::Bludgeoning] = $CategoryBludgeoning;
$CategoryID[$Skill::Bashing] = $CategoryBludgeoning;
$CategoryID[$Skill::Archery] = $CategoryArchery;
$CategoryID[$Skill::IgniteArrow] = $CategoryArchery;
$CategoryID[$Skill::OffensiveCasting] = $CategorySpells;
$CategoryID[$Skill::DefensiveCasting] = $CategorySpells;
$CategoryID[$Skill::NeutralCasting] = $CategorySpells;
$CategoryID[$Skill::Focus] = $CategorySpells;
$CategoryID[$Skill::WeightCapacity] = $CategoryStrength;
$CategoryID[$Skill::Endurance] = $CategoryStrength;
$CategoryID[$Skill::Mining] = $CategoryStrength;
$CategoryID[$Skill::Stealing] = $CategoryThieving;
$CategoryID[$Skill::Hiding] = $CategoryThieving;
$CategoryID[$Skill::SenseHeading] = $CategoryIntelligence;
$CategoryID[$Skill::Speech] = $CategoryIntelligence;
$CategoryID[$Skill::Haggling] = $CategoryIntelligence;

//######################################################################################
// Class multipliers
//######################################################################################

//***********************************
// GENERAL RULES FOR MULTIPLIERS:
//***********************************
//- Maximum multiplier should be 2.0
//- Minimum multiplier should be 0.1
//- A 0.1 should be VERY rare.  The normal minimum is 0.2.  If a class should not even
//  be near a certain skill, that's when the 0.1 comes in.

//******** SUMMARY ******************
//- Primary skills use a 2.0 multiplier
//- Secondary skills use a 1.5 multiplier
//- Normal skills use a ~1.0 multiplier
//- Weak skills use a ~0.5 multiplier
//- VERY weak skills use a 0.2
//- Unsuitable skills for a specific class use a 0.1

//--------------
// Cleric
//--------------
// Clerics are good with Bludgeoning weapons but VERY good at healing spells.  They also
// know the basics behind offensive spells.

//Primary Skill: Defensive Casting
//Secondary Skills: Healing, Energy, Bludgeoning

$SkillMultiplier[Cleric, $Skill::Slashing] = 0.6;
$SkillMultiplier[Cleric, $Skill::Piercing] = 0.7;
$SkillMultiplier[Cleric, $Skill::Bludgeoning] = 1.5;
$SkillMultiplier[Cleric, $Skill::Dodging] = 0.7;
$SkillMultiplier[Cleric, $Skill::WeightCapacity] = 1.0;
$SkillMultiplier[Cleric, $Skill::Bashing] = 0.5;
$SkillMultiplier[Cleric, $Skill::Stealing] = 0.2;
$SkillMultiplier[Cleric, $Skill::Hiding] = 0.2;
$SkillMultiplier[Cleric, $Skill::Backstabbing] = 0.2;
$SkillMultiplier[Cleric, $Skill::OffensiveCasting] = 0.9;
$SkillMultiplier[Cleric, $Skill::DefensiveCasting] = 2.0;
$SkillMultiplier[Cleric, $Skill::NeutralCasting] = 1.2;
$SkillMultiplier[Cleric, $Skill::SpellResistance] = 1.5;
$SkillMultiplier[Cleric, $Skill::Healing] = 2.0;
$SkillMultiplier[Cleric, $Skill::Archery] = 0.5;
$SkillMultiplier[Cleric, $Skill::Endurance] = 1.1;
$SkillMultiplier[Cleric, $Skill::Mining] = 1.0;
$SkillMultiplier[Cleric, $Skill::Speech] = 1.0;
$SkillMultiplier[Cleric, $Skill::SenseHeading] = 1.0;
$SkillMultiplier[Cleric, $Skill::Energy] = 1.5;
$SkillMultiplier[Cleric, $Skill::Haggling] = 1.0;
$SkillMultiplier[Cleric, $Skill::Cleaving] = 0.5;
$SkillMultiplier[Cleric, $Skill::IgniteArrow] = 0.4;
$SkillMultiplier[Cleric, $Skill::Focus] = 1.5;
$HPPerLvl[Cleric] = 0;
$EXPmultiplier[Cleric] = 0.85;

//--------------
// Druid
//--------------
// Druids are good with Bludgeoning weapons and are somewhat familiar with spells.  They specialize in Neutral casting.
// However they are also able to easily hide.

//Primary Skill: Neutral Casting
//Secondary Skill: Hiding, Slashing, Spell Resistance

$SkillMultiplier[Druid, $Skill::Slashing] = 1.5;
$SkillMultiplier[Druid, $Skill::Piercing] = 0.7;
$SkillMultiplier[Druid, $Skill::Bludgeoning] = 0.6;
$SkillMultiplier[Druid, $Skill::Dodging] = 2.0;
$SkillMultiplier[Druid, $Skill::WeightCapacity] = 2.0;
$SkillMultiplier[Druid, $Skill::Bashing] = 0.5;
$SkillMultiplier[Druid, $Skill::Stealing] = 0.2;
$SkillMultiplier[Druid, $Skill::Hiding] = 2.0;
$SkillMultiplier[Druid, $Skill::Backstabbing] = 0.5;
$SkillMultiplier[Druid, $Skill::OffensiveCasting] = 0.7;
$SkillMultiplier[Druid, $Skill::DefensiveCasting] = 0.7;
$SkillMultiplier[Druid, $Skill::NeutralCasting] = 2.0;
$SkillMultiplier[Druid, $Skill::SpellResistance] = 1.0;
$SkillMultiplier[Druid, $Skill::Healing] = 1.3;
$SkillMultiplier[Druid, $Skill::Archery] = 0.7;
$SkillMultiplier[Druid, $Skill::Endurance] = 0.8;
$SkillMultiplier[Druid, $Skill::Mining] = 2.0;
$SkillMultiplier[Druid, $Skill::Speech] = 1.0;
$SkillMultiplier[Druid, $Skill::SenseHeading] = 1.7;
$SkillMultiplier[Druid, $Skill::Energy] = 1.2;
$SkillMultiplier[Druid, $Skill::Haggling] = 1.3;
$SkillMultiplier[Druid, $Skill::Cleaving] = 1.0;
$SkillMultiplier[Druid, $Skill::IgniteArrow] = 0.5;
$SkillMultiplier[Druid, $Skill::Focus] = 1.0;
$HPPerLvl[Druid] = 1;
$EXPmultiplier[Druid] = 0.8;

//--------------
// Thief
//--------------
//Thieves handle piercing weapons well enough, and are very good at hiding and backstabbing.
//And of course, they are great at stealing.

//Primary Skill: Stealing
//Secondary Skill: Hiding, Backstabbing, Piercing, Archery

$SkillMultiplier[Thief, $Skill::Slashing] = 0.6;
$SkillMultiplier[Thief, $Skill::Piercing] = 1.8;
$SkillMultiplier[Thief, $Skill::Bludgeoning] = 0.5;
$SkillMultiplier[Thief, $Skill::Dodging] = 1.1;
$SkillMultiplier[Thief, $Skill::WeightCapacity] = 0.7;
$SkillMultiplier[Thief, $Skill::Bashing] = 0.2;
$SkillMultiplier[Thief, $Skill::Stealing] = 1.8;
$SkillMultiplier[Thief, $Skill::Hiding] = 1.8;
$SkillMultiplier[Thief, $Skill::Backstabbing] = 2.0;
$SkillMultiplier[Thief, $Skill::OffensiveCasting] = 0.2;
$SkillMultiplier[Thief, $Skill::DefensiveCasting] = 0.2;
$SkillMultiplier[Thief, $Skill::NeutralCasting] = 0.2;
$SkillMultiplier[Thief, $Skill::SpellResistance] = 0.3;
$SkillMultiplier[Thief, $Skill::Healing] = 0.5;
$SkillMultiplier[Thief, $Skill::Archery] = 1.6;
$SkillMultiplier[Thief, $Skill::Endurance] = 1.0;
$SkillMultiplier[Thief, $Skill::Mining] = 1.0;
$SkillMultiplier[Thief, $Skill::Speech] = 1.0;
$SkillMultiplier[Thief, $Skill::SenseHeading] = 1.0;
$SkillMultiplier[Thief, $Skill::Energy] = 0.5;
$SkillMultiplier[Thief, $Skill::Haggling] = 1.5;
$SkillMultiplier[Thief, $Skill::Cleaving] = 0.1;
$SkillMultiplier[Thief, $Skill::IgniteArrow] = 0.3;
$SkillMultiplier[Thief, $Skill::Focus] = 0.1;
$HPPerLvl[Thief] = 4;
$EXPmultiplier[Thief] = 0.8;

//--------------
// Bard
//--------------
//Bards are much like thieves, except that they are a bit more evenly balanced.

//Primary Skill: Stealing
//Secondary Skill: Archery

$SkillMultiplier[Bard, $Skill::Slashing] = 1.3;
$SkillMultiplier[Bard, $Skill::Piercing] = 1.5;
$SkillMultiplier[Bard, $Skill::Bludgeoning] = 1.3;
$SkillMultiplier[Bard, $Skill::Dodging] = 2.0;
$SkillMultiplier[Bard, $Skill::WeightCapacity] = 0.8;
$SkillMultiplier[Bard, $Skill::Bashing] = 0.2;
$SkillMultiplier[Bard, $Skill::Stealing] = 2.0;
$SkillMultiplier[Bard, $Skill::Hiding] = 1.8;
$SkillMultiplier[Bard, $Skill::Backstabbing] = 1.8;
$SkillMultiplier[Bard, $Skill::OffensiveCasting] = 0.3;
$SkillMultiplier[Bard, $Skill::DefensiveCasting] = 0.3;
$SkillMultiplier[Bard, $Skill::NeutralCasting] = 0.5;
$SkillMultiplier[Bard, $Skill::SpellResistance] = 0.5;
$SkillMultiplier[Bard, $Skill::Healing] = 2.0;
$SkillMultiplier[Bard, $Skill::Archery] = 1.4;
$SkillMultiplier[Bard, $Skill::Endurance] = 2.0;
$SkillMultiplier[Bard, $Skill::Mining] = 2.0;
$SkillMultiplier[Bard, $Skill::Speech] = 1.0;
$SkillMultiplier[Bard, $Skill::SenseHeading] = 1.5;
$SkillMultiplier[Bard, $Skill::Energy] = 0.6;
$SkillMultiplier[Bard, $Skill::Haggling] = 2.0;
$SkillMultiplier[Bard, $Skill::Cleaving] = 0.5;
$SkillMultiplier[Bard, $Skill::IgniteArrow] = 0.3;
$SkillMultiplier[Bard, $Skill::Focus] = 0.9;
$HPPerLvl[Bard] = 2;
$EXPmultiplier[Bard] = 0.8;

//--------------
// Fighter
//--------------
// Fighters are great with swords, namely slashing weapons.  They are strong, but dumb.
// They know nothing when it comes to spells.  However they can easily wear armor and
// wield all kinds of weapons.

//Primary Skill: Slashing
//Secondary Skill: Bludgeoning

$SkillMultiplier[Fighter, $Skill::Slashing] = 2.0;
$SkillMultiplier[Fighter, $Skill::Piercing] = 1.5;
$SkillMultiplier[Fighter, $Skill::Bludgeoning] = 2.0;
$SkillMultiplier[Fighter, $Skill::Dodging] = 1.5;
$SkillMultiplier[Fighter, $Skill::WeightCapacity] = 1.5;
$SkillMultiplier[Fighter, $Skill::Bashing] = 1.6;
$SkillMultiplier[Fighter, $Skill::Stealing] = 0.2;
$SkillMultiplier[Fighter, $Skill::Hiding] = 0.2;
$SkillMultiplier[Fighter, $Skill::Backstabbing] = 0.2;
$SkillMultiplier[Fighter, $Skill::OffensiveCasting] = 0.1;
$SkillMultiplier[Fighter, $Skill::DefensiveCasting] = 0.1;
$SkillMultiplier[Fighter, $Skill::NeutralCasting] = 0.1;
$SkillMultiplier[Fighter, $Skill::SpellResistance] = 0.2;
$SkillMultiplier[Fighter, $Skill::Healing] = 1.2;
$SkillMultiplier[Fighter, $Skill::Archery] = 1.6;
$SkillMultiplier[Fighter, $Skill::Endurance] = 1.6;
$SkillMultiplier[Fighter, $Skill::Mining] = 1.0;
$SkillMultiplier[Fighter, $Skill::Speech] = 0.8;
$SkillMultiplier[Fighter, $Skill::SenseHeading] = 0.8;
$SkillMultiplier[Fighter, $Skill::Energy] = 0.2;
$SkillMultiplier[Fighter, $Skill::Haggling] = 1.0;
$SkillMultiplier[Fighter, $Skill::Cleaving] = 1.5;
$SkillMultiplier[Fighter, $Skill::IgniteArrow] = 0.9;
$SkillMultiplier[Fighter, $Skill::Focus] = 0.1;
$HPPerLvl[Fighter] = 12;
$EXPmultiplier[Fighter] = 1.0;

//--------------
// Paladin
//--------------
//Paladins are much like Fighters, except that they are a bit more evenly balanced.

//Primary Skill: Bludgeoning
//Secondary Skill: Healing

$SkillMultiplier[Paladin, $Skill::Slashing] = 1.5;
$SkillMultiplier[Paladin, $Skill::Piercing] = 1.5;
$SkillMultiplier[Paladin, $Skill::Bludgeoning] = 1.9;
$SkillMultiplier[Paladin, $Skill::Dodging] = 1.5;
$SkillMultiplier[Paladin, $Skill::WeightCapacity] = 1.5;
$SkillMultiplier[Paladin, $Skill::Bashing] = 1.5;
$SkillMultiplier[Paladin, $Skill::Stealing] = 0.3;
$SkillMultiplier[Paladin, $Skill::Hiding] = 0.3;
$SkillMultiplier[Paladin, $Skill::Backstabbing] = 0.3;
$SkillMultiplier[Paladin, $Skill::OffensiveCasting] = 0.2;
$SkillMultiplier[Paladin, $Skill::DefensiveCasting] = 1.0;
$SkillMultiplier[Paladin, $Skill::NeutralCasting] = 0.3;
$SkillMultiplier[Paladin, $Skill::SpellResistance] = 0.9;
$SkillMultiplier[Paladin, $Skill::Healing] = 2.0;
$SkillMultiplier[Paladin, $Skill::Archery] = 1.2;
$SkillMultiplier[Paladin, $Skill::Endurance] = 1.5;
$SkillMultiplier[Paladin, $Skill::Mining] = 1.0;
$SkillMultiplier[Paladin, $Skill::Speech] = 0.8;
$SkillMultiplier[Paladin, $Skill::SenseHeading] = 0.7;
$SkillMultiplier[Paladin, $Skill::Energy] = 0.9;
$SkillMultiplier[Paladin, $Skill::Haggling] = 1.3;
$SkillMultiplier[Paladin, $Skill::Cleaving] = 0.5;
$SkillMultiplier[Paladin, $Skill::IgniteArrow] = 0.5;
$SkillMultiplier[Paladin, $Skill::Focus] = 0.7;
$HPPerLvl[Paladin] = 5;
$EXPmultiplier[Paladin] = 1.0;

//--------------
// Ranger
//--------------
// Rangers specialize in ranged weaponry.  They are also good at finding their way when lost.
// They can also wear armors and wield weapons easily enough.

//Primary Skill: Archery
//Secondary Skills: Slashing, Sense Heading

$SkillMultiplier[Ranger, $Skill::Slashing] = 1.6;
$SkillMultiplier[Ranger, $Skill::Piercing] = 1.1;
$SkillMultiplier[Ranger, $Skill::Bludgeoning] = 1.2;
$SkillMultiplier[Ranger, $Skill::Dodging] = 1.8;
$SkillMultiplier[Ranger, $Skill::WeightCapacity] = 1.0;
$SkillMultiplier[Ranger, $Skill::Bashing] = 0.9;
$SkillMultiplier[Ranger, $Skill::Stealing] = 0.5;
$SkillMultiplier[Ranger, $Skill::Hiding] = 1.0;
$SkillMultiplier[Ranger, $Skill::Backstabbing] = 0.4;
$SkillMultiplier[Ranger, $Skill::OffensiveCasting] = 0.2;
$SkillMultiplier[Ranger, $Skill::DefensiveCasting] = 0.4;
$SkillMultiplier[Ranger, $Skill::NeutralCasting] = 0.3;
$SkillMultiplier[Ranger, $Skill::SpellResistance] = 0.2;
$SkillMultiplier[Ranger, $Skill::Healing] = 0.8;
$SkillMultiplier[Ranger, $Skill::Archery] = 2.0;
$SkillMultiplier[Ranger, $Skill::Endurance] = 1.2;
$SkillMultiplier[Ranger, $Skill::Mining] = 1.0;
$SkillMultiplier[Ranger, $Skill::Speech] = 1.0;
$SkillMultiplier[Ranger, $Skill::SenseHeading] = 2.0;
$SkillMultiplier[Ranger, $Skill::Energy] = 0.7;
$SkillMultiplier[Ranger, $Skill::Haggling] = 0.7;
$SkillMultiplier[Ranger, $Skill::Cleaving] = 0.5;
$SkillMultiplier[Ranger, $Skill::IgniteArrow] = 1.8;
$SkillMultiplier[Ranger, $Skill::Focus] = 0.1;
$HPPerLvl[Ranger] = 10;
$EXPmultiplier[Ranger] = 0.95;

//--------------
// Mage
//--------------
// Mages are horrible with weapons and armor, but excel in anything that
// relates to spells.

//Primary Skill: Offensive Casting
//Secondary Skills: Focus

$SkillMultiplier[Mage, $Skill::Slashing] = 0.3;
$SkillMultiplier[Mage, $Skill::Piercing] = 0.8;
$SkillMultiplier[Mage, $Skill::Bludgeoning] = 1.0;
$SkillMultiplier[Mage, $Skill::Dodging] = 1.2;
$SkillMultiplier[Mage, $Skill::WeightCapacity] = 0.6;
$SkillMultiplier[Mage, $Skill::Bashing] = 0.1;
$SkillMultiplier[Mage, $Skill::Stealing] = 0.1;
$SkillMultiplier[Mage, $Skill::Hiding] = 0.1;
$SkillMultiplier[Mage, $Skill::Backstabbing] = 0.1;
$SkillMultiplier[Mage, $Skill::OffensiveCasting] = 2.0;
$SkillMultiplier[Mage, $Skill::DefensiveCasting] = 0.6;
$SkillMultiplier[Mage, $Skill::NeutralCasting] = 1.2;
$SkillMultiplier[Mage, $Skill::SpellResistance] = 1.5;
$SkillMultiplier[Mage, $Skill::Healing] = 0.7;
$SkillMultiplier[Mage, $Skill::Archery] = 0.8;
$SkillMultiplier[Mage, $Skill::Endurance] = 0.5;
$SkillMultiplier[Mage, $Skill::Mining] = 1.0;
$SkillMultiplier[Mage, $Skill::Speech] = 1.5;
$SkillMultiplier[Mage, $Skill::SenseHeading] = 0.1;
$SkillMultiplier[Mage, $Skill::Energy] = 0.0;//not in
$SkillMultiplier[Mage, $Skill::Haggling] = 1.0;
$SkillMultiplier[Mage, $Skill::Cleaving] = 0.1;
$SkillMultiplier[Mage, $Skill::IgniteArrow] = 1.0;
$SkillMultiplier[Mage, $Skill::Focus] = 2.0;
$HPPerLvl[Mage] = 2;
$EXPmultiplier[Mage] = 1.0;

//--------------
// Conjurer
//--------------
// Conjurer are horrible with weapons and armor, but excel in anything that
// relates to spells (offensive and neutral)

//Primary Skill: Offensive Casting
//Secondary Skills: Neutral Casting

$SkillMultiplier[Conjurer, $Skill::Slashing] = 0.6;
$SkillMultiplier[Conjurer, $Skill::Piercing] = 0.3;
$SkillMultiplier[Conjurer, $Skill::Bludgeoning] = 1.0;
$SkillMultiplier[Conjurer, $Skill::Dodging] = 1.2;
$SkillMultiplier[Conjurer, $Skill::WeightCapacity] = 0.9;
$SkillMultiplier[Conjurer, $Skill::Bashing] = 0.1;
$SkillMultiplier[Conjurer, $Skill::Stealing] = 0.1;
$SkillMultiplier[Conjurer, $Skill::Hiding] = 0.1;
$SkillMultiplier[Conjurer, $Skill::Backstabbing] = 0.1;
$SkillMultiplier[Conjurer, $Skill::OffensiveCasting] = 2.0;
$SkillMultiplier[Conjurer, $Skill::DefensiveCasting] = 0.1;
$SkillMultiplier[Conjurer, $Skill::NeutralCasting] = 2.0;
$SkillMultiplier[Conjurer, $Skill::SpellResistance] = 1.0;
$SkillMultiplier[Conjurer, $Skill::Healing] = 0.6;
$SkillMultiplier[Conjurer, $Skill::Archery] = 0.2;
$SkillMultiplier[Conjurer, $Skill::Endurance] = 0.6;
$SkillMultiplier[Conjurer, $Skill::Mining] = 0.9;
$SkillMultiplier[Conjurer, $Skill::Speech] = 1.9;
$SkillMultiplier[Conjurer, $Skill::SenseHeading] = 0.5;
$SkillMultiplier[Conjurer, $Skill::Energy] = 1.5;
$SkillMultiplier[Conjurer, $Skill::Haggling] = 1.3;
$SkillMultiplier[Conjurer, $Skill::Cleaving] = 0.8;
$SkillMultiplier[Conjurer, $Skill::IgniteArrow] = 0.5;
$SkillMultiplier[Conjurer, $Skill::Focus] = 1.5;
$HPPerLvl[Conjurer] = 4;
$EXPmultiplier[Conjurer] = 0.95;

//######################################################################################
// Skill Restriction tables
//######################################################################################

//To determine skill restrictions, do the following:
//
//-Determine the following variables first:
//	(weapon):
//	a = ATK * 1.1 (archery is 0.75)
//	b = Delay = Cap((Weight / 3), 1, "inf")
//
//	(armor):
//	a = (DEF + MDEF) / 6
//	b = 1.0
//
//-To find out what the skill restriction number is, follow this formula, where s is the final skill restriction:
//	s = Cap((a / b) - 20), 0, "inf") * 10.0;
//

$SkillRestriction[BluePotion] = $Skill::Healing @ " 0";
$SkillRestriction[CrystalBluePotion] = $Skill::Healing @ " 0";
$SkillRestriction[BasicRobe]  = $Magic @ " 10";
$SkillRestriction[ApprenticeRobe] = $Magic SPC "30";
$SkillRestriction[LightRobe] = $Skill::Endurance @ " 3 " @ $Magic @ " 80";
$SkillRestriction[FineRobe] = $Skill::Endurance @ " 9 " @ $Magic @ " 175";
$SkillRestriction[BloodRobe] = $Skill::Endurance @ " 8 " @ $Magic @ " 300";
$SkillRestriction[AdvisorRobe] = $Skill::Endurance @ " 10 " @ $Magic @ " 450";
$SkillRestriction[ElvenRobe] = $Skill::Endurance @ " 12 " @ $Magic @ " 620";
//$SkillRestriction[RobeOfVenjance] = $Skill::Endurance @ " 18 " @ $Magic @ " 800";
$SkillRestriction[PhensRobe] = $Skill::Endurance @ " 20 " @ $Magic @ " 980";
$SkillRestriction[QuestMasterRobe] = $MinAdmin @ " 3";

$SkillRestriction[PaddedArmor] = $SkillEndurance @ " 5";
$SkillRestriction[LeatherArmor] = $SkillEndurance @ " 40";
$SkillRestriction[StuddedLeatherArmor] = $SkillEndurance @ " 95";
$SkillRestriction[SpikedLeatherArmor] = $SkillEndurance @ " 135";
$SkillRestriction[HideArmor] = $SkillEndurance @ " 180";
$SkillRestriction[ScaleMailBody] = $SkillEndurance @ " 240";
$SkillRestriction[BrigandineBody] = $SkillEndurance @ " 300";
$SkillRestriction[ChainMailBody] = $SkillEndurance @ " 350";
$SkillRestriction[RingMailBody] = $SkillEndurance @ " 410";
$SkillRestriction[BandedMailArmor] = $SkillEndurance @ " 490";
$SkillRestriction[SplintMailBody] = $SkillEndurance @ " 580";
$SkillRestriction[BronzePlateMail] = $SkillEndurance @ " 660";
$SkillRestriction[HalfPlate] = $SkillEndurance @ " 775";
$SkillRestriction[FieldPlate] = $SkillEndurance @ " 840";
$SkillRestriction[DragonMail] = $SkillEndurance @ " 950";
$SkillRestriction[FullPlate] = $SkillEndurance @ " 1065";
$SkillRestriction[LeatherBoots] = $Skill::Endurance @ " 8";
$SkillRestriction[BootsOfGliding] = $MinLevel @ " 25";
$SkillRestriction[WindWalkers] = $MinLevel @ " 60";
$SkillRestriction[KeldrinitePlate] = $SkillEndurance @ " 1305";

$SkillRestriction[KnightShield] = $SkillEndurance @ " 140";
$SkillRestriction[HeavenlyShield] = $SkillEndurance @ " 540";
$SkillRestriction[DragonShield] = $SkillEndurance @ " 715";

$SkillRestriction[Hatchet] = $Skill::Slashing @ " 0";
$SkillRestriction[WarAxe] = $Skill::Slashing @ " 35";
$SkillRestriction[BroadSword] = $Skill::Slashing @ " 70";
$SkillRestriction[LongSword] = $Skill::Slashing @ " 140";
$SkillRestriction[IceBroadSword] = $Skill::Slashing SPC "170";
$SkillRestriction[FireBroadSword] = $skillRestriction[IceBroadSword]; 
$SkillRestriction[WaterBroadSword] = $skillRestriction[IceBroadSword]; 
$SkillRestriction[BattleAxe] = $Skill::Slashing @ " 300";
$SkillRestriction[BastardSword] = $Skill::Slashing @ " 620";
$SkillRestriction[Halberd] = $Skill::Slashing @ " 768";
$SkillRestriction[Claymore] = $Skill::Slashing @ " 900";
$SkillRestriction[GreatClaymore] = $Skill::Slashing @ " 900";
$SkillRestriction[KeldriniteLS] = $Skill::Slashing @ " 1120";
//.................................................................................
$SkillRestriction[Club] = $SkillBludgeoning @ " 0";
$SkillRestriction[QuarterStaff] = $SkillBludgeoning @ " 20";
$SkillRestriction[BoneClub] = $SkillBludgeoning @ " 45";
$SkillRestriction[SpikedClub] = $SkillBludgeoning @ " 60";
$SkillRestriction[Mace] = $SkillBludgeoning @ " 140";
$SkillRestriction[HammerPick] = $SkillBludgeoning @ " 300";
$SkillRestriction[SpikedBoneClub] = $SkillBludgeoning @ " 450";
$SkillRestriction[LongStaff] = $SkillBludgeoning @ " 620";
$SkillRestriction[WarHammer] = $SkillBludgeoning @ " 768";
$SkillRestriction[JusticeStaff] = $SkillBludgeoning @ " 834";
$SkillRestriction[WarMaul] = $SkillBludgeoning @ " 900";
//.................................................................................
$SkillRestriction[PickAxe] = $Skill::Piercing @ " 0";
$SkillRestriction[Knife] = $Skill::Piercing @ " 0";
$SkillRestriction[Dagger] = $Skill::Piercing @ " 60";
$SkillRestriction[ShortSword] = $Skill::Piercing @ " 140";
$SkillRestriction[Spear] = $Skill::Piercing @ " 280";
$SkillRestriction[Gladius] = $Skill::Piercing @ " 450";
$SkillRestriction[Trident] = $Skill::Piercing @ " 620";
$SkillRestriction[Rapier] = $Skill::Piercing @ " 768";
$SkillRestriction[Katana] = $Skill::Piercing @ " 768";
$SkillRestriction[AwlPike] = $Skill::Piercing @ " 900";
$SkillRestriction[Katar] = $Skill::Piercing @ " 680";
//.................................................................................
$SkillRestriction[Sling] = $SkillArchery @ " 0";
$SkillRestriction[ShortBow] = $SkillArchery @ " 25";
$SkillRestriction[LightCrossbow] = $SkillArchery @ " 160";
$SkillRestriction[LongBow] = $SkillArchery @ " 318";
$SkillRestriction[CompositeBow] = $SkillArchery @ " 438";
$SkillRestriction[RepeatingCrossbow] = $SkillArchery @ " 550";
$SkillRestriction[ElvenBow] = $SkillArchery @ " 685";
$SkillRestriction[AeolusWing] = $SkillArchery @ " 805";
$SkillRestriction[HeavyCrossbow] = $SkillArchery @ " 925";
//.................................................................................
$SkillRestriction[SmallRock] = $Skill::Archery @ " 0";
$SkillRestriction[BasicArrow] = $Skill::Archery @ " 0";
$SkillRestriction[ShortQuarrel] = $Skill::Archery @ " 0";
$SkillRestriction[LightQuarrel] = $Skill::Archery @ " 0";
$SkillRestriction[SheafArrow] = $Skill::Archery @ " 0";
$SkillRestriction[StoneFeather] = $Skill::Archery @ " 0";
$SkillRestriction[BladedArrow] = $Skill::Archery @ " 0";
$SkillRestriction[HeavyQuarrel] = $Skill::Archery @ " 0";
$SkillRestriction[MetalFeather] = $Skill::Archery @ " 0";
$SkillRestriction[Talon] = $Skill::Archery @ " 0";
$SkillRestriction[CeraphumsFeather] = $Skill::Archery @ " 0";


// Chat functions
//Phantom139: Re-ordered by level requirement/class
$SkillRestriction["#backstab"] = $Skill::Backstabbing @ " 15";
$SkillRestriction["#surge"] = $Skill::Backstabbing @ " 50";
$SkillRestriction["#encumber"] = $Skill::Backstabbing @ " 150";

$SkillRestriction["#ignite"] = $Skill::IgniteArrow @ " 15";

$SkillRestriction["#cleave"] = $Skill::Cleaving @ " 15";
$SkillRestriction["#berserk"] = $Skill::Cleaving @ " 50";
$SkillRestriction["#targetleg"] = $Skill::Cleaving @ " 150";

$SkillRestriction["#say"] = $Skill::Speech @ " 0";
$SkillRestriction["#whisper"] = $Skill::Speech @ " 0";
$SkillRestriction["#tell"] = $Skill::Speech @ " 0";
$SkillRestriction["#zone"] = $Skill::Speech @ " 0";
$SkillRestriction["#party"] = $Skill::Speech @ " 0";
$SkillRestriction["#shout"] = $Skill::Speech @ " 3";
$SkillRestriction["#global"] = $Skill::Speech @ " 0";
$SkillRestriction["#group"] = $Skill::Speech @ " 5";
$SkillRestriction["#guild"] = $Skill::Speech @ " 10";

$SkillRestriction["#steal"] = $Skill::Stealing @ " 15";
$SkillRestriction["#pickpocket"] = $Skill::Stealing @ " 270";
$SkillRestriction["#mug"] = $Skill::Stealing @ " 620";

$SkillRestriction["#hide"] = $Skill::Hiding @ " 15";

$SkillRestriction["#focus"] = $Skill::Focus @ " 15";

$SkillRestriction["#shove"] = $Skill::Bashing @ " 5";
$SkillRestriction["#bash"] = $Skill::Bashing @ " 15";
$SkillRestriction["#disrupt"] = $Skill::Bashing @ " 50";
$SkillRestriction["#stun"] = $Skill::Bashing @ " 170";

$SkillRestriction["#compass"] = $Skill::SenseHeading @ " 3";
$SkillRestriction["#track"] = $Skill::SenseHeading @ " 15";
$SkillRestriction["#advcompass"] = $Skill::SenseHeading @ " 20";
$SkillRestriction["#zonelist"] = $Skill::SenseHeading @ " 45";
$SkillRestriction["#trackpack"] = $Skill::SenseHeading @ " 85";


// Spells
$SkillRestriction[Bolt] = $Skill::OffensiveCasting @ " 5";
$SkillRestriction[thorn] = $Skill::OffensiveCasting @ " 15";
$SkillRestriction[fireball] = $Skill::OffensiveCasting @ " 20";
$SkillRestriction[firebomb] = $Skill::OffensiveCasting @ " 35";
$SkillRestriction[icespike] = $Skill::OffensiveCasting @ " 45";
$SkillRestriction[icestorm] = $Skill::OffensiveCasting @ " 85";
$SkillRestriction[ironfist] = $Skill::OffensiveCasting @ " 110";
$SkillRestriction[cloud] = $Skill::OffensiveCasting @ " 145";
$SkillRestriction[melt] = $Skill::OffensiveCasting @ " 220";
$SkillRestriction[powercloud] = $Skill::OffensiveCasting @ " 340";
$skillRestriction[spikes]		= $skill::OffensiveCasting @ " 380";
$SkillRestriction[hellstorm] = $Skill::OffensiveCasting @ " 420";
$SkillRestriction[beam] = $Skill::OffensiveCasting @ " 520";
$SkillRestriction[dimensionrift] = $Skill::OffensiveCasting @ " 950";
$SkillRestriction[snowstorm] = $skill::OffensiveCasting @ " 750";

$SkillRestriction[Flare] = $Skill::NeutralCasting @ " 5";
$SkillRestriction[SignalFlare] = $Skill::NeutralCasting @ " 20";
$SkillRestriction[AdminSignalFlare] = $MinAdmin SPC "3";
$SkillRestriction[teleport] = $Skill::NeutralCasting @ " 60";
$SkillRestriction[Guildteleport] = $Skill::NeutralCasting @ " 20";
$SkillRestriction[AdvGuildteleport] = $Skill::NeutralCasting @ " 260";
$SkillRestriction[transport] = $Skill::NeutralCasting @ " 200";
$SkillRestriction[advtransport] = $Skill::NeutralCasting @ " 350";
$SkillRestriction[masstransport] = $Skill::NeutralCasting @ " 650";
$SkillRestriction[flow] = $Skill::NeutralCasting @ " 50";
$SkillRestriction[bound] = $Skill::NeutralCasting @ " 100"; 

$SkillRestriction[heal] = $Skill::DefensiveCasting @ " 10";
$SkillRestriction[strongheal] = $Skill::DefensiveCasting @ " 80";
$SkillRestriction[advheal] = $Skill::DefensiveCasting @ " 110";
$SkillRestriction[ExpertHeal] = $Skill::DefensiveCasting @ " 200";
//$SkillRestriction[advheal4] = $SkillDefensiveCasting @ " 320";
//$SkillRestriction[advheal5] = $SkillDefensiveCasting @ " 400";
//$SkillRestriction[advheal6] = $SkillDefensiveCasting @ " 500";
$SkillRestriction[godlyheal] = $Skill::DefensiveCasting @ " 600";
$SkillRestriction[fullheal] = $Skill::DefensiveCasting @ " 750";
$SkillRestriction[massheal] = $Skill::DefensiveCasting @ " 850";
$SkillRestriction[massfullheal] = $Skill::DefensiveCasting @ " 950";
$SkillRestriction[shield] = $Skill::DefensiveCasting @ " 20";
$SkillRestriction[FireShield] = $Skill::DefensiveCasting @ " 60";
$SkillRestriction[EarthShield] = $Skill::DefensiveCasting @ " 70";
$SkillRestriction[WaterShield] = $Skill::DefensiveCasting @ " 80";
$SkillRestriction[WindShield] = $Skill::DefensiveCasting @ " 90";
$SkillRestriction[EnergyShield] = $Skill::DefensiveCasting @ " 100";
$SkillRestriction[GravityShield] = $Skill::DefensiveCasting @ " 140";
$SkillRestriction[advshield] = $Skill::DefensiveCasting @ " 290";
//$SkillRestriction[advshield4] = $Skill::DefensiveCasting @ " 420";
$SkillRestriction[AdvFireShield] = $Skill::DefensiveCasting @ " 420";
$SkillRestriction[AdvEarthShield] = $Skill::DefensiveCasting @ " 440";
$SkillRestriction[AdvWaterShield] = $Skill::DefensiveCasting @ " 480";
$SkillRestriction[AdvWindShield] = $Skill::DefensiveCasting @ " 500";
$SkillRestriction[AdvEnergyShield] = $Skill::DefensiveCasting @ " 520";
$SkillRestriction[AdvGravityShield] = $Skill::DefensiveCasting @ " 540";
$SkillRestriction[godlyshield] = $Skill::DefensiveCasting @ " 635";
$SkillRestriction[massshield] = $Skill::DefensiveCasting @ " 680";
$SkillRestriction[heavenlyshield] = $Skill::DefensiveCasting @ " 1100";

//######################################################################################
// Skill functions
//######################################################################################

function GetNumSkills()
{
	for(%i = 1; $SkillDesc[%i] !$= ""; %i++){}
	return %i-1;
}
function AddSkillPoint(%client, %skill, %delta)
{
	return game.AddSkillPoint(%client, %skill, %delta);
}
function RPGGame::AddSkillPoint(%game, %client, %skill, %delta)
{
	if(%delta $= "")
		%delta = 1;

	//==== CAPS ================
	//if(%client.PlayerSkill[%skill] >= $SkillCap)
	//	return false;

	//%ub = ($skillRangePerLevel * fetchData(%client, "LVL")) + 20;
	//if(%client.PlayerSkill[%skill] >= %ub)
	//	return false;
	//==========================

	%a = GetSkillMultiplier(%client, %skill) * %delta;
	%b = %client.data.PlayerSkill[%skill];
	%c = %a + %b;

	%client.data.PlayerSkill[%skill] = %c;
 
    //Phantom139: Display any newly obtained skills
    CaptureNewSkills(%client, %skill, %b, %c);

	return true;
}
function RPGGame::GetPlayerSkill(%game, %client, %skill)
{
	return %client.data.PlayerSkill[%skill];
}
function GetPlayerSkill(%client, %skill)
{
	return game.GetplayerSkill(%client, %skill);
}
function GetSkillMultiplier(%client, %skill)
{
	game.GetSkillMultiplier(%client, %skill);
}
function RPGGame::GetSkillMultiplier(%game, %client, %skill)
{
	%a = $SkillMultiplier[fetchData(%client, "CLASS"), %skill];

	%c = Cap(%a, "inf", 30.0);

	return %c;
}
function GetEXPmultiplier(%client)
{
	%a = $EXPmultiplier[fetchData(%client, "CLASS")];
	

	%c = %a + %b;

	return %c;
}

function SetAllSkills(%client, %n)
{
	for(%i = 1; $SkillDesc[%i] !$= ""; %i++)
		%client.data.PlayerSkill[%i] = %n;
}
function SetSkillsToMulti(%client)
{
	for(%i = 1; $skillDesc[%i] !$= ""; %i++)
	{
		AddSkillPoint(%client, %i, 1);
	}

}
function SkillCanUse(%client, %thing)
{
	return game.SkillCanUse(%client, %thing);
}
function RPGGame::SkillCanUse(%game, %client, %thing)
{
	if(%client.adminLevel >= 5)
		return true;

	%flag = 0;
	%gc = 0;
	%gcflag = 0;
	
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) !$= ""; %i+=2)
	{
		%s = GetWord($SkillRestriction[%thing], %i);
		%n = GetWord($SkillRestriction[%thing], %i+1);

		if(%s $= "L")
		{
			if(fetchData(%client, "LVL") < %n)
				%flag = 1;
		}
		else if(%s $= "R")
		{
			if(fetchData(%client, "RemortStep") < %n)
				%flag = 1;
		}
		else if(%s $= "A")
		{
			if(%client.adminLevel < %n)
				%flag = 1;
		}
		else if(%s $= "G")
		{
			%gcflag++;
			if(stricmp(fetchData(%client, "GROUP"), %n) $= 0)
				%gc = 1;
		}
		else if(%s $= "C")
		{
			%gcflag++;
			if(stricmp(fetchData(%client, "CLASS"), %n) $= 0)
				%gc = 1;
		}
		else if(%s $= "H")
		{
			%hflag++;
			if(stricmp(fetchData(%client, "MyHouse"), %n) $= 0)
				%hh = 1;
		}
		else if(%s $= "M")
		{
			if(%client.data.PlayerSkill[$skill::defensivecasting] + %client.data.PlayerSkill[$skill::OffensiveCasting] + %Client.data.PlayerSkill[$skill::neutralCasting] < %n)
				%flag = 1;
		}
		else if(%s $= "MF")
		{
			if(%client.data.PlayerSkill[$skill::defensivecasting] + %client.data.PlayerSkill[$skill::OffensiveCasting] + %Client.data.PlayerSkill[$skill::neutralCasting] < %n)
				%flag = 1;
		}
		else
		{
			if(%client.data.PlayerSkill[%s] < %n)
				%flag = 1;
			
			
		}
	}

	//First, if there are any class/group restrictions, house restrictions, check these first.
	if(%gcflag > 0)
	{
		if(%gc $= 0)
			%flag = 1;
	}
	if(%hflag > 0)
	{
		if(%hh $= 0)
			%flag = 1;
	}

	
	if(%flag !$= 1)
		return true;
	else
		return false;
}
function RPGGame::UseSkill(%game, %client, %skilltype, %successful, %showmsg, %base, %refreshall)
{
	if(%base $= "") %base = 35;
	%lvl = fetchdata(%client, "LVL");
	if(%lvl > 101 && !isTaggedSkill(%client, %skilltype)) %lvl = 101;//cap all but 'tagged' skills at level 101.
	
	%ub = ($skillRangePerLevel * %lvl) + 20;
	if(%client.data.PlayerSkill[%skilltype] < %ub) {
		if(%successful)
			%client.data.SkillCounter[%skilltype] += 1;
		else
			%client.data.SkillCounter[%skilltype] += 0.05;

		//%p = 1 - (%client.PlayerSkill[%skilltype] / 1150);
		%e = round( (%base / GetSkillMultiplier(%client, %skilltype)) );
		if(%client.data.SkillCounter[ %skilltype] >= %e) {
			%client.data.SkillCounter[%skilltype] = 0;
			%retval = AddSkillPoint(%client, %skilltype, 1);

			if(%retval) {
				if(%showmsg)
					messageClient(%client, 'UseSkill', "You have increased your skill in " @ $SkillDesc[%skilltype] @ " (" @ %client.data.PlayerSkill[%skilltype] @ ")");
				if(%refreshall)
					RefreshAll(%client);
			}
		}
	}
}
function UseSkill(%client, %skilltype, %successful, %showmsg, %base, %refreshall)
{
	return Game.useSkill(%client, %skilltype, %successful, %showmsg, %base, %refreshall);
}
function isTaggedSkill(%client, %skilltype)
{
	
	if(fetchdata(%client, "TagSkill1") == %skilltype )
		return true;
	if(fetchdata(%client, "TagSkill2") == %skilltype )
		return true;
	if(fetchdata(%client, "TagSkill3") == %skilltype )
		return true;
	return false;
}
function WhatSkills(%thing)
{
	%t = "";
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) !$= ""; %i+=2)
	{
		%s = GetWord($SkillRestriction[%thing], %i);
		%n = GetWord($SkillRestriction[%thing], %i+1);

		%t = %t @ $SkillDesc[%s] @ ": " @ %n @ ", ";
	}
	if(%t $= "")
		%t = "None";
	else
		%t = getsubstr(%t, 0, strlen(%t)-2);
	
	return %t;
}

function GetSkillAmount(%thing, %skill)
{
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) !$= ""; %i+=2)
	{
		%s = GetWord($SkillRestriction[%thing], %i);

		if(%s $= %skill)
			return GetWord($SkillRestriction[%thing], %i+1);
	}
	return 0;
}

function CaptureNewSkills(%client, %skill, %oldLevel, %newLevel) {

}
