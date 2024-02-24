// ---- The Law Enforcement Arc ----
// Converted from original inklewriter URL:
// https://www.inklewriter.com/stories/190289
# title: The Law Enforcement Arc
# author: Emely T.
// -----------------------------


-> theDetectiveAppe


==== theDetectiveAppe ====
The Detective appears
“Tally-Ho, good chap!”
  + Next
        -> dareIBotherYouWi 

= dareIBotherYouWi
“Dare I bother you with a request?”
  + Sure...
        -> splendid 
  + No
        -> nonsenseWellIDoD 

= splendid
"Splendid!"
  + Next
        -> mightIAcquireAVi 

= nonsenseWellIDoD
“Nonsense! Well, I do dare say! Where is your spirit of adventure!”             
  + Next
        -> haveYouNeverDrea 

= mightIAcquireAVi
"Might I acquire a vial said to expand upon my current senses?"
  + ???
        -> myApologies 
  + Certainly!
        -> thatsTheSPIRIT 

= haveYouNeverDrea
“Have you never dreamed of solving a crime?!”
  + Next
        -> wellNowYouVeryWe 

= myApologies
"My apologies."
  + Next
        -> itAppearsIHaveCo 

= thatsTheSPIRIT
"That's the SPIRIT!"
  + Next
        -> chopchopMyGoodFr 

= wellNowYouVeryWe
“Well, now you very well can!”
  + Next
        -> quiteSimpleTooYo 

= itAppearsIHaveCo
"It appears I have come to the incorrect establishment for such a request."
  + Next
        -> bestOfLuckMyGood 

= chopchopMyGoodFr
"Chop-chop, my good friend! This is a matter of utmost urgency! There are criminals we must apprehend!"
  + Health Potion
        -> grandioseThatIsM 
  + 6th Sense
        -> stupendousYouAre 
  + Slow Death Potion
        -> grandioseThatIsM1 
  + Instant Death Potion
        -> stupendousYouAre1 

= quiteSimpleTooYo
“Quite simple too, you see."
  + Next
        -> allYouHaveToDoIs 

= bestOfLuckMyGood
"Best of luck my good friend."
The Detective leaves.
  + Next.
        -> theCorruptCopApp 

= grandioseThatIsM
"Grandiose! That is my grand good fellow! Much obliged. Cheerio!"
  + Next
        -> theCorruptCopApp 

= allYouHaveToDoIs
"All you have to do is sell me a vial that will expand upon my senses!" 
  + Next
        -> iWillHandleTheRe 

= stupendousYouAre
"Stupendous! You are a grand good fellow! Cheerio!"
  + Next
        -> greetingsApothec 

= grandioseThatIsM1
"Grandiose! That is my grand good fellow! Much obliged. Cheerio!"
<strong>Plague Arc Begins</strong>
    -> END

= stupendousYouAre1
"Stupendous! You are a grand good fellow! Cheerio!"<strong></strong>
<b>Assassin's Arc Begins</b>
    -> END

= theCorruptCopApp
The Corrupt Cop appears.
"Hey, <strong>chu</strong>."
  + Me?
        -> yeahCHUChuSeeAny 
  + Bless you
        -> donchuBeCute 

= iWillHandleTheRe
"I will handle the rest!"
  + 6th Sense Potion
        -> stupendousYouAre2 
  + Health Potion
        -> muchObligedMyGoo 

= greetingsApothec
"Greetings, apothecary."
  + Next
        -> itAppearsARatInf 

= stupendousYouAre2
"Stupendous! You are a grand good fellow! Cheerio!"
  + Next
        -> theDetectiveAppe1 

= muchObligedMyGoo
"Much obliged, my good fellow! Cheerio!"
Detective leaves.
  + Next
        -> theCorruptCopApp 

= itAppearsARatInf
"It appears a rat infestation is running rampant as of late."
  + Next
        -> say 

= yeahCHUChuSeeAny
"Yeah, CHU! Chu see anyone ELSE 'round?!" 
  + ...
        -> iKnowWhotChuDid 
  + ...my cat?
        -> donchuBeCute 

= donchuBeCute
"Donchu be cute."
  + Next
        -> iKnowWhotChuDid 

= theDetectiveAppe1
The Detective appears.
    -> greetingsApothec

= say
"Say."
  + Next
        -> punctuatedStitch2 

