function GetMyID()
{
	$myId = ClientGroup.getObject(0);
	return $myId;
}
datablock StaticShapeData(ShortSword2SS)  
{
   //className = Logo;
   shapeFile = "shortsword2.dts";
   alwaysAmbient = true;
};
function MountWeapon(%client, %slot)
{

	%rightbarrel = new StaticShape() 
		{
		dataBlock = ShortSword2SS;
		};
	%rightbarrel.startFade(0,0,1);
	//%rightbarrel.SetScale("5 4 5");
	%client.player.mountObject(%rightbarrel, 1);

}
function errorReport(%msg, %emergencyshutdown)
{
	echo(%msg);
}
datablock ShapeBaseImageData(ShieldPackImage)
{
   shapeFile = "plasmabolt.dts";
   item = ShieldPack;
   mountPoint = 1;
   offset = "0 0 0";
   scale = "5 5 5";
   usesEnergy = false;
   minEnergy = 3;

	
};
function realtest()
{
	%anmstr1 = initqueue(4);//left and right arms
	%anmstr2 = initqueue(4);//whooo
	%anmstr1.push(11);
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr1.push(12).put(2).put(6);
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr2.push(4);
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr1.flush();
	%anmstr2.flush();
	echo("user dies");
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr1.push(5);
	%anmstr2.push(6).push(6).push(8).push(15).push(20);
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr2.push(10);
	%anmstr1.push(5);
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	echo("user does:right/left:" @ %anmstr1.pop() @ "/" @ %anmstr2.pop());
	%anmstr1.flush();
	%anmstr2.flush();
	%anmstr1.delete();
	%anmstr2.delete();
}
//queue functions for Tribes 2
//Created by Trident
//May be used in any mod/script for Tribes 2 but client must leave these comments in
//below is a test example to show how to use the queue functions
//Date: 11/8/2002
//to run test example first load the .cs file then type in testqueue();
//new ScriptObject(Queue) {
//	class = "Queue";
//};

function Queue::setsize(%this, %data)
{
	if(%data >= 1)
	{
		%this.size = %data;
		while(%this.size < %this.pos)
			%this.pop();
	}
	else
		echo("Invalid size for SimSet::setsize var=" @ %this @ " size = " @ %data @".");
	return %this;
}
function Queue::bad(%this)
{
	if(%this.pos == 0 || %this.size == 0)
		return true;
	else
		return false;
}
function Queue::put(%this, %data)
{
	return %this.push(%data);
}
function Queue::flush(%this)
{
	while(%this.pos > 0)
		%this.pop();
	return %this;
}
function Queue::push(%this, %data)
{
	if( %this.size == 0)//double check for init
		%this.init(20);//default 20
	if(%this.size > %this.pos)
	{
		%this.data[%this.pos] = %data; 
		%this.pos++;
	}
	return %this;
}
function Queue::full(%this)
{
	if(%this.size >= %this.pos)
		return true;

	return false;
}
function Queue::pop(%this)
{
	%data = %this.get();
	for(%i = 0; %i < %this.size-1;%i++)
	%this.data[%i] = %this.data[%i+1];
	%this.data[%i] = 0;
	if(%this.pos > 0)
	%this.pos--;
	return %data;
}
function Queue::getpos(%this, %pos)
{

	return %this.data[%pos-1];
}
function Queue::get(%this)
{
	%data = %this.data[0];
	return %data;
}
function Queue::init(%this, %size)
{
	if(%size > 0)
		%this.setsize(%size);
	else
		%this.setsize(20);
	%this.pos = 0;
	return %this;
}
function initqueue(%size)
{
	%queue = new ScriptObject() {
		class = "Queue";
	};
	%queue.init(%size);
	return %queue;
}

function testqueue()          //ancient magic of some sort...
{
	%queue = initqueue();
	echo("%queue = initqueue();");
	echo("Putting values 0 5 10 23 53 onto queue");
	%queue.push(0);
	%queue.push(5);
	%queue.push(10);
	%queue.push(23);
	%queue.push(53);//5 in
	echo("Popping 4 values");
	echo(%queue.pop());
	echo(%queue.pop());
	echo(%queue.pop());
	echo(%queue.pop());//4 out
	echo("placing another value");
	%queue.push(11);//1 more in
	echo("getting value w/out popping");
	echo(%queue.get());//look at top of the queue
	echo("flushing queue and getting the top value");
	echo(%queue.flush().get());//look at top of queue
	echo("putting 5 40 5 on the queue then flushing then placing 6 on the queue");
	echo("using cascading sequence:");
	echo("%queue.push(5).push(40).push(5).flush().push(6);");
	%queue.push(5).push(40).push(5).flush().push(6);//queueable
	echo("popping top value");
	echo(%queue.pop());//pop top of queue
	echo("popping again even though there is nothing on our queue");
	echo(%queue.pop());//pop again! uh-oh
	echo("putting the strings \"bad\" \"boys\" \"are\" \"cool\" on queue");
	%queue.push("bad").push("boys").push("are").push("cool");
	echo("Popping the top value getting the 2nd value popping again then getting the first");
	echo(%queue.pop());//cool
	echo(%queue.getpos(2));//boys
	echo(%queue.pop());//are
	echo(%queue.getpos(1));//bad
	echo("flushing the queue");
	%queue.flush();
	echo("setting size to 2");
	%queue.setsize(2);
	echo("placing values 5 6 on the queue");
	%queue.push(5).push(6);
	echo("changing queue size to 1");
	%queue.setsize(1);
	echo("popping the top value changing size back to 20");
	%queue.pop();
	%queue.setsize(20);
	echo("initing %queue2 and giving it a size of 1 pushing value of 49 on it");
	%queue2 = initqueue(1);
	%queue2.push(49);
	echo("initing %queue3 and giving it a size of 2 pushing values 10 5");
	%queue3 = initqueue(2);
	%queue3.push(10).push(5);
	echo("pushing %queue2 and %queue3 onto %queue");
	%queue.push(%queue2).push(%queue3);
	echo("popping top value of %queue and popping the popped value of %queue");
	echo(%queue.pop().pop());
	echo("getting the value of the value from the top of the queue");
	echo(%queue.get().get());
}
//stack functions for Tribes 2
//Created by Trident
//May be used in any mod/script for Tribes 2 but client must leave these comments in
//below is a test example to show how to use the stack functions
//Date: 11/8/2002
//Edit: 2/16/2003
//to run test example first load the .cs file then type in teststack();
//new ScriptObject(Stack) {
//	class = "Stack";
//};

function Stack::setsize(%this, %data)
{
	if(%delta == 0)	%this.unlim = true;
	else if(%data >= 1)
	{
		this.unlim = false;
		%this.size = %data;
		while(%this.size < %this.pos)
			%this.pop();
	}
	else
		echo("Invalid size for stack::setsize var=" @ %this @ " size = " @ %data @".");
	return %this;
}
function Stack::bad(%this)
{
	if(%this.pos < 0)
		return true;
	if(%this.size = 0 && !%this.unlim)
		return true;
	return false;
}
function Stack::put(%this, %data)
{
	return %this.push(%data);
}
function Stack::push(%this, %data)
{

	if(%this.size > %this.pos || %this.unlim)
	{
		%this.data[%this.pos] = %data; 
		%this.pos++;
	}
	return %this;
}
function Stack::empty(%this)
{
	return (%this.pos == 0);
}
function Stack::getpos(%this, %pos)
{
	%temppos = %this.pos;
	%this.pos = %pos;
	%retval = %this.get();
	%this.pos = %temppos;
	return %retval;
}
function Stack::flush(%this)
{
	while(%this.pos > 0)
		%this.pop();
	return %this;
}
function Stack::pop(%this)
{
	%data = %this.get();
	if(%this.pos > 0)
	%this.pos--;
	%this.data[%this.pos] = 0;
	return %data;
}
function Stack::get(%this)
{
	%retval = 0;
	if(%this.pos > 0)
		%retval = %this.data[(%this.pos-1)];
	return %retval;
}
function Stack::init(%this, %size)
{
	if(%size > 0)
		%this.setsize(%size);
	else
		%this.unlim = true;
	%this.pos = 0;
	return %this;
	
}

function initstack(%size)
{
	%stack = new ScriptObject() {
		class = "Stack";
	};
	%stack.init(%size);
	return %stack;
}
function teststack()
{
	%stack = initstack();
	echo("%stack = initstack();");
	echo("Putting values 0 5 10 23 53 onto stack");
	%stack.push(0);
	%stack.push(5);
	%stack.push(10);
	%stack.push(23);
	%stack.push(53);//5 in
	echo("Popping 4 values");
	echo(%stack.pop());
	echo(%stack.pop());
	echo(%stack.pop());
	echo(%stack.pop());//4 out
	echo("placing another value");
	%stack.push(11);//1 more in
	echo("getting value w/out popping");
	echo(%stack.get());//look at top of the stack
	echo("flushing stack and getting the top value");
	echo(%stack.flush().get());//look at top of stack
	echo("putting 5 40 5 on the stack then flushing then placing 6 on the stack");
	echo("using cascading sequence:");
	echo("%stack.push(5).push(40).push(5).flush().push(6);");
	%stack.push(5).push(40).push(5).flush().push(6);//stackable
	echo("popping top value");
	echo(%stack.pop());//pop top of stack
	echo("popping again even though there is nothing on our stack");
	echo(%stack.pop());//pop again! uh-oh
	echo("putting the strings \"bad\" \"boys\" \"are\" \"cool\" on stack");
	%stack.push("bad").push("boys").push("are").push("cool");
	echo("Popping the top value getting the 2nd value popping again then getting the first");
	echo(%stack.pop());//cool
	echo(%stack.getpos(2));//boys
	echo(%stack.pop());//are
	echo(%stack.getpos(1));//bad
	echo("flushing the stack");
	%stack.flush();
	echo("setting size to 2");
	%stack.setsize(2);
	echo("placing values 5 6 on the stack");
	%stack.push(5).push(6);
	echo("changing stack size to 1");
	%stack.setsize(1);
	echo("popping the top value changing size back to 20");
	echo(%stack.pop());
	%stack.setsize(20);
	echo("initing %stack2 and giving it a size of 1 pushing value of 49 on it");
	%stack2 = initstack(1);
	%stack2.push(49);
	echo("initing %stack3 and giving it a size of 2 pushing values 10 5");
	%stack3 = initstack(2);
	%stack3.push(10).push(5);
	echo("pushing %stack2 and %stack3 onto %stack");
	%stack.push(%stack2).push(%stack3);
	echo("popping top value of %stack and popping the popped value of %stack");
	echo(%stack.pop().pop());
	echo("getting the value of the value from the top of the stack");
	echo(%stack.get().get());
}


