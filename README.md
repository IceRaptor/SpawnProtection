# Spawn Protection

This mod for the [HBS BattleTech](http://battletechgame.com/) provides significant defensive bonuses to units when they spawn. Units can be marked to receive a specific number of evasive pips and to be marked as braced. This makes for a better play experience on small maps where the player could be pummeled before they could even move.

Only Mechs and Vehicles receive this protection. Turrets do not, as they cannot move and thus the modifiers never go away.

This has been extracted from CWolf's amazing [Mission Control](https://github.com/CWolfs/MissionControl) mod - check it out! 

## Configurable Options

The following values can be tweaked in `mod.json` to customize your experience:
* **ApplyGuard:** If true, the protected unit will be Braced and thus gain the Guarded state. Defaults to true.
* **EvasionPips:** The number of evasion pips to add to the target unit. Defaults to 6.
* **ApplyToEnemies:** If true, enemies will be protected when they spawn as well. Defaults to true.
* **ApplyToNeutrals:** If true, neutrals will be protected when they spawn as well. Defaults to true.
* **ApplyToAllies:** If true, allies will be protected when they spawn as well. Defaults to true.
* **ApplyToReinforcements:** If true, enemy reinforces that spawn during a mission will be protected just like those that spawn at the start of a mission. Defaults to true.
