// ---- The Assassins Arc ----
// Converted from original inklewriter URL:
// https://www.inklewriter.com/stories/191641
# title: The Assassins Arc
# author: Emely T.
// -----------------------------

VAR 6th_sense = false
VAR helpedcain = false
VAR antidote = false

-> gedeonAppears


==== gedeonAppears ====
Gedeon appears.
<em>"Greetingsssss sssssly one."</em>
  + Hello
        -> weHaveBeenObssss 

= weHaveBeenObssss
<em>"We have been obssssserving you."</em>
  + So?
        -> sssssoWeWantToAs 

= sssssoWeWantToAs
<em>"Ssssso, we want to asssssissssst you."</em>
  + How?
        -> doYouDreamOfRich 

= doYouDreamOfRich
<em>"Do you dream of richesssss?"</em>
  + Yes
        -> weCanGrantYouSss 
  + No
        -> ahSssssoWeAreThe 

= weCanGrantYouSss
<em>"We can grant you sssssaid wish."</em>
  + Next
        -> allYouMussssstDo 

= ahSssssoWeAreThe
<em>"Ah, ssssso we are the sssssame."</em>
  + Next
        -> wePlaccccceImpor 

= allYouMussssstDo
<em>"All you mussssst do isssss sssssimple."</em>
  + Next
        -> pledgeYourLoyalt 

= wePlaccccceImpor
<em>"We placcccce importanccccce on more valuable mattersssss."</em>
  + Next
        -> suchAsssss 

= pledgeYourLoyalt
<em>"Pledge your loyalty and allegianccccce to usssss." </em>
  + Next
        -> iAndAllYouDessss 

= suchAsssss
<em>"Such asssss..."</em>
  + Next
        -> loyalty 

= iAndAllYouDessss
<i>"And all you desssssire will become reality."</i>
  + I pledge
        -> fantassssstic 
  + Thanks, but no thanksssss
        -> gedeonLooksAngry 

= loyalty
<em>"loyalty..."</em>
  + Next
        -> wouldYouNotAgree 

= fantassssstic
<em>"Fantassssstic."</em>
  + Next
        -> nowYouMussssstPa 

= wouldYouNotAgree
<em>"Would you not agree?"</em>
  + I would
        -> excccccellent 
  + No, not really
        -> hmmmmm 
  + Why loyalty, when I can have money
        -> punctuatedStitch 

= gedeonLooksAngry
Gedeon looks angry.<em></em>
<i>"You dare mock usssss?!"</i>
  + Yesssss
        -> itSsssseemsssssW 
  + No, I pledge
        -> good 

= nowYouMussssstPa
<em>"Now, you mussssst passsss a tessssst."</em>
  + What test?
        -> weRequireYourSss 
  + Nevermind
        -> no 

= excccccellent
<em>"Excccccellent"</em>
  + Next
        -> pledgeYourAllegi 

= hmmmmm
<em>"Hmmmmm"</em>
  + Next
        -> thenPerhapsssss 

= punctuatedStitch
<em>"...."</em>
  + Next
        -> well 

= itSsssseemsssssW
<em>"It ssssseemsssss we were wrong about you after all." </em>
  + Next
        -> iHowDisssssappoi 

= good
<em>"Good."</em>
  + Next
        -> nowYouMussssstPa 

= weRequireYourSss
<em>"We require your ssssstrongest poissssson for tesssssting."</em>
  + Death Potion
        -> excccccellent1 
  + Non-death Potion
        -> hmmmmm1 

= no
<em>"No"</em>
  + Next
        -> youMussssst 

= pledgeYourAllegi
<em>"Pledge your allegianccccce to the most loyal of guildsssss."</em>
  + I Pledge
        -> fantassssstic 
  + Nahh, I don't wanna
        -> gedeonLooksAngry1 

= thenPerhapsssss
<em>"Then perhapsssss..."</em>
  + Next
        -> weDifferFarGreat 

= well
<em>"Well..."</em>
  + Next
        -> weCanGrantYouSss 

= iHowDisssssappoi
<i>"How disssssappointing."</i>
Gedeon leaves.
<strong>Defaults to Normal Arc.</strong>
    -> END

= excccccellent1
<em>"Excccccellent"</em>
Gedeon leaves.<em></em>
  + Next
        -> deidamiaAppears 

= youMussssst
<em>"You mussssst."</em>
  + Next
        -> youHaveAlreadyPl 

= hmmmmm1
<em>"Hmmmmm"</em>
  + Next
        -> weFear 

= weDifferFarGreat
<em>"We differ far greater than I had anticccccipated."</em>
  + Next
        -> iHowDisssssappoi 

= gedeonLooksAngry1
Gedeon looks angry.
<i>"You dare wassssste my time?"</i>
  + I just don't want to join
        -> punctuatedStitch2 
  + Yes
        -> theRoomDarkens 

= youHaveAlreadyPl
<em>"You have already pledged."</em>
  + Well, okay then
        -> youSsssseeReasss 
  + Well, I un-pledge
        -> theOnlyWayOut 

= weFear
<em>"We fear..."</em>
  + Next
        -> weFindYourWork 

= deidamiaAppears
Deidamia appears<em></em>
<em>"...."</em>
  + Next
        -> heeell 

= punctuatedStitch2
<em>"...."</em>
  + Next
        -> thenIWillAllowYo 

= theRoomDarkens
The room darkens.
A random amount of time passes.
Gedeon's snake appears.
<strong>Abrupt Ending Unlocked: Maybe don't piss off the assassin? (Gedeon Edition)</strong>
    -> END

= youSsssseeReasss
<em>"You sssssee reassssson."</em>
  + Next
        -> veryGood 

= weFindYourWork
<em>"We find your work..."</em>
  + Next
        -> sssssubssssstand 

= theOnlyWayOut
<em>"The only way out."</em>
  + Next
        -> isssssThroughThe 

= thenIWillAllowYo
<em>"Then I will allow you to live."</em>
Gedeon leaves.
<strong>Defaults to Normal Arc.</strong><em></em>
    -> END

= veryGood
<em>"Very good."</em>
  + Next
        -> iNowi 

= sssssubssssstand
<em>"...sssssubssssstandard."</em>
  + Next
        -> theGuildHasssssN 

= isssssThroughThe
<em>"Isssss through the passsssing of life."</em>
  + Well, where is it
        -> punctuatedStitch3 
  + Okay
        -> thenAllowMe 
  + So, what's this test?
        -> weRequireYourSss 

= heeell
<em>"heeell...."</em>
  + Next
        -> ooo 

= iNowi
<i>"Now."</i>
  + Next
        -> weRequireYourSss 

= theGuildHasssssN
<em>"The guild hasssss no ussssse for unssssskilled membersssss."</em>
  + Next
        -> youAreHereby 

= punctuatedStitch3
<em>"...."</em>
  + Next
        -> perhapsssssIHave 

= thenAllowMe
<em>"Then allow me..."</em>
  + Next
        -> theRoomDarkens1 

= ooo
<em>"ooo..."</em>
  + Hey
        -> youuuHaaaveeePle 

= youAreHereby
<em>"You are hereby..."</em>
  + ....
        -> releasssssedOfYo 
  + WAIT!
        -> punctuatedStitch4 

= perhapsssssIHave
<em>"Perhapsssss I have confusssssed you."</em>
  + Next
        -> insteadOfASssssl 

= theRoomDarkens1
The room darkens.
A random amount of time passes.
A snake appears.
<strong>Abrupt Ending Unlocked: Releassssed From Duty: Indefinitely</strong>
    -> END

= youuuHaaaveeePle
<em>"Youuu haaaveee pleeedgeeed."</em>
  + Next
        -> aaandYouuurTeees 

= releasssssedOfYo
<em>"releasssssed of your resssssponsssssibilitiesssss..."</em>
  + Next
        -> theRoomDarkens1 

= insteadOfASssssl
<em>"Instead of a sssssly one, you appear to be a mere ignoramusssss."</em>
  + Next
        -> whatIMeantYoungO 

= punctuatedStitch4
<em>"...."</em>
  + Next
        -> yesssss 

= aaandYouuurTeees
<em>"Aaand youuur teeessst."</em>
  + Next
        -> waaasssDeeemeeed 

= whatIMeantYoungO
<em>"What I meant, young one."</em>
  + Next
        -> isssssThatTheOnl 

= yesssss
<em>"Yesssss?"</em>
  + Give me two days
        -> punctuatedStitch5 

= waaasssDeeemeeed
<em>"Waaasss deeemeeed..."</em>
  + Next
        -> hmmm 

= isssssThatTheOnl
<em>"Isssss that the only way to leave the asssssasssssin's guild..."</em>
  + Next
        -> isssssDeath 

= punctuatedStitch5
<em>"...."</em>
  + Next
        -> youGetOne 

= hmmm
<em>"Hmmm..."</em>
  + Next
        -> tOleeerableee 

= isssssDeath
<em>"Isssss death."</em>
  + ...oh
        -> confirmed 
  + I knew that
        -> ifYouInsssssisss 

= youGetOne
<em>"You get one."</em>
  + Next
        -> doNotDisssssappo 

= tOleeerableee
<em>"tOleeerableee."</em>
  + Next
        -> andSooo 

= doNotDisssssappo
<em>"Do not disssssappoint."</em>
Gedeon leaves.
  + Next
        -> gedeonReturns 

= confirmed
<em>"Confirmed."</em>
  + Next
        -> ignorumasssss 

= ifYouInsssssisss
<em>"If you insssssissssst."</em>
  + Next
        -> iNowi 

= andSooo
<em>"And sooo..."</em>
  + Next
        -> iveComeToCaaallU 

= ignorumasssss
<em>"Ignorumasssss."</em>
  + Next
        -> iNowi 

= iveComeToCaaallU
<em>"I've come to caaall upooon the pleeedgeee."</em>
  + Next
        -> aDeathPotionAppe 

= gedeonReturns
Gedeon returns.
<em>"Greetingssss, little ignoramusssss." </em>
  + Hello
        -> iHopeForYourSsss 
  + Who are you again?
        -> youLiveUpToYourN 

= aDeathPotionAppe
A death potion appears on the counter.
<em>"Give that to the oneee naaameeeD."</em>
  + Next
        -> punctuatedStitch6 

= iHopeForYourSsss
<em>"I hope, for your sssssake, that you have lived up to your purpossssse."</em>
  + Next
        -> now 

= youLiveUpToYourN
<em>"You live up to your nickname."</em>
  + Next
        -> nowEnoughPlaying 

= punctuatedStitch6
<em>"..."</em>
  + Next
        -> caaaiiin 

= now
<em>"Now."</em>
  + Next
        -> whereIsssssIt 

= nowEnoughPlaying
<em>"Now, enough playing gamesssss."</em>
  + Next
        -> whereIsssssIt 

= caaaiiin
<em><strong>"Caaaiiin"</strong></em>
  + Next
        -> heWearsssThisssH 

= whereIsssssIt
<em>"Where isssss it."</em>
  + Death Potion
        -> punctuatedStitch7 
  + Other Potion
        -> punctuatedStitch8 

= heWearsssThisssH
<em>"He wearsss thisss <strong>hideousss </strong>bolerooo haaat aaand..."</em>
  + Next
        -> ressspiratooor 

= punctuatedStitch7
<em>“….”</em>
  + Next
        -> good1 

= punctuatedStitch8
"...."
  + Next
        -> youHaveDaredToWa 

= ressspiratooor
<em>"Ressspiratooor."</em>
  + Next
        -> heeeavensss 

= good1
<em>“Good”</em>
Gedeon leaves.
  + Next
        -> deidamiaAppears 

= youHaveDaredToWa
<em>"You have dared to wassssste my time."</em>
  + Next
        -> perhapsssssYouWi 

= heeeavensss
<em>"Heeeavensss"</em>
  + Next
        -> iHaaateThatMaaan 

= perhapsssssYouWi
<em>"Perhapsssss you will wisssssen up in your next life."</em>
  + Next
        -> theRoomDarkens 

= iHaaateThatMaaan
<em>"I haaate that maaan."</em>
  + Next
        -> killlHimmm 

= killlHimmm
<em>"Killl himmm."</em>
  + Next
        -> befoooreHeee 

= befoooreHeee
<em>"Befooore heee..."</em>
  + Next
        -> killsssAlllOfUss 

= killsssAlllOfUss
<em>"Killsss alll of usss."</em>
  + Next
        -> yeaaa 

= yeaaa
<em>"Yeaaa?"</em>
  + ...
        -> heeell1 
  + Got it
        -> goood 
  + No
        -> punctuatedStitch9 

= heeell1
<em>"Heeell..."</em>
  + Next
        -> oooo 

= goood
<em>"Goood."</em>
Deidamia leaves.<em></em>
  + Next
        -> cainAppears 

= punctuatedStitch9
<em>"...."</em>
  + Next
        -> teeellMeee 

= oooo
<em>"oooo..."</em>
  + Next
        -> wakeUuup 

= teeellMeee
<em>"Teeell meee..."</em>
  + Next
        -> doooYouuuWishhhT 

= wakeUuup
<em>"Wake uuup."</em>
  + Next
        -> werrreSsspeaking 

= doooYouuuWishhhT
<em>"Dooo youuu wishhh to diiie?"</em>
  + ....
        -> yesssOrNooo 
  + Yes
        -> punctuatedStitch10 
  + No
        -> theeennnDoooAsss 

= cainAppears
Cain appears.
"Greetings partner, K-k-kshhhhh"
  + Next
        -> fearIGotNickedGa 

= werrreSsspeaking
<em>"We'rrre ssspeakinggg heeerrre"</em>
  + Oops
        -> deidamiaLooksAng 
  + Right, got it
        -> goood 

= yesssOrNooo
<em>"Yesss or Nooo?"</em>
  + Yes
        -> punctuatedStitch10 
  + No
        -> theeennnDoooAsss 

= punctuatedStitch10
<em>"...."</em>
  + Next
        -> welll 

= theeennnDoooAsss
<em>"Theeennn dooo asss you'rrre tooold."</em>
Deidamia leaves.<em></em>
  + Next
        -> cainAppears 

= deidamiaLooksAng
Deidamia looks angry.<em></em>
<i>"..."</i>
  + Next
        -> ooopsss 

= welll
<em>"Welll...."</em>
  + Next
        -> ifYouInsssissst 

= fearIGotNickedGa
"Fear I got nicked 'gain."
  + Next
        -> butImAboveSnakes 

= ifYouInsssissst
<em>"If you insssissst."</em>
  + Next
        -> theRoomDarkens1 

= butImAboveSnakes
"But I'm above snakes yet 'gain K-kshhh"
  + Next
        -> iHearYaGotTheBes 

= ooopsss
<em>"Ooopsss?!"</em>
  + Next
        -> whaaatDoooYouuuM 

= iHearYaGotTheBes
"I hear ya got the best antidotes money can buy 'round these parts."
  + Next
        -> justWonderingIfT 

= whaaatDoooYouuuM
<em>"Whaaat dooo youuu meeeannn...."</em>
  + Next
        -> oooopssss 

= justWonderingIfT
"Just wondering if that were true? K-kshhh"
  + Yes
        -> thatRight 
  + No
        -> mightyShame 

= oooopssss
<em><strong>"Oooopssss?!!"</strong></em>
  + I'm kidding!
        -> youuuBessstBeee 
  + I was ignoring you
        -> deidamiaLooksPis 

= thatRight
"That right?"
  + Next
        -> wellInThatCaseKk 

= mightyShame
"Mighty shame."
  + ....
        -> butISupposeNotAl 
  + I've got something better
        -> imListeningKkshh 

= youuuBessstBeee
<em>"Youuu bessst beee."</em>
  + Next
        -> ifYouuuVaaaluuue 

= deidamiaLooksPis
Deidamia looks pissed.
<em>"Are you atteeempting to enraaage usss!!"</em>
  + Next
        -> punctuatedStitch11 

= wellInThatCaseKk
"Well, in that case...K-k-kshhhhh"
  + Next
        -> mindHelpingABrot 

= butISupposeNotAl
"But I 'suppose not all rumors can be true K-k-kshhhh"
  + Next
        -> bestOfLuckToYa 

= imListeningKkshh
"I'm listening k-kshhh"
  + 6th Sense
        -> wellIllBe 
  + Other Potion
        -> punctuatedStitch14 

= ifYouuuVaaaluuue
<em>"If youuu vaaaluuue your liiife."</em>
  + Next
        -> iRecommmennnD 

= mindHelpingABrot
"Mind helping a brother out?"
  + Deidamia's Poison
        -> muchObligedKkshh 
  + Antidote
        -> mightyKindOfYaKk 

= bestOfLuckToYa
"Best of luck to ya."
Cain leaves.
  + Next
        -> gedeonAppearsLoo 

= wellIllBe
"Well, I'll be..."
  ~ 6th_sense = true
  ~ helpedcain = true
  + Next
        -> ifThisWorksAsInt 

= punctuatedStitch14
"...."
  + Next
        -> kkkshhhhh 

= punctuatedStitch11
"..."
  + Next
        -> deidamiaReturnsT 

= iRecommmennnD
<em>"I recommmennnD..."</em>
  + Next
        -> noooTTeeesssting 

= muchObligedKkshh
"Much obliged K-kshhh"
Cain leaves.
  + Next
        -> deidamiaReturns 

= mightyKindOfYaKk
"Mighty kind of ya K-kshhh"
  ~ antidote = true
  ~ helpedcain = true
  + Next
        -> illBeSeeingYaRou 

= ifThisWorksAsInt
"If this works as intended? K-k-kshhhhh"
  + Next
        -> wellIReckonYoull 

= kkkshhhhh
"K-k-kshhhhh"
  + Next
        -> wellThereAintNoN 

= deidamiaReturnsT
Deidamia returns to looking angry.
<em>"Becaussse it'sss nooot going to wooork."</em>
  + Just calm down, sweety
        -> theRoomDarkens2 
  + ....
        -> ifYouuuVaaaluuue 

= noooTTeeesssting
<em>"noooT teeesssting usss."</em>
  + Not even a little?
        -> theRoomDarkens3 
  + I understand
        -> goood 

= gedeonAppearsLoo
Gedeon appears looking angry.
<em>"It has come to my attention that you have failed at your asssssignment."</em>
  + Next
        -> failureIsNotAccc 

= illBeSeeingYaRou
"I'll be seeing ya 'round."
Cain leaves.
  + Next
        -> theRoomDarkens4 

= wellIReckonYoull
"Well, I reckon you'll have yourself a loyal customer."
  + Next
        -> muchObligedKkshh1 

= wellThereAintNoN
"Well, there ain't no need to lie to me, partner."
  + Next
        -> iBestBeOffNowKks 

= theRoomDarkens3
The room darkens.
"...."
  + ....
        -> theRoomDarkensEv 
  + I'll take that as a no
        -> goood 

= deidamiaReturns
Deidamia returns.
<em> "I aaam pleeeasssed."</em>
  + Next
        -> youuHaaveDooonnn 

= muchObligedKkshh1
"Much obliged K-kshhh'"
Cain leaves
  + Next
        -> gedeonAppearsLoo 

= iBestBeOffNowKks
"I best be off now k-kshhh"
Cain leaves.
  + Next
        -> gedeonAppearsLoo 

= theRoomDarkens2
The room darkens.
Deidamia looks pissed.
".........."
  + Next
        -> foool 

= theRoomDarkens4
The room darkens.
    -> gedeonAppearsLoo

= failureIsNotAccc
<em>"Failure is <strong>not </strong>accccceptable."</em>
  + Next
        -> theRoomDarkensEv1 

= theRoomDarkensEv
The room darkens even more.
Random amount of time passes.
Diedamia's Snake appears.
<em><strong>"DIIIE!!"</strong></em>
<strong>Abrupt Ending Unlocked: Maybe don't piss off the assassin? (Deidamia Edition)</strong>
    -> END

= youuHaaveDooonnn
<em>"Youu haave dooonnne <strong>veeerrry </strong>weeelll."</em><em></em>
  + Next
        -> aCoinBagAppearsO 

= theRoomDarkensEv1
The room darkens even more.
<em>"Now, you mussssst die!"</em>
  + {helpedcain} Next
        -> aSlashCrossesThe 
  + {not helpedcain} Next
        -> gedeonsSnakeAppe1 

= aCoinBagAppearsO
A coin bag appears on your counter.
"Treeeat yourrrssseeelf to sooomethiiing niiiccce, yeaaa?"
Deidamia leaves.
  + Next
        -> mysteryAssassins 

= foool
<em>"Foool."</em>
  + Next
        -> theRoomDarkensEv 

= aSlashCrossesThe
A slash crosses the room.
The room lightens.
Gedeon looks surprised.
A red line appears across him and begins to bleed.
His top body slides to the side and falls to the ground with a thud.
A moment of silence.
Cain appears.
"...."
  + Next
        -> kkkshhhhh1 

