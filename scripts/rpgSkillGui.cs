
if(isObject(SkillsTreeList))
	SkillsTreeList.delete();

new SimSet(SkillsTreeList);

function BuildSkillsMenu() {

	SkillsTreeList.clear();

	if(isObject($RootSkillMenu))
		$RootSkillMenu.delete();

	$RootSkillMenu = new ActionMap();
	SkillsTreeList.add($RootSkillMenu);
	$CurrentSkillMenu = $RootSkillMenu;
	$CurrentSkillMenu.optionCount = 0;

	rpgBuildSkills();

}

function SkillTree(%skillName, %amount) {

	if(%amount >= 1) {
		%menu = new ActionMap();
		SkillsTreeList.add(%menu);
		%cm = $CurrentSkillMenu;

		%cm.option[%cm.optionCount] = %skillName@": \c0"@mfloor(%amount); // \c3 makes black
		%cm.command[%cm.optionCount] = %menu;
		%cm.isMenu[%cm.optionCount] = 1;
		%cm.optionCount++;
		%menu.parent = %cm;

		%menu.optionCount = 0;
		$CurrentSkillMenu = %menu;
	}
}

function SkillNode(%skillName, %amount) {

	if(%amount >= 1) {
		%cm = $CurrentSkillMenu;
		%cm.option[%cm.optionCount] = %skillName@": \c0"@mfloor(%amount);

		%cm.isMenu[%cm.optionCount] = 0;
		%cm.optionCount++;
	}
}

function endSkillTree() {
	$CurrentSkillMenu = $CurrentSkillMenu.parent;
}

// Complex Skills Tree --------------------------------------------------

function rpgBuildSkills() {

//NOTE:
// The real tree should look something like this
//
//				Skill Name				Clients Skill amount
//SkillTree($Skill[<some numbers>], $ClientSkill[<some numbers>]);


// JUST A DEMO OF HOW THE TREE WORKS
// Mind =========================================== Mind
SkillTree("Mind", 5);

	SkillTree("Intelligence", 2);
		SkillTree("Tracking", 1);
		endSkillTree();
	endSkillTree();

	SkillTree("Endurance", 4);
		SkillTree("Ranged", 1);
		endSkillTree();
	endSkillTree();

endSkillTree();

// Body =========================================== Body
SkillTree("Body", 11);

	SkillTree("Dexterity", 6);
		SkillTree("SwordPlay", 2);
			SkillTree("Dodging", 1);
			endSkillTree();
		endSkillTree();
	endSkillTree();

	SkillTree("Strength", 10);
		SkillTree("Bashing", 3);
		endSkillTree();
	endSkillTree();

endSkillTree();

// Soul =========================================== Soul
SkillTree("Soul", 4);

	SkillTree("Energy", 2);
		SkillTree("Flexibility", 1);
		endSkillTree();
	endSkillTree();

	SkillTree("Mystic", 4);
		SkillTree("Stamina", 3);
		endSkillTree();
	endSkillTree();

endSkillTree();

}
//---------------------------------------------------------------------------------

BuildSkillsMenu(); // This builds the tree, DisplayTree displays the tree

function rpgSkillsGui::onWake(%this) {
	%this.fillSkillsMenuTree();
}

function rpgSkillsGui::onSleep(%this) {
	SkillsGuiTree.clear();
	SkillsGuiTree.LastClick = "";
}

function rpgSkillsGui::DisplayTree() {
	SkillsGuiTree.clear();
	%guiRoot = SkillsGuiTree.getFirstRootItem();

	traverseSkillsMenu(%guiRoot, $RootSkillMenu);

	if(SkillsGuiTree.LastClick !$= "") {
		SkillsGuiTree.expandItem(SkillsGuiTree.LastClick);
		SkillsGuiTree.selectItem(SkillsGuiTree.LastClick);
	}
}

function traverseSkillsMenu(%guiID, %menu) {

	for(%i = 0; %i < %menu.optionCount; %i++) {
		%text = %menu.option[%i];

		if(%menu.isMenu[%i]) {
			%newGuiID = SkillsGuiTree.insertItem(%guiID, %text, 0);
			traverseSkillsMenu(%newGuiID, %menu.command[%i]);
		}
		else {
			 SkillsGuiTree.insertItem(%guiID, %text, 0);
		}
	}
}

//MessageBoxOK("NEW SKILL", "You have gained a new skill in ...");

function SkillsGuiTree::onRightMouseDown(%this, %item, %pos) {

	SkillsGuiTree.selectItem(%item);
	SkillMenuItemActionPopup.awaken(%item, %pos);
}

function SkillMenuItemActionPopup::awaken(%this, %item, %pos) {

	%this.position = %pos;
	%this.clear();

	%treeRoot = SkillsGuiTree.getFirstRootItem();
	%text = SkillsGuiTree.getItemText(%item);

	%skill = getSubStr(%text, 0, strstr(%text, ":"));
	%this.addEntry("  Skill: "@%skill, 0);
	%this.addEntry(" ", 1);
	%this.addEntry("Information:", 3);
	%this.addEntry($Skill::Info[%skill], 4);

	Canvas.pushDialog(SkillMenuItemActionDlg);
	%this.forceOnAction();
}


function SkillMenuItemActionPopup::addEntry(%this, %text, %id) {
	%this.add(%text, %id);
}

function SkillMenuItemActionPopup::reset(%this) {
	%this.forceClose();
	Canvas.popDialog(SkillMenuItemActionDlg);
}


function SkillMenuItemActionPopup::onSelect(%this, %id, %text) {

	//%item = SkillsGuiTree.getSelectedItem();
	%this.reset();
}


function ChatMenuItemActionPopup::onCancel(%this) {
	%this.reset();
}