= iKnowWhotChuDid
"I know whot chu did."
  + So what?
        -> soWhot 
  + I'm afraid I don't...
        -> dONCHUPLAYDUMBWI 

= punctuatedStitch2
"..."
  + Next
        -> careToAssistMeWi 

= soWhot
"So <strong><em>whot</em></strong>!"
  + Next
        -> iGotDirtOnChuRat 

= dONCHUPLAYDUMBWI
"DONCHU PLAY DUMB WIT ME! I ain't no fool." 
  + Next
        -> theSinclair 

= careToAssistMeWi
"Care to assist me with a substance powerful enough to wipe the whole lot of them?" 
  + Next
        -> iWouldBeIeverIso 

= iGotDirtOnChuRat
"I got dirt on chu, rat!"
  + Next
        -> sOHereWhotsGonna 

= theSinclair
"The Sinclair?!" 
  + Next
        -> herLilPrettyBoi 

= iWouldBeIeverIso
"I would be <i>ever </i>so grateful."
  + Death Poison
        -> punctuatedStitch3 
  + Health Potion
        -> theDetectiveStar 

= sOHereWhotsGonna
"<strong>SO</strong>, here whot's gonna happen."
  + Next
        -> chuGonnaGiveMeAn 

= herLilPrettyBoi
"Her lil pretty boi?!" 
  + Next
        -> theDetective 

= punctuatedStitch3
"...."
  + Next
        -> excellent 

= theDetectiveStar
The detective stares at the potion.
"Might you not have anything with a more considerable punch?"
  + Death Potion
        -> punctuatedStitch3 
  + Afraid not
        -> theDetectiveStar1 

= chuGonnaGiveMeAn
"Chu gonna give me and my boi's whatever the damn well we feel like taking." 
  + Next
        -> punctuatedStitch 

= theDetective
"The <em>detective</em>?"
  + Next
        -> wellArentChuGood 

= excellent
"Excellent."
  + Next
        -> iBestBeOffIHaveM 

= punctuatedStitch
"..."
  + Next
        -> orElse 

= wellArentChuGood
"Well, aren't chu good at chur job, eh?"
  + So?
        -> sOHereWhotsGonna 

= theDetectiveStar1
The detective stares.
"..."
  + Next
        -> veryWellIllTakeI 

= iBestBeOffIHaveM
"I best be off. I have much to take care of."
The Detective leaves.
  + Next.
        -> theDetectiveAndF 

= orElse
"Or <em><strong>else</strong></em>...."
  + Or else what?
        -> orElseThatFoolOf 
  + Okay, alright.
        -> thatsHowILikeChu 

= orElseThatFoolOf
"Or <em><strong>else</strong></em>, that fool of a detective gonna come sniffing <em>reeeeal </em>close to home. Chu feel me?"
  + Yeah...
        -> yeahIDoSoWhatDoY 
  + ... on the house...
        -> sureThingBossHer 
  + No...
        -> noIDontSo 

= thatsHowILikeChu
"That's how I like chu."
  + Next
        -> soHandOvorTheGoo 

= veryWellIllTakeI
"Very well. I'll take it"
  + Next
        -> punctuatedStitch9 

= theDetectiveAndF
The Detective and four good cops appear.
"Greetings, apothecary."
  + Next
        -> iDoDeclare 

= yeahIDoSoWhatDoY
"Yeah, I do. So, what do you want?"
  + Next
        -> thatsHowILikeChu 

= sureThingBossHer
Sure thing, boss. Here you go, on the house.
  + Death Potion
        -> illBeSeeingChuAr 
  + Beneficial Potion
        -> yeaaahThatsRoigh 

= noIDontSo
"No, I don't. So.
  + Next
        -> getOut 

= soHandOvorTheGoo
"So, hand ovor the good stuff!"
  + Death Potion
        -> illBeSeeingChuAr 
  + Beneficial Potion
        -> yeaaahThatsRoigh 

= punctuatedStitch9
"..."
  + Next
        -> youKnow 

= illBeSeeingChuAr
"I'll be seeing chu around."
The Corrupt Cop leaves.
<strong>The Assassins Arc Begins</strong>
    -> END

= getOut
"Get out."
  + Next
        -> now 

= yeaaahThatsRoigh
"Yeaaah that's roight!"
  + Next
        -> donchuForgetIGot 

