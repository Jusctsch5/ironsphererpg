//==========================================================
//	Real Time Sim
//
// (Deus 03/11/2002)

function getRTime(%time) {

	if(strstr(strlwr(%time), "12") != -1) {
		if((%hour = $RTime::Hour) > 12) {
			%hour -= 12;
			%ext = "PM";
		}
		else
			%ext = "AM";
	}

	switch$(strlwr(%time)) {

		//24 hour types
		case "hour" or "h" : %returnTime = $RTime::Hour;

		case "min" or "m" : %returnTime = $RTime::Min;

		case "sec" or "s" : %returnTime = $RTime::Sec;

		case "day" or "d" : %returnTime = $RTime::Day;

		case "weekday" or "wd" : %returnTime = $RTime::WeekDay;

		case "month" or "mo" : %returnTime = $RTime::Month;

		case "year" or "y" : %returnTime = $RTime::Year;

		case "full" or "f" : %returnTime = $RTime::WeekDay@" "@$RTime::Month@" "@$RTime::Day@" "@$RTime::Year@" "@$RTime::Hour@":"@$RTime::Min@":"@$RTime::Sec;

		//12 hour types
		case "12" : %returnTime = %hour@":"@$RTime::Min@":"@$RTime::Sec@" "@%ext;

		case "12hour" or "12h" : %returnTime = %hour;

		case "12ext" or "12e" : %returnTime = %ext;

		case "12full" or "12f" : %returnTime = $RTime::WeekDay@" "@$RTime::Month@" "@$RTime::Day@" "@$RTime::Year@" "@%hour@":"@$RTime::Min@":"@$RTime::Sec@" "@%ext;

		//24 hour type default return (meaning if no case was found return this one)
		default : %returnTime = $RTime::Hour@":"@$RTime::Min@":"@$RTime::Sec;
	}
	return %returnTime;
}

// (Deus 03/11/2002)
function StartRealTime() {
	if(!$StartedRealTime) {
		exec("prefs/rpgTime.cs"); // $rpgTime = "Mon Mar 11 16:44:04 2002";
		$StartedRealTime = true;

		$RTime::WeekDay = getWord($rpgTime, 0);
		$RTime::Month = getWord($rpgTime, 1);
		$RTime::Day = getWord($rpgTime, 2);
		%fullTime = getWord($rpgTime, 3); // HR:MIN:SEC
		$RTime::Year = getWord($rpgTime, 4);

		// break apart %fullTime
		%fullTime = strreplace(strreplace(%fullTime, ":", " "), ":", " "); // Remove Both :
		$RTime::Hour = getWord(%fullTime, 0);
		$RTime::Min = getWord(%fullTime, 1);
		$RTime::Sec = getWord(%fullTime, 2);

		RealSimTime();
	}
}

function RealSimTime() { // DO NOT CALL THIS
	$RTime::Sec++;
	if($RTime::Sec >= 60) {
		$RTime::Sec = 0;
		$RTime::Min++;
	}
	if($RTime::Min >= 60) {
		$RTime::Min = 0;
		$RTime::Hour++;
	}
	if($RTime::Hour >= 24) {
		$RTime::Hour = 0;
		$RTime::Day++;
		$RTime::WeekDay = $RealDays[$RealDays[$RTime::WeekDay]++];
		if($RTime::WeekDay $= "NULL")
			$RTime::WeekDay = "Mon";
	}
	if($RTime::Day >= $MONTHLIMITS[$RTime::Month, $RTime::Year]) {
		$RTime::Month = "";
	}
	schedule(1000, 0, RealSimTime);
}

$RealDays[1] = "Sun";
$RealDays[2] = "Mon";
$RealDays[3] = "Tue";
$RealDays[4] = "Wed";
$RealDays[5] = "Tur";
$RealDays[6] = "Fri";
$RealDays[7] = "Sat";
$RealDays[8] = "NULL";

$RealDays["Sun"] = 1;
$RealDays["Mon"] = 2;
$RealDays["Tue"] = 3;
$RealDays["Wed"] = 4;
$RealDays["Tur"] = 5;
$RealDays["Fri"] = 6;
$RealDays["Sat"] = 7;

$RealMonths[1] = "Jan";
$RealMonths[2] = "Feb";
$RealMonths[3] = "Mar";
$RealMonths[4] = "Apr";
$RealMonths[5] = "May";
$RealMonths[6] = "Jun";
$RealMonths[7] = "Jul";
$RealMonths[8] = "Aug";
$RealMonths[9] = "Sep";
$RealMonths[10] = "Oct";
$RealMonths[11] = "Nov";
$RealMonths[12] = "Dec";

$MONTHLIMITS[Feb, 2002] = 28;
$MONTHLIMITS[Mar, 2002] = 31;
$MONTHLIMITS[Apr, 2002] = 30;
$MONTHLIMITS[May, 2002] = 31;
$MONTHLIMITS[Jun, 2002] = 30;
$MONTHLIMITS[Jul, 2002] = 31;
$MONTHLIMITS[Aug, 2002] = 30;
$MONTHLIMITS[Sep, 2002] = 31;
$MONTHLIMITS[Oct, 2002] = 30;
$MONTHLIMITS[Nov, 2002] = 31;
$MONTHLIMITS[Dec, 2002] = 30;

$MONTHLIMITS[Jan, 2003] = 31;
$MONTHLIMITS[Feb, 2003] = 28;
$MONTHLIMITS[Mar, 2003] = 31;
$MONTHLIMITS[Apr, 2003] = 30;
$MONTHLIMITS[May, 2003] = 31;
$MONTHLIMITS[Jun, 2003] = 30;
$MONTHLIMITS[Jul, 2003] = 31;
$MONTHLIMITS[Aug, 2003] = 30;
$MONTHLIMITS[Sep, 2003] = 31;
$MONTHLIMITS[Oct, 2003] = 30;
$MONTHLIMITS[Nov, 2003] = 31;
$MONTHLIMITS[Dec, 2003] = 30;

$MONTHLIMITS[Jan, 2004] = 31;
$MONTHLIMITS[Feb, 2004] = 29;
$MONTHLIMITS[Mar, 2004] = 31;
$MONTHLIMITS[Apr, 2004] = 30;
$MONTHLIMITS[May, 2004] = 31;
$MONTHLIMITS[Jun, 2004] = 30;
$MONTHLIMITS[Jul, 2004] = 31;
$MONTHLIMITS[Aug, 2004] = 30;
$MONTHLIMITS[Sep, 2004] = 31;
$MONTHLIMITS[Oct, 2004] = 30;
$MONTHLIMITS[Nov, 2004] = 31;
$MONTHLIMITS[Dec, 2004] = 30;

$MONTHLIMITS[Jan, 2005] = 31;
$MONTHLIMITS[Feb, 2005] = 28;
$MONTHLIMITS[Mar, 2005] = 31;
$MONTHLIMITS[Apr, 2005] = 30;
$MONTHLIMITS[May, 2005] = 31;
$MONTHLIMITS[Jun, 2005] = 30;
$MONTHLIMITS[Jul, 2005] = 31;
$MONTHLIMITS[Aug, 2005] = 30;
$MONTHLIMITS[Sep, 2005] = 31;
$MONTHLIMITS[Oct, 2005] = 30;
$MONTHLIMITS[Nov, 2005] = 31;
$MONTHLIMITS[Dec, 2005] = 30;

//==========================================================