SOMEWHERE IN A CABINET
Residence of a well-known psychologist sir Robert Hilltop Montgomery 3rd.
-> theCabinet

=== theCabinet

"So, we are here in the cousy cabinet to try and find out what is going on."

+ [nod to confirm] -> cabinet01
+ [stare blankly] -> blankstare
+ [I think there is something wrong with me] -> wrong


=== wrong

I would be more than gald to hear out... 

** [I feel as if everybody uses me]
    Please continue -> dreams
    
** [I fell like everybody is just tossing me around]
    Please continue -> dreams
    
** [They toch me everywhere, it feels like my life is currency]
    Please continue -> dreams

=== dreams

** [I should tell you more about my dreams] -> nightmares01





=== cabinetAgain
"So, we are here in the cousy cabinet to try and find out what is going on."
 feel like we have already met somewhere...
+ [think about it and nod] -> cabinet01
+ [stare at the man blankly] -> blankstare
+ I also have such a feeling but I was created recently -> cabinet01 

=== cabinet01

"You keep telling that your life is a currency, 
and we are here to dig deeper in your mind and memories."

+ "Yes, I assume we are"
    (The coin seemed quite sad)
    -> astonished
    
+ [Nod with assurance.] -> coinroll01
+ [blank stare] -> blankstare


=== blankstare

"It seems that you are in no condition to talk right now, perheps a short story will help?"
    
    + [maybe it will] -> storie01
    + [no, I doubt it will] ->cabinet01
    
    
=== storie01

A lovely little girl was holding two apples with both hands.
Her mom came in and softly asked her little daughter with a smile: my sweetie, 
could you give your mom one of your two apples?

The girl looked up at her mom for some seconds, then she suddenly took a quick bite on one apple, 
and then quickly on the other. The mom felt the smile on her face freeze.
She tried hard not to reveal her disappointment.

Then the little girl handed one of her bitten apples to her mom, and said: mommy, 
here you are. This is the sweeter one.

No matter who you are, how experienced you are, and how knowledgeable you think you are,
always delay judgment. Give others the privilege to explain themselves.
What you see may not be the reality.
Never conclude for others. 
    
  + [Wow, I need this to sink in]  ->cabinet01
  + [Actually I am ready to share a story too, but it is a nightmare] ->nightmares01







=== nightmares01
#parallaxshow
So please tell me what kind of dreams do you have,
perhaps nightmares then? 

+ [I am ready to tell you the one that bothers me]-> coinroll01
+ [There is one that I can not understand]-> coinroll01




=== coinrunner01


    ->ending
    
=== coinrunner02


    ->ending
    

=== endgameMuseum


    ->ending
    
    
=== endgameSmelter


    ->ending
    
    
    









=== astonished

"Wow this is such news for me!" I have lived such a life all the time I remember."
"I am quite serious." - he repied.

+ "I am so surprised by this!" -> astonished01
+ "I am surprised but also quite intrigued" -> astonished02

=== astonished01


    Well that as something new.
    -> ending

=== astonished02

Well we were not expecting this
    -> ending

=== nod
I nodded curtly, not believing a word of it.
-> ending


=== coinroll01

Let's use the power of hypnose and see what your dreamstate will tell us.

* Okay let's see -> rollevel

* Well if you think this is necessary -> rollevel

=== rollevel

# rolllevel

->END


=== ending 

And that is the story we wanted to tell you.
-> END