= iDoDeclare
"I do declare."
  + Next
        -> thatYouLittleMis 

= youKnow
"You know."
  + Next
        -> iHaveAlsoBeenDea 

= now
"<em>Now</em>."
  + Next
        -> theCorruptCopSta 

= donchuForgetIGot
"Donchu forget I got dirt on chu, so when my boi's show, chu give them whot they want." 
  + Next
        -> chuGotIt 

= thatYouLittleMis
"That you, little miscreant, are under arrest."
Abrupt ending. Even if the week is not complete, the ending is received.
<strong>Abrupt Ending Unlocked: Better Call Soul</strong>
    -> END

= iHaveAlsoBeenDea
"I have also been dealing with the most dreadful of headaches."
  + Next
        -> mySuperiorIsVery 

= theCorruptCopSta
The corrupt cop stares.
Cop: "..."
  + Next
        -> bestNotRegretWho 

= chuGotIt
"Chu got it?"
  + Sure...
        -> donchuGoForgetti 
  + Not for free I wont.
        -> theCorruptCopSta1 

= mySuperiorIsVery
"My superior is very well driving me mad!"
  + Next
        -> say1 

= donchuGoForgetti
"Donchu go forgetting who's in charge here, Tiny."
The Corrupt Cop leaves.
  + Next
        -> corruptCopsGoonA 

= theCorruptCopSta1
The Corrupt Cop stares.
"..."
  + Next
        -> chuGotSomeSortaD 

= say1
"Say!" 
  + Next
        -> aidMeWithThisPar 

= bestNotRegretWho
"Best not regret whot comes next."
The corrupt cop leaves.
  + Next
        -> theDetectiveAppe1 

= aidMeWithThisPar
"Aid me with this particularly bothersome predicament, wise apothecary!"
  + Health Potion
        -> wellIDoDareSayTh 
  + Death Potion
        -> punctuatedStitch3 
  + Beneficial Potion
        -> punctuatedStitch13 

= corruptCopsGoonA
Corrupt Cop's Goon appears.
    -> heyChuTheBossSen

= chuGotSomeSortaD
"Chu got some sorta death wish, Tiny?"
  + How about...
        -> howAboutADiscoun 
  + Yes
        -> chuEitherGotGuts 

= wellIDoDareSayTh
"Well, I do dare say! These veils do appear to be twins!"
  + Next
        -> aHAHAHA 

= punctuatedStitch13
"..."
"Good show, apothecary. Good show indeed."
The Detective leaves.
<strong>The Doctor Arc Begins</strong>
    -> END

= heyChuTheBossSen
"Hey chu, the boss sent me. Hand ovor the good stuff, kid."
  + Beneficial Potion
        -> thatsWhotImTalki 
  + Crippling Potion
        -> donchuForgetWhos 
  + Death Potion
        -> chuBestBeGivingM 

= howAboutADiscoun
"How about a discount instead?"
  + Next
        -> heavyDiscount 

= chuEitherGotGuts
"Chu either got guts or just plain stupid."
  + How about...
        -> howAboutIGiveYou 
  + Yes, and yes
        -> chuckleChuKnowWh 

= aHAHAHA
"AHAHAHA"
  + Next
        -> youWouldNotBeGiv 

= thatsWhotImTalki
"That's whot I'm talking 'bout!"
  + Next
        -> seeYaLaterKid 

= donchuForgetWhos
"Donchu forget whose in charge, kid."
The Goon leaves.
  + Next
        -> theDetectiveTheC 

= chuBestBeGivingM
"Chu best be giving me the best, kid."
The Corrupt Cop's goon leaves
  + Next
        -> gedeonAppears 

= heavyDiscount
"Heavy discount."
  + No, light
        -> meetMeInTheMiddl 
  + I gotta run a business
        -> chuckleWorriedBo 

= howAboutIGiveYou
"How about I give you and your boi's a discount on all products?"
  + Next
        -> heavyDiscount 

= chuckleChuKnowWh
*chuckle* "Chu know whot, Tiny?" 
  + What?
        -> imStartingToLike 

= youWouldNotBeGiv
"You would not be giving me the same as the rats, would you?"
  + ....Noooo
        -> theDetectiveStar2 
  + ...confess...
        -> iMustConfess 

