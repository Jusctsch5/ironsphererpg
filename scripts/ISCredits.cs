//credits

function ISC_setDefaultCredits()
{
     deleteVariables("$ISCredits*");
     $ISCreditsCount = 0;

     ISC_addSubHeading("<just:center>Third Team");

     ISC_addSubHeading("<just:left> Project Leads/Coordinators");
     ISC_addPerson("Shinji");

     ISC_addSubHeading("Co-Project Lead");
     
     ISC_addSubHeading("Scripting");
     ISC_addPerson("Shinji");
     
     ISC_addSubHeading("Artists");
     
     ISC_addSubHeading("Mapping");
     ISC_addPerson("Shinji");
     
     ISC_addSubHeading("Audio");
     ISC_addPerson("Shinji");
     
     ISC_addSubHeading("Testers");
     ISC_addPerson("kmjn");
     ISC_addPerson("Minehem");
     
     
     
     
     ISC_addSubHeading("<just:center>Second Team");
     
    ISC_addSubHeading("<just:left>Project Leads");
    ISC_addPerson("Shinji");
    ISC_addPerson("Thalagyrt");

    ISC_addSubHeading("Co-Project Lead");
    ISC_addPerson("Phantom139");
    
    ISC_addSubHeading("Scripting");
    ISC_addPerson("KeyboardCat (Signal360)");
    ISC_addPerson("Shinji");
    ISC_addPerson("Thalagyrt");
    ISC_addPerson("Tricon2 Elf");
    ISC_addPerson("Phantom139");
    ISC_addPerson("DarkDragonDX (Nyoka)");
    
    ISC_addSubHeading("Artists");
    ISC_addPerson("Zaxxman - Modeller");
    ISC_addPerson("Tricon2 Elf - Textures");
    ISC_addPerson("GADGET44 - Textures");
    
    ISC_addSubHeading("Mapping");
    ISC_addPerson("Shinji");
    
    ISC_addSubHeading("Testers");
    ISC_addPerson("Castiger");
    ISC_addPerson("ChuckConnors");
    ISC_addPerson("Chutriel");
    ISC_addPerson("Damaso");
    ISC_addPerson("Drakethesly");
    ISC_addPerson("FURB");
    ISC_addPerson("Iplop");
    ISC_addPerson("LLJKH");
    ISC_addPerson("m31ster");
    ISC_addPerson("MightyZuex");
    ISC_addPerson("Noodude");
	ISC_addPerson("Zipperdude");
    
    
    
    ISC_addSubHeading("<just:center>First Team");
    
    ISC_addSubHeading("Original Team");
    ISC_addPerson("Goodie - WorldCraft Modeller");
    ISC_addPerson("Gul'Dar - WorldCraft Modeller and Mapper");
    ISC_addPerson("HiVoltage - Modeller");
    ISC_addPerson("JeremyIrons - Original Creator");
    ISC_addPerson("Lone Predator - Skinner");
    ISC_addPerson("Scourage - Coder");
    ISC_addPerson("SoulSlayer - WorldCraft Modeller");
    ISC_addPerson("Trident_RX - Head Coder, Creator");
    ISC_addPerson("Toaster - Documentation");
    ISC_addPerson("Twister - WorldCraft Modeller");
    ISC_addPerson("Fina - Head Mapper, Worldcraft Modeller");
    ISC_addPerson("Sirsteven - Lead Tester");
    ISC_addPerson("Jardin De Cecile - Music Score");
}

function ISC_addSubHeading(%str)
{
    $ISCredits[$ISCreditsCount++] = "<font:Verdana Bold:22>" @ %str;
    $ISCreditsCount++;
}
function ISC_addPerson(%str)
{
    $ISCredits[$ISCreditsCount] = "<font:Verdana:18>" @ %str;
    $ISCreditsCount++;
}

function ISCredits::onWake(%this)
{
    ISC_setDefaultCredits();
    ISCredits_Text.setValue("");
    for (%i = 0; %i < $ISCreditsCount; %i++)
        ISCredits_Text.addText($ISCredits[%i] @ "\n", false);
}
loadGui("ISCredits");
