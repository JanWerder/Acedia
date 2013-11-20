Acedia - Design Document
======

Acedia is a Unity 3D platformer game, which is based around reflexes, foresight and luck. The player is set on a level, surrounded by deadly gaps, on platforms with varying heights and forced to dodge incoming missiles from the sky and enemies on the ground. While doing so the player has multiple ways of improving his score.



Camera
-----------

The camera displays the scene view and rotates around the level while being locked to the player regarding the distance. As a matter of that fact, it will be possible that the player is not able to see the whole level, deping on its size. The camera will rotate to the background music, meaning that in case the music gets faster and more intense, the camera move will too. That should create a feeling of immersion and set highlights in the gameplay, as well as pacing the gameplay naturally with slow and fast passages.

Player
---------
The players central role is highlighted by being the target of the camera. The player is moved by swiping in a direction. The player will move correspondly to it's current position and _not_ correspondent to the camera. This should create a confusion for new players, but also do the trick to set the gameplay apart. The movement requires constant dimensional thinking and planning, which should make the game reasonably hard to play. The player can be easily killed by almost everything:
- missiles
- roller
- falling of the level

Level
-------
The level is set in spaaace for no aparent reason. It consists out of a few planes and cubes which create the feeling of a spaaace-station look-alike. The spaaace setting is supported by incoming missles targeted onto certain tiles of the levels. The level on the other hands, detects the distance of the missles to the ground and displays a number, which acts like a countdown to the impact.

Enemies
------------

### Missiles ###
Missiles are automatically launched at certain tiles of the level. On impact missiles explode, but only kill the player if he stands on the tile the missile impacted on. missiles are alsways at the same speed, so the only variation regarding difficulty is the amount of missiles launched. The amount increases heavily over time until a certain point is reached, which is considered to be hard enough, from which point on, other enemies have to the trick.

### Rollers ###
Rollers are cylindrical enemies, which roll around the level. They have a fixed point a & b, through which the cycle. A roller doesn't react to the player, nor does is stop after killing it. A Roller is spawned by randomly dropping into the level like a missile. A roller can not be killed or harmed and will therefor kill itself by rolling of the stage or shrinking into nothing after a certain time, which is determined by the advance of the player.


## Powerups ##

Powerups randomly spawned on the level. There is no certain pattern regarding difficulty.

### Score Boost ###
The score boost give the player additional 1000 Points on pickup. The score amount is not scaled with difficulty, but with upgrades

### Invincible ###
Ad the name suggest, the invincible powerup, protects the player from all damage during its activation. Dropping of the stage, will respawn the player at a random location and running into a roller, will deflect him. The time is not scaled with difficulty, but with upgrades

### Speed ###
The speed boost, gives the player addional movement speed for a certain amount of seconds. The amount is not scaled with difficulty, but with upgrades