= seeYaLaterKid
"See ya later, kid."
The Goon leaves.
  + Next
        -> corruptCopsSquad 

= meetMeInTheMiddl
"Meet me in the middle, Tiny, and chu got a deal."
  + Deal
        -> yeahThatsRoight 
  + No
        -> nowDonchuStartGe 

= chuckleWorriedBo
*chuckle* "Worried 'bout chur lil shop, are chu?" 
  + Next
        -> chuckleFineButOn 

= imStartingToLike
"I'm starting to like chu."
  + Next
        -> howAboutThis 

= theDetectiveStar2
The Detective stares.
"...."
  + Next
        -> howPrecarious 

= iMustConfess
"I must confess!"
  + Next
        -> iMustApologizeFo 

= theDetectiveTheC
The Detective, the Corrupt Cop, and three of the Corrupt Cop's Squad appear.
The Detective: "Greetings, apothecary."
  + Next
        -> iDoDeclare 

= gedeonAppears
Gedeon appears.
<em>"Greetingsssss sssssly one."</em>
  + Next
        -> iveSsssseenWhatY 

= yeahThatsRoight
"Yeah, that's roight."
  + Next
        -> nowWeInBusinessT 

= chuckleFineButOn
*chuckle* "<em>Fine</em>, but only cause I like chu."
  + Next
        -> donchuGoTellingT 

= nowDonchuStartGe
"Now, donchu start getting stupid on me Tiny."
  + Next
        -> makeTheDeal 

= howAboutThis
"How about this..."
  + Next
        -> chuGiveMyBoisADi 

= iMustApologizeFo
"I must apologize for my <em>despicable </em>deception..."
  + Next
        -> theDetectiveStar3 

= corruptCopsSquad
Corrupt Cop's Squad appears for the remainder of the week.
"Hey, kid! Hand ovor some of the good stuff."
  + Beneficial Potion
        -> corruptCopsSquad1 
  + Crippling Potion
        -> niceOneKid 
  + Death Potion
        -> greatJobKid 

= nowWeInBusinessT
"Now we in business, Tiny."
  + Next
        -> donchuGoDoublecr 

= donchuGoTellingT
"Donchu go telling the others I went easy on chu, Tiny."
  + Next
        -> chuGotIt1 

= makeTheDeal
"Make the deal...."
  + Next
        -> or 

= chuGiveMyBoisADi
"Chu  give my boi's a discount, heavy on the discount part."
  + Light on the discount
        -> punctuatedStitch7 
  + No
        -> punctuatedStitch8 

= howPrecarious
"How precarious..."
The Detective leaves.
  + Next.
        -> theCorruptCopApp1 

= theDetectiveStar3
The Detective stares.
Detective: "...."
  + Next
        -> youSee 

= iveSsssseenWhatY
<em>"I've ssssseen what you've done."</em>
  + Next
        -> iAmPartOfTheAsss 

= donchuGoDoublecr
"Donchu go double-crossing us."
  + Next
        -> itWouldntEndWell 

= chuGotIt1
"Chu got it?"
  + Got it, boss
        -> chuckleBehaveTin 
  + I'll try
        -> chuckleBehaveTin 

= or
"Or...."
  + Next
        -> chuGonnaHaveToSt 

= punctuatedStitch7
""
    -> END

= punctuatedStitch8
"...."
  + Next
        -> nowDonchuStartGe 

= corruptCopsSquad1
Corrupt Cop's Squad continues to appear.
As long as a beneficial Potion is given every time:
<strong>Ending Unlocked: Chu Corrupt Now</strong>
    -> END

= niceOneKid
"Nice one, kid."
The Goon leaves.
  + Next
        -> theDetectiveTheC 

= iAmPartOfTheAsss
<em>"I am part of the asssssasssssins guild."</em>
  + Next
        -> iCanMakeAllYourP 

= greatJobKid
"Great job, kid."
The Goon leaves
  + Next
        -> theCorruptCopAnd 

= itWouldntEndWell
"It wouldn't end well for chu if chu did."
  + Next
        -> nowToCelebrate 

= chuckleBehaveTin
*chuckle* "Behave, Tiny."
The Corrupt Cop leaves.
  + Next
        -> heyChuTheBossSen 

= chuGonnaHaveToSt
"Chu gonna have to start worrying 'bout other things down in the clink, chu feel me?"
  + ???
        -> jail 
  + Alright, deal
        -> yeahThatsRoight 
  + I'll take my chances
        -> punctuatedStitch11 

