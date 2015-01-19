//RPG Quick Cast Gui Scripts
//Handles population and select functions for the quick cast gui

if(!isObject(RPGQuickCastGui))
   exec("gui/RPGQuickCastGui.gui");
//execute the quick perfs
exec("prefs/RPGQuickBinds.cs");

//Globals
//$primarySkillSet -> Lists all the avaliable primary skills
$primarySkillSet[0] = "Offensive Casting";
$primarySkillSet[1] = "Defensive Casting";
//$primarySkillSet[2] = "Neutral Casting";   //really nothing good to use in here
$primarySkillSet[2] = "Stealing";
$primarySkillSet[3] = "Sense Heading";
$primarySkillSet[4] = "Backstabbing";
$primarySkillSet[5] = "IgniteArrow";
$primarySkillSet[6] = "Focus";
$primarySkillSet[7] = "Hiding";
$primarySkillSet[8] = "Bashing";
$primarySkillSet[9] = "Cleaving";

//Functions


//These are our two basic open/close functions
function RPGQC_OpenGUI() {
   Canvas.PushDialog(RPGQuickCastGui);
}

function RPGQC_CloseGUI() {
   Canvas.PopDialog(RPGQuickCastGui);
}
//
function RPGQuickCastGui::onWake(%this) {
   //populate primary skill set
   %p = 0;
   RQC_PSkillList.clear();
   RQC_SlotList.clear();
   while(isSet($primarySkillSet[%p])) {
      RQC_PSkillList.addRow(%p, $primarySkillSet[%p]);
      %p++;
   }
   //populate quick slots
   for(%i = 0; %i < 10; %i++) {
      RQC_SlotList.addRow(%i, "Quick Bind "@%i+1@"");
   }
   //
   $RPGQCs::UpdateLoop = %this.update();
}

function UpdateSecondaryList(%primary) {
   %i = 0;
   RQC_SSkillList.clear();
   while(isSet($RPGQCs::SkillSetOpen[%primary, %i])) {
      RQC_SSkillList.addRow(%i, $RPGQCs::SkillSetOpen[%primary, %i]);
      %i++;
   }
}

function RPGQuickCastGui::onSleep(%this) {
   cancel($RPGQCs::UpdateLoop);
}

function RPGQuickCastGui::update(%this) {
   CommandToServer('GatherOpenSkills');
   $RPGQCs::UpdateLoop = %this.schedule(5000, "Update");   //update open skills from server
}
//

function clientCmdReturnOpenSkills(%primary, %sList) {
   //primary comes back at us in a nice text block
   //sList contains tabbed avaliable secondary skills
   %fields = getFieldCount(%sList);
   for(%i = 0; %i < %fields; %i++) {
      $RPGQCs::SkillSetOpen[%primary, %i] = getField(%sList, %i);
   }
}

//we use our saved binds on the server (usage of selectWeaponSlot implementation)
function RPGQC_UseBind1() {
   commandToServer( 'selectWeaponSlot', 0 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind2() {
   commandToServer( 'selectWeaponSlot', 1 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind3() {
   commandToServer( 'selectWeaponSlot', 2 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind4() {
   commandToServer( 'selectWeaponSlot', 3 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind5() {
   commandToServer( 'selectWeaponSlot', 4 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind6() {
   commandToServer( 'selectWeaponSlot', 5 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind7() {
   commandToServer( 'selectWeaponSlot', 6 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind8() {
   commandToServer( 'selectWeaponSlot', 7 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind9() {
   commandToServer( 'selectWeaponSlot', 8 );
   RPGQC_CloseGUI();
}

function RPGQC_UseBind10() {
   commandToServer( 'selectWeaponSlot', 9 );
   RPGQC_CloseGUI();
}

function RPGQC_BindToSlot() {
   // Gather the current details of the selected slot
   %primary = RQC_getPHA();
   %secondary = RQC_getSHA();
   %slot = RQC_getSelectedSlot();
   // Fix the data to read as slot notation
   %slotNumber = strReplace(%slot, "Quick Bind ", "");
   //
   %fixedData = "#"; //start with the use symbol #
   if(strstr(%primary, "Casting") != -1) {
      //it's a spell!
      %fixedData = %fixedData @ "cast";
   }
   %fixedData = %fixedData SPC %secondary;
   //Set the data using #quickbind # #data
   %out = "#quickbind "@%slotNumber@" "@%fixedData@"";
   RPGQC_chat(%out);
}

function RPGQC_UseAbility() {
   %primary = RQC_getPHA();
   %secondary = RQC_getSHA();
   // Fix the data to read as near slot notation
   %fixedData = "#"; //start with the use symbol #
   if(strstr(%primary, "Casting") != -1) {
      //it's a spell!
      %fixedData = %fixedData @ "cast";
   }
   %fixedData = %fixedData SPC %secondary;
   // Speak the command as a normal chat bind
   echo(%fixedData);
   RPGQC_chat(%fixedData);
}
//asset functions
function RPGQC_chat(%message) {
   commandtoserver('messagesent', %message);
   RPGQC_CloseGUI();
}

//RPGQCs does not save, it is the locally stored versions
function RQC_getPHA() {
   return $RPGQCs::PrimaryAbilityStore;
}

function RQC_getSHA() {
   return $RPGQCs::SecondaryAbilityStore;
}

function RQC_getSelectedSlot() {
   return $RPGQCs::SelectedSlot;
}

function RQC_SlotList::onSelect(%this, %id, %text) {
	//Client-side
	RQC_SlotList.SelectText = %text;
    $RPGQCs::SelectedSlot = %text;
}

function RQC_PSkillList::onSelect(%this, %id, %text) {
	//Client-side
	RQC_PSkillList.SelectText = %text;
    $RPGQCs::PrimaryAbilityStore = %text;
    //
    UpdateSecondaryList(%text);
}

function RQC_SSkillList::onSelect(%this, %id, %text) {
	//Client-side
	RQC_SSkillList.SelectText = %text;
    $RPGQCs::SecondaryAbilityStore = %text;
}
