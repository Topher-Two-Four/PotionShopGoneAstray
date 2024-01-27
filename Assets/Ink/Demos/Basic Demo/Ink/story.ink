// ---- Love Potion ----
// Converted from original inklewriter URL:
// https://www.inklewriter.com/stories/190785
# title: Love Potion
# author: Emely
// -----------------------------


-> adoraAppears


==== adoraAppears ====
Adora appears.
"Greetings, peasant."
"Your vision has not failed you yet. It is indeed I, the treasured rose of the court, Adora Sinclair."
"I demand your strongest tonic that will inspire great fondness for my person." 
"Quickly now.”
Your cat looks at you.
(If first playthrough) "Should I break it to her that neither of us knows who she is?"
(If replaying) "Here we go again."
  + Love Potion
        -> certainlyTookYou1 
  + Health Potion
        -> certainlyTookYou2 
  + Hate Potion
        -> certainlyTookYou3 

= certainlyTookYou1
“Certainly took you long enough.”
Adora leaves.
    -> later3

= certainlyTookYou2
"Certainly took you long enough."
Adora leaves.
Later.
Cullen appears.
    -> greetingsAlchemi

= certainlyTookYou3
"Certainly took you long enough."
Adora leaves.
Later.
Cullen appears.
"Greetings Alchemist. Quite embarrassed to say this, but I fear I struggle with the most annoying of pests in my home." 
"Say, do you happen to sell anything strong enough to deal with this predicament?"
Your cat looks at you.
"Strange one, isn't he?"
  + Slow Death Potion
        -> youHaveMyGratitu 
  + Instant Death Potion
        -> youHaveMyGratitu1 
  + Health Potion
        -> youHaveMyGratitu2 

= later3
Later.
Adora appears again. Ecstatic. 
“Marvelous! It worked wonderfully! I’ll tell my admirers all about you.
A coin bag appears on your counter
“For your good work.”
Adora leaves.
Adora’s influence appears over the remainder of the week.
“Hello there, alchemist. I heard from the lovely Sinclair that you’re the master of love. May I receive aid?”
  + Love Potion
        -> pleaseAcceptMyDe 
  + Health Potion
        -> adorasInfluenceS1 

= greetingsAlchemi
“Greetings Alchemist. This is a matter of utmost urgency. I fear I may have been poisoned. Tell me. Have you given a woman by the name of Adora a toxin?
  + Afraid so…
        -> afraidSoBestBegi 
  + No, but…
        -> noButSheDidOrder 

= afraidSoBestBegi
“Afraid so. Best begin enjoying your last days on this land.”
Cullen’s expression turns to one of fear
“Please, alchemist! I beg of you, provide me with the antidote! I will pay you handsomely!
  + Love Potion
        -> cullensExpressio1 
  + Health Potion
        -> cullensExpressio2 
  + Hate Potion
        -> youHaveMyGratitu5 

= noButSheDidOrder
“No, but she did order a love potion."
"Thankfully for you, I gave her a health potion instead.”
Cullen’s eyes darken.
“I would like to purchase a toxin for eternal rest. I have a rat problem, you see.
  + Death Potion
        -> muchObliged1 
  + Hatred Potion
        -> muchObliged2 
  + Slow Death Potion
        -> muchObliged3 

= youHaveMyGratitu
"You have my gratitude."
Cullen leaves.
The Plague Arc Begins
  + Next
      TODO: This choice is a loose end.

= youHaveMyGratitu1
"You have my gratitude."
Cullen leaves.
The Assassin Arc Begins
  + Next
      TODO: This choice is a loose end.

= youHaveMyGratitu2
"You have my gratitude"
Cullen leaves.
Later
    -> adoraAppearsSheL

= cullensExpressio1
Cullen’s expression returns to normal.
“You have my gratitude.”
    -> later3

= cullensExpressio2
Cullen’s expression returns to normal.
“You have my gratitude”
Later.
Adora returns to the shop. She looks angry.
“You incompetent idiot! Your toxin might as well have been plain water! It did nothing but waste my time. I demand my funds be returned to me. NOW.
  + Refund
        -> tskUseless 
  + No, no I don’t think I will
        -> wHAT 

= youHaveMyGratitu5
"You have my gratitude"
Cullen leaves.
Later
    -> adoraAppearsSheL

= muchObliged1
"Much obliged"
Cullen leaves.
The Assassin Arc Begins
  + Next
      TODO: This choice is a loose end.

