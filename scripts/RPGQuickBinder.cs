//RPGQuickBinder.cs
//Goes hand in hand with the quick cast dialog

//Globals
$MinimumSkillID = 1;
$MaximumSkillID = 22;
// Skill List
// KEEP UPDATED WITH RPGSKILLS.CS!!!

//Cleaving
$SkillSet["Cleaving", 0] = "cleave\t15";
$SkillSet["Cleaving", 1] = "berserk\t50";
$SkillSet["Cleaving", 2] = "targetleg\t150";
//Bashing
$SkillSet["Bashing", 0] = "shove\t5";
$SkillSet["Bashing", 1] = "bash\t15";
$SkillSet["Bashing", 2] = "disrupt\t50";
$SkillSet["Bashing", 3] = "stun\t170";
//Hiding
$SkillSet["Hiding", 0] = "hide\t15";
//Focus
$SkillSet["Focus", 0] = "focus\t15";
//IgniteArrow
$SkillSet["IgniteArrow", 0] = "ignite\t15";
//Backstabbing
$SkillSet["Backstabbing", 0] = "backstab\t15";
$SkillSet["Backstabbing", 1] = "surge\t50";
$SkillSet["Backstabbing", 2] = "encumber\t150";
//Sense Heading
$SkillSet["Sense Heading", 0] = "compass\t3";
$SkillSet["Sense Heading", 1] = "track\t15";
$SkillSet["Sense Heading", 2] = "advcompass\t20";
$SkillSet["Sense Heading", 3] = "zonelist\t45";
$SkillSet["Sense Heading", 4] = "trackpack\t85";
//Stealing
$SkillSet["Stealing", 0] = "steal\t15";
$SkillSet["Stealing", 1] = "pickpocket\t270";
$SkillSet["Stealing", 2] = "mug\t620";
//Offensive Casting
$SkillSet["Offensive Casting", 0] = "bolt\t5";
$SkillSet["Offensive Casting", 1] = "thorn\t15";
$SkillSet["Offensive Casting", 2] = "fireball\t20";
$SkillSet["Offensive Casting", 3] = "firebomb\t35";
$SkillSet["Offensive Casting", 4] = "icespike\t45";
$SkillSet["Offensive Casting", 5] = "icestorm\t85";
$SkillSet["Offensive Casting", 6] = "ironfist\t110";
$SkillSet["Offensive Casting", 7] = "cloud\t145";
$SkillSet["Offensive Casting", 8] = "melt\t220";
$SkillSet["Offensive Casting", 9] = "powercloud\t340";
$SkillSet["Offensive Casting", 10] = "spikes\t380";
$SkillSet["Offensive Casting", 11] = "hellstorm\t420";
$SkillSet["Offensive Casting", 12] = "beam\t520";
$SkillSet["Offensive Casting", 13] = "snowstorm\t750";
$SkillSet["Offensive Casting", 14] = "dimensionrift\t950";
//Defensive Casting
$SkillSet["Defensive Casting", 0] = "heal\t10";
$SkillSet["Defensive Casting", 1] = "shield\t20";
$SkillSet["Defensive Casting", 2] = "fireshield\t60";
$SkillSet["Defensive Casting", 3] = "earthshield\t70";
$SkillSet["Defensive Casting", 4] = "watershield\t80";
$SkillSet["Defensive Casting", 5] = "strongheal\t80";
$SkillSet["Defensive Casting", 6] = "windshield\t90";
$SkillSet["Defensive Casting", 7] = "energyshield\t100";
$SkillSet["Defensive Casting", 8] = "advheal\t110";
$SkillSet["Defensive Casting", 9] = "gravityshield\t140";
$SkillSet["Defensive Casting", 10] = "expertheal\t200";
$SkillSet["Defensive Casting", 11] = "advshield\t290";
$SkillSet["Defensive Casting", 12] = "advfireshield\t420";
$SkillSet["Defensive Casting", 13] = "advearthhhield\t440";
$SkillSet["Defensive Casting", 14] = "advwatershield\t480";
$SkillSet["Defensive Casting", 15] = "advwindshield\t500";
$SkillSet["Defensive Casting", 16] = "advenergyshield\t520";
$SkillSet["Defensive Casting", 17] = "advgravityshield\t540";
$SkillSet["Defensive Casting", 18] = "godlyheal\t600";
$SkillSet["Defensive Casting", 19] = "godlyshield\t635";
$SkillSet["Defensive Casting", 20] = "massshield\t680";
$SkillSet["Defensive Casting", 21] = "fullheal\t750";
$SkillSet["Defensive Casting", 22] = "massheal\t850";
$SkillSet["Defensive Casting", 23] = "massfullheal\t950";
$SkillSet["Defensive Casting", 24] = "heavenlyshield\t1100";

//Functions
function serverCmdGatherOpenSkills(%client) {
   for(%i = $MinimumSkillID; %i <= $MaximumSkillID; %i++) {
      %primaryString = $SkillDesc[%i];
      %x = 0;
      %finalSecond = "";
      while(isSet($SkillSet[%primaryString, %x])) {
         %secondaryString = $SkillSet[%primaryString, %x];
         //
         %skillName = getField(%secondaryString, 0);
         %requiredSkillLv = getField(%secondaryString, 1);
         //
         if(GetPlayerSkill(%client, %i) >= %requiredSkillLv) {
            if(!isSet(%finalSecond)) {
               %finalSecond = %skillName;
            }
            else {
               %finalSecond = %finalSecond TAB %skillName;
            }
         }
         %x++;
      }
      //
      if(isSet(%finalSecond)) {
         //no point in sending blank secondary skill sets.
         commandToClient(%client, 'ReturnOpenSkills', %primaryString, %finalSecond);
      }
   }
}

//Assets
function isSet(%v) {
   return (%v !$= "");
}