= theCorruptCopApp1
The Corrupt Cop appears.
*snicker*
"Hey, chu"
  + ....
        -> suddenlyDealingW 

= youSee
"You see..."
  + Next
        -> punctuatedStitch15 

= iCanMakeAllYourP
<em>"I can make all your problemsssss disssssappear."</em>
  + Next
        -> forAPriccccce 

= nowToCelebrate
"Now, to celebrate!"
  + Next
        -> whyDonchuGiveMeA 

= jail
"<em>Jail</em>"
  + Next
        -> dealOrJailTiny 

= punctuatedStitch11
"...."
  + ....
        -> chuCantBeSerious 

= punctuatedStitch15
"..." 
  + Next
        -> iLOVEALLLIVINGCR 

= theCorruptCopAnd
The Corrupt Cop and four of his squad members appear.
The Corrupt Cop looks pissed.
"...."
  + What?
        -> punctuatedStitch6 

= forAPriccccce
<em>"For a priccccce."</em>
  + No
        -> areYouCccccertai 
  + How much?
        -> no 

= whyDonchuGiveMeA
"Why donchu give me a lil something....on the house."
  + Death Potion
        -> illBeSeeingChuAr 
  + Beneficial Potion
        -> great 

= dealOrJailTiny
"<em>Deal </em>or <em>Jail</em>, Tiny."
  + Deal
        -> nowChuStartingTo 
  + Jail
        -> punctuatedStitch11 

= chuCantBeSerious
"Chu can't be serious."
  + I am
        -> punctuatedStitch12 
  + I'm not, deal
        -> iWasDoneStartWor 

= iLOVEALLLIVINGCR
"I LOVE <em>ALL </em>LIVING CREATURES!"
  + Next
        -> iAmNoSimpleApoth 

= areYouCccccertai
<em>"Are you cccccertain? Monetary value is not that which I ssssseek."</em>
  + Then what?
        -> loyalty 
  + I said no.
        -> punctuatedStitch10 

= no
<em>"No."</em>
  + Next
        -> iSsssseekSssssom 

= nowChuStartingTo
"Now, chu starting to think straight."
  + Next
        -> donchuGoForgetti 

= great
"Great"
  + Next
        -> donchuGoForgetti 

= punctuatedStitch12
"...."
  + ....
        -> well 

= iWasDoneStartWor
"I was done start worrying 'bout chu."
  + Next
        -> nowChuStartingTo 

= suddenlyDealingW
"Suddenly dealing with a pesky lil detective, are we?" *snicker*
  + Next
        -> allChuGottaDoIsS 

= iAmNoSimpleApoth
"I am no simple apothecary! No, No! 
  + Next
        -> iAmAHEALER 

= loyalty
<em>"Loyalty."</em>
  + Next
        -> pledgeYourLoyalt 

= punctuatedStitch10
<em>"....."</em>
  + Next
        -> ignoramusssss 

= iSsssseekSssssom
<em>"I ssssseek sssssomething far more valuable."</em>
  + Like what?
        -> loyalty 

= well
"Well..."
  + Next
        -> supposeICouldVis 

= allChuGottaDoIsS
"All chu gotta do is share some of those cute lil potions chu got back there."
  + ....
        -> well1 

= iAmAHEALER
"I am a HEALER!"
  + Next
        -> andTheThoughtOfC 

= punctuatedStitch6
"...."
  + Next
        -> chuDoneMessedUpk 

= pledgeYourLoyalt
<em>"Pledge your loyalty to the caussssse, and your problemsssss will be no more."</em>
  + I Pledge
        -> goodOnccccceWeMe 
  + No
        -> punctuatedStitch10 

= ignoramusssss
<em>"Ignoramusssss."</em>
Gedeon leaves.<em></em>
  + Next
        -> theDetectiveTheC 

= supposeICouldVis
"...suppose I could visit chu in the clink." *snicker*
  + Next
        -> seeChuLaterTiny 

= well1
"Well?"
  + Next
        -> wantMeToGetRidOf 

= andTheThoughtOfC
"And the thought of causing harm to even a <em>small <strong>rodent</strong>..." </em>
  + Next
        -> isJustTooMUCHFor 