= muchObliged2
"Much obliged"
Cullen leaves. 
Later.
Adora appears.
"What have you <em>done</em>."
"Give me two of your strongest toxins. One with a slow, painful death and another with an instant, painless one." 
"You will give them to me if you know what is good for you."
Your cat looks at you.
"Well, well, well, so many choices, so little time. Whatever will you do?"
  + Two Requested Death Potions
        -> adorLeaves 
  + Two Slow Death Potions
        -> adoraLeaves7 
  + Two Instant Death Potions
        -> adoraLeaves8 
  + Two Love Potions
        -> adoraLeaves9 
  + Two Health Potions
        -> adoraLeaves10 
  + Other Combo
        -> theLawEnforcemen1 
  + No
        -> what 

= muchObliged3
"Much obliged"
Cullen leaves
Plague Arc Begins
    -> END

= pleaseAcceptMyDe
"Please accept my deepest thanks." OR "I appreciate your assistance." OR "Your assistance is sincerely appreciated." OR "Accept my endless gratitude." (Random)
Adora’s influence stays in effect until the end. Cupid ending unlocked.
    -> END

= adorasInfluenceS1
Adora’s Influence stops appearing. Defaults to Normal Narrative Arc.
    -> END

= adoraAppearsSheL
Adora appears. She looks as if a tragedy has occurred.
She mumbles something.
"Sorry, I didn't catch that. What do you need?"
"Death .... death ... the sweet embrace of nothingness ... Please alchemist .... please allow me to ...." 
Her voice trails back into an incoherent mumble.
  + Death Potion
        -> theLawEnforcemen 
  + Health Potion
        -> theDoctorsArcBeg1 
  + Slow Death Potion
        -> thePlagueArcBegi4 

= tskUseless
“Tsk. Useless.”
Adora leaves.
Doctor Arc Begins
    -> END

= wHAT
“WHAT.”
“Give. Me. My. Money. Back…. <em>Now</em>.”
  + Hmmm no
        -> adorasGazeDarken 
  + Refund
        -> tskUseless 

= adorasGazeDarken
Adora’s gaze darkens.
You hear the sound of glass shattering in the background.
“Keep your loose change. I no longer desire it.”
Adora leaves.
Doctor Arc Begins
    -> END

= theLawEnforcemen
The Law Enforcement Act Begins
  + Next
        -> Law_Enforcement 

= theDoctorsArcBeg1
The Doctors Arc Begins
  + Next
      TODO: This choice is a loose end.

= thePlagueArcBegi4
The Plague Arc Begins
    -> END

= adorLeaves
Ador leaves.
Law Enforcement Arc.
  + Next
      TODO: This choice is a loose end.

= adoraLeaves7
Adora leaves.
The Plague Arc Begins
    -> END

= adoraLeaves8
Adora leaves.
The Assassin Arc Begins.
    -> END

= adoraLeaves9
Adora leaves.
Adora's influence appears.
    -> END

= adoraLeaves10
Adora leaves.
The Doctor Arc Begins.
  + Next
      TODO: This choice is a loose end.

= theLawEnforcemen1
The Law Enforcement Arc Begins
    -> END

= what
"<em>What</em>"
  + I said no.
        -> yourCatGetsUp 
  + I'll brew you those potions now.
        -> potionChoice 

= yourCatGetsUp
Your cat gets up.
"Heh, I suddenly feel the urge to nap upstairs."
Your cat disappears.
"How <em>dare </em>you. After what you have done, you <em>DARE </em>REFUSE ME!"
  + On second thought, I'll get right on those potions.
        -> potionChoice1 
  + Yea, so what?
        -> theRoomDarkens 

= potionChoice
Potion choice.
    -> END

= potionChoice1
Potion choice.
    -> END

= theRoomDarkens
The room darkens.
There's an explosion from the cauldron room.
"I hope you regret what you have done."
Adora leaves.
Law Enforcement Arc Begins
    -> END

==== Law_Enforcement ====

    -> END

= later
Later.
    -> adoraAppears.greetingsAlchemi

= certainlyTookYou
"Certainly took you long enough"
Adora leaves.
    -> END

= later1
Later.
    -> adoraAppears.adoraAppearsSheL

= muchObliged
"Much obliged"
Cullen leaves.
Later.
    -> adoraAppears.adoraAppearsSheL

= thePlagueArcBegi
The Plague Arc Begings
  + Next.
      TODO: This choice is a loose end.

= theDoctorsArcBeg
The Doctors Arc Begins
  + Next
      TODO: This choice is a loose end.

= adoraLeaves1
Adora leaves.
The Plague Arc Begins
    -> END