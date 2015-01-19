//shinji's tea party
//rpggame.cs calls this file in its exec list

SetPerfCounterEnable(0); //server stutter fix

//admin passwords
//CUSTOM
$AdminPassword[1]=forumwarrior;
$AdminPassword[2]=applecalculator;
$AdminPassword[3]=lampsphere;
$AdminPassword[4]=boiledbeef;
$AdminPassword[5]=babybackbunny;

//dbgsetparameters(28000,lolwut);

$pref::Net::PacketRateToClient = "20"; //determines how many packets per second sent to each client
$pref::Net::PacketRateToServer = "32"; //may determine how many packets are allowed from each client
$pref::Net::PacketSize = "256"; //size of each packet sent to each client, maximum.has no effect on size of packets client send to the server


setlogmode(1); //leave this set to zero unless you are coding and need a log it will make a huge file...!!!

$logechoenabled=1; //set to 1 you can now see game details in console.  Thanks to tubaguy.
setLogMode(1); //console output to .\GameData
//telnetsetparameters(1338,ReadWritePassword,ReadOnlyPassword);  //telnet fix (i think!)





//-nopure - this makes it so your server isn't "pure" (no calls to eval) and some other stuff (not entirely sure).
//-clientprefs path - specifies while clientprefs file to use, default is prefs/ClientPrefs.cs.
//-serverprefs path - specifies while serverprefs file to use, default is prefs/ServerPrefs.cs.
//-host - makes a listen server?
//-password arg - automatically puts in your password to log in (may not be compatible with TribesNext patch).
//-bot arg - specifies how many bots there should be.

//Some Others
//-navBuild (build a navigation graph), -spnBuild (builds a spawn graph), -prepBuild (prepares the server and compiles all the scripts), -quit (quits the game immediately), -demo (puts the game in demo mode?)

//There are all lot of others, however, I'm not sure what a few do and if you'd find them useful or not (I'm not even sure about a few above).