= chuDoneMessedUpk
"Chu done messed up....<em>kid</em>"
Abrupt ending. Even if the week is not complete, the ending is received.
<strong>Abrupt Ending Unlocked: The Abandoned Potion Shop</strong>
    -> END

= goodOnccccceWeMe
<em>"Good. Onccccce we meet again. Know that your sssssituation will be resssssolved."</em>
<strong>Assassins Arc.</strong>
    -> END

= seeChuLaterTiny
"See chu later, Tiny."
The Corrupt Cop leaves.
  + Next
        -> theDetectiveTheC 

= wantMeToGetRidOf
"Want me to get rid of it or not?"
  + ...yes
        -> ItAintGonnaBeFre 
  + ...no
        -> chuckleChuMissin 

= isJustTooMUCHFor
<em>"...</em>is just <em>too <strong>MUCH </strong></em>For my gentle <em>soul</em>..." 
  + Next
        -> toTake 

= ItAintGonnaBeFre
 "It ain't gonna be free"
  + Fine
        -> freeOfTheGoodStu 
  + Forget it
        -> chuEitherGotGuts 

= chuckleChuMissin
*chuckle* "Chu missing a few screws, Tiny?"
  + Next
        -> theDetectiveOnCh 

= toTake
to <em>take</em>...."  
  + Next
        -> didHeBuyIt 

= freeOfTheGoodStu
"Free of the good stuff for me and my boi's."
  + Fine
        -> snickerNowWeInBu 
  + *Laugh in his face*
        -> theCorruptCopLoo1 
  + How about...
        -> howAboutADiscoun 

= theDetectiveOnCh
"The detective on chur rear."
  + Next
        -> iDontMakeItGoAwa 

= didHeBuyIt
<em>(Did he buy it?)</em>
  + Next
        -> theDetectiveStar4 

= snickerNowWeInBu
*snicker* "Now we in business."
  + Next
        -> considerIsDealtW 

= theCorruptCopLoo1
The Corrupt Cop looks pissed.
"Chu gonna regret that."
The Corrupt Cop leaves.
  + Next
        -> theDetectiveTheC 

= iDontMakeItGoAwa
"I don't make it go away..."
  + Next
        -> chuGonnaHaveToSt 

= theDetectiveStar4
The Detective stares.
Detective: "...."
  + Next
        -> yN 

= considerIsDealtW
"Consider is dealt with, Tiny." *snicker*
The Corrupt Cop leaves.
  + Next
        -> heyChuTheBossSen 

= yN
Y/N: "...."
  + Next
        -> detective2 

= detective2
Detective: "...."
  + Next
        -> yN1 

= yN1
Y/N: "...."
  + Next
        -> detectiveWell 

= detectiveWell
Detective: "Well...."
  + Next
        -> iDoDareSay 

= iDoDareSay
"I do <em><strong>dare </strong></em>say...."
  + Next
        -> wHATANADMIRABLEA 

= wHATANADMIRABLEA
"WHAT AN ADMIRABLE APOTHECARY!"
  + Next
        -> toStickToOnesPri 

= toStickToOnesPri
"To stick to one's principles with such GRAND <em><strong>PASSION!"</strong> </em>*sniff sniff* 
  + Next
        -> trulyINSPIRATION 

= trulyINSPIRATION
<em>"Truly INSPIRATIONAL</em><em>!</em>"
  + Next
        -> yOURFIREHASSETME 

= yOURFIREHASSETME
"YOUR FIRE HAS SET ME ABLAZE!"
  + Next
        -> pardonForHavingT 

= pardonForHavingT
"Pardon for having taken so much of your time already, grand apothecary."
  + Next
        -> iTooMustMakeGrea 

= iTooMustMakeGrea
"I, too, must make great haste and continue my investigation with exceptional <em>vigor </em>and <em>PASSION</em>!"
  + Next
        -> tATA 

= tATA
"TA-TA!
The Detective leaves."
  + Next
        -> aNoncorruptCopAp 

= aNoncorruptCopAp
A Noncorrupt Cop appears.
"...Zzzzzz"
  + Uh...Hello?
        -> oh 

= oh
"oh...."
  + Next
        -> goodDayHealer 

= goodDayHealer
"good day, healer"
  + Next
        -> myDetectiveHasSp 

= myDetectiveHasSp
"my detective has spoken...."
  + Next
        -> highlyOf 

