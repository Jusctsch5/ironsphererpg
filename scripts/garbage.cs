	$ItemType[ShortSword] = "weapon";
	$ItemSubType[ShortSword] = $SwordAccessoryType;
	$ItemDesc[ShortSword] = "Short Sword";
	$ItemSize[ShortSword] = "small";
	$ItemBaseWeight[ShortSword] = 0.5;
	$ItemBaseDelay[ShortSword] = 1.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[ShortSword] = "6 1r4";
	$ItemBaseRange[ShortSword] = $minRange + 1.0;
	$ItemDamageType[ShortSword] = $PiercingDamageType;
	$PrefixExclusions[ShortSword] = ",";
	$SuffixExclusions[ShortSword] = "3,";
	$SkillType[ShortSword] = $SkillPiercing;
	$DataBlock[ShortSword] = "ShortSword";

	$ItemType[handaxe] = "weapon";
	$ItemSubType[handaxe] = $SwordAccessoryType;
	$ItemDesc[handaxe] = "Hand Axe";
	$ItemSize[handaxe] = "small";
	$ItemBaseWeight[handaxe] = 0.5;
	$ItemBaseDelay[handaxe] = 1.5 * $ADnDdelayToRPG;
	$ItemBaseSpecialVar[handaxe] = "6 1r4";
	$ItemBaseRange[handaxe] = $minRange + 1.0;
	$ItemDamageType[handaxe] = $PiercingDamageType;
	$PrefixExclusions[handaxe] = ",";
	$SuffixExclusions[handaxe] = "3,";
	$SkillType[handaxe] = $SkillPiercing;
	$DataBlock[handaxe] = "HandAxe";
	//----------------------------------------
	// ShortSword
	//----------------------------------------
	datablock ShapeBaseImageData(ShortSwordImage)
	{
		className = WeaponImage;
		shapeFile = "shortsword.dts";
		item = ShortSword;

		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

		stateName[0] = "Activate";
		stateTransitionOnTimeout[0] = "ActivateReady";
		stateTimeoutValue[0] = 0.5;
		stateSequence[0] = "Activate";
		stateSound[0] = BlasterSwitchSound;

		stateName[1] = "ActivateReady";
		stateTransitionOnLoaded[1] = "Ready";

		stateName[2] = "Ready";
		stateTransitionOnTriggerDown[2] = "Fire";

		stateName[3] = "Fire";
		stateTransitionOnTimeout[3] = "Ready";
		stateTimeoutValue[3] = 0.1;
		stateFire[3] = true;
		stateRecoil[3] = NoRecoil;
		stateAllowImageChange[3] = false;
		stateSequence[3] = "Fire";
		stateScript[3] = "onFire";
	};
	datablock ItemData(ShortSword)
	{
		className = Weapon;
		catagory = "Spawn Items";
		shapeFile = "shortsword.dts";
		image = ShortSwordImage;
		mass = 1;
		elasticity = 0.2;
		friction = 0.6;
		pickupRadius = 2;
		pickUpPrefix = "a";
		description = "ShortSword_model";

		computeCRC = true;
		emap = true;
	};
	//----------------------------------------
	// HandAxe
	//----------------------------------------
	//datablock ShapeBaseImageData(HandAxeImage)
	//{
	//	className = WeaponImage;
	//	shapeFile = "handax.dts";
	//	item = HandAxe;
//
		//extension = $ItemBaseRange[Knife];

		//usesEnergy = true;

		//projectile = EnergyBolt;
		//projectileType = EnergyProjectile;
		//fireEnergy = 4;
		//minEnergy = 4;

	//	stateName[0] = "Activate";
	//	stateTransitionOnTimeout[0] = "ActivateReady";
	//	stateTimeoutValue[0] = 0.5;
	//	stateSequence[0] = "Activate";
	//	stateSound[0] = BlasterSwitchSound;