= gedeonsSnakeAppe1
Gedeon's Snake appears.
<strong>Abrupt Ending Unlocked: Helped another, but at what cost?</strong>
    -> END

= mysteryAssassins
Mystery Assassins appear throughout the remainder of the week asking for random potions.
<em>"Greetingssss, I require a potion that will affect my targetssss negatively."</em>
  + Correct Potion
        -> thankssss 
  + Incorrect Potion
        -> punctuatedStitch15 

= thankssss
<em>"Thankssss."</em>
The Mystery Assassin leaves.<em></em>
The Assassins continue to appear.
<strong>Ending Unlocked: Menacccce to Ssssocccciety</strong>
    -> END

= punctuatedStitch15
<em>"...."</em>
  + Next
        -> failureIssssNotT 

= failureIssssNotT
<em>"Failure issss not tolerated."</em>
A slash crosses the room.
<strong>Abrupt Ending Unlocked: Terminated. Reason: Failure to meet sssstandardssss.</strong><em></em>
    -> END

= kkkshhhhh1
"K-k-kshhhhh"
  + Next
        -> wellIllBe1 

= wellIllBe1
"Well, I'll be..."
  + Next
        -> hesUglierThanABu 

= hesUglierThanABu
"He's uglier than a burned boot. K-kshhh"
  + ....
        -> nowDontYaGoLooki 

= nowDontYaGoLooki
"Now, don't ya go looking at me like that partner."
  + Next
        -> imHandsomeAsCanB 

= imHandsomeAsCanB
"I'm handsome as can be. k-kshhh"
  + Next
        -> aCoinBagAppearsO1 

= aCoinBagAppearsO1
A coin bag appears on your counter.
"For the clean-up."
  + Next
        -> yourCatSitsUp 

= yourCatSitsUp
Your cat sits up.
"Don't mind if I do."
  + Next
        -> yourCatsEyesGlow 

= yourCatsEyesGlow
Your cat's eyes glow, and both the coin bag and body disappear. 
Your cat returns to sleep.
  + Next
        -> kkshhhNowForWhat 

= kkshhhNowForWhat
"K-kshhh. Now for what I was really here for..."
  + Next
        -> mindSellingMeNot 

= mindSellingMeNot
"Mind selling me 'nother of the same potion?"
  + Antidote
        -> muchObligedKkshh2 
  + 6th Sense
        -> muchObligedKkshh3 
  + Health Potion
        -> wellISupposeISho 

= muchObligedKkshh2
{ antidote:
    "Much obliged. K-kshhh"
}
{ 6th_sense:
    "Well, I suppose this works too. K-kshhh"
}
  + Next
        -> andDontYaWorryBo 

= muchObligedKkshh3
{ 6th_sense:
    "Much obliged. K-kshhh"
}
{ antidote:
    "Well I'll be, this is better. Much obliged. K-kshhh"
}
  + Next
        -> andDontYaWorryBo 

= wellISupposeISho
"Well, I suppose I shoulda specified. K-kshhh"
  + Next
        -> goodLuckPartner 

= goodLuckPartner
"Good luck, partner."
  + Next
        -> theRoomDarkens5 

= andDontYaWorryBo
"And don't ya worry 'bout them slithering snakes. As long as you do good by me, I'll make sure they don't bother ya."
  + Next
        -> seeYouRoundPartn 

= theRoomDarkens5
The room darkens.
A Mystery Assassin appears.
<em>"You will pay for what you've done."</em>
A snake appears.
<strong>Abrupt Ending Unlocked: Revenge of the Assassin</strong><em></em>
    -> END

= seeYouRoundPartn
"See you 'round, partner. K-k-kshhhhh"
  + Next
        -> cainReappearsThr 

= cainReappearsThr
Cain reappears throughout the week.
"K-kshhh the usual please."
  + Correct Potion
        -> seeYaRoundPartne 
  + Incorrect Potion
        -> punctuatedStitch17 

= seeYaRoundPartne
"See ya 'round partner."
appears until the end of the week.
<strong>Ending Unlocked: Protective Services</strong><em></em>
    -> END

= punctuatedStitch17
"...."
  + Next
        -> wellISupposeISho1 

= wellISupposeISho1
"Well, I suppose I shoulda specified. K-kshhh"
Cain leaves and never returns.
  + Next
        -> theRoomDarkens5 