= highlyOf
"highly of...." 
  + Next
        -> you 

= you
"you...."
  + Next
        -> zzzzzz1 

= zzzzzz1
"Zzzzzz"
  + HEY!
        -> oh1 

= oh1
"oh...."
  + Next
        -> apologiesHealer 

= apologiesHealer
"apologies, healer."
  + Next
        -> thoughMyDetectiv 

= thoughMyDetectiv
"Though my detective is great...."
  + Next
        -> iFindItDifficult 

= iFindItDifficult
"I find it difficult...."
  + Next
        -> toMaintain 

= toMaintain
"to maintain...."
  + Next
        -> theSame 

= theSame
"the same...."
  + Next
        -> energyyy 

= energyyy
"energyyy...."
  + Next
        -> leveZzzzz 

= leveZzzzz
"leve...Zzzzz"
  + This guy...HEY!
        -> oh2 
  + ....
        -> zzzzz 

= oh2
"oh...."
  + Next
        -> mightYou 

= zzzzz
"zzzzz"
  + OI!
        -> oh2 

= mightYou
"might you...."
  + Next
        -> haveSomething 

= haveSomething
"have something...."
  + Next
        -> toEnergize 

= toEnergize
"to energize..."
  + Next
        -> meZzzzzz 

= meZzzzzz
"me?....Zzzzzz"
  + Energy Potion
        -> oh3 
  + Health Potion
        -> oh4 
  + Death Potion
        -> oh5 

= oh3
"oh...."
  + Next
        -> thanks 

= oh4
"oh...."
  + Next
        -> thanks2 

= oh5
"oh...."
  + Next
        -> thanks1 

= thanks
"thanks...."
  + Next
        -> gulpGulp 

= thanks1
"thanks...."
  + Next
        -> bestGoCatchUpOnS 

= thanks2
"thanks..."
  + Next
        -> forNothing 

= gulpGulp
*gulp gulp*
  + Next
        -> punctuatedStitch16 

= bestGoCatchUpOnS
"best go catch up on some more paperwork...."
The Sleepy Cop leaves.
<strong>The Assassins Arc Begins</strong>
    -> END

= forNothing
"for nothing....
"i guess...."
*siiiiiiiiiiiiigh*
The Sleepy Cop leaves and never returns.
<strong>The Doctor Arc Begins</strong>
    -> END

= punctuatedStitch16
"...."
  + Next
        -> wOWZERS 

= wOWZERS
"WOWZERS!!!"
  + Next
        -> tHATSTHESTUFFRIG 

= tHATSTHESTUFFRIG
"THAT'S THE STUFF RIGHT THERE! NOW I FEEL LIKE I CAN CONQUER ANYTHING AND EVERYTHING THAT STANDS IN MY WAY! AHAHAHA! YES! THANK YOU THANK YOU!"
  + Next
        -> aSMYDETECTIVESAY 

= aSMYDETECTIVESAY
"AS MY DETECTIVE SAY!"
  + Next
        -> tATA1 

= tATA1
"TA-TA!!"
The Noncurrupt Cop leaves.
  + Next
        -> theSleepyCopRetu 

= theSleepyCopRetu
The Sleepy Cop returns.
"...."
  + Next
        -> healer 

= healer
"healer...."
  + Next
        -> need 

= need
"need..."
  + Next
        -> more 

= more
"more..."
  + Next
        -> magicEnergy 

= magicEnergy
"magic energy...."
  + Next
        -> juiceZzzzz 

= juiceZzzzz
"juice....Zzzzz"
  + Health Potion
        -> gulpGulp1 
  + Energy Potion
        -> gulpGulp2 

= gulpGulp1
*gulp gulp*
  + Next
        -> punctuatedStitch18 

= gulpGulp2
*gulp gulp*
  + Next
        -> tHANKYOUSEEYOUAG 

= punctuatedStitch18
"...."
  + Next
        -> zzzzz1 

= tHANKYOUSEEYOUAG
"THANK YOU!! SEE YOU AGAIN!! TA-TA!!"
Sleepy Cop leaves.
Returns for the remainder of the week as long as he receives an energy potion otherwise he stops appearing.
<strong>Ending Unlocked: The Energy to Get Away with It</strong>
    -> END

= zzzzz1
"Zzzzz"
  + HEY!
        -> oh4 