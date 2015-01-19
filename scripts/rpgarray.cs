//======================================= ARRAYS ======================================
// Written by JeremyIrons for IronSphere
// March 07 2002
// -Requires an isnumeric function
//=====================================================================================
//
// Tribes2 CS code does not properly support arrays, and so I've coded a "hack" to store data
// in SimSets and emulating array functionality.
// Every support function supports the key => value system (see PHP), but when specifying keys
// and values, the => must be an argument by itself enclosed by quotes.
//
// Example:
// array("test", "hello", "there", "mykey", "=>", "myvalue", "everyone");
//
// This will create the global variable $test and the equivalent in PHP would be:
// $test = array("hello", "there", "mykey" => "myvalue", "everyone");
//
// For a hack, it's not that far off.  I've written a bunch of useful support functions, which
// are listed below.  They attempt to emulate their PHP counterparts.  For more details on their
// functionality, visit http://www.php.net/
//
// To access the value of an array, you must access the array as if it were an object. The
// parameter will be the key with an underscore in front of it:
//
// Example:
// array("test", "hello", "there", "mykey", "=>", "myvalue", "everyone");
// echo($test._0);
// echo($test._1);
// echo($test._mykey);
// echo($test._2);
//
// will display:
//
// hello
// there
// myvalue
// everyone
//
// Unfortunately we can't place a variable in the place of _0, _1, etc. such as $test.%k
// Instead you will need to use the array_get_value_at function, like in this example:
//
// array("myarray", "hello", "there", "mykey", "=>", "myvalue", "everyone");
// %k = "mykey";
// echo($myarray.array_get_value_at(%k));
//
// Notice that you did not need to specify the underscore.

//=====================================================================================
// SUPPORT FUNCTIONS:
//=====================================================================================
//
// array(%arrayname, %arg1, %arg2, %arg3, etc)
// explode(%arrayname, %string, %delimiter, optional %limit)
// %x = array.implode(%glue);
// split = explode
// join = implode
// array_reset()
// array_end()
// array_next()
// array_prev()
// array_current()
// array_count()
// array_sizeof() = array_count
// is_array()
// array_get_key()
// array_get_value()
// array_get_key_at_index(%index)
// array_get_value_at_index(%index)
// array_get_value_at(%key)
// array_key_exists(%key)
// in_array(%value)
// array_search(%value)
// array_pop(%newarrayname)
// array_push(%newarrayname, %arg1, %arg2, %arg3, etc)
// array_shift(%newarrayname)
// array_unshift(%newarrayname, %arg1, %arg2, %arg3, etc)
// array_sum()
//
//=====================================================================================

$array_debug = true;

//%a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50
function array(%arrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	_array(%arrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50);
}

function _array(%arrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	%key = "";
	%j = 0;
	%count = 0;	//indicates the actual amount of key => value combinations

	eval("if(isObject($" @ %arrname @ ")){$" @ %arrname @ ".delete();}");

	%sb = "$" @ %arrname @ " = new SimSet(){";

	for(%i = 0; %i <= 50; %i++)
	{
		eval("%arg = %a" @ %i @ ";");
		eval("%nextarg = %a" @ (%i+1) @ ";");

		if(%arg $= "")
			break;

		if(%nextarg $= "=>")
		{
			%key = %arg;
			%i++;
		}
		else
		{
			if(%key !$= "" && !isnumeric(%key))
			{
				%k = "_" @ %key;
			}
			else
			{
				if(%key !$= "")
					%j = %key;

				%k = "_" @ %j;
				%j++;
			}
			%key = "";

			%sb = %sb @ %k @ " = \"" @ %arg @ "\";";
			%sb = %sb @ "__internal_key" @ %count @ " = \"" @ %k @ "\";";
			%sb = %sb @ "__internal_value" @ %count @ " = \"" @ %arg @ "\";";
			%count++;
		}
	}

	%sb = %sb @ "__internal_pointer = 0;";
	%sb = %sb @ "__internal_count = " @ (%count-1) @ ";";
	%sb = %sb @ "};";

	if($array_debug) echo(%sb);
	eval(%sb);
}

