SOMWHERE IN A CABINET
Residence of a well-known psychologist sir Robert Hilltop Montgomery 3rd.
-> theCabinet

=== theCabinet

"So, we are here in the cousy cabinet to try and find out what is going on."

+ [nod to confirm] -> cabinet01
+ [stare blankly] -> blankstare

=== cabinetAgain
"So, we are here in the cousy cabinet to try and find out what is going on."
 feel like we have already met somewhere...
+ [think about it and nod] -> cabinet01
+ [stare at the man blankly] -> blankstare
+ I also have such a feeling but I was created recently -> cabinet01 

=== cabinet01

"You keep telling that your life is a currency, and we are here to dig deeper in your mind and memories."

"Passepartout," said he. "We are going around the world!"

+ "Around the world, Monsieur?"
    I was utterly astonished. 
    -> astonished
+ [Nod curtly.] -> nod



=== blankstare

"It seems that you are in no condition to talk today, perheps a short story will help?"
    
    + maybe it will -> storie01
    + no, I doubt it will ->cabinet01
    
    
    == storie01
    
    
    ->ending




=== nightmares




    -> ending


=== coinrunner01


    ->ending
    
=== coinrunner02


    ->ending
    

=== endgameMuseum


    ->ending
    
    
=== endgameSmelter


    ->ending
    
    
    









=== astonished

"You must be joking!"- I told the man. "I have lived such a life all the time I remember."
"I am quite serious." - he repied.

+ "I am so surprised by this!" -> astonished01
+ "I am surprised but also quite intrigued" -> astonished02
== astonished01

Well that as something new.
    -> ending

==astonished02

Well we were not expecting this
    -> ending


=== nod
I nodded curtly, not believing a word of it.
-> ending


=== ending
"We shall circumnavigate the globe within eighty days." He was quite calm as he proposed this wild scheme. "We leave for Paris on the 8:25. In an hour."
-> END