//
	//	stateName[1] = "ActivateReady";
	//	stateTransitionOnLoaded[1] = "Ready";

	//	stateName[2] = "Ready";
	//	stateTransitionOnTriggerDown[2] = "Fire";

	//	stateName[3] = "Fire";
	//	stateTransitionOnTimeout[3] = "Ready";
	//	stateTimeoutValue[3] = 0.1;
	//	stateFire[3] = true;
	//	stateRecoil[3] = NoRecoil;
	//	stateAllowImageChange[3] = false;
	//	stateSequence[3] = "Fire";
	//	stateScript[3] = "onFire";
	//};
	//datablock ItemData(HandAxe)
	//{
	//	className = Weapon;
	//	catagory = "Spawn Items";
	//	shapeFile = "handax.dts";
	//	image = HandAxeImage;
	//	mass = 1;
	//	elasticity = 0.2;
	//	friction = 0.6;
	//	pickupRadius = 2;
	//	pickUpPrefix = "a";
	//	description = "HandAxe1_model";
	//	computeCRC = true;
	//	emap = true;
	//};
	
function setupObjHud(%gameType)
{
   switch$ (%gameType)
	{
		case BountyGame:
			// set separators
			objectiveHud.setSeparators("56 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "90 16";
				visible = "1";
			};
			// Target label ("TARGET")
			objectiveHud.targetLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "TARGET";
			};
			// your target's name
			objectiveHud.yourTarget = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "90 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.targetLabel);
			objectiveHud.add(objectiveHud.yourTarget);

		case CnHGame:
			// set separators
			objectiveHud.setSeparators("96 162 202");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "105 3";
				extent = "50 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "105 19";
				extent = "50 16";
				visible = "1";
			};
			// Hold label ("HOLD")
			objectiveHud.holdLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "165 3";
				extent = "35 16";
				visible = "1";
				text = "HOLD";
			};
			objectiveHud.holdLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "165 19";
				extent = "35 16";
				visible = "1";
				text = "HOLD";
			};
			// number of points held
			objectiveHud.numHeld[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "205 3";
				extent = "30 16";
				visible = "1";
			};
			objectiveHud.numHeld[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "205 19";
				extent = "30 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.holdLabel[%i]);
				objectiveHud.add(objectiveHud.numHeld[%i]);
			}

		case CTFGame:
			// set separators
			objectiveHud.setSeparators("72 97 130");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "65 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "65 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "75 3";
				extent = "20 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "75 19";
				extent = "20 16";
				visible = "1";
			};
			// Flag label ("FLAG")
			objectiveHud.flagLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "30 16";
				visible = "1";
				text = "FLAG";
			};
			objectiveHud.flagLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "30 16";
				visible = "1";
				text = "FLAG";
			};
			// flag location (at base/in field/player carrying it)
			objectiveHud.flagLocation[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "135 3";
				extent = "105 16";
				visible = "1";
			};
			objectiveHud.flagLocation[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "135 19";
				extent = "105 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.flagLabel[%i]);
				objectiveHud.add(objectiveHud.flagLocation[%i]);
			}

		case DMGame:
			// set separators
			objectiveHud.setSeparators("56 96 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "30 16";
				visible = "1";
			};
			// Your kills label ("KILLS")
			objectiveHud.killsLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "KILLS";
			};
			// Your kills
			objectiveHud.yourKills = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "30 16";
				visible = "1";
			};
			// Your deaths label ("DEATHS")
			objectiveHud.deathsLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "50 16";
				visible = "1";
				text = "DEATHS";
			};
			// Your deaths
			objectiveHud.yourDeaths = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "160 19";
				extent = "30 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.killsLabel);
			objectiveHud.add(objectiveHud.yourKills);
			objectiveHud.add(objectiveHud.deathsLabel);
			objectiveHud.add(objectiveHud.yourDeaths);

		case DnDGame:

      case HuntersGame:
			// set separators
			objectiveHud.setSeparators("96 132");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "30 16";
				visible = "1";
			};
			// flags label ("FLAGS")
			objectiveHud.flagLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
				text = "FLAGS";
			};
			// number of flags
			objectiveHud.yourFlags = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "30 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.flagLabel);
			objectiveHud.add(objectiveHud.yourFlags);

		case RabbitGame:
			// set separators
			objectiveHud.setSeparators("56 156");
			objectiveHud.disableHorzSeparator();

			// Your score label ("SCORE")
			objectiveHud.scoreLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "SCORE";
			};
			// Your score
			objectiveHud.yourScore = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "90 16";
				visible = "1";
			};
			// Rabbit label ("RABBIT")
			objectiveHud.rabbitLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "50 16";
				visible = "1";
				text = "RABBIT";
			};
			// rabbit name
			objectiveHud.rabbitName = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 19";
				extent = "90 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.scoreLabel);
			objectiveHud.add(objectiveHud.yourScore);
			objectiveHud.add(objectiveHud.rabbitLabel);
			objectiveHud.add(objectiveHud.rabbitName);

		case SiegeGame:
			// set separators
			objectiveHud.setSeparators("96 122 177");
			objectiveHud.enableHorzSeparator();

			// Team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "90 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "90 16";
				visible = "1";
			};
			// Team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 3";
				extent = "20 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "100 19";
				extent = "20 16";
				visible = "1";
			};
			// Role label ("PROTECT" or "DESTROY")
			objectiveHud.roleLabel[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "125 3";
				extent = "50 16";
				visible = "1";
			};
			objectiveHud.roleLabel[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "125 19";
				extent = "50 16";
				visible = "1";
			};
			// number of objectives to protect/destroy
			objectiveHud.objectives[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "180 3";
				extent = "60 16";
				visible = "1";
			};
			objectiveHud.objectives[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "180 19";
				extent = "60 16";
				visible = "1";
			};

			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
				objectiveHud.add(objectiveHud.roleLabel[%i]);
				objectiveHud.add(objectiveHud.objectives[%i]);
			}

		case TeamHuntersGame:
			// set separators
			objectiveHud.setSeparators("57 83 197");
			objectiveHud.enableHorzSeparator();

			// flags label ("FLAGS")
			objectiveHud.flagLabel = new GuiTextCtrl() {
				profile = "GuiTextObjGreenLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "50 16";
				visible = "1";
				text = "FLAGS";
			};
			// number of flags
			objectiveHud.yourFlags = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "60 3";
				extent = "20 16";
				visible = "1";
			};
			// team names
			objectiveHud.teamName[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "85 3";
				extent = "110 16";
				visible = "1";
			};
			objectiveHud.teamName[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "85 19";
				extent = "110 16";
				visible = "1";
			};
			// team scores
			objectiveHud.teamScore[1] = new GuiTextCtrl() {
				profile = "GuiTextObjGreenCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "200 3";
				extent = "40 16";
				visible = "1";
			};
			objectiveHud.teamScore[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudCenterProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "200 19";
				extent = "40 16";
				visible = "1";
			};

			objectiveHud.add(objectiveHud.flagLabel);
			objectiveHud.add(objectiveHud.yourFlags);
			for(%i = 1; %i <= 2; %i++)
			{
				objectiveHud.add(objectiveHud.teamName[%i]);
				objectiveHud.add(objectiveHud.teamScore[%i]);
			}

		case SinglePlayerGame:
			// no separator lines
			objectiveHud.setSeparators("");
			objectiveHud.disableHorzSeparator();

			// two lines to print objectives
			objectiveHud.spText[1] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.spText[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.add(objectiveHud.spText[1]);
			objectiveHud.add(objectiveHud.spText[2]);

		case RPGGame:
			// no separator lines
			objectiveHud.setSeparators("");
			objectiveHud.disableHorzSeparator();

			// two lines to print objectives
			objectiveHud.spText[1] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 3";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.spText[2] = new GuiTextCtrl() {
				profile = "GuiTextObjHudLeftProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "4 19";
				extent = "235 16";
				visible = "1";
			};
			objectiveHud.add(objectiveHud.spText[1]);
			objectiveHud.add(objectiveHud.spText[2]);
	}

	chatPageDown.setVisible(false);
}