function SimSet::implode(%this, %glue)
{
	%flag = 0;
	%g = "";

	%this.array_reset();
	while((%val = %this.array_get_value()) !$= false)
	{
		if(%flag $= 1)
			%g = %glue;
		%flag++;

		%stick = %stick @ %g @ %val;
	}

	return %stick;
}
function explode(%arrname, %string, %delimiter, %limit)
{
	%sb = "array(\"" @ %arrname @ "\"";

	%tstr = %string @ %delimiter;
	%lpos = 0;
	while((%pos = strpos(%tstr, %delimiter)) !$= -1)
	{
		%chunk = getsubstr(%tstr, %lpos, (%pos - %lpos));
		%tstr = getsubstr(%tstr, %pos + strlen(%delimiter), 999999);

		%sb = %sb @ ", \"" @ %chunk @ "\"";

		if(%limit !$= "")
		{
			%count++;
			if(%count >= %limit)
				break;
		}
	}

	%sb = %sb @ ");";

	eval(%sb);
}

function SimSet::join(%this, %glue)
{
	//same as implode
	return %this.implode(%glue);
}
function split(%newarrname, %string, %delimiter, %limit)
{
	//same as explode
	explode(%newarrname, %string, %delimiter, %limit);
}

function SimSet::array_reset(%this)
{
	%this.__internal_pointer = 0;

	return true;
}
function SimSet::array_end(%this)
{
	%this.__internal_pointer = %this.__internal_count;

	return true;
}
function SimSet::array_next(%this)
{
	if(%this.__internal_pointer <= %this.__internal_count)
	{
		%this.__internal_pointer++;
		return true;
	}
	else
		return false;
}
function SimSet::array_prev(%this)
{
	if(%this.__internal_pointer > 0)
	{
		%this.__internal_pointer--;
		return true;
	}
	else
		return false;
}
function SimSet::array_current(%this)
{
	return %this.__internal_pointer;
}
function SimSet::array_count(%this)
{
	return %this.__internal_count;
}
function SimSet::array_sizeof(%this)
{
	//same as array_count
	return %this.array_count();
}
function SimSet::is_array(%this)
{
	if(%this.__internal_count !$= "")
		return true;
	else
		return false;
}

function SimSet::array_get_key(%this)
{
	if(%this.__internal_pointer > %this.__internal_count)
		return false;

	//gets the key at the current array pointer and moves the pointer forward
	%key = %this.array_get_key_at_index(%this.__internal_pointer);
	%this.array_next();

	return %key;
}
function SimSet::array_get_value(%this)
{
	if(%this.__internal_pointer > %this.__internal_count)
		return false;

	//gets the value at the current array pointer and moves the pointer forward
	%val = %this.array_get_value_at_index(%this.__internal_pointer);
	%this.array_next();

	return %val;
}
function SimSet::array_get_key_at_index(%this, %index)
{
	if(%index > %this.__internal_count)
		return false;

	eval("%key = %this.__internal_key" @ %index @ ";");
	return getsubstr(%key, 1, 999999);
}
function SimSet::array_get_value_at_index(%this, %index)
{
	if(%index > %this.__internal_count)
		return false;

	eval("%val = %this.__internal_value" @ %index @ ";");
	return %val;
}
function SimSet::array_get_value_at(%this, %key)
{
	eval("%val = %this._" @ %key @ ";");
	return %val;
}

function SimSet::array_key_exists(%this, %keyfind)
{
	for(%i = 0; %i <= %this.__internal_count; %i++)
	{
		%key = %this.array_get_key_at_index(%i);
		if(%key $= %keyfind)
			return true;
	}
	return false;
}
function SimSet::in_array(%this, %valfind)
{
	for(%i = 0; %i <= %this.__internal_count; %i++)
	{
		%val = %this.array_get_value_at_index(%i);
		if(%val $= %valfind)
			return true;
	}
	return false;
}
function SimSet::array_search(%this, %valfind)
{
	for(%i = 0; %i <= %this.__internal_count; %i++)
	{
		%val = %this.array_get_value_at_index(%i);
		if(%val $= %valfind)
		{
			%key = %this.array_get_key_at_index(%i);
			return %key;
		}
	}
	return false;
}

//WARNING: echo'ing the result of this function crashes T2.  No idea why.
function SimSet::array_pop(%this, %newarrname)
{
	%ret = %this.array_get_value_at_index(%this.__internal_count);

	%sb = "array(\"" @ %newarrname @ "\"";

	//rebuild array with the last element removed
	for(%i = 0; %i < %this.__internal_count; %i++)
	{
		%key = %this.array_get_key_at_index(%i);
		%value = %this.array_get_value_at_index(%i);

		%sb = %sb @ ", \"" @ %key @ "\", \"=>\", \"" @ %value @ "\"";
	}

	%sb = %sb @ ");";

	if($array_debug) echo(%sb);
	eval(%sb);

	return %ret;
}

