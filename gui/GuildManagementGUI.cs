if(!isobject(GuildManagementGUI))
exec("gui/GuildManagementGUI.gui");
function OpenGuildManagementGUI()
{
	Canvas.PushDialog(GuildManagementGUI);
}
function GuildManagementGUI::OnWake(%this)
{
	ClientCMDClearManagementGUI();
	OM_RosterList.clear();
	OM_RosterList.clearColumns();
	OM_RosterList.addColumn( 0, "Player", 250, 50, 200 );
	OM_RosterList.addColumn( 1, "Rank", 250, 25, 200  );
	GM_TI_List.clear();
	GM_TI_List.clearColumns();
	GM_TI_List.addColumn(0, "Territory", 250, 50, 200);
	GM_TI_List.addColumn(1, "Challenged", 250, 25, 200);
	
	OM_RosterList.setSortColumn(0);
	GM_Information.setValue(false);

	GM_Roster.setValue(false);
	GM_PlayerInfobtn.setValue(false);
	GM_TerritoryInfobtn.setValue(false);
	
	GM_RosterPane.setVisible(false);
	GM_InformationPane.setVisible(false);
	GM_PlayerInfobtnPane.setVisible(false);
	GM_TerritoryInfobtnPane.setVisible(false);

	commandToServer('GetGuildInfo');

	CommandToServer('GuildPlayerInfo');
	
	CommandToServer('GuildGetTerritoryInfo');
	%this.setPane("Roster");

}

function GuildManagementGUI::SetPane(%this, %pane)
{
	if(%this.pane !$= "")
	{
		%oldpane = "GM_" @ %this.pane @ "Pane";
		%oldbtn = "GM_" @ %this.pane;
		%oldpane.setVisible(false);
		%oldbtn.setValue(false);
	}
	%newpane = "GM_" @ %pane @ "Pane";
	%newpane.setVisible(true);
	%newbtn = "GM_" @ %pane;
	%newbtn.setValue(true);
	

	%this.pane = %pane;
}

function GuildManagementGUI::onSleep(%this)
{

}

function GuildManagementGUI::KickPlayer(%this)
{
	%guid = OM_RosterList.getSelectedID();
	CommandToServer('GuildKick', %guid);
}
function GuildManagementGUI::ViewPlayerInfo(%this)
{
	%guid = OM_RosterList.getSelectedID();
	CommandToServer('GuildPlayerInfo', %guid);
	%this.setPane("PlayerInfobtn");
}
function GuildManagementGUI::ViewMyInfo(%this)
{
	CommandToServer('GuildPlayerInfo');
}
function GuildManagementGUI::editProfile(%this)
{
	%guid = OM_RosterList.getSelectedID();
	GM_EditPlayerRankList.clear();
	commandToServer('PopulateGuildRanks');
	GM_EditProfileDialog.setVisible(true);
}
function GuildManagementGUI::submitEditProfile(%this)
{
	%guid = OM_RosterList.getSelectedID();
	%rank = GM_EditPLayerRankList.getSelected();
	
	commandtoServer('submitEditProfile', %guid, %rank);
	CommandToServer('GuildPlayerInfo', %guid);
	GM_EditProfileDialog.setVisible(false);
	
}
function GuildManagementGUI::editInformation(%this)
{
	GM_GUI_EditInformationPane.setVisible(true);
	GM_GUI_GUIInformationText.setValue(GM_Info_Text.getValue());
}
function GuildManagementGUI::AcceptInformationChange(%this)
{
	GM_GUI_EditInformationPane.setVisible(false);
	//send off new data to server for verification.
	%text = GM_GUI_GUIInformationText.getValue();
	commandToServer( 'ChangeGuildInfo', getsubStr(%text, 0, 255), getsubstr(%text, 255, 255), getsubstr(%text, 255*2, 255), getsubstr(%text, 255*3, 255), getsubstr(%text, 255*4, 255), getsubstr(%text, 255*5, 255), getsubstr(%text, 255*6, 255), getsubstr(%text, 255*7, 255) ,getsubstr(%text, 255*8, 255), getsubstr(%text, 255*9, 255));
}
function GuildManagementGUI::EditShortDescription(%this)
{
	GM_GUI_EditShortDescription.setVisible(true);
}
function GuildManagementGUI::AcceptShortDescription(%this)
{
	%text = GM_GUI_ShortDescription_Text.getValue();
	commandToServer('changeGuildShortDesc', %text);
	GM_GUI_EditShortDescription.setVisible(false);
}
function ClientCMDReceiveGuildDescText(%text)
{
	GM_GUI_ShortDescription_Text.setValue(%text);
}
function ClientCMDReceiveInformationText(%Part1, %part2, %part3, %part4, %part5, %part6, %part7, %part8, %part9, %part10)
{
	%text = %part1 @ %part2 @ %part3 @ %part4 @ %part5 @ %part6 @ %part7 @ %part8 @ %part9 @ %part10;
	GM_Info_Text.setValue(%text);
}
function ClientCMDAddEditPlayerRank(%id, %name)
{
	GM_EditPlayerRankList.add(%name, %id);
}
function ClientCMDEditPlayerRankDone()
{
	
	
	CommandToServer('getPlayerRank', OM_RosterList.getSelectedID());
}
function ClientCMDFinishPlayerRank(%id)
{
	

	GM_EditPlayerRankList.setSelected( %id) ;
}
function ClientCMDClearManagementGUI()
{
	OM_RosterList.clear();
	GM_MLText_PlayerInfo.setText("");
}

function ClientCMDaddPlayerToGuildRoster(%guid, %name, %rank)
{
	OM_RosterList.addRow(%guid, %name TAB %rank);
}
function ClientCMDsetGuildPlayerInfo(%name, %rank)
{
	%red = "<COLOR:FF0000>";
	%green = "<COLOR:00FF00>";
	%blue = "<COLOR:0000FF>";
	%text = %blue @ "Name:" SPC %red @ %name NL
	%blue @ "Rank:" SPC %red @ %rank;
	GM_MLText_PlayerInfo.setText(%text);
}
function ClientCMDGuildAddTerritory(%index, %zone, %challenged)
{
	
	if(%challenged)
	%chal = "yes";
	else
	%chal = "no";
	GM_TI_List.addRow(%index, %zone TAB %chal);
}
function ClientCMDGuildTerritoryDone()
{
	return;
}