//helpers.cs
//function libs for addition and assist functioning
//Phantom139

function GetRandomPosition(%mult,%nz) {
   %x = getRandom()*%mult;
   %y = getRandom()*%mult;
   %z = getRandom()*%mult;

   %rndx = getrandom(0,1);
   %rndy = getrandom(0,1);
   %rndz = getrandom(0,1);

   if(%nz) {
      %z = 0;
   }

   if (%rndx == 1){
      %negx = -1;
   }
   if (%rndx == 0){
      %negx = 1;
   }
   if (%rndy == 1){
      %negy = -1;
   }
   if (%rndy == 0){
      %negy = 1;
   }
   if (%rndz == 1){
      %negz = -1;
   }
   if (%rndz == 0){
      %negz = 1;
   }

   %rand = %negx * %x SPC %negy * %y SPC %Negz * %z;
   return %rand;
}
