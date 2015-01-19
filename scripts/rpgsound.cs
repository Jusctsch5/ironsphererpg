datablock AudioDescription(SwingDescription)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = true;
   minDistance= 5.0;
   MaxDistance= 10.0;
   type     = $EffectAudioType;
};


datablock AudioProfile(Swing1)
{
   filename  = "Swing.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(LevelUp)
{
   filename  = "LevelUp.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(Swing2)
{
   filename  = "Swing2.wav";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(Swing3)
{
   filename  = "Swing3.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(Swing4)
{
   filename  = "Swing4.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(Swing5)
{
   filename  = "Swing5.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(Swing6)
{
   filename  = "Swing6.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioDescription(WeaponHitDescription)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = true;
   minDistance= 5.0;
   MaxDistance= 10.0;
   type     = $EffectAudioType;
};

datablock AudioProfile(WeaponHit1)
{
   filename  = "WeaponHurt.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(WeaponHit2)
{
   filename  = "WeaponHurt2.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(WeaponHit3)
{
   filename  = "WeaponHurt3.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(WeaponHit4)
{
   filename  = "WeaponHurt4.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(WeaponHit5)
{
   filename  = "WeaponHurt5.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(WeaponHit6)
{
   filename  = "WeaponHurt6.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(DrawKnife)
{
   filename  = "Knife_Deploy1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(DrawLongSword)
{
   filename  = "LongBladOUT.wav";
   description = AudioDefault3d;
   preload = true;
};

//datablock AudioProfile(BlasterSwitchSound)
//{
//   filename    = "fx/weapons/blaster_activate.wav";
//   description = AudioClosest3d;
//   preload = true;
//   effect = BlasterSwitchEffect;
//};