function SimSet::array_push(%this, %newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	%this._array_push(%newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50);
}
function SimSet::_array_push(%this, %newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	%sb = "array(\"" @ %newarrname @ "\"";

	//get contents of current array
	for(%i = 0; %i < %this.__internal_count; %i++)
	{
		%key = %this.array_get_key_at_index(%i);
		%value = %this.array_get_value_at_index(%i);

		%sb = %sb @ ", \"" @ %key @ "\", \"=>\", \"" @ %value @ "\"";
	}

	%j = 0;
	%key = "";
	for(%i = 0; %i <= 50; %i++)
	{
		eval("%arg = %a" @ %i @ ";");
		eval("%nextarg = %a" @ (%i+1) @ ";");

		if(%arg $= "")
			break;

		if(%nextarg $= "=>")
		{
			%key = %arg;
			%i++;
		}
		else
		{
			if(%key !$= "")
			{
				%k = %key;
				%key = "";
			}
			else
				%k = "";

			if(%k $= "")
				%sb = %sb @ ", \"" @ %arg @ "\"";
			else
				%sb = %sb @ ", \"" @ %k @ "\", \"=>\", \"" @ %arg @ "\"";
		}
	}

	%sb = %sb @ ");";

	if($array_debug) echo(%sb);
	eval(%sb);
}

//WARNING: echo'ing the result of this function crashes T2.  No idea why.
function SimSet::array_shift(%this, %newarrname)
{
	%ret = %this.array_get_value_at_index(0);

	%sb = "array(\"" @ %newarrname @ "\"";

	//rebuild array with the last element removed
	for(%i = 1; %i <= %this.__internal_count; %i++)
	{
		%key = %this.array_get_key_at_index(%i);
		%value = %this.array_get_value_at_index(%i);

		%sb = %sb @ ", \"" @ %key @ "\", \"=>\", \"" @ %value @ "\"";
	}

	%sb = %sb @ ");";

	if($array_debug) echo(%sb);
	eval(%sb);

	return %ret;
}

function SimSet::array_unshift(%this, %newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	%this._array_unshift(%newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50);
}
function SimSet::_array_unshift(%this, %newarrname, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29, %a30, %a31, %a32, %a33, %a34, %a35, %a36, %a37, %a38, %a39, %a40, %a41, %a42, %a43, %a44, %a45, %a46, %a47, %a48, %a49, %a50)
{
	%sb = "array(\"" @ %newarrname @ "\"";

	//build new part of array
	%j = 0;
	%key = "";
	for(%i = 0; %i <= 50; %i++)
	{
		eval("%arg = %a" @ %i @ ";");
		eval("%nextarg = %a" @ (%i+1) @ ";");

		if(%arg $= "")
			break;

		if(%nextarg $= "=>")
		{
			%key = %arg;
			%i++;
		}
		else
		{
			if(%key !$= "")
			{
				%k = %key;
				%key = "";
			}
			else
				%k = "";

			if(%k $= "")
				%sb = %sb @ ", \"" @ %arg @ "\"";
			else
				%sb = %sb @ ", \"" @ %k @ "\", \"=>\", \"" @ %arg @ "\"";
		}
	}

	//get contents of current array
	for(%i = 0; %i < %this.__internal_count; %i++)
	{
		%key = %this.array_get_key_at_index(%i);
		%value = %this.array_get_value_at_index(%i);

		%sb = %sb @ ", \"" @ %key @ "\", \"=>\", \"" @ %value @ "\"";
	}

	%sb = %sb @ ");";

	if($array_debug) echo(%sb);
	eval(%sb);
}

function SimSet::array_sum(%this)
{
	%total = 0;
	for(%i = 0; %i <= %this.__internal_count; %i++)
		%total += %this.array_get_value_at_index(%i);

	return %total;
}

function SimSet::test(%this, %tmp)
{
	%this.array_reset();
	while((%val = %this.array_get_value()) !$= false)
	{
		echo("val: " @ %val);
	}

	%r = %this.array_search(%tmp);
	echo("array_search(" @ %tmp @ "): " @ %r);
	echo(%this.array_get_value_at(%r));
}

if($array_debug) array("test", "hello", "there", "mykey", "=>", "myvalue", "everyone");