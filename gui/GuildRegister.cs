if(!isObject(GuildRegister))
exec("gui/guildRegister.gui");


function ClientCmdOpenGuildGUI()
{
	Canvas.pushDialog(GuildRegister);
}

function GuildRegJoin::OnClick(%this)
{
	JoinGuildPane.setVisible(true);
	%id = GuildRegList.getSelectedID();
	%text = GuildRegList.getRowTextById(%id);
	GuildNameSureJoin.setText(GetField(%text, 0));
}
function GuildRegJoinNo::OnClick(%this)
{
	JoinGuildPane.setVisible(false);
}
function GuildRegJoinYes::OnClick(%this)
{
	//tell the server we are trying to join this guild
	JoinGuildPane.setVisible(false);
	CommandToServer('JoinGuild',  GuildRegList.getSelectedID());
}
function GuildRegNew::OnClick(%this)
{
	CreateGuildPane.setVisible(true);
}
function GuildRegCreateNo::onClick(%this)
{
	CreateGuildPane.setVisible(false);
}
function GuildRegCreateYes::onClick(%this)
{
	CreateGuildPane.setVisible(false);
	%name = namedguild.getValue();
	echo(%name);
	CommandToServer('CreateGuild', %name);
}

function GuildRegister::onWake(%this)
{
	GuildRegList.setup();
}

function GuildRegister::onSleep(%this)
{

}

function GuildRegList::setup(%this)
{
	%this.clear();
	%this.clearColumns();
	%this.addColumn( 0, "Name", 200, 50, 200 );
	%this.addColumn( 1, "Owner", 120, 25, 200 );
	%this.addColumn( 2, "Description", 500, 25, 200 );
	%this.setSortColumn(0);
	CommandToServer('GetGuildList');
}
function ClientCmdstartGuildLoad()
{
	GuildRegList.clear();
}

function ClientCMDAddGuildLine(%guildid, %guildname, %guildowner, %guildDesc)
{
	
	

	%text = %guildName TAB %guildowner TAB %guildDesc;
	

	GuildRegList.addRow(%guildid, %text);
}

function ClientCMDEndGuildLoad()
{
	//GuildRegList.sort();
}