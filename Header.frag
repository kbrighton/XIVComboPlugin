# XIVComboPlugin
This plugin condenses combos and mutually exclusive abilities onto a single button. Thanks to Meli for the initial start, attick and daemitus for continuing, and obviously goat for making any of this possible.

![GitHub build status](https://img.shields.io/github/workflow/status/PrincessRTFM/XIVComboPlugin/Build?logo=github)
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/PrincessRTFM/XIVComboPlugin?label=version&color=informational)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/PrincessRTFM/XIVComboPlugin/master?label=updated)
[![GitHub issues](https://img.shields.io/github/issues-raw/PrincessRTFM/XIVComboPlugin?label=known%20issues)](https://github.com/PrincessRTFM/XIVComboPlugin/issues?q=is%3Aissue+is%3Aopen+sort%3Aupdated-desc)

## About
[![License](https://img.shields.io/github/license/PrincessRTFM/XIVComboPlugin?logo=github&color=informational&cacheSeconds=86400)](https://github.com/PrincessRTFM/XIVComboPlugin/blob/master/LICENSE)

XIVCombo is a plugin to allow for "one-button" combo chains, as well as implementing various other mutually-exclusive button consolidation and quality of life replacements.

For some jobs, this frees a massive amount of hotbar space (looking at you, DRG). For most, it removes a lot of mindless tedium associated with having to press various buttons that have little logical reason to be separate.

## Installation
Type `/xlplugins` in-game to access the plugin installer and updater. Note that you will need to add [my custom plugin repository](https://github.com/PrincessRTFM/MyDalamudPlugins) (full instructions included at that link) in order to find this plugin.

## In-game usage
* Type `/pcombo` to pull up a GUI for editing active combo replacements.
* Drag the named ability from your ability list onto your hotbar to use.
  * For example, to use DRK's Souleater combo, first check the box, then place Souleater on your bar. It should automatically turn into Hard Slash.
  * The description associated with each combo should be enough to tell you which ability needs to be placed on the hotbar.

### Examples
![](https://github.com/PrincessRTFM/XIVComboPlugin/raw/master/res/souleater_combo.gif)
![](https://github.com/PrincessRTFM/XIVComboPlugin/raw/master/res/hypercharge_heat_blast.gif)
![](https://github.com/PrincessRTFM/XIVComboPlugin/raw/master/res/eno_swap.gif)

## Why Another Fork?
Because the original fork developer (daemitus) has a different philosophy regarding how much the plugin should be allowed to do. They want to avoid "intelligent" decisions in the plugin, because they feel it's too close to botting. While I respect their decision and their reasoning, I also personally disagree with it, and additionally believe that since this plugin _only_ operates in PvE, there's no real harm or reason to restrict it like that. You aren't gaining an advantage over another player unless you're comparing parses, and even then nobody "wins" or "loses" anything. <!-- Although it could be said that everyone comparing parses like that loses, in a way :P -->

Furthermore, I've seen _so_ many people who want to play this game, but have some disability or another - carpal tunnel, for instance - that makes it hard for them to do so. It is my hope that my fork will make the game more accessible to people like that, thereby bringing in more players to enjoy it.

## Full list of supported combos
**Please note**: in the original fork, you _had_ to be on the listed job for replacements to function. You couldn't be on the base class, which means that the plugin wouldn't help until you reached level 30. This should now be fixed, so that applicable skills will update even on the base pre-job classes.

Now sorted by job and combo name!

