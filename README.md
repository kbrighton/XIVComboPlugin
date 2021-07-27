# XIVComboPlugin
This plugin condenses combos and mutually exclusive abilities onto a single button. Thanks to Meli for the initial start, and obviously goat for making any of this possible.

## About
XIVCombo is a plugin to allow for "one-button" combo chains, as well as implementing various other mutually-exclusive button consolidation and quality of life replacements.

For some jobs, this frees a massive amount of hotbar space (looking at you, DRG). For most, it removes a lot of mindless tedium associated with having to press various buttons that have little logical reason to be separate.

## Installation
Type `/xlplugins` in-game to access the plugin installer and updater. Note that you will need to add [my custom plugin repository](https://github.com/PrincessRTFM/MyDalamudPlugins) (instructions included at that link) in order to find this plugin.

## In-game usage
* Type `/pcombo` to pull up a GUI for editing active combo replacements.
* Drag the named ability from your ability list onto your hotbar to use.
  * For example, to use DRK's Souleater combo, first check the box, then place Souleater on your bar. It should automatically turn into Hard Slash.
  * The description associated with each combo should be enough to tell you which ability needs to be placed on the hotbar.

### Examples
![](https://github.com/daemitus/xivcomboplugin/raw/master/res/souleater_combo.gif)
![](https://github.com/daemitus/xivcomboplugin/raw/master/res/hypercharge_heat_blast.gif)
![](https://github.com/daemitus/xivcomboplugin/raw/master/res/eno_swap.gif)

## Known Issues
* None, for now!

## Why Another Fork?
Because the original fork developer (daemitus) has a different philosophy regarding how much the plugin should be allowed to do. They want to avoid "intelligent" decisions in the plugin, because they feel it's too close to botting. While I respect their decision and their reasoning, I also personally disagree with it, and additionally believe that since this plugin _only_ operates in PvE, there's no real harm or reason to restrict it like that. You aren't gaining an advantage over another player unless you're comparing parses, and even then nobody "wins" or "loses" anything.

Furthermore, I've seen _so_ many people who want to play this game, but have some disability or another - carpal tunnel, for instance - that makes it hard for them to do so. It is my hope that my fork will make the game more accessible to people like that, thereby bringing in more players to enjoy it.

## Full list of supported combos
**Please note**: in the original fork, you _had_ to be on the listed job for replacements to function. You couldn't be on the base class, which means that the plugin wouldn't help until you reached level 30. This should now be fixed, so that applicable skills will update even on the base pre-job classes.

Now sorted by job and combo name!

| Job | Name | Description |
|-----|------|-------------|
| AST | Benefic 2 to Benefic Level Sync | Changes Benefic 2 to Benefic when below level 26. |
| AST | Draw on Play | Play turns into Draw when no card is drawn, as well as the usual Play behavior. |
| AST | Minor Arcana to Sleeve Draw | Changes Minor Arcana to Sleeve Draw when a card is not drawn. |
| AST | Swiftcast Ascend | Ascend turns into Swiftcast when it's off cooldown. |
| BLM | (Between the) Ley Lines | Change Ley Lines into BTL when Ley Lines is active. |
| BLM | Blizzard 1/2/3 | Blizzard 1 becomes Blizzard 3 when out of Umbral Ice. Freeze becomes Blizzard 2 when synced. |
| BLM | Despair | Despair replaces Fire 4 on Enochian switcher when below 2400 MP. |
| BLM | Enochian Stance Switcher | Change Enochian to Fire 4 or Blizzard 4 depending on stance. |
| BLM | Fire 1/3 | Fire 1 becomes Fire 3 outside of Astral Fire, OR when Firestarter proc is up. |
| BLM | Fire 3 to Fire 1 Feature | Fire 1 (and 3 if procced) will replace Fire 4 on Enochian Switcher. |
| BLM | Thunder | Thunder 1/3 replaces Enochian/Fire 4/Blizzard 4 on Enochian switcher. |
| BLM | Umbral Soul/Transpose Switcher | Change Transpose into Umbral Soul when Umbral Soul is usable. |
| BRD | Burst Shot/Quick Nock into Apex Arrow | Replaces Burst Shot and Quick Nock with Apex Arrow when gauge is full. |
| BRD | Heavy Shot into Straight Shot | Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced. |
| BRD | Iron Jaws Feature | Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up. |
| BRD | Wanderer's into Pitch Perfect | Replaces Wanderer's Minuet with Pitch Perfect while in WM. |
| DNC | AoE Multibutton | Change Windmill into procs and combos as available. |
| DNC | Dance Step Combo | Change Standard Step and Technical Step into each dance step while dancing. |
| DNC | Dance Step Feature | Change custom actions into dance steps while dancing. |
| DNC | Fan Dance Combos | Change Fan Dance and Fan Dance 2 into Fan Dance 3 while flourishing. |
| DNC | Flourish Proc Saver | Change Flourish into any available procs before using. |
| DNC | Single Target Multibutton | Change Cascade into procs and combos as available. |
| DRG | BOTD Into Stardiver | Replace Blood of the Dragon with Stardiver when in Life of the Dragon. |
| DRG | Chaos Thrust Combo | Replace Chaos Thrust with its combo chain. |
| DRG | Coerthan Torment Combo | Replace Coerthan Torment with its combo chain. |
| DRG | Full Thrust Combo | Replace Full Thrust with its combo chain. |
| DRG | Jump + Mirage Dive | Replace (High) Jump with Mirage Dive when Dive Ready. |
| DRK | Dark Knight Gauge Overcap Feature | Replace AoE combo with gauge spender if you are about to overcap. |
| DRK | Delirium Feature | Replace Souleater and Stalwart Soul with Bloodspiller and Quietus when Delirium is active. |
| DRK | Souleater Combo | Replace Souleater with its combo chain. |
| DRK | Stalwart Soul Combo | Replace Stalwart Soul with its combo chain. |
| GNB | Burst Strike to Bloodfest Feature | Replace Burst Strike with Bloodfest if you have no powder gauge. |
| GNB | Demon Slaughter Combo | Replace Demon Slaughter with its combo chain. |
| GNB | Fated Circle Feature | In addition to the Demon Slaughter combo, add Fated Circle when charges are full. |
| GNB | No Mercy Feature | Replace No Mercy with Bow Shock, and then Sonic Break, while No Mercy is active. |
| GNB | Solid Barrel Combo | Replace Solid Barrel with its combo chain. |
| GNB | Wicked Talon Combo | Replace Wicked Talon with its combo chain. |
| GNB | Wicked Talon Continuation | In addition to the Wicked Talon combo chain, put Continuation moves on Wicked Talon when appropriate. |
| MCH | (Heated) Shot Combo | Replace either form of Clean Shot with its combo chain. |
| MCH | Gauss Round / Ricochet Feature | Replace Gauss Round and Ricochet with one or the other depending on which has more charges. |
| MCH | Hypercharge Feature | Replace Heat Blast and Auto Crossbow with Hypercharge when not overheated. |
| MCH | Overdrive Feature | Replace Rook Autoturret and Automaton Queen with Overdrive while active. |
| MCH | Spread Shot Heat | Replace Spread Shot with Auto Crossbow when overheated. |
| MNK | Monk AoE Combo | Replaces Rockbreaker with the AoE combo chain, or Rockbreaker when Perfect Balance is active. |
| MNK | Monk Bootshine Feature | Replaces Dragon Kick with Bootshine if both a form and Leaden Fist are up. |
| NIN | Aeolian Edge Combo | Replace Aeolian Edge with its combo chain. |
| NIN | Armor Crush Combo | Replace Armor Crush with its combo chain. |
| NIN | Dream to Assassinate | Replace Dream Within a Dream with Assassinate when Assassinate Ready. |
| NIN | GCDs to Ninjutsu Feature | Every GCD combo becomes Ninjutsu while Mudras are being used. |
| NIN | Hakke Mujinsatsu Combo | Replace Hakke Mujinsatsu with its combo chain. |
| NIN | Hide to Mug | Replaces Hide with Trick Attack while under the effect of Suiton or Hidden, or else Mug if in combat. |
| NIN | Kassatsu Chi/Jin Feature | Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu. |
| NIN | Kassatsu to Trick | Replaces Kassatsu with Trick Attack while Suiton or Hidden is up. |
| NIN | Ten Chi Jin to Meisui | Replaces Ten Chi Jin (the move) with Meisui while Suiton is up. |
| PLD | Atonement Feature | Replace Royal Authority with Atonement when under the effect of Sword Oath. |
| PLD | Confiteor Feature | Replace Holy Spirit/Circle with Confiteor while MP is under 4000 and Requiescat is up. |
| PLD | Goring Blade Combo | Replace Goring Blade with its combo chain. |
| PLD | Prominence Combo | Replace Prominence with its combo chain. |
| PLD | Requiescat Confiteor | Replace Requiescat with Confiter while under the effect of Requiescat. |
| PLD | Requiescat Feature | Replace Royal Authority/Goring Blade combo with Holy Spirit and Prominence combo with Holy Circle while Requiescat is active. |
| PLD | Royal Authority Combo | Replace Royal Authority/Rage of Halone with its combo chain. |
| RDM | Red Mage AoE Combo | Replaces Veraero/Verthunder 2 with Impact when Dualcast or Swiftcast are active. |
| RDM | Redoublement Combo Plus | Replaces Redoublement with Verflare/Verholy after Enchanted Redoublement, whichever is more appropriate. Also replaces Redoublement with Scorch after Verflare/Verholy. |
| RDM | Redoublement combo | Replaces Redoublement with its combo chain, following enchantment rules. |
| RDM | Swiftcast Verraise | Verraise turns into Swiftcast when it's off cooldown and Dualcast isn't up. |
| RDM | Verproc into Jolt | Replaces Verstone/Verfire with Jolt/Scorch when no proc is available. |
| RDM | Verproc into Jolt Plus | Additionally replaces Verstone/Verfire with Veraero/Verthunder if dualcast/swiftcast are up. |
| RDM | Verproc into Jolt Plus Opener Feature | Turns Verfire into Verthunder when out of combat. |
| SAM | Gekko Combo | Replace Gekko with its combo chain. |
| SAM | Iaijutsu to Shoha | Replace Iaijutsu with Shoha when meditation is 3. |
| SAM | Iaijutsu to Tsubame-gaeshi | Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty. |
| SAM | Jinpu/Shifu Feature | Replace Meikyo Shisui with Jinpu or Shifu depending on what is needed. |
| SAM | Kasha Combo | Replace Kasha with its combo chain. |
| SAM | Mangetsu Combo | Replace Mangetsu with its combo chain. |
| SAM | Oka Combo | Replace Oka with its combo chain. |
| SAM | Seigan to Third Eye | Replace Seigan with Third Eye when not procced. |
| SAM | Tsubame-gaeshi to Iaijutsu | Replace Tsubame-gaeshi with Iaijutsu when Sen is empty. |
| SAM | Tsubame-gaeshi to Shoha | Replace Tsubame-gaeshi with Shoha when meditation is 3. |
| SAM | Yukikaze Combo | Replace Yukikaze with its combo chain. |
| SCH | ED Aetherflow | Change Energy Drain into Aetherflow when you have no more Aetherflow stacks. |
| SCH | Seraph Fey Blessing/Consolation | Change Fey Blessing into Consolation when Seraph is out. |
| SCH | Swiftcast Resurrection | Resurrection turns into Swiftcast when it's off cooldown. |
| SMN | Brand of Purgatory Combo | Replaces Fountain of Fire with Brand of Purgatory when under the affect of Hellish Conduit. |
| SMN | Demi-Summon Combiners Ultra | Dreadwyrm Trance, Summon Bahamut, Firebird Trance, Deathflare, Enkindle Bahamut, and Enkindle Phoenix are now one button. |
| SMN | Demi-summon combiners | Dreadwyrm Trance, Summon Bahamut, and Firebird Trance are now one button. |
| SMN | ED Fester | Change Fester into Energy Drain when out of Aetherflow stacks. |
| SMN | ES Painflare | Change Painflare into Energy Syphon when out of Aetherflow stacks. |
| SMN | Swiftcast Resurrection | Resurrection turns into Swiftcast when it's off cooldown. |
| WAR | Inner Release Feature | Replace Single-target and AoE combo with Fell Cleave/Decimate during Inner Release. |
| WAR | Mythril Tempest Combo | Replace Mythril Tempest with its combo chain. |
| WAR | Nascent Flash Feature | Replace Nascent Flash with Raw intuition when below level 76. |
| WAR | Storms Eye Combo | Replace Storms Eye with its combo chain. |
| WAR | Storms Path Combo | Replace Storms Path with its combo chain. |
| WAR | Warrior Gauge Overcap Feature | Replace Single-target or AoE combo with gauge spender if you are about to overcap and are before a step of a combo that would generate beast gauge. |
| WHM | Afflatus Feature | Changes Cure 2 into Afflatus Solace, and Medica into Afflatus Rapture, when lilies are up. |
| WHM | Cure 2 to Cure Level Sync | Changes Cure 2 to Cure when below level 30 in synced content. |
| WHM | Rapture into Misery | Replaces Afflatus Rapture with Afflatus Misery when Misery is ready to be used. |
| WHM | Solace into Misery | Replaces Afflatus Solace with Afflatus Misery when Misery is ready to be used. |
| WHM | Swiftcast Raise | Raise turns into Swiftcast when it's off cooldown. |
