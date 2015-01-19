

//function _() { exec("scripts/rpgmath.cs"); }//temp


//(Deus 03/09/2002)
//echo(m("9999999999999999999999999999999999 + 999999999999999999999999999999999999999999999999999999999999999999999"));
//Works goods

// dividing not done yet.

//Useage:
//	m("100 + 25");	m("2 * 5");
//Working signs: + - * ^
function m(%math, %tmp) {

	// %tmp is used only by .DoMath()
	// Do not fill this in!

	if($debug) echo("m("@%math@", "@%tmp@");");//DB

	%retry = %total = %done = 0;
	while(!%done) {
		%w1 = getWord(%math, 0);
		%w2 = getWord(%math, 1);
		%w3 = getWord(%math, 2);

		if(%w2 !$= "") { // Make sure we have input

			// Just to make life easier
			if(%w1 $= "")
				%w1 = 0;
			if(%w3 $= "")
				%w3 = 0;

			// First check to see if this number will be less then a million
			if(%w2 $= "^") {
				eval("%a = (mpow("@%w1@", "@%w3@"));");
				if(%a $= "inf") {
					error("Error: m("@math@"); returns \"inf\"");
					return "inf";
				}
			}
			else
				eval("%a = ("@%w1@" "@%w2@" "@%w3@");");
			if(%a <= 999999 && %a >= -999999) { // Disable this to debug easier
				return %a;
			}

			if(%tmp !$= "tmp") {
				new ScriptObject(MathArray) {
					class = MathArray;
				};
				MathArray.num1 = %w1;
				MathArray.num2 = %w3;
			}
			MathArray.l1 = strlen(%w1);
			MathArray.l2 = strlen(%w3);
		}
		else {
			echo("Error: m("@%math@");");
			echo("Useage example: m(\"1 + 1\");");
			return;
		}

		if(MathArray.l1 > MathArray.l2)
			MathArray.max = MathArray.l1;
		else
			MathArray.max = MathArray.l2;

		%ii = MathArray.max;
		if(%w2 $= "-" && %w3 > %w1) { // Subtracting? and is the second number bigger?

			%switch = true;
			for(%i = 0; %i <= MathArray.max; %i++) {
				if(MathArray.l2-%ii >= 0) // To stop getSubStr error messages
					MathArray.Old[%ii] = getSubStr(%w3, MathArray.l2-%ii, 1);
				if(MathArray.l1-%ii >= 0)
					MathArray.New[%ii] = getSubStr(%w1, MathArray.l1-%ii, 1);

				%ii--;
			}
		}
		else {

			for(%i = 0; %i <= MathArray.max; %i++) {
				if(MathArray.l1-%ii >= 0) // To stop getSubStr error messages
					MathArray.Old[%ii] = getSubStr(%w1, MathArray.l1-%ii, 1);
				if(MathArray.l2-%ii >= 0)
					MathArray.New[%ii] = getSubStr(%w3, MathArray.l2-%ii, 1);

				%ii--;
			}
		}

		if(%w2 $= "+" || %w2 $= "-" || %w2 $= "*" || %w2 $= "^" || %w2 $= "/") {

			%done = MathArray.DoMath(""@%w2@"");

			if(!%done && %tmp !$= "tmp") {
				MathArray.delete();
			}
		}
		else {
			echo("Error: m("@%math@");");
			return;
		}

		if(%retry++ > 4) {
			error("Error in function m("@%math@"); %retry limit reached ("@%retry@").");
			return;
		}
	} //end while

	MathArray.Total[Total] = "";
	if(MathArray.Total !$= "") //Multiple uses this
		MathArray.Total[Total] = MathArray.Total;

	//==========================
	//%total = "";
	//for(%i = 1; %i <= MathArray.max; %i++) {
	//	%total = MathArray.Data[%i]@%total; // put the numbers back together
	//	//echo("total: "@%total);
	//}
	//
	//%check = %i = 0;
	//while(!%check) {
	//	if(strlen(%total) == 1)
	//		%check = 1;
	//	%num = _getSubStr(%total, %i, 1);
	//	if(%num $= 0) // Check to see if the first number is a zero
	//		//%total = getSubStr(%total, 1, 50); // 99999999999999999999999999999999999999999999999999
	//		%pt = _getSubStr(%total, 0, 50);//.....
	//	else
	//		%check = 1;
	//	%i++;
	//	%total = %opt;
	//}
	//==========================

	//Have to do it this way...
	%flag = false;
	for(%i = MathArray.max; %i > 0  && MathArray.Total $= ""; %i--) {
		MathArray.Total[%i] = MathArray.Data[%i]; // getSubStr(%total, %i, 1);
		if( (MathArray.Total[%i] $= 0 && %flag) || MathArray.Total[%i] !$= 0 ) {
			%flag = true;
			MathArray.Total[Total] = MathArray.Total[Total]@MathArray.Total[%i];//MathArray.Total[%i]@MathArray.Total[Total];
		}
	}

	%total = MathArray.Total[Total];

	if(%tmp !$= "tmp")
		MathArray.delete();	// Delete our array to free up that memory.
								// This is to insure that no old MathArray data
								// gets used next time.

	if(%switch && %total !$= "")
		%total = "-"@%total;

	if(%total $= "")
		%total = 0;

	return %total;
}

function _getSubStr(%a, %b, %c) {
	%d = getSubStr(%a, %b, %c);
	return %d;
}

//(Deus 03/09/2002)
//Do not call this function manually!
function ScriptObject::DoMath(%this, %f) {

	%check = 0;
	if(%f $= "+") {
		for(%i = 1; %i <= %this.max; %i++) {
			%check = 0;
			%this.Data[%i] = %this.Old[%i] + %this.New[%i];
		}

		%i = 0;

		while(!%check) {

			if(%this.Data[%i] > 9) {
				%this.Data[%i] -= 10;
				%this.Data[%i+1]++;
				%this.max++;
			}
			else
				%i++;

			if(%i >= %this.max) {
				%check = 1;
			}
		} //end while
	}

	else if(%f $= "-") {
		for(%i = 1; %i <= %this.max; %i++) {
			%check = 0;
			%this.Data[%i] = %this.Old[%i] - %this.New[%i];
		}

		%i = 0;

		while(!%check) {
			if(%this.Data[%i] < 0) {
				%this.Data[%i] += 10;
				%this.Data[%i+1]--;
			}
			else
				%i++;

			if(%i >= %this.max) {
				%check = 1;
			}
		} //end while
	}

	else if(%f $= "*") { // Re-wrote this part, works much better now IMO (Deus 03/10/2002)

		%this.Total = 0;

		if(%this.num1 $= 0)
			%this.Total = 0;
		else if(%this.num2 $= 0)
			%this.Total = 0;
		else {

			if(%this.num1 > %this.num2) { // This will speed up the loop if one number is smaller (Ex: 200 * 1000)
				%max = %this.num2;
				%x = %this.num1;
			}
			else {
				%max = %this.num1;
				%x = %this.num2;
			}

			for(%i = 0; %i < %max; %i++) {

				%this.Total = m(%this.Total@" + "@%x, tmp);
				if(%i == 999999) { // Make sure our %i don't get passed a million
					%max = m(%max@" - 999999", tmp);
					%i = 0;
				}
			}
		}
	}
	else if(%f $="^") { // Using high numbers here will cuz slow downs (Ex: 3 ^ 40)

		if(%this.num1 $= 0)
			%this.Total = 0;
		else if(%this.num2 $= 0)
			%this.Total = 0;
		else {

			%total = %x = %this.num1;
			%max = %this.num2;
			for(%i = 1; %i < %max; %i++) {
				%total = m(%total@" * "@%x, tmp);

				if(%i == 999999) { // Make sure our %i don't get passed a million
					%max = m(%max@" - 999999", tmp);
					%i = 0;
				}
			}

			%this.Total = %total;
		}
	}

	else if(%f $= "/") {

	}
	else
		return false;

	return true